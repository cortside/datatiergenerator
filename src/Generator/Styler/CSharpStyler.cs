using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Collections;

using Spring2.DataTierGenerator.Generator.Styler;

namespace Spring2.DataTierGenerator.Generator.Styler {

    public enum BraceStyle {
	Block,
	C
    }

    public enum IndentStyle {
	Tab,
	Space
    }

    public class CSharpStyler : BaseStyler, IStyler {
	private String inFile = null;
	private String outFile = null;
	private Boolean linespace = true;
	private StringCollection filebuffer = new StringCollection();

	private BraceStyle typeBracing = BraceStyle.Block;
	private BraceStyle flowBracing = BraceStyle.Block;
	private BraceStyle functionBracing = BraceStyle.Block;
	private BraceStyle propertyBracing = BraceStyle.Block;

	private IndentStyle indent = IndentStyle.Tab;
	private Int32 tabSize = 8;
	private Int32 indentSize = 4;

	private static void Usage() {
	    Console.Write (
		"csharpstyle -f file.cs -l <true|false> -b <block|c|mono> > output.cs\n\n" +
		"   -f || /-f || --file  file.cs          The csharp source file to parse.\n\n" +
		"   -l || /-l || --line  <true|false>     Specifies wether to use line spacing.\n\n" +
		"   -b || /-b || --brace  <block|c|mono>  Specifies the bracing style to use.\n\n" +
		"   -i || /-i || --indent <space|tab>     Specifies the begining of line indentation to use.\n\n" +
		"   -o || /-o || --out  file.cs           The output file for the styled output, or standard out if not specified.\n\n"
		);
	}

	public static void Main (string[] args) {
	    String version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
	    // TODO: need --version or quiet mode
	    //Console.Out.WriteLine("CSharpStyle version " + version);
	    CSharpStyler style = new CSharpStyler();

	    ParseOptions(style, args);

	    // TODO: add options to:
	    //  - type, flow, function, property brace style options
	    //	- fix whitespace at beginning of line
	    //  - fix whitespace at end of line
	    //  - fix indentation
	    //  - fix line spacing (blank line at beginning of method)
	    //  - fix spacing between members
	    //  - add missing comments to public members

	    if(style.File == null) {
		Usage();
		return;
	    }

	    // Style method should take a filename making the configuration reusable
	    StringBuilder sb = style.Style();
	    if (style.Out == null) {
		Console.Out.Write(sb.ToString());
	    } else {
		Boolean write = true;
		if (System.IO.File.Exists(style.Out)) {
		    StreamReader sr = new StreamReader(style.Out, System.Text.Encoding.Default);
		    String contents = sr.ReadToEnd();
		    sr.Close();
		    if (contents.Equals(sb.ToString())) {
			write = false;
		    }
		}

		// only write out file if it is different than target or target does not exist - to keep from updating the timestamp
		if (write) {
		    Console.Out.WriteLine("styling " + style.File);
		    StreamWriter sw = new StreamWriter(style.Out, false, System.Text.Encoding.Default);
		    sw.Write(sb.ToString());
		    sw.Flush();
		    sw.Close();
		}
	    }
	}

	private static void ParseOptions(CSharpStyler style, string[] args) {
	    int argc = args.Length;
	    for(int i = 0; i < argc; i++) {
		string arg = args[i];
		// The "/" switch is there for wine users, like me ;-)
		if(arg.StartsWith("-") || arg.StartsWith("/")) {
		    switch(arg) {
			case "-l": case "/-l": case "--line":
			    if((i + 1) >= argc) {
				Usage();
				return;
			    }
			    if (args[++i] == "false") {
				style.LineSpacing = false;
			    }
			    continue;
			case "-f": case "/-f": case "--file":
			    if((i + 1) >= argc) {
				Usage();
				return;
			    }
			    style.File = args[++i];
			    continue;
			case "-o": case "/-o": case "--out":
			    if((i + 1) >= argc) {
				Usage();
				return;
			    }
			    style.Out = args[++i];
			    continue;
			case "-i": case "/-i": case "--indent":
			    if((i + 1) >= argc) {
				Usage();
				return;
			    }
			    String s = args[++i];
			    if (s.Equals("space")) {
				style.Indent = IndentStyle.Space;
			    }
			    continue;
			case "-b": case "/-b": case "--brace":
			    if((i + 1) >= argc) {
				Usage();
				return;
			    }
			    String bstyle = args[++i];
			    if (bstyle == "c") {
				style.TypeBracing = BraceStyle.C;
				style.FlowBracing = BraceStyle.C;
				style.FunctionBracing = BraceStyle.C;
				style.PropertyBracing = BraceStyle.C;
			    } else if (bstyle == "block") {
				style.TypeBracing = BraceStyle.Block;
				style.FlowBracing = BraceStyle.Block;
				style.FunctionBracing = BraceStyle.Block;
				style.PropertyBracing = BraceStyle.Block;
			    } else if (bstyle == "mono") {
				style.TypeBracing = BraceStyle.Block;
				style.FlowBracing = BraceStyle.Block;
				style.FunctionBracing = BraceStyle.C;
				style.PropertyBracing = BraceStyle.Block;
			    } else {
				Usage();
				return;
			    }
			    continue;
			default:
			    Usage();
			    return;
		    }
		}
	    }
	}

