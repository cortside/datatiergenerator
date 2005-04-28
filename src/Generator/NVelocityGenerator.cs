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
using Spring2.Core.Util;
using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Generator.Writer;
using Spring2.DataTierGenerator.Generator.Styler;


namespace Spring2.DataTierGenerator.Generator {

    public class NVelocityGenerator : IGenerator {

	private IList log = new ArrayList();
	private Int64 generateTicks = 0;
	private Int64 mergeTicks = 0;
	private Int64 stylerTicks = 0;
	private Int64 writerTicks = 0;
	private Int64 files = 0;

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
	    try {
		Velocity.GetTemplate("Template\\dtg_csharp_library.vm");
	    } catch (ResourceNotFoundException) {
		WriteToLog("ERROR: Unable to locate Template\\dtg_csharp_library.vm");
		hasErrors = true;
	    }

	    try {
		Velocity.GetTemplate("Template\\dtg_java_library.vm");
	    } catch (ResourceNotFoundException) {
		WriteToLog("ERROR: Unable to locate Template\\dtg_java_library.vm");
		hasErrors = true;
	    }
		
	    foreach(ITask task in parser.Tasks) {
		try {
		    Velocity.GetTemplate(task.Template);
		} catch (ResourceNotFoundException) {
		    WriteToLog("ERROR: Unable to locate " + task.Template);
		    hasErrors = true;
		}
	    }

	    if (parser.IsValid && !hasErrors) {
		Timer timer = new Timer();
		WriteToLog("Starting generation");
		WriteToLog(parser.Tasks.Count.ToString() + " generator tasks");
		if (parser.Log.Count>0) {
		    WriteToLog(String.Empty.PadLeft(40,'-'));
		    WriteToLog("The parser is in a valid state, but reported the following issues:");
		    foreach(String s in parser.Log) {
			WriteToLog(s);
		    }
		}

		WriteToLog(String.Empty.PadLeft(40,'-'));
		foreach(ITask task in parser.Tasks) {
		    Template template = Velocity.GetTemplate(task.Template);
		    foreach(IElement element in task.Elements) {
			GenerateFile(parser, element, task, template);
		    }
		}

		timer.Stop();
		WriteToLog(String.Empty.PadLeft(40,'-'));
		WriteToLog("files processed: " + files);
		WriteToLog("       generate: " + new TimeSpan(generateTicks).TotalMilliseconds.ToString("####0.000").PadLeft(9) + "ms (" + (new TimeSpan(generateTicks).TotalMilliseconds / timer.TimeSpan.TotalMilliseconds).ToString("P") + ")");
		WriteToLog(" template merge: " + new TimeSpan(mergeTicks).TotalMilliseconds.ToString("####0.000").PadLeft(9) + "ms (" + (new TimeSpan(mergeTicks).TotalMilliseconds / timer.TimeSpan.TotalMilliseconds).ToString("P") + ")");
		WriteToLog("         styler: " + new TimeSpan(stylerTicks).TotalMilliseconds.ToString("####0.000").PadLeft(9) + "ms (" + (new TimeSpan(stylerTicks).TotalMilliseconds / timer.TimeSpan.TotalMilliseconds).ToString("P") + ")");
		WriteToLog("         writer: " + new TimeSpan(writerTicks).TotalMilliseconds.ToString("####0.000").PadLeft(9) + "ms (" + (new TimeSpan(writerTicks).TotalMilliseconds / timer.TimeSpan.TotalMilliseconds).ToString("P") + ")");
	    } else {
		WriteToLog("Parser was not in a valid state and reported the following errors:");
		foreach(String s in parser.Log) {
		    WriteToLog(s);
		}
	    }
	}

	private void GenerateFile(IParser parser, IElement element, ITask task, Template template) {
	    Timer generateTimer = new Timer();
	    files++;
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

	    Timer timer = new Timer();
	    try {
	    	timer.Start();
		template.Merge(vc, writer);
		timer.Stop();
		mergeTicks += timer.TimeSpan.Ticks;

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
			timer.Start();
			String styledContent = s.Style(content);
			timer.Stop();
			stylerTicks += timer.TimeSpan.Ticks;

			timer.Start();
			Boolean changed = w.Write(file, styledContent);
			timer.Stop();
			writerTicks += timer.TimeSpan.Ticks;
			
			if (changed) {
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
	    
	    generateTimer.Stop();
	    generateTicks += generateTimer.TimeSpan.Ticks;
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

