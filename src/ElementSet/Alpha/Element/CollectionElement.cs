using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class CollectionElement : ElementSkeleton {

	private static readonly String TYPE = "type";
	private static readonly String TEMPLATE = "template";

	protected String type = String.Empty;
	protected EntityElement entity = new EntityElement();

	public String Type {
	    get { return this.type; }
	    set { this.type = value; }
	}

	public EntityElement Entity {
	    get { return this.entity; }
	    set { this.entity = value; }
	}

	/// <summary>
	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
	/// list.
	/// </summary>
	/// <param name="node"></param>
	/// <param name="entityElements"></param>
	public static void ParseFromXml(XmlNode node, IList collectionElements) {
	    if (node != null && collectionElements != null) {

		foreach (XmlNode collectionNode in node.ChildNodes) {
		    if (collectionNode.NodeType.Equals(XmlNodeType.Element)) {
			CollectionElement collectionElement = new CollectionElement();

			collectionElement.Name = GetAttributeValue(collectionNode, NAME, collectionElement.Name);
			collectionElement.Type = GetAttributeValue(collectionNode, TYPE, collectionElement.Type);
			collectionElement.Template = GetAttributeValue(collectionNode, TEMPLATE, collectionElement.Template);
		
			collectionElements.Add(collectionElement);
		    }
		}
	    }
	}

	public static ArrayList ParseFromXml(Configuration options, XmlDocument doc, Hashtable sqltypes, Hashtable types, ParserValidationDelegate vd, ArrayList entities) {
	    ArrayList list = new ArrayList();
	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("collection");
	    foreach (XmlNode node in elements) {
		CollectionElement collection = new CollectionElement();
		collection.Name = node.Attributes["name"].Value;
		collection.Type = node.Attributes["type"].Value;
		if (node.Attributes["template"] != null) {
		    collection.Template = node.Attributes["template"].Value;
		}
	
		// TODO: this is a hack - need to specify entity as an attribute so that "Data" does not have to be assumed
		// and so that an error can be reported if it does not exist
		foreach(EntityElement entity in entities) {
		    if (collection.Type.Equals(entity.Name + "Data")) {
			collection.Entity = entity;
		    }
		}

		collection.Description = node.InnerText;	
		list.Add(collection);
	    }
	    return list;
	}

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
