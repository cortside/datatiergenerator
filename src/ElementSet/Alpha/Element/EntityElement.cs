using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class EntityElement : ElementSkeleton, IPropertyContainer, ICollectable {

	private static readonly String PROPERTIES = "properties";
	private static readonly String FINDERS = "finders";
	private static readonly String COMPARERS = "comparers";
	private static readonly String SQLENTITY = "sqlentity";
	private static readonly String BASE_ENTITY = "baseentity";
	private static readonly String ABSTRACT = "abstract";
	private static readonly String LOG = "log";
	private static readonly String RETURNWHOLEOBJECT = "returnwholeobject";
	private static readonly String PREPAREFORINSERT = "prepareforinsert";

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

	public static readonly EntityElement EMPTY = new EntityElement();

	static EntityElement() {
	    EMPTY = new EntityElement();
	    EMPTY.BaseEntity = EMPTY;
	}

	private SqlEntityElement sqlEntity = new SqlEntityElement();
	private ArrayList fields = new ArrayList();
	private ArrayList finders = new ArrayList();
	private ArrayList comparers = new ArrayList();
	private EntityElement baseEntity = EntityElement.EMPTY;
	private Boolean isAbstract = false;
	private Boolean doLog = false;
	private Boolean returnWholeObject = false;
	private Boolean prepareForInsert = false;

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

	public Boolean DoLog {
	    get { return this.doLog; }
	    set { this.doLog = value; }
	}
        
	public Boolean ReturnWholeObject {
	    get { return this.returnWholeObject; }
	    set { this.returnWholeObject = value; }
	}

	public Boolean PrepareForInsert {
	    get { return this.prepareForInsert; }
	    set { this.prepareForInsert = value; }
	}

	public ArrayList Fields {
	    get { return this.fields; }
	    set { this.fields = value; }
	}

	public ArrayList Finders {
	    get { return this.finders; }
	    set { this.finders = value; }
	}

	public ArrayList Comparers {
	    get { return this.comparers; }
	    set { this.comparers = value; }
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

	public static EntityElement FindEntityByName(IList entities, String name) {
	    foreach (EntityElement entity in entities) {
		if (entity.Name.Equals(name)) {
		    return entity;
		}
	    }
	    return null;
	}

	/// <summary>
	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
	/// list.
	/// </summary>
	/// <param name="node"></param>
	/// <param name="entityElements"></param>
	public static void ParseFromXml(XmlNode node, IList entityElements) {

	    if (node != null && entityElements != null) {

		foreach (XmlNode entityNode in node.ChildNodes) {
		    if (entityNode.NodeType.Equals(XmlNodeType.Element)) {
			EntityElement entityElement = new EntityElement();

			entityElement.Name = GetAttributeValue(entityNode, NAME, entityElement.Name);
			entityElement.BaseEntity.Name = GetAttributeValue(entityNode, BASE_ENTITY, entityElement.BaseEntity.Name);
			entityElement.SqlEntity.Name = GetAttributeValue(entityNode, SQLENTITY, entityElement.SqlEntity.Name);
			entityElement.IsAbstract = Boolean.Parse(GetAttributeValue(entityNode, ABSTRACT, entityElement.IsAbstract.ToString()));
			entityElement.DoLog = Boolean.Parse (GetAttributeValue (entityNode, LOG, entityElement.DoLog.ToString ()));
			entityElement.ReturnWholeObject = Boolean.Parse (GetAttributeValue (entityNode, RETURNWHOLEOBJECT, entityElement.ReturnWholeObject.ToString ()));
			entityElement.PrepareForInsert = Boolean.Parse (GetAttributeValue (entityNode, PREPAREFORINSERT, entityElement.PrepareForInsert.ToString ()));

			PropertyElement.ParseFromXml(GetChildNodeByName(entityNode, PROPERTIES), entityElement.Fields);
			FinderElement.ParseFromXml(GetChildNodeByName(entityNode, FINDERS), entityElement.Finders);
			ComparerElement.ParseFromXml(GetChildNodeByName(entityNode, COMPARERS), entityElement.Comparers);
			entityElements.Add(entityElement);
		    }
		}
	    }
	}

	public static ArrayList ParseFromXml(Configuration options, XmlDocument doc, Hashtable sqltypes, Hashtable types, ArrayList sqlentities, ParserValidationDelegate vd) {
	    ArrayList entities = new ArrayList();
	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("entity");
	    foreach (XmlNode node in elements) {
		if (node.NodeType == XmlNodeType.Comment) {
		    continue;
		}
		EntityElement entity = new EntityElement();
		entity.Name = node.Attributes["name"].Value;
		if (node.Attributes["sqlentity"] != null) {
		    SqlEntityElement sqlentity = SqlEntityElement.FindByName(sqlentities, node.Attributes["sqlentity"].Value);
		    if (sqlentity!=null) {
			entity.SqlEntity = (SqlEntityElement)sqlentity.Clone();
		    } else {
			entity.SqlEntity.Name = node.Attributes["sqlentity"].Value;
			vd(ParserValidationArgs.NewError("sqlentity (" + entity.SqlEntity.Name + ") specified in entity " + entity.Name + " could not be found as an defined sql entity"));
		    }
		}

		if (node.Attributes["baseentity"] != null) {
		    EntityElement baseentity = EntityElement.FindEntityByName(entities, node.Attributes["baseentity"].Value);
		    if (baseentity!=null) {
			entity.BaseEntity = (EntityElement)baseentity.Clone();
		    } else {
			entity.BaseEntity.Name = node.Attributes["baseentity"].Value;
			vd(ParserValidationArgs.NewError("baseentity (" + entity.BaseEntity.Name + ") specified in entity " + entity.Name + " could not be found as an defined entity (or is defined after this entity in the config file)"));
		    }
		}

		if (node.Attributes["abstract"] != null) {
		    entity.IsAbstract = Boolean.Parse(node.Attributes["abstract"].Value);
		}

		if (node.Attributes["log"] != null) {
		    entity.DoLog = Boolean.Parse (node.Attributes["log"].Value);
		}

		if (node.Attributes["returnwholeobject"] != null) {
		    entity.ReturnWholeObject = Boolean.Parse (node.Attributes["returnwholeobject"].Value);
		}

		if (node.Attributes["prepareforinsert"] != null) {
		    entity.PrepareForInsert = Boolean.Parse (node.Attributes["prepareforinsert"].Value);
		}

		entity.Fields = PropertyElement.ParseFromXml(doc, entities, entity, sqltypes, types, vd);
		entity.Finders = FinderElement.ParseFromXml(doc, node, entities, entity, sqltypes, types, vd);
		entity.Comparers = ComparerElement.ParseFromXml(node, entities, entity, sqltypes, types, vd);
		entities.Add(entity);
	    }
	    return entities;
	}

	public PropertyElement GetIdentityField() {
	    foreach (PropertyElement field in fields) {
		if (field.Column.Identity && !field.Column.ViewColumn) {
		    return field;
		}
	    }
	    return null;
	}

	public PropertyElement GetInsertReturnField() {
	    foreach (PropertyElement field in fields) {
		if (field.Column.Identity || field.ReturnAsIdentity) {
		    return field;
		}
	    }
	    return null;
	}

	public PropertyElement FindFieldByName(String name) {
	    foreach (PropertyElement field in fields) {
		if (field.Name == name) {
		    return field;
		}
	    }
	    return null;
	}


	public PropertyElement FindFieldByColumnName(String name) {
	    foreach (PropertyElement field in fields) {
		if (field.Column.Name.ToLower().Equals(name.ToLower())) {
		    return field;
		}
	    }
	    return null;
	}


	public IList GetPrimaryKeyFields() {
	    ArrayList list = new ArrayList();
	    PropertyElement id = GetIdentityField();
	    if (id != null) {
		list.Add(id);
	    } else {
		foreach (PropertyElement field in fields) {
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

	public ComparerElement FindComparerByName(String name)	{
	    foreach (ComparerElement comparer in comparers)	    {
		if (comparer.Name == name)		{
		    return comparer;
		}
	    }
	    return null;
	}

	/// <summary>
	/// top level method - so that it can be called from Velocity templates
	/// </summary>
	/// <returns></returns>
	public ArrayList GetPropertyNames(Configuration options, IList entities) {
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
	public static ArrayList GetPropertyNames(Configuration options, IList entities, EntityElement entity, String prefix, System.Collections.Stack parentEntities) {
	    ArrayList propertyNames = new ArrayList();

	    // avoid loops
	    bool matchedParent = false;
	    foreach (EntityElement searchEntity in parentEntities) {
		if (searchEntity.Equals(entity)) {
		    matchedParent = true;
		    break;
		}
	    }

	    // Add current entity to parent stack
	    parentEntities.Push(entity);

	    if (!matchedParent) {
		foreach (PropertyElement field in entity.Fields) {
		    if (field.Name.IndexOf(".") < 0) {
			PropertyName propertyName = new PropertyName(prefix, field.Name);
			propertyNames.Add(propertyName);

			// Determine if this is a data object and if so append it's members.
			foreach (EntityElement subEntity in entities) {
			    if (field.Type.Name.Equals(options.GetDOClassName(subEntity.Name))) {
				ArrayList subProperties = GetPropertyNames(options, entities, subEntity, 
				    PropertyName.AppendPrefix(prefix, field.Name), 
				    parentEntities);
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

	public override Boolean IsEmpty() {
	    return this == EMPTY;
	}
    }
}