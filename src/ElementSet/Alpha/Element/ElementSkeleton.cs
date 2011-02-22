using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Generator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class ElementSkeleton : Spring2.Core.DataObject.DataObject, ICloneable, IElement {

	protected static readonly String NAME = "name";

	protected String name = String.Empty;
	protected String namespace_ = String.Empty;
	protected String description = String.Empty;
	protected String template = String.Empty;
        protected Hashtable attributes = new Hashtable();

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

	public String Namespace {
	    get { return this.namespace_; }
	    set { this.namespace_ = value; }
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

        public Hashtable Attributes {
            get { return this.attributes; }
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

    }
}
