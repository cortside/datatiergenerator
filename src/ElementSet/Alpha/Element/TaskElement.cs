using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class TaskElement : ElementSkeleton {

	private static readonly String TEMPLATE = "template";
	private static readonly String ELEMENT = "element";
	private static readonly String DIRECTORY = "directory";
	private static readonly String FILENAME_FORMAT = "filenameformat";
	private static readonly String WRITER = "writer";
	private static readonly String STYLER = "styler";
	private static readonly String BACKUP_DIRECTORY = "backupdirectory";

	protected String element = String.Empty;
	protected String directory = String.Empty;
	protected String filenameformat = String.Empty;
	protected String backupDirectory = String.Empty;
	private IList includes = new ArrayList();
	private IList excludes = new ArrayList();
	private IList parameters = new ArrayList();
	private String writer = String.Empty;
	private String styler = String.Empty;
	private IList types = new ArrayList();

	public String Element {
	    get { return this.element; }
	    set { this.element = value; }
	}

	public String FileNameFormat {
	    get { return this.filenameformat; }
	    set { this.filenameformat = value; }
	}

	public String Directory {
	    get { return this.directory; }
	    set { this.directory = value; }
	}

	public String BackupDirectory 
	{
	    get { return this.backupDirectory; }
	    set { this.backupDirectory = value; }
	}

	public IList Includes 
	{
	    get { return includes; }
	    set { includes = value; }
	}

	public IList Excludes {
	    get { return excludes; }
	    set { excludes = value; }
	}

	public IList Parameters {
	    get { return parameters; }
	    set { parameters = value; }
	}

	public String Writer {
	    get { return this.writer; }
	    set { this.writer = value; }
	}

	public String Styler {
	    get { return this.styler; }
	    set { this.styler = value; }
	}

	public IList Types {
	    get { return this.types; }
	    set { this.types = value; }
	}

	/// <summary>
	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
	/// list.
	/// </summary>
	/// <param name="node"></param>
	/// <param name="taskElements"></param>
	public static void ParseFromXml(XmlNode node, IList taskElements) {
	    if (node != null && taskElements != null) {

		XmlNodeList nodes = node.SelectNodes("tasks/task");
		foreach (XmlNode taskNode in nodes) {
		    if (node.NodeType == XmlNodeType.Comment) {
			continue;
		    }
		    TaskElement taskElement = new TaskElement();

		    taskElement.Name = GetAttributeValue(taskNode, NAME, taskElement.Name);
		    taskElement.Template = GetAttributeValue(taskNode, TEMPLATE, taskElement.Template);
		    taskElement.Element = GetAttributeValue(taskNode, ELEMENT, taskElement.Element);
		    taskElement.Directory = GetAttributeValue(taskNode, DIRECTORY, taskElement.Directory);
		    taskElement.FileNameFormat = GetAttributeValue(taskNode, FILENAME_FORMAT, taskElement.FileNameFormat);
		    taskElement.Writer = GetAttributeValue(taskNode, WRITER, taskElement.Writer);
		    taskElement.Styler = GetAttributeValue(taskNode, STYLER, taskElement.Styler);
		    taskElement.BackupDirectory = GetAttributeValue(taskNode, BACKUP_DIRECTORY, taskElement.BackupDirectory);

		    taskElements.Add(taskElement);
		}
	    }
	}

	public static ArrayList ParseFromXml(Configuration options, XmlNode root, ParserValidationDelegate vd) {
	    ArrayList list = new ArrayList();
	    XmlNodeList nodes = root.SelectNodes("tasks/task");
	    foreach (XmlNode node in nodes) {
		if (node.NodeType == XmlNodeType.Comment) {
		    continue;
		}
		TaskElement task = new TaskElement();
		task.Name = node.Attributes["name"].Value;
		task.Element = node.Attributes["element"].Value;
		task.Template = node.Attributes["template"].Value;
		task.Directory = node.Attributes["directory"].Value;
		task.FileNameFormat = node.Attributes["filenameformat"].Value;
		if (node.Attributes[BACKUP_DIRECTORY] != null) {
		    task.BackupDirectory = node.Attributes["backupdirectory"].Value;
		}
		else {
		    task.BackupDirectory = task.Directory;
		}
		if (!node.InnerText.Equals(String.Empty)) {
		    task.Description = node.InnerText;
		}
		task.Includes = IncludeElement.ParseFromXml(options, node, vd);
		task.Excludes = ExcludeElement.ParseFromXml(options, node, vd);
		task.Parameters = ParameterElement.ParseFromXml(options, node, vd);
		task.Writer = GetAttributeValue(node, WRITER, task.Writer);
		task.Styler = GetAttributeValue(node, STYLER, task.Styler);
		TypeElement.ParseFromXml(node, task.Types);

		list.Add(task);
	    }
	    return list;
	}

	public Boolean IsIncluded(String name) {
	    // if nothing was explicitly defined, they are all included
	    if (includes.Count==0 && excludes.Count==0) {
		return true;
	    }

	    // see if is explicitly excluded
	    foreach (ExcludeElement exclude in excludes) {
		if (exclude.Name.ToLower().Equals(name.ToLower())) {
		    return false;
		}
	    }

	    // see if it is explicitly included
	    foreach (IncludeElement include in includes) {
		if (include.Name.ToLower().Equals(name.ToLower())) {
		    return true;
		}
	    }

	    // if there are no excludes and not found in includes, not included
	    // if there are not includes and not found in excludes, included
	    // if there are both includes and excludes and not found, then not included
	    if (excludes.Count==0) {
		return false;
	    } else if (includes.Count==0) {
		return true;
	    } else {
		return false;
	    }
	}

	public static void RegisterTypes(XmlNode root, Configuration options, IList tasks, Hashtable types) {
	    foreach(TaskElement task in tasks) {
		String xpath = String.Empty;
		if (task.Element.Equals("entity")) {
		    xpath = "DataTierGenerator/entities/entity";
		} else if (task.Element.Equals("enum")) {
		    xpath = "DataTierGenerator/enums/enum";
		} else if (task.Element.Equals("collection")) {
		    xpath = "DataTierGenerator/collections/collection";
		}
		if (xpath.Length > 0) {
		    foreach (XmlNode node in root.SelectNodes(xpath)) {
			foreach (TypeElement type in task.Types) {
			    TypeElement t = new TypeElement();
			    t.ConcreteType = type.ConcreteType.Replace("{element.Name}", node.Attributes["name"].Value);
			    t.ConvertForCompare = type.ConvertForCompare.Replace("{element.Name}", node.Attributes["name"].Value);
			    t.ConvertFromSqlTypeFormat = type.ConvertFromSqlTypeFormat.Replace("{element.Name}", node.Attributes["name"].Value);
			    t.ConvertToSqlTypeFormat = type.ConvertToSqlTypeFormat.Replace("{element.Name}", node.Attributes["name"].Value);
			    t.Name = type.Name.Replace("{element.Name}", node.Attributes["name"].Value);
			    t.NewInstanceFormat = type.NewInstanceFormat.Replace("{element.Name}", node.Attributes["name"].Value);
			    t.NullInstanceFormat = type.NullInstanceFormat.Replace("{element.Name}", node.Attributes["name"].Value);
			    if (type.Package.Equals(""))
			    {
				t.Package = options.RootNameSpace + "." + task.Directory.Replace("\\", ".");
			    }
			    else
			    {
				t.Package = type.Package;
			    }

			    if (!types.Contains(t.Name)) {{}
				types.Add(t.Name, t);
			    } else {
				throw new Exception(t.Name + " already registered as type :: \n" + node.OuterXml );
			    }
			}
		    }
		}
	    }
	}

    }
}
