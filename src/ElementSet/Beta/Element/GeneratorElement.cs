using System;
using System.Collections;
using System.Xml;

namespace Spring2.DataTierGenerator.Element {

    public class GeneratorElement : ElementSkeleton {

	public static readonly String GENERATOR = "generator";
	private static readonly String TOOLS = "tools";
	private static readonly String TASKS = "tasks";
	private static readonly String CLASS = "class";

	private String className = String.Empty;
	private IList tasks = new ArrayList();
	private IList tools = new ArrayList();

	public GeneratorElement() {}

	public GeneratorElement(XmlNode generatorNode) : base(generatorNode) {

	    if (GENERATOR.Equals(generatorNode.Name)) {
		Class = GetAttributeValue(generatorNode, CLASS, Class);
		foreach (XmlNode node in GetChildNodes(generatorNode, TASKS, TaskElement.TASK)) {
		    this.tasks.Add(new TaskElement(node));
		}
		foreach (XmlNode node in GetChildNodes(generatorNode, TOOLS, ToolElement.TOOL)) {
		    this.tools.Add(new ToolElement(node));
		}
	    }
	}

	public String Class {
	    get { return this.className; }
	    set { this.className = value; }
	}

	public IList Tasks {
	    get { return tasks; }
	    set { tasks = value; }
	}

	public IList Tools {
	    get { return tools; }
	    set { tools = value; }
	}

	public override void Validate(RootElement root) {}

	public IList FindTasksByElement(String element) {
	    IList list = new ArrayList();
	    foreach (TaskElement task in tasks) {
		if (task.Element.ToLower().Equals(element.ToLower())) {
		    list.Add(task);
		}
	    }
	    return list;
	}
    }
}
