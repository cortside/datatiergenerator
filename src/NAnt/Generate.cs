using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

using NAnt;
using NAnt.Core;
using NAnt.Core.Attributes;

using NVelocity.App;
using NVelocity.Runtime;

using Spring2.Core.Xml;

using Spring2.DataTierGenerator.Generator;

namespace Spring2.DataTierGenerator.NAntTasks {
    
    [TaskName("generate")]
    public class Generate : Task {

	private FileInfo configFile = null;

	/// <summary>
	/// Sets the project file (i.e. foo.csproj)
	/// </summary>
	[TaskAttribute("configfile", Required=true)]
	public virtual String ConfigFile {
	    set {
		this.configFile = new FileInfo(value);
	    }
	}
        
	protected override void ExecuteTask() {
	    if (configFile == null) {
		throw new BuildException("no project specified", Location);
	    }

	    // use Project.BaseDirectory to setup logging and template path (as NAnt changes the current directory to where the nant.exe is)
	    Velocity.SetProperty(RuntimeConstants_Fields.RUNTIME_LOG, Project.BaseDirectory + @"\nvelocity.log");
	    Velocity.SetProperty(RuntimeConstants_Fields.FILE_RESOURCE_LOADER_PATH, Project.BaseDirectory);

	    try {
		Log(NAnt.Core.Level.Info, String.Empty.PadLeft(20,'='));
		Log(NAnt.Core.Level.Info, "Start :: " + DateTime.Now.ToString());
		Log(NAnt.Core.Level.Info, String.Empty.PadLeft(20,'='));

		XmlDocument doc = new XmlDocument();
		// while this might seem silly, extended ASCII chararcter encoding does not happen if just the filename 
		// is passed to the Load method
		StreamReader reader = configFile.OpenText();
		doc.Load(reader);
		reader.Close();

		XmlNode pi = GetProcessingInstruction(doc, "dtg");
		String parserClassname = GetProcessingInstructionAttribute(pi, "parser");

		// instantiate the parser class
		System.Type clazz = System.Type.GetType(parserClassname, true);
		Object o = System.Activator.CreateInstance(clazz);
		if (o is IParser) {
		    IParser parser = (IParser) o;
		    parser.Parse(configFile.FullName);

		    if (parser.IsValid) {
			IGenerator generator = null;
			try {
			    // locate and instanciate the Generator class specified by the parser
			    clazz = System.Type.GetType(parser.Generator, true);
			    o = System.Activator.CreateInstance(clazz);
			    if (o is IGenerator) {
				generator = (IGenerator) o;
			    } else  {
				Log(NAnt.Core.Level.Info, "ERROR: class " + parser.Generator + " does not support IGenerator interface.\n");
			    }
			} catch (Exception ex) {
			    Log(NAnt.Core.Level.Info, "ERROR: could not instanciate generator class " + parser.Generator + "\n" + ex);
			}

			// if the generator is not null, generate
			if (generator != null) {
			    generator.Generate(parser);
			    LogToNAnt(generator.Log);
			}
		    } else {
			Log(NAnt.Core.Level.Info, "ERROR: Parser found errors:");
			LogToNAnt(parser.Log);
		    }
		} else  {
		    Log(NAnt.Core.Level.Info, "ERROR: class " + parserClassname + " does not support IParser interface.\n");
		}


		Log(NAnt.Core.Level.Info, String.Empty.PadLeft(20,'='));
		Log(NAnt.Core.Level.Info, "Done :: " + DateTime.Now.ToString());
		Log(NAnt.Core.Level.Info, String.Empty.PadLeft(20,'='));
	    } catch (Exception ex) {
		Log(NAnt.Core.Level.Info, "An error occcurred while generating.\n\n" + ex.ToString());
	    }
	}

	private void LogToNAnt(IList messages) {
	    foreach(String s in messages) {
		Log(NAnt.Core.Level.Info, s);
	    }
	}

	protected XmlNode GetProcessingInstruction(XmlDocument doc, String pi) {
	    foreach(XmlNode node in doc.ChildNodes) {
		if (node.Name.Equals(pi)) {
		    return node;
		}
	    }
	    return null;
	}

	protected String GetProcessingInstructionAttribute(XmlNode node, String attribute) {
	    String s = node == null ? String.Empty : node.Value;
	    if (s != null && s.IndexOf(attribute + "=\"") >= 0) { 
		String value = s.Substring(s.IndexOf(attribute + "=\"") + attribute.Length + 2);
		return value.Substring(0,value.IndexOf("\""));
	    }

	    return null;
	}

    }
}
