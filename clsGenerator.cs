// This component makes extensive use of inline documentation comments.
// In Visual Studio .NET, select <Build Comment Web Pages> on the <Tools> menu to create HTML documentation of the class.

using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Text;
using DataTierGenerator;

namespace DataTierGenerator {
	/// <summary>
	/// Generates stored procedures and associated data access code for the specified database.
	/// </summary>
	public class clsGenerator {
		// Connection to the database
		private	SqlConnection	m_objConnection;
		private StreamWriter	m_objStreamWriter;

		private Boolean			m_blnSingleFile;
		private Boolean			m_blnCreateViews;
		private Boolean			m_blnUseViews;
		private Boolean			m_blnCreateDataObjects;
		private Boolean			m_blnScriptDropStatement;
		private String			m_strStoredProcNameFormat;
		private String			m_strProjectNameSpace;

		/// <summary>
		/// Contructor for the Generator class.
		/// </summary>
		/// <param name="strConnectionString">Connecion string to a SQL Server database.</param>
		public clsGenerator(string strConnectionString, String strStoredProcNameFormat, Boolean blnSingleFile, Boolean blnCreateViews, Boolean blnUseViews, Boolean blnCreateDataObjects, Boolean blnScriptDropStatement, String strProjectNamespace) {
			m_objConnection = new SqlConnection(strConnectionString);
			m_objConnection.Open();
			m_blnSingleFile = blnSingleFile;
			m_blnCreateViews = blnCreateViews;
			m_blnUseViews = blnUseViews;
			m_blnCreateDataObjects = blnCreateDataObjects;
			m_blnScriptDropStatement = blnScriptDropStatement;
			m_strStoredProcNameFormat = strStoredProcNameFormat;
			m_strProjectNameSpace = strProjectNamespace;
		}
		
		/// <summary>
		/// Generates SQL scripts for the stored procedures for each table in the database.
		/// </summary>
		public void ProcessTables() {
			SqlDataAdapter	objDataAdapter;
			DataTable		objDataTable;
			string			strTableName;
			string			strFileName;

			// Check to see if the "SQL Scripts" directory exists; if not, create it; otherwise, clear it out
			if (!Directory.Exists("SQL Scripts"))
				Directory.CreateDirectory("SQL Scripts");

			// Check to see if the "Data Access Classes" directory exists; if not, create it; otherwise, clear it out
			if (!Directory.Exists("Data Access Classes"))
				Directory.CreateDirectory("Data Access Classes");

			// Check to see if the "Data Object Classes" directory exists; if not, create it; otherwise, clear it out
			if (!Directory.Exists("Data Object Classes"))
				Directory.CreateDirectory("Data Object Classes");

			// Get a list of the entities in the database
			objDataTable = new DataTable();
			objDataAdapter = new SqlDataAdapter("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG = '" + m_objConnection.Database + "'", m_objConnection);
			objDataAdapter.Fill(objDataTable);
			
			// Create and open the file stream - if in single file mode
			if (m_blnSingleFile) {
				strFileName = "SQL Scripts\\" + m_objConnection.Database.Replace(" ", "_") + ".sql";
				if (File.Exists(strFileName))
					File.Delete(strFileName);
				m_objStreamWriter = new StreamWriter(strFileName);
			}

			// Process each table
			foreach (DataRow objDataRow in objDataTable.Rows) {
				if (objDataRow["TABLE_TYPE"].ToString() == "BASE TABLE" && objDataRow["TABLE_NAME"].ToString() != "dtproperties") {
					strTableName = objDataRow["TABLE_NAME"].ToString();
					ProcessTable(strTableName);
				}
			}
			
			// Close and deallocate the file stream
			if (m_blnSingleFile) {
				m_objStreamWriter.Close();
				m_objStreamWriter = null;
			}
		}

		
		/// <summary>
		/// Processes the specified table, creating stored procedures and C# data access classes for it.
		/// </summary>
		/// <param name="strTableName">Name of the table to be processed.</param>
		private void ProcessTable(string strTableName) {
			ArrayList		arrFieldList;
			int				intIndex;
			clsField		objField;
			SqlDataAdapter	objDataAdapter;
			DataTable		objDataTable;
			DataTable		objDataTableConstraint;
			int				intLength;
			String			sql;


			sql = "	SELECT	INFORMATION_SCHEMA.COLUMNS.*, ";
 			sql = sql + " 		systypes.length AS COLUMN_LENGTH, ";
 			sql = sql + " 		syscolumns.iscomputed AS COLUMN_COMPUTED, ";
 			sql = sql + "		'0' VIEW_COLUMN, ";
 			sql = sql + "		coalesce(VC.colid, 1000+ORDINAL_POSITION) COLUMN_ID ";
 			sql = sql + " 	FROM INFORMATION_SCHEMA.COLUMNS ";
 			sql = sql + "  	INNER JOIN systypes ON INFORMATION_SCHEMA.COLUMNS.DATA_TYPE = systypes.name ";
 			sql = sql + "  	INNER JOIN syscolumns ON INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME = syscolumns.name  AND syscolumns.id = OBJECT_ID('" + strTableName + "') ";
 			sql = sql + "	left join syscolumns vc on INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME = vc.name AND vc.id = OBJECT_ID('" + strTableName + "') ";
 			sql = sql + "  	WHERE INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = '" + strTableName + "' ";

			// if basing data objects on views, get additional fields found in the corresponding view (by naming convention of vw + tablename) -- should be configuration option
			if (m_blnUseViews) {
 				sql = sql + "union ";
 				sql = sql + " 	SELECT	INFORMATION_SCHEMA.COLUMNS.*, ";
 				sql = sql + "  		systypes.length AS COLUMN_LENGTH, ";
 				sql = sql + "  		syscolumns.iscomputed AS COLUMN_COMPUTED, ";
 				sql = sql + " 		'1' VIEW_COLUMN, ";
 				sql = sql + "		ORDINAL_POSITION COLUMN_ID ";
 				sql = sql + " 	FROM INFORMATION_SCHEMA.COLUMNS ";
 				sql = sql + " 	INNER JOIN systypes ON INFORMATION_SCHEMA.COLUMNS.DATA_TYPE = systypes.name ";
 				sql = sql + " 	INNER JOIN syscolumns ON INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME = syscolumns.name ";
 				sql = sql + " 	WHERE INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = 'vw" + strTableName + "' AND syscolumns.id = OBJECT_ID('vw" + strTableName + "') ";
 				sql = sql + " 	and INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME not in (select INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = '" + strTableName + "') ";
			}

 			sql = sql + "order by column_id ";

			// Fill the dataset with the information for the current table
			objDataTable = new DataTable();
			objDataAdapter = new SqlDataAdapter(sql, m_objConnection);
			objDataAdapter.Fill(objDataTable);

			// Store each field's information in the field list
			arrFieldList = new ArrayList();
			foreach (DataRow objDataRow in objDataTable.Rows) {
				if (objDataRow["COLUMN_COMPUTED"].ToString() == "0") {
					// Create the array
					objField = new clsField();
					objField.ColumnName = objDataRow["COLUMN_NAME"].ToString();
					objField.DBType = objDataRow["DATA_TYPE"].ToString();
					if (objDataRow["CHARACTER_MAXIMUM_LENGTH"].ToString().Length > 0)
						objField.Length = objDataRow["CHARACTER_MAXIMUM_LENGTH"].ToString();
					else
						objField.Length = objDataRow["COLUMN_LENGTH"].ToString();
					objField.Precision = objDataRow["NUMERIC_PRECISION"].ToString();
					objField.Scale = objDataRow["NUMERIC_SCALE"].ToString();
					objField.IsPrimaryKey = false;
					objField.IsViewColumn = objDataRow["VIEW_COLUMN"].ToString() == "1";

					// Check for unicode columns
					if (objField.DBType.ToLower() == "nchar" || objField.DBType.ToLower() == "nvarchar" || objField.DBType.ToLower() == "ntext") {
						intLength = Int32.Parse(objField.Length);
						intLength /= 2;
						objField.Length = intLength.ToString();
					}
					
					// Check for text or ntext columns, which require a different length from what SQL Server reports
					if (objField.DBType.ToLower() == "text")
						objField.Length = "2147483647";
					else if (objField.DBType.ToLower() == "ntext")
						objField.Length = "1073741823";

					// Check to see if the current field is a primary key
					objDataTableConstraint = new DataTable();
					objDataAdapter = new SqlDataAdapter("SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS ON INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.CONSTRAINT_NAME = INFORMATION_SCHEMA.TABLE_CONSTRAINTS.CONSTRAINT_NAME WHERE INFORMATION_SCHEMA.TABLE_CONSTRAINTS.TABLE_NAME = '" + strTableName + "'", m_objConnection);
					objDataAdapter.Fill(objDataTableConstraint);
					
					foreach (DataRow objDataRowConstraint in objDataTableConstraint.Rows) {
						if (objDataRowConstraint["COLUMN_NAME"].ToString() == objField.ColumnName && objDataRowConstraint["CONSTRAINT_TYPE"].ToString() == "PRIMARY KEY") {
							objField.IsPrimaryKey = true;
						}
					}
					
					// Append the array to the array list
					arrFieldList.Add(objField);
					objField = null;
					objDataTableConstraint = null;
				}
			}
			
			// Check for an identity column
			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];
				
				objDataTable = new DataTable();
				objDataAdapter = new SqlDataAdapter("SELECT ColumnProperty(OBJECT_ID('" + strTableName + "'), '" + objField.ColumnName + "', 'IsIdentity') AS IsIdentity", m_objConnection);
				objDataAdapter.Fill(objDataTable);
				
				if (objDataTable.Rows.Count > 0) {
					if (objDataTable.Rows[0]["IsIdentity"].ToString() == "1") {
						objField.IsIdentity = true;
						break;
					}
				}
				objDataTable = null;
				objDataAdapter = null;
			}
			