	public CSharpStyler () {
	}

	public CSharpStyler (Hashtable options) {

	    //TODO: handle unknown option specified

	    if (options.Contains("LineSpacing")) {
		// expect: true, false
		if (options["LineSpacing"].ToString().Equals("false")) {
		    this.LineSpacing = false;
		}
	    }
	    
	    if (options.Contains("IndentStyle")) {
		// expect: tab, space
		if (options["IndentStyle"].ToString().Equals("space")) {
		    this.Indent = IndentStyle.Space;
		}
	    }

	    if (options.Contains("BraceStyle")) {
		// expect: block, c, mono
		String bstyle = options["BraceStyle"].ToString();
		if (bstyle == "c") {
		    this.TypeBracing = BraceStyle.C;
		    this.FlowBracing = BraceStyle.C;
		    this.FunctionBracing = BraceStyle.C;
		    this.PropertyBracing = BraceStyle.C;
		} else if (bstyle == "block") {
		    this.TypeBracing = BraceStyle.Block;
		    this.FlowBracing = BraceStyle.Block;
		    this.FunctionBracing = BraceStyle.Block;
		    this.PropertyBracing = BraceStyle.Block;
		} else if (bstyle == "mono") {
		    this.TypeBracing = BraceStyle.Block;
		    this.FlowBracing = BraceStyle.Block;
		    this.FunctionBracing = BraceStyle.C;
		    this.PropertyBracing = BraceStyle.Block;
		}
	    }
	}

	public String File {
	    get { return this.inFile; }
	    set { this.inFile = value; }
	}

	public String Out {
	    get { return this.outFile; }
	    set { this.outFile = value; }
	}

	public Boolean LineSpacing {
	    get { return this.linespace; }
	    set { this.linespace = value; }
	}

	public BraceStyle TypeBracing {
	    get { return this.typeBracing; }
	    set { this.typeBracing = value; }
	}

	public BraceStyle FlowBracing {
	    get { return this.flowBracing; }
	    set { this.flowBracing = value; }
	}

	public BraceStyle FunctionBracing {
	    get { return this.functionBracing; }
	    set { this.functionBracing = value; }
	}

	public BraceStyle PropertyBracing {
	    get { return this.propertyBracing; }
	    set { this.propertyBracing = value; }
	}

	public IndentStyle Indent {
	    get { return this.indent; }
	    set { this.indent = value; }
	}


	public String Style(String input) {
	    TextReader sr = new StringReader(input);
	    return Style(sr).ToString();
	}

	public StringBuilder Style() {
	    TextReader sr = new StreamReader(inFile, System.Text.Encoding.Default);
	    return Style(sr);
	}

	private StringBuilder Style(TextReader reader) {
	    FillBuffer(reader);
	    FixMonoStyle();
	    FixIndentation();

	    StringBuilder sb = new StringBuilder();
	    for (int i=0; i < filebuffer.Count; i++) {
		sb.Append(filebuffer[i]).Append(Environment.NewLine);
	    }

	    return sb;
	}

	public void FillBuffer(TextReader sr) {
	    filebuffer = new StringCollection();
	    while (sr.Peek() > -1) {
		filebuffer.Add(sr.ReadLine());
	    }
	    sr.Close();
	}

