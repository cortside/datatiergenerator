using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;

using NVelocity;
using NVelocity.App;
using NVelocity.Exception;
using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Generator.Writer;
using Spring2.DataTierGenerator.Generator.Styler;


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
	    Boolean hasErrors = false;
	    foreach(ITask task in parser.Tasks) {
		try {
		    Velocity.GetTemplate(task.Template);
		} catch (ResourceNotFoundException) {
		    WriteToLog("ERROR: Unable to locate " + task.Template);
		    hasErrors = true;
		}
	    }

	    if (parser.IsValid && !hasErrors) {
		WriteToLog("Starting generation");
		if (parser.Log.Count>0) {
		    WriteToLog("The parser is in a valid state, but reported the following issues:");
		    foreach(String s in parser.Log) {
			WriteToLog(s);
		    }
		}

		foreach(ITask task in parser.Tasks) {
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
	    vc.Put("task", task);

	    try {
		Template template = Velocity.GetTemplate("Template\\dtg_csharp_library.vm");
		template = Velocity.GetTemplate("Template\\dtg_java_library.vm");
		template = Velocity.GetTemplate(task.Template);
		template.Merge(vc, writer);

		FileInfo file = new FileInfo(task.Directory + "\\" + task.FileNameFormat.Replace("{removewhitespace(element.Name)}", element.Name.Replace(" ", String.Empty)).Replace("{element.Name}", element.Name));
		String content = writer.ToString();
		if (content.Length > 0) {
		    IStyler s = parser.GetStyler(task.Styler);
		    if (s == null) {
			s = new NoStyler();
		    }
		    s.File = file.FullName;
		    IWriter w = parser.GetWriter(task.Writer);
		    try {
			w.BackupFilePath = task.BackupDirectory + "\\" + file.Name + "~";
			if (w.Write(file, s.Style(content))) {
			    WriteToLog(w.Log);
			    w.Log.Clear();
			    WriteToLog("generating " + file.FullName);
			} 
		    } catch(Exception ex) { 
			WriteToLog("error generating " + file.FullName + " -- " + ex.Message);
		    }
		}
	    } catch (Exception ex) {
		WriteToLog(ex.ToString());                                                                      
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

