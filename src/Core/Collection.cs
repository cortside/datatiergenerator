using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator.Core {
    public class Collection : Spring2.Core.DataObject.DataObject {

	protected String name = String.Empty;
	protected String type = String.Empty;

	public String Name {
	    get { return this.name; }
	    set { this.name = value; }
	}

	public String Type {
	    get { return this.type; }
	    set { this.type = value; }
	}

	public static ArrayList ParseFromXml(Configuration options, XmlDocument doc, Hashtable sqltypes, Hashtable types) {
	    ArrayList list = new ArrayList();
	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("collection");
	    foreach (XmlNode node in elements) {
		Collection collection = new Collection();
		collection.Name = node.Attributes["name"].Value;
		collection.Type = node.Attributes["type"].Value;
		list.Add(collection);
	    }
	    return list;
	}

    }
}
