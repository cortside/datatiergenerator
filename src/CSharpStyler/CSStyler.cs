using System;
using Spring2.DataTierGenerator.Generator.Styler;
using System.IO;
using System.Text;

namespace Spring2.DataTierGenerator.CSharpStyler {

    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    class CSStyler {

	public static void Main (string[] args) {
	    String version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
	    // TODO: need --version or quiet mode
	    //Console.Out.WriteLine("CSharpStyle version " + version);
	    Generator.Styler.CSharpStyler style = new Generator.Styler.CSharpStyler();

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

	private static void ParseOptions(Generator.Styler.CSharpStyler style, string[] args) {
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


    }
}
