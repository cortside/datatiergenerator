using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator.Element {

    public class ParameterElement : ElementSkeleton {

	public static String PARAMETER = "parameter";
	private static readonly String VALUE = "value";

	private String value = String.Empty;

	public ParameterElement() {}

	public ParameterElement(XmlNode parameterNode) : base(parameterNode) {
	    if (PARAMETER.Equals(parameterNode.Name)) {
		value = GetAttributeValue(parameterNode, VALUE, value);
	    } else {
		throw new ArgumentException("The XmlNode argument is not a parameter node.");
	    }
	}

	public String Value {
	    get { return this.value; }
	    set { this.value = value; }
	}

//	/// <summary>
//	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
//	/// list.
//	/// </summary>
//	/// <param name="node"></param>
//	/// <param name="taskElements"></param>
//	public static void ParseFromXml(XmlNode root, IList elements) {
//	    if (root != null && elements != null) {
//		XmlNodeList nodes = root.SelectNodes("parameters/parameter");
//		foreach (XmlNode node in nodes) {
//		    ParameterElement element = new ParameterElement();
//		    element.Name = GetAttributeValue(node, NAME, element.Name);
//		    element.Value = GetAttributeValue(node, VALUE, element.Value);
//		    elements.Add(element);
//		}
//	    }
//	}

	public override void Validate(RootElement root) {
	    // No validation necessary.
	}
    }
}
