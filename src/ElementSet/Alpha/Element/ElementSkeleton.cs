using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Generator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class ElementSkeleton : Spring2.Core.DataObject.DataObject, ICloneable, IElement {

	protected static readonly String NAME = "name";

	protected String name = String.Empty;
	protected String description = String.Empty;
	protected String template = String.Empty;

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
	    if (node == null || attributeName == null || node.Attributes == null || node.Attributes[attributeName]== null) {
		return defaultValue;
	    }
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
    }
}