	public void FixMonoStyle() {
	    // break the opening brace off of the line if using hanging braces
	    if (flowBracing == BraceStyle.C) {
		for (int i=0; i < filebuffer.Count; i++) {
		    String str = filebuffer[i];

		    if (str.Trim().StartsWith("}") && (IsElse(str) || IsElseIf(str) || IsCatch(str) || IsFinally(str))) {
			filebuffer.RemoveAt(i);
			filebuffer.Insert(i, "}");
			filebuffer.Insert(i + 1, str.Trim().Substring(1).Trim());
		    }

		    if (IsSingleLineGet(str)) {
			filebuffer.RemoveAt(i);
			filebuffer.Insert(i, "get");
			filebuffer.Insert(i + 1, "{");
			String s = str.Substring(str.IndexOf("get")).Trim();
			s = s.Substring(s.IndexOf("{")+1);
			s = s.Substring(0,s.Length-1).Trim();
			filebuffer.Insert(i + 2, s);
			filebuffer.Insert(i + 3, "}");
		    }

		    if (IsSingleLineSet(str)) {
			filebuffer.RemoveAt(i);
			filebuffer.Insert(i, "set");
			filebuffer.Insert(i + 1, "{");
			String s = str.Substring(str.IndexOf("set")).Trim();
			s = s.Substring(s.IndexOf("{")+1);
			s = s.Substring(0,s.Length-1).Trim();
			filebuffer.Insert(i + 2, s);
			filebuffer.Insert(i + 3, "}");
		    }

		}
	    }

	    // combine the opening brace with next line if using block brace style
	    if (flowBracing == BraceStyle.Block) {
		for (int i=0; i < filebuffer.Count; i++) {
		    String str = filebuffer[i];

		    if (str.Trim().Equals("}") && i < (filebuffer.Count -1) && !StartWithOpenBrace(filebuffer[i+1]) && (IsElse(filebuffer[i+1]) || IsElseIf(filebuffer[i+1]) || IsCatch(filebuffer[i+1]) || IsFinally(filebuffer[i+1]))) {
			filebuffer[i] = "} " + filebuffer[i+1].Trim();
			filebuffer.RemoveAt(i+1);
		    }

		    // move closing brace to new line if not a single line property set/get
		    if (!IsComment(filebuffer[i]) && EndWithCloseBrace(filebuffer[i]) && filebuffer[i].Trim().Length > 1 && !IsSingleLineGet(filebuffer[i]) && !IsSingleLineSet(filebuffer[i])) {
			filebuffer[i] = filebuffer[i].Substring(0, filebuffer[i].LastIndexOf("}")).Trim();
			filebuffer.Insert(i+1, "}");
		    }

		    // open up properties that are multiple lines starting on line with get/set
		    if ((IsGet(filebuffer[i]) && !IsSingleLineGet(filebuffer[i]) && !IsSingleLineGetWithComment(filebuffer[i])) || (IsSet(filebuffer[i]) && !IsSingleLineSet(filebuffer[i]) && !IsSingleLineSetWithComment(filebuffer[i]))) {
			String rightOfOpenBrace = str.Substring(str.IndexOf("{")+1);
			rightOfOpenBrace = rightOfOpenBrace.Replace("\t","").Trim();
			if (rightOfOpenBrace.Length > 0) {
			    filebuffer.Insert(i+1, rightOfOpenBrace);
			    filebuffer[i] = str.Substring(0, str.IndexOf("{")+1);
			}
		    }
		}
	    }

	    for (int i=0; i < filebuffer.Count; i++) {
		IsBadMonoStyle(filebuffer[i]);
	    }
	}

