using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class ToolElement : ElementSkeleton {

	private static readonly String CLASS = "class";

	private String clazz = String.Empty;

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
	public static void ParseFromXml(XmlNode root, IList elements) {
	    if (root != null && elements != null) {
		XmlNodeList nodes = root.SelectNodes("tools/tool");
		foreach (XmlNode node in nodes) {
		    if (node.NodeType == XmlNodeType.Comment)
		    {
			continue;
		    }
		    ToolElement element = new ToolElement();
		    element.Name = GetAttributeValue(node, NAME, element.Name);
		    element.Class = GetAttributeValue(node, CLASS, element.Class);
		    elements.Add(element);
		}
	    }
	}

	public static ArrayList ParseFromXml(Configuration options, XmlNode root, ParserValidationDelegate vd) {
	    ArrayList elements = new ArrayList();
	    XmlNodeList nodes = root.SelectNodes("tools/tool");
	    foreach (XmlNode node in nodes) {
		if (node.NodeType == XmlNodeType.Comment)
		{
		    continue;
		}
		ToolElement element = new ToolElement();
		element.Name = GetAttributeValue(node, NAME, element.Name);
		element.Class = GetAttributeValue(node, CLASS, element.Class);
		elements.Add(element);
	    }
	    return elements;
	}

    }
}
