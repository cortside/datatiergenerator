// This component makes extensive use of inline documentation comments.
// In Visual Studio .NET, select <Build Comment Web Pages> on the <Tools> menu to create HTML documentation of the class.

using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Text;

namespace Spring2.DataTierGenerator {
	/// <summary>
	/// Generates stored procedures and associated data access code for the specified database.
	/// </summary>
	public class Generator {
		// Connection to the database
		private	SqlConnection connection;
		private StreamWriter writer;
		private Configuration options;

		/// <summary>
		/// Contructor for the Generator class.
		/// </summary>
		/// <param name="strConnectionString">Connecion string to a SQL Server database.</param>
		public Generator(Configuration options) {
			connection = new SqlConnection(options.ConnectionString);
			connection.Open();
            this.options = options;
            this.options.Database = connection.Database.ToString();
		}
		
		/// <summary>
		/// Generates SQL scripts for the stored procedures for each table in the database.
		/// </summary>
		public void ProcessTables() {
			SqlDataAdapter objDataAdapter;
			DataTable objDataTable;
			string strTableName;
			string strFileName;

			// Check to see if the "SQL Scripts" directory exists; if not, create it; otherwise, clear it out
			if (!Directory.Exists(options.SqlScriptDirectory))
				Directory.CreateDirectory(options.SqlScriptDirectory);

			// Check to see if the "Data Access Classes" directory exists; if not, create it; otherwise, clear it out
			if (!Directory.Exists(options.DaoClassDirectory))
				Directory.CreateDirectory(options.DaoClassDirectory);

			// Check to see if the "Data Object Classes" directory exists; if not, create it; otherwise, clear it out
			if (!Directory.Exists(options.DoClassDirectory))
				Directory.CreateDirectory(options.DoClassDirectory);

			// Get a list of the entities in the database
			objDataTable = new DataTable();
			objDataAdapter = new SqlDataAdapter("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG = '" + connection.Database + "'", connection);
			objDataAdapter.Fill(objDataTable);
			
			// Create and open the file stream - if in single file mode
			if (options.SingleFile) {
				strFileName = options.SqlScriptDirectory + "\\" + connection.Database.Replace(" ", "_") + ".sql";
				if (File.Exists(strFileName))
					File.Delete(strFileName);
				writer = new StreamWriter(strFileName);
			}

			// Process each table
			foreach (DataRow objDataRow in objDataTable.Rows) {
				if (objDataRow["TABLE_TYPE"].ToString() == "BASE TABLE" && objDataRow["TABLE_NAME"].ToString() != "dtproperties") {
					strTableName = objDataRow["TABLE_NAME"].ToString();
					ProcessTable(strTableName);
				}
			}
			
			// Close and deallocate the file stream
			if (options.SingleFile) {
				writer.Close();
				writer = null;
			}
		}

		/// <summary>
		/// Processes the specified table, creating stored procedures and C# data access classes for it.
		/// </summary>
		/// <param name="strTableName">Name of the table to be processed.</param>
		private void ProcessTable(string strTableName) {
			ArrayList		arrFieldList;
			int				intIndex;
			Field		objField;
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
			if (options.UseViews) {
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
			objDataAdapter = new SqlDataAdapter(sql, connection);
			objDataAdapter.Fill(objDataTable);

			// Store each field's information in the field list
			arrFieldList = new ArrayList();
			foreach (DataRow objDataRow in objDataTable.Rows) {
				if (objDataRow["COLUMN_COMPUTED"].ToString() == "0") {
					// Create the array
					objField = new Field();
					objField.ColumnName = objDataRow["COLUMN_NAME"].ToString();
					objField.DBType = objDataRow["DATA_TYPE"].ToString();
					if (objDataRow["CHARACTER_MAXIMUM_LENGTH"].ToString().Length > 0)
						objField.Length = (Int32)objDataRow["CHARACTER_MAXIMUM_LENGTH"];
					else
						objField.Length = (Int32)(Int16)objDataRow["COLUMN_LENGTH"];
					if (System.DBNull.Value.GetType() != objDataRow["NUMERIC_PRECISION"].GetType() ) objField.Precision = (Int32)(Byte)objDataRow["NUMERIC_PRECISION"];
					if (!System.DBNull.Value.Equals(objDataRow["NUMERIC_SCALE"])) objField.Scale = (Int32)objDataRow["NUMERIC_SCALE"];
					objField.IsPrimaryKey = false;
					objField.IsViewColumn = objDataRow["VIEW_COLUMN"].ToString() == "1";

					// Check for unicode columns
					if (objField.DBType.ToLower() == "nchar" || objField.DBType.ToLower() == "nvarchar" || objField.DBType.ToLower() == "ntext") {
						intLength = objField.Length;
						intLength /= 2;
						objField.Length = intLength;
					}
					
					// Check for text or ntext columns, which require a different length from what SQL Server reports
					if (objField.DBType.ToLower() == "text")
						objField.Length = 2147483647;
					else if (objField.DBType.ToLower() == "ntext")
						objField.Length = 1073741823;

					// Check to see if the current field is a primary key
					objDataTableConstraint = new DataTable();
					objDataAdapter = new SqlDataAdapter("SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS ON INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.CONSTRAINT_NAME = INFORMATION_SCHEMA.TABLE_CONSTRAINTS.CONSTRAINT_NAME WHERE INFORMATION_SCHEMA.TABLE_CONSTRAINTS.TABLE_NAME = '" + strTableName + "'", connection);
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
				objField = (Field)arrFieldList[intIndex];
				
				objDataTable = new DataTable();
				objDataAdapter = new SqlDataAdapter("SELECT ColumnProperty(OBJECT_ID('" + strTableName + "'), '" + objField.ColumnName + "', 'IsIdentity') AS IsIdentity", connection);
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
				objField = (Field)arrFieldList[intIndex];
				
				objDataTable = new DataTable();
				objDataAdapter = new SqlDataAdapter("SELECT ColumnProperty(OBJECT_ID('" + strTableName + "'), '" + objField.ColumnName + "', 'IsRowGuidCol') AS IsRowGuidCol", connection);
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
				objField = (Field)arrFieldList[intIndex];
				
				objDataTable = new DataTable();
				objDataAdapter = new SqlDataAdapter("SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS ON INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.CONSTRAINT_NAME = INFORMATION_SCHEMA.TABLE_CONSTRAINTS.CONSTRAINT_NAME WHERE INFORMATIoN_SCHEMA.TABLE_CONSTRAINTS.TABLE_NAME = '" + strTableName + "' AND CONSTRAINT_TYPE = 'FOREIGN KEY' AND COLUMN_NAME = '" + objField.ColumnName + "'", connection);
				objDataAdapter.Fill(objDataTable);
				
				if (objDataTable.Rows.Count > 0)
					objField.IsForeignKey = true;

				objDataTable = null;
				objDataAdapter = null;
			}

			// create views
            SQLGenerator sqlgen = new SQLGenerator(options, writer, strTableName, arrFieldList);
			//sqlgen.CreateView();
			//sqlgen.CreateInsertStoredProcedure();
			//sqlgen.CreateUpdateStoredProcedure();
			sqlgen.CreateDeleteStoredProcedures();
			if (options.GenerateSelectStoredProcs) sqlgen.CreateSelectStoredProcedures();

			// create classes
            DAOGenerator daogen = new DAOGenerator(options, writer, strTableName, arrFieldList);
            DOGenerator dogen = new DOGenerator(options, writer, strTableName, arrFieldList);
			//dogen.CreateDataObjectClass();
			//daogen.CreateDataAccessClass();
		}

	}
}

