using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class ArgumentElement : ElementSkeleton {

	private static readonly String VALUE = "value";

	protected String value = String.Empty;

	public String Value {
	    get { return this.value; }
	    set { this.value = value; }
	}

	/// <summary>
	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
	/// list.
	/// </summary>
	/// <param name="node"></param>
	/// <param name="entityElements"></param>
	public static void ParseFromXml(XmlNode node, IList argumentElements) {

	    if (node != null && argumentElements != null) {

		foreach (XmlNode argumentNode in node.ChildNodes) {
		    if (argumentNode.NodeType.Equals(XmlNodeType.Element)) {
			ArgumentElement argumentElement = new ArgumentElement();

			argumentElement.Name = GetAttributeValue(argumentNode, NAME, argumentElement.Name);
			argumentElement.Value = GetAttributeValue(argumentNode, VALUE, argumentElement.Value);
		
			argumentElements.Add(argumentElement);
		    }
		}
	    }
	}

	public static ArrayList ParseFromXml(Configuration options, XmlNode root, ParserValidationDelegate vd) {
	    ArrayList list = new ArrayList();
	    foreach (XmlNode node in root.ChildNodes) {
		if (node.Name.Equals("argument")) {
		    ArgumentElement arg = new ArgumentElement();
		    arg.Name = node.Attributes["name"].Value;
		    arg.Value = node.Attributes["value"].Value;
		    if (!node.InnerText.Equals(String.Empty)) {
			arg.Description = node.InnerText;
		    }
		    list.Add(arg);
		}
	    }
	    return list;
	}

    }
}
