using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator {

    public class Field : FieldData {

	/// <summary>
	/// Creates a String for a method parameter representing the specified field.
	/// </summary>
	/// <param name="this">Object that stores the information for the field the parameter represents.</param>
	/// <returns>String containing parameter information of the specified field for a method call.</returns>
	public String ParameterTypeX {
	    get {
		switch (sqlType.Name.ToLower()) {
		    case "binary":
			return "byte[]";
		    case "bigint":
			return "Int64";
		    case "bit":
			return "Boolean";
		    case "char":
			return "String";
		    case "datetime":
			return "DateTime";
		    case "decimal":
			return "Decimal";
		    case "float":
			return "Double";
		    case "image":
			return "String"; //"byte[]";
		    case "int":
			return "Int32";
		    case "money":
			return "Decimal";
		    case "nchar":
			return "String";
		    case "ntext":
			return "String";
		    case "nvarchar":
			return "String";
		    case "numeric":
			return "Decimal";
		    case "real":
			return "Single";
		    case "smalldatetime":
			return "DateTime";
		    case "smallint":
			return "Int16";
		    case "smallmoney":
			return "Decimal";
		    case "sql_variant":
			return "Object";
		    case "sysname":
			return "String";
		    case "text":
			return "String";
		    case "timestamp":
			return "DateTime";
		    case "tinyint":
			return "byte";
		    case "varbinary":
			return "byte[]";
		    case "varchar":
			return "String";
		    case "uniqueidentifier":
			return "Guid";
		    default:  // Unknow data type
			throw(new Exception("Invalid SQL Server data type specified: " + sqlType));
		}
	    }
	}

	/// <summary>
	/// Creates a String for a method parameter representing the specified field.
	/// </summary>
	/// <param name="this">Object that stores the information for the field the parameter represents.</param>
	/// <returns>String containing parameter information of the specified field for a method call.</returns>
	public String ReaderType {
	    get {
		switch (sqlType.Name.ToLower()) {
		    case "binary":
			return "Bytes";
		    case "bigint":
			return "Int64";
		    case "bit":
			return "Int16";  //Boolean
		    case "char":
			return "String";
		    case "datetime":
			return "DateTime";
		    case "decimal":
			return "Decimal";
		    case "float":
			return "Double";
		    case "image":
			return "String";  //Bytes
		    case "int":
			return "Int32";
		    case "money":
			return "Decimal";
		    case "nchar":
			return "String";
		    case "ntext":
			return "String";
		    case "nvarchar":
			return "String";
		    case "numeric":
			return "Decimal";
		    case "real":
			return "Single";
		    case "smalldatetime":
			return "DateTime";
		    case "smallint":
			return "Int16";
		    case "smallmoney":
			return "Decimal";
		    case "sql_variant":
			return "Object";
		    case "sysname":
			return "String";
		    case "text":
			return "String";
		    case "timestamp":
			return "DateTime";
		    case "tinyint":
			return "Byte";
		    case "varbinary":
			return "Bytes";
		    case "varchar":
			return "String";
		    case "uniqueidentifier":
			return "Guid";
		    default:  // Unknow data type
			throw(new Exception("Invalid SQL Server data type specified: " + sqlType));
		}
	    }
	}

	/// <summary>
	/// Matches a SQL Server data type to a SqlClient.SqlDbType.
	/// </summary>
	/// <param name="strType">A String representing a SQL Server data type.</param>
	/// <returns>A String representing a SqlClient.SqlDbType.</returns>
	public String GetSqlDbType(String strType) {
	    switch (strType.ToLower()) {
		case "binary":
		    return "Binary";
		case "bigint":
		    return "BigInt";
		case "bit":
		    return "Bit";
		case "char":
		    return "Char";
		case "datetime":
		    return "DateTime";
		case "decimal":
		    return "Decimal";
		case "float":
		    return "Float";
		case "image":
		    return "VarChar";
		case "int":
		    return "Int";
		case "money":
		    return "Money";
		case "nchar":
		    return "NChar";
		case "ntext":
		    return "NText";
		case "nvarchar":
		    return "NVarChar";
		case "numeric":
		    return "Decimal";
		case "real":
		    return "Real";
		case "smalldatetime":
		    return "SmallDateTime";
		case "smallint":
		    return "SmallInt";
		case "smallmoney":
		    return "SmallMoney";
		case "sql_variant":
		    return "Variant";
		case "sysname":
		    return "VarChar";
		case "text":
		    return "Text";
		case "timestamp":
		    return "Timestamp";
		case "tinyint":
		    return "TinyInt";
		case "varbinary":
		    return "VarBinary";
		case "varchar":
		    return "VarChar";
		case "uniqueidentifier":
		    return "UniqueIdentifier";
		default:  // Unknow data type
		    throw(new Exception("Invalid SQL Server data type specified: " + strType));
	    }
	}

	/// <summary>
	/// Creates a string for a SqlParameter representing the specified field.
	/// </summary>
	/// <param name="this">Object that stores the information for the field the parameter represents.</param>
	/// <returns>String containing SqlParameter information of the specified field for a method call.</returns>
	public string CreateSqlParameter(bool blnOutput, bool useDataObject) {
	    // Get an array of data types and variable names
	    //	    String[] strMethodParameter = this.CreateMethodParameter().Split(new Char[] {' '});

	    // this needs to be cleaned up!!!!			
	    // Is the parameter used for input or output
	    //	    if (blnOutput) {
	    //	    if (useDataObject) {
	    //		StringBuilder sb = new StringBuilder();
	    //		sb.Append("cmd.Parameters.Add(new SqlParameter(\"@" + this.SqlName + "\", SqlDbType." + GetSqlDbType(this.SqlType.Name) + ", " + this.SqlType.Length + ", ParameterDirection.");
	    //		if (blnOutput) {
	    //		    sb.Append("Output"); 
	    //		} else {
	    //		    sb.Append("Input");
	    //		}
	    //		sb.Append(", false, " + this.SqlType.Precision + ", " + this.SqlType.Scale + ", \"" + this.Name + "\", DataRowVersion.Proposed, ");
	    //		sb.Append(String.Format(type.ConvertToSqlTypeFormat, "data", GetMethodFormat(), "", ""));
	    //		sb.Append("));\n");
	    //		return sb.ToString();
	    //	    } else {
	    StringBuilder sb = new StringBuilder();
	    sb.Append("cmd.Parameters.Add(new SqlParameter(\"@" + this.SqlName + "\", SqlDbType." + GetSqlDbType(this.SqlType.Name) + ", " + this.SqlType.Length + ", ParameterDirection.");
	    if (blnOutput) {
		sb.Append("Output"); 
	    } else {
		sb.Append("Input");
	    }
	    sb.Append(", false, " + this.SqlType.Precision + ", " + this.SqlType.Scale + ", \"" + this.Name + "\", DataRowVersion.Proposed, ");
	    if (useDataObject) {
		sb.Append(String.Format(type.ConvertToSqlTypeFormat, "data", "data."+GetMethodFormat(), "", "", GetMethodFormat()));
	    } else {
		sb.Append(String.Format(type.ConvertToSqlTypeFormat, "", GetMethodFormat(), "", "", GetMethodFormat()));
	    }
	    sb.Append("));\n");
	    return sb.ToString();
	}
	//	}

	public String GetFieldFormat() {
	    return this.Name.Substring(0, 1).ToLower() + this.Name.Substring(1);
	}

	public String GetMethodFormat() {
	    return this.Name.Substring(0, 1).ToUpper() + this.Name.Substring(1);
	}

	/// <summary>
	/// Creates a string for a method parameter representing the specified field.
	/// </summary>
	/// <returns>String containing parameter information of the specified field for a method call.</returns>
	public string CreateMethodParameter() {
	    return type.Name + " " + this.Name.Substring(0, 1).ToUpper() + this.Name.Substring(1);
	}

	/// <summary>
	/// Creates a string containing the parameter declaration for a stored procedure based on the parameters passed in.
	/// </summary>
	/// <returns>String containing parameter information of the specified field for a stored procedure.</returns>
	public string CreateParameterString(bool blnCheckForOutput) {
	    String strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name;
	    // this should be defined as a sqltype attribute - a format string		
	    switch (this.SqlType.Name) {
		case "nchar":
		case "nvarchar":
		case "varbinary":
		case "varchar":
		case "binary":
		case "char":
		    strParameter += "(" + this.SqlType.Length + ")";
		    break;
		case "decimal":
		    if (this.SqlType.Scale == 0)
			strParameter += "(" + this.SqlType.Precision + ")";
		    else
			strParameter += "(" + this.SqlType.Precision + ", "+ this.SqlType.Scale + ")";
		    break;
		case "float":
		    strParameter += "(" + this.SqlType.Precision + ")";
		    break;
		case "numeric":
		    if (this.SqlType.Scale == 0)
			strParameter += "(" + this.SqlType.Precision + ")";
		    else
			strParameter += "(" + this.SqlType.Precision + ", "+ this.SqlType.Scale + ")";
		    break;
		case "bigint":
		case "bit":
		case "datetime":
		case "image":
		case "int":
		case "money":
		case "ntext":
		case "real":
		case "smalldatetime":
		case "smallint":
		case "smallmoney":
		case "sql_variant":
		case "sysname":
		case "text":
		case "timestamp":
		case "tinyint":
		case "uniqueidentifier":
		    // default format is OK
		    break;
		default:  // Unknow data type
		    throw(new Exception("Invalid SQL Server data type specified: " + this.SqlType.Name.ToLower()));
	    }
			
	    // Is the parameter an output parameter?
	    if (blnCheckForOutput)
		if (this.IsRowGuidCol || this.IsIdentity)
		    strParameter += " output";
			
	    // Return the new parameter string
	    return strParameter;
	}

	public String ToXml(Boolean sqlAttributesOnly) {
	    StringBuilder sb = new StringBuilder();
	    sb.Append("<property name=\"").Append(name).Append("\"");
	    sb.Append(" sqlname=\"").Append(sqlName).Append("\"");
	    sb.Append(" sqltype=\"").Append(sqlType.Name).Append("\"");
	    if (sqlType.Length>0 && sqlType.Scale==0 && sqlType.Precision==0) sb.Append(" length=\"").Append(sqlType.Length.ToString()).Append("\"");
	    if (sqlType.Scale>0) sb.Append(" scale=\"").Append(sqlType.Scale.ToString()).Append("\"");
	    if (sqlType.Precision>0) sb.Append(" precision=\"").Append(sqlType.Precision.ToString()).Append("\"");
	    if (!sqlAttributesOnly) {
		if (sqlType.ReaderMethodFormat.Length>0) sb.Append(" readermethodformat=\"").Append(sqlType.ReaderMethodFormat).Append("\"");
		if (type.Name.Length>0) sb.Append(" type=\"").Append(type.Name).Append("\"");
		if (type.ConcreteType.Length>0) sb.Append(" concretetype=\"").Append(type.ConcreteType).Append("\"");
		if (type.Package.Length>0) sb.Append(" namespace=\"").Append(type.Package).Append("\"");
		if (type.ConvertToSqlTypeFormat.Length>0) sb.Append(" converttosqltypeformat=\"").Append(type.ConvertToSqlTypeFormat).Append("\"");
		if (type.ConvertFromSqlTypeFormat.Length>0) sb.Append(" convertfromsqltypeformat=\"").Append(type.ConvertFromSqlTypeFormat).Append("\"");
		if (type.NewInstanceFormat.Length>0) sb.Append(" newinstanceformat=\"").Append(type.NewInstanceFormat).Append("\"");
		if (!accessModifier.Equals("public")) sb.Append(" accessmodifier=\"").Append(accessModifier).Append("\"");
	    }
	    if (isRowGuidCol) sb.Append(" isrowguidcol=\"True\"");
	    if (isIdentity) sb.Append(" isidentity=\"True\"");
	    if (isPrimaryKey) sb.Append(" isprimarykey=\"True\"");
	    if (isForeignKey) sb.Append(" isforeignkey=\"True\"");
	    if (IsViewColumn) sb.Append(" isviewcolumn=\"True\"");
	    if (!sqlAttributesOnly && description.Length>0) {
		sb.Append("\t\t").Append(description).Append("\n");
		sb.Append("\t</property>");
	    } else {
		sb.Append(" />");
	    }

	    return sb.ToString();
	}

	public static ArrayList ParseFromXml(XmlDocument doc, Entity entity, Hashtable sqltypes, Hashtable types) {
	    ArrayList fields = new ArrayList();
	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("entity");
	    foreach (XmlNode element in elements) {
		String name = element.Attributes["name"].Value;
		String sqlObject = (element.Attributes["sqlobject"] == null) ? "" : element.Attributes["sqlobject"].Value;
		if (((entity.SqlObject.Length>0 && sqlObject == entity.SqlObject) || (entity.SqlObject.Length==0 && name == entity.Name)) && element.HasChildNodes) {
		    foreach (XmlNode node in element.ChildNodes) {
			// for properties  - collections and nested elements to be handled seperately
			if (node.Name.ToLower().Equals("property")) {
			    Field field = new Field();
			    if (node.Attributes["name"] != null) {
				field.Name = node.Attributes["name"].Value;
				field.SqlName = field.Name;
			    }
			    if (node.Attributes["sqlname"] != null) {
				if (node.Attributes["sqlname"].Value.Equals("*")) {
				    field.SqlName = field.Name;
				} else {
				    field.SqlName = node.Attributes["sqlname"].Value;
				}
			    }
			    field.Description = node.InnerText.Trim();
			    if (node.Attributes["sqltype"] != null) {
				field.SqlType.Name = node.Attributes["sqltype"].Value;

				// if the sql type is defined, default to all values defined in it
				if (sqltypes.ContainsKey(field.SqlType.Name)) {
				    field.SqlType = (SqlType)((SqlType)sqltypes[field.SqlType.Name]).Clone();
				    if (types.Contains(field.SqlType.Type)) {
					field.Type = (Type)((Type)types[field.SqlType.Type]).Clone();
				    } else {
					Console.Out.WriteLine("Type " + field.SqlType.Type + " was not defined");
				    }
				} else {
				    Console.Out.WriteLine("SqlType " + field.SqlType.Name + " was not defined");
				}
			    }
			    if (node.Attributes["length"] != null) {
				field.SqlType.Length = Int32.Parse(node.Attributes["length"].Value);
			    }
			    if (node.Attributes["scale"] != null) {
				field.SqlType.Scale = Int32.Parse(node.Attributes["scale"].Value);
			    }
			    if (node.Attributes["precision"] != null) {
				field.SqlType.Precision = Int32.Parse(node.Attributes["precision"].Value);
			    }

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

			    field.IsIdentity = (node.Attributes["isidentity"] != null);
			    field.IsPrimaryKey = (node.Attributes["isprimarykey"] != null);
			    field.IsRowGuidCol = (node.Attributes["isrowguidcol"] != null);
			    field.IsForeignKey = (node.Attributes["isforeignkey"] != null);
			    field.IsViewColumn = (node.Attributes["isviewcolumn"] != null);
			    fields.Add(field);
			}
		    }
		}
	    }
	    return fields;
	}

    }
}
