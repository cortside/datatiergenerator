using System;
using System.Collections;
using System.IO;

using IvanZ.CSParser;
using Mono.CSharp;
using System.CodeDom;

using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Text;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Attribute;


namespace Spring2.DataTierGenerator.Generator.Writer {

    /// <summary>
    /// Wrapper for creating and merging CodeCompileUnits
    /// </summary>
    public class CodeUnit {

	private ArrayList src;
	private CodeCompileUnit unit;
	private IList log;

	public CodeUnit(String filename, Stream stream, IList log) {
	    this.log = log;

	    StringWriter sw =new StringWriter();
	    System.IO.TextWriter cout = Console.Out;
	    System.IO.TextWriter cerr = Console.Error;
	    Console.SetOut(sw);
	    Console.SetError(sw);
	    CSharpParser p = new CSharpParser(filename, stream, null);

	    Console.SetOut(cout);
	    Console.SetError(cerr);
			
	    int errno= p.parse();

	    if (errno>0 || Report.Warnings>0) {
		log.Add(sw.ToString());
	    }

	    unit = p.Builder.CurrCompileUnit;
	    stream.Position=0;
	    src = GetSourceLines(new StreamReader(stream).ReadToEnd());

	    ExtractMemberBodies();
	}

	public CodeCompileUnit Unit {
	    get { return this.unit; }
	}

	private ArrayList GetSourceLines(String source) {
	    ArrayList lines = new ArrayList();
	    StringReader reader = new StringReader(source);
	    
	    String line = reader.ReadLine();
	    while (line != null) {
		lines.Add(line);
		line = reader.ReadLine();
	    }
	    return lines;
	}

	private String GetSource(Member member) {
	    StringBuilder sb = new StringBuilder();
	    for (Int32 i=member.FirstLine; i<=member.LastLine && i<src.Count; i++) {
		sb.Append(src[i-1].ToString()).Append(Environment.NewLine);
	    }

	    String hold = sb.ToString();

	    String s = sb.ToString();
	    s = s.Substring(s.IndexOf("{")+1);

	    ArrayList source = GetSourceLines(s);
	    // remove the comment and attribute lines that belong to the next method
	    while (source[source.Count-1].ToString().Trim().StartsWith("#region") || source[source.Count-1].ToString().Trim().StartsWith("//") || source[source.Count-1].ToString().Trim().StartsWith("[") || source[source.Count-1].ToString().Trim().Equals(String.Empty)) {
		if (source[source.Count-1].ToString().Trim().StartsWith("#region")) {
		    member.HasBeginRegion = true;
		    member.RegionName = source[source.Count-1].ToString().Trim().Substring(7).Trim();
		}
		source.RemoveAt(source.Count-1);
	    }

	    if (source[source.Count-1].ToString().Trim().Equals("}")) {
		source.RemoveAt(source.Count-1);
	    } else {
		String line = source[source.Count-1].ToString();
		if (line.LastIndexOf("}")>=0) {
		    source[source.Count-1] = line.Substring(0,line.LastIndexOf("}")-1);
		} else {
		    throw new Exception("could not find closing brace: " + hold);
		}
	    }

	    // if this is the last member, remove the extra curly brace
	    if (member.LastLine>=9999) {
		// remove emtpy lines at the end of the method block
		while (source[source.Count-1].ToString().Trim().Equals(String.Empty)) {
		    source.RemoveAt(source.Count-1);
		}

		if (source[source.Count-1].ToString().Trim().Equals("}")) {
		    source.RemoveAt(source.Count-1);
		} else {
		    // remove #endregion lines
		    while (source[source.Count-1].ToString().Trim().StartsWith("#endregion")) {
			member.HasEndRegion = true;
			source.RemoveAt(source.Count-1);
		    }

		    String line = source[source.Count-1].ToString();
		    if (line.LastIndexOf("}")>=0) {
			source[source.Count-1] = line.Substring(0,line.LastIndexOf("}")-1);
		    } else {
			throw new Exception(">>>" + s + "<<<");
		    }
		}
	    }

	    // remove emtpy lines at the beginning of the method block
	    while (source.Count > 0 && source[0].ToString().Trim().Equals(String.Empty)) {
		source.RemoveAt(0);
	    }

	    sb = new StringBuilder();
	    foreach(String line in source) {
		sb.Append(line).Append(Environment.NewLine);
	    }

	    s= sb.ToString().Trim();
	    return s;
	}

