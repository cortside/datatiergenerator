using System;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator {
    public class Configuration : ConfigurationData {

	public Configuration () {
	}

	public Configuration(XmlNode root) {
	    if (root.HasChildNodes) {
		for (Int32 i=0; i<root.ChildNodes.Count; i++) {
		    XmlNode node = root.ChildNodes[i];
		    switch(node.Attributes["name"].Value.ToLower()) {
			case "rootnamespace":
			    this.rootNameSpace = node.Attributes["value"].Value;
			    break;
			case "server":
			    this.server = node.Attributes["value"].Value;
			    break;
			case "database":
			    this.database = node.Attributes["value"].Value;
			    break;
			case "user":
			    this.user = node.Attributes["value"].Value;
			    break;
			case "password":
			    this.password = node.Attributes["value"].Value;
			    break;
			case "rootdirectory":
			    this.rootDirectory = node.Attributes["value"].Value;
			    break;
			case "sqlscriptdirectory":
			    this.sqlScriptDirectory = node.Attributes["value"].Value;
			    break;
			case "daoclassdirectory":
			    this.daoClassDirectory = node.Attributes["value"].Value;
			    break;
			case "doclassdirectory":
			    this.doClassDirectory = node.Attributes["value"].Value;
			    break;
			case "singlefile":
			    this.singleFile = Boolean.Parse(node.Attributes["value"].Value);
			    break;
			case "generatesqlviewscripts":
			    this.generateSqlViewScripts = Boolean.Parse(node.Attributes["value"].Value);
			    break;
			case "generatesqltablescripts":
			    this.generateSqlTableScripts = Boolean.Parse(node.Attributes["value"].Value);
			    break;
			case "generatedataobjectclasses":
			    this.generateDataObjectClasses = Boolean.Parse(node.Attributes["value"].Value);
			    break;
			case "scriptdropstatement":
			    this.scriptDropStatement = Boolean.Parse(node.Attributes["value"].Value);
			    break;
			case "storedprocnameformat":
			    this.storedProcNameFormat = node.Attributes["value"].Value;
			    break;
			case "generateprocsforforeignkey":
			    this.generateProcsForForeignKey = Boolean.Parse(node.Attributes["value"].Value);
			    break;
			case "generateselectstoredprocs":
			    this.generateSelectStoredProcs= Boolean.Parse(node.Attributes["value"].Value);
			    break;
			case "generateonlyprimarydeletestoredproc":
			    this.generateOnlyPrimaryDeleteStoredProc= Boolean.Parse(node.Attributes["value"].Value);
			    break;
			case "allowupdateofprimarykey":
			    this.allowUpdateOfPrimaryKey = Boolean.Parse(node.Attributes["value"].Value);
			    break;
			case "useviews":
			    this.allowUpdateOfPrimaryKey = Boolean.Parse(node.Attributes["value"].Value);
			    break;
			case "autodiscoverentities":
			    this.autoDiscoverEntities = Boolean.Parse(node.Attributes["value"].Value);
			    break;
			case "autodiscoverproperties":
			    this.autoDiscoverProperties = Boolean.Parse(node.Attributes["value"].Value);
			    break;
			case "autodiscoverattributes":
			    this.autoDiscoverAttributes = Boolean.Parse(node.Attributes["value"].Value);
			    break;
			default:
			    Console.Out.WriteLine("Unrecognized configuration option: " + node.Attributes["name"].Value + " = " + node.Attributes["value"].Value);
			    break;
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

	public String ConnectionString {
	    get { 			
		StringBuilder objStringBuilder = new StringBuilder(255);
		objStringBuilder.Append("Data Source = " + server + ";");
		objStringBuilder.Append("Initial Catalog = " + database + ";");
		objStringBuilder.Append("User ID = " + user + ";");
		objStringBuilder.Append("Password = " + password + ";");
		return objStringBuilder.ToString();
	    }
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

	    s = this.Database + ".DataAccess." + table.Replace(" ", "_");
	    s = this.rootNameSpace;
	    if (daoClassDirectory.Length>0) {
		s += "." + daoClassDirectory;
	    }

	    return s;
	}

	public String GetDONameSpace(String table) {
	    String s;

	    if (this.Database != null && table != null) {
		s = this.Database + ".DataAccess." + table.Replace(" ", "_");
	    }
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

    }
}