			// Check for a row GUID column
			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];
				
				objDataTable = new DataTable();
				objDataAdapter = new SqlDataAdapter("SELECT ColumnProperty(OBJECT_ID('" + strTableName + "'), '" + objField.ColumnName + "', 'IsRowGuidCol') AS IsRowGuidCol", m_objConnection);
				objDataAdapter.Fill(objDataTable);
				
				if (objDataTable.Rows.Count > 0) {
					if (objDataTable.Rows[0]["IsRowGuidCol"].ToString() == "1") {
						objField.IsRowGuidCol = true;
						break;
					}
				}
				objDataTable = null;
				objDataAdapter = null;
			}

			// Check for foreign keys
			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];
				
				objDataTable = new DataTable();
				objDataAdapter = new SqlDataAdapter("SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS ON INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.CONSTRAINT_NAME = INFORMATION_SCHEMA.TABLE_CONSTRAINTS.CONSTRAINT_NAME WHERE INFORMATIoN_SCHEMA.TABLE_CONSTRAINTS.TABLE_NAME = '" + strTableName + "' AND CONSTRAINT_TYPE = 'FOREIGN KEY' AND COLUMN_NAME = '" + objField.ColumnName + "'", m_objConnection);
				objDataAdapter.Fill(objDataTable);
				
				if (objDataTable.Rows.Count > 0)
					objField.IsForeignKey = true;

				objDataTable = null;
				objDataAdapter = null;
			}

			// create views
			CreateView(strTableName, arrFieldList);

			// Create the stored procedures
			CreateInsertStoredProcedure(strTableName, arrFieldList);
			CreateUpdateStoredProcedure(strTableName, arrFieldList);
			CreateDeleteStoredProcedures(strTableName, arrFieldList);
			CreateSelectStoredProcedures(strTableName, arrFieldList);

			// create classes
			CreateDataObjectClass(strTableName, arrFieldList);
			CreateDataAccessClass(strTableName, arrFieldList);
		}


		/// <summary>
		/// Creates an insert stored procedure SQL script for the specified table
		/// </summary>
		/// <param name="strTableName">Name of the table the stored procedure should be generated from.</param>
		/// <param name="arrFieldList">ArrayList object containing one or more clsField objects as defined in the table.</param>
		private void CreateInsertStoredProcedure(string strTableName, ArrayList arrFieldList) {
			clsField		objField;
			int				intIndex;
			StringBuilder	objStringBuilder;
			String			strProcName;
			
			// Create the SQL for the stored procedure
			objStringBuilder = new StringBuilder(1024);

			strProcName = getProcName(strTableName, "Insert");

			objStringBuilder.Append("CREATE PROCEDURE " + strProcName + "\n\n");

			// Create the parameter list
			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];
				if (!objField.IsViewColumn) {
					if (intIndex>0) {
						objStringBuilder.Append(",\n");
					}
					objStringBuilder.Append(CreateParameterString(objField, true));
				}
				objField = null;
			}
			objStringBuilder.Append("\n");

			objStringBuilder.Append("\nAS\n\n");
			
			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];
				if (!objField.IsViewColumn) {
					if (objField.IsRowGuidCol) {
						objStringBuilder.Append("SET @" + objField.ColumnName.Replace(" ", "_") + " = @@NEWID()\n\n");
						break;
					}
				}
			}
			
			objStringBuilder.Append("INSERT INTO [" + strTableName + "]\n(\n");
			
			// Create the parameter list
			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];
				if (!objField.IsViewColumn) {
					// Is the current field an identity column?
					if (objField.IsIdentity == false) {
						if (intIndex>0) {
							objStringBuilder.Append(",\n");
						}
						objStringBuilder.Append("\t[" + objField.ColumnName + "]");
					}
				}
				objField = null;
			}
			objStringBuilder.Append("\n");			

			objStringBuilder.Append(")\nVALUES\n(\n");

			// Create the parameter list
			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];
				if (!objField.IsViewColumn) {
					// Is the current field an identity column?
					if (objField.IsIdentity == false) {
						if (intIndex>0) {
							objStringBuilder.Append(",\n");
						}
						objStringBuilder.Append("\t@" + objField.ColumnName.Replace(" ", "_"));
					}
				}
				objField = null;
			}
			objStringBuilder.Append("\n");
			objStringBuilder.Append(")\n");

			// Should we include a line for returning the identity?
			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];
				if (!objField.IsViewColumn) {
					// Is the current field an identity column?
					if (objField.IsIdentity)
						objStringBuilder.Append("\nSET @" + objField.ColumnName.Replace(" ", "_") + " = @@IDENTITY\n");
				}
				objField = null;
			}
			
			// Write out the stored procedure
			WriteToFile(strProcName, objStringBuilder.ToString() + "\nGO\n\n");
			objStringBuilder = null;
		}

		
		/// <summary>
		/// Creates an update stored procedure SQL script for the specified table
		/// </summary>
		/// <param name="strTableName">Name of the table the stored procedure should be generated from.</param>
		/// <param name="arrFieldList">ArrayList object containing one or more clsField objects as defined in the table.</param>
		private void CreateUpdateStoredProcedure(string strTableName, ArrayList arrFieldList) {
			clsField		objField;
			clsField		objOldField;
			clsField		objNewField;
			int				intIndex;
			StringBuilder	objStringBuilder;
			int				intWhereClauseCount;

			String			strProcName;
			
			// Create the SQL for the stored procedure
			objStringBuilder = new StringBuilder(1024);

			strProcName = getProcName(strTableName, "Update");

			objStringBuilder.Append("CREATE PROCEDURE " + strProcName + "\n\n");

			// Create the parameter list
			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];
				if (!objField.IsViewColumn) {
					if (intIndex>0) {
						objStringBuilder.Append(",\n");
					}
					if (objField.IsPrimaryKey && objField.IsIdentity == false && objField.IsRowGuidCol == false) {
						objOldField = objField.Copy();
						objOldField.ColumnName = "Old" + objOldField.ColumnName;
					
						objNewField = objField.Copy();
						objNewField.ColumnName = "New" + objNewField.ColumnName;
					
						objStringBuilder.Append(CreateParameterString(objOldField, false));
						objStringBuilder.Append(",\n");
						objStringBuilder.Append(CreateParameterString(objNewField, false));
					} else {
						objStringBuilder.Append(CreateParameterString(objField, false));
					}
				}
				objField = null;
			}
			objStringBuilder.Append("\n");

			objStringBuilder.Append("\nAS\n");
			objStringBuilder.Append("\nUPDATE\n\t[" + strTableName + "]\n");
			objStringBuilder.Append("SET\n");
			
			// Create the set statement
			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];
				if (!objField.IsViewColumn) {
					if (objField.IsIdentity == false && objField.IsRowGuidCol == false) {
						if (intIndex>0) {
							objStringBuilder.Append(",\n");
						}
						if (objField.IsPrimaryKey) {
							objStringBuilder.Append("\t[" + objField.ColumnName + "] = @New" + objField.ColumnName.Replace(" ", "_"));
						} else {
							objStringBuilder.Append("\t[" + objField.ColumnName + "] = @" + objField.ColumnName.Replace(" ", "_"));
						}
					}
				}
				objField = null;
			}
			objStringBuilder.Append("\n");
			objStringBuilder.Append("WHERE\n");
			
			// Create the where clause
			intWhereClauseCount = 0;
			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];
				if (!objField.IsViewColumn) {
					if (objField.IsIdentity || objField.IsRowGuidCol || objField.IsPrimaryKey) {
						intWhereClauseCount++;
				
						if (intIndex == (arrFieldList.Count  - 1))
							objStringBuilder.Append("\t");
						else if (intIndex < (arrFieldList.Count - 1) && intIndex > 0 && intWhereClauseCount > 1)
							objStringBuilder.Append("\tAND ");
						else
							objStringBuilder.Append("\t");
					
						if (objField.IsPrimaryKey && objField.IsIdentity == false && objField.IsRowGuidCol == false)
							objStringBuilder.Append("[" + objField.ColumnName + "] = @Old" + objField.ColumnName.Replace(" ", "_") + "\n");
						else
							objStringBuilder.Append("[" + objField.ColumnName + "] = @" + objField.ColumnName.Replace(" ", "_") + "\n");
					}
				}
				objField = null;
			}

			// Write out the stored procedure
			WriteToFile(strProcName, objStringBuilder.ToString() + "\nGO\n\n");
			objStringBuilder = null;
		}


		/// <summary>
		/// Creates delete stored procedures for the specified table
		/// </summary>
		/// <param name="strTableName">Name of the table the stored procedure should be generated from.</param>
		/// <param name="arrFieldList">ArrayList object containing one or more clsField objects as defined in the table.</param>
		private void CreateDeleteStoredProcedures(string strTableName, ArrayList arrFieldList) {
			ArrayList		arrKeyList;
			clsField		objField;
			int				intIndex;
			string			strColumnName;
			StringBuilder	objStringBuilder;
			string			strPrimaryKeyList;

			// Create the array list of key fields
			strPrimaryKeyList = "";
			arrKeyList = new ArrayList();
			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];
				if (objField.IsPrimaryKey) {
					arrKeyList.Add(objField);
					strPrimaryKeyList += objField.ColumnName.Replace(" ", "_") + "_";
				}
				objField = null;
			}
			
			// Trim off the last underscore
			if (strPrimaryKeyList.Length > 0)
				strPrimaryKeyList = strPrimaryKeyList.Substring(0, strPrimaryKeyList.Length - 1);

			/************************************************************************************/
			// Create the stored procedures, with parameters for each identity, RowGuidCol, primary key, or foreign key column
			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];

				// Is is an identity or uniqueidentifier column?
				if (objField.IsIdentity || objField.IsRowGuidCol || objField.IsPrimaryKey || objField.IsForeignKey) {
					// Format the column name to make sure the first character is upper case
					strColumnName = objField.ColumnName;
					strColumnName = strColumnName.Substring(0, 1).ToUpper() + strColumnName.Substring(1);
				
					// Create the SQL for the stored procedure
					String			strProcName;
			
					// Create the SQL for the stored procedure
					objStringBuilder = new StringBuilder(1024);

					strProcName = getProcName(strTableName, "DeleteBy" + strColumnName.Replace(" ", "_"));

					objStringBuilder.Append("CREATE PROCEDURE " + strProcName + "\n\n");

					objStringBuilder.Append("@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType);
					objStringBuilder.Append("\n\nAS\n\n");
					objStringBuilder.Append("DELETE\n");
					objStringBuilder.Append("FROM\n\t[" + strTableName + "]\n");
					objStringBuilder.Append("WHERE \n");
					objStringBuilder.Append("\t[" + objField.ColumnName + "] = @" + objField.ColumnName.Replace(" ", "_") + "\n");
					
					// Write out the stored procedure
					WriteToFile(strProcName, objStringBuilder.ToString() + "\nGO\n\n");
					objStringBuilder = null;
				}
			}
				
			/************************************************************************************/
			// Create the stored procedure for a composite primary key
			if (arrKeyList.Count > 1) {
				// Create the SQL for the stored procedure
				String			strProcName;
			
				// Create the SQL for the stored procedure
				objStringBuilder = new StringBuilder(1024);

				strProcName = getProcName(strTableName, "DeleteBy" + strPrimaryKeyList);

				objStringBuilder.Append("CREATE PROCEDURE " + strProcName + "\n\n");				
				
				//// Is this a self-referencing key?
				//objStringBuilder.Append("CREATE PROCEDURE proc" + strTableName.Replace(" ", "_") + "DeleteBy" + strPrimaryKeyList + "\n\n");

				// Create the parameter list
				for (intIndex = 0; intIndex < arrKeyList.Count; intIndex++) {
					objField = (clsField)arrKeyList[intIndex];
					objStringBuilder.Append("@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType);
					objField = null;
					
					if (intIndex == arrKeyList.Count)
						objStringBuilder.Append("\n");
					else
						objStringBuilder.Append(",\n");
				}
				
				objStringBuilder.Append("\n\nAS\n\n");
				objStringBuilder.Append("DELETE\n");
				objStringBuilder.Append("FROM\n\t[" + strTableName + "]\n");
				objStringBuilder.Append("WHERE \n");

				// Create the where clause
				for (intIndex = 0; intIndex < arrKeyList.Count; intIndex++) {
					objField = (clsField)arrFieldList[intIndex];
					objStringBuilder.Append("\t[" + objField.ColumnName + "] = @" + objField.ColumnName.Replace(" ", "_"));
					
					if (intIndex != arrFieldList.Count)
						objStringBuilder.Append(",\n");
					else
						objStringBuilder.Append("\n");
				}
				
				// Write out the stored procedure
				WriteToFile(strProcName, objStringBuilder.ToString() + "\nGO\n\n");
				objStringBuilder = null;
			}
		}


		/// <summary>
		/// Creates a select stored procedure SQL script for the specified table by the table's identity or uniqueidentifier column,
		///	and select stored procedures for all foreign keys columns in the table.
		/// </summary>
		/// <param name="strTableName">Name of the table the stored procedure should be generated from.</param>
		/// <param name="arrFieldList">ArrayList object containing one or more clsField objects as defined in the table.</param>
		private void CreateSelectStoredProcedures(string strTableName, ArrayList arrFieldList) {
			ArrayList		arrKeyList;
			clsField		objField;
			string			strColumnName;
			int				intIndex;
			StringBuilder	objStringBuilder;
			string			strPrimaryKeyList;

			/************************************************************************************/
			// Create the full list stored procedure
			String			strProcName;
			
			// Create the SQL for the stored procedure
			objStringBuilder = new StringBuilder(1024);

			strProcName = getProcName(strTableName, "Select");

			objStringBuilder.Append("CREATE PROCEDURE " + strProcName + "\n\n");
			objStringBuilder.Append("\n\nAS\n\n");
			objStringBuilder.Append("SELECT\n\t*\n");
			objStringBuilder.Append("FROM\n\t[");
			if (m_blnUseViews)
				objStringBuilder.Append("vw");
			objStringBuilder.Append(strTableName);
			objStringBuilder.Append("]\n");
			
			// Write out the stored procedure
			WriteToFile(strProcName, objStringBuilder.ToString() + "\nGO\n\n");
			objStringBuilder = null;

			// Create the array list of key fields
			strPrimaryKeyList = "";
			arrKeyList = new ArrayList();
			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];
				if (objField.IsPrimaryKey) {
					arrKeyList.Add(objField);
					strPrimaryKeyList += objField.ColumnName.Replace(" ", "_") + "_";
				}
				objField = null;
			}
			
			// Trim off the last underscore
			if (strPrimaryKeyList.Length > 0)
				strPrimaryKeyList = strPrimaryKeyList.Substring(0, strPrimaryKeyList.Length - 1);

			/************************************************************************************/
			// Create the stored procedures, with parameters for each identity, RowGuidCol, primary key, or foreign key column
			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];

				// Is is an identity or uniqueidentifier column?
				if (objField.IsIdentity || objField.IsRowGuidCol || objField.IsPrimaryKey || objField.IsForeignKey) {
					// Format the column name to make sure the first character is upper case
					strColumnName = objField.ColumnName;
					strColumnName = strColumnName.Substring(0, 1).ToUpper() + strColumnName.Substring(1);
				
					// Create the SQL for the stored procedure
					objStringBuilder = new StringBuilder(1024);

					strProcName = getProcName(strTableName, "SelectBy" + strColumnName.Replace(" ", "_"));
					objStringBuilder.Append("CREATE PROCEDURE " + strProcName + "\n\n");

					objStringBuilder.Append("@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType);
					objStringBuilder.Append("\n\nAS\n\n");
					objStringBuilder.Append("SELECT\n\t*\n");
					objStringBuilder.Append("FROM\n\t[");
					if (m_blnUseViews)
						objStringBuilder.Append("vw");
					objStringBuilder.Append(strTableName);
					objStringBuilder.Append("]\n");

					objStringBuilder.Append("WHERE \n");
					objStringBuilder.Append("\t[" + objField.ColumnName + "] = @" + objField.ColumnName.Replace(" ", "_") + "\n");
					
					// Write out the stored procedure
					WriteToFile(strProcName, objStringBuilder.ToString() + "\nGO\n\n");
					objStringBuilder = null;
				}
			}
				
			/************************************************************************************/
			// Create the stored procedure for a composite primary key
			if (arrKeyList.Count > 1) {
				// Create the SQL for the stored procedure
				objStringBuilder = new StringBuilder(1024);

				strProcName = getProcName(strTableName, "SelectBy" + strPrimaryKeyList);
				objStringBuilder.Append("CREATE PROCEDURE " + strProcName + "\n\n");
				
				//// Is this a self-referencing key?
				//objStringBuilder.Append("CREATE PROCEDURE proc" + strTableName.Replace(" ", "_") + "SelectBy" + strPrimaryKeyList + "\n\n");

				// Create the parameter list
				for (intIndex = 0; intIndex < arrKeyList.Count; intIndex++) {
					objField = (clsField)arrKeyList[intIndex];
					objStringBuilder.Append("@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType);
					objField = null;
					
					if (intIndex == arrKeyList.Count)
						objStringBuilder.Append("\n");
					else
						objStringBuilder.Append(",\n");
				}
				
				objStringBuilder.Append("\n\nAS\n\n");
				objStringBuilder.Append("SELECT\n\t*\n");
				objStringBuilder.Append("FROM\n\t[" + strTableName + "]\n");
				objStringBuilder.Append("WHERE \n");

				// Create the where clause
				for (intIndex = 0; intIndex < arrKeyList.Count; intIndex++) {
					objField = (clsField)arrFieldList[intIndex];
					objStringBuilder.Append("\t[" + objField.ColumnName + "] = @" + objField.ColumnName.Replace(" ", "_"));
					
					if (intIndex != arrFieldList.Count)
						objStringBuilder.Append(",\n");
					else
						objStringBuilder.Append("\n");
				}
				
				// Write out the stored procedure
				WriteToFile(strProcName, objStringBuilder.ToString() + "\nGO\n\n");
				objStringBuilder = null;
			}
		}


		/// <summary>
		/// Creates a C# data access class for all of the table's stored procedures.
		/// </summary>
		/// <param name="strTableName">Name of the table the class should be generated for.</param>
		/// <param name="arrFieldList">ArrayList object containing one or more clsField objects as defined in the table.</param>
		private void CreateDataAccessClass(string strTableName, ArrayList arrFieldList) {
			StreamWriter	objStreamWriter;
			StringBuilder	objStringBuilder;
			string			strFileName;
			
			objStringBuilder = new StringBuilder(4096);

			// Create the header for the class
			objStringBuilder.Append("using System;\n");
			objStringBuilder.Append("using System.Data;\n");
			objStringBuilder.Append("using System.Data.SqlClient;\n");
			objStringBuilder.Append("using System.Configuration;\n");
			objStringBuilder.Append("using System.Collections;\n");
			objStringBuilder.Append("using ").Append(getDONameSpace(null, null)).Append(";\n");

			objStringBuilder.Append("\n");
			objStringBuilder.Append("namespace " + getDAONameSpace(m_objConnection.Database.ToString(), strTableName) + " {\n");
			objStringBuilder.Append("\tpublic class " + getDAOClassName(strTableName) + " {\n");

			objStringBuilder.Append("\n\t\tprivate readonly String TABLE=\"").Append(strTableName).Append("\";\n\n");
			objStringBuilder.Append("\n\t\tprivate readonly String VIEW=\"vw").Append(strTableName).Append("\";\n\n");

			CreateDAOListMethods(strTableName, arrFieldList, objStringBuilder);

			// Append the access methods
			CreateInsertMethod(strTableName, arrFieldList, objStringBuilder);
			objStringBuilder.Append("\n\n");
			CreateUpdateMethod(strTableName, arrFieldList, objStringBuilder);
			objStringBuilder.Append("\n\n");
			CreateDeleteMethods(strTableName, arrFieldList, objStringBuilder);
			objStringBuilder.Append("\n\n");
			CreateSelectMethods(strTableName, arrFieldList, objStringBuilder);
		
			// Close out the class and namespace
			objStringBuilder.Append("\t}\n");
			objStringBuilder.Append("}\n");

			// Create the output stream
			strFileName = "Data Access Classes\\" + getDAOClassName(strTableName) + ".cs";
			if (File.Exists(strFileName))
				File.Delete(strFileName);
			objStreamWriter = new StreamWriter(strFileName);
			objStreamWriter.Write(objStringBuilder.ToString());
			objStreamWriter.Close();
			objStreamWriter = null;
			objStringBuilder = null;
		}

		
		/// <summary>
		/// Creates a string that represents the insert functionality of the data access class.
		/// </summary>
		/// <param name="strTableName">Name of the table the data access class is for.</param>
		/// <param name="arrFieldList">ArrayList object containing one or more clsField objects as defined in the table.</param>
		/// <param name="objStringBuilder">StreamBuilder object that the resulting string should be appended to.</param>
		private void CreateInsertMethod(string strTableName, ArrayList arrFieldList, StringBuilder objStringBuilder) {
			clsField	objField;
			int			intIndex;
			bool		blnReturnVoid;
			
			// Append the method header
			objStringBuilder.Append("\t\t/// <summary>\n");
			objStringBuilder.Append("\t\t/// Inserts a record into the " + strTableName + " table.\n");
			objStringBuilder.Append("\t\t/// </summary>\n");
			objStringBuilder.Append("\t\t/// <param name=\"\"></param>\n");
			
			// Determine the return type of the insert function
			blnReturnVoid = true;
			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];
				if (objField.IsIdentity) {
					objStringBuilder.Append("\t\tpublic Int32 Insert(");
					blnReturnVoid = false;
					break;
				} else if (objField.IsRowGuidCol) {
					objStringBuilder.Append("\t\tpublic Guid Insert(");
					blnReturnVoid = false;
					break;
				}
			}
			
			if (blnReturnVoid)
				objStringBuilder.Append("\t\tpublic void Insert(");
			
			// Append the method call parameters
