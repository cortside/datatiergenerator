using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class EntityElement : ElementSkeleton {

	// Element name.
	public static readonly String ENTITY = "entity";

	// Element property names.
	public static readonly String PROPERTIES = "properties";
	private static readonly String FINDERS = "finders";
	private static readonly String BASE_ENTITY = "baseentity";
	private static readonly String ABSTRACT = "abstract";
	private static readonly String SQLENTITY = "sqlentity";

	private static readonly EntityElement EMPTY = new EntityElement();

	private EntityElement baseEntity = EMPTY;
	private Boolean isAbstract = false;
	private ArrayList properties = new ArrayList();
	private ArrayList finders = new ArrayList();
	private ArrayList dataMaps = new ArrayList();

	private SqlEntityElement sqlEntity = new SqlEntityElement();

	/// <summary>
	/// Construct an empty entity element.
	/// </summary>
	public EntityElement() {}

	/// <summary>
	/// Construct an entity element by parsing an xml node.
	/// </summary>
	/// <param name="entityNode"></param>
	public EntityElement(XmlNode entityNode) : base(entityNode) {

	    if (ENTITY.Equals(entityNode.Name)) {
		
		baseEntity.Name = GetAttributeValue(entityNode, BASE_ENTITY, baseEntity.Name);
		isAbstract = Boolean.Parse(GetAttributeValue(entityNode, ABSTRACT, isAbstract.ToString()));

		foreach (XmlNode node in GetChildNodes(entityNode, PROPERTIES, PropertyElement.PROPERTY)) {
		    String propertyName = GetAttributeValue(node, NAME, String.Empty);
		    if (propertyName.IndexOf('.') < 0) {
			properties.Add(new PropertyElement(node, this));
		    }
		}

		foreach (XmlNode node in GetChildNodes(entityNode, FINDERS, FinderElement.FINDER)) {
		    finders.Add(new FinderElement(node, this));
		}

		String sqlEntityName = GetAttributeValue(entityNode, SQLENTITY, String.Empty);
		if (!String.Empty.Equals(sqlEntityName)) {
		    dataMaps.Add(new DataMapElement(entityNode, sqlEntityName, this));
		}
	    
	    } else {
		throw new ArgumentException("The XmlNode argument is not an entity node.");
	    }
	}

	public SqlEntityElement SqlEntity {
	    get { return this.sqlEntity; }
	    set { this.sqlEntity = value; }
	}

	public EntityElement BaseEntity {
	    get { return this.baseEntity; }
	    set { this.baseEntity = value; }
	}

	public Boolean IsAbstract {
	    get { return this.isAbstract; }
	    set { this.isAbstract = value; }
	}

	public ArrayList Properties {
	    get { return this.properties; }
	    set { this.properties = value; }
	}

	public ArrayList Finders {
	    get { return this.finders; }
	    set { this.finders = value; }
	}

	public IList DataMaps {
	    get { return ArrayList.ReadOnly(dataMaps); }
	}

	public override void Validate(IParser parser) {

	    // Look up the SQL entity in the database elements.
	    //	    if (!this.SqlEntity.Name.Equals(String.Empty)) {
	    //		SqlEntityElement sqlEntity = RootElement.Instance.FindSqlEntityByName(SqlEntity.Name);
	    //		if (sqlEntity == null) {
	    //		    this.AddValidationMessage(ParserValidationMessage.NewError("sqlentity (" + SqlEntity.Name + ") specified in entity " + Name + " could not be found as an defined sql entity"));
	    //		} else {
	    //		    this.sqlEntity = sqlEntity;
	    //		}
	    //	    }

	    // Look up the base entity in the entity collection.
	    if (!this.BaseEntity.Name.Equals(String.Empty)) {
		EntityElement baseEntity = parser.FindEntity(this.BaseEntity.Name);
		if (baseEntity == null) {
		    parser.AddValidationMessage(ParserValidationMessage.NewError("baseentity (" + BaseEntity.Name + ") specified in entity " + Name + " could not be found as an defined entity (or is defined after this entity in the config file)"));
		} else {
		    this.baseEntity = baseEntity;
		}
	    }

	    foreach (PropertyElement property in this.Properties) {
		property.Validate(parser);
	    }
	    foreach (DataMapElement dataMap in this.dataMaps) {
		dataMap.Validate(parser);
	    }
	    foreach (FinderElement finder in this.Finders) {
		finder.Validate(parser);
	    }
	}

	public static ArrayList ParseFromXml(ConfigurationElement options, XmlDocument doc, Hashtable sqltypes, Hashtable types, ArrayList sqlentities, IParser parser) {
	    ArrayList entities = new ArrayList();
	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("entity");
	    foreach (XmlNode node in elements) {
		EntityElement entity = new EntityElement();
		entity.Name = node.Attributes["name"].Value;
		if (node.Attributes["sqlentity"] != null) {
		    SqlEntityElement sqlentity = SqlEntityElement.FindByName(sqlentities, node.Attributes["sqlentity"].Value);
		    if (sqlentity!=null) {
			entity.SqlEntity = (SqlEntityElement)sqlentity.Clone();
		    } else {
			entity.SqlEntity.Name = node.Attributes["sqlentity"].Value;
			parser.AddValidationMessage(ParserValidationMessage.NewError("sqlentity (" + entity.SqlEntity.Name + ") specified in entity " + entity.Name + " could not be found as an defined sql entity"));
		    }
		}

		if (node.Attributes["baseentity"] != null) {
		    EntityElement baseentity = EntityElement.FindEntityByName(entities, node.Attributes["baseentity"].Value);
		    if (baseentity!=null) {
			entity.BaseEntity = (EntityElement)baseentity.Clone();
		    } else {
			entity.BaseEntity.Name = node.Attributes["baseentity"].Value;
			parser.AddValidationMessage(ParserValidationMessage.NewError("baseentity (" + entity.BaseEntity.Name + ") specified in entity " + entity.Name + " could not be found as an defined entity (or is defined after this entity in the config file)"));
		    }
		}

		if (node.Attributes["abstract"] != null) {
		    entity.IsAbstract = Boolean.Parse(node.Attributes["abstract"].Value);
		}

		entity.Properties = PropertyElement.ParseFromXml(doc, entities, entity, sqltypes, types, parser);
		entity.Finders = FinderElement.ParseFromXml(node, entity, parser);
		entities.Add(entity);
	    }
	    return entities;
	}

	public override String ToString() {
	    StringBuilder buffer = new StringBuilder("ENTITY\n");
	    foreach (DataMapElement dataMap in dataMaps) {
		buffer.Append(dataMap.ToString());
	    }
	    return buffer.ToString();
	}

	public override void ToXml(StringBuilder buffer, Int32 indentLevel) {
	    buffer.Append(OpenTag(String.Format("{0} name=\"{1}\"", ENTITY, this.Name), indentLevel));

	    if (properties.Count > 0) {
		buffer.Append(OpenTag(PROPERTIES, indentLevel + 1));
		foreach (PropertyElement property in properties) {
		    property.ToXml(buffer, indentLevel + 2);
		}
		buffer.Append(CloseTag(PROPERTIES, indentLevel + 1));
	    }
	    if (finders.Count > 0) {
		buffer.Append(OpenTag(FINDERS, indentLevel + 1));
		buffer.Append(CloseTag(FINDERS, indentLevel + 1));
	    }

	    buffer.Append(CloseTag(ENTITY, indentLevel));
	}

	public String ToXml() {
	    StringBuilder sb = new StringBuilder();
	    sb.Append("<entity name=\"").Append(name).Append("\"");
	    sb.Append(" sqlentity.name=\"").Append(sqlEntity.Name).Append("\"");
	    sb.Append(" sqlentity.view=\"").Append(sqlEntity.View).Append("\"");
	    sb.Append(" />");

	    return sb.ToString();
	}

	public static EntityElement FindEntityBySqlEntity(ArrayList entities, String name) {
	    foreach (EntityElement entity in entities) {
		if (entity.SqlEntity.Name.ToLower().Equals(name.ToLower())) {
		    return entity;
		}
	    }
	    return null;
	}

	public static EntityElement FindEntityByName(ArrayList entities, String name) {
	    foreach (EntityElement entity in entities) {
		if (entity.Name.Equals(name)) {
		    return entity;
		}
	    }
	    return null;
	}

	public Boolean SingleDataMap {
	    get { return DataMaps.Count == 1; }
	}

	public String ViewName {
	    get {
		if (SingleDataMap) {
		    DataMapElement dataMap = (DataMapElement)DataMaps[0];
		    return dataMap.SqlEntity.View;
		} else {
		    return String.Empty;
		}
	    }
	}

	public String ConnectionStringKey {
	    get {
		if (SingleDataMap) {
		    DataMapElement dataMap = (DataMapElement)DataMaps[0];
		    return dataMap.SqlEntity.Key;
		} else {
		    return String.Empty;
		}
	    }
	}

	public Int32 CommandTimeout {
	    get {
		if (SingleDataMap) {
		    DataMapElement dataMap = (DataMapElement)dataMaps[0];
		    return dataMap.SqlEntity.CommandTimeout;
		} else {
		    return 0;
		}
	    }
	}

	public ColumnMapElement GetIdentityColumnMap() {
	    foreach (DataMapElement dataMap in dataMaps) {
		foreach (ColumnMapElement columnMap in dataMap.ColumnMaps) {
		    if (columnMap.Column.Identity) {
			return columnMap;
		    }
		}
	    }
	    return null;
	}

	public PropertyElement GetIdentityProperty() {
	    ColumnMapElement columnMap = GetIdentityColumnMap();
	    return columnMap == null ? null : columnMap.Property;
	}

	public PropertyElement GetIdentityField() {
	    foreach (PropertyElement property in properties) {
		if (property.Column.Identity && !property.Column.ViewColumn) {
		    return property;
		}
	    }
	    return null;
	}

	public PropertyElement FindFieldByName(String name) {
	    foreach (PropertyElement field in properties) {
		if (field.Name == name) {
		    return field;
		}
	    }
	    return null;
	}

	public PropertyElement FindProperty(String name) {
	    Int32 dot = name.IndexOf('.');
	    if (dot < 0) {
		foreach (PropertyElement property in this.Properties) {
		    if (property.Name.Equals(name)) {
			return property;
		    }
		}
	    } else {
		String propertyName = name.Substring(0, dot);
		foreach (PropertyElement property in this.Properties) {
		    if (property.Name.Equals(propertyName)) {
			return property.EntityType.FindProperty(name.Substring(dot + 1));
		    }
		}
	    }
	    return null;
	}

	public PropertyElement FindFieldByColumnName(String name) {
	    foreach (PropertyElement property in properties) {
		if (property.Column.Name.ToLower().Equals(name.ToLower())) {
		    return property;
		}
	    }
	    return null;
	}

	public ColumnMapElement FindColumnMap(String name) {
	    foreach (DataMapElement dataMap in this.dataMaps) {
		foreach (ColumnMapElement columnMap in dataMap.ColumnMaps) {
		    if (columnMap.Name.Equals(name)) {
			return columnMap;
		    }
		}
	    }
	    return null;
	}

	public IList GetPrimaryKeyColumnMaps() {
	    ArrayList list = new ArrayList();
	    ColumnMapElement identity = GetIdentityColumnMap();
	    if (identity != null) {
		list.Add(identity);
	    } else {
		foreach (DataMapElement dataMap in dataMaps) {
		    foreach (ColumnMapElement columnMap in dataMap.ColumnMaps) {
			if (dataMap.SqlEntity.IsPrimaryKeyColumn(columnMap.Column.Name)) {
			    list.Add(columnMap);
			}
		    }
		}
	    }
	    return list;
	}

	public IList GetPrimaryKeyProperties() {
	    ArrayList list = new ArrayList();
	    foreach (ColumnMapElement columnMap in GetPrimaryKeyColumnMaps()) {
		list.Add(columnMap.Property);
	    }
	    return list;
	}

	public IList GetPrimaryKeyFields() {
	    ArrayList list = new ArrayList();
	    PropertyElement id = GetIdentityField();
	    if (id != null) {
		list.Add(id);
	    } else {
		foreach (PropertyElement field in properties) {
		    if (sqlEntity.IsPrimaryKeyColumn(field.Column.Name)) {
			list.Add(field);
		    }
		}
	    }
	    return list;
	}

	public FinderElement FindFinderByName(String name) {
	    foreach (FinderElement finder in finders) {
		if (finder.Name == name) {
		    return finder;
		}
	    }
	    return null;
	}

	/// <summary>
	/// top level method - so that it can be called from Velocity templates
	/// </summary>
	/// <returns></returns>
	public ArrayList GetPropertyNames(ConfigurationElement options, IList entities) {
	    return GetPropertyNames(options, entities, this, "", new Stack());
	}

	/// <summary>
	/// Returns a list of all properties in the class.  Note that it will create 
	/// a DOGenerator class for each DataObject entity that it finds as a property and
	/// will call that class to get it's entities.
	/// </summary>
	/// <param name="namePrefix">Prefix to add to the name representing parent entities.</param>
	/// <param name="entities">List of all entities in system.  Used to determine if property is a DataObject entity type</param>
	/// <param name="parentEntities">Stack of all parent entities.  Used to avoid loops.</param>
	/// <returns></returns>
	public static ArrayList GetPropertyNames(ConfigurationElement options, IList entities, EntityElement entity, String prefix, System.Collections.Stack parentEntities) {
	    ArrayList propertyNames = new ArrayList();

	    // avoid loops
	    bool matchedParent = false;
	    foreach (EntityElement searchEntity in parentEntities) {
		if (ReferenceEquals(searchEntity, entity)) {
		    matchedParent = true;
		    break;
		}
	    }

	    // Add current entity to parent stack
	    parentEntities.Push(entity);

	    if (!matchedParent) {
		foreach (PropertyElement property in entity.Properties) {
		    if (property.Name.IndexOf(".") < 0) {
			PropertyName propertyName = new PropertyName(prefix, property.Name);
			propertyNames.Add(propertyName);

			// Determine if this is a data object and if so append it's members.
			foreach (EntityElement subEntity in entities) {
			    if (property.Type != null && property.Type.Name.Equals(options.GetDOClassName(subEntity.Name)) || ReferenceEquals(subEntity, property.EntityType)) {
				ArrayList subProperties = GetPropertyNames(options, entities, subEntity, PropertyName.AppendPrefix(prefix, property.Name), parentEntities);
				foreach (PropertyName subProperty in subProperties) {
				    propertyNames.Add(subProperty);
				}
				break;
			    }
			} // end of loop through entities
		    }// end of if on indexOf(".")
		} // end of loop through fields.
	    } // end of matched parent check

	    parentEntities.Pop();
	    return propertyNames;
	} // end of GetPropertyNames

	/// <summary>
	/// Handles creation of the static readonly property names and values.  Creates
	/// entries of the form:
	///	public static readonly string PROPERTY_NAME="Property.Name";
	/// </summary>
	public class PropertyName {
	    public String fieldName = "";
	    public String fieldValue = "";

	    private static readonly String PREFIX_SEPARATOR = "~";

	    public String FieldName {
		get { return this.fieldName; }
	    }

	    public String FieldValue {
		get { return this.fieldValue; }
	    }

	    /// <summary>
	    /// Constructor
	    /// </summary>
	    /// <param name="prefix">Prefix for the property name/value.  Prefix separates classes with ~.  Like Property~Name</param>
	    /// <param name="propertyName">Property name</param>
	    public PropertyName(String prefix, String propertyName) {
		fieldName = prefix.Replace(PREFIX_SEPARATOR,"_").ToUpper() + "_" + propertyName.ToUpper();
		fieldName = fieldName.StartsWith("_") ? fieldName.Substring(1) : fieldName;

		if (prefix.Length > 0) {
		    fieldValue = "\"" + prefix.Replace(PREFIX_SEPARATOR,".")
			+ "." +  propertyName + "\"";
		}
		else {
		    fieldValue = "\"" + propertyName + "\"";
		}
	    }

	    /// <summary>
	    /// Appends a property name to the current prefix so the new prefix
	    /// can be used for creating names for it's members.
	    /// </summary>
	    /// <param name="prefix">Old prefix in the form Property~Property...</param>
	    /// <param name="propertyName">New property to append</param>
	    /// <returns>New prefix in the same format.</returns>
	    public static String AppendPrefix(String prefix, String propertyName) {
		if (prefix.Length > 0) {
		    return prefix + PREFIX_SEPARATOR + propertyName;
		}
		else {
		    return propertyName;
		}
	    }
	}
    }
}
