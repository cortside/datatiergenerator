using System;
using System.Collections;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class GeneratorElement : ElementSkeleton {

	private static readonly String CLASS = "class";

	private String className = String.Empty;
	private IList tasks = new ArrayList();
	private IList tools = new ArrayList();

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

	/// <summary>
	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
	/// list.
	/// </summary>
	/// <param name="node"></param>
	/// <param name="generatorElements"></param>
	public static void ParseFromXml(XmlNode node, IList generatorElements) {

	    if (node != null && generatorElements != null) {

		foreach (XmlNode generatorNode in node.ChildNodes) {
		    if (generatorNode.NodeType.Equals(XmlNodeType.Element)) {
			GeneratorElement generatorElement = new GeneratorElement();

			generatorElement.Class = GetAttributeValue(generatorNode, CLASS, generatorElement.Class);
			TaskElement.ParseFromXml(generatorNode, generatorElement.Tasks);
			TaskElement.ParseFromXml(generatorNode, generatorElement.Tools);
		
			generatorElements.Add(generatorElement);
		    }
		}
	    }
	}

	public static GeneratorElement ParseFromXml(Configuration options, XmlDocument doc, ParserValidationDelegate vd) {
	    GeneratorElement generator = new GeneratorElement();
	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("generator");
	    if (elements.Count == 0) {
		generator.Class = "Spring2.DataTierGenerator.Generator.CodeGenerator,Spring2.DataTierGenerator";
	    } else {
		generator.Class = elements[0].Attributes["class"].Value;
		generator.Tasks = TaskElement.ParseFromXml(options, elements[0], vd);
		generator.Tools = ToolElement.ParseFromXml(options, elements[0], vd);
	    }
	    return generator;
	}

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
