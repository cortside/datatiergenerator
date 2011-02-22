using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Reflection;

using Spring2.Core.Xml;
using Spring2.Core.Util;

using Spring2.DataTierGenerator.Generator;

namespace Spring2.DataTierGenerator.ConsoleRunner {

    /// <summary>
    /// Console runner
    /// </summary>
    class ConsoleRunner {
	private static Boolean verbose = false;
	private static Boolean showHelp = false;
	private static String filename = String.Empty;
	private static OptionSet os = null;

	/// <summary>
	/// Assembly entry point
	/// </summary>
	[STAThread]
	static void Main(string[] args) {
	    ParseArguments(args);
	    Boolean valid = Validate();

	    if (showHelp) {
		ShowHelp(os);
	    }else if (valid) {
		Generate(filename, verbose);
		//Console.ReadKey();
	    } else {
		SuggestHelp();
	    }
	}

	private static void Generate(String filename, Boolean verbose) {
	    FileInfo file = new FileInfo(filename);
	    if (!file.Exists) {
		Console.Out.WriteLine("could not find config file: " + file.FullName);
		return;
	    }
	    try {
		Console.Out.WriteLine(String.Empty.PadLeft(40, '='));
		Console.Out.WriteLine("Start :: " + DateTime.Now.ToString());
		Console.Out.WriteLine(String.Empty.PadLeft(40, '='));

		XmlDocument doc = new XmlDocument();
		// while this might seem silly, extended ASCII character encoding does not happen if just the filename 
		// is passed to the Load method
		StreamReader reader = file.OpenText();
		doc.Load(reader);
		reader.Close();

		XmlNode pi = GetProcessingInstruction(doc, "dtg");
		String parserClassname = GetProcessingInstructionAttribute(pi, "parser");

		// instantiate the parser class
		System.Type clazz = System.Type.GetType(parserClassname, true);
		Object o = System.Activator.CreateInstance(clazz);
		if (o is IParser) {
		    IParser parser = (IParser)o;
		    parser.Parse(filename);

		    if (parser.IsValid) {
			IGenerator generator = null;
			try {
			    // locate and instantiate the Generator class specified by the parser
			    clazz = System.Type.GetType(parser.Generator, true);
			    o = System.Activator.CreateInstance(clazz);
			    if (o is IGenerator) {
				generator = (IGenerator)o;
			    } else {
				Console.Out.WriteLine("ERROR: class " + parser.Generator + " does not support IGenerator interface.\n");
			    }
			} catch (Exception ex) {
			    Console.Out.WriteLine("ERROR: could not instantiate generator class " + parser.Generator + "\n" + ex);
			}

			// if the generator is not null, generate
			if (generator != null) {
			    if (verbose && generator is NVelocityGenerator) {
				((NVelocityGenerator)generator).AttachToOutputEvent(ConsoleRunner.Output);
			    }
			    generator.Generate(parser);
			    foreach (String s in generator.Log) {
				Console.Out.WriteLine(s);
			    }
			}
		    } else {
			Console.Out.WriteLine("ERROR: Parser found errors:");
			foreach (String s in parser.Log) {
			    Console.Out.WriteLine(s);
			}
		    }
		} else {
		    Console.Out.WriteLine("ERROR: class " + parserClassname + " does not support IParser interface.\n");
		}

		Console.Out.WriteLine(String.Empty.PadLeft(40, '='));
		Console.Out.WriteLine("Done :: " + DateTime.Now.ToString());
		Console.Out.WriteLine(String.Empty.PadLeft(40, '='));
	    } catch (Exception ex) {
		Console.Out.WriteLine("An error occurred while generating.\n\n" + ex.ToString());
	    }
	}

	protected static XmlNode GetProcessingInstruction(XmlDocument doc, String pi) {
	    foreach (XmlNode node in doc.ChildNodes) {
		if (node.Name.Equals(pi)) {
		    return node;
		}
	    }
	    return null;
	}

	protected static String GetProcessingInstructionAttribute(XmlNode node, String attribute) {
	    String s = node == null ? String.Empty : node.Value;
	    if (s != null && s.IndexOf(attribute + "=\"") >= 0) {
		String value = s.Substring(s.IndexOf(attribute + "=\"") + attribute.Length + 2);
		return value.Substring(0, value.IndexOf("\""));
	    }

	    return null;
	}

	public static void Output(String s) {
	    Console.Out.WriteLine(s);
	}

	static void ParseArguments(string[] args) {
	    os = new OptionSet(){
		{"v|verbose", "Increases debug verbosity", v => verbose = v != null},
		{"h|help", "Show help message and exists", h => showHelp = h != null},
		{"f|file=", "The config {FILE} to process", f => filename = f }
	    };

	    try {
		os.Parse(args);
	    } catch (OptionException e) {
		Console.Write("DataTierGenerator: ");
		Console.WriteLine(e.Message);
		SuggestHelp();
	    }
	}

	static Boolean Validate() {
	    bool valid = true;

	    if (string.IsNullOrEmpty(filename)) {
		Console.WriteLine("Try `DataTierGenerator.exe --help' for more information.");
		Console.ReadLine();
		valid = false;
	    }

	    return valid;
	}

	static void ShowHelp(OptionSet p) {
	    Console.WriteLine("Usage: DataTierGenerator.exe [OPTIONS]");
	    Console.WriteLine("Options:");
	    p.WriteOptionDescriptions(Console.Out);
	    Console.WriteLine();
	    Console.WriteLine("Deprecated Usage: DataTierGenerator.exe (optional)-v configFile");
	    Console.ReadLine();
	}

	static void SuggestHelp() {
	    Console.WriteLine("Try `DataTierGenerator.exe --help' for more information.");
	    Console.ReadLine();
	}
    }
}
