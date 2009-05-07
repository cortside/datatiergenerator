using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class OptionElement : ElementSkeleton {

	public static readonly String OPTION = "option";
	private static readonly String VALUE = "value";

	private String value = String.Empty;

	public OptionElement(XmlNode node) : base(node) {
	    if (OPTION.Equals(node.Name)) {
		Value = GetAttributeValue(node, VALUE, Value);
	    }
	}

	public String Value {
	    get { return this.value; }
	    set { this.value = value; }
	}

    }
}
