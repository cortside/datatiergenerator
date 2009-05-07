using System;
using System.Collections;
using System.Xml;

namespace Spring2.DataTierGenerator.Element {

    public class WriterElement : ElementSkeleton {

	public static readonly String WRITER = "writer";
	private static readonly String CLASS = "class";

	private String className = String.Empty;
	private IList options = new ArrayList();

	public WriterElement() {}

	public WriterElement(XmlNode generatorNode) : base(generatorNode) {
	    if (WRITER.Equals(generatorNode.Name)) {
		Class = GetAttributeValue(generatorNode, CLASS, Class);
		foreach (XmlNode node in GetChildNodes(generatorNode, OptionElement.OPTION)) {
		    this.options.Add(new OptionElement(node));
		}
	    }
	}

	/// <summary>
	/// The name of the writer class.
	/// </summary>
	public String Class {
	    get { return this.className; }
	    set { this.className = value; }
	}

	/// <summary>
	/// The options specified for the writer.
	/// </summary>
	public IList Options {
	    get { return options; }
	    set { options = value; }
	}

	/// <summary>
	/// Get the options specified for the writer in Hashtable format.
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
