using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class EnumValueElement : ElementSkeleton { 

	private static readonly String CODE = "code";

	protected String code = String.Empty;
	protected String description = String.Empty;

	public String Code {
	    get { return this.code; }
	    set { this.code = value; }
	}

	public String ToXml() {
	    StringBuilder sb = new StringBuilder();
	    sb.Append("<value name=\"").Append(name).Append("\"");
	    sb.Append(" code=\"").Append(code).Append("\"");
	    if (description.Equals(String.Empty)) {
		sb.Append(" />");
	    } else {
		sb.Append(">");
		sb.Append(description);
		sb.Append("</value>");
	    }

	    return sb.ToString();
	}

	/// <summary>
	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
	/// list.
	/// </summary>
	/// <param name="node"></param>
	/// <param name="enumValueElements"></param>
	public static void ParseFromXml(XmlNode node, IList enumValueElements) {

	    if (node != null && enumValueElements != null) {

		foreach (XmlNode enumNode in node.ChildNodes) {
		    if (enumNode.NodeType.Equals(XmlNodeType.Element)) {
			EnumValueElement enumValueElement = new EnumValueElement();

			enumValueElement.Name = GetAttributeValue(enumNode, NAME, enumValueElement.Name);
			enumValueElement.Code = GetAttributeValue(enumNode, CODE, enumValueElement.Code);
		
			enumValueElements.Add(enumValueElement);
		    }
		}
	    }
	}

	public static ArrayList ParseFromXml(String name, Configuration options, XmlDocument doc, Hashtable sqltypes, Hashtable types, ParserValidationDelegate vd) {
	    ArrayList values = new ArrayList();
	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("enum");
	    foreach (XmlNode element in elements) {
		if (element.Attributes["name"].Value.Equals(name) && element.HasChildNodes) {
		    foreach (XmlNode node in element.ChildNodes) {
			if (node.Name.Equals("value")) {
			    EnumValueElement value = new EnumValueElement();
			    value.Name = node.Attributes["name"].Value;
			    value.Code = node.Attributes["code"].Value;
			    if (!node.InnerText.Equals(String.Empty)) {
				value.Description = node.InnerText;
			    }
			    values.Add(value);
			}
		    }
		}
	    }
	    return values;
	}

    }
}
