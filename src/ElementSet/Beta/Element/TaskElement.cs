using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator.Element {

    public class TaskElement : ElementSkeleton {

	public static readonly String TASK = "task";

	private static readonly String INCLUDES = "includes";
	private static readonly String EXCLUDES = "excludes";
	private static readonly String PARAMETERS = "parameters";
	private static readonly String TEMPLATE = "template";
	private static readonly String ELEMENT = "element";
	private static readonly String DIRECTORY = "directory";
	private static readonly String FILENAME_FORMAT = "filenameformat";

	protected String element = String.Empty;
	protected String directory = String.Empty;
	protected String filenameformat = String.Empty;
	private IList includes = new ArrayList();
	private IList excludes = new ArrayList();
	private IList parameters = new ArrayList();

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

	public IList Includes {
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

	public TaskElement() {}

	public TaskElement(XmlNode taskNode) {

	    if (taskNode != null && TASK.Equals(taskNode.Name)) {
		name = GetAttributeValue(taskNode, NAME, name);
		Template = GetAttributeValue(taskNode, TEMPLATE, Template);
		element = GetAttributeValue(taskNode, ELEMENT, element);
		directory = GetAttributeValue(taskNode, DIRECTORY, directory);
		FileNameFormat = GetAttributeValue(taskNode, FILENAME_FORMAT, FileNameFormat);
		foreach (XmlNode node in GetChildNodes(taskNode, INCLUDES, IncludeElement.INCLUDE)) {
		    this.includes.Add(new IncludeElement(node));
		}
		foreach (XmlNode node in GetChildNodes(taskNode, EXCLUDES, ExcludeElement.EXCLUDE)) {
		    this.excludes.Add(new ExcludeElement(node));
		}
		foreach (XmlNode node in GetChildNodes(taskNode, PARAMETERS, ParameterElement.PARAMETER)) {
		    this.parameters.Add(new ParameterElement(node));
		}
	    } else {
		throw new ArgumentException("The XmlNode argument is not a task node.");
	    }
	}
// =======
// 	/// <summary>
// 	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
// 	/// list.
// 	/// </summary>
// 	/// <param name="node"></param>
// 	/// <param name="taskElements"></param>
// 	public static void ParseFromXml(XmlNode node, IList taskElements) {

// 	    if (node != null && taskElements != null) {

// 		XmlNodeList nodes = node.SelectNodes("tasks/task");
// 		foreach (XmlNode taskNode in nodes) {
// 		    TaskElement taskElement = new TaskElement();

// 		    taskElement.Name = GetAttributeValue(taskNode, NAME, taskElement.Name);
// 		    taskElement.Template = GetAttributeValue(taskNode, TEMPLATE, taskElement.Template);
// 		    taskElement.Element = GetAttributeValue(taskNode, ELEMENT, taskElement.Element);
// 		    taskElement.Directory = GetAttributeValue(taskNode, DIRECTORY, taskElement.Directory);
// 		    taskElement.FileNameFormat = GetAttributeValue(taskNode, FILENAME_FORMAT, taskElement.FileNameFormat);
		
// 		    taskElements.Add(taskElement);
// 		}
// >>>>>>> 1.2

	public override void Validate(RootElement root) {
	}

//	public static ArrayList ParseFromXml(ConfigurationElement options, XmlNode root, IParser vd) {
//	    ArrayList list = new ArrayList();
//	    XmlNodeList nodes = root.SelectNodes("tasks/task");
//	    foreach (XmlNode node in nodes) {
//		TaskElement task = new TaskElement();
//		task.Name = node.Attributes["name"].Value;
//		task.Element = node.Attributes["element"].Value;
//		task.Template = node.Attributes["template"].Value;
//		task.Directory = node.Attributes["directory"].Value;
//		task.FileNameFormat = node.Attributes["filenameformat"].Value;
//		if (!node.InnerText.Equals(String.Empty)) {
//		    task.Description = node.InnerText;
//		}
//		task.Includes = IncludeElement.ParseFromXml(options, node, vd);
//		task.Excludes = ExcludeElement.ParseFromXml(options, node, vd);
//		task.Parameters = ParameterElement.ParseFromXml(options, node, vd);
//		list.Add(task);
//	    }
//	    return list;
//	}

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
    }
}
