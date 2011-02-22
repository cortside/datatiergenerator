using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.IO;
using System.Xml;

using Spring2.Core.Util;

namespace Spring2.DataTierGenerator.UpdateProjectFiles {

    /// <summary>
    /// Updates a project with files not already included in project based on file pattern arguments
    /// </summary>
    public class UpdateProjectFiles {
	private static Boolean recurse = false;
	private static Boolean showHelp = false;
	private static String searchPattern = "*.cs";
	private static List<string> directoriesToProcess = new List<string>();
	private static List<string> filesToProcess = new List<string>();
	private static FileInfo project = null;
	private static OptionSet os = null;

	private static List<string> files = new List<string>();

	/// <summary>
	/// The main entry point for the application.
	/// </summary>
	[STAThread]
	static void Main(string[] args) {
	    ParseArguments(args);
	    Boolean valid = Validate();

	    if (showHelp) {
		ShowHelp(os);
	    } else if (valid) {
		ExecuteTask();
	    } else {
		SuggestHelp();
	    }
	}

	private static void ExecuteTask() {
	    GetFiles();

	    XmlDocument doc = new XmlDocument();
	    doc.Load(project.FullName);

	    Boolean updated = false;
	    if (doc.DocumentElement.Name.Equals("VisualStudioProject")) {
		updated = AddFilesVS2003(doc);
	    } else if (doc.DocumentElement.Name.Equals("Project")) {
		updated = AddFilesVS2005(doc);
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

	private static bool AddFilesVS2003(XmlDocument doc) {
	    XmlNode include = doc.SelectSingleNode("/VisualStudioProject/CSHARP/Files/Include");

	    if (include == null) {
		Console.Out.WriteLine("ERROR: Include node not found");
		return false;
	    }

	    Boolean updated = false;
	    foreach (String filename in files) {
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

	private static bool AddFilesVS2005(XmlDocument doc) {
	    //Instantiate an XmlNamespaceManager object. 
	    XmlNamespaceManager xmlnsManager = new XmlNamespaceManager(doc.NameTable);

	    //Add the namespaces used in books.xml to the XmlNamespaceManager.
	    xmlnsManager.AddNamespace("default", "http://schemas.microsoft.com/developer/msbuild/2003");

	    XmlNodeList itemGroups = doc.SelectNodes("/default:Project/default:ItemGroup", xmlnsManager);
	    XmlNode include = null;
	    foreach (XmlNode node in itemGroups) {
		if (node.FirstChild.Name == "Compile") {
		    include = node;
		}
	    }

	    if (include == null) {
		Console.Out.WriteLine("ERROR: ItemGroup node with Compile nodes not found");
		return false;
	    }

	    Boolean updated = false;
	    foreach (String filename in files) {
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

	private static void GetFiles() {
	    foreach (String directory in directoriesToProcess) {
		if (recurse) {
		    AddFilesRecursively(directory);
		} else {
		    AddFiles(directory);
		}
	    }

	    foreach (String file in filesToProcess) {
		files.Add(file);
	    }
	}

	private static void AddFilesRecursively(String directory) {
	    AddFiles(directory);
	    List<String> subDirectories = new List<String>(Directory.GetDirectories(directory));
	    foreach (String subDir in subDirectories) {
		AddFilesRecursively(subDir);
	    }

	}

	private static void AddFiles(String directory) {
	    files.AddRange(Directory.GetFiles(directory, searchPattern));
	}

	private static void ParseArguments(string[] args) {
	    var os = new OptionSet(){
		{"p|project=", "The fileName of the project", p => project = new FileInfo(p) },
		{"d|directories=", "Comma separated directories to process", d => directoriesToProcess.AddRange(d.Split(',')) },
		{"f|files=", "Comma separated files to process", f => filesToProcess.AddRange(f.Split(',')) },
		{"s|searchPattern=", "Allows you to specify which files to process. Default is *.cs", s => searchPattern = s },
		{"h|help", "Show help message and exists", h => showHelp = h != null },
		{"r|recurse", "Recurse on all given directories", r => recurse = r != null }
	    };

	    try {
		os.Parse(args);
	    } catch (OptionException e) {
		Console.Write("UpdateProjectFiles: ");
		Console.WriteLine(e.Message);
		SuggestHelp();
	    }
	}

	static Boolean Validate() {
	    bool valid = true;


	    if (project == null){
		Console.Out.WriteLine("Invalid Project Specified");
		valid = false;
	    }else if(!project.Exists) {
		Console.Out.WriteLine("Project file could not be found: " + project.FullName);
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
	}

	static void SuggestHelp() {
	    Console.WriteLine("Try `UpdateProjectFiles.exe --help' for more information.");
	    Console.ReadLine();
	}
    }
}
