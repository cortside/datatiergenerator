using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.Core.Util;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class EnumElement : ElementSkeleton {

	private static readonly String INTEGER_BASED = "integerbased";
	private static readonly String TEMPLATE = "template";
			    
	protected Boolean integerBased = false;
	protected IList values = new ArrayList();

	public Boolean IntegerBased {
	    get { return this.integerBased; }
	    set { this.integerBased = value; }
	}

	public IList Values{
	    get { return this.values; }
	    set { this.values = value; }
	}

	/// <summary>
	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
	/// list.
	/// </summary>
	/// <param name="node"></param>
	/// <param name="enumElements"></param>
	public static void ParseFromXml(XmlNode node, IList enumElements) {

	    if (node != null && enumElements != null) {

		foreach (XmlNode enumNode in node.ChildNodes) {
		    if (enumNode.NodeType.Equals(XmlNodeType.Element)) {
			EnumElement enumElement = new EnumElement();

			enumElement.Name = GetAttributeValue(enumNode, NAME, enumElement.Name);
			enumElement.Template = GetAttributeValue(enumNode, TEMPLATE, enumElement.Template);
			enumElement.IntegerBased = Boolean.Parse(GetAttributeValue(enumNode, INTEGER_BASED, enumElement.IntegerBased.ToString()));

			EnumValueElement.ParseFromXml(enumNode, enumElement.Values);
		
			enumElements.Add(enumElement);
		    }
		}
	    }
	}

	public static ArrayList ParseFromXml(Configuration options, XmlDocument doc, Hashtable sqltypes, Hashtable types, ParserValidationDelegate vd) {
	    ArrayList enums = new ArrayList();
	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("enum");
	    foreach (XmlNode node in elements) {
		if (node.NodeType == XmlNodeType.Comment)
		{
		    continue;
		}
		EnumElement type = new EnumElement();
		type.Name = node.Attributes["name"].Value;
		// TODO: this returns all of the children node's innerText as well
		//type.Description = StringUtil.RemoveTrailingBlankLines(node.InnerText.Trim());
		if (node.Attributes["template"] != null) {
		    type.Template = node.Attributes["template"].Value;
		}
		if (node.Attributes["integerbased"] != null) {
		    type.IntegerBased = Boolean.Parse(node.Attributes["integerbased"].Value);
		}
		type.Values = EnumValueElement.ParseFromXml(type.Name, options, doc, sqltypes, types, vd);

		// if IsIntegerBased - validate that all values are parsable by Int32
		if (type.IntegerBased) {
		    foreach(EnumValueElement v in type.Values) {
			try {
			    Int32.Parse(v.Code);
			} catch (Exception) {
			    // parse error - must not be an Int32
			    vd(ParserValidationArgs.NewError("IntegerBased was set for enum " + type.Name + " and code '" + v.Code + "' was not parsable by Int32."));
			}
		    }
		}

		enums.Add(type);
	    }
	    return enums;
	}

	public static EnumElement FindByName(ArrayList list, String name) {
	    foreach (EnumElement item in list) {
		if (item.Name.Equals(name)) {
		    return item;
		}
	    }
	    return null;
	}

    }
}
