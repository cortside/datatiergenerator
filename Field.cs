using System;
using System.Collections;

namespace Spring2.DataTierGenerator {
    /// <summary>
    /// Class that stores information for fields in SQL Server tables.
    /// </summary>
    public class Field {
        // Private variable used to hold the property values
        String	columnName;
        String	dbType;
        Int32 length;
        Int32 precision;
        Int32 scale;
        Boolean	isRowGuidCol;
        Boolean	isIdentity;
        Boolean	isPrimaryKey;
        Boolean	isForeignKey;
        Boolean	isViewColumn;
	
        /// <summary>
        /// Copies one field object to another.
        /// </summary>
        public Field Copy() {
            Field field;
			
            field = new Field();
            field.ColumnName = columnName;
            field.IsIdentity = isIdentity;
            field.IsForeignKey = isForeignKey;
            field.IsPrimaryKey = isPrimaryKey;
            field.IsRowGuidCol = isRowGuidCol;
            field.Length = length;
            field.Precision = precision;
            field.Scale = scale;
            field.DBType = dbType;
            return field;
        }

        /// <summary>
        /// Name of the column.
        /// </summary>
        public String ColumnName {
            get {
                return columnName;
            }
            set {
                columnName = value;
            }
        }
		
        /// <summary>
        /// Data type of the column.
        /// </summary>
        public String DBType {
            get {
                return dbType;
            }
            set {
                dbType = value;
            }
        }

        /// <summary>
        /// Length in bytes of the column.
        /// </summary>
        public Int32 Length {
            get {
                return length;
            }
            set {
                length = value;
            }
        }
		
        /// <summary>
        /// Precision of the column.  Applicable to decimal, float, and numeric data types only.
        /// </summary>
        public Int32 Precision {
            get {
                return precision;
            }
            set {
                precision = value;
            }
        }
		
        /// <summary>
        /// Scale of the column.  Applicable to decimal, and numeric data types only.
        /// </summary>
        public Int32 Scale {
            get {
                return scale;
            }
            set {
                scale = value;
            }
        }
		
        /// <summary>
        /// Flags the column as a uniqueidentifier column.
        /// </summary>
        public Boolean IsRowGuidCol {
            get {
                return isRowGuidCol;
            }
            set {
                isRowGuidCol = value;
                if (isRowGuidCol) {
                    dbType = "uniqueidentifier";
                    length = 16;
                }
            }
        }

        /// <summary>
        /// Flags the column as an identity column.
        /// </summary>
        public Boolean IsIdentity {
            get {
                return isIdentity;
            }
            set {
                isIdentity = value;
                if (isIdentity) {
                    dbType = "int";
                    length = 4;
                }
            }
        }

        /// <summary>
        /// Flags the column as a primary key.
        /// </summary>
        public Boolean IsPrimaryKey {
            get {
                return isPrimaryKey;
            }
            set {
                isPrimaryKey = value;
            }
        }

        /// <summary>
        /// Flags the column as a foreign key.
        /// </summary>
        public Boolean IsForeignKey {
            get {
                return isForeignKey;
            }
            set {
                isForeignKey = value;
            }
        }

        public Boolean IsViewColumn {
            get { return isViewColumn;	}
            set { isViewColumn = value; }
        }


