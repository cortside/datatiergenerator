using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator.Core {
    public class EnumType : Spring2.Core.DataObject.DataObject {

	protected String name = String.Empty;
	protected IList values = new ArrayList();

	public String Name {
	    get { return this.name; }
	    set { this.name = value; }
	}

	public IList Values{
	    get { return this.values; }
	    set { this.values = value; }
	}

	public static ArrayList ParseFromXml(Configuration options, XmlDocument doc, Hashtable sqltypes, Hashtable types) {
	    ArrayList enums = new ArrayList();
	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("enum");
	    foreach (XmlNode node in elements) {
		EnumType type = new EnumType();
		type.Name = node.Attributes["name"].Value;
		type.Values = EnumValue.ParseFromXml(type.Name, options, doc, sqltypes, types);
		enums.Add(type);
	    }
	    return enums;
	}

    }
}
