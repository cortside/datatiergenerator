using System;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator.Core {
    public class Configuration : ConfigurationData {

	public Configuration () {
	}

	public Configuration(XmlNode root) {
	    if (root.HasChildNodes) {
		for (Int32 i=0; i<root.ChildNodes.Count; i++) {
		    XmlNode node = root.ChildNodes[i];
		    if (node.Name.Equals("setting")) {
			switch(node.Attributes["name"].Value.ToLower()) {
			    case "rootnamespace":
				this.rootNameSpace = node.Attributes["value"].Value;
				break;
			    case "rootdirectory":
				this.rootDirectory = node.Attributes["value"].Value;
				break;
			    case "typesclassdirectory":
				this.typesClassDirectory = node.Attributes["value"].Value;
				break;
			    case "daoclassdirectory":
				this.daoClassDirectory = node.Attributes["value"].Value;
				break;
			    case "doclassdirectory":
				this.doClassDirectory = node.Attributes["value"].Value;
				break;
			    case "collectionclassdirectory":
				this.collectionClassDirectory = node.Attributes["value"].Value;
				break;
			    case "generatedataobjectclasses":
				this.generateDataObjectClasses = Boolean.Parse(node.Attributes["value"].Value);
				break;
			    case "dataobjectbaseclass":
				this.dataObjectBaseClass= node.Attributes["value"].Value;
				break;
			    case "daobaseclass":
				this.daoBaseClass= node.Attributes["value"].Value;
				break;
			    case "enumbaseclass":
				this.enumBaseClass= node.Attributes["value"].Value;
				break;
			    default:
				Console.Out.WriteLine("Unrecognized configuration option: " + node.Attributes["name"].Value + " = " + node.Attributes["value"].Value);
				break;
			}
		    }
		}
	    }
	    Console.Out.Write("\n");
	    Console.Out.WriteLine(String.Empty.PadLeft(20,'='));
	    Console.Out.WriteLine("Configuration Information");
	    Console.Out.WriteLine(String.Empty.PadLeft(20,'='));
	    Console.Out.WriteLine(ToString());
	    Console.Out.WriteLine(String.Empty.PadLeft(20,'='));
	}

	// methods
	public String GetProcName(String table, String type) {
	    String s;

	    s = "proc" + table.Replace(" ", "_") + type;
	    s = "sp" + table.Replace(" ", "_") + "_" + type;

	    return s;
	}

	public String GetDAONameSpace(String table) {
	    String s;

	    s = this.rootNameSpace;
	    if (daoClassDirectory.Length>0) {
		s += "." + daoClassDirectory;
	    }

	    return s;
	}

	public String GetDONameSpace(String table) {
	    String s;

	    s = this.rootNameSpace;
	    if (doClassDirectory.Length>0) {
		s += "." + doClassDirectory;
	    }

	    return s;
	}

	public String GetDAOClassName(String table) {
	    String s;

	    s = "cls" + table.Replace(" ", "_");
	    s = table.Replace(" ", "_") + "DAO";

	    return s;
	}

	public String GetDOClassName(String table) {
	    String s;

	    s = "cls" + table.Replace(" ", "_");
	    s = table.Replace(" ", "_") + "Data";

	    return s;
	}

	public String GetTypeClassName(String name) {
	    return name;
	}

	public String GetTypeNameSpace(String name) {
	    String s = this.rootNameSpace;
	    if (typesClassDirectory.Length>0) {
		s += "." + typesClassDirectory;
	    }
	    return s;
	}

	public String GetCollectionClassName(String name) {
	    return name;
	}

	public String GetCollectionNameSpace(String name) {
	    String s = this.rootNameSpace;
	    if (collectionClassDirectory.Length>0) {
		s += "." + collectionClassDirectory;
	    }
	    return s;
	}

    }
}
