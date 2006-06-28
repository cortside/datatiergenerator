using System;
using System.Collections.Specialized;
using System.IO;
using System.Xml;

namespace Spring2.DataTierGenerator.UpdateProjectFiles {
 
    /// <summary>
    /// Updates a project with files not already included in project based on file pattern arguments
    /// </summary>
    public class UpdateProjectFiles {
	
    	/// <summary>
	/// The main entry point for the application.
	/// </summary>
	[STAThread]
	static void Main(string[] args) {
	    // assumes that the project filename is the first argument
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

	    Boolean updated = false;
	    if (doc.FirstChild.Name.Equals("VisualStudioProject")) {
		updated = AddFilesVS2003(doc, files, project);
	    } else if (doc.FirstChild.Name.Equals("Project")) {
		updated = AddFilesVS2005(doc, files, project);
	    } else {
		Console.Out.WriteLine("ERROR: Unknown project file version");
	    }

	    if (updated) {
		Console.Out.WriteLine("Saving " + project.Name);
		doc.Save(project.FullName);
	    } else {
		Console.Out.WriteLine("no new files to add");
	    }
	}

	private static bool AddFilesVS2003(XmlDocument doc, StringCollection files, FileInfo project) {
	    XmlNode include = doc.SelectSingleNode("/VisualStudioProject/CSHARP/Files/Include");
		
	    if (include == null) {
		Console.Out.WriteLine("ERROR: Include node not found");
		return false;
	    }

	    Boolean updated = false;
	    foreach(String filename in files) {
		FileInfo file = new FileInfo(filename);

		String path = file.FullName.Substring(project.DirectoryName.Length + 1);
		String xpath = "/VisualStudioProject/CSHARP/Files/Include/File[@RelPath='" + path + "']";

		XmlNode node = doc.SelectSingleNode(xpath);
		if (node == null) {
		    Console.Out.WriteLine("Adding " + path + " to " + project.FullName);

		    //<File
		    //    RelPath = "AssemblyVersionInfo.cs"
		    //    SubType = "Code"
		    //    BuildAction = "Compile"
		    ///>

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
	    return updated;
	}
    	
	private static bool AddFilesVS2005(XmlDocument doc, StringCollection files, FileInfo project) {
	    //Instantiate an XmlNamespaceManager object. 
	    XmlNamespaceManager xmlnsManager = new XmlNamespaceManager(doc.NameTable);

	    //Add the namespaces used in books.xml to the XmlNamespaceManager.
	    xmlnsManager.AddNamespace("default", "http://schemas.microsoft.com/developer/msbuild/2003");

	    XmlNodeList itemGroups = doc.SelectNodes("/default:Project/default:ItemGroup", xmlnsManager);
	    XmlNode include = null;
	    foreach(XmlNode node in itemGroups) {
		if (node.FirstChild.Name == "Compile") {
		    include = node;
		}
	    }
		
	    if (include == null) {
		Console.Out.WriteLine("ERROR: ItemGroup node with Compile nodes not found");
	    	return false;
	    }
		
	    Boolean updated = false;
	    foreach(String filename in files) {
		FileInfo file = new FileInfo(filename);

		String path = file.FullName.Substring(project.DirectoryName.Length + 1);
		String xpath = "//default:ItemGroup/default:Compile[@Include='" + path + "']";

		XmlNode node = doc.SelectSingleNode(xpath, xmlnsManager);
		if (node == null) {
		    Console.Out.WriteLine("Adding " + path + " to " + project.FullName);

		    //<Compile Include="BusinessLogic\Address.cs">
		    //  <SubType>Code</SubType>
		    //</Compile>

		    XmlElement element = doc.CreateElement("Compile", "http://schemas.microsoft.com/developer/msbuild/2003");
		    XmlAttribute attribute = doc.CreateAttribute("Include");
		    attribute.Value = path;
		    element.Attributes.Append(attribute);
		
		    XmlElement subType = doc.CreateElement("SubType", "http://schemas.microsoft.com/developer/msbuild/2003");
		    subType.InnerText = "Code";
		    element.AppendChild(subType);

		    include.AppendChild(element);
		    updated = true;
		}
	    }
	    return updated;
	}
    	
    	
    }
}
