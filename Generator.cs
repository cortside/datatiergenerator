// This component makes extensive use of inline documentation comments.
// In Visual Studio .NET, select <Build Comment Web Pages> on the <Tools> menu to create HTML documentation of the class.

using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator {
	/// <summary>
	/// Generates stored procedures and associated data access code for the specified database.
	/// </summary>
	public class Generator : GeneratorBase {
		// Connection to the database
		private	SqlConnection connection;

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
			CreateDirectories();

			// Create and open the file stream - if in single file mode
			if (options.SingleFile) {
				String strFileName = options.SqlScriptDirectory + "\\" + connection.Database.Replace(" ", "_") + ".sql";
				if (File.Exists(strFileName))
					File.Delete(strFileName);
				writer = new StreamWriter(strFileName);
			}

			// Process each table
			ArrayList entities = GetEntities();
			foreach (Entity entity in entities) {
				ProcessTable(entity);
			}
			
			// Close and deallocate the file stream
			if (options.SingleFile) {
				writer.Close();
				writer = null;
			}
		}

		private ArrayList GetEntities() {
			ArrayList entities = new ArrayList();

			if (options.XmlConfigFilename.Length > 0) {
				// get entities from xml
				XmlDocument doc = new XmlDocument();
				doc.Load(options.XmlConfigFilename);
				XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("entity");
				for (Int32 i = 0; i<elements.Count; i++) {
					XmlNode node = elements[i];
					Entity entity = new Entity();
					entity.Name = node.Attributes["name"].Value;
					entity.SqlObject = node.Attributes["sqlobject"].Value;
					entities.Add(entity);
				}
			}

			if (options.AutoDiscoverEntities) {
				// Get a list of the entities in the database
				DataTable objDataTable = new DataTable();
				SqlDataAdapter objDataAdapter = new SqlDataAdapter("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG = '" + connection.Database + "'", connection);
				objDataAdapter.Fill(objDataTable);
				foreach (DataRow row in objDataTable.Rows) {
					if (row["TABLE_TYPE"].ToString() == "BASE TABLE" && row["TABLE_NAME"].ToString() != "dtproperties") {
						if (FindEntityBySqlObject(entities, row["TABLE_NAME"].ToString()) == null) {
							Entity entity = new Entity();
							entity.Name = row["TABLE_NAME"].ToString();
							entity.SqlObject = row["TABLE_NAME"].ToString();
							entities.Add(entity);
						}
					}
				}
			}

			return entities;
		}

		private Entity FindEntityBySqlObject(ArrayList entities, String sqlObject) {
			foreach (Entity entity in entities) {
				if (entity.SqlObject == sqlObject) {
					return entity;
				}
			}
			return null;
		}
		
		private void CreateDirectories() {
			// Check to see if the "SQL Scripts" directory exists; if not, create it; otherwise, clear it out
			if (!Directory.Exists(options.RootDirectory + options.SqlScriptDirectory))
				Directory.CreateDirectory(options.RootDirectory + options.SqlScriptDirectory);

			// Check to see if the "Data Access Classes" directory exists; if not, create it; otherwise, clear it out
			if (!Directory.Exists(options.RootDirectory + options.DaoClassDirectory))
				Directory.CreateDirectory(options.RootDirectory + options.DaoClassDirectory);

			// Check to see if the "Data Object Classes" directory exists; if not, create it; otherwise, clear it out
			if (!Directory.Exists(options.RootDirectory + options.DoClassDirectory))
				Directory.CreateDirectory(options.RootDirectory + options.DoClassDirectory);
		}


		/// <summary>
		/// Processes the specified table, creating stored procedures and C# data access classes for it.
		/// </summary>
		/// <param name="strTableName">Name of the table to be processed.</param>
		private void ProcessTable(Entity entity) {
			ArrayList arrFieldList = GetFields(entity);

			// create views
            SQLGenerator sqlgen = new SQLGenerator(options, writer, entity, arrFieldList);
			if (!String.Empty.Equals(entity.SqlObject)) sqlgen.CreateView();
			if (!String.Empty.Equals(entity.SqlObject)) sqlgen.CreateInsertStoredProcedure();
			if (!String.Empty.Equals(entity.SqlObject)) sqlgen.CreateUpdateStoredProcedure();
			if (!String.Empty.Equals(entity.SqlObject)) sqlgen.CreateDeleteStoredProcedures();
			if (options.GenerateSelectStoredProcs && !String.Empty.Equals(entity.SqlObject)) sqlgen.CreateSelectStoredProcedures();

			// create classes
            DAOGenerator daogen = new DAOGenerator(options, writer, entity, arrFieldList);
            DOGenerator dogen = new DOGenerator(options, writer, entity, arrFieldList);
			dogen.CreateDataObjectClass();
			if (!String.Empty.Equals(entity.SqlObject)) daogen.CreateDataAccessClass();
		}

		private ArrayList GetFields(Entity entity) {
			ArrayList fields = new ArrayList();

			if (options.XmlConfigFilename.Length > 0) {
				// get entities from xml
				XmlDocument doc = new XmlDocument();
				doc.Load(options.XmlConfigFilename);
				XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("entity");
				foreach (XmlNode element in elements) {
					String name = element.Attributes["name"].Value;
					String sqlObject = element.Attributes["sqlobject"].Value;
					if (((entity.SqlObject.Length>0 && sqlObject == entity.SqlObject) || (entity.SqlObject.Length==0 && name == entity.Name)) && element.HasChildNodes) {
						foreach (XmlNode node in element.ChildNodes) {
							Field field = new Field();
							if (node.Attributes["name"] != null) {
								field.ColumnName = node.Attributes["name"].Value;
							}
							if (node.Attributes["sqltype"] != null) {
								field.SqlType = node.Attributes["sqltype"].Value;
							}
							if (node.Attributes["length"] != null) {
								field.Length = Int32.Parse(node.Attributes["length"].Value);
							}
							if (node.Attributes["scale"] != null) {
								field.Scale = Int32.Parse(node.Attributes["scale"].Value);
							}
							if (node.Attributes["precision"] != null) {
								field.Precision = Int32.Parse(node.Attributes["precision"].Value);
							}
							if (node.Attributes["datatype"] != null) {
								field.DataType = node.Attributes["datatype"].Value;
							} else if (options.UseDataTypes) {
								if (field.IsIdentity) {
									field.DataType = "Spring2.Core.Types.IdType";
								} else {
									field.DataType = Field.GetSpring2DataType(field.SqlType);
								}
							}
							if (node.Attributes["constructor"] != null) {
								field.Constructor = node.Attributes["constructor"].Value;
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

			if (options.AutoDiscoverProperties) {
				DataTable columns = GetTableColumns(entity.SqlObject);
				foreach (DataRow objDataRow in columns.Rows) {
					if (objDataRow["COLUMN_COMPUTED"].ToString() == "0") {
						if (FindFieldByName(fields, objDataRow["COLUMN_NAME"].ToString()) == null) {
							Field field = new Field();
							field.ColumnName = objDataRow["COLUMN_NAME"].ToString();
							field.SqlType = objDataRow["DATA_TYPE"].ToString();
							field.Length = objDataRow["CHARACTER_MAXIMUM_LENGTH"].ToString().Length > 0 ? (Int32)objDataRow["CHARACTER_MAXIMUM_LENGTH"] : (Int32)(Int16)objDataRow["COLUMN_LENGTH"];
							if (!System.DBNull.Value.Equals(objDataRow["NUMERIC_PRECISION"])) field.Precision = (Int32)(Byte)objDataRow["NUMERIC_PRECISION"];
							if (!System.DBNull.Value.Equals(objDataRow["NUMERIC_SCALE"])) field.Scale = (Int32)objDataRow["NUMERIC_SCALE"];
							field.IsIdentity = objDataRow["IsIdentity"].ToString() == "1";
							field.IsPrimaryKey = objDataRow["IsPrimaryKey"].ToString() == "1";
							field.IsRowGuidCol = objDataRow["IsRowGuidCol"].ToString() == "1";
							field.IsForeignKey = objDataRow["IsForeignKey"].ToString() == "1";
							field.IsViewColumn = objDataRow["IsViewColumn"].ToString() == "1";

							if (options.UseDataTypes) {
								if (field.IsIdentity) {
									field.DataType = "Spring2.Core.Types.IdType";
								} else {
									field.DataType = Field.GetSpring2DataType(field.SqlType);
								}
							}

							// Check for unicode columns
							if (field.SqlType.ToLower() == "nchar" || field.SqlType.ToLower() == "nvarchar" || field.SqlType.ToLower() == "ntext") {
								field.Length = field.Length / 2;
							}
					
							// Check for text or ntext columns, which require a different length from what SQL Server reports
							if (field.SqlType.ToLower() == "text") {
								field.Length = 2147483647;
							} else if (field.SqlType.ToLower() == "ntext") {
								field.Length = 1073741823;
							}
					
							// Append the array to the array list
							fields.Add(field);
						}
					}
				}
			}

			return fields;
		}

		private Field FindFieldByName(ArrayList fields, String name) {
			foreach (Field field in fields) {
				if (field.ColumnName == name) {
					return field;
				}
			}
			return null;
		}


		private DataTable GetTableColumns(string strTableName) {
			String sql = "	SELECT	INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, ";
			sql = sql + " 		INFORMATION_SCHEMA.COLUMNS.DATA_TYPE, ";
			sql = sql + " 		INFORMATION_SCHEMA.COLUMNS.CHARACTER_MAXIMUM_LENGTH, ";
			sql = sql + " 		INFORMATION_SCHEMA.COLUMNS.NUMERIC_SCALE, ";
			sql = sql + " 		INFORMATION_SCHEMA.COLUMNS.NUMERIC_PRECISION, ";
			sql = sql + " 		systypes.length AS COLUMN_LENGTH, ";
			sql = sql + " 		syscolumns.iscomputed AS COLUMN_COMPUTED, ";
			sql = sql + "		'0' IsViewColumn, ";
			sql = sql + "		coalesce(VC.colid, 1000+ORDINAL_POSITION) COLUMN_ID, ";
			sql = sql + "		ColumnProperty(OBJECT_ID(INFORMATION_SCHEMA.COLUMNS.TABLE_NAME), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsIdentity') AS IsIdentity, ";
			sql = sql + "		ColumnProperty(OBJECT_ID(INFORMATION_SCHEMA.COLUMNS.TABLE_NAME), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsRowGuidCol') AS IsRowGuidCol, ";
			sql = sql + "		case when INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME in (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS ON INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.CONSTRAINT_NAME = INFORMATION_SCHEMA.TABLE_CONSTRAINTS.CONSTRAINT_NAME WHERE INFORMATIoN_SCHEMA.TABLE_CONSTRAINTS.TABLE_NAME = INFORMATION_SCHEMA.COLUMNS.TABLE_NAME AND CONSTRAINT_TYPE = 'FOREIGN KEY') then 1 else 0 end IsForeignKey, ";
			sql = sql + "		case when INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME in (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS ON INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.CONSTRAINT_NAME = INFORMATION_SCHEMA.TABLE_CONSTRAINTS.CONSTRAINT_NAME WHERE INFORMATIoN_SCHEMA.TABLE_CONSTRAINTS.TABLE_NAME = INFORMATION_SCHEMA.COLUMNS.TABLE_NAME AND CONSTRAINT_TYPE = 'PRIMARY KEY') then 1 else 0 end IsPrimaryKey ";
			sql = sql + " 	FROM INFORMATION_SCHEMA.COLUMNS ";
			sql = sql + "  	INNER JOIN systypes ON INFORMATION_SCHEMA.COLUMNS.DATA_TYPE = systypes.name ";
			sql = sql + "  	INNER JOIN syscolumns ON INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME = syscolumns.name  AND syscolumns.id = OBJECT_ID('" + strTableName + "') ";
			sql = sql + "	left join syscolumns vc on INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME = vc.name AND vc.id = OBJECT_ID('" + strTableName + "') ";
			sql = sql + "  	WHERE INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = '" + strTableName + "' ";

			// if basing data objects on views, get additional fields found in the corresponding view (by naming convention of vw + tablename) -- should be configuration option
			if (options.UseViews) {
				sql = sql + "union ";
				sql = "	SELECT	INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, ";
				sql = sql + " 		INFORMATION_SCHEMA.COLUMNS.DATA_TYPE, ";
				sql = sql + " 		INFORMATION_SCHEMA.COLUMNS.CHARACTER_MAXIMUM_LENGTH, ";
				sql = sql + " 		INFORMATION_SCHEMA.COLUMNS.NUMERIC_SCALE, ";
				sql = sql + " 		INFORMATION_SCHEMA.COLUMNS.NUMERIC_PRECISION, ";
				sql = sql + "  		systypes.length AS COLUMN_LENGTH, ";
				sql = sql + "  		syscolumns.iscomputed AS COLUMN_COMPUTED, ";
				sql = sql + " 		'1' IsViewColumn, ";
				sql = sql + "		ORDINAL_POSITION COLUMN_ID, ";
				sql = sql + "		ColumnProperty(OBJECT_ID(INFORMATION_SCHEMA.COLUMNS.TABLE_NAME), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsIdentity') AS IsIdentity, ";
				sql = sql + "		ColumnProperty(OBJECT_ID(INFORMATION_SCHEMA.COLUMNS.TABLE_NAME), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsRowGuidCol') AS IsRowGuidCol, ";
				sql = sql + "		case when INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME in (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS ON INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.CONSTRAINT_NAME = INFORMATION_SCHEMA.TABLE_CONSTRAINTS.CONSTRAINT_NAME WHERE INFORMATIoN_SCHEMA.TABLE_CONSTRAINTS.TABLE_NAME = INFORMATION_SCHEMA.COLUMNS.TABLE_NAME AND CONSTRAINT_TYPE = 'FOREIGN KEY') then 1 else 0 end IsForeignKey, ";
				sql = sql + "		case when INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME in (SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS ON INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.CONSTRAINT_NAME = INFORMATION_SCHEMA.TABLE_CONSTRAINTS.CONSTRAINT_NAME WHERE INFORMATIoN_SCHEMA.TABLE_CONSTRAINTS.TABLE_NAME = INFORMATION_SCHEMA.COLUMNS.TABLE_NAME AND CONSTRAINT_TYPE = 'PRIMARY KEY') then 1 else 0 end IsPrimaryKey ";
				sql = sql + " 	FROM INFORMATION_SCHEMA.COLUMNS ";
				sql = sql + " 	INNER JOIN systypes ON INFORMATION_SCHEMA.COLUMNS.DATA_TYPE = systypes.name ";
				sql = sql + " 	INNER JOIN syscolumns ON INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME = syscolumns.name ";
				sql = sql + " 	WHERE INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = 'vw" + strTableName + "' AND syscolumns.id = OBJECT_ID('vw" + strTableName + "') ";
				sql = sql + " 	and INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME not in (select INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = '" + strTableName + "') ";
			}

			sql = sql + "order by column_id ";

			// Fill the dataset with the information for the current table
			DataTable objDataTable = new DataTable();
			SqlDataAdapter objDataAdapter = new SqlDataAdapter(sql, connection);
			objDataAdapter.Fill(objDataTable);
			return objDataTable;
		}

		public void GenerateXML() {
			ArrayList entities = GetEntities();
			StringBuilder sb = new StringBuilder();
			sb.Append("\t<entities>\n");
			foreach (Entity entity in entities) {
				sb.Append("\t\t<entity name=\"").Append(entity.Name).Append("\"");
				sb.Append(" sqlobject=\"").Append(entity.SqlObject).Append("\"");
				sb.Append(">\n");
				ArrayList columns = GetFields(entity);
				for (Int32 i = 0; i<columns.Count; i++) {
					Field field = (Field)columns[i];
					sb.Append("\t\t\t<property");
					sb.Append(" name=\"").Append(field.ColumnName).Append("\"");
					sb.Append(" sqltype=\"").Append(field.SqlType).Append("\"");
					if (field.IsText) {
						sb.Append(" length=\"").Append(field.Length.ToString()).Append("\"");
					}
					if (field.IsNumber || field.IsDecimal || field.IsCurrency) {
						sb.Append(" scale=\"").Append(field.Scale.ToString()).Append("\"");
						sb.Append(" precision=\"").Append(field.Precision.ToString()).Append("\"");
					}
					sb.Append(" datatype=\"").Append(Field.GetSpring2DataType(field.SqlType)).Append("\"");
					if (field.IsIdentity) {
						sb.Append(" isidentity=\"True\"");
					}
					if (field.IsPrimaryKey) {
						sb.Append(" isprimarykey=\"True\"");
					}
					if (field.IsRowGuidCol) {
						sb.Append(" isrowguidcol=\"True\"");
					}
					if (field.IsForeignKey) {
						sb.Append(" isforeignkey=\"True\"");
					}
					if (field.IsViewColumn) {
						sb.Append(" isviewcolumn=\"True\"");
					}
					sb.Append(" />\n");
				}
				sb.Append("\t\t</entity>\n");
			}
			sb.Append("\t</entities>\n");
			WriteToFile(options.XmlConfigFilename + ".generated.xml", sb.ToString());
		}


	}
}

