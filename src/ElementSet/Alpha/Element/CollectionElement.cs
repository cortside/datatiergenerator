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

	protected TypeElement type = new TypeElement();
	protected ICollectable collectedClass = new EntityElement();

	protected EntityElement entity = new EntityElement();

	public EntityElement Entity {
	    get { return this.entity; }
	    set { this.entity = value; }
	}

	public TypeElement Type {
	    get { return this.type; }
	    set { this.type = value; }
	}

	public ICollectable CollectedClass {
	    get { return this.collectedClass; }
	    set { this.collectedClass = value; }
	}

	/// <summary>
	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
	/// list.
	/// </summary>
	/// <param name="node">Node containing the collection entries</param>
	/// <param name="collectionElements">List of collection elements created.</param>
	public static void ParseFromXml(XmlNode node, IList collectionElements) {
	    if (node != null && collectionElements != null) {

		foreach (XmlNode collectionNode in node.ChildNodes) {
		    if (collectionNode.NodeType.Equals(XmlNodeType.Element)) {
			CollectionElement collectionElement = new CollectionElement();

			collectionElement.Name = GetAttributeValue(collectionNode, NAME, collectionElement.Name);
			collectionElement.Type.Name = GetAttributeValue(collectionNode, TYPE, collectionElement.Type.Name);
			collectionElement.Template = GetAttributeValue(collectionNode, TEMPLATE, collectionElement.Template);
		
			collectionElements.Add(collectionElement);
		    }
		}
	    }
	}

	/// <summary>
	/// Parses for real on second pass.
	/// </summary>
	/// <param name="options">Configuration class with optinos</param>
	/// <param name="doc">Document being parsed.</param>
	/// <param name="sqltypes">List of sql types defined</param>
	/// <param name="types">List of .Net types defined</param>
	/// <param name="vd">Validation delegate for error reporting</param>
	/// <param name="collectableClasses">List of classes that are valid to have collections created.</param>
	/// <param name="autoGenerateClasses">List of classes that should have collections generated if generateall is specified.</param>
	/// <returns>List of collection elements created</returns>
	public static ArrayList ParseFromXml(Configuration options, XmlDocument doc, Hashtable sqltypes, Hashtable types, ParserValidationDelegate vd, 
	    IList collectableClasses, IList autoGenerateClasses, ArrayList entities) {

	    // see if we want to generate collections for all classes.
	    XmlNodeList collectionElement = doc.DocumentElement.GetElementsByTagName ("collections");
	    XmlNode collectionNode = collectionElement[0];
	    Boolean generateAll = false;
	    if (collectionNode.Attributes["generateall"] != null) {
		generateAll = Boolean.Parse (collectionNode.Attributes["generateall"].Value.ToString ());
	    }

	    // Add all collectable classses if chosen.
	    ArrayList list = new ArrayList();
	    if (generateAll) {
		foreach (ICollectable collectableClass in autoGenerateClasses) {
		    CollectionElement collection  = new CollectionElement ();
		    collection.Name = collectableClass.Name + "List";
		    collection.Type.Name = collectableClass.Name + "Data";
		    collection.Description = "Auto-generated collection for " + collectableClass.Name + "Data type.";
		    collection.CollectedClass = collectableClass;
		    list.Add (collection);
		}
	    }

	    // Now add explicitly mentioned elements.  Note we add explicit elements
	    // as well as we might have lists of enums here.
	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("collection");
	    foreach (XmlNode node in elements) {
		if (node.NodeType == XmlNodeType.Comment) {
		    continue;
		}
		CollectionElement collection = new CollectionElement();
		collection.Name = node.Attributes["name"].Value;
		collection.Type.Name = node.Attributes["type"].Value;
		if (node.Attributes["template"] != null) {
		    collection.Template = node.Attributes["template"].Value;
		}
	
		// TODO: this is a hack - need to specify entity as an attribute so that "Data" does not have to be assumed
		// and so that an error can be reported if it does not exist
		foreach(ICollectable collectableClass in collectableClasses) {
		    if (collection.Type.Name.Equals(collectableClass.Name + "Data")) {
			collection.CollectedClass = collectableClass;
		    }
		}

		// TODO: how can this be modified for "reportexcations"
		foreach(EntityElement entity in entities) {
		    if (collection.Type.Name.Equals(entity.Name + "Data")) {
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
