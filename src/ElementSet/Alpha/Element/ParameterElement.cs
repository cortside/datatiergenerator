using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class ParameterElement : ElementSkeleton {

	private static readonly String VALUE = "value";

	private String value = String.Empty;

	public String Value {
	    get { return this.value; }
	    set { this.value = value; }
	}

	/// <summary>
	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
	/// list.
	/// </summary>
	/// <param name="node"></param>
	/// <param name="taskElements"></param>
	public static void ParseFromXml(XmlNode root, IList elements) {
	    if (root != null && elements != null) {
		XmlNodeList nodes = root.SelectNodes("parameters/parameter");
		foreach (XmlNode node in nodes) {
		    if (node.NodeType == XmlNodeType.Comment)
		    {
			continue;
		    }
		    ParameterElement element = new ParameterElement();
		    element.Name = GetAttributeValue(node, NAME, element.Name);
		    element.Value = GetAttributeValue(node, VALUE, element.Value);
		    elements.Add(element);
		}
	    }
	}

	public static ArrayList ParseFromXml(Configuration options, XmlNode root, ParserValidationDelegate vd) {
	    ArrayList elements = new ArrayList();
	    XmlNodeList nodes = root.SelectNodes("parameters/parameter");
	    foreach (XmlNode node in nodes) {
		if (node.NodeType == XmlNodeType.Comment)
		{
		    continue;
		}
		ParameterElement element = new ParameterElement();
		element.Name = GetAttributeValue(node, NAME, element.Name);
		element.Value = GetAttributeValue(node, VALUE, element.Value);
		elements.Add(element);
	    }
	    return elements;
	}

    }
}