	public void FixIndentation() {
	    Int32 indent = 0;
	    Boolean skipIndent = false;
	    Boolean inComment = false;
	    Boolean inSwitch = false;
	    Int32 switchIndent = 0;

	    for (int i=0; i < filebuffer.Count; i++) {
		Int32 currentIndent = indent;
		String str = filebuffer[i].Trim();

		if (str.StartsWith("/*")) {
		    inComment = true;
		}

// TODO: remove - this is for debugging only
if (str.Trim().IndexOf("set{localDeliveryDate = value;} //added this line") >= 0) {
    int foo=0;
}

		if (!inComment) {
		    //                                                  + && !IsElse(str)
		    if (!IsComment(str) 
			    && (IsType(str) 
				|| (IsFlow(str) && (!IsElse(str) || flowBracing == BraceStyle.C) && !IsElseIf(str) && !IsCatch(str) && !IsFinally(str) ) 
			    || IsFunction(str) 
			    || IsProperty(str) 
			    || IsHangingBrace(str) 
			    || IsGet(str) 
			    || IsSet(str) 
			    || IsLock(str)) 
			&& !EndWithCloseBrace(str) 
			&& !EndWithCloseBraceAndComment(str)
			&& !IsClosingBrace(str) ) {

			// if next line is { only, then don't indent next line but 2 lines from then.
			if (filebuffer[i+1].Trim().Equals("{")) {
			    skipIndent = true;
			} else {
			    if (IsIf(str)) {
				if (EndWithBrace(str) || EndWithBraceAndComment(str) || IsHangingBrace(filebuffer[i+1]) || EndWithContinuationOperator(str) || StartWithContinuationOperator(filebuffer[i+1])) {
				    indent++;
				}
			    } else {
				// the line may be misinterpreted if there is multiline concatination
				if (!StartWithContinuationOperator(filebuffer[i+1]) && !EndWithContinuationOperator(str)) {
				    indent++;
				}
			    }
			}
			// TODO: this may be a problem with nested switch statements
			if (IsSwitch(str)) {
			    inSwitch = true;
			    switchIndent = indent;
			}
		    } else if (!IsComment(str) && (IsElse(str) ||
			IsElseIf(str) ||
			IsCatch(str) ||
			IsFinally(str)) ) {
			if (flowBracing == BraceStyle.Block && (StartWithOpenBrace(str) || EndWithBrace(filebuffer[i-1]))) {
			    currentIndent--;
			}

			if (flowBracing == BraceStyle.Block && (!StartWithOpenBrace(str) && !(i>0 && EndWithBrace(filebuffer[i-1]))) && (IsElse(str) || IsElseIf(str))) {
			    indent++;
			}

			// handle single line else statments (bad idea in my opinion)
			if (IsElse(str) && !EndWithBrace(str) && !EndWithBraceAndComment(str) && !EndWithContinuationOperator(str) && !StartWithContinuationOperator(filebuffer[i+1])) {
			    indent--;
			}
		    } else if (!IsComment(str) && IsClosingBrace(str)) {
			indent--;
			currentIndent--;

			// TODO: this may be a problem with nested switch statements
			if (inSwitch && (indent == switchIndent || (IsBreak(filebuffer[i-1]) && indent==switchIndent-1) ) && !IsComment(str) && IsClosingBrace(str)) {
			    inSwitch = false;
			    indent = switchIndent -1;
			    currentIndent = indent;
			    switchIndent=0;
			}
		    } else if (!IsComment(str) && (IsCase(str) || IsSwitchDefault(str))) {
			currentIndent = switchIndent;
			indent = currentIndent +1;
		    } else if (!IsComment(str) && IsBreak(str) && inSwitch) {
			indent--;
		    }

		    // handle method calls that are broken into multiple lines and string concatination
		    if (StartWithContinuationOperator(str) || (i > 0 && EndWithContinuationOperator(filebuffer[i-1]))) {
			currentIndent++;
		    } else if (i > 0 && !IsComment(filebuffer[i-1]) && (IsIf(filebuffer[i-1]) || IsElse(filebuffer[i-1])) && !EndWithBrace(filebuffer[i-1]) && !EndWithBraceAndComment(filebuffer[i-1]) && !EndWithSemicolon(filebuffer[i-1])) {
			// handle single line if/else statements
			currentIndent++;
		    }

		    // handle single line while/for/foreach statements
		    if (i > 0 && !IsComment(filebuffer[i-1]) && (IsWhile(filebuffer[i-1]) || IsFor(filebuffer[i-1]) || IsForEach(filebuffer[i-1])) && !EndWithBrace(filebuffer[i-1]) && !EndWithBraceAndComment(filebuffer[i-1]) && !EndWithSemicolon(filebuffer[i-1])) {
			indent--;
		    }
		}


		if (str.EndsWith("*/")) {
		    inComment = false;
		}


		filebuffer[i] = GetIndentation(currentIndent) + str;

		if (skipIndent) {
		    i++;
		    str = str = filebuffer[i].Trim();
		    filebuffer[i] = GetIndentation(currentIndent) + str;

		    indent++;
		}
		skipIndent = false;
	    }

	    // output warning 
	    if (indent != 0) {
		Console.Out.WriteLine("WARNING: indent level did not end up at 0 when finished - " + this.File);
	    }


	    // remove leading whitespace for blank lines
	    for (int i=0; i < filebuffer.Count; i++) {
		if (IsBlankLine(filebuffer[i])) {
		    filebuffer[i] = String.Empty;
		}
	    }

	}

