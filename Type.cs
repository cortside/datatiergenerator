using System;
using System.Collections;
using System.Xml;

namespace Spring2.DataTierGenerator {
    public class Type : Spring2.Core.DataObject.DataObject, ICloneable {

	private String name = String.Empty;
	private String concreteType = String.Empty;
	private String package = String.Empty;
	private String convertToSqlTypeFormat = "{1}";
	private String convertFromSqlTypeFormat = "{2}";
	private String newInstanceFormat = String.Empty;

	public String Name {
	    get { return this.name; }
	    set { this.name = value; }
	}

	public String ConcreteType {
	    get { return this.concreteType; }
	    set { this.concreteType = value; }
	}
	    
	public String Package {
	    get { return this.package; }
	    set { this.package = value; }
	}

	public String ConvertToSqlTypeFormat {
	    get { return this.convertToSqlTypeFormat; }
	    set { this.convertToSqlTypeFormat = value; }
	}

	public String ConvertFromSqlTypeFormat {
	    get { return this.convertFromSqlTypeFormat; }
	    set { this.convertFromSqlTypeFormat = value; }
	}

	public String NewInstanceFormat {
	    get { return this.newInstanceFormat; }
	    set { this.newInstanceFormat = value; }
	}

	public static Hashtable ParseFromXml(Configuration options, XmlDocument doc) {
	    Hashtable types = new Hashtable();
	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("type");
	    foreach (XmlNode node in elements) {
		Type type = new Type();

		type.Name = node.Attributes["name"].Value;
		type.ConcreteType = type.Name;

		if (node.Attributes["concretetype"] != null) {
		    type.ConcreteType = node.Attributes["concretetype"].Value;
		}
		if (node.Attributes["namespace"] != null) {
		    type.Package = node.Attributes["namespace"].Value;
		}
		if (node.Attributes["newinstanceformat"] != null) {
		    type.NewInstanceFormat = node.Attributes["newinstanceformat"].Value;
		}
		if (node.Attributes["converttosqltypeformat"] != null) {
		    type.ConvertToSqlTypeFormat = node.Attributes["converttosqltypeformat"].Value;
		}
		if (node.Attributes["convertfromsqltypeformat"] != null) {
		    type.ConvertFromSqlTypeFormat = node.Attributes["convertfromsqltypeformat"].Value;
		}
		types.Add(type.Name, type);
	    }

	    elements = doc.DocumentElement.GetElementsByTagName("entity");
	    foreach (XmlNode node in elements) {
		if (!types.Contains(node.Attributes["name"].Value + "Data")) {
		    Type type = new Type();
		    type.Name = node.Attributes["name"].Value + "Data";
		    type.ConcreteType = type.Name;
		    type.Package = options.GetDONameSpace("");
		    type.NewInstanceFormat = "new " + type.Name + "Data()";
		    types.Add(type.Name, type);
		}
	    }
	    
	    return types;
	}

	public Object Clone() {
	    return MemberwiseClone();
	}

    }
}
