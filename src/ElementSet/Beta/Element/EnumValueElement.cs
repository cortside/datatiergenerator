using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator.Element {

    public class EnumValueElement : ElementSkeleton { 

	public static readonly String VALUE = "value";
	private static readonly String CODE = "code";

	protected String code = String.Empty;
	protected String description = String.Empty;

	public EnumValueElement() {}

	public EnumValueElement(XmlNode enumValueNode) : base(enumValueNode) {

	    if (VALUE.Equals(enumValueNode.Name)) {
		code = GetAttributeValue(enumValueNode, CODE, code);
	    } else {
		throw new ArgumentException("The XmlNode argument is not an enum value node.");
	    }
	}

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

	public override void Validate(RootElement root) {}

//	public static ArrayList ParseFromXml(String name, ConfigurationElement options, XmlDocument doc, Hashtable sqltypes, Hashtable types, IParser vd) {
//	    ArrayList values = new ArrayList();
//	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("enum");
//	    foreach (XmlNode element in elements) {
//		if (element.Attributes["name"].Value.Equals(name) && element.HasChildNodes) {
//		    foreach (XmlNode node in element.ChildNodes) {
//			if (node.Name.Equals("value")) {
//			    EnumValueElement value = new EnumValueElement();
//			    value.Name = node.Attributes["name"].Value;
//			    value.Code = node.Attributes["code"].Value;
//			    if (!node.InnerText.Equals(String.Empty)) {
//				value.Description = node.InnerText;
//			    }
//			    values.Add(value);
//			}
//		    }
//		}
//	    }
//	    return values;
//	}
    }
}