	private String GetIndentation(Int32 indent) {
	    // safety to prevent errors if indent is < 0
	    if (indent < 0) {
		indent = 0;
		Console.Out.WriteLine("WARNING: indent level < 0 in " + this.File);
	    }

	    String str = String.Empty;
	    if (this.indent == IndentStyle.Tab) {
		str = "              ".Substring(0, (indent * indentSize) % tabSize) + str;
		for(int x=0; x < (indent * indentSize) / tabSize; x++) {
		    str = "\t" + str;
		}

		return str;
	    } else {
		str = "                                                                                ".Substring(0,indent * 4);
		return str;
	    }
	}


	public void IsBadMonoStyle(String str) {
	    if (IsBadMonoType(str)) {
		if (typeBracing == BraceStyle.Block) {
		    FixHangingBrace(str);
		} else {
		    FixEndBrace(str);
		}
	    } else if(IsBadMonoFlow(str)) {
		if (flowBracing == BraceStyle.Block) {
		    FixHangingBrace(str);
		} else {
		    FixEndBrace(str);
		}
	    } else if(IsBadMonoFunction(str)) {
		if (functionBracing == BraceStyle.Block) {
		    FixHangingBrace(str);
		} else {
		    FixEndBrace(str);
		}
	    } else if(IsBadMonoProperty(str) || IsBadPropertyAccessor(str)) {
		if (propertyBracing == BraceStyle.Block) {
		    FixHangingBrace(str);
		} else {
		    FixEndBrace(str);
		}
	    }
	}

	public void FixHangingBrace(String str) {
	    int strloc = filebuffer.IndexOf(str);
	    int brcloc = FindHangingBrace(strloc);
	    int diff = brcloc - strloc;
	    if (brcloc > 0) {
		for (int i = 0; i < diff+1; i++) {
		    filebuffer.RemoveAt(strloc);
		}
		filebuffer.Insert(strloc, str + " {");
		if (linespace) {
		    filebuffer.Insert(strloc+1, "");
		}
	    } else {
	    }
	}

	public int FindHangingBrace(int strloc) {
	    strloc++;
	    bool found = false;
	    while (!found) {
		try {
		    string str = filebuffer[strloc++].Trim().Replace("\t", "");
		    found = IsHangingBrace(str);
		    if (found && str != "{") {
			filebuffer.Insert(strloc, str.Substring(1));
		    }
		    if (!found && !IsBlankLine(str)) {
			return -1;
		    }
		} catch (Exception) {
		    return -1;
		}
	    }
	    return strloc -1;
	}

	public void FixEndBrace(String str) {
	    int strloc = filebuffer.IndexOf(str);
	    filebuffer.RemoveAt(strloc);
	    filebuffer.Insert(strloc, RemoveEndBrace(str));
	    filebuffer.Insert(strloc+1, AddHangingBrace(str));
	}

	public bool IsBadMonoType(String str) {
	    if ( IsType(str) ) {
		if (typeBracing == BraceStyle.Block && !EndWithBrace(str) || typeBracing == BraceStyle.C && EndWithBrace(str)) {
		    return true;
		}
	    }

	    return false;
	}

	public bool IsBadMonoFlow(String str) {
	    if (IsFlow(str)) {
		if (flowBracing == BraceStyle.Block && !EndWithBrace(str) || flowBracing == BraceStyle.C && EndWithBrace(str)) {
		    return true;
		}
	    }

	    return false;
	}

	public bool IsBadMonoFunction(String str) {
	    if (IsFunction(str)) {
		if (functionBracing == BraceStyle.Block && !EndWithBrace(str) || functionBracing == BraceStyle.C && EndWithBrace(str)) {
		    return true;
		}
	    }

	    return false;
	}

	public bool IsBadMonoProperty(String str) {
	    if (IsProperty(str)) {
		if (propertyBracing == BraceStyle.Block && !EndWithBrace(str) || propertyBracing == BraceStyle.C && EndWithBrace(str)) {
		    return true;
		}
	    }

	    return false;
	}

