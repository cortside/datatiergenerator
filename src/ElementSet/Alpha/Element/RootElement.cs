using System;
using System.Collections;
using System.Xml;

namespace Spring2.DataTierGenerator.Element {
    /// <summary>
    /// Summary description for RootElement.
    /// </summary>
    public class RootElement : ElementSkeleton {

	private IList configElements = new ArrayList();
	private IList entityElements = new ArrayList();
	private IList collectionElements = new ArrayList();
	private IList enumElements = new ArrayList();
	private IList typeElements = new ArrayList();
	private IList sqlTypeElements = new ArrayList();
	private IList databaseElements = new ArrayList();
	private IList generatorElements = new ArrayList();
	private IList parserElements = new ArrayList();

	public IList ConfigElements {
	    get { return configElements; }
	}

	public IList EntityElements {
	    get { return entityElements; }
	}

	public IList CollectionElements {
	    get { return collectionElements; }
	}

	public IList EnumElements {
	    get { return enumElements; }
	}
	
	public IList TypeElements {
	    get { return typeElements; }
	}
	
	public IList SqlTypeElements {
	    get { return sqlTypeElements; }
	}
	
	public IList DatabaseElements {
	    get { return databaseElements; }
	}
	
	public IList GeneratorElements {
	    get { return generatorElements; }
	}
	
	public IList ParserElements {
	    get { return parserElements; }
	}

	/// <summary>
	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
	/// list.
	/// </summary>
	/// <param name="node"></param>
	/// <param name="entityElements"></param>
	public static void ParseFromXml(XmlNode rootNode) {

	    if (rootNode != null) {
		RootElement rootElement = new RootElement();

//		ConfigElement.ParseFromXml(GetChildNodeByName(rootNode, "config"), rootElement.ConfigElements);
		EntityElement.ParseFromXml(GetChildNodeByName(rootNode, "entities"), rootElement.EntityElements);
		CollectionElement.ParseFromXml(GetChildNodeByName(rootNode, "collections"), rootElement.CollectionElements);
		EnumElement.ParseFromXml(GetChildNodeByName(rootNode, "enums"), rootElement.EnumElements);
		TypeElement.ParseFromXml(GetChildNodeByName(rootNode, "types"), rootElement.TypeElements);
		SqlTypeElement.ParseFromXml(GetChildNodeByName(rootNode, "sqltypes"), rootElement.SqlTypeElements);
		DatabaseElement.ParseFromXml(GetChildNodeByName(rootNode, "databases"), rootElement.DatabaseElements);
		GeneratorElement.ParseFromXml(GetChildNodeByName(rootNode, "generator"), rootElement.GeneratorElements);
		ParserElement.ParseFromXml(GetChildNodeByName(rootNode, "parser"), rootElement.ParserElements);
	    }
	}
    }
}
