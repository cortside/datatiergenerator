using System;
using System.Collections;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class ParserElement : ElementSkeleton {

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

	/// <summary>
	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
	/// list.
	/// </summary>
	/// <param name="node"></param>
	/// <param name="entityElements"></param>
	public static void ParseFromXml(XmlNode node, IList parserElements) {

	    if (node != null && parserElements != null) {

		foreach (XmlNode parserNode in node.ChildNodes) {
		    if (parserNode.NodeType.Equals(XmlNodeType.Element)) {
			ParserElement parserElement = new ParserElement();

			parserElement.Class = GetAttributeValue(parserNode, CLASS, parserElement.Class);
			ArgumentElement.ParseFromXml(parserNode, parserElement.Arguments);

			parserElements.Add(parserElement);
		    }
		}
	    }
	}

	public static ParserElement ParseFromXml(Configuration options, XmlDocument doc, ParserValidationDelegate vd) {
	    ParserElement parser = new ParserElement();
	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("parser");
	    if (elements.Count == 0) {
		parser.Class = String.Empty;
	    } else {
		parser.Class = elements[0].Attributes["class"].Value;
		parser.Arguments = ArgumentElement.ParseFromXml(options, elements[0], vd);
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