	public bool IsBadPropertyAccessor(String str) {
	    if (IsGet(str) || IsSet(str)) {
		if (propertyBracing == BraceStyle.Block && !EndWithBrace(str) || propertyBracing == BraceStyle.C && EndWithBrace(str)) {
		    return true;
		}
	    }

	    return false;
	}

	public static bool IsType(String str) {
	    if (	!IsComment(str) && (
		IsNameSpace(str) ||
		IsClass(str) ||
		IsStruct(str) ||
		IsEnum(str) )) {
		return true;
	    } else {
		return false;
	    }
	}

	public static bool IsFlow(String str) {
	    if (	!IsComment(str) && (
		IsIf(str) ||
		IsElse(str) ||
		IsElseIf(str) ||
		IsTry(str) ||
		IsCatch(str) ||
		IsFinally(str) ||
		IsFor(str) ||
		IsForEach(str) ||
		IsWhile(str) ||
		IsSwitch(str)
		)) {
		return true;
	    } else {
		return false;
	    }
	}

	public static bool IsFunction(String str) {
	    if (	Regex.IsMatch(str, @"^\s*(\w+)\s+(\w+).*\(+") &&
		!IsDeclaration(str) &&
		!IsComment(str) &&
		!IsType(str) &&
		!IsFlow(str) &&
		!IsReturn(str)) {
		return true;
	    } else {
		return false;
	    }
	}

	public bool IsProperty(String str) {
	    Int32 index = filebuffer.IndexOf(str);
	    if (	Regex.IsMatch(str, @"^\s*(\w+)\s+(\w+).*") &&
		!IsDeclaration(str) &&
		!IsComment(str) &&
		!IsType(str) &&
		!IsFlow(str) &&
		!IsFunction(str) &&
		!IsReturn(str)  && (EndWithBrace(str) || StartWithOpenBrace(filebuffer[index+1]) ) ) {
		return true;
	    } else {
		return false;
	    }
	}

	public static string RemoveEndBrace(String str) {
	    Regex rg = new Regex(@"\{\s*$");
	    return rg.Replace(str, "");
	}

	public static string AddHangingBrace(String str) {
	    Regex rg = new Regex(@"\S+\s*");
	    string blank = rg.Replace(str,"");
	    return blank + "{";
	}

	public static bool IsDeclaration(String str) {
	    return Regex.IsMatch(str, @"\;\s*(\/+|$)");
	}

	public static bool IsComment(String str) {
	    return Regex.IsMatch(str, @"^(\s*\/+|\s*\*+|\s*\#+)");
	}

	public static bool EndWithBrace(String str) {
	    return Regex.IsMatch(str, @"\{\s*$");
	}

	public static bool IsHangingBrace(String str) {
	    return Regex.IsMatch(str, @"^\s*\{");
	}

	public static bool IsBlankLine(String str) {
	    return Regex.IsMatch(str, @"^\s*$");
	}

	public static bool IsNameSpace(String str) {
	    return Regex.IsMatch(str, @"(^|\s+)namespace\s+");
	}

	public static bool IsClass(String str) {
	    return Regex.IsMatch(str, @"(public|internal|abstract|private|^)(\s+|^)class\s+\w+");
	}

	public static bool IsStruct(String str) {
	    return Regex.IsMatch(str, @"\s+struct\s+");
	}

	public static bool IsEnum(String str) {
	    return Regex.IsMatch(str, @"\s+enum\s+");
	}

	public static bool IsIf(String str) {
	    return Regex.IsMatch(str, @"(^|\s+|\}+)if(\s+|\(+|$)");
	}

	public static bool IsElse(String str) {
	    return Regex.IsMatch(str, @"(^|\s*|\}+)else(\s*|\{+|$|/s*\/\/.*$)");
	}

	public static bool IsElseIf(String str) {
	    return Regex.IsMatch(str, @"(^|\s+|\}+)else if(\s+|\(+|$)");
	}

	public static bool IsTry(String str) {
	    return Regex.IsMatch(str, @"^\s*try(\s*|\(+|$)");
	}

	public static bool IsCatch(String str) {
	    return Regex.IsMatch(str, @"(^|\s+|\}+)catch(\s+|\(+|$)");
	}

	public static bool IsFinally(String str) {
	    return Regex.IsMatch(str, @"(^|\s+|\}+)finally(\s+|\{+|$)");
	}

