using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Xml;

namespace Spring2.DataTierGenerator.UpdateProjectFiles {
 
    /// <summary>
    /// Updates a project with files not already included in project based on file pattern arguments
    /// </summary>
    class UpdateProjectFiles {
	/// <summary>
	/// The main entry point for the application.
	/// </summary>
	[STAThread]
	static void Main(string[] args) {
	    // assumes that the project is the first argument
	    FileInfo project = new FileInfo(args[0]);
	    if (!project.Exists) {
		Console.Out.WriteLine("Project file could not be found: " + project.FullName);
		return;
	    }

	    // the rest of the arguments could be files or directories
	    StringCollection files = new StringCollection();
	    for(Int32 i=1; i < args.Length; i++) {
	    	String[] filenames = Directory.GetFiles(Environment.CurrentDirectory, args[i]);
		files.AddRange(filenames);
	    }

	    ExecuteTask(project, files);
	}

	private static void ExecuteTask(FileInfo project, StringCollection files) {
	    XmlDocument doc = new XmlDocument();
	    doc.Load(project.FullName);

	    XmlNode include = doc.SelectSingleNode("/VisualStudioProject/CSHARP/Files/Include");

	    Boolean updated = false;
	    foreach(String filename in files) {
		FileInfo file = new FileInfo(filename);

		String path = file.FullName.Substring(project.DirectoryName.Length + 1);
		String xpath = "/VisualStudioProject/CSHARP/Files/Include/File[@RelPath='" + path + "']";

		XmlNode node = doc.SelectSingleNode(xpath);
		if (node == null) {
		    Console.Out.WriteLine("Adding " + path + " to " + project.FullName);

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
		Console.Out.WriteLine("Saving " + project.Name);
		doc.Save(project.FullName);
	    } else {
		Console.Out.WriteLine("no new files to add");
	    }
	}

    }
}
