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
	private String nullInstanceFormat = String.Empty;

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

	public String NullInstanceFormat {
	    get { return this.nullInstanceFormat; }
	    set { this.nullInstanceFormat = value; }
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
		    type.NullInstanceFormat = type.NewInstanceFormat;
		}
		if (node.Attributes["nullinstanceformat"] != null) {
		    type.NullInstanceFormat = node.Attributes["nullinstanceformat"].Value;
		}
		if (node.Attributes["converttosqltypeformat"] != null) {
		    type.ConvertToSqlTypeFormat = node.Attributes["converttosqltypeformat"].Value;
		}
		if (node.Attributes["convertfromsqltypeformat"] != null) {
		    type.ConvertFromSqlTypeFormat = node.Attributes["convertfromsqltypeformat"].Value;
		}
		if (types.ContainsKey(type.Name)) {
		    Console.Out.WriteLine ("WARNING: ignoring duplicate definition of type: " + type.Name);
		} else {
		    types.Add(type.Name, type);
		}
	    }

	    // add entities as data objects to types if not already defined
	    elements = doc.DocumentElement.GetElementsByTagName("entity");
	    foreach (XmlNode node in elements) {
		if (!types.Contains(node.Attributes["name"].Value + "Data")) {
		    Type type = new Type();
		    type.Name = node.Attributes["name"].Value + "Data";
		    type.ConcreteType = type.Name;
		    type.Package = options.GetDONameSpace("");
		    type.NewInstanceFormat = "new " + type.Name + "()";
		    types.Add(type.Name, type);
		}
	    }

	    // add enums to types if not already defined
	    elements = doc.DocumentElement.GetElementsByTagName("enum");
	    foreach (XmlNode node in elements) {
		if (!types.Contains(node.Attributes["name"].Value)) {
		    Type type = new Type();
		    type.Name = node.Attributes["name"].Value;
		    type.ConcreteType = type.Name;
		    type.Package = options.GetTypeNameSpace("");
		    type.ConvertToSqlTypeFormat = "{1}.DBValue";
		    type.ConvertFromSqlTypeFormat = type.Name + ".GetInstance({2})";
		    type.NewInstanceFormat = type.Name + ".DEFAULT";
		    type.NullInstanceFormat = type.Name + ".UNSET";
		    types.Add(type.Name, type);
		}
	    }

	    // add collections as data objects to types if not already defined
	    elements = doc.DocumentElement.GetElementsByTagName("collection");
	    foreach (XmlNode node in elements) {
		if (!types.Contains(node.Attributes["name"].Value)) {
		    Type type = new Type();
		    type.Name = node.Attributes["name"].Value;
		    type.ConcreteType = type.Name;
		    type.Package = options.GetDONameSpace("");
		    //type.NewInstanceFormat = "new " + type.Name + "()";
		    type.NewInstanceFormat = type.Name + ".DEFAULT";
		    type.NullInstanceFormat = type.Name + ".UNSET";
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