	public static bool IsFor(String str) {
	    //return Regex.IsMatch(str, @"(^|\s+|\}+)for(\s+|\(+|$)");
	    return Regex.IsMatch(str, @"(^|\s+|\}+)for(\s*\(+|$)");
	}

	public static bool IsForEach(String str) {
	    return Regex.IsMatch(str, @"^\s*foreach(\s+|\(+|$)");
	}

	public static bool IsWhile(String str) {
	    return Regex.IsMatch(str, @"(^\s*|\}+)while(\s+|\(+|$)");
	}

	public static bool IsSwitch(String str) {
	    return Regex.IsMatch(str, @"(^|\s+|\}+)switch(\s+|\(+|$)");
	}

	public static bool IsCase(String str) {
	    return Regex.IsMatch(str, @"(^\s*|\}+)case(\s+|\(+|$)");
	}

	public static bool IsGet(String str) {
	    return Regex.IsMatch(str, @"(^\s*|\}+)get\s*({|$)");
	}

	public static bool IsSet(String str) {
	    return Regex.IsMatch(str, @"(^\s*|\}+)set\s*({|$)");
	}

	public static bool EndWithCloseBrace(String str) {
	    return Regex.IsMatch(str, @"\}\s*$");
	}

	public static bool EndWithCloseBraceAndComment(String str) {
	    return Regex.IsMatch(str, @"\}\s*(\/\/|$)");
	}

	public static bool StartWithOpenBrace(String str) {
	    return Regex.IsMatch(str, @"^\s*\}");
	}

	public static bool IsReturn(String str) {
	    return Regex.IsMatch(str, @"^\s*return\s+");
	}

	public static bool IsLock(String str) {
	    return Regex.IsMatch(str, @"(^\s*|\}+)lock\s*\(");
	}

	public static bool EndWithBraceAndComment(String str) {
	    return Regex.IsMatch(str, @"\{\s*(\/\/|$)");
	}

	public static bool EndWithSemicolon(String str) {
	    return Regex.IsMatch(str, @"\;\s*(\/+|$)");
	}

	/// <summary>
	/// Check to see if the line is:
	///   }
	///   } // comment
	///   };
	/// </summary>
	public static bool IsClosingBrace(String str) {
	    return Regex.IsMatch(str, @"^\s*\}\s*(\;|\/\/|$)");
	}

	public static bool IsSwitchClosingBrace(String str) {
	    return Regex.IsMatch(str, @"^\s*\}\s*\;(\/\/|$)");
	}

	public static bool IsBreak(String str) {
	    return Regex.IsMatch(str, @"^\s*break\s*\;");
	}

	public static bool IsSwitchDefault(String str) {
	    return Regex.IsMatch(str, @"^\s*default\s*:\s*$");
	}

	/// <summary>
	/// Ends with something that would mean the next line is a continuation (+/,/&&/||/()
	/// </summary>
	/// <param name="str"></param>
	/// <returns></returns>
	public static bool EndWithContinuationOperator(String str) {
	    return Regex.IsMatch(str, @"(\+\s*$|,\s*$|&&\s*$|\|\|\s*$|\(\s*$|\*\s*$)");
	}

	/// <summary>
	/// Ends with something that would mean the next line is a continuation (+/,/&&/||)
	/// </summary>
	/// <param name="str"></param>
	/// <returns></returns>
	public static bool StartWithContinuationOperator(String str) {
	    return Regex.IsMatch(str, @"(^\s*\*|^\s*\+|^\s*,|^\s*&&|^\s*\|\|)");
	}

	public static bool IsSingleLineGet(String str) {
	    return Regex.IsMatch(str, @"(^\s*)get\s*{*\s*\w+") && EndWithCloseBrace(str);
	}

	public static bool IsSingleLineSet(String str) {
	    return Regex.IsMatch(str, @"(^\s*)set\s*{*\s*\w+") && EndWithCloseBrace(str);
	}

	public static bool IsSingleLineGetWithComment(String str) {
	    return Regex.IsMatch(str, @"(^\s*)get\s*{*\s*\w+") && EndWithCloseBraceAndComment(str);
	}

	public static bool IsSingleLineSetWithComment(String str) {
	    return Regex.IsMatch(str, @"(^\s*)set\s*{*\s*\w+") && EndWithCloseBraceAndComment(str);
	}


    }
}
