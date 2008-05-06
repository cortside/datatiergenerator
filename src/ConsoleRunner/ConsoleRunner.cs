using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Reflection;

using Spring2.Core.Xml;

using Spring2.DataTierGenerator.Generator;

namespace Spring2.DataTierGenerator.ConsoleRunner {

    /// <summary>
    /// Console runner
    /// </summary>
    class ConsoleRunner {

	/// <summary>
	/// Assembly entry point
	/// </summary>
	[STAThread]
	static void Main(string[] args) {
	    if (args.Length==1) {
		Generate(args[0]);
	    } else {
		Console.Out.WriteLine("usage: DataTierGenerator.exe <xml config filename>");
	    }
	}

	private static void Generate(String filename) {
	    FileInfo file = new FileInfo(filename);
	    if (!file.Exists) {
		Console.Out.WriteLine("could not find config file: " + file.FullName);
		return;
	    }
	    try {
		Console.Out.WriteLine(String.Empty.PadLeft(40,'='));
		Console.Out.WriteLine("Start :: " + DateTime.Now.ToString());
		Console.Out.WriteLine(String.Empty.PadLeft(40,'='));

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
		    IParser parser = (IParser) o;
		    parser.Parse(filename);

		    if (parser.IsValid) {
			IGenerator generator = null;
			try {
			    // locate and instantiate the Generator class specified by the parser
			    clazz = System.Type.GetType(parser.Generator, true);
			    o = System.Activator.CreateInstance(clazz);
			    if (o is IGenerator) {
				generator = (IGenerator) o;
			    } else  {
				Console.Out.WriteLine("ERROR: class " + parser.Generator + " does not support IGenerator interface.\n");
			    }
			} catch (Exception ex) {
			    Console.Out.WriteLine("ERROR: could not instantiate generator class " + parser.Generator + "\n" + ex);
			}

			// if the generator is not null, generate
			if (generator != null) {
			    generator.Generate(parser);
			    foreach(String s in generator.Log) {
				Console.Out.WriteLine(s);
			    }
			}
		    } else {
			Console.Out.WriteLine("ERROR: Parser found errors:");
			foreach(String s in parser.Log) {
			    Console.Out.WriteLine(s);
			}
		    }
		} else  {
		    Console.Out.WriteLine("ERROR: class " + parserClassname + " does not support IParser interface.\n");
		}

		Console.Out.WriteLine(String.Empty.PadLeft(40,'='));
		Console.Out.WriteLine("Done :: " + DateTime.Now.ToString());
		Console.Out.WriteLine(String.Empty.PadLeft(40,'='));
	    } catch (Exception ex) {
		Console.Out.WriteLine("An error occurred while generating.\n\n" + ex.ToString());
	    }
	}

	protected static XmlNode GetProcessingInstruction(XmlDocument doc, String pi) {
	    foreach(XmlNode node in doc.ChildNodes) {
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
		return value.Substring(0,value.IndexOf("\""));
	    }

	    return null;
	}

    }
}
