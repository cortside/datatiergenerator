using System;
using System.Collections;
using System.Xml;

namespace Spring2.DataTierGenerator.Element {

    public class TypeElement : ElementSkeleton { 

	public static readonly String TYPE = "type";
	private static readonly String NEW_INSTANCE_FORMAT = "newinstanceformat";
	private static readonly String NAMESPACE = "namespace";
	private static readonly String CONVERT_TO_SQLTYPE_FORMAT = "converttosqltypeformat";
	private static readonly String CONVERT_FROM_SQLTYPE_FORMAT = "convertfromsqltypeformat";
	private static readonly String NULL_INSTANCE_FORMAT = "nullinstanceformat";

	private String concreteType = String.Empty;
	private String package = String.Empty;
	private String convertToSqlTypeFormat = "{1}";
	private String convertFromSqlTypeFormat = "{2}";
	private String newInstanceFormat = String.Empty;
	private String nullInstanceFormat = String.Empty;

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

	public TypeElement() {}

	public TypeElement(XmlNode typeNode) {

	    if (typeNode != null && TYPE.Equals(typeNode.Name)) {
		name = GetAttributeValue(typeNode, NAME, name);
		newInstanceFormat = GetAttributeValue(typeNode, NEW_INSTANCE_FORMAT, newInstanceFormat);
		package = GetAttributeValue(typeNode, NAMESPACE, package);
		convertToSqlTypeFormat = GetAttributeValue(typeNode, CONVERT_TO_SQLTYPE_FORMAT, convertToSqlTypeFormat);
		convertFromSqlTypeFormat = GetAttributeValue(typeNode, CONVERT_FROM_SQLTYPE_FORMAT, convertFromSqlTypeFormat);
		nullInstanceFormat = GetAttributeValue(typeNode, NULL_INSTANCE_FORMAT, nullInstanceFormat);
	    } else {
		throw new ArgumentException("The XmlNode argument is not a type node.");
	    }
	}

	public override void Validate(RootElement root) {
	    // Nothing to be done here, but root element should add collections, enums and entity variants to 
	    // its type collection.
	}

//	public static Hashtable ParseFromXml(ConfigurationElement options, XmlDocument doc, IParser parser) {
//	    Hashtable types = new Hashtable();
//	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("type");
//	    foreach (XmlNode node in elements) {
//		TypeElement type = new TypeElement();
//
//		type.Name = node.Attributes["name"].Value;
//		type.ConcreteType = type.Name;
//
//		if (node.Attributes["concretetype"] != null) {
//		    type.ConcreteType = node.Attributes["concretetype"].Value;
//		}
//		if (node.Attributes["namespace"] != null) {
//		    type.Package = node.Attributes["namespace"].Value;
//		}
//		if (node.Attributes["newinstanceformat"] != null) {
//		    type.NewInstanceFormat = node.Attributes["newinstanceformat"].Value;
//		    type.NullInstanceFormat = type.NewInstanceFormat;
//		}
//		if (node.Attributes["nullinstanceformat"] != null) {
//		    type.NullInstanceFormat = node.Attributes["nullinstanceformat"].Value;
//		}
//		if (node.Attributes["converttosqltypeformat"] != null) {
//		    type.ConvertToSqlTypeFormat = node.Attributes["converttosqltypeformat"].Value;
//		}
//		if (node.Attributes["convertfromsqltypeformat"] != null) {
//		    type.ConvertFromSqlTypeFormat = node.Attributes["convertfromsqltypeformat"].Value;
//		}
//		if (types.ContainsKey(type.Name)) {
//		    parser.AddValidationMessage(ParserValidationMessage.NewWarning("ignoring duplicate definition of type: " + type.Name));
//		} else {
//		    types.Add(type.Name, type);
//		}
//	    }
//
//	    // add entities as data objects to types if not already defined
//	    elements = doc.DocumentElement.GetElementsByTagName("entity");
//	    foreach (XmlNode node in elements) {
//		if (!types.Contains(node.Attributes["name"].Value + "Data")) {
//		    TypeElement type = new TypeElement();
//		    type.Name = node.Attributes["name"].Value + "Data";
//		    type.ConcreteType = type.Name;
//		    type.Package = options.GetDONameSpace("");
//		    type.NewInstanceFormat = "new " + type.Name + "()";
//		    types.Add(type.Name, type);
//		}
//
//		// TODO: needs review, hacked for Dave
//		// add interfaces - use new x() for empty instance constructor
//		if (!types.Contains("I" + node.Attributes["name"].Value)) {
//		    TypeElement type = new TypeElement();
//		    type.Name = "I" + node.Attributes["name"].Value;
//		    type.ConcreteType = type.Name;
//		    type.Package = options.GetDONameSpace("");
//		    type.NewInstanceFormat = "new " + node.Attributes["name"].Value + "()";
//		    types.Add(type.Name, type);
//		}
//
//		// TODO: needs review, hacked for Dave
//		// add business entities - use new x() for empty instance constructor
//		if (!types.Contains(node.Attributes["name"].Value)) {
//		    TypeElement type = new TypeElement();
//		    type.Name = node.Attributes["name"].Value;
//		    type.ConcreteType = type.Name;
//		    type.Package = options.GetBusinessLogicNameSpace();
//		    type.NewInstanceFormat = "new " + type.Name + "()";
//		    types.Add(type.Name, type);
//		}
//	    }
//
//
//	    // add enums to types if not already defined
//	    elements = doc.DocumentElement.GetElementsByTagName("enum");
//	    foreach (XmlNode node in elements) {
//		if (!types.Contains(node.Attributes["name"].Value)) {
//		    TypeElement type = new TypeElement();
//		    type.Name = node.Attributes["name"].Value;
//		    type.ConcreteType = type.Name;
//		    type.Package = options.GetTypeNameSpace("");
//		    type.ConvertToSqlTypeFormat = "{1}.DBValue";
//		    type.ConvertFromSqlTypeFormat = type.Name + ".GetInstance({2})";
//		    type.NewInstanceFormat = type.Name + ".DEFAULT";
//		    type.NullInstanceFormat = type.Name + ".UNSET";
//		    types.Add(type.Name, type);
//		}
//	    }
//
//	    // add collections as data objects to types if not already defined
//	    elements = doc.DocumentElement.GetElementsByTagName("collection");
//	    foreach (XmlNode node in elements) {
//		if (!types.Contains(node.Attributes["name"].Value)) {
//		    TypeElement type = new TypeElement();
//		    type.Name = node.Attributes["name"].Value;
//		    type.ConcreteType = type.Name;
//		    type.Package = options.GetDONameSpace("");
//		    //type.NewInstanceFormat = "new " + type.Name + "()";
//		    type.NewInstanceFormat = type.Name + ".DEFAULT";
//		    type.NullInstanceFormat = type.Name + ".UNSET";
//		    types.Add(type.Name, type);
//		}
//	    }
//	    
//	    return types;
//	}
    }
}
