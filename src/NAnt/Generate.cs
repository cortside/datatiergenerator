using System;
using System.Collections;
using System.IO;
using System.Xml;

using SourceForge.NAnt;
using SourceForge.NAnt.Attributes;

using NVelocity.App;
using NVelocity.Runtime;

using Spring2.Core.Xml;

using Spring2.DataTierGenerator.Element;
using Spring2.DataTierGenerator.Generator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.NAnt {
    
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
		LogToNAnt(String.Empty.PadLeft(20,'='));
		LogToNAnt("Start :: " + DateTime.Now.ToString());
		LogToNAnt(String.Empty.PadLeft(20,'='));

		ConfigParser p = new ConfigParser(configFile.FullName);
		if (p.IsValid) {
		    IGenerator g = null;
		    try {
			// locate and instanciate the Generator class specified by the parser
			System.Type clazz = System.Type.GetType(p.Generator.Class, true);
			Object[] args = { p };
			Object o = System.Activator.CreateInstance(clazz, args);
			if (o is IGenerator) {
			    g = (IGenerator) o;
			} else  {
			    LogToNAnt("ERROR: class " + p.Generator.Class + " does not support IGenerator interface.\n");
			}
		    } catch (Exception ex) {
			LogToNAnt("ERROR: could not instanciate generator class " + p.Generator.Class + "\n" + ex);
		    }

		    // if the generator is not null, generate
		    if (g != null) {
			g.Generate();
			LogToNAnt(g.Log);
		    }
		} else {
		    LogToNAnt("ERROR: Parser found errors:");
		    LogToNAnt(p.Log);
		}

		LogToNAnt(String.Empty.PadLeft(20,'='));
		LogToNAnt("Done :: " + DateTime.Now.ToString());
		LogToNAnt(String.Empty.PadLeft(20,'='));
	    } catch (Exception ex) {
		LogToNAnt("An error occcurred while generating.\n\n" + ex.ToString());
	    }
	}

	private void LogToNAnt(String s) {
	    Log.WriteLine("  [generate] " + s);
	}

	private void LogToNAnt(IList messages) {
	    foreach(String s in messages) {
		LogToNAnt(s);
	    }
	}


    }
}
