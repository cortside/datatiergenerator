using System;
using System.Collections;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class TypeElement : ElementSkeleton {

	private static readonly String NEW_INSTANCE_FORMAT = "newinstanceformat";
	private static readonly String NAMESPACE = "namespace";
	private static readonly String CONVERT_TO_SQLTYPE_FORMAT = "converttosqltypeformat";
	private static readonly String CONVERT_FROM_SQLTYPE_FORMAT = "convertfromsqltypeformat";
	private static readonly String CONVERT_FOR_COMPARE = "convertforcompare";
	private static readonly String NULL_INSTANCE_FORMAT = "nullinstanceformat";

	private String concreteType = String.Empty;
	private String package = String.Empty;
	private String convertToSqlTypeFormat = "{1}";
	private String convertFromSqlTypeFormat = "{2}";
	private String convertForCompare = "{0}.ToString().CompareTo({1}.ToString())";
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

	public String ConvertForCompare {
	    get { return this.convertForCompare; }
	    set { this.convertForCompare = value; }
	}

	public String NewInstanceFormat {
	    get { return this.newInstanceFormat; }
	    set { this.newInstanceFormat = value; }
	}

	public String NullInstanceFormat {
	    get { return this.nullInstanceFormat; }
	    set { this.nullInstanceFormat = value; }
	}

	/// <summary>
	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
	/// list.
	/// </summary>
	/// <param name="node"></param>
	/// <param name="typeElements"></param>
	public static void ParseFromXml(XmlNode node, IList typeElements) {
	    if (node != null && typeElements != null) {

		foreach (XmlNode typeNode in node.SelectNodes("type")) {
		    if (typeNode.NodeType.Equals(XmlNodeType.Element)) {
			TypeElement typeElement = new TypeElement();

			typeElement.Name = GetAttributeValue(typeNode, NAME, typeElement.Name);
			typeElement.NewInstanceFormat = GetAttributeValue(typeNode, NEW_INSTANCE_FORMAT, typeElement.NewInstanceFormat);
			typeElement.Package = GetAttributeValue(typeNode, NAMESPACE, typeElement.Package);
			typeElement.ConvertToSqlTypeFormat = GetAttributeValue(typeNode, CONVERT_TO_SQLTYPE_FORMAT, typeElement.ConvertToSqlTypeFormat);
			typeElement.ConvertFromSqlTypeFormat = GetAttributeValue(typeNode, CONVERT_FROM_SQLTYPE_FORMAT, typeElement.ConvertFromSqlTypeFormat);
			typeElement.NullInstanceFormat = GetAttributeValue(typeNode, NULL_INSTANCE_FORMAT, typeElement.NullInstanceFormat);
		
			typeElements.Add(typeElement);
		    }
		}
	    }
	}

	public static Hashtable ParseFromXml(Configuration options, XmlNode doc, ParserValidationDelegate vd) {
	    Hashtable types = new Hashtable();
	    XmlNodeList nodes = doc.SelectNodes("DataTierGenerator/types/type");
	    foreach (XmlNode node in nodes) {
		if (node.NodeType == XmlNodeType.Comment) {
		    continue;
		}
		TypeElement type = new TypeElement();

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
		if (node.Attributes[CONVERT_FOR_COMPARE] != null) {
		    type.ConvertForCompare = node.Attributes[CONVERT_FOR_COMPARE].Value;
		}
		if (types.ContainsKey(type.Name)) {
		    vd(ParserValidationArgs.NewWarning("ignoring duplicate definition of type: " + type.Name));
		} else {
		    types.Add(type.Name, type);
		}
	    }

//	    // add entities as data objects to types if not already defined
//	    elements = doc.DocumentElement.GetElementsByTagName("entity");
//	    foreach (XmlNode node in elements) {
//		if (node.NodeType == XmlNodeType.Comment) {
//		    continue;
//		}
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
//		if (node.NodeType == XmlNodeType.Comment) {
//		    continue;
//		}
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
//	    // see if we want to generate collections for all entities
//	    XmlNodeList collectionElement = doc.DocumentElement.GetElementsByTagName ("collections");
//	    XmlNode collectionNode = collectionElement[0];
//	    Boolean generateAll = false;
//	    if (collectionNode.Attributes["generateall"] != null) {
//		generateAll = Boolean.Parse (collectionNode.Attributes["generateall"].Value.ToString ());
//	    }
//
//	    if (generateAll) {
//		// add collections for all entities as data objects to types if not already defined
//		elements = doc.DocumentElement.GetElementsByTagName ("entity");
//		foreach (XmlNode node in elements) {
//		    if (node.NodeType == XmlNodeType.Comment) {
//			continue;
//		    }
//		    if (!types.Contains (node.Attributes["name"].Value + "List")) {
//			TypeElement type = new TypeElement ();
//			type.Name = node.Attributes["name"].Value + "List";
//			type.ConcreteType = type.Name;
//			type.Package = options.GetDONameSpace ("");
//			type.NewInstanceFormat = type.Name + ".DEFAULT";
//			type.NullInstanceFormat = type.Name + ".UNSET";
//			types.Add (type.Name, type);
//		    }
//		}
//	    } else {
//		// add collections as data objects to types if not already defined
//		elements = doc.DocumentElement.GetElementsByTagName("collection");
//		foreach (XmlNode node in elements) {
//		    if (node.NodeType == XmlNodeType.Comment) {
//			continue;
//		    }
//		    if (!types.Contains(node.Attributes["name"].Value)) {
//			TypeElement type = new TypeElement();
//			type.Name = node.Attributes["name"].Value;
//			type.ConcreteType = type.Name;
//			type.Package = options.GetDONameSpace("");
//			//type.NewInstanceFormat = "new " + type.Name + "()";
//			type.NewInstanceFormat = type.Name + ".DEFAULT";
//			type.NullInstanceFormat = type.Name + ".UNSET";
//			types.Add(type.Name, type);
//		    }
//		}
//	    }
	    
	    return types;
	}

    }
}
