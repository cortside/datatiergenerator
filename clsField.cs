using System;

namespace DataTierGenerator
{
	/// <summary>
	/// Class that stores information for fields in SQL Server tables.
	/// </summary>
	public class clsField
	{
		// Private variable used to hold the property values
		string	m_strColumnName;
		string	m_strType;
		string	m_strLength;
		string	m_strPrecision;
		string	m_strScale;
		bool	m_blnIsRowGuidCol;
		bool	m_blnIsIdentity;
		bool	m_blnIsPrimaryKey;
		bool	m_blnIsForeignKey;
	
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
			objField.Type = m_strType;
			
			return objField;
		}

		/// <summary>
		/// Name of the column.
		/// </summary>
		public string ColumnName {
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
		public string Type {
			get {
				return m_strType;
			}
			set {
				m_strType = value;
			}
		}

		/// <summary>
		/// Length in bytes of the column.
		/// </summary>
		public string Length {
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
		public string Precision {
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
		public string Scale {
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
					m_strType = "uniqueidentifier";
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
					m_strType = "int";
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
	}
}
