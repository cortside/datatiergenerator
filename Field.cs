using System;
using System.Collections;
using System.Text;

namespace Spring2.DataTierGenerator {

    public class Field : FieldData {

	/// <summary>
	/// Creates a String for a method parameter representing the specified field.
	/// </summary>
	/// <param name="this">Object that stores the information for the field the parameter represents.</param>
	/// <returns>String containing parameter information of the specified field for a method call.</returns>
	public String ParameterType {
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
	    String[] strMethodParameter;
			
	    // Get an array of data types and variable names
	    strMethodParameter = this.CreateMethodParameter().Split(new Char[] {' '});

	    // this needs to be cleaned up!!!!			
	    // Is the parameter used for input or output
	    //	    if (blnOutput) {
	    if (useDataObject) {
		StringBuilder sb = new StringBuilder();
		sb.Append("cmd.Parameters.Add(new SqlParameter(\"@" + this.Name + "\", SqlDbType." + GetSqlDbType(this.SqlType.Name) + ", " + this.SqlType.Length + ", ParameterDirection.");
		if (blnOutput) {
		    sb.Append("Output"); 
		} else {
		    sb.Append("Input");
		}
		sb.Append(", false, " + this.SqlType.Precision + ", " + this.SqlType.Scale + ", \"" + this.Name + "\", DataRowVersion.Proposed, ");
		sb.Append(String.Format(type.ConvertToSqlTypeFormat, "data", GetMethodFormat(), "", ""));
		sb.Append("));\n");
		return sb.ToString();
		//		    if (useDataTypes) {
		//			return "cmd.Parameters.Add(new SqlParameter(\"@" + this.Name + "\", SqlDbType." + GetSqlDbType(this.SqlType.Name) + ", " + this.SqlType.Length + ", ParameterDirection.Output, false, " + this.SqlType.Precision + ", " + this.SqlType.Scale + ", \"" + this.Name + "\", DataRowVersion.Proposed, data." + this.GetMethodFormat() + ".DBValue));\n";
		//		    } else {
		//			return "cmd.Parameters.Add(new SqlParameter(\"@" + this.Name + "\", SqlDbType." + GetSqlDbType(this.SqlType.Name) + ", " + this.SqlType.Length + ", ParameterDirection.Output, false, " + this.SqlType.Precision + ", " + this.SqlType.Scale + ", \"" + this.Name + "\", DataRowVersion.Proposed, data." + this.GetMethodFormat() + "));\n";
		//		    }
	    } else {
		StringBuilder sb = new StringBuilder();
		sb.Append("cmd.Parameters.Add(new SqlParameter(\"@" + this.Name + "\", SqlDbType." + GetSqlDbType(this.SqlType.Name) + ", " + this.SqlType.Length + ", ParameterDirection.");
		if (blnOutput) {
		    sb.Append("Output"); 
		} else {
		    sb.Append("Input");
		}
		sb.Append(", false, " + this.SqlType.Precision + ", " + this.SqlType.Scale + ", \"" + this.Name + "\", DataRowVersion.Proposed, " + strMethodParameter[1] + "));\n");
		return sb.ToString();
	    }
	    //	    } else {
	    //		//return "objCommand.Parameters.Add(new SqlParameter(\"@" + this.Name + "\", SqlDbType." + GetSqlDbType(this.SqlType.Name) + ", " + this.Length + ", ParameterDirection.Input, false, " + this.Precision + ", " + this.Scale + ", \"" + this.Name + "\", DataRowVersion.Proposed, " + strMethodParameter[1] + "));\n";
	    //		if (useDataObject) {
	    //		    if (useDataTypes) {
	    //			return "cmd.Parameters.Add(new SqlParameter(\"@" + this.Name + "\", SqlDbType." + GetSqlDbType(this.SqlType.Name) + ", " + this.SqlType.Length + ", ParameterDirection.Input, false, " + this.SqlType.Precision + ", " + this.SqlType.Scale + ", \"" + this.Name + "\", DataRowVersion.Proposed, data." + this.GetMethodFormat() + ".DBValue));\n";
	    //		    } else { 
	    //			return "cmd.Parameters.Add(new SqlParameter(\"@" + this.Name + "\", SqlDbType." + GetSqlDbType(this.SqlType.Name) + ", " + this.SqlType.Length + ", ParameterDirection.Input, false, " + this.SqlType.Precision + ", " + this.SqlType.Scale + ", \"" + this.Name + "\", DataRowVersion.Proposed, data." + this.GetMethodFormat() + "));\n";
	    //		    }
	    //		} else {
	    //		    return "cmd.Parameters.Add(new SqlParameter(\"@" + this.Name + "\", SqlDbType." + GetSqlDbType(this.SqlType.Name) + ", " + this.SqlType.Length + ", ParameterDirection.Input, false, " + this.SqlType.Precision + ", " + this.SqlType.Scale + ", \"" + this.Name + "\", DataRowVersion.Proposed, " + strMethodParameter[1] + "));\n";
	    //		}
	    //	    }
	}

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
	    string		strParameter;
	    string	strColumnName;

	    // Format the column name
	    strColumnName = this.Name;
	    strColumnName = strColumnName.Substring(0, 1).ToUpper() + strColumnName.Substring(1); 
		
