using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator.Core {

    public class Field : Spring2.Core.DataObject.DataObject, ICloneable {

	private String name = String.Empty;
	private Column column = new Column();
	private Type type = new Type();
	private String accessModifier = "private";
	private String description = String.Empty;

	public String Name {
	    get { return this.name; }
	    set { this.name = value; }
	}

	public Column Column {
	    get { return this.column; }
	    set { this.column = value; }
	}

	public Type Type {
	    get { return this.type; }
	    set { this.type = value; }
	}

	public String AccessModifier {
	    get { return this.accessModifier; }
	    set { this.accessModifier = value; }
	}

	public String Description {
	    get { return this.description; }
	    set { this.description = value; }
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

	public String ToXml(Boolean sqlAttributesOnly) {
	    StringBuilder sb = new StringBuilder();
//	    sb.Append("<property name=\"").Append(name).Append("\"");
//	    sb.Append(" sqlname=\"").Append(sqlName).Append("\"");
//	    sb.Append(" sqltype=\"").Append(sqlType.Name).Append("\"");
//	    if (sqlType.Length>0 && sqlType.Scale==0 && sqlType.Precision==0) sb.Append(" length=\"").Append(sqlType.Length.ToString()).Append("\"");
//	    if (sqlType.Scale>0) sb.Append(" scale=\"").Append(sqlType.Scale.ToString()).Append("\"");
//	    if (sqlType.Precision>0) sb.Append(" precision=\"").Append(sqlType.Precision.ToString()).Append("\"");
//	    if (!sqlAttributesOnly) {
//		if (sqlType.ReaderMethodFormat.Length>0) sb.Append(" readermethodformat=\"").Append(sqlType.ReaderMethodFormat).Append("\"");
//		if (type.Name.Length>0) sb.Append(" type=\"").Append(type.Name).Append("\"");
//		if (type.ConcreteType.Length>0) sb.Append(" concretetype=\"").Append(type.ConcreteType).Append("\"");
//		if (type.Package.Length>0) sb.Append(" namespace=\"").Append(type.Package).Append("\"");
//		if (type.ConvertToSqlTypeFormat.Length>0) sb.Append(" converttosqltypeformat=\"").Append(type.ConvertToSqlTypeFormat).Append("\"");
//		if (type.ConvertFromSqlTypeFormat.Length>0) sb.Append(" convertfromsqltypeformat=\"").Append(type.ConvertFromSqlTypeFormat).Append("\"");
//		if (type.NewInstanceFormat.Length>0) sb.Append(" newinstanceformat=\"").Append(type.NewInstanceFormat).Append("\"");
//		if (!accessModifier.Equals("public")) sb.Append(" accessmodifier=\"").Append(accessModifier).Append("\"");
//	    }
//	    if (isRowGuidCol) sb.Append(" isrowguidcol=\"True\"");
//	    if (isIdentity) sb.Append(" isidentity=\"True\"");
//	    if (isPrimaryKey) sb.Append(" isprimarykey=\"True\"");
//	    if (isForeignKey) sb.Append(" isforeignkey=\"True\"");
//	    if (IsViewColumn) sb.Append(" isviewcolumn=\"True\"");
//	    if (!sqlAttributesOnly && description.Length>0) {
//		sb.Append("\t\t").Append(description).Append("\n");
//		sb.Append("\t</property>");
//	    } else {
//		sb.Append(" />");
//	    }
//
	    return sb.ToString();
	}

	public static ArrayList ParseFromXml(XmlDocument doc, Entity entity, Hashtable sqltypes, Hashtable types) {
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
			Console.Out.WriteLine("WARNING:  <property> elements on entity (" + entity.Name + ") should be defined within a <properties> element.");
		    }
		    foreach (XmlNode node in nodes) {
			// for properties  - collections and nested elements to be handled seperately
			if (node.Name.ToLower().Equals("property")) {
			    Field field = new Field();
			    if (node.Attributes["name"] != null) {
				field.Name = node.Attributes["name"].Value;
			    }
			    if (node.Attributes["column"] != null) {
				if (node.Attributes["column"].Value.Equals("*")) {
				    field.Column.Name = field.Name;
				} else {
				    field.Column.Name = node.Attributes["column"].Value;
				}
				Column column = entity.SqlEntity.FindColumnByName(field.Column.Name);
				if (column!= null) {
				    field.Column = (Column)column.Clone();
				    if (types.Contains(field.Column.SqlType.Type)) {
					field.Type = (Type)((Type)types[field.Column.SqlType.Type]).Clone();
				    } else {
					Console.Out.WriteLine("ERROR: Type " + field.Column.SqlType.Type + " was not defined [property=" + field.name + "]");
				    }

				} else {
				    Console.Out.WriteLine("ERROR: column (" + field.Column.Name + ") specified for property (" + field.Name + ") on entity (" + entity.Name + ") was not found in sql entity (" + entity.SqlEntity.Name + ")");
				}
			    }
			    field.Description = node.InnerText.Trim();

//			    if (node.Attributes["sqltype"] != null) {
//				field.SqlType.Name = node.Attributes["sqltype"].Value;
//
//				// if the sql type is defined, default to all values defined in it
//				if (sqltypes.ContainsKey(field.SqlType.Name)) {
//				    field.SqlType = (SqlType)((SqlType)sqltypes[field.SqlType.Name]).Clone();
//				    if (types.Contains(field.SqlType.Type)) {
//					field.Type = (Type)((Type)types[field.SqlType.Type]).Clone();
//				    } else {
//					Console.Out.WriteLine("ERROR: Type " + field.SqlType.Type + " was not defined [property=" + field.name + "]");
//				    }
//				} else {
//				    Console.Out.WriteLine("ERROR: SqlType " + field.SqlType.Name + " was not defined [property=" + field.name + "]");
//				}
//			    }
//			    if (node.Attributes["length"] != null) {
//				field.SqlType.Length = Int32.Parse(node.Attributes["length"].Value);
//			    }
//			    if (node.Attributes["scale"] != null) {
//				field.SqlType.Scale = Int32.Parse(node.Attributes["scale"].Value);
//			    }
//			    if (node.Attributes["precision"] != null) {
//				field.SqlType.Precision = Int32.Parse(node.Attributes["precision"].Value);
//			    }

			    // the concrete type is the *real* type, type can be the same or can be in interface or coersable type
			    if (node.Attributes["type"] != null) {
				String type = node.Attributes["type"].Value;
				String concreteType = type;
				if (node.Attributes["concretetype"] != null) {
				    concreteType = node.Attributes["concretetype"].Value;
				}
				// if the data type is defined, default it as the property and left be overridden
				if (types.Contains(concreteType)) {
				    field.Type = (Type)((Type)types[concreteType]).Clone();
				    field.Type.Name = type;
				} else {
				    Console.Out.WriteLine("Type " + concreteType + " was not defined");
				}
			    }

			    if (node.Attributes["accessmodifier"] != null) {
				field.AccessModifier = node.Attributes["accessmodifier"].Value;
			    }

			    if (node.Attributes["convertfromsqltypeformat"] != null) {
				field.Type.ConvertFromSqlTypeFormat = node.Attributes["convertfromsqltypeformat"].Value;
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
	    sb.Append("));\n");
	    return sb.ToString();
	}



	public Object Clone() {
	    return MemberwiseClone();
	}

    }
}