        /// <summary>
        /// Creates a String for a method parameter representing the specified field.
        /// </summary>
        /// <param name="this">Object that stores the information for the field the parameter represents.</param>
        /// <returns>String containing parameter information of the specified field for a method call.</returns>
        public String ParameterType {
            get {
                switch (dbType.ToLower()) {
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
                        return "byte[]";
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
                        throw(new Exception("Invalid SQL Server data type specified: " + dbType));
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
                    return "Image";
                case "int":
                    return "Int32";
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
            String[]	strMethodParameter;
			
            // Get an array of data types and variable names
            strMethodParameter = this.CreateMethodParameter().Split(new Char[] {' '});

            // this needs to be cleaned up!!!!			
            // Is the parameter used for input or output
            if (blnOutput)
                //return "Int32 " +  strMethodParameter[1] +"=0; // dummy value so that parameter can be declared output - should use return value\n" +"objCommand.Parameters.Add(new SqlParameter(\"@" + this.ColumnName + "\", SqlDbType." + GetSqlDbType(this.DBType) + ", " + this.Length + ", ParameterDirection.Output, false, " + this.Precision + ", " + this.Scale + ", \"" + this.ColumnName + "\", DataRowVersion.Proposed, " + strMethodParameter[1] + "));\n";
                if (useDataObject)
                    return "objCommand.Parameters.Add(new SqlParameter(\"@" + this.ColumnName + "\", SqlDbType." + GetSqlDbType(this.DBType) + ", " + this.Length + ", ParameterDirection.Output, false, " + this.Precision + ", " + this.Scale + ", \"" + this.ColumnName + "\", DataRowVersion.Proposed, data." + this.ColumnName + "));\n";
                else
                    return "objCommand.Parameters.Add(new SqlParameter(\"@" + this.ColumnName + "\", SqlDbType." + GetSqlDbType(this.DBType) + ", " + this.Length + ", ParameterDirection.Output, false, " + this.Precision + ", " + this.Scale + ", \"" + this.ColumnName + "\", DataRowVersion.Proposed, " + strMethodParameter[1] + "));\n";
            else
                //return "objCommand.Parameters.Add(new SqlParameter(\"@" + this.ColumnName + "\", SqlDbType." + GetSqlDbType(this.DBType) + ", " + this.Length + ", ParameterDirection.Input, false, " + this.Precision + ", " + this.Scale + ", \"" + this.ColumnName + "\", DataRowVersion.Proposed, " + strMethodParameter[1] + "));\n";
                if (useDataObject)
                return "objCommand.Parameters.Add(new SqlParameter(\"@" + this.ColumnName + "\", SqlDbType." + GetSqlDbType(this.DBType) + ", " + this.Length + ", ParameterDirection.Input, false, " + this.Precision + ", " + this.Scale + ", \"" + this.ColumnName + "\", DataRowVersion.Proposed, data." + this.ColumnName + "));\n";
            else
                return "objCommand.Parameters.Add(new SqlParameter(\"@" + this.ColumnName + "\", SqlDbType." + GetSqlDbType(this.DBType) + ", " + this.Length + ", ParameterDirection.Input, false, " + this.Precision + ", " + this.Scale + ", \"" + this.ColumnName + "\", DataRowVersion.Proposed, " + strMethodParameter[1] + "));\n";
        }

        public override String ToString() {
            return columnName;
        }

        public String GetFieldFormat() {
            return this.ColumnName.Substring(0, 1).ToLower() + this.ColumnName.Substring(1);
        }

        public String GetMethodFormat() {
            return this.ColumnName.Substring(0, 1).ToUpper() + this.ColumnName.Substring(1);
        }

        /// <summary>
        /// Creates a string for a method parameter representing the specified field.
        /// </summary>
        /// <returns>String containing parameter information of the specified field for a method call.</returns>
        public string CreateMethodParameter() {
            string		strParameter;
            string	strColumnName;

            // Format the column name
            strColumnName = this.ColumnName;
            strColumnName = strColumnName.Substring(0, 1).ToUpper() + strColumnName.Substring(1); 
		
            switch (this.DBType.ToLower()) {
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
                    strParameter = "byte[] byte" + strColumnName.Replace(" ", "_");
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
                    throw(new Exception("Invalid SQL Server data type specified: " + this.DBType));
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
		
            switch (this.DBType) {
                case "binary":
                    strParameter = "@" + this.ColumnName.Replace(" ", "_") + "\t" + this.DBType + "(" + this.Length + ")";
                    break;
                case "bigint":
                    strParameter = "@" + this.ColumnName.Replace(" ", "_") + "\t" + this.DBType;
                    break;
                case "bit":
                    strParameter = "@" + this.ColumnName.Replace(" ", "_") + "\t" + this.DBType;
                    break;
                case "char":
                    strParameter = "@" + this.ColumnName.Replace(" ", "_") + "\t" + this.DBType + "(" + this.Length + ")";
                    break;
                case "datetime":
                    strParameter = "@" + this.ColumnName.Replace(" ", "_") + "\t" + this.DBType;
                    break;
                case "decimal":
                    if (this.Scale == 0)
                        strParameter = "@" + this.ColumnName.Replace(" ", "_") + "\t" + this.DBType + "(" + this.Precision + ")";
                    else
                        strParameter = "@" + this.ColumnName.Replace(" ", "_") + "\t" + this.DBType + "(" + this.Precision + ", "+ this.Scale + ")";
                    break;
                case "float":
                    strParameter = "@" + this.ColumnName.Replace(" ", "_") + "\t" + this.DBType + "(" + this.Precision + ")";
                    break;
                case "image":
                    strParameter = "@" + this.ColumnName.Replace(" ", "_") + "\t" + this.DBType;
                    break;
                case "int":
                    strParameter = "@" + this.ColumnName.Replace(" ", "_") + "\t" + this.DBType;
                    break;
                case "money":
                    strParameter = "@" + this.ColumnName.Replace(" ", "_") + "\t" + this.DBType;
                    break;
                case "nchar":
                    strParameter = "@" + this.ColumnName.Replace(" ", "_") + "\t" + this.DBType + "(" + this.Length + ")";
                    break;
                case "ntext":
                    strParameter = "@" + this.ColumnName.Replace(" ", "_") + "\t" + this.DBType;
                    break;
                case "nvarchar":
                    strParameter = "@" + this.ColumnName.Replace(" ", "_") + "\t" + this.DBType + "(" + this.Length + ")";
                    break;
                case "numeric":
                    if (this.Scale == 0)
                        strParameter = "@" + this.ColumnName.Replace(" ", "_") + "\t" + this.DBType + "(" + this.Precision + ")";
                    else
                        strParameter = "@" + this.ColumnName.Replace(" ", "_") + "\t" + this.DBType + "(" + this.Precision + ", "+ this.Scale + ")";
                    break;
                case "real":
                    strParameter = "@" + this.ColumnName.Replace(" ", "_") + "\t" + this.DBType;
                    break;
                case "smalldatetime":
                    strParameter = "@" + this.ColumnName.Replace(" ", "_") + "\t" + this.DBType;
                    break;
                case "smallint":
                    strParameter = "@" + this.ColumnName.Replace(" ", "_") + "\t" + this.DBType;
                    break;
                case "smallmoney":
                    strParameter = "@" + this.ColumnName.Replace(" ", "_") + "\t" + this.DBType;
                    break;
                case "sql_variant":
                    strParameter = "@" + this.ColumnName.Replace(" ", "_") + "\t" + this.DBType;
                    break;
                case "sysname":
                    strParameter = "@" + this.ColumnName.Replace(" ", "_") + "\t" + this.DBType;
                    break;
                case "text":
                    strParameter = "@" + this.ColumnName.Replace(" ", "_") + "\t" + this.DBType;
                    break;
                case "timestamp":
                    strParameter = "@" + this.ColumnName.Replace(" ", "_") + "\t" + this.DBType;
                    break;
                case "tinyint":
                    strParameter = "@" + this.ColumnName.Replace(" ", "_") + "\t" + this.DBType;
                    break;
                case "varbinary":
                    strParameter = "@" + this.ColumnName.Replace(" ", "_") + "\t" + this.DBType + "(" + this.Length + ")";
                    break;
                case "varchar":
                    strParameter = "@" + this.ColumnName.Replace(" ", "_") + "\t" + this.DBType + "(" + this.Length + ")";
                    break;
                case "uniqueidentifier":
                    strParameter = "@" + this.ColumnName.Replace(" ", "_") + "\t" + this.DBType;
                    break;
                default:  // Unknow data type
                    throw(new Exception("Invalid SQL Server data type specified: " + this.DBType.ToLower()));
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
            Int32 intIndex;
            Field objField;

            for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
                objField = (Field)arrFieldList[intIndex];
                if (objField.IsIdentity) {
                    return objField;
                }
            }

            return new Field();
        }



    }
}
