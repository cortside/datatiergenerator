using System;
using System.Collections;
using System.Xml;

namespace Spring2.DataTierGenerator.Element {

    public class StylerElement : ElementSkeleton {

	public static readonly String STYLER = "styler";
	private static readonly String CLASS = "class";

	private String className = String.Empty;
	private IList options = new ArrayList();

	public StylerElement() {}

	public StylerElement(XmlNode generatorNode) : base(generatorNode) {
	    if (STYLER.Equals(generatorNode.Name)) {
		Class = GetAttributeValue(generatorNode, CLASS, Class);
		foreach (XmlNode node in GetChildNodes(generatorNode, OptionElement.OPTION)) {
		    this.options.Add(new OptionElement(node));
		}
	    }
	}

	public String Class {
	    get { return this.className; }
	    set { this.className = value; }
	}

	public IList Options {
	    get { return options; }
	    set { options = value; }
	}

	/// <summary>
	/// Get the options specified for the styler in Hashtable format.
	/// </summary>
	/// <returns></returns>
	public Hashtable GetOptions() {
	    Hashtable o = new Hashtable();
	    foreach (OptionElement option in Options) {
		o.Add(option.Name, option.Value);
	    }
	    return o;
	}

    }
}
