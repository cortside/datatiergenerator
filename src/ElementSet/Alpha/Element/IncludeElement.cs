using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class IncludeElement : ElementSkeleton {

	/// <summary>
	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
	/// list.
	/// </summary>
	/// <param name="node"></param>
	/// <param name="taskElements"></param>
	public static void ParseFromXml(XmlNode node, IList elements) {
	    if (node != null && elements != null) {
		XmlNodeList includes = node.SelectNodes("includes/include");
		foreach (XmlNode include in includes) {
		    IncludeElement element = new IncludeElement();
		    element.Name = GetAttributeValue(include, NAME, element.Name);
		    elements.Add(element);
		}
	    }
	}

	public static ArrayList ParseFromXml(Configuration options, XmlNode node, ParserValidationDelegate vd) {
	    ArrayList elements = new ArrayList();
	    XmlNodeList includes = node.SelectNodes("includes/include");
	    foreach (XmlNode include in includes) {
		IncludeElement element = new IncludeElement();
		element.Name = GetAttributeValue(include, NAME, element.Name);
		elements.Add(element);
	    }
	    return elements;
	}

    }
}
