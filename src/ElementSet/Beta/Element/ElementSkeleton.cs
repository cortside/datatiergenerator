using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator.Element {

    public abstract class ElementSkeleton {

	protected static readonly String NAME = "name";

	protected String name = String.Empty;
	private String description = String.Empty;
	private String template = String.Empty;

	public ElementSkeleton() {}

	public ElementSkeleton(XmlNode node) {
	    if (node == null) {
		throw new ArgumentException("The XmlNode argument cannot be null.");
	    } else {
		name = GetAttributeValue(node, NAME, name);
		description = node.InnerText.Trim();
	    }
	}

	public String Name {
	    get { return this.name; }
	    set { this.name = value; }
	}

	public String Description {
	    get { return this.description; }
	    set { this.description = value; }
	}

	public String Template {
	    get { return this.template; }
	    set { this.template = value; }
	}

	public Object Clone() {
	    return MemberwiseClone();
	}

	public virtual void ToXml(StringBuilder buffer, Int32 indentLevel) {
	}

	public abstract void Validate(RootElement root);

//	protected virtual void AddValidationMessage(ParserValidationMessage message) {
//	    parentElement.AddValidationMessage(message);
//	}

	/// <summary>
	/// Returns the attribute value as a String if found, or the defaultValue if not.
	/// </summary>
	/// <param name="node"></param>
	/// <param name="attribute"></param>
	/// <param name="defaultValue"></param>
	/// <returns></returns>
	protected static String ParseStringAttribute(XmlNode node, String attribute, String defaultValue) {
	    String s = defaultValue;
	    if (node.Attributes[attribute] != null) {
		s = node.Attributes[attribute].Value;
	    }
	    return s;
	}

	public static String GetAttributeValue(XmlNode node, String attributeName, String defaultValue) {
	    XmlAttribute attribute = node.Attributes[attributeName];
	    return attribute == null ? defaultValue : attribute.Value;
	}

	public static XmlNode GetChildNodeByName(XmlNode parentNode, String childNodeName) {
	    foreach (XmlNode childNode in parentNode.ChildNodes) {
		if (childNode.Name.Equals(childNodeName)) {
		    return childNode;
		}
	    }
	    return null;
	}

	public static ArrayList GetChildNodes(XmlNode node, String name, String childNodeName) {
	    ArrayList childNodes = new ArrayList();
	    XmlNode childNode = GetChildNodeByName(node, name);
	    if (childNode != null) {
		foreach (XmlNode grandchildNode in childNode.ChildNodes) {
		    if (grandchildNode.NodeType.Equals(XmlNodeType.Element) && grandchildNode.Name.Equals(childNodeName)) {
			childNodes.Add(grandchildNode);
		    }
		}
	    }

	    return childNodes;
	}

	public static ArrayList GetChildNodes(XmlNode node, String name) {
	    ArrayList childNodes = new ArrayList();
	    foreach (XmlNode childNode in node.ChildNodes) {
		if (childNode.NodeType.Equals(XmlNodeType.Element) && childNode.Name.Equals(name)) {
		    childNodes.Add(childNode);
		}
	    }

	    return childNodes;
	}

	protected String Indent(Int32 level) {
	    String indent = String.Empty;
	    while (level > 0) {
		indent = indent + "  ";
		level--;
	    }
	    return indent;
	}

	protected String OpenTag(String value, Int32 indentLevel) {
	    return Indent(indentLevel) + String.Format("<{0}>", value) + Environment.NewLine;
	}

	protected String CloseTag(String value, Int32 indentLevel) {
	    return Indent(indentLevel) + String.Format("</{0}>", value) + Environment.NewLine;
	}
    }
}
