using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class ExcludeElement : ElementSkeleton {

	/// <summary>
	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
	/// list.
	/// </summary>
	/// <param name="node"></param>
	/// <param name="taskElements"></param>
	public static void ParseFromXml(XmlNode node, IList elements) {
	    if (node != null && elements != null) {
		XmlNodeList excludes = node.SelectNodes("excludes/exclude");
		foreach (XmlNode exclude in excludes) {
		    if (exclude.NodeType == XmlNodeType.Comment)
		    {
			continue;
		    }
		    ExcludeElement element = new ExcludeElement();
		    element.Name = GetAttributeValue(exclude, NAME, element.Name);
		    elements.Add(element);
		}
	    }
	}

	public static ArrayList ParseFromXml(Configuration options, XmlNode node, ParserValidationDelegate vd) {
	    ArrayList elements = new ArrayList();
	    XmlNodeList excludes = node.SelectNodes("excludes/exclude");
	    foreach (XmlNode exclude in excludes) {
		if (exclude.NodeType == XmlNodeType.Comment)
		{
		    continue;
		}
		ExcludeElement element = new ExcludeElement();
		element.Name = GetAttributeValue(exclude, NAME, element.Name);
		elements.Add(element);
	    }
	    return elements;
	}

    }
}
