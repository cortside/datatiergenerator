using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator.Element {

    public class CollectionElement : ElementSkeleton {

	public static readonly String COLLECTION = "collection";
	private static readonly String TYPE = "type";
	private static readonly String TEMPLATE = "template";

	protected String type = String.Empty;

	public String Type {
	    get { return this.type; }
	    set { this.type = value; }
	}

	public CollectionElement() {}

        public CollectionElement(XmlNode collectionNode) : base(collectionNode) {

	    if (COLLECTION.Equals(collectionNode.Name)) {
		type = GetAttributeValue(collectionNode, TYPE, type);
		Template = GetAttributeValue(collectionNode, TEMPLATE, Template);
	    } else {
		throw new ArgumentException("The XmlNode argument is not a collection node.");
	    }
	}

	public override void Validate(RootElement root) {
	}

//	public static ArrayList ParseFromXml(ConfigurationElement options, XmlDocument doc, Hashtable sqltypes, Hashtable types, IParser vd) {
//	    ArrayList list = new ArrayList();
//	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("collection");
//	    foreach (XmlNode node in elements) {
//		CollectionElement collection = new CollectionElement();
//		collection.Name = node.Attributes["name"].Value;
//		collection.Type = node.Attributes["type"].Value;
//		if (node.Attributes["template"] != null) {
//		    collection.Template = node.Attributes["template"].Value;
//		}
//		collection.Description = node.InnerText;	
//		list.Add(collection);
//	    }
//	    return list;
//	}

	public static CollectionElement FindByName(ArrayList list, String name) {
	    foreach (CollectionElement item in list) {
		if (item.Name.Equals(name)) {
		    return item;
		}
	    }
	    return null;
	}
    }
}
