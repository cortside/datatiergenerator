using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator.Core {
    public class EnumValue : Spring2.Core.DataObject.DataObject {

	protected String name = String.Empty;
	protected String code = String.Empty;

	public String Name {
	    get { return this.name; }
	    set { this.name = value; }
	}

	public String Code {
	    get { return this.code; }
	    set { this.code = value; }
	}

	public String ToXml() {
	    StringBuilder sb = new StringBuilder();
	    sb.Append("<value name=\"").Append(name).Append("\"");
	    sb.Append(" code=\"").Append(code).Append("\"");
	    sb.Append(" />");

	    return sb.ToString();
	}

	public static ArrayList ParseFromXml(String name, Configuration options, XmlDocument doc, Hashtable sqltypes, Hashtable types) {
	    ArrayList values = new ArrayList();
	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("enum");
	    foreach (XmlNode element in elements) {
		if (element.Attributes["name"].Value.Equals(name) && element.HasChildNodes) {
		    foreach (XmlNode node in element.ChildNodes) {
			if (node.Name.Equals("value")) {
			    EnumValue value = new EnumValue();
			    value.Name = node.Attributes["name"].Value;
			    value.Code = node.Attributes["code"].Value;
			    values.Add(value);
			}
		    }
		}
	    }
	    return values;
	}

    }
}