	private String GetGetSource(Member member) {
	    String body = GetSource(member);

	    // no get statement
	    if (!body.StartsWith("get")) {
		return String.Empty;
	    } else {
		body = body.Substring(3).Trim();
		body = body.Substring(1).Trim();

		Int32 count = 0;
		StringBuilder sb = new StringBuilder();
		while (count >=0 && body.Length>0) {
		    if (body.Substring(0,1).Equals("{")) {
			count++;
		    }
		    if (body.Substring(0,1).Equals("}")) {
			count--;
		    }
		    sb.Append(body.Substring(0,1));
		    body = body.Substring(1);
		}
		body = sb.ToString().Trim();
		return body.Substring(0,body.Length-1).Trim();
	    }
	}

	private String GetSetSource(Member member) {
	    String body = GetSource(member).Trim();

	    // no get statement
	    if (!body.StartsWith("get")) {
		if (body.StartsWith("set")) {
		    body = body.Substring(3).Trim();
		    body = body.Substring(1, body.Length-2).Trim();

		    return body;
		}
	    } else {
		body = body.Substring(3).Trim();
		body = body.Substring(1).Trim();

		Int32 count = 0;
		while (count >=0 && body.Length>0) {
		    if (body.Substring(0,1).Equals("{")) {
			count++;
		    }
		    if (body.Substring(0,1).Equals("}")) {
			count--;
		    }
		    body = body.Substring(1).Trim();
		}

		if (body.StartsWith("set")) {
		    body = body.Substring(3).Trim();
		    body = body.Substring(1, body.Length-2).Trim();

		    return body;
		}

	    }

	    return String.Empty;
	}

	private void ExtractMemberBodies() {
	    ArrayList members1 = GetMembers(unit);

	    foreach(Member member in members1) {
		log.Add("found " + (member.Generate ? "[generate] " : "") + member.Element.GetType().FullName + ": " + member.Element.Name + " starting at line " + member.FirstLine.ToString() + " and ending on line " + member.LastLine.ToString());
		if (member.Element is CodeMemberMethod) {
		    String s = GetSource(member);
		    Int32 index = member.Type.Members.IndexOf(member.Element);
		    if (member.HasBeginRegion) {
			//log.Add("found #region " + member.RegionName);
			//member.Type.Members.Insert(index, new CodeSnippetTypeMember("#region " + member.RegionName));

		    }
		    ((CodeMemberMethod)member.Element).Statements.Clear();
		    ((CodeMemberMethod)member.Element).Statements.Add(new CodeSnippetStatement(s));
		    if (member.HasEndRegion) {
			//log.Add("found #endregion");
			//member.Type.Members.Insert(index, new CodeSnippetTypeMember("#endregion"));
		    }
		} else if (member.Element is CodeMemberProperty) {
		    ((CodeMemberProperty)member.Element).GetStatements.Clear();
		    ((CodeMemberProperty)member.Element).SetStatements.Clear();
		    String get = GetGetSource(member);
		    if (!get.Equals(String.Empty)) {
			((CodeMemberProperty)member.Element).GetStatements.Add(new CodeSnippetStatement(get));
		    }
		    String set = GetSetSource(member);
		    if (!set.Equals(String.Empty)) {
			((CodeMemberProperty)member.Element).SetStatements.Add(new CodeSnippetStatement(set));
		    }
		} else {
		    throw new Exception("Can't exctract body from unknown member type: " + member.Element.GetType().FullName);
		}
	    }
	}

