using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;

using NVelocity;
using NVelocity.App;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Generator.Writer;

namespace Spring2.DataTierGenerator.Generator {

    public class NVelocityGenerator : IGenerator {

	private IList log = new ArrayList();

	public NVelocityGenerator() {
	    InitNVelocity();
	}


	/// <summary>
	/// Init the NVelocity singleton
	/// </summary>
	private void InitNVelocity() {
	    Velocity.SetProperty(NVelocity.Runtime.RuntimeConstants_Fields.FILE_RESOURCE_LOADER_CACHE, true);
	    Velocity.Init();
	}

	
	public void Generate(IParser parser) {
	    if (parser.IsValid) {
		if (parser.Log.Count>0) {
		    WriteToLog("The parser is in a valid state, but reported the following issues:");
		    foreach(String s in parser.Log) {
			WriteToLog(s);
		    }
		}

		foreach(ITask task in parser.Tasks) {
//WriteToLog("executing task: " + task.Template);
		    foreach(IElement element in task.Elements) {
			GenerateFile(parser, element, task);
		    }
		}
	    } else {
		WriteToLog("Parser was not in a valid state and reported the following errors:");
		foreach(String s in parser.Log) {
		    WriteToLog(s);
		}
	    }
	}


	private void GenerateFile(IParser parser, IElement element, ITask task) {
	    StringWriter writer = new StringWriter();

	    VelocityContext vc = new VelocityContext();
	    foreach(Object key in parser.Tools.Keys) {
		vc.Put(key.ToString(), parser.Tools[key]);
	    }
	    foreach(Object key in task.Parameters.Keys) {
		vc.Put(key.ToString(), task.Parameters[key]);
	    }
	    vc.Put("dtgversion", this.GetType().Assembly.FullName);
	    vc.Put("options", parser.Configuration);
	    vc.Put("element", element);
	    vc.Put("elements", task.Elements);

	    Template template = Velocity.GetTemplate("Template\\dtg_csharp_library.vm");
	    template = Velocity.GetTemplate("Template\\dtg_java_library.vm");
	    template = Velocity.GetTemplate(task.Template);
	    template.Merge(vc, writer);

	    FileInfo file = new FileInfo(task.Directory + "\\" + String.Format(task.FileNameFormat, element.Name));
	    String content = writer.ToString();
	    if (content.Length > 0) {
		IWriter w = WriterFactory.GetWriter(task.Writer);
		try {
		    if (w.Write(file, content)) {
			WriteToLog(w.Log);
			WriteToLog("generating " + file.FullName);
		    } 
		} catch(Exception ex) { 
		    WriteToLog("error generating " + file.FullName + " -- " + ex.Message);
		}
	    }
	}

	protected void WriteToLog(String s) {
	    log.Add(s);
	}

	protected void WriteToLog(IList list) {
	    foreach(String s in list) {
		log.Add(s);
	    }
	}

	public IList Log {
	    get { return log; }
	}


    }
}

