using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class ToolElement : ElementSkeleton {

	public static readonly String TOOL = "tool";
	private static readonly String CLASS = "class";

	private String clazz = String.Empty;

	public ToolElement() {}

	public ToolElement(XmlNode toolNode) : base(toolNode) {
	    if (TOOL.Equals(toolNode.Name)) {
		name = GetAttributeValue(toolNode, Name, name);
		clazz = GetAttributeValue(toolNode, CLASS, clazz);
	    }
	}

	public String Class {
	    get { return this.clazz; }
	    set { this.clazz = value; }
	}

	/// <summary>
	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
	/// list.
	/// </summary>
	/// <param name="node"></param>
	/// <param name="taskElements"></param>
	public static void ParseFromXml(XmlNode root, IList elements, IParser vd) {
	    if (root != null && elements != null) {
		XmlNodeList nodes = root.SelectNodes("tools/tool");
		foreach (XmlNode node in nodes) {
		    ToolElement element = new ToolElement();
		    element.Name = GetAttributeValue(node, NAME, element.Name);
		    element.Class = GetAttributeValue(node, CLASS, element.Class);
		    elements.Add(element);
		}
	    }
	}

	public override void Validate(IParser parser) {
	}
    }
}
