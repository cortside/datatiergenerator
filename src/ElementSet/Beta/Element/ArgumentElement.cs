using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator.Element {

    public class ArgumentElement : ElementSkeleton {

	public static readonly String ARGUMENT = "argument";
	private static readonly String VALUE = "value";

	protected String value = String.Empty;

	public String Value {
	    get { return this.value; }
	    set { this.value = value; }
	}

	public override void Validate(RootElement root) {}

	public ArgumentElement() {}

	public ArgumentElement(XmlNode argumentNode) : base(argumentNode) {

	    if (ARGUMENT.Equals(argumentNode.Name)) {
		value = GetAttributeValue(argumentNode, VALUE, value);
	    } else {
		throw new ArgumentException("The XmlNode argument is not an argument node.");
	    }
	}

	public static ArrayList ParseFromXml(ConfigurationElement options, XmlNode root) {
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
