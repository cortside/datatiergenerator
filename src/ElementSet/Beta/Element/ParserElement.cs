using System;
using System.Collections;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Generator;

namespace Spring2.DataTierGenerator.Element {

    public class ParserElement : ElementSkeleton {

	public static readonly String PARSER = "parser";
	private static readonly String CLASS = "class";

	private String className = String.Empty;
	private IList arguments = new ArrayList();

	public String Class {
	    get { return this.className; }
	    set { this.className = value; }
	}

	public IList Arguments {
	    get { return arguments; }
	    set { arguments = value; }
	}

	public ParserElement() {}

	public ParserElement(XmlNode parserNode) {

	    if (parserNode != null && PARSER.Equals(parserNode.Name)) {
		Class = GetAttributeValue(parserNode, CLASS, Class);
		foreach (XmlNode node in GetChildNodes(parserNode, ArgumentElement.ARGUMENT)) {
		    arguments.Add(new ArgumentElement(node));
		}
	    } else {
		throw new ArgumentException("The XmlNode argument is not a parser node.");
	    }
	}

	public override void Validate(IParser parser) {
	}

	public static ParserElement ParseFromXml(ConfigurationElement options, XmlDocument doc) {
	    ParserElement parser = new ParserElement();
	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("parser");
	    if (elements.Count == 0) {
		parser.Class = "Spring2.DataTierGenerator.Parser.XmlParser";
	    } else {
		parser.Class = elements[0].Attributes["class"].Value;
		parser.Arguments = ArgumentElement.ParseFromXml(options, elements[0]);
	    }
	    return parser;
	}

	public ArgumentElement FindArgumentByName(String name) {
	    foreach (ArgumentElement arg in arguments) {
		if (arg.Name.ToLower().Equals(name.ToLower())) {
		    return arg;
		}
	    }
	    return null;
	}
    }
}
