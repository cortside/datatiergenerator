using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class PropertyElement : ElementSkeleton {

	private static readonly String TYPE = "type";
	private static readonly String COLUMN = "column";
	private static readonly String CONCRETE_TYPE = "concretetype";
	private static readonly String CONVERT_FROM_SQLTYPE_FORMAT = "convertfromsqltypeformat";
	private static readonly String CONVERT_TO_SQLTYPE_FORMAT = "converttosqltypeformat";
	private static readonly String ENTITY = "entity";
	private static readonly String PREFIX = "prefix";
	private static readonly String READABLE = "readable";
	private static readonly String WRITABLE = "writable";

	private TypeElement type = new TypeElement();
	private ColumnElement column = new ColumnElement();
	private TypeElement concreteType = new TypeElement();
	private String convertFromSqlTypeFormat = String.Empty;
	private String convertToSqlTypeFormat = String.Empty;
	private TypeElement entity = new TypeElement();
	private String prefix = String.Empty;
	private Boolean readable = true;
	private Boolean writable = true;
	
	private String accessModifier = "private";

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
	    get { return this.writable; }
	    set { this.writable = value; }
	}

	public String AccessModifier {
	    get { return this.accessModifier; }
	    set { this.accessModifier = value; }
	}

	public String GetFieldFormat() {
	    return this.Name.Substring(0, 1).ToLower() + this.Name.Substring(1).Replace('.', '_');
	}

	public String GetMethodFormat() {
	    return this.Name.Substring(0, 1).ToUpper() + this.Name.Substring(1);
	}

	/// <summary>
	/// Creates a string for a method parameter representing the specified field.
	/// </summary>
	/// <returns>String containing parameter information of the specified field for a method call.</returns>
	public string CreateMethodParameter() {
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

			propertyElement.Name = GetAttributeValue(propertyNode, NAME, propertyElement.Name);
			propertyElement.Type.Name = GetAttributeValue(propertyNode, TYPE, propertyElement.Type.Name);
			propertyElement.Column.Name = GetAttributeValue(propertyNode, COLUMN, propertyElement.Column.Name);
			propertyElement.ConcreteType.Name = GetAttributeValue(propertyNode, CONCRETE_TYPE, propertyElement.ConcreteType.Name);
			propertyElement.ConvertFromSqlTypeFormat = GetAttributeValue(propertyNode, CONVERT_FROM_SQLTYPE_FORMAT, propertyElement.ConvertFromSqlTypeFormat);
			propertyElement.ConvertToSqlTypeFormat = GetAttributeValue(propertyNode, CONVERT_TO_SQLTYPE_FORMAT, propertyElement.ConvertToSqlTypeFormat);
			propertyElement.Entity.Name = GetAttributeValue(propertyNode, ENTITY, propertyElement.Entity.Name);
			propertyElement.Prefix = GetAttributeValue(propertyNode, PREFIX, propertyElement.Prefix);
			propertyElement.Readable = Boolean.Parse(GetAttributeValue(propertyNode, READABLE, propertyElement.Readable.ToString()));
			propertyElement.Writable = Boolean.Parse(GetAttributeValue(propertyNode, WRITABLE, propertyElement.Writable.ToString()));

			propertyElements.Add(propertyElement);
		    }
		}
	    }
	}

	public static ArrayList ParseFromXml(XmlDocument doc, IList entities, EntityElement entity, Hashtable sqltypes, Hashtable types, ParserValidationDelegate vd) {
	    ArrayList fields = new ArrayList();
	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("entity");
	    foreach (XmlNode element in elements) {
		String name = element.Attributes["name"].Value;
		String sqlEntity = (element.Attributes["sqlentity"] == null) ? "" : element.Attributes["sqlentity"].Value;
		if (((entity.SqlEntity.Name.Length>0 && sqlEntity == entity.SqlEntity.Name) || (entity.SqlEntity.Name.Length==0 && name == entity.Name)) && element.HasChildNodes) {
		    // look for a properties element, if one does not exist, assume that everything under the entity is a property (for backward compatablility)
		    XmlNodeList nodes = element.ChildNodes;
		    Boolean hasProperties = false;
		    foreach(XmlNode node in element.ChildNodes) {
			if (node.Name.Equals("properties")) {
			    nodes = node.ChildNodes;
			    hasProperties = true;
			}
		    }
		    if (!hasProperties) {
			vd(ParserValidationArgs.NewError("<property> elements on entity (" + entity.Name + ") should be defined within a <properties> element."));
		    }
		    foreach (XmlNode node in nodes) {
			// for properties  - collections and nested elements to be handled seperately
			if (node.Name.ToLower().Equals("property")) {
			    PropertyElement field = new PropertyElement();
			    if (node.Attributes["name"] != null) {
				field.Name = node.Attributes["name"].Value;
			    }

			    if (node.Attributes["column"] != null) {
				if (node.Attributes["column"].Value.Equals("*")) {
				    field.Column.Name = field.Name;
				} else {
				    field.Column.Name = node.Attributes["column"].Value;
				}
				ColumnElement column = entity.SqlEntity.FindColumnByName(field.Column.Name);
				if (column!= null) {
				    field.Column = (ColumnElement)column.Clone();
				    if (types.Contains(field.Column.SqlType.Type)) {
					field.Type = (TypeElement)((TypeElement)types[field.Column.SqlType.Type]).Clone();
				    } else {
					vd(ParserValidationArgs.NewError("Type " + field.Column.SqlType.Type + " was not defined [property=" + field.name + "]"));
				    }

				} else {
				    vd(ParserValidationArgs.NewError("column (" + field.Column.Name + ") specified for property (" + field.Name + ") on entity (" + entity.Name + ") was not found in sql entity (" + entity.SqlEntity.Name + ")"));
				}
			    }

			    if (node.Attributes["readable"] != null) {
				field.Readable = Boolean.Parse(node.Attributes["readable"].Value);
			    }
			    if (node.Attributes["writable"] != null) {
				field.Writable = Boolean.Parse(node.Attributes["writable"].Value);
			    }

			    field.Description = node.InnerText.Trim();

			    // the concrete type is the *real* type, type can be the same or can be in interface or coersable type
			    if (node.Attributes["type"] != null) {
				String type = node.Attributes["type"].Value;
				String concreteType = type;
				if (node.Attributes["concretetype"] != null) {
				    concreteType = node.Attributes["concretetype"].Value;
				}
				// if the data type is defined, default it as the property and left be overridden
				if (types.Contains(concreteType)) {
				    field.Type = (TypeElement)((TypeElement)types[concreteType]).Clone();
				    field.Type.Name = type;
				} else {
				    vd(ParserValidationArgs.NewError("Type " + concreteType + " was not defined"));
				}
			    }

			    if (node.Attributes["accessmodifier"] != null) {
				field.AccessModifier = node.Attributes["accessmodifier"].Value;
			    }

			    if (node.Attributes["convertfromsqltypeformat"] != null) {
				field.Type.ConvertFromSqlTypeFormat = node.Attributes["convertfromsqltypeformat"].Value;
			    }
			    if (node.Attributes["converttosqltypeformat"] != null) {
				field.Type.ConvertToSqlTypeFormat = node.Attributes["converttosqltypeformat"].Value;
			    }

			    if (node.Attributes["entity"]!=null) {
				EntityElement subentity = EntityElement.FindEntityByName((ArrayList)entities, node.Attributes["entity"].Value);
				if (subentity != null) {
				    String prefix = subentity.Name + "_";
				    if (node.Attributes["prefix"]!=null) {
					prefix = node.Attributes["prefix"].Value;
				    }
				    
				    foreach(PropertyElement f in subentity.Fields) {
					PropertyElement subfield = (PropertyElement)f.Clone();
					subfield.Name = field.Name + "." + subfield.Name;

					// if field has sql column defined
					if (!f.Column.Name.Equals(String.Empty)) {
					    ColumnElement column = entity.SqlEntity.FindColumnByName(prefix + subfield.Column.Name);
					    if (column != null) {
						subfield.Column = (ColumnElement)column.Clone();
					    } else {
						vd(ParserValidationArgs.NewError("column (" + prefix + subfield.Column.Name + ") specified for property (" + subfield.Name + ") on entity (" + entity.Name + ") was not found in sql entity (" + entity.SqlEntity.Name + ")"));
					    }
					}
					fields.Add(subfield);
				    }
				} else {
				    vd(ParserValidationArgs.NewError("Entity " + entity.Name + " referenced another entity that was not defined (or defined below this one): " + node.Attributes["entity"].Value));
				}
			    } 

			    fields.Add(field);
			    
			}
		    }
		}
	    }
	    return fields;
	}

	/// <summary>
	/// Creates a string for a SqlParameter representing the specified field.
	/// </summary>
	/// <param name="this">Object that stores the information for the field the parameter represents.</param>
	/// <returns>String containing SqlParameter information of the specified field for a method call.</returns>
	public string CreateSqlParameter(bool blnOutput, bool useDataObject) {
	    StringBuilder sb = new StringBuilder();
	    sb.Append("cmd.Parameters.Add(new SqlParameter(\"@" + column.Name + "\", SqlDbType." + column.SqlType.SqlDbType + ", " + column.SqlType.Length + ", ParameterDirection.");
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


	public static PropertyElement FindByName(IList fields, String name) {
	    foreach (PropertyElement field in fields) {
		if (field.Name.Equals(name)) {
		    return field;
		}
	    }
	    return null;
	}

	public static PropertyElement FindByColumnName(IList fields, String name) {
	    foreach (PropertyElement field in fields) {
		if (field.Column.Name.Equals(name)) {
		    return field;
		}
	    }
	    return null;
	}
    }
}
