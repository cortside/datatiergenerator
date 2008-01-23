using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Ast;
using ICSharpCode.NRefactory.PrettyPrinter;

namespace Spring2.DataTierGenerator.Generator.Writer {
    public class SourceUnit {
    	private CompilationUnit unit;
    	private IList<ISpecial> specials;

    	private List<NamespaceUnit> namespaces = new List<NamespaceUnit>();
	private List<UsingDeclaration> imports = new List<UsingDeclaration>();

	public SourceUnit(String content) {
	    using (ICSharpCode.NRefactory.IParser parser = ParserFactory.CreateParser(SupportedLanguage.CSharp, new StringReader(content))) {
		parser.Parse();
		if (parser.Errors.Count > 0) {
		    throw new Exception(parser.Errors.ErrorOutput);
		}
		specials = parser.Lexer.SpecialTracker.RetrieveSpecials();
		unit = parser.CompilationUnit;
	    	NRefactoryUtil.InsertSpecials(unit, specials);
		//List<ISpecial> rightSpecials = new List<ISpecial>();
		//rightSpecials.AddRange(specials);
		//MapSpecials(unit.Children, rightSpecials);
		//
		//if (rightSpecials.Count > 0) {
		//    System.Diagnostics.Debug.WriteLine("There are specials that did not get mapped to members.");
		//}
	    }

	    foreach (INode child in unit.Children) {
		if (child is UsingDeclaration) {
		    imports.Add(child as UsingDeclaration);
		} else if (child is NamespaceDeclaration) {
		    namespaces.Add(new NamespaceUnit(child as NamespaceDeclaration));
		} else if (child is TypeDeclaration) {
		    if (namespaces.Count == 0) {
			namespaces.Add(new NamespaceUnit());
		    }
		    namespaces[0].AddType(new TypeUnit(child as TypeDeclaration));
		} else {
		    throw new Exception("Unknown node type: " + child.GetType() + " - " + child.ToString());
		}
	    }

	}

	public SourceUnit(CompilationUnit unit, IList<ISpecial> specials) {
	    this.unit = unit;
	    this.specials = specials;
	}

	public void Merge(SourceUnit rightUnit) {
	    RemoveGeneratedMembers(this.unit.Children);

	    foreach (INode child in rightUnit.unit.Children) {
		// need to have a wrapper for the CompilationUnit that has
		// * this could be what keeps the CU and the specials collection together
		// * methods to expose imports and namespaces
		// * then namespaces needs to have wrapper (like NamespaceUnit?) that exposes types
		// * then types needs to have wrapper that exposes members
		// will need to deal with the specials collection

		if (child is UsingDeclaration) {
		    MergeUsing(child as UsingDeclaration);
		} else if (child is NamespaceDeclaration) {
		    MergeNamespace(child as NamespaceDeclaration);
		} else if (child is TypeDeclaration) {
		    MergeType(unit.Children, child as TypeDeclaration);
		} else {
		    throw new Exception("Unknown node type: " + child.GetType() + " - " + child.ToString());
		}
	    }
	}

	//private static void MapSpecials(List<INode> children, List<ISpecial> unmappedSpecials) {
	//    foreach (INode node in children) {
	//        if (node.UserData != null) {
	//            System.Diagnostics.Debug.WriteLine(node.UserData);
	//        }
	//        List<ISpecial> memberSpecials = GetMemberSpecials(unmappedSpecials, node);
	//        node.UserData = memberSpecials;
	//        foreach(ISpecial memberSpecial in memberSpecials) {
	//            unmappedSpecials.Remove(memberSpecial);
	//        }

	//        MapSpecials(node.Children, unmappedSpecials);
	//    }
	//}

	//private static List<ISpecial> GetMemberSpecials(IList<ISpecial> specials, INode node) {
	//    List<ISpecial> results = new List<ISpecial>();

	//    foreach (ISpecial special in specials) {
	//        if (special.StartPosition < node.StartLocation) {
	//            results.Add(special);
	//        }
	//    }
	//    return results;
	//}

    	private static void RemoveGeneratedMembers(List<INode> nodes) {
	    List<INode> nodesToRemove = new List<INode>();

	    foreach(INode node in nodes) {
	    	Boolean removed = false;
		if (node is AttributedNode) {
		    AttributedNode field = node as AttributedNode;
		    if (HasGenerateAttribute(field.Attributes)) {
			nodesToRemove.Add(node);
		    	removed = true;
		    }
		}
		if (!removed) {
		    RemoveGeneratedMembers(node.Children);
		}
	    }

	    foreach(INode node in nodesToRemove) {
	    	nodes.Remove(node);
	    }
	}

    	private static bool HasGenerateAttribute(List<AttributeSection> attributes) {
    	    foreach(AttributeSection section in attributes) {
    	    	foreach(ICSharpCode.NRefactory.Ast.Attribute attribute in section.Attributes) {
    	    	    if (attribute.Name == "Generate") {
    	    	    	return true;
    	    	    }
    	    	}
    	    }

    	    return false;
    	}

    	public String ToCSharp2() {
	    IOutputAstVisitor outputVisitor = new CSharpOutputVisitor();
	    SetPrettyPrintOptions(outputVisitor);

	    using (SpecialNodesInserter.Install(specials, outputVisitor)) {
		unit.AcceptVisitor(outputVisitor, null);
	    }
	    return outputVisitor.Text;
	}

	public String ToCSharp() {
	    IOutputAstVisitor outputVisitor = new CSharpOutputVisitor();
	    SetPrettyPrintOptions(outputVisitor);

	    using (SpecialNodesByMapInserter.Install(new Hashtable(), outputVisitor)) {
		unit.AcceptVisitor(outputVisitor, null);
	    }
	    String code = outputVisitor.Text;
	    return NRefactoryUtil.FixSourceFormatting(code);
	}

