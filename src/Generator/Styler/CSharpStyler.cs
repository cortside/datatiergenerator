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
	private StylerLineList filebuffer = new StylerLineList();

	private BraceStyle typeBracing = BraceStyle.Block;
	private BraceStyle flowBracing = BraceStyle.Block;
	private BraceStyle functionBracing = BraceStyle.Block;
	private BraceStyle propertyBracing = BraceStyle.Block;

	private IndentStyle indent = IndentStyle.Tab;
	private Int32 tabSize = 8;
	private Int32 indentSize = 4;

	private Boolean hadIndentWarning = false;

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
		sb.Append(GetIndentation(filebuffer[i].Indent)).Append(filebuffer[i].Text).Append(Environment.NewLine);
	    }

	    return sb;
	}

	public void FillBuffer(TextReader sr) {
	    filebuffer = new StylerLineList();
	    while (sr.Peek() > -1) {
		filebuffer.Add(new StylerLine(sr.ReadLine(), filebuffer));
	    }
	    sr.Close();
	}

	public void FixMonoStyle() {
	    // break the opening brace off of the line if using hanging braces
	    if (flowBracing == BraceStyle.C) {
		for (int i=0; i < filebuffer.Count; i++) {
		    StylerLine line = filebuffer[i];

		    if (line.StartsWithClosingBrace && (line.IsElse || line.IsElseIf || line.IsCatch || line.IsFinally)) {
			filebuffer.RemoveAt(i);
			filebuffer.Insert(i, new StylerLine("}", filebuffer));
			filebuffer.Insert(i + 1, new StylerLine(line.Text.Trim().Substring(1).Trim(), filebuffer));
		    }

		    if (line.IsSingleLineGet) {
			filebuffer.RemoveAt(i);
			filebuffer.Insert(i, new StylerLine("get", filebuffer));
			filebuffer.Insert(i + 1, new StylerLine("{", filebuffer));
			String s = line.Text.Substring(line.Text.IndexOf("get")).Trim();
			s = s.Substring(s.IndexOf("{")+1);
			s = s.Substring(0,s.Length-1).Trim();
			filebuffer.Insert(i + 2, new StylerLine(s, filebuffer));
			filebuffer.Insert(i + 3, new StylerLine("}", filebuffer));
		    }

		    if (line.IsSingleLineSet) {
			filebuffer.RemoveAt(i);
			filebuffer.Insert(i, new StylerLine("set", filebuffer));
			filebuffer.Insert(i + 1, new StylerLine("{", filebuffer));
			String s = line.Text.Substring(line.Text.IndexOf("set")).Trim();
			s = s.Substring(s.IndexOf("{")+1);
			s = s.Substring(0,s.Length-1).Trim();
			filebuffer.Insert(i + 2, new StylerLine(s, filebuffer));
			filebuffer.Insert(i + 3, new StylerLine("}", filebuffer));
		    }
		}
	    }

	    // combine the opening brace with next line if using block brace style
	    if (flowBracing == BraceStyle.Block) {
		for (int i=0; i < filebuffer.Count; i++) {
		    StylerLine line = filebuffer[i];

		    if (line.StartsWithClosingBrace && i < (filebuffer.Count -1) && !filebuffer[i+1].StartWithOpenBrace && (filebuffer[i+1].IsElse || filebuffer[i+1].IsElseIf || filebuffer[i+1].IsCatch || filebuffer[i+1].IsFinally)) {
			filebuffer[i] = new StylerLine("} " + filebuffer[i+1].Text.Trim(), filebuffer);
			filebuffer.RemoveAt(i+1);
		    }

		    // move closing brace to new line if not a single line property set/get
		    if (!line.IsComment && line.EndWithCloseBrace && line.Text.Trim().Length > 1 && !line.IsSingleLineGet && !line.IsSingleLineSet) {
			filebuffer[i] = new StylerLine(line.Text.Substring(0, line.Text.LastIndexOf("}")).Trim(), filebuffer);
			filebuffer.Insert(i+1, new StylerLine("}", filebuffer));
		    }

		    // open up properties that are multiple lines starting on line with get/set
		    if ((line.IsGet && !line.IsSingleLineGet && !line.IsSingleLineGetWithComment) || (line.IsSet && !line.IsSingleLineSet && !line.IsSingleLineSetWithComment)) {
			String rightOfOpenBrace = line.Text.Substring(line.Text.IndexOf("{")+1);
			rightOfOpenBrace = rightOfOpenBrace.Replace("\t","").Trim();
			if (rightOfOpenBrace.Length > 0) {
			    filebuffer.Insert(i+1, new StylerLine(rightOfOpenBrace, filebuffer));
			    filebuffer[i] = new StylerLine(line.Text.Substring(0, line.Text.IndexOf("{")+1), filebuffer);
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
		StylerLine line = filebuffer[i];

		if (line.Text.StartsWith("/*")) {
		    inComment = true;
		}

// TODO: remove - this is for debugging only
//if (str.Trim().IndexOf("set{localDeliveryDate = value;} //added this line") >= 0) {
//    int foo=0;
//}

		if (!inComment) {
		    //                                                  + && !IsElse(str)
		    if (!line.IsComment 
			    && (line.IsType
				|| (line.IsFlow && (!line.IsElse || flowBracing == BraceStyle.C) && !line.IsElseIf && !line.IsCatch && !line.IsFinally ) 
			    || line.IsFunction 
			    || line.IsProperty
			    || line.IsHangingBrace
			    || line.IsGet
			    || line.IsSet
			    || line.IsLock) 
			&& !line.EndWithCloseBrace
			&& !line.EndWithCloseBraceAndComment
			&& !line.IsClosingBrace ) {

			// if next line is { only, then don't indent next line but 2 lines from then.
			if (filebuffer[i+1].Text.Equals("{")) {
			    skipIndent = true;
			} else {
			    if (line.IsIf) {
				if (line.EndWithBrace || line.EndWithBraceAndComment || filebuffer[i+1].IsHangingBrace || line.EndWithContinuationOperator || filebuffer[i+1].StartWithContinuationOperator) {
				    indent++;
				}
			    } else {
				// the line may be misinterpreted if there is multiline concatination
				if (!filebuffer[i+1].StartWithContinuationOperator && !line.EndWithContinuationOperator) {
				    indent++;
				}
			    }
			}
			// TODO: this may be a problem with nested switch statements
			if (line.IsSwitch) {
			    inSwitch = true;
			    switchIndent = indent;
			}
		    } else if (!line.IsComment && (line.IsElse ||
			line.IsElseIf ||
			line.IsCatch ||
			line.IsFinally) ) {
			if (flowBracing == BraceStyle.Block && (line.StartWithOpenBrace || filebuffer[i-1].EndWithBrace)) {
			    currentIndent--;
			}

			if (flowBracing == BraceStyle.Block && (!line.StartWithOpenBrace && !(i>0 && filebuffer[i-1].EndWithBrace)) && (line.IsElse || line.IsElseIf)) {
			    indent++;
			}

			// handle single line else statments (bad idea in my opinion)
			if (line.IsElse && !line.EndWithBrace && !line.EndWithBraceAndComment && !line.EndWithContinuationOperator && !filebuffer[i+1].StartWithContinuationOperator) {
			    indent--;
			}
		    } else if (!line.IsComment && line.IsClosingBrace) {
			indent--;
			currentIndent--;

			// TODO: this may be a problem with nested switch statements
			if (inSwitch && (indent == switchIndent || (filebuffer[i-1].IsBreak && indent==switchIndent-1) ) && !line.IsComment && line.IsClosingBrace) {
			    inSwitch = false;
			    indent = switchIndent -1;
			    currentIndent = indent;
			    switchIndent=0;
			}
		    } else if (!line.IsComment && (line.IsCase || line.IsSwitchDefault)) {
			currentIndent = switchIndent;
			indent = currentIndent +1;
		    } else if (!line.IsComment && line.IsBreak && inSwitch) {
			indent--;
		    }

		    // handle method calls that are broken into multiple lines and string concatination
		    if (line.StartWithContinuationOperator || (i > 0 && filebuffer[i-1].EndWithContinuationOperator)) {
			currentIndent++;
		    } else if (i > 0 && !filebuffer[i-1].IsComment && (filebuffer[i-1].IsIf || filebuffer[i-1].IsElse) && !filebuffer[i-1].EndWithBrace && !filebuffer[i-1].EndWithBraceAndComment && !filebuffer[i-1].EndWithSemicolon) {
			// handle single line if/else statements
			currentIndent++;
		    }

		    // handle single line while/for/foreach statements
		    if (i > 0 && !filebuffer[i-1].IsComment && (filebuffer[i-1].IsWhile || filebuffer[i-1].IsFor || filebuffer[i-1].IsForEach) && !filebuffer[i-1].EndWithBrace && !filebuffer[i-1].EndWithBraceAndComment && !filebuffer[i-1].EndWithSemicolon) {
			indent--;
		    }
		}


		if (line.Text.EndsWith("*/")) {
		    inComment = false;
		}

		filebuffer[i].Indent = currentIndent;

		if (skipIndent) {
		    i++;
		    filebuffer[i].Indent = currentIndent;

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
		if (filebuffer[i].IsBlankLine) {
		    filebuffer[i] = new StylerLine(String.Empty, filebuffer);
		}
	    }

	}

	private String GetIndentation(Int32 indent) {
	    // safety to prevent errors if indent is < 0
	    if (indent < 0) {
		indent = 0;
		if (!hadIndentWarning) {
		    Console.Out.WriteLine("WARNING: indent level < 0 in " + this.File);
			hadIndentWarning = true;
		}
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

	public void FixHangingBrace(StylerLine line) {
	    int strloc = filebuffer.IndexOf(line);
	    int brcloc = FindHangingBrace(strloc);
	    int diff = brcloc - strloc;
	    if (brcloc > 0) {
		for (int i = 0; i < diff+1; i++) {
		    filebuffer.RemoveAt(strloc);
		}
		filebuffer.Insert(strloc, new StylerLine(line.Text + " {", filebuffer));
		if (linespace) {
		    filebuffer.Insert(strloc+1, new StylerLine("", filebuffer));
		}
	    } else {
	    }
	}

	public int FindHangingBrace(int strloc) {
	    strloc++;
	    bool found = false;
	    while (!found) {
		try {
		    //string str = filebuffer[strloc++].Text.Replace("\t", "");
		    filebuffer[strloc] = new StylerLine(filebuffer[strloc].Text.Replace("\t", ""), filebuffer);
		    StylerLine line = filebuffer[strloc];
		    strloc++;
		    found = line.IsHangingBrace;
		    if (found && line.Text != "{") {
			filebuffer.Insert(strloc, new StylerLine(line.Text.Substring(1), filebuffer));
		    }
		    if (!found && !line.IsBlankLine) {
			return -1;
		    }
		} catch (Exception) {
		    return -1;
		}
	    }
	    return strloc -1;
	}

	public void FixEndBrace(StylerLine line) {
	    int strloc = filebuffer.IndexOf(line);
	    filebuffer.RemoveAt(strloc);
	    filebuffer.Insert(strloc, line.RemoveEndBrace);
	    filebuffer.Insert(strloc+1, line.AddHangingBrace);
	}

	public bool IsBadMonoType(StylerLine line) {
	    if (line.IsType) {
		if (typeBracing == BraceStyle.Block && !line.EndWithBrace || typeBracing == BraceStyle.C && line.EndWithBrace) {
		    return true;
		}
	    }

	    return false;
	}

	public bool IsBadMonoFlow(StylerLine line) {
	    if (line.IsFlow) {
		if (flowBracing == BraceStyle.Block && !line.EndWithBrace || flowBracing == BraceStyle.C && line.EndWithBrace) {
		    return true;
		}
	    }

	    return false;
	}

	public bool IsBadMonoFunction(StylerLine line) {
	    if (line.IsFunction) {
		if (functionBracing == BraceStyle.Block && !line.EndWithBrace || functionBracing == BraceStyle.C && line.EndWithBrace) {
		    return true;
		}
	    }

	    return false;
	}

	public bool IsBadMonoProperty(StylerLine line) {
	    if (line.IsProperty) {
		if (propertyBracing == BraceStyle.Block && !line.EndWithBrace || propertyBracing == BraceStyle.C && line.EndWithBrace) {
		    return true;
		}
	    }

	    return false;
	}

	public bool IsBadPropertyAccessor(StylerLine line) {
	    if (line.IsGet || line.IsSet) {
		if (propertyBracing == BraceStyle.Block && !line.EndWithBrace || propertyBracing == BraceStyle.C && line.EndWithBrace) {
		    return true;
		}
	    }

	    return false;
	}

	public void IsBadMonoStyle(StylerLine line) {
	    if (IsBadMonoType(line)) {
		if (typeBracing == BraceStyle.Block) {
		    FixHangingBrace(line);
		} else {
		    FixEndBrace(line);
		}
	    } else if(IsBadMonoFlow(line)) {
		if (flowBracing == BraceStyle.Block) {
		    FixHangingBrace(line);
		} else {
		    FixEndBrace(line);
		}
	    } else if(IsBadMonoFunction(line)) {
		if (functionBracing == BraceStyle.Block) {
		    FixHangingBrace(line);
		} else {
		    FixEndBrace(line);
		}
	    } else if(IsBadMonoProperty(line) || IsBadPropertyAccessor(line)) {
		if (propertyBracing == BraceStyle.Block) {
		    FixHangingBrace(line);
		} else {
		    FixEndBrace(line);
		}
	    }
	}


    }
}
