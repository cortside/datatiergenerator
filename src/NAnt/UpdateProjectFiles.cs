using System;
using System.Collections;
using System.IO;
using System.Xml;

using NAnt;
using NAnt.Core;
using NAnt.Core.Attributes;
using NAnt.Core.Types;


namespace Spring2.DataTierGenerator.NAntTasks {
    
    [TaskName("updateprojectfiles")]
    public class ProjectFileIncludeTask : Task {

	private FileInfo project = null;
	private FileSet fileset = new FileSet();

	/// <summary>
	/// Sets the project file (i.e. foo.project)
	/// </summary>
	[TaskAttribute("project", Required=true)]
	public virtual String ProjectFile {
	    set {
		this.project = new FileInfo(value);
	    }
	}
        
	/// <summary>
	/// The set of files to be checked
	/// </summary>
	[FileSet("fileset")]
	public FileSet FileSet { 
	    get { return fileset; } 
	    set { this.fileset = value; }
	}


	protected override void ExecuteTask() {
	    if (project == null) {
		throw new BuildException("no project specified", Location);
	    }

	    XmlDocument doc = new XmlDocument();
	    doc.Load(project.FullName);

	    XmlNode include = doc.SelectSingleNode("/VisualStudioProject/CSHARP/Files/Include");

	    Boolean updated = false;
	    foreach(String filename in fileset.FileNames) {
		FileInfo file = new FileInfo(filename);

		String path = file.FullName.Substring(project.DirectoryName.Length + 1);
		String xpath = "/VisualStudioProject/CSHARP/Files/Include/File[@RelPath='" + path + "']";

		XmlNode node = doc.SelectSingleNode(xpath);
		if (node == null) {
		    Log(NAnt.Core.Level.Info, "Adding " + path + " to " + project.FullName);

		    XmlElement element = doc.CreateElement("File");
		    XmlAttribute attribute = doc.CreateAttribute("RelPath");
		    attribute.Value = path;
		    element.Attributes.Append(attribute);

		    attribute = doc.CreateAttribute("SubType");
		    attribute.Value = "Code";
		    element.Attributes.Append(attribute);

		    attribute = doc.CreateAttribute("BuildAction");
		    attribute.Value = "Compile";
		    element.Attributes.Append(attribute);

		    include.AppendChild(element);
		    updated = true;
		}
	    }

	    if (updated) {
		Log(NAnt.Core.Level.Info, "Saving " + project.Name);
		doc.Save(project.FullName);
	    } else {
		Log(NAnt.Core.Level.Info, "no new files to add");
	    }
	}

    }
}