	public String ToNodeMap() {
	    StringBuilder sb = new StringBuilder();
	    ToNodeMap(unit.Children, sb, 0);
	    return sb.ToString();
	}

	private void ToNodeMap(List<INode> nodes, StringBuilder sb, Int32 level) {
	    level++;
	    
	    foreach(INode node in nodes) {
		sb.Append(level).Append(" - ").AppendLine(node.GetType().Name);
	    	ToNodeMap(node.Children, sb, level);
		if (node is MethodDeclaration) {
		    MethodDeclaration method = node as MethodDeclaration;
		    ToNodeMap(method.Body.Children, sb, level);
		}
		if (node is IfElseStatement) {
			IfElseStatement ifelse = node as IfElseStatement;
			ToNodeMap(ifelse.Condition.Children, sb, level);
		}
	    }
	}

	private void SetPrettyPrintOptions(IOutputAstVisitor outputVisitor) {
	    PrettyPrintOptions options = outputVisitor.Options as PrettyPrintOptions;
	    options.NamespaceBraceStyle = BraceStyle.EndOfLine;
	    options.ClassBraceStyle = BraceStyle.EndOfLine;
	    options.MethodBraceStyle = BraceStyle.EndOfLine;
	    options.PropertyBraceStyle = BraceStyle.EndOfLine;
	    options.PropertyGetBraceStyle = BraceStyle.EndOfLine;
	    options.PropertySetBraceStyle = BraceStyle.EndOfLine;
	    options.ConstructorBraceStyle = BraceStyle.EndOfLine;
	    options.TabSize = 8;
	    options.IndentSize = 4;
	    options.IndentationChar = ' ';
	}

    	private void MergeUsing(UsingDeclaration child) {
	    if (child.Usings.Count != 1) {
		throw new ArgumentOutOfRangeException("child", "didn't expect to have more than 1 in Usings collection");
	    }
	    String mergeName = child.Usings[0].Name;
	    Int32 insertAt = 0;
	    Boolean add = true;
	    foreach (INode node in unit.Children) {
		if (node is UsingDeclaration) {
		    UsingDeclaration element = node as UsingDeclaration;
		    if (element.Usings.Count != 1) {
			throw new Exception("didn't expect to have more than 1 in the Usings collection");
		    }
		    if (element.Usings[0].Name == mergeName) {
			add = false;
		    }
		    insertAt++;
		}
	    }

	    if (add) {
	    	unit.Children.Insert(insertAt, child);
	    }
	}

	//private void AddSpecialsForNode(INode node, Hashtable specialMap) {
	//    List<ISpecial> memberSpecials = specialMap[node] as List<ISpecial>;
	//    if (memberSpecials != null && memberSpecials.Count > 0) {
	//        ((List<ISpecial>)specials).AddRange(memberSpecials);
	//        node.UserData = memberSpecials;
	//    }
	//    foreach(INode child in node.Children) {
	//        AddSpecialsForNode(child, specialMap);
	//    }
	//}

    	private void MergeNamespace(NamespaceDeclaration child) {
	    Int32 insertAt = 0;
	    Boolean add = true;
	    foreach (INode node in unit.Children) {
		if (node is UsingDeclaration) {
		    insertAt++;
		} else if (node is NamespaceDeclaration) {
		    NamespaceDeclaration element = node as NamespaceDeclaration;
		    if (element.Name == child.Name) {
			add = false;
		    	MergeTypes(element, child);
		    }
		}
	    }

	    if (add) {
		unit.Children.Insert(insertAt, child);
	    }
	}

	private void MergeType(List<INode> collection, TypeDeclaration child) {
	    Boolean add = true;
	    foreach (INode node in collection) {
		if (node is TypeDeclaration) {
		    TypeDeclaration element = node as TypeDeclaration;
		    if (element.Name == child.Name) {
			add = false;
			MergeMembers(element, child);
		    }
		}
	    }
	    if (add) {
	    	collection.Add(child);
	    }
	}

	private void MergeTypes(NamespaceDeclaration left, NamespaceDeclaration right) {
	    foreach(INode node in right.Children) {
		if (node is TypeDeclaration)
	    	MergeType(left.Children, node as TypeDeclaration);
	    }
	}

	private void MergeMembers(TypeDeclaration left, TypeDeclaration right) {
	    foreach (INode node in right.Children) {
		if (node is MemberNode) {
		    MergeMember(left.Children, node as MemberNode);
		} else if (node is FieldDeclaration) {
		    MergeField(left.Children, node as FieldDeclaration);
		} else {
		    System.Diagnostics.Debug.WriteLine(node.GetType());
		}
	    }
	}

	private void MergeMember(List<INode> collection, MemberNode child) {
	    Boolean add = true;
	    foreach (INode node in collection) {
		if (node is MemberNode) {
		    MemberNode element = node as MemberNode;
		    if (element.Name == child.Name) {
			add = false;
		    }
		}
	    }
	    if (add) {
		collection.Add(child);
	    }
	}

	private void MergeField(List<INode> collection, FieldDeclaration child) {
	    Boolean add = true;
	    foreach (INode node in collection) {
		if (node is FieldDeclaration) {
		    FieldDeclaration element = node as FieldDeclaration;
		    if (element.Fields[0].Name == child.Fields[0].Name) {
			add = false;
		    }
		}
	    }
	    if (add) {
		collection.Add(child);
	    }
	}

    }
}
