using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {
    /// <summary>
    /// Summary description for RootElement.
    /// </summary>
    public class RootElement : ElementSkeleton {

	private Boolean isValid = true;

	// Element name.
	public static readonly String DATA_TIER_GENERATOR = "DataTierGenerator";

	// Sub element names.
	private static readonly String ENTITIES = "entities";
	private static readonly String COLLECTIONS = "collections";
	private static readonly String ENUMS = "enums";
	private static readonly String TYPES = "types";
	private static readonly String SQLTYPES = "sqltypes";
	private static readonly String DATABASES = "databases";

	private IList configElements = new ArrayList();
	private IList entityElements = new ArrayList();
	private IList collectionElements = new ArrayList();
	private IList enumElements = new ArrayList();
	private IDictionary typeElements = new Hashtable();
	private IList sqlTypeElements = new ArrayList();
	private IList databaseElements = new ArrayList();
	private IList generatorElements = new ArrayList();
	private IList parserElements = new ArrayList();

	private IList log = new ArrayList();

	private RootElement(XmlNode rootNode) {
	    
	    // Parse entities.
	    foreach (XmlNode node in GetChildNodes(rootNode, ENTITIES, EntityElement.ENTITY)) {
		entityElements.Add(new EntityElement(node));
	    }

	    // Parse collections.
	    foreach (XmlNode node in GetChildNodes(rootNode, COLLECTIONS, CollectionElement.COLLECTION)) {
		collectionElements.Add(new CollectionElement(node));
	    }

	    // Parse enums.
	    foreach (XmlNode node in GetChildNodes(rootNode, ENUMS, EnumElement.ENUM)) {
		enumElements.Add(new EnumElement(node));
	    }

	    // Parse types.
	    foreach (XmlNode node in GetChildNodes(rootNode, TYPES, TypeElement.TYPE)) {
		TypeElement typeElement = new TypeElement(node);
		typeElements.Add(typeElement.Name, typeElement);
	    }

	    // Parse SQL types.
	    foreach (XmlNode node in GetChildNodes(rootNode, SQLTYPES, SqlTypeElement.SQLTYPE)) {
		sqlTypeElements.Add(new SqlTypeElement(node));
	    }

	    // Parse databases.
	    XmlNode databasesNode = GetChildNodeByName(rootNode, DATABASES);
	    SqlEntityData databaseDefaults = new SqlEntityData(databasesNode, new SqlEntityData());
	    foreach (XmlNode node in GetChildNodes(rootNode, DATABASES, DatabaseElement.DATABASE)) {
		databaseElements.Add(new DatabaseElement(node, databaseDefaults));
	    }

	    // Parse generator.
	    foreach (XmlNode node in GetChildNodes(rootNode, GeneratorElement.GENERATOR)) {
		generatorElements.Add(new GeneratorElement(node));
	    }

	}

//	public RootElement(ParserElement parser, ConfigurationElement options, XmlDocument doc) {
//	    this.configElements.Add(options);
//	    XmlNode node = doc.GetElementsByTagName(DATA_TIER_GENERATOR)[0];
//	    Parse(node);
//	}

	public override void ToXml(StringBuilder buffer, Int32 indentLevel) {
	    buffer.Append(OpenTag(DATA_TIER_GENERATOR, indentLevel));

	    foreach (ConfigurationElement element in this.ConfigElements) {
		element.ToXml(buffer, indentLevel + 1);
	    }

	    if (entityElements.Count > 0) {
		buffer.AppendFormat(OpenTag(ENTITIES, indentLevel + 1));
		foreach (EntityElement element in entityElements) {
		    element.ToXml(buffer, indentLevel + 2);
		}
		buffer.AppendFormat(CloseTag(ENTITIES, indentLevel + 1));
	    }

	    buffer.Append(CloseTag(DATA_TIER_GENERATOR, indentLevel));
	}

	public override String ToString() {
	    StringBuilder buffer = new StringBuilder("ROOT\n");
	    foreach (EntityElement entity in entityElements) {
		buffer.Append(entity.ToString());
	    }
	    return buffer.ToString();
	}

	protected void WriteToLog(String s) {
	    log.Add(s);
	}

	public IList Log {
	    get { return log; }
	}

	public ConfigurationElement Configuration {
	    get { return (ConfigurationElement)ConfigElements[0]; }
	}

	public Boolean HasWarnings {
	    get { return Errors.Count > 0; }
	}

	public Boolean IsValid {
	    get { return true; }
	    set { isValid = value; }
	}

	public IList Errors {
	    get { return this.log; }
	}

	public String ErrorDescription {
	    get { return String.Empty; }
	}

	public IList ConfigElements {
	    get { return configElements; }
	}

	public IList Entities {
	    get { return entityElements; }
	}

	public IList Collections {
	    get { return collectionElements; }
	}

	public IList Enums {
	    get { return enumElements; }
	}
	
	public IDictionary TypeElements {
	    get { return typeElements; }
	}

	public ICollection Types {
	    get { return typeElements.Values; }
	}
	
	public ICollection SqlTypes {
	    get { return sqlTypeElements; }
	}
	
	public IList Databases {
	    get { return databaseElements; }
	}
	
	public GeneratorElement Generator {
	    get { return (GeneratorElement)generatorElements[0]; }
	}
	
	public void AddValidationMessage(ParserValidationMessage message) {
	    this.log.Add(message);
	}

	public void Parse(XmlNode rootNode) {
	}

	public EntityElement FindEntity(String name) {
	    foreach (EntityElement entity in Entities) {
		if (entity.Name.Equals(name)) {
		    return entity;
		}
	    }
	    return null;
	}

	public SqlEntityElement FindSqlEntity(String name) {
	    foreach (DatabaseElement database in Databases) {
		foreach (SqlEntityElement sqlEntity in database.SqlEntities) {
		    if (sqlEntity.Name.Equals(name)) {
			return sqlEntity;
		    }
		}
	    }
	    return null;
	}

	public TypeElement FindType(String name) {
	    foreach (TypeElement type in TypeElements.Values) {
		if (type.Name.Equals(name)) {
		    return type;
		}
	    }
	    return null;
	}

	public SqlTypeElement FindSqlType(String name) {
	    foreach (SqlTypeElement sqlType in SqlTypes) {
		if (sqlType.Name.Equals(name)) {
		    return sqlType;
		}
	    }
	    return null;
	}

	public void Validate() {
	    Validate(this);
	}

	public override void Validate(RootElement root) {

	    PreValidate();
	    
	    // Recursively validate all parsed elements.
	    foreach (EntityElement elem in entityElements) {
		elem.Validate(root);
	    }
	    foreach (CollectionElement elem in collectionElements) {
		elem.Validate(root);
	    }
	    foreach (EnumElement elem in enumElements) {
		elem.Validate(root);
	    }
	    foreach (TypeElement elem in typeElements.Values) {
		elem.Validate(root);
	    }
	    foreach (SqlTypeElement elem in sqlTypeElements) {
		elem.Validate(root);
	    }
	    foreach (DatabaseElement elem in databaseElements) {
		elem.Validate(root);
	    }
	    foreach (GeneratorElement elem in generatorElements) {
		elem.Validate(root);
	    }

	    PostValidate();
	}

	/// <summary>
	/// Sets up type collection and does anything else that is needed for the
	/// validation step.
	/// </summary>
	private void PreValidate() {
	    // Add types to the type collection.
	    foreach (EntityElement elem in entityElements) {
		if (!typeElements.Contains(elem.Name + "Data")) {
		    TypeElement type = new TypeElement();
		    type.Name = elem.Name + "Data";
		    type.Package = Configuration.GetDONameSpace("");
		    type.NewInstanceFormat = "new " + type.Name + "()";
		    typeElements.Add(type.Name, type);
		}

		// TODO: needs review, hacked for Dave
		// add interfaces - use new x() for empty instance constructor
		if (!typeElements.Contains("I" + elem.Name)) {
		    TypeElement type = new TypeElement();
		    type.Name = "I" + elem.Name;
		    type.ConcreteType = type.Name;
		    type.Package = Configuration.GetDONameSpace("");
		    type.NewInstanceFormat = "new " + elem.Name + "()";
		    typeElements.Add(type.Name, type);
		}

		// TODO: needs review, hacked for Dave
		// add business entities - use new x() for empty instance constructor
		if (!typeElements.Contains(elem.Name)) {
		    TypeElement type = new TypeElement();
		    type.Name = elem.Name;
		    type.ConcreteType = type.Name;
		    type.Package = Configuration.GetBusinessLogicNameSpace();
		    type.NewInstanceFormat = "new " + type.Name + "()";
		    typeElements.Add(type.Name, type);
		}
	    }

	    // Add enums to types if not already defined.
	    foreach (EnumElement elem in enumElements) {
		if (!typeElements.Contains(elem.Name)) {
		    TypeElement type = new TypeElement();
		    type.Name = elem.Name;
		    type.ConcreteType = type.Name;
		    type.Package = Configuration.GetTypeNameSpace("");
		    type.ConvertToSqlTypeFormat = "{1}.DBValue";
		    type.ConvertFromSqlTypeFormat = type.Name + ".GetInstance({2})";
		    type.NewInstanceFormat = type.Name + ".DEFAULT";
		    type.NullInstanceFormat = type.Name + ".UNSET";
		    typeElements.Add(type.Name, type);
		}
	    }

	    // Add collections as data objects to types if not already defined.
	    foreach (CollectionElement elem in collectionElements) {
		if (!typeElements.Contains(elem.Name)) {
		    TypeElement type = new TypeElement();
		    type.Name = elem.Name;
		    type.ConcreteType = type.Name;
		    type.Package = Configuration.GetDONameSpace("");
		    //type.NewInstanceFormat = "new " + type.Name + "()";
		    type.NewInstanceFormat = type.Name + ".DEFAULT";
		    type.NullInstanceFormat = type.Name + ".UNSET";
		    typeElements.Add(type.Name, type);
		}
	    }
	}

	private void PostValidate() {
	}
    }
}