/*			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];
				if (objField.IsIdentity == false && objField.IsRowGuidCol == false) {
					strMethodParameter = CreateMethodParameter(objField);
					objStringBuilder.Append(strMethodParameter);
					objStringBuilder.Append(", ");
				}
				objField = null;
			}
*/			

			// Append the method call parameters - data object
			objStringBuilder.Append(strTableName).Append("Data data, ");

			// Append the connection string parameter
			objStringBuilder.Append("SqlConnection connection");
			
			// Append the method header
			objStringBuilder.Append(") {\n");
			
			// Append the variable declarations
			objStringBuilder.Append("\t\t\tSqlConnection	objConnection;\n");
			objStringBuilder.Append("\t\t\tSqlCommand		objCommand;\n");
			objStringBuilder.Append("\n");

			// Append the try block
			objStringBuilder.Append("\t\t\ttry {\n");

			// Append the connection object creation
			objStringBuilder.Append("\t\t\t\t// Create and open the database connection\n");
			objStringBuilder.Append("\t\t\t\tobjConnection = new SqlConnection(ConfigurationSettings.AppSettings[\"ConnectionString\"]);\n");
			objStringBuilder.Append("\t\t\t\tobjConnection.Open();\n");
			objStringBuilder.Append("\n");
			
			// Append the command object creation
			objStringBuilder.Append("\t\t\t\t// Create and execute the command\n");
			objStringBuilder.Append("\t\t\t\tobjCommand = new SqlCommand();\n");
			objStringBuilder.Append("\t\t\t\tobjCommand.Connection = objConnection;\n");
			objStringBuilder.Append("\t\t\t\tobjCommand.CommandText = \"" + getProcName(strTableName, "Insert") + "\";\n");
			objStringBuilder.Append("\t\t\t\tobjCommand.CommandType = CommandType.StoredProcedure;\n");
			objStringBuilder.Append("\n");
			
			// Append the parameter appends  ;)
			objStringBuilder.Append("\t\t\t\t//Create the parameters and append them to the command object\n");
			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];
				if (!objField.IsViewColumn) {
					if (objField.IsIdentity || objField.IsRowGuidCol)
						objStringBuilder.Append("\t\t\t\t" + CreateSqlParameter(objField, true, true));
					else
						objStringBuilder.Append("\t\t\t\t" + CreateSqlParameter(objField, false, true));
				}
				objField = null;
			}
			objStringBuilder.Append("\n");

			// Append the execute statement
			objStringBuilder.Append("\t\t\t\t// Execute the query\n");
			objStringBuilder.Append("\t\t\t\tobjCommand.ExecuteNonQuery();\n");
			
			// Append the parameter value extraction
			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];
				if (objField.IsIdentity || objField.IsRowGuidCol) {
					objStringBuilder.Append("\n\t\t\t\t// Set the output paramter value(s)\n");
					if (objField.IsIdentity)
						objStringBuilder.Append("\t\t\t\treturn Int32.Parse(objCommand.Parameters[\"@" + objField.ColumnName.Replace(" ", "_") + "\"].Value.ToString());\n");
					else
						objStringBuilder.Append("\t\t\t\treturn Guid.NewGuid(objCommand.Parameters[\"@" + objField.ColumnName.Replace(" ", "_") + "\"].Value.ToString());\n");
				}
				objField = null;
			}
			
			// Append the catch block
			objStringBuilder.Append("\t\t\t} catch (Exception objException) {\n");
			objStringBuilder.Append("\t\t\t\tthrow (new Exception(\"cls" + strTableName.Replace(" ", "_") + "::Insert\\n\\n\" + objException.Message));\n");
			objStringBuilder.Append("\t\t\t}\n");
			
			// Append the method footer
			objStringBuilder.Append("\t\t}\n");
		}


		/// <summary>
		/// Creates a string that represents the update functionality of the data access class.
		/// </summary>
		/// <param name="strTableName">Name of the table the data access class is for.</param>
		/// <param name="arrFieldList">ArrayList object containing one or more clsField objects as defined in the table.</param>
		/// <param name="objStringBuilder">StreamBuilder object that the resulting string should be appended to.</param>
		private void CreateUpdateMethod(string strTableName, ArrayList arrFieldList, StringBuilder objStringBuilder) {
			clsField	objField;
			clsField	objNewField;
			clsField	objOldField;
			//string		strMethodParameter;
			int			intIndex;
			
			// Append the method header
			objStringBuilder.Append("\t\t/// <summary>\n");
			objStringBuilder.Append("\t\t/// Updates a record in the " + strTableName + " table.\n");
			objStringBuilder.Append("\t\t/// </summary>\n");
			objStringBuilder.Append("\t\t/// <param name=\"\"></param>\n");
			objStringBuilder.Append("\t\tpublic void Update(");
			
			// Append the method call parameters
/*			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];
				if (objField.IsPrimaryKey && objField.IsIdentity == false && objField.IsRowGuidCol == false) {
					objOldField = objField.Copy();
					objOldField.ColumnName = "Old" + objOldField.ColumnName;
					strMethodParameter = CreateMethodParameter(objOldField);
					objStringBuilder.Append(strMethodParameter);
					objStringBuilder.Append(", ");
					
					objNewField = objField.Copy();
					objNewField.ColumnName = "New" + objNewField.ColumnName;
					strMethodParameter = CreateMethodParameter(objNewField);
					objStringBuilder.Append(strMethodParameter);
					objStringBuilder.Append(", ");
				} else {
					strMethodParameter = CreateMethodParameter(objField);
					objStringBuilder.Append(strMethodParameter);
					objStringBuilder.Append(", ");
				}
				objField = null;
			}
*/
			// Append the method call parameters - data object
			objStringBuilder.Append(strTableName).Append("Data data, ");

			
			// Append the connection string parameter
			objStringBuilder.Append("string strConnectionString");
			
			// Append the method header
			objStringBuilder.Append(") {\n");
			
			// Append the variable declarations
			objStringBuilder.Append("\t\t\tSqlConnection	objConnection;\n");
			objStringBuilder.Append("\t\t\tSqlCommand		objCommand;\n");
			objStringBuilder.Append("\n");

			// Append the try block
			objStringBuilder.Append("\t\t\ttry {\n");

			// Append the connection object creation
			objStringBuilder.Append("\t\t\t\t// Create and open the database connection\n");
			//objStringBuilder.Append("\t\t\t\tobjConnection = new SqlConnection(strConnectionString);\n");
			objStringBuilder.Append("\t\t\t\tobjConnection = new SqlConnection(ConfigurationSettings.AppSettings[\"ConnectionString\"]);\n");
			objStringBuilder.Append("\t\t\t\tobjConnection.Open();\n");
			objStringBuilder.Append("\n");
			
			// Append the command object creation
			objStringBuilder.Append("\t\t\t\t// Create and execute the command\n");
			objStringBuilder.Append("\t\t\t\tobjCommand = new SqlCommand();\n");
			objStringBuilder.Append("\t\t\t\tobjCommand.Connection = objConnection;\n");
			objStringBuilder.Append("\t\t\t\tobjCommand.CommandText = \"" + getProcName(strTableName, "Update") + "\";\n");
			objStringBuilder.Append("\t\t\t\tobjCommand.CommandType = CommandType.StoredProcedure;\n");
			objStringBuilder.Append("\n");
			
			// Append the parameter appends  ;)
			objStringBuilder.Append("\t\t\t\t//Create the parameters and append them to the command object\n");
			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];
				if (!objField.IsViewColumn) {
					if (objField.IsPrimaryKey && objField.IsIdentity == false && objField.IsRowGuidCol == false) {
						objOldField = objField.Copy();
						objOldField.ColumnName = "Old" + objOldField.ColumnName;
						objStringBuilder.Append("\t\t\t\t" + CreateSqlParameter(objOldField, false, true));
					
						objNewField = objField.Copy();
						objNewField.ColumnName = "New" + objNewField.ColumnName;
						objStringBuilder.Append("\t\t\t\t" + CreateSqlParameter(objNewField, false, true));
					} else {
						objStringBuilder.Append("\t\t\t\t" + CreateSqlParameter(objField, false, true));
					}
				}
				objField = null;
			}
			objStringBuilder.Append("\n");
			
			// Append the execute statement
			objStringBuilder.Append("\t\t\t\t// Execute the query\n");
			objStringBuilder.Append("\t\t\t\tobjCommand.ExecuteNonQuery();\n");
			
			// Append the catch block
			objStringBuilder.Append("\t\t\t} catch (Exception objException) {\n");
			objStringBuilder.Append("\t\t\t\tthrow (new Exception(\"cls" + strTableName.Replace(" ", "_") + "::Update\\n\\n\" + objException.Message));\n");
			objStringBuilder.Append("\t\t\t}\n");
			
			// Append the method footer
			objStringBuilder.Append("\t\t}\n");
		}


		/// <summary>
		/// Creates a string that represents the delete functionality of the data access class.
		/// </summary>
		/// <param name="strTableName">Name of the table the data access class is for.</param>
		/// <param name="arrFieldList">ArrayList object containing one or more clsField objects as defined in the table.</param>
		/// <param name="objStringBuilder">StreamBuilder object that the resulting string should be appended to.</param>
		private void CreateDeleteMethods(string strTableName, ArrayList arrFieldList, StringBuilder objStringBuilder) {
			clsField	objField;
			int			intIndex;
			string		strColumnName;
			string		strPrimaryKeyList;
			ArrayList	arrKeyList;
			
			// Create the array list of key fields
			strPrimaryKeyList = "";
			arrKeyList = new ArrayList();
			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];
				if (objField.IsPrimaryKey) {
					arrKeyList.Add(objField);
					strPrimaryKeyList += objField.ColumnName.Replace(" ", "_") + "_";
				}
				objField = null;
			}
			
			// Trim off the last underscore
			if (strPrimaryKeyList.Length > 0)
				strPrimaryKeyList = strPrimaryKeyList.Substring(0, strPrimaryKeyList.Length - 1);

			/*********************************************************************************************************/
			// Create the remaining select functions based on identity columns or uniqueidentifiers
			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];
			
				if (objField.IsIdentity || objField.IsRowGuidCol || objField.IsPrimaryKey || objField.IsForeignKey) {
					strColumnName = objField.ColumnName.Substring(0, 1).ToUpper() + objField.ColumnName.Substring(1);
				
					// Append the method header
					objStringBuilder.Append("\t\t/// <summary>\n");
					objStringBuilder.Append("\t\t/// Deletes a record from the " + strTableName + " table by " + objField.ColumnName + ".\n");
					objStringBuilder.Append("\t\t/// </summary>\n");
					objStringBuilder.Append("\t\t/// <param name=\"\"></param>\n");
					objStringBuilder.Append("\t\tpublic SqlDataReader DeleteBy" + strColumnName.Replace(" ", "_") + "(" + CreateMethodParameter(objField) + ", string strConnectionString) {\n");
					
					// Append the variable declarations
					objStringBuilder.Append("\t\t\tSqlConnection	objConnection;\n");
					objStringBuilder.Append("\t\t\tSqlCommand		objCommand;\n");
					objStringBuilder.Append("\n");

					// Append the try block
					objStringBuilder.Append("\t\t\ttry {\n");

					// Append the connection object creation
					objStringBuilder.Append("\t\t\t\t// Create and open the database connection\n");
					objStringBuilder.Append("\t\t\t\tobjConnection = new SqlConnection(ConfigurationSettings.AppSettings[\"ConnectionString\"]);\n");
					objStringBuilder.Append("\t\t\t\tobjConnection.Open();\n");
					objStringBuilder.Append("\n");
					
					// Append the command object creation
					objStringBuilder.Append("\t\t\t\t// Create and execute the command\n");
					objStringBuilder.Append("\t\t\t\tobjCommand = new SqlCommand();\n");
					objStringBuilder.Append("\t\t\t\tobjCommand.Connection = objConnection;\n");
					objStringBuilder.Append("\t\t\t\tobjCommand.CommandText = \"" + getProcName(strTableName, "DeleteBy" + strColumnName.Replace(" ", "_")) + "\";\n");
					objStringBuilder.Append("\t\t\t\tobjCommand.CommandType = CommandType.StoredProcedure;\n");
					objStringBuilder.Append("\n");

					// Append the parameters
					objStringBuilder.Append("\t\t\t\t// Create and append the parameters\n");
					objStringBuilder.Append("\t\t\t\t" + CreateSqlParameter(objField, false, false));
					objStringBuilder.Append("\n");

					// Append the execute statement
					objStringBuilder.Append("\t\t\t\t// Execute the query and return the result\n");
					objStringBuilder.Append("\t\t\t\treturn objCommand.ExecuteReader(CommandBehavior.CloseConnection);\n");
					
					// Append the catch block
					objStringBuilder.Append("\t\t\t} catch (Exception objException) {\n");
					objStringBuilder.Append("\t\t\t\tthrow (new Exception(\"cls" + strTableName.Replace(" ", "_") + "::DeleteBy" + strColumnName.Replace(" ", "_") + "\\n\\n\" + objException.Message));\n");
					objStringBuilder.Append("\t\t\t}\n");
					
					// Append the method footer
					if (arrKeyList.Count > 0)
						objStringBuilder.Append("\t\t}\n\n\n");
					else
						objStringBuilder.Append("\t\t}\n\n");
				
					objField = null;
				}
			}

			/*********************************************************************************************************/
			// Create the select functions based on a composite primary key
			if (arrKeyList.Count > 1) {
				// Append the method header
				objStringBuilder.Append("\t\t/// <summary>\n");
				objStringBuilder.Append("\t\t/// Deletes a record from the " + strTableName + " table by a composite primary key.\n");
				objStringBuilder.Append("\t\t/// </summary>\n");
				objStringBuilder.Append("\t\t/// <param name=\"\"></param>\n");
				
				objStringBuilder.Append("\t\tpublic SqlDataReader DeleteBy" + strPrimaryKeyList + "(");
				for (intIndex = 0; intIndex < arrKeyList.Count; intIndex++) {
					objField = (clsField)arrKeyList[intIndex];
					objStringBuilder.Append(CreateMethodParameter(objField) + ", ");
				}
				objStringBuilder.Append("string strConnectionString) {\n");
				
				// Append the variable declarations
				objStringBuilder.Append("\t\t\tSqlConnection	objConnection;\n");
				objStringBuilder.Append("\t\t\tSqlCommand		objCommand;\n");
				objStringBuilder.Append("\n");

				// Append the try block
				objStringBuilder.Append("\t\t\ttry {\n");

				// Append the connection object creation
				objStringBuilder.Append("\t\t\t\t// Create and open the database connection\n");
				objStringBuilder.Append("\t\t\t\tobjConnection = new SqlConnection(ConfigurationSettings.AppSettings[\"ConnectionString\"]);\n");
				objStringBuilder.Append("\t\t\t\tobjConnection.Open();\n");
				objStringBuilder.Append("\n");
				
				// Append the command object creation
				objStringBuilder.Append("\t\t\t\t// Create and execute the command\n");
				objStringBuilder.Append("\t\t\t\tobjCommand = new SqlCommand();\n");
				objStringBuilder.Append("\t\t\t\tobjCommand.Connection = objConnection;\n");
				objStringBuilder.Append("\t\t\t\tobjCommand.CommandText = \"" + getProcName(strTableName, "DeleteBy" + strPrimaryKeyList) + "\";\n");
				objStringBuilder.Append("\t\t\t\tobjCommand.CommandType = CommandType.StoredProcedure;\n");
				objStringBuilder.Append("\n");

				// Append the parameters
				objStringBuilder.Append("\t\t\t\t// Create and append the parameters\n");
				for (intIndex = 0; intIndex < arrKeyList.Count; intIndex++) {
					objField = (clsField)arrKeyList[intIndex];
					objStringBuilder.Append("\t\t\t\t" + CreateSqlParameter(objField, false, false));
					objField = null;
				}
				objStringBuilder.Append("\n");

				// Append the execute statement
				objStringBuilder.Append("\t\t\t\t// Execute the query and return the result\n");
				objStringBuilder.Append("\t\t\t\treturn objCommand.ExecuteReader(CommandBehavior.CloseConnection);\n");
				
				// Append the catch block
				objStringBuilder.Append("\t\t\t} catch (Exception objException) {\n");
				objStringBuilder.Append("\t\t\t\tthrow (new Exception(\"cls" + strTableName.Replace(" ", "_") + "::DeleteBy" + strPrimaryKeyList + "\\n\\n\" + objException.Message));\n");
				objStringBuilder.Append("\t\t\t}\n");
				
				// Append the method footer
				objStringBuilder.Append("\t\t}\n\n\n");
			
				objField = null;
			}
		}


		/// <summary>
		/// Creates a string that represents the select functionality of the data access class.
		/// </summary>
		/// <param name="strTableName">Name of the table the data access class is for.</param>
		/// <param name="arrFieldList">ArrayList object containing one or more clsField objects as defined in the table.</param>
		/// <param name="objStringBuilder">StreamBuilder object that the resulting string should be appended to.</param>
		private void CreateSelectMethods(string strTableName, ArrayList arrFieldList, StringBuilder objStringBuilder) {
			clsField	objField;
			int			intIndex;
			string		strColumnName;
			string		strPrimaryKeyList;
			ArrayList	arrKeyList;
			
			// Create the array list of key fields
			strPrimaryKeyList = "";
			arrKeyList = new ArrayList();
			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];
				if (objField.IsPrimaryKey) {
					arrKeyList.Add(objField);
					strPrimaryKeyList += objField.ColumnName.Replace(" ", "_") + "_";
				}
				objField = null;
			}
			
			// Trim off the last underscore
			if (strPrimaryKeyList.Length > 0)
				strPrimaryKeyList = strPrimaryKeyList.Substring(0, strPrimaryKeyList.Length - 1);

			/*********************************************************************************************************/
			// Create the initial "select all" function
			
			// Append the method header
			objStringBuilder.Append("\t\t/// <summary>\n");
			objStringBuilder.Append("\t\t/// Selects a record from the " + strTableName + " table.\n");
			objStringBuilder.Append("\t\t/// </summary>\n");
			objStringBuilder.Append("\t\t/// <param name=\"\"></param>\n");
			objStringBuilder.Append("\t\tpublic SqlDataReader Select(string strConnectionString) {\n");
			
			// Append the variable declarations
			objStringBuilder.Append("\t\t\tSqlConnection	objConnection;\n");
			objStringBuilder.Append("\t\t\tSqlCommand		objCommand;\n");
			objStringBuilder.Append("\n");

			// Append the try block
			objStringBuilder.Append("\t\t\ttry {\n");

			// Append the connection object creation
			objStringBuilder.Append("\t\t\t\t// Create and open the database connection\n");
			objStringBuilder.Append("\t\t\t\tobjConnection = new SqlConnection(ConfigurationSettings.AppSettings[\"ConnectionString\"]);\n");
			objStringBuilder.Append("\t\t\t\tobjConnection.Open();\n");
			objStringBuilder.Append("\n");
			
			// Append the command object creation
			objStringBuilder.Append("\t\t\t\t// Create and execute the command\n");
			objStringBuilder.Append("\t\t\t\tobjCommand = new SqlCommand();\n");
			objStringBuilder.Append("\t\t\t\tobjCommand.Connection = objConnection;\n");
			objStringBuilder.Append("\t\t\t\tobjCommand.CommandText = \"" + getProcName(strTableName, "Select") + "\";\n");
			objStringBuilder.Append("\t\t\t\tobjCommand.CommandType = CommandType.StoredProcedure;\n");
			objStringBuilder.Append("\n");

			// Append the execute statement
			objStringBuilder.Append("\t\t\t\t// Execute the query and return the result\n");
			objStringBuilder.Append("\t\t\t\treturn objCommand.ExecuteReader(CommandBehavior.CloseConnection);\n");
			
			// Append the catch block
			objStringBuilder.Append("\t\t\t} catch (Exception objException) {\n");
			objStringBuilder.Append("\t\t\t\tthrow (new Exception(\"cls" + strTableName.Replace(" ", "_") + "::Select\\n\\n\" + objException.Message));\n");
			objStringBuilder.Append("\t\t\t}\n");
			
			// Append the method footer
			objStringBuilder.Append("\t\t}\n\n\n");

			/*********************************************************************************************************/
			// Create the remaining select functions based on identity columns or uniqueidentifiers
			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];
			
				if (objField.IsIdentity || objField.IsRowGuidCol || objField.IsPrimaryKey || objField.IsForeignKey) {
					strColumnName = objField.ColumnName.Substring(0, 1).ToUpper() + objField.ColumnName.Substring(1);
				
					// Append the method header
					objStringBuilder.Append("\t\t/// <summary>\n");
					objStringBuilder.Append("\t\t/// Selects a record from the " + strTableName + " table by " + objField.ColumnName + ".\n");
					objStringBuilder.Append("\t\t/// </summary>\n");
					objStringBuilder.Append("\t\t/// <param name=\"\"></param>\n");
					objStringBuilder.Append("\t\tpublic SqlDataReader SelectBy" + strColumnName.Replace(" ", "_") + "(" + CreateMethodParameter(objField) + ", string strConnectionString) {\n");
					
					// Append the variable declarations
					objStringBuilder.Append("\t\t\tSqlConnection	objConnection;\n");
					objStringBuilder.Append("\t\t\tSqlCommand		objCommand;\n");
					objStringBuilder.Append("\n");

					// Append the try block
					objStringBuilder.Append("\t\t\ttry {\n");

					// Append the connection object creation
					objStringBuilder.Append("\t\t\t\t// Create and open the database connection\n");
					//objStringBuilder.Append("\t\t\t\tobjConnection = new SqlConnection(strConnectionString);\n");
					objStringBuilder.Append("\t\t\t\tobjConnection = new SqlConnection(ConfigurationSettings.AppSettings[\"ConnectionString\"]);\n");
					objStringBuilder.Append("\t\t\t\tobjConnection.Open();\n");
					objStringBuilder.Append("\n");
					
					// Append the command object creation
					objStringBuilder.Append("\t\t\t\t// Create and execute the command\n");
					objStringBuilder.Append("\t\t\t\tobjCommand = new SqlCommand();\n");
					objStringBuilder.Append("\t\t\t\tobjCommand.Connection = objConnection;\n");
					objStringBuilder.Append("\t\t\t\tobjCommand.CommandText = \"" + getProcName(strTableName, "SelectBy" + strColumnName.Replace(" ", "_")) +  "\";\n");
					objStringBuilder.Append("\t\t\t\tobjCommand.CommandType = CommandType.StoredProcedure;\n");
					objStringBuilder.Append("\n");

					// Append the parameters
					objStringBuilder.Append("\t\t\t\t// Create and append the parameters\n");
					objStringBuilder.Append("\t\t\t\t" + CreateSqlParameter(objField, false, false));
					objStringBuilder.Append("\n");

					// Append the execute statement
					objStringBuilder.Append("\t\t\t\t// Execute the query and return the result\n");
					objStringBuilder.Append("\t\t\t\treturn objCommand.ExecuteReader(CommandBehavior.CloseConnection);\n");
					
					// Append the catch block
					objStringBuilder.Append("\t\t\t} catch (Exception objException) {\n");
					objStringBuilder.Append("\t\t\t\tthrow (new Exception(\"cls" + strTableName.Replace(" ", "_") + "::SelectBy" + strColumnName.Replace(" ", "_") + "\\n\\n\" + objException.Message));\n");
					objStringBuilder.Append("\t\t\t}\n");
					
					// Append the method footer
					if (arrKeyList.Count > 0)
						objStringBuilder.Append("\t\t}\n\n\n");
					else
						objStringBuilder.Append("\t\t}\n\n");
				
					objField = null;
				}
			}

			/*********************************************************************************************************/
			// Create the select functions based on a composite primary key
			if (arrKeyList.Count > 1) {
				// Append the method header
				objStringBuilder.Append("\t\t/// <summary>\n");
				objStringBuilder.Append("\t\t/// Selects a record from the " + strTableName + " table by a composite primary key.\n");
				objStringBuilder.Append("\t\t/// </summary>\n");
				objStringBuilder.Append("\t\t/// <param name=\"\"></param>\n");
				
				objStringBuilder.Append("\t\tpublic SqlDataReader SelectBy" + strPrimaryKeyList + "(");
				for (intIndex = 0; intIndex < arrKeyList.Count; intIndex++) {
					objField = (clsField)arrKeyList[intIndex];
					objStringBuilder.Append(CreateMethodParameter(objField) + ", ");
				}
				objStringBuilder.Append("string strConnectionString) {\n");
				
				// Append the variable declarations
				objStringBuilder.Append("\t\t\tSqlConnection	objConnection;\n");
				objStringBuilder.Append("\t\t\tSqlCommand		objCommand;\n");
				objStringBuilder.Append("\n");

				// Append the try block
				objStringBuilder.Append("\t\t\ttry {\n");

				// Append the connection object creation
				objStringBuilder.Append("\t\t\t\t// Create and open the database connection\n");
				objStringBuilder.Append("\t\t\t\tobjConnection = new SqlConnection(ConfigurationSettings.AppSettings[\"ConnectionString\"]);\n");
				objStringBuilder.Append("\t\t\t\tobjConnection.Open();\n");
				objStringBuilder.Append("\n");
				
				// Append the command object creation
				objStringBuilder.Append("\t\t\t\t// Create and execute the command\n");
				objStringBuilder.Append("\t\t\t\tobjCommand = new SqlCommand();\n");
				objStringBuilder.Append("\t\t\t\tobjCommand.Connection = objConnection;\n");
				objStringBuilder.Append("\t\t\t\tobjCommand.CommandText = \"" + getProcName(strTableName, "SelectBy" + strPrimaryKeyList) +  "\";\n");
				objStringBuilder.Append("\t\t\t\tobjCommand.CommandType = CommandType.StoredProcedure;\n");
				objStringBuilder.Append("\n");

				// Append the parameters
				objStringBuilder.Append("\t\t\t\t// Create and append the parameters\n");
				for (intIndex = 0; intIndex < arrKeyList.Count; intIndex++) {
					objField = (clsField)arrKeyList[intIndex];
					objStringBuilder.Append("\t\t\t\t" + CreateSqlParameter(objField, false, false));
					objField = null;
				}
				objStringBuilder.Append("\n");

				// Append the execute statement
				objStringBuilder.Append("\t\t\t\t// Execute the query and return the result\n");
				objStringBuilder.Append("\t\t\t\treturn objCommand.ExecuteReader(CommandBehavior.CloseConnection);\n");
				
				// Append the catch block
				objStringBuilder.Append("\t\t\t} catch (Exception objException) {\n");
				objStringBuilder.Append("\t\t\t\tthrow (new Exception(\"cls" + strTableName.Replace(" ", "_") + "::SelectBy" + strPrimaryKeyList + "\\n\\n\" + objException.Message));\n");
				objStringBuilder.Append("\t\t\t}\n");
				
				// Append the method footer
				objStringBuilder.Append("\t\t}\n\n\n");
			
				objField = null;
			}
		}


		/// <summary>
		/// Creates a string containing the parameter declaration for a stored procedure based on the parameters passed in.
		/// </summary>
		/// <param name="objField">Object that stores the information for the field the parameter represents.</param>
		/// <returns>String containing parameter information of the specified field for a stored procedure.</returns>
		private string CreateParameterString(clsField objField, bool blnCheckForOutput) {
			string	strParameter;
		
			switch (objField.DBType.ToLower()) {
				case "binary":
					strParameter = "@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType + "(" + objField.Length + ")";
					break;
				case "bigint":
					strParameter = "@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType;
					break;
				case "bit":
					strParameter = "@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType;
					break;
				case "char":
					strParameter = "@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType + "(" + objField.Length + ")";
					break;
				case "datetime":
					strParameter = "@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType;
					break;
				case "decimal":
					if (objField.Scale.Length == 0)
						strParameter = "@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType + "(" + objField.Precision + ")";
					else
						strParameter = "@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType + "(" + objField.Precision + ", "+ objField.Scale + ")";
					break;
				case "float":
					strParameter = "@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType + "(" + objField.Precision + ")";
					break;
				case "image":
					strParameter = "@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType;
					break;
				case "int":
					strParameter = "@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType;
					break;
				case "money":
					strParameter = "@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType;
					break;
				case "nchar":
					strParameter = "@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType + "(" + objField.Length + ")";
					break;
				case "ntext":
					strParameter = "@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType;
					break;
				case "nvarchar":
					strParameter = "@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType + "(" + objField.Length + ")";
					break;
				case "numeric":
					if (objField.Scale.Length == 0)
						strParameter = "@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType + "(" + objField.Precision + ")";
					else
						strParameter = "@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType + "(" + objField.Precision + ", "+ objField.Scale + ")";
					break;
				case "real":
					strParameter = "@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType;
					break;
				case "smalldatetime":
					strParameter = "@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType;
					break;
				case "smallint":
					strParameter = "@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType;
					break;
				case "smallmoney":
					strParameter = "@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType;
					break;
				case "sql_variant":
					strParameter = "@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType;
					break;
				case "sysname":
					strParameter = "@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType;
					break;
				case "text":
					strParameter = "@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType;
					break;
				case "timestamp":
					strParameter = "@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType;
					break;
				case "tinyint":
					strParameter = "@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType;
					break;
				case "varbinary":
					strParameter = "@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType + "(" + objField.Length + ")";
					break;
				case "varchar":
					strParameter = "@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType + "(" + objField.Length + ")";
					break;
				case "uniqueidentifier":
					strParameter = "@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.DBType;
					break;
				default:  // Unknow data type
					throw(new Exception("Invalid SQL Server data type specified: " + objField.DBType));
			}
			
			// Is the parameter an output parameter?
			if (blnCheckForOutput)
				if (objField.IsRowGuidCol || objField.IsIdentity)
					strParameter += " output";
			
			// Return the new parameter string
			return strParameter;
		}
		

		/// <summary>
		/// Creates a string for a method parameter representing the specified field.
		/// </summary>
		/// <param name="objField">Object that stores the information for the field the parameter represents.</param>
		/// <returns>String containing parameter information of the specified field for a method call.</returns>
		public string CreateMethodParameter(clsField objField) {
			string		strParameter;
			string	strColumnName;

			// Format the column name
			strColumnName = objField.ColumnName;
			strColumnName = strColumnName.Substring(0, 1).ToUpper() + strColumnName.Substring(1); 
		
			switch (objField.DBType.ToLower()) {
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
					throw(new Exception("Invalid SQL Server data type specified: " + objField.DBType));
			}
			
			// Return the new parameter string
			return strParameter;
		}


		/// <summary>
		/// Matches a SQL Server data type to a SqlClient.SqlDbType.
		/// </summary>
		/// <param name="strType">A string representing a SQL Server data type.</param>
		/// <returns>A string representing a SqlClient.SqlDbType.</returns>
		public string GetSqlDbType(string strType) {
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


		/// <summary>
		/// Creates a string for a SqlParameter representing the specified field.
		/// </summary>
		/// <param name="objField">Object that stores the information for the field the parameter represents.</param>
		/// <returns>String containing SqlParameter information of the specified field for a method call.</returns>
		public string CreateSqlParameter(clsField objField, bool blnOutput, bool useDataObject) {
			byte		bytePrecision;
			byte		byteScale;
			string[]	strMethodParameter;
			
			// Get an array of data types and variable names
			strMethodParameter = CreateMethodParameter(objField).Split(new Char[] {' '});
			
			// Convert the precision value
			if (objField.Precision.Length > 0)
				bytePrecision = byte.Parse(objField.Precision);
			else
				bytePrecision = 0;

			// Convert the scale value
			if (objField.Scale.Length > 0)
				byteScale = byte.Parse(objField.Scale);
			else
				byteScale = 0;

// this needs to be cleaned up!!!!			
			// Is the parameter used for input or output
			if (blnOutput)
				//return "Int32 " +  strMethodParameter[1] +"=0; // dummy value so that parameter can be declared output - should use return value\n" +"objCommand.Parameters.Add(new SqlParameter(\"@" + objField.ColumnName + "\", SqlDbType." + GetSqlDbType(objField.DBType) + ", " + objField.Length + ", ParameterDirection.Output, false, " + bytePrecision + ", " + byteScale + ", \"" + objField.ColumnName + "\", DataRowVersion.Proposed, " + strMethodParameter[1] + "));\n";
				if (useDataObject)
					return "objCommand.Parameters.Add(new SqlParameter(\"@" + objField.ColumnName + "\", SqlDbType." + GetSqlDbType(objField.DBType) + ", " + objField.Length + ", ParameterDirection.Output, false, " + bytePrecision + ", " + byteScale + ", \"" + objField.ColumnName + "\", DataRowVersion.Proposed, data." + objField.ColumnName + "));\n";
				else
					return "objCommand.Parameters.Add(new SqlParameter(\"@" + objField.ColumnName + "\", SqlDbType." + GetSqlDbType(objField.DBType) + ", " + objField.Length + ", ParameterDirection.Output, false, " + bytePrecision + ", " + byteScale + ", \"" + objField.ColumnName + "\", DataRowVersion.Proposed, " + strMethodParameter[1] + "));\n";
			else
				//return "objCommand.Parameters.Add(new SqlParameter(\"@" + objField.ColumnName + "\", SqlDbType." + GetSqlDbType(objField.DBType) + ", " + objField.Length + ", ParameterDirection.Input, false, " + bytePrecision + ", " + byteScale + ", \"" + objField.ColumnName + "\", DataRowVersion.Proposed, " + strMethodParameter[1] + "));\n";
				if (useDataObject)
					return "objCommand.Parameters.Add(new SqlParameter(\"@" + objField.ColumnName + "\", SqlDbType." + GetSqlDbType(objField.DBType) + ", " + objField.Length + ", ParameterDirection.Input, false, " + bytePrecision + ", " + byteScale + ", \"" + objField.ColumnName + "\", DataRowVersion.Proposed, data." + objField.ColumnName + "));\n";
				else
					return "objCommand.Parameters.Add(new SqlParameter(\"@" + objField.ColumnName + "\", SqlDbType." + GetSqlDbType(objField.DBType) + ", " + objField.Length + ", ParameterDirection.Input, false, " + bytePrecision + ", " + byteScale + ", \"" + objField.ColumnName + "\", DataRowVersion.Proposed, " + strMethodParameter[1] + "));\n";
		}

		/// <summary>
		/// Internal helper method to write stored procedure to file based on m_blnSingleFile
		/// </summary>
		/// <param name="strStoredProcName">Name of stored procedure (used to name file if not outputting as single file)</param>
		/// <param name="strStoredProcText">Text to create stored procedure.</param>
		private void WriteToFile(String strStoredProcName, String strStoredProcText) {
			StringBuilder	objStringBuilder;

			if (!m_blnSingleFile) {
				String strFileName = "SQL Scripts\\" + strStoredProcName + ".sql";
				if (File.Exists(strFileName))
					File.Delete(strFileName);
				m_objStreamWriter = new StreamWriter(strFileName);
			}

			objStringBuilder = new StringBuilder();
			if (m_blnSingleFile) {
				objStringBuilder.Append("/*\n");
				objStringBuilder.Append("******************************************************************************\n");
				objStringBuilder.Append("******************************************************************************\n");
				objStringBuilder.Append("*/\n");
			}

			if (m_blnScriptDropStatement) {
				objStringBuilder.Append("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[" + strStoredProcName + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)\n");
				objStringBuilder.Append("drop procedure [dbo].[" + strStoredProcName + "]\n");
				objStringBuilder.Append("GO\n\n");
			}

			objStringBuilder.Append(strStoredProcText);
			m_objStreamWriter.Write(objStringBuilder.ToString());

			if (!m_blnSingleFile) {
				m_objStreamWriter.Close();
				m_objStreamWriter = null;
			}
		}

		private String getProcName(String table, String type) {
			String s;

			s = "proc" + table.Replace(" ", "_") + type;
			s = "sp" + table.Replace(" ", "_") + "_" + type;

			return s;
		}

		private String getDAONameSpace(String db, String table) {
			String s;

			s = db + ".DataAccess." + table.Replace(" ", "_");
			s = m_strProjectNameSpace  + ".DAO";

			return s;
		}

		private String getDONameSpace(String db, String table) {
			String s;

			if (db != null && table != null)
				s = db + ".DataAccess." + table.Replace(" ", "_");
			s = m_strProjectNameSpace  + ".DataObject";

			return s;
		}

		private String getDAOClassName(String table) {
			String s;

			s = "cls" + table.Replace(" ", "_");
			s = table.Replace(" ", "_") + "DAO";

			return s;
		}

		private String getDOClassName(String table) {
			String s;

			s = "cls" + table.Replace(" ", "_");
			s = table.Replace(" ", "_") + "Data";

			return s;
		}


		private void CreateDataObjectClass(string strTableName, ArrayList arrFieldList) {
			StreamWriter	objStreamWriter;
			StringBuilder	objStringBuilder;
			string			strFileName;
			
			objStringBuilder = new StringBuilder(4096);

			// Create the header for the class
			objStringBuilder.Append("using System;\n");
			objStringBuilder.Append("\n");
			objStringBuilder.Append("namespace " + getDONameSpace(m_objConnection.Database.ToString(), strTableName) + " {\n");
			objStringBuilder.Append("\tpublic class " + getDOClassName(strTableName) + " {\n\n");

			Int32 intIndex;
			clsField objField;
			
			// declaration of private member variables
			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];
				//if (objField.IsIdentity == false && objField.IsRowGuidCol == false) {
					objStringBuilder.Append("\t\tprivate ").Append(objField.ParameterType).Append(" ").Append(GetFieldFormat(objField.ColumnName)).Append(";\n");
				//}
				objField = null;
			}
			objStringBuilder.Append("\n");

			// accessor methods
			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];
				//if (objField.IsIdentity == false && objField.IsRowGuidCol == false) {
					objStringBuilder.Append("\t\tpublic ").Append(objField.ParameterType).Append(" ").Append(GetMethodFormat(objField.ColumnName)).Append(" {\n");
					objStringBuilder.Append("\t\t\tget { return this.").Append(GetFieldFormat(objField.ColumnName)).Append("; }\n");
					objStringBuilder.Append("\t\t\tset { this.").Append(GetFieldFormat(objField.ColumnName)).Append(" = value; }\n");
					objStringBuilder.Append("\t\t}\n\n");
				//}
				objField = null;
			}

		
			// Close out the class and namespace
			objStringBuilder.Append("\t}\n");
			objStringBuilder.Append("}\n");

			// Create the output stream
			strFileName = "Data Object Classes\\" + getDOClassName(strTableName) + ".cs";
			if (File.Exists(strFileName))
				File.Delete(strFileName);
			objStreamWriter = new StreamWriter(strFileName);
			objStreamWriter.Write(objStringBuilder.ToString());
			objStreamWriter.Close();
			objStreamWriter = null;
			objStringBuilder = null;
		}

		private void CreateView(string strTableName, ArrayList arrFieldList) {
			clsField		objField;
			int				intIndex;
			StringBuilder	objStringBuilder;
			String			strProcName;
			
			// Create the SQL for the stored procedure
			objStringBuilder = new StringBuilder(1024);

			strProcName = "vw" + strTableName;

			objStringBuilder.Append("if exists (select * from sysobjects where id = object_id(N'[" + strProcName + "]') and OBJECTPROPERTY(id, N'IsView') = 1)\n");
			objStringBuilder.Append("drop view [" + strProcName + "]\n");
			objStringBuilder.Append("GO\n");
			objStringBuilder.Append("\n");
			objStringBuilder.Append("create view " + strProcName + "\n");
			objStringBuilder.Append("\n");
			objStringBuilder.Append("AS\n");
			objStringBuilder.Append("\n");
			objStringBuilder.Append("SELECT ");
			
			// Create the parameter list
			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];
				if (!objField.IsViewColumn) {
					if (intIndex>0) {
						objStringBuilder.Append(",\n");
					}
					objStringBuilder.Append("\t[" + objField.ColumnName + "]");
				}
				objField = null;
			}
			objStringBuilder.Append("\n");			
			objStringBuilder.Append("FROM\n\t[");
			objStringBuilder.Append(strTableName);
			objStringBuilder.Append("]\n");

			// Write out the stored procedure
			WriteToFile(strProcName, objStringBuilder.ToString() + "\nGO\n\n");
			objStringBuilder = null;
		}

		private void CreateDAOListMethods(string strTableName, ArrayList arrFieldList, StringBuilder objStringBuilder) {
			clsField	objField;
			int			intIndex;
			
			// Append the method header
			objStringBuilder.Append("\t\t/// <summary>\n");
			objStringBuilder.Append("\t\t/// Inserts a record into the " + strTableName + " table.\n");
			objStringBuilder.Append("\t\t/// </summary>\n");
			objStringBuilder.Append("\t\t/// <param name=\"\"></param>\n");

			objStringBuilder.Append("\t\tprivate SqlDataReader GetListReader() { \n");
			objStringBuilder.Append("\t\treturn GetListReader(\"\", \"\"); \n");
			objStringBuilder.Append("\t\t} \n");
			objStringBuilder.Append("\t\t \n");
			objStringBuilder.Append("\t\tprivate SqlDataReader GetListReader(String whereClause, String orderByClause) { \n");
			objStringBuilder.Append("\t\tSqlDataReader dataReader = null; \n");
			objStringBuilder.Append("\t\t \n");
			objStringBuilder.Append("\t\ttry { \n");
			objStringBuilder.Append("\t\t	Database data = new Database(); \n");
			objStringBuilder.Append("\t\t \n");
			objStringBuilder.Append("\t\t	String sql = \"select * from \" + VIEW;\n");
			objStringBuilder.Append("\t\t	if (whereClause.Trim().Length >0) { \n");
			objStringBuilder.Append("\t\t		sql = sql + \" where \" + whereClause; \n");
			objStringBuilder.Append("\t\t	} \n");
			objStringBuilder.Append("\t\t	if (orderByClause.Trim().Length >0) { \n");
			objStringBuilder.Append("\t\t		sql = sql + \" order by \" + orderByClause; \n");
			objStringBuilder.Append("\t\t	} \n");
			objStringBuilder.Append("\t\t \n");
			objStringBuilder.Append("\t\t	data.ExecuteSQLSelect (sql, out dataReader); \n");
			objStringBuilder.Append("\t\t} catch (Exception ex) { \n");
			objStringBuilder.Append("\t\t	Error.Log(ex.ToString()); \n");
			objStringBuilder.Append("\t\t} \n");
			objStringBuilder.Append("\t\t \n");
			objStringBuilder.Append("\t\treturn dataReader; \n");
			objStringBuilder.Append("\t\t}\n");
			objStringBuilder.Append("\n");

			objStringBuilder.Append("\t\t	public ICollection GetList() {\n");
			objStringBuilder.Append("\t\t		return GetList(\"\", \"\");\n");
			objStringBuilder.Append("\t\t	}\n");
			objStringBuilder.Append("\n");
			objStringBuilder.Append("\t\tpublic ICollection GetList(String whereClause) {\n");
			objStringBuilder.Append("\t\t	return GetList(whereClause, \"\");\n");
			objStringBuilder.Append("\t\t}\n");
			objStringBuilder.Append("\n");
			objStringBuilder.Append("\t\tpublic ICollection GetList(String whereClause, String orderByClause) {\n");
			objStringBuilder.Append("\t\t	SqlDataReader dataReader = GetListReader();\n");
			objStringBuilder.Append("\t\t    \n");
			objStringBuilder.Append("\t\t	ArrayList list = new ArrayList();\n");
			objStringBuilder.Append("\t\t	while (dataReader.Read()) {\n");
			objStringBuilder.Append("\t\t		list.Add(getDataObjectFromReader(dataReader));\n");
			objStringBuilder.Append("\t\t	}\n");
			objStringBuilder.Append("\t\t	dataReader.Close();\n");
			objStringBuilder.Append("\t\t	return list;\n");
			objStringBuilder.Append("\t\t}\n");
			objStringBuilder.Append("\n");			

objStringBuilder.Append("\t\tpublic ").Append(getDOClassName(strTableName)).Append(" load(Int32 id) {\n");
objStringBuilder.Append("\t\t	SqlDataReader dataReader = GetListReader(\"").Append(getIdentityColumn(arrFieldList)).Append("=\" + id.ToString(), \"\");\n");
objStringBuilder.Append("\t\t    \n");
objStringBuilder.Append("\t\t	dataReader.Read();\n");
objStringBuilder.Append("\t\t	return getDataObjectFromReader(dataReader);\n");
objStringBuilder.Append("\t\t}\n");
objStringBuilder.Append("\n");			

			objStringBuilder.Append("\t\tprivate ").Append(getDOClassName(strTableName)).Append(" getDataObjectFromReader(SqlDataReader dataReader) {\n");
			objStringBuilder.Append("\t\t	").Append(getDOClassName(strTableName)).Append(" data = new ").Append(getDOClassName(strTableName)).Append("();\n");

			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];
				objStringBuilder.Append("\t\t\tdata.").Append(objField.ColumnName).Append(" = ").Append("dataReader.Get").Append(objField.ParameterType).Append("(").Append(intIndex).Append(");\n");
				objField = null;
			}
			/*
			objStringBuilder.Append("\t\t	if (dataReader.IsDBNull(7)) { \n");
			objStringBuilder.Append("\t\t		data.LastUpdateUser = \"\";\n");
			objStringBuilder.Append("\t\t	} else {\n");
			objStringBuilder.Append("\t\t		data.LastUpdateUser = dataReader.GetString(7);\n");
			objStringBuilder.Append("\t\t	}\n");
			*/
			objStringBuilder.Append("\t\t\n");
			objStringBuilder.Append("\t\t	return data;\n");
			objStringBuilder.Append("\t\t}\n");
			objStringBuilder.Append("\n");			
		}

		private clsField getIdentityColumn(ArrayList arrFieldList) {
			Int32 intIndex;
			clsField objField;

			for (intIndex = 0; intIndex < arrFieldList.Count; intIndex++) {
				objField = (clsField)arrFieldList[intIndex];
				if (objField.IsIdentity) {
					return objField;
				}
			}

			return new clsField();
		}


		private String GetFieldFormat(String s) {
			return s.Substring(0, 1).ToLower() + s.Substring(1);
		}

		private String GetMethodFormat(String s) {
			return s.Substring(0, 1).ToUpper() + s.Substring(1);
		}

	}
}