	public void Merge(CodeUnit mergeUnit) {
	    // add new namespaces
	    // add imports
	    // add types

	    // add members

	    foreach(CodeNamespace mergeNamespace in mergeUnit.Unit.Namespaces) {
		if (!HasNamespace(unit, mergeNamespace.Name)) {
		    unit.Namespaces.Add(mergeNamespace);
		} else {
		    CodeNamespace ns = GetNamespace(unit, mergeNamespace.Name);
		    foreach(CodeNamespaceImport import in mergeNamespace.Imports) {
			if (!HasImport(ns, import.Namespace)) {
			    ns.Imports.Add(import);
			}
		    }
		    foreach(CodeTypeDeclaration mergeType in mergeNamespace.Types) {
			if (!HasType(ns, mergeType.Name)) {
			    ns.Types.Add(mergeType);
			} else {
			    CodeTypeDeclaration type = GetType(ns, mergeType.Name);
			    // TODO: attributes
			    // TODO: basetypes
			    // TODO: comments
			    // TODO: custom attributes
			    // TODO: type attributes
			    foreach(CodeTypeMember mergeMember in mergeType.Members) {
				if (!HasMember(type, mergeMember)) {
				    //log.Add("adding member: " + mergeMember.Name);
				    type.Members.Add(mergeMember);
				} else {
				    UpdateMember(type, mergeMember);
				}
			    }
			}
		    }

		}
	    }

	    // search for members with the Generate attribute that are no longer in the in the generated code
	    foreach(CodeNamespace mergeNamespace in unit.Namespaces) {
		if (HasNamespace(mergeUnit.Unit, mergeNamespace.Name)) {
		    CodeNamespace ns = GetNamespace(mergeUnit.Unit, mergeNamespace.Name);
		    foreach(CodeTypeDeclaration mergeType in mergeNamespace.Types) {
			if (HasType(ns, mergeType.Name)) {
			    CodeTypeDeclaration type = GetType(ns, mergeType.Name);
			    ArrayList remove = new ArrayList();
			    foreach(CodeTypeMember mergeMember in mergeType.Members) {
				if (!HasMember(type, mergeMember)) {
				    if (IsMemberGenerated(mergeMember)) {
					log.Add("removing member with [Generate] attribute that is no longer in generated code: " + mergeType.Name + "." + mergeMember.Name);
					remove.Add(mergeMember);
				    }
				}
			    }
			    foreach(CodeTypeMember o in remove) {
				mergeType.Members.Remove(o);
			    }
			}
		    }

		}
	    }

	    
	}

	private CodeNamespace GetNamespace(CodeCompileUnit unit, String name) {
	    foreach(CodeNamespace ns in unit.Namespaces) {
		if (ns.Name.Equals(name)) {
		    return ns;
		}
	    }
	    return null;
	}

	private CodeNamespaceImport GetImport(CodeNamespace ns, String name) {
	    foreach(CodeNamespaceImport import in ns.Imports) {
		if (import.Namespace.Equals(name)) {
		    return import;
		}
	    }
	    return null;
	}

	private CodeTypeDeclaration GetType(CodeNamespace ns, String name) {
	    foreach(CodeTypeDeclaration type in ns.Types) {
		if (type.Name.Equals(name)) {
		    return type;
		}
	    }
	    return null;
	}

	private CodeTypeMember GetMember(CodeTypeDeclaration type, CodeTypeMember mergeMember) {
	    foreach(CodeTypeMember member in type.Members) {
		if (member.Name.Equals(mergeMember.Name)) {
		    Boolean match = true;
		    if (member is CodeMemberMethod && mergeMember is CodeMemberMethod) {
			CodeMemberMethod method = (CodeMemberMethod)member;
			CodeMemberMethod mergeMethod = (CodeMemberMethod)mergeMember;
			if (method.Parameters.Count == mergeMethod.Parameters.Count) {
			    foreach(CodeParameterDeclarationExpression parameter in mergeMethod.Parameters) {
				if (!HasParameter(method, parameter.Type)) {
				    match = false;
				}
			    }
			} else {
			    match = false;
			}
		    } else if (member is CodeMemberProperty) {
			match = true;
		    } else if (member is CodeMemberField) {
			match = true;
		    } else {
			throw new Exception("unable to get member of type: " + mergeMember.GetType().FullName);
		    }
		    if (match) {
			return member;
		    }
		}
	    }
	    return null;
	}

