using System;

namespace DataTierGenerator
{
	/// <summary>
	/// Class that stores information for fields in SQL Server tables.
	/// </summary>
	public class clsField
	{
		// Private variable used to hold the property values
		String	m_strColumnName;
		String	m_strDBType;
		String	m_strLength;
		String	m_strPrecision;
		String	m_strScale;
		bool	m_blnIsRowGuidCol;
		bool	m_blnIsIdentity;
		bool	m_blnIsPrimaryKey;
		bool	m_blnIsForeignKey;
		bool	m_blnIsViewColumn;
	
		/// <summary>
		/// Copies one field object to another.
		/// </summary>
		public clsField Copy() {
			clsField	objField;
			
			objField = new clsField();
			objField.ColumnName = m_strColumnName;
			objField.IsIdentity = m_blnIsIdentity;
			objField.IsForeignKey = m_blnIsForeignKey;
			objField.IsPrimaryKey = m_blnIsPrimaryKey;
			objField.IsRowGuidCol = m_blnIsRowGuidCol;
			objField.Length = m_strLength;
			objField.Precision = m_strPrecision;
			objField.Scale = m_strScale;
			objField.DBType = m_strDBType;
			
			return objField;
		}

		/// <summary>
		/// Name of the column.
		/// </summary>
		public String ColumnName {
			get {
				return m_strColumnName;
			}
			set {
				m_strColumnName = value;
			}
		}
		
		/// <summary>
		/// Data type of the column.
		/// </summary>
		public String DBType {
			get {
				return m_strDBType;
			}
			set {
				m_strDBType = value;
			}
		}

		/// <summary>
		/// Length in bytes of the column.
		/// </summary>
		public String Length {
			get {
				return m_strLength;
			}
			set {
				m_strLength = value;
			}
		}
		
		/// <summary>
		/// Precision of the column.  Applicable to decimal, float, and numeric data types only.
		/// </summary>
		public String Precision {
			get {
				return m_strPrecision;
			}
			set {
				m_strPrecision = value;
			}
		}
		
		/// <summary>
		/// Scale of the column.  Applicable to decimal, and numeric data types only.
		/// </summary>
		public String Scale {
			get {
				return m_strScale;
			}
			set {
				m_strScale = value;
			}
		}
		
		/// <summary>
		/// Flags the column as a uniqueidentifier column.
		/// </summary>
		public bool IsRowGuidCol {
			get {
				return m_blnIsRowGuidCol;
			}
			set {
				m_blnIsRowGuidCol = value;
				if (m_blnIsRowGuidCol) {
					m_strDBType = "uniqueidentifier";
					m_strLength = "16";
				}
			}
		}

		/// <summary>
		/// Flags the column as an identity column.
		/// </summary>
		public bool IsIdentity {
			get {
				return m_blnIsIdentity;
			}
			set {
				m_blnIsIdentity = value;
				if (m_blnIsIdentity) {
					m_strDBType = "int";
					m_strLength = "4";
				}
			}
		}

		/// <summary>
		/// Flags the column as a primary key.
		/// </summary>
		public bool IsPrimaryKey {
			get {
				return m_blnIsPrimaryKey;
			}
			set {
				m_blnIsPrimaryKey = value;
			}
		}

		/// <summary>
		/// Flags the column as a foreign key.
		/// </summary>
		public bool IsForeignKey {
			get {
				return m_blnIsForeignKey;
			}
			set {
				m_blnIsForeignKey = value;
			}
		}

		public bool IsViewColumn {
			get { return m_blnIsViewColumn;	}
			set { m_blnIsViewColumn = value; }
		}


		/// <summary>
		/// Creates a String for a method parameter representing the specified field.
		/// </summary>
		/// <param name="objField">Object that stores the information for the field the parameter represents.</param>
		/// <returns>String containing parameter information of the specified field for a method call.</returns>
		public String ParameterType {
			get {
				switch (m_strDBType.ToLower()) {
					case "binary":
						return "byte[]";
					case "bigint":
						return "Int64";
					case "bit":
						return "bool";
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
						throw(new Exception("Invalid SQL Server data type specified: " + m_strDBType));
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

		public override String ToString() {
			return m_strColumnName;
		}

	}
}
