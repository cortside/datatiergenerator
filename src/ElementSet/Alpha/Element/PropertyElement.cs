using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class PropertyElement : ElementSkeleton {

	protected static readonly String PROPERTY="property";

	protected static readonly String TYPE = "type";
	protected static readonly String COLUMN = "column";
	protected static readonly String CONCRETE_TYPE = "concretetype";
	protected static readonly String CONVERT_FROM_SQLTYPE_FORMAT = "convertfromsqltypeformat";
	protected static readonly String CONVERT_TO_SQLTYPE_FORMAT = "converttosqltypeformat";
	protected static readonly String ENTITY = "entity";
	protected static readonly String PREFIX = "prefix";
	protected static readonly String PARAMETER_NAME = "parametername";
	protected static readonly String EXPRESSION = "expression";
	protected static readonly String GROUP_FUNCTION = "groupfunction";
	protected static readonly String GROUP_BY = "groupby";
	protected static readonly String SQL_TYPE = "sqltype";
	private static readonly String ALIAS = "alias";
	protected static readonly String READABLE = "readable";
	protected static readonly String WRITABLE = "writable";
	protected static readonly String DERIVED = "derived";
	protected static readonly String LOG = "log";
	protected static readonly String RETURNASIDENTITY = "returnasidentity";
	protected static readonly String DIRECTION = "direction";
	protected static readonly String CONVERT_FOR_COMPARE = "convertforcompare";

	protected static readonly string ASCENDING = "ascending";
	protected static readonly string DESCENDING = "descending";

	public static readonly String GROUP_FUNCTION_SUM = "sum";
	public static readonly String GROUP_FUNCTION_MIN = "min";
	public static readonly String GROUP_FUNCTION_MAX = "max";
	public static readonly String GROUP_FUNCTION_AVG = "avg";
	public static readonly String GROUP_FUNCTION_STDEV = "stdev";
	public static readonly String GROUP_FUNCTION_STDEVP = "stdevp";
	public static readonly String GROUP_FUNCTION_VAR = "var";
	public static readonly String GROUP_FUNCTION_VARP = "varp";

	protected TypeElement type = new TypeElement();
	protected ColumnElement column = new ColumnElement();
	protected TypeElement concreteType = new TypeElement();
	protected TypeElement dataObjectType = new TypeElement();
	protected String convertFromSqlTypeFormat = String.Empty;
	protected String convertToSqlTypeFormat = String.Empty;
	protected TypeElement entity = new TypeElement();
	protected String prefix = String.Empty;
	protected Boolean readable = true;
	protected Boolean writable = true;
	protected Boolean derived = false;

	protected String accessModifier = "private";

	protected String parameterName = String.Empty;
	protected String expression = String.Empty;
	protected String groupFunction = String.Empty;
	protected bool groupBy = false;
	private String alias = String.Empty;
	protected Boolean doLog = false;
	protected Boolean returnAsIdentity = false;
	protected String direction = String.Empty;
	private IPropertyContainer container = new EntityElement();

	public TypeElement Type {
	    get { return this.type; }
	    set { this.type = value; }
	}

	public ColumnElement Column {
	    get { return this.column; }
	    set { this.column = value; }
	}

	public TypeElement ConcreteType {
	    get { return this.concreteType; }
	    set { this.concreteType = value; }
	}

	public TypeElement DataObjectType {
	    get { return this.dataObjectType; }
	    set { this.dataObjectType = value; }
	}

	public String ConvertFromSqlTypeFormat {
	    get { return this.convertFromSqlTypeFormat; }
	    set { this.convertFromSqlTypeFormat = value; }
	}

	public String ConvertToSqlTypeFormat {
	    get { return this.convertToSqlTypeFormat; }
	    set { this.convertToSqlTypeFormat = value; }
	}

	public TypeElement Entity {
	    get { return this.entity; }
	    set { this.entity = value; }
	}

	public String Prefix {
	    get { return this.prefix; }
	    set { this.prefix = value; }
	}

	public Boolean Readable {
	    get { return this.readable; }
	    set { this.readable = value; }
	}

	public Boolean Writable {
	    get { return this.writable  && !this.Derived; }
	    set { this.writable = value; }
	}

	public Boolean Derived {
	    get { return this.derived; }
	    set { this.derived = value; }
	}

	public String ParameterName {
	    get { return this.parameterName; }
	    set { this.parameterName = value; }
	}
	
	public String AccessModifier {
	    get { return this.accessModifier; }
	    set { this.accessModifier = value; }
	}

	/// <summary>
	/// Expression to use for computation.
	/// </summary>
	public String Expression {
	    get { return this.expression; }
	    set { this.expression = value; }
	}

	/// <summary>
	/// Function to use when grouping is required.
	/// </summary>
	public String GroupFunction {
	    get { return this.groupFunction; }
	    set { this.groupFunction = value; }
	}

	/// <summary>
	/// Indicates if this property is to be used for grouping.
	/// </summary>
	public bool GroupBy {
	    get { return this.groupBy; }
	    set { this.groupBy = value; }
	}

	/// <summary>
	/// Alias to use as the SQL Name in the query.
	/// </summary>
	public string  Alias {
	    get { return this.alias; }
	    set { this.alias = value; }
	}

	public Boolean DoLog {
	    get { return this.doLog; }
	    set { this.doLog = value; }
	}

	public Boolean ReturnAsIdentity {
	    get { return this.returnAsIdentity; }
	    set { this.returnAsIdentity = value; }
	}

	public String Direction {
	    get { return this.direction; }
	    set { this.direction = value; }
	}
	
	public String GetFieldFormat() {
	    return this.GetPropertyName().Substring(0, 1).ToLower() + this.GetPropertyName().Substring(1).Replace('.', '_');
	}

	public virtual String GetMethodFormat() {
	    return this.GetPropertyName().Substring(0, 1).ToUpper() + this.GetPropertyName().Substring(1);
	}

	/// <summary>
	/// Creates a string for a method parameter representing the specified field.
	/// </summary>
	/// <returns>String containing parameter information of the specified field for a method call.</returns>
	public String CreateMethodParameter() {
	    return type.Name + " " + GetFieldFormat();
	}

	/// <summary>
	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
	/// list.
	/// </summary>
	/// <param name="node"></param>
	/// <param name="entityElements"></param>
	public static void ParseFromXml(XmlNode node, IList propertyElements) {

	    if (node != null && propertyElements != null) {

		foreach (XmlNode propertyNode in node.ChildNodes) {
		    if (propertyNode.NodeType.Equals(XmlNodeType.Element)) {
			PropertyElement propertyElement = new PropertyElement();
			BuildElement(propertyElement, propertyNode);
			propertyElements.Add(propertyElement);
		    }
		}
	    }
	}

	public static void BuildElement(PropertyElement propertyElement, XmlNode propertyNode) {
	    propertyElement.Name = GetAttributeValue(propertyNode, NAME, propertyElement.Name);
	    propertyElement.Type.Name = GetAttributeValue(propertyNode, TYPE, propertyElement.Type.Name);
	    propertyElement.Column.Name = GetAttributeValue(propertyNode, COLUMN, propertyElement.Column.Name);
	    propertyElement.ConcreteType.Name = GetAttributeValue(propertyNode, CONCRETE_TYPE, propertyElement.ConcreteType.Name);
	    propertyElement.ConvertFromSqlTypeFormat = GetAttributeValue(propertyNode, CONVERT_FROM_SQLTYPE_FORMAT, propertyElement.ConvertFromSqlTypeFormat);
	    propertyElement.ConvertToSqlTypeFormat = GetAttributeValue(propertyNode, CONVERT_TO_SQLTYPE_FORMAT, propertyElement.ConvertToSqlTypeFormat);
	    propertyElement.Entity.Name = GetAttributeValue(propertyNode, ENTITY, propertyElement.Entity.Name);
	    propertyElement.Prefix = GetAttributeValue(propertyNode, PREFIX, propertyElement.Prefix);
	    propertyElement.ParameterName = GetAttributeValue(propertyNode, PARAMETER_NAME, propertyElement.ParameterName);
	    propertyElement.Expression = GetAttributeValue(propertyNode, EXPRESSION, propertyElement.Expression);
	    propertyElement.GroupFunction = GetAttributeValue(propertyNode, GROUP_FUNCTION, propertyElement.GroupFunction);
	    propertyElement.GroupBy = Boolean.Parse(GetAttributeValue(propertyNode, GROUP_BY, propertyElement.GroupBy.ToString()));
	    propertyElement.Column.SqlType.Name = GetAttributeValue(propertyNode, SQL_TYPE, propertyElement.Column.SqlType.Name);
	    propertyElement.Alias = GetAttributeValue(propertyNode, ALIAS, propertyElement.Alias);
	    propertyElement.DoLog = Boolean.Parse (GetAttributeValue (propertyNode, LOG, propertyElement.DoLog.ToString ()));
	    propertyElement.ReturnAsIdentity = Boolean.Parse (GetAttributeValue (propertyNode, RETURNASIDENTITY, propertyElement.ReturnAsIdentity.ToString ()));
	    propertyElement.Readable = Boolean.Parse(GetAttributeValue(propertyNode, READABLE, propertyElement.Readable.ToString()));
	    propertyElement.Writable = Boolean.Parse(GetAttributeValue(propertyNode, WRITABLE, propertyElement.Writable.ToString()));
	    propertyElement.Derived = Boolean.Parse(GetAttributeValue(propertyNode, DERIVED, propertyElement.Derived.ToString()));
	    propertyElement.Direction = GetAttributeValue (propertyNode, DIRECTION, propertyElement.Direction.ToString ());
	    propertyElement.Type.ConvertForCompare = GetAttributeValue (propertyNode, CONVERT_FOR_COMPARE, propertyElement.Type.ConvertForCompare);
	}

	public static ArrayList ParseFromXml(XmlDocument doc, IList entities, EntityElement entity, Hashtable sqltypes, Hashtable types, ParserValidationDelegate vd) {
	    return ParseFromXml(doc, entities, entity, sqltypes, types, false, vd);
	}

	public static ArrayList ParseFromXml(XmlDocument doc, IList entities, EntityElement entity, Hashtable sqltypes, Hashtable types, bool isReference, ParserValidationDelegate vd) {
	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("entity");
	    foreach (XmlNode element in elements) {
		String name = element.Attributes["name"].Value;
		String sqlEntity = (element.Attributes["sqlentity"] == null) ? String.Empty : element.Attributes["sqlentity"].Value;
//		if ((sqlEntity.Equals(entity.SqlEntity.Name) || (entity.SqlEntity.Name.Length == 0 && name == entity.Name)) && element.HasChildNodes) {
		if (name.Equals(entity.Name) && element.HasChildNodes) {
		    // look for a properties element, if one does not exist, assume that everything under the entity is a property (for backward compatablility)
		    XmlNode propertiesNode = element;
		    Boolean hasProperties = false;
		    foreach(XmlNode node in element.ChildNodes) {
			if (node.Name.Equals("properties")) {
			    propertiesNode = node;
			    hasProperties = true;
			}
		    }
		    if (!hasProperties) {
			vd(ParserValidationArgs.NewError("<property> elements on entity (" + entity.Name + ") should be defined within a <properties> element."));
		    }

		    return ParseFromXml(propertiesNode, entities, entity, sqltypes, types, isReference, vd);
		}
	    }

	    return new ArrayList();
	}

	public static ArrayList ParseFromXml(XmlNode propertiesNode, IList entities, IPropertyContainer entity, Hashtable sqltypes, Hashtable types, bool isReference, ParserValidationDelegate vd) {
	    ArrayList fields = new ArrayList();
	    foreach (XmlNode node in propertiesNode.ChildNodes) {
		if (node.NodeType == XmlNodeType.Comment) {
		    continue;
		}
		PropertyElement field = BuildElement(node, types, sqltypes, entity, isReference, vd);

		// Add in any subfields...
		if (node.Attributes["entity"]!=null) {
		    EntityElement subentity = EntityElement.FindEntityByName((ArrayList)entities, node.Attributes["entity"].Value);
		    if (subentity != null) {
			// Only entity elements have entity atttribute
			SqlEntityElement sqlEntity = ((EntityElement)entity).SqlEntity;

			String prefix = subentity.Name + "_";
			if (node.Attributes["prefix"]!=null) {
			    prefix = node.Attributes["prefix"].Value;
			}
				    
			foreach(PropertyElement f in subentity.Fields) {
			    PropertyElement subfield = (PropertyElement)f.Clone();
			    subfield.Name = field.Name + "." + subfield.Name;

			    // if field has sql column defined
			    if (!f.Column.Name.Equals(String.Empty)) {
				ColumnElement column = sqlEntity.FindColumnByName(prefix + subfield.Column.Name);
				if (column != null) {
				    subfield.Column = (ColumnElement)column.Clone();
				} 
				else {
				    vd(ParserValidationArgs.NewError("column (" + prefix + subfield.Column.Name + ") specified for property (" + subfield.Name + ") on entity (" + entity.Name + ") was not found in sql entity (" + sqlEntity.Name + ")"));
				}
			    }
			    fields.Add(subfield);
			}
		    } 
		    else {
			vd(ParserValidationArgs.NewError("Entity " + entity.Name + " referenced another entity that was not defined (or defined below this one): " + node.Attributes["entity"].Value));
		    }
		} 

		fields.Add(field);
			    
	    }
	    return fields;
	}

	public static PropertyElement BuildElement(XmlNode node, Hashtable types, Hashtable sqltypes, IPropertyContainer entity, bool isReference, ParserValidationDelegate vd) {
	    PropertyElement field = new PropertyElement();

	    if (node.Attributes["name"] != null) {
		field.Name = node.Attributes["name"].Value;
	    }
	    else {
		vd(ParserValidationArgs.NewError("Property in " + entity.Name + " has no name."));
	    }

	    if (isReference && field.Name != "*") {
		PropertyElement refProperty = entity.FindFieldByName(field.Name);
		if (refProperty == null) {
		    vd(ParserValidationArgs.NewError("Property " + field.Name + " in " + entity.Name + " refers to a property that does not exist."));
		}
		else {
		    field = (PropertyElement)(refProperty.Clone());
		}
	    }

	    if (node.Attributes["column"] != null) {
		if (node.Attributes["column"].Value.Equals("*")) {
		    field.Column.Name = field.Name;
		} 
		else {
		    field.Column.Name = node.Attributes["column"].Value;
		}

		// Column only occurs on entity eement.
		SqlEntityElement sqlEntity = ((EntityElement)entity).SqlEntity;
		ColumnElement column = sqlEntity.FindColumnByName(field.Column.Name);
		if (column!= null) {
		    field.Column = (ColumnElement)column.Clone();
		    if (types.Contains(field.Column.SqlType.Type)) {
			field.Type = (TypeElement)((TypeElement)types[field.Column.SqlType.Type]).Clone();
		    } 
		    else {
			vd(ParserValidationArgs.NewError("Type " + field.Column.SqlType.Type + " was not defined [property=" + field.name + "]"));
		    }

		} 
		else {
		    vd(ParserValidationArgs.NewError("column (" + field.Column.Name + ") specified for property (" + field.Name + ") on entity (" + entity.Name + ") was not found in sql entity (" + sqlEntity.Name + ")"));
		}
	    }

	    field.Description = node.InnerText.Trim();

	    if (node.Attributes["accessmodifier"] != null) {
		field.AccessModifier = node.Attributes["accessmodifier"].Value;
	    }

	    // the concrete type is the *real* type, type can be the same or can be in interface or coersable type
	    if (node.Attributes["type"] != null) {
		String type = node.Attributes[TYPE].Value;
		String concreteType = type;
		if (node.Attributes[CONCRETE_TYPE] != null) {
		    concreteType = node.Attributes[CONCRETE_TYPE].Value;
		}
		// if the data type is defined, default it as the property and left be overridden
		if (types.Contains(concreteType)) {
		    field.Type = (TypeElement)((TypeElement)types[concreteType]).Clone();
		    field.Type.Name = type;
		} 
		else {
		    vd(ParserValidationArgs.NewError("Type " + concreteType + " was not defined for property " + field.Name + " in " + entity.Name + "."));
		}

		String dataObjectTypeName = concreteType + "Data";
		if (types.Contains(dataObjectTypeName)) {
		    field.DataObjectType = (TypeElement)((TypeElement)types[dataObjectTypeName]).Clone();
		} else {
		    field.DataObjectType = field.Type;
		}
	    }

	    if (node.Attributes["convertfromsqltypeformat"] != null) {
		field.Type.ConvertFromSqlTypeFormat = node.Attributes["convertfromsqltypeformat"].Value;
	    }
	    if (node.Attributes["converttosqltypeformat"] != null) {
		field.Type.ConvertToSqlTypeFormat = node.Attributes["converttosqltypeformat"].Value;
	    }
	    if (node.Attributes[PARAMETER_NAME] != null) {
		field.ParameterName = node.Attributes[PARAMETER_NAME].Value;
	    }
	    if (node.Attributes[EXPRESSION] != null) {
		field.Expression = node.Attributes[EXPRESSION].Value;
	    }
	    if (node.Attributes[GROUP_FUNCTION] != null) {
		field.GroupFunction = node.Attributes[GROUP_FUNCTION].Value;
		if (field.GroupFunction.ToLower() != GROUP_FUNCTION_SUM
		    && field.GroupFunction.ToLower() != GROUP_FUNCTION_MIN
		    && field.GroupFunction.ToLower() != GROUP_FUNCTION_MAX
		    && field.GroupFunction.ToLower() != GROUP_FUNCTION_AVG
		    && field.GroupFunction.ToLower() != GROUP_FUNCTION_STDEV
		    && field.GroupFunction.ToLower() != GROUP_FUNCTION_STDEVP
		    && field.GroupFunction.ToLower() != GROUP_FUNCTION_VAR
		    && field.GroupFunction.ToLower() != GROUP_FUNCTION_VARP)
		    vd(ParserValidationArgs.NewError("Invalid group function specified for entity reference property " + field.Name + " in " + entity.Name));
	    }

	    if (node.Attributes[GROUP_BY] != null) {
		field.GroupBy = Boolean.Parse(node.Attributes[GROUP_BY].Value);
	    }
	    if (node.Attributes[SQL_TYPE] != null) {
		field.Column.SqlType.Name = node.Attributes[SQL_TYPE].Value;
		if (sqltypes.ContainsKey(field.Column.SqlType.Name)) {
		    field.Column.SqlType = (SqlTypeElement)((SqlTypeElement)sqltypes[field.Column.SqlType.Name]).Clone();
		} 
		else {
		    vd(ParserValidationArgs.NewError("SqlType " + field.Column.SqlType.Name + " was not defined in " + entity.Name + " for property " + field.Name + "."));
		}
	    }
	    if (node.Attributes[ALIAS] != null) {
		field.Alias = node.Attributes[ALIAS].Value;
	    }

	    field.container = entity;

	    if (node.Attributes["readable"] != null) {
		field.Readable = Boolean.Parse(node.Attributes["readable"].Value);
	    }
	    if (node.Attributes["writable"] != null) {
		field.Writable = Boolean.Parse(node.Attributes["writable"].Value);
	    }
	    if (node.Attributes["derived"] != null) {
		field.Derived = Boolean.Parse(node.Attributes["derived"].Value);
	    }

	    if (node.Attributes["log"] != null) {
		field.DoLog = Boolean.Parse (node.Attributes["log"].Value);
	    }

	    if (node.Attributes["returnasidentity"] != null) {
		field.ReturnAsIdentity = Boolean.Parse (node.Attributes["returnasidentity"].Value);
	    }

	    if (node.Attributes[DIRECTION] == null) {
		field.Direction = ASCENDING;
	    }
	    else if (node.Attributes["direction"].Value != ASCENDING && node.Attributes["direction"].Value != DESCENDING) {
		vd(ParserValidationArgs.NewError("Comparer in entity " + entity.Name + " has direction value other than 'ascending' or 'descending'"));
	    }
	    else {
		field.Direction = node.Attributes[DIRECTION].Value;
	    }
		    
	    if (node.Attributes[CONVERT_FOR_COMPARE] != null) {
		field.Type.ConvertForCompare = node.Attributes[CONVERT_FOR_COMPARE].Value;
	    }

	    return field;
	}

	/// <summary>
	/// Creates a string for a SqlParameter representing the specified field.
	/// </summary>
	/// <param name="this">Object that stores the information for the field the parameter represents.</param>
	/// <returns>String containing SqlParameter information of the specified field for a method call.</returns>
	public String CreateSqlParameter(Boolean blnOutput, Boolean useDataObject) {
	    StringBuilder sb = new StringBuilder();
	    sb.Append("cmd.Parameters.Add(new SqlParameter(\"@" + GetSqlAlias() + "\", SqlDbType." + GetSqlType().SqlDbType + ", " + GetSqlType().Length + ", ParameterDirection.");
	    if (blnOutput) {
		sb.Append("Output"); 
	    } else {
		sb.Append("Input");
	    }
	    sb.Append(", false, " + column.SqlType.Precision + ", " + column.SqlType.Scale + ", \"" + name + "\", DataRowVersion.Proposed, ");
	    if (useDataObject) {
		sb.Append(String.Format(type.ConvertToSqlTypeFormat, "data", "data."+GetMethodFormat(), "", "", GetMethodFormat()));
	    } else {
		sb.Append(String.Format(type.ConvertToSqlTypeFormat, "", GetFieldFormat(), "", "", GetFieldFormat()));
	    }
	    sb.Append("));" + Environment.NewLine);
	    return sb.ToString();
	}

	public String CreateDataParameter(Boolean blnOutput, Boolean useDataObject) {
	    StringBuilder buffer = new StringBuilder();
	    buffer.Append("cmd.Parameters.Add(CreateDataParameter(\"@" + GetSqlAlias() + "\", ParameterDirection.");
	    buffer.Append(blnOutput ? "Output, " : "Input, ");
	    if (useDataObject) {
		buffer.Append(String.Format(type.ConvertToSqlTypeFormat, "data", "data."+GetMethodFormat(), "", "", GetMethodFormat()));
	    } else {
		buffer.Append(String.Format(type.ConvertToSqlTypeFormat, "", GetFieldFormat(), "", "", GetFieldFormat()));
	    }
	    buffer.Append("));" + Environment.NewLine);
	    return buffer.ToString();
	}

	public String CreateDbDataParameter(Boolean blnOutput, Boolean useDataObject) {
	    StringBuilder buffer = new StringBuilder();
	    buffer.Append("cmd.Parameters.Add(CreateDataParameter(\"@" + GetSqlAlias() + "\", DbType." + this.Column.SqlType.DbType + ", ParameterDirection.");
	    buffer.Append(blnOutput ? "Output, " : "Input, ");
	    if (useDataObject) {
		buffer.Append(String.Format(type.ConvertToSqlTypeFormat, "data", "data."+GetMethodFormat(), "", "", GetMethodFormat()));
	    } else {
		buffer.Append(String.Format(type.ConvertToSqlTypeFormat, "", GetFieldFormat(), "", "", GetFieldFormat()));
	    }
	    buffer.Append("));" + Environment.NewLine);
	    return buffer.ToString();
	}

	/// <summary>
	/// Returns string that compares two objects for the type of this property.
	/// </summary>
	/// <param name="obj1Name">Name of data object instance containing first object to compare</param>
	/// <param name="obj1Name">Name of data object instance containing second object to compare</param>
	/// <returns>Compare code string</returns>
	public string CreateCompareString(string obj1Name, string obj2Name) {
	    return String.Format(type.ConvertForCompare, obj1Name + "." + GetMethodFormat(), obj2Name + "." + GetMethodFormat());
	}

	/// <summary>
	/// Returns the sql name for the property that can be used to reference the property in the data reader, ADO.Net parameter, etc.
	/// </summary>
	/// <returns>Sql alias string</returns>
	public virtual string GetSqlAlias() {
	    if (this.Alias == String.Empty) {
		return this.Column.Name;
	    }
	    else {
		return this.Alias;
	    }
	}

	/// <summary>
	/// Returns name to be used for the property in the data object, etc.
	/// </summary>
	/// <returns>Property namel.</returns>
	public virtual string GetPropertyName() {
	    if (this.ParameterName != "") {
		return this.ParameterName;
	    }
	    else if (this.Alias != "") {
		return this.Alias;
	    }
	    else {
		return this.Name;
	    }
	}

	/// <summary>
	/// Returns the sql expression for the property that can be used to reference/compute the property in an sql statement.
	/// </summary>
	/// <returns>Sql expression string.</returns>
	public virtual string GetSqlExpression() {
	    return GetSqlExpression(false);
	}
	/// <summary>
	/// Returns the sql expression for the property that can be used to reference/compute the property in an sql statement.
	/// </summary>
	/// <param name="useGrouping">Indicates that the grouping function is to be included, if any.</param>
	/// <returns>Sql expression string.</returns>
	public virtual string GetSqlExpression(bool useGrouping) {
	    string s = GetSqlAlias();
	    if (this.Expression != String.Empty) {
		// We replace line feeds with blank to work around bug in CSharpParser with
		// multi-line literals.
		s = this.Expression.Replace("\r\n"," ");
	    }

	    if (useGrouping && this.GroupFunction != String.Empty && !this.GroupBy) {
		return this.GroupFunction + "(" + s + ")";
	    }
	    else {
		return s;
	    }
	}

	/// <summary>
	/// Returns the sql type information for the property.
	/// </summary>
	/// <returns></returns>
	public virtual SqlTypeElement GetSqlType() {
	    return this.Column.SqlType;
	}

	/// <summary>
	/// Populates this property element with values from the specified element.  Usually used to intitalize a derived class instance.
	/// </summary>
	/// <param name="element">Element to populate from.</param>
	public virtual void PopulatePropertyElement(PropertyElement element) {
	    this.Column = (ColumnElement)element.Column.Clone();
	    this.ConcreteType = element.ConcreteType;
	    this.ConvertFromSqlTypeFormat = element.ConvertFromSqlTypeFormat;
	    this.ConvertToSqlTypeFormat = element.ConvertToSqlTypeFormat;
	    this.Description = element.Description;
	    this.Entity = element.Entity;
	    this.Name = element.Name;
	    this.Prefix = element.Prefix;
	    this.Readable = element.Readable;
	    this.Type = (TypeElement)element.Type.Clone();
	    this.Writable = element.Writable;
	    this.Derived = element.Derived;
	}

    }
}
