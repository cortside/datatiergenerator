using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class EnumElement : ElementSkeleton {

	public static readonly String ENUM = "enum";
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

	public EnumElement() {}

	public EnumElement(XmlNode enumNode) : base(enumNode) {

	    if (ENUM.Equals(enumNode.Name)) {
		Template = GetAttributeValue(enumNode, TEMPLATE, Template);
		integerBased = Boolean.Parse(GetAttributeValue(enumNode, INTEGER_BASED, integerBased.ToString()));

		foreach (XmlNode node in GetChildNodes(enumNode, EnumValueElement.VALUE)) {
		    values.Add(new EnumValueElement(node));
		}
	    } else {
		throw new ArgumentException("The XmlNode argument is not an enum node.");
	    }
	}

	public override void Validate(RootElement root) {
	    if (this.IntegerBased) {
		foreach (EnumValueElement enumValue in this.values) {
		    try {
			Int32.Parse(enumValue.Code);
		    } catch {
			root.AddValidationMessage(ParserValidationMessage.NewError(String.Format("IntegerBased was set for enum {0} and code '{1}' was not parsable by Int32.", this.Name, enumValue.Code )));
		    }
		}
	    }
	}

//	public static ArrayList ParseFromXml(ConfigurationElement options, XmlDocument doc, Hashtable sqltypes, Hashtable types, IParser parser) {
//	    ArrayList enums = new ArrayList();
//	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("enum");
//	    foreach (XmlNode node in elements) {
//		EnumElement type = new EnumElement();
//		type.Name = node.Attributes["name"].Value;
//		// TODO: this returns all of the children node's innerText as well
//		//type.Description = StringUtil.RemoveTrailingBlankLines(node.InnerText.Trim());
//		if (node.Attributes["template"] != null) {
//		    type.Template = node.Attributes["template"].Value;
//		}
//		if (node.Attributes["integerbased"] != null) {
//		    type.IntegerBased = Boolean.Parse(node.Attributes["integerbased"].Value);
//		}
//		type.Values = EnumValueElement.ParseFromXml(type.Name, options, doc, sqltypes, types, parser);
//
//		// if IsIntegerBased - validate that all values are parsable by Int32
//		if (type.IntegerBased) {
//		    foreach(EnumValueElement v in type.Values) {
//			try {
//			    Int32.Parse(v.Code);
//			} catch (Exception) {
//			    // parse error - must not be an Int32
//			    parser.AddValidationMessage(ParserValidationMessage.NewError("IntegerBased was set for enum " + type.Name + " and code '" + v.Code + "' was not parsable by Int32."));
//			}
//		    }
//		}
//
//		enums.Add(type);
//	    }
//	    return enums;
//	}

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