	private CodeParameterDeclarationExpression GetParameter(CodeMemberMethod method, CodeTypeReference type) {
	    foreach(CodeParameterDeclarationExpression parameter in method.Parameters) {
		if (parameter.Type.BaseType.Equals(type.BaseType)) {
		    return parameter;
		}
	    }
	    return null;
	}

	private Boolean HasNamespace(CodeCompileUnit unit, String name) {
	    return GetNamespace(unit, name) == null ? false : true;
	}

	private Boolean HasImport(CodeNamespace ns, String name) {
	    return GetImport(ns, name) == null ? false : true;
	}

	private Boolean HasType(CodeNamespace ns, String name) {
	    return GetType(ns, name) == null ? false : true;
	}

	private Boolean HasMember(CodeTypeDeclaration type, CodeTypeMember member) {
	    return GetMember(type, member) == null ? false : true;
	}

	private Boolean HasParameter(CodeMemberMethod method, CodeTypeReference type) {
	    return GetParameter(method, type) == null ? false : true;
	}

	private void UpdateMember(CodeTypeDeclaration type, CodeTypeMember mergeMember) {
	    CodeTypeMember member = GetMember(type, mergeMember);

	    if (IsMemberGenerated(member)) {
		Int32 index = type.Members.IndexOf(member);
		type.Members[index] = mergeMember;
	    } else {
		log.Add("member " + type.Name + "." + member.Name + " was found in generated source but was not overwritten because it does not have the Generate attribute.");
	    }
	}

	private Boolean IsMemberGenerated(CodeTypeMember member) {
	    foreach(CodeAttributeDeclaration cad in member.CustomAttributes) {
		if (cad.Name.Equals("Generate")) {
		    return true;
		}
	    }
	    return false;
	}

	public String Generate() {
	    CodeDomProvider provider= new CSharpCodeProvider();
	    StringWriter sw = new StringWriter();
	    ICodeGenerator generator= provider.CreateGenerator();

	    generator.GenerateCodeFromCompileUnit(unit,sw,new CodeGeneratorOptions());

	    /// get rid of "autogenerated" header
	    StringReader reader = new StringReader(sw.ToString());
	    for(Int32 i=0; i<9; i++) {
		reader.ReadLine();
	    }

	    return reader.ReadToEnd().Trim();
	}

	private ArrayList GetMembers(CodeCompileUnit unit) {
	    ArrayList members = new ArrayList();
	    foreach(CodeNamespace ns in unit.Namespaces) {
		foreach(CodeTypeDeclaration type in ns.Types) {
		    Member member = null;
		    foreach(CodeTypeMember m in type.Members) {
			CodeLinePragma line = (CodeLinePragma)m.UserData["Location"];
			if (member != null) {
			    member.LastLine = line.LineNumber - 1;
			    members.Add(member);
			    member = null;
			}
			if (m is CodeMemberField) {
			} else if (m is CodeMemberMethod || m is CodeMemberProperty || m is CodeConstructor) {
			    member = new Member();
			    member.FirstLine = line.LineNumber;
			    member.Element = m;
			    member.Type = type;
			} else {
			    throw new Exception("unknown type member type: " + m.GetType().FullName);
			}
		    }
		    if (member!=null) {
			member.LastLine = 9999;
			members.Add(member);
		    }
		}
	    }

	    return members;
	}

	public class Member {
	    public CodeTypeDeclaration Type = null;
	    public CodeTypeMember Element = null;
	    public Int32 FirstLine;
	    public Int32 LastLine;
	    public Boolean HasBeginRegion = false;
	    public Boolean HasEndRegion = false;
	    public String RegionName = String.Empty;

	    public Boolean Generate {
		get {
		    if (Element == null) {
			return false;
		    } else {
			foreach(CodeAttributeDeclaration cad in Element.CustomAttributes) {
			    if (cad.Name.Equals("Generate")) {
				return true;
			    }
			}
			return false;
		    }
		}
	    }
	}


    }
}