	    switch (this.SqlType.Name.ToLower()) {
		case "binary":
		    strParameter = "byte[] byte" + strColumnName.Replace(" ", "_");
		    break;
		case "bigint":
		    strParameter = "Int64 int" + strColumnName.Replace(" ", "_");
		    break;
		case "bit":
		    strParameter = "bool bln" + strColumnName.Replace(" ", "_");
		    break;
		case "char":
		    strParameter = "string str" + strColumnName.Replace(" ", "_");
		    break;
		case "datetime":
		    strParameter = "DateTime dt" + strColumnName.Replace(" ", "_");
		    break;
		case "decimal":
		    strParameter = "Decimal dec" + strColumnName.Replace(" ", "_");
		    break;
		case "float":
		    strParameter = "Double dbl" + strColumnName.Replace(" ", "_");
		    break;
		case "image":
		    strParameter = "String str" + strColumnName.Replace(" ", "_");  //"byte[] byte" + strColumnName.Replace(" ", "_");
		    break;
		case "int":
		    strParameter = "Int32 int" + strColumnName.Replace(" ", "_");
		    break;
		case "money":
		    strParameter = "Decimal dec" + strColumnName.Replace(" ", "_");
		    break;
		case "nchar":
		    strParameter = "string str" + strColumnName.Replace(" ", "_");
		    break;
		case "ntext":
		    strParameter = "string str" + strColumnName.Replace(" ", "_");
		    break;
		case "nvarchar":
		    strParameter = "string str" + strColumnName.Replace(" ", "_");
		    break;
		case "numeric":
		    strParameter = "Decimal dec" + strColumnName.Replace(" ", "_");
		    break;
		case "real":
		    strParameter = "Single sng" + strColumnName.Replace(" ", "_");
		    break;
		case "smalldatetime":
		    strParameter = "DateTime dt" + strColumnName.Replace(" ", "_");
		    break;
		case "smallint":
		    strParameter = "Int16 int" + strColumnName.Replace(" ", "_");
		    break;
		case "smallmoney":
		    strParameter = "Decimal dec" + strColumnName.Replace(" ", "_");
		    break;
		case "sql_variant":
		    strParameter = "Object obj" + strColumnName.Replace(" ", "_");
		    break;
		case "sysname":
		    strParameter = "string str" + strColumnName.Replace(" ", "_");
		    break;
		case "text":
		    strParameter = "string str" + strColumnName.Replace(" ", "_");
		    break;
		case "timestamp":
		    strParameter = "DateTime dt" + strColumnName.Replace(" ", "_");
		    break;
		case "tinyint":
		    strParameter = "byte byte" + strColumnName.Replace(" ", "_");
		    break;
		case "varbinary":
		    strParameter = "byte[] byte" + strColumnName.Replace(" ", "_");
		    break;
		case "varchar":
		    strParameter = "string str" + strColumnName.Replace(" ", "_");
		    break;
		case "uniqueidentifier":
		    strParameter = "Guid guid" + strColumnName.Replace(" ", "_");
		    break;
		default:  // Unknow data type
		    throw(new Exception("Invalid SQL Server data type specified: " + this.SqlType));
	    }
			
	    // Return the new parameter string
	    return strParameter;
	}

	/// <summary>
	/// Creates a string containing the parameter declaration for a stored procedure based on the parameters passed in.
	/// </summary>
	/// <returns>String containing parameter information of the specified field for a stored procedure.</returns>
	public string CreateParameterString(bool blnCheckForOutput) {
	    string	strParameter;
		
	    switch (this.SqlType.Name) {
		case "binary":
		    strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name + "(" + this.SqlType.Length + ")";
		    break;
		case "bigint":
		    strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name;
		    break;
		case "bit":
		    strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name;
		    break;
		case "char":
		    strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name + "(" + this.SqlType.Length + ")";
		    break;
		case "datetime":
		    strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name;
		    break;
		case "decimal":
		    if (this.SqlType.Scale == 0)
			strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name + "(" + this.SqlType.Precision + ")";
		    else
			strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name + "(" + this.SqlType.Precision + ", "+ this.SqlType.Scale + ")";
		    break;
		case "float":
		    strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name + "(" + this.SqlType.Precision + ")";
		    break;
		case "image":
		    strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name;
		    break;
		case "int":
		    strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name;
		    break;
		case "money":
		    strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name;
		    break;
		case "nchar":
		    strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name + "(" + this.SqlType.Length + ")";
		    break;
		case "ntext":
		    strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name;
		    break;
		case "nvarchar":
		    strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name + "(" + this.SqlType.Length + ")";
		    break;
		case "numeric":
		    if (this.SqlType.Scale == 0)
			strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name + "(" + this.SqlType.Precision + ")";
		    else
			strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name + "(" + this.SqlType.Precision + ", "+ this.SqlType.Scale + ")";
		    break;
		case "real":
		    strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name;
		    break;
		case "smalldatetime":
		    strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name;
		    break;
		case "smallint":
		    strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name;
		    break;
		case "smallmoney":
		    strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name;
		    break;
		case "sql_variant":
		    strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name;
		    break;
		case "sysname":
		    strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name;
		    break;
		case "text":
		    strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name;
		    break;
		case "timestamp":
		    strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name;
		    break;
		case "tinyint":
		    strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name;
		    break;
		case "varbinary":
		    strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name + "(" + this.SqlType.Length + ")";
		    break;
		case "varchar":
		    strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name + "(" + this.SqlType.Length + ")";
		    break;
		case "uniqueidentifier":
		    strParameter = "@" + this.Name.Replace(" ", "_") + "\t" + this.SqlType.Name;
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

	// static helper method
	public static Field GetIdentityColumn(ArrayList arrFieldList) {
	    foreach (Field field in arrFieldList) {
		if (field.IsIdentity) {
		    return field;
		}
	    }

	    return new Field();   // this should not return this - should return null
	}

    }
}
