using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator {

    public class Generator : GeneratorBase {
	private ArrayList entities = new ArrayList();
	private ArrayList sqlentities = new ArrayList();
	private Hashtable types = new Hashtable();
	private Hashtable sqltypes = new Hashtable();

	public Generator(Configuration options) {
	    this.options = options;
	}
		
	public void GenerateSource() {
	    Console.Out.Write("\n");
	    Console.Out.WriteLine(String.Empty.PadLeft(20,'='));
	    Console.Out.WriteLine("Parse/Load entities and properties");
	    Console.Out.WriteLine(String.Empty.PadLeft(20,'='));

	    SqlConnection connection = null;
	    if (options.AutoDiscoverEntities || options.AutoDiscoverProperties) {
		connection = new SqlConnection(options.ConnectionString);
		connection.Open();
	    }

	    XmlDocument doc = null;
	    if (options.XmlConfigFilename.Length > 0) {
		doc = new XmlDocument();
		doc.Load(options.XmlConfigFilename);
	    }

	    // Process each table
	    sqltypes = SqlType.ParseFromXml(doc);
	    types = Type.ParseFromXml(options, doc);
	    entities = GetEntities(doc, connection);
	    sqlentities = GetSqlEntities(doc, connection);

	    foreach (SqlEntity sqlentity in sqlentities) {
		SQLGenerator sqlgen = new SQLGenerator(options, sqlentity);
		sqlgen.CreateTable();
		if (options.GenerateSqlViewScripts) sqlgen.CreateView();
		sqlgen.CreateInsertStoredProcedure();
		if (sqlentity.HasUpdatableColumns()) sqlgen.CreateUpdateStoredProcedure();
		sqlgen.CreateDeleteStoredProcedures();
//		if (options.GenerateSelectStoredProcs) sqlgen.CreateSelectStoredProcedures();
	    }

	    foreach (Entity entity in entities) {
		if (!entity.HasUpdatableFields()) {
		    Console.Out.WriteLine("WARNING: entity " + entity.Name + " does not have any editable fields and does not have x specified.  No update stored procedures or update DAO methods will be generated.");
		}
		// create classes
		DAOGenerator daogen = new DAOGenerator(options, entity);
		DOGenerator dogen = new DOGenerator(options, entity);
		dogen.CreateDataObjectClass();
		if (!String.Empty.Equals(entity.SqlObject)) daogen.CreateDataAccessClass();
	    }

	    ArrayList enumtypes = EnumType.ParseFromXml(options,doc,sqltypes,types);
	    foreach (EnumType type in enumtypes) {
		EnumGenerator eg = new EnumGenerator(options, type);
		eg.Generate();
	    }
			
	    Console.Out.WriteLine(String.Empty.PadLeft(20,'='));
	}

	public void GenerateXML() {
	    SqlConnection connection = new SqlConnection(options.ConnectionString);
	    connection.Open();

	    ArrayList entities = GetEntities(null, connection);
	    StringBuilder sb = new StringBuilder();
	    sb.Append("\t<entities>\n");
	    foreach (Entity entity in entities) {
		sb.Append("\t\t<entity name=\"").Append(entity.Name).Append("\"");
		sb.Append(" sqlobject=\"").Append(entity.SqlObject).Append("\"");
		sb.Append(">\n");
		//ArrayList columns = GetFields(entity, connection, null, new Hashtable(), new Hashtable());
		foreach (Field field in entity.Fields) {
		    sb.Append("\t\t\t").Append(field.ToXml(true)).Append("\n");
		}
		sb.Append("\t\t</entity>\n");
	    }
	    sb.Append("\t</entities>\n");
	    FileInfo file = new FileInfo(options.XmlConfigFilename + ".generated.xml");
	    WriteToFile(file, sb.ToString(), false);
	}


	private ArrayList GetEntities(XmlDocument doc, SqlConnection connection) {
	    ArrayList entities = new ArrayList();

	    if (doc != null) {
		entities.AddRange(Entity.ParseFromXml(options, doc, sqltypes, types));

		if (options.AutoDiscoverProperties) {
		    foreach (Entity entity in entities) {
			entity.Fields = GetFields(entity, connection, doc, sqltypes, types);
		    }
		}
	    }

	    if (options.AutoDiscoverEntities) {
		// Get a list of the entities in the database
		DataTable objDataTable = new DataTable();
		SqlDataAdapter objDataAdapter = new SqlDataAdapter("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG = '" + connection.Database + "'", connection);
		objDataAdapter.Fill(objDataTable);
		foreach (DataRow row in objDataTable.Rows) {
		    if (row["TABLE_TYPE"].ToString() == "BASE TABLE" && row["TABLE_NAME"].ToString() != "dtproperties") {
			if (Entity.FindEntityBySqlObject(entities, row["TABLE_NAME"].ToString()) == null) {
			    Entity entity = new Entity();
			    entity.Name = row["TABLE_NAME"].ToString();
			    entity.SqlObject = row["TABLE_NAME"].ToString();
			    if (options.UseViews) {
				entity.SqlView = "vw" + entity.SqlObject;
			    }
			    entity.Fields = GetFields(entity, connection, doc, sqltypes, types);
			    entities.Add(entity);
			}
		    }
		}	    
	    }

	    return entities;
	}


	private ArrayList GetSqlEntities(XmlDocument doc, SqlConnection connection) {
	    ArrayList entities = new ArrayList();

	    if (doc != null) {
		entities.AddRange(SqlEntity.ParseFromXml(options, doc, sqltypes, types));

		if (options.AutoDiscoverProperties) {
		    foreach (Entity entity in entities) {
			entity.Fields = GetFields(entity, connection, doc, sqltypes, types);
		    }
		}
	    }
	    return entities;
	}


	private ArrayList GetFields(Entity entity, SqlConnection connection, XmlDocument doc, Hashtable sqltypes, Hashtable types) {
	    ArrayList fields = entity.Fields;

	    Boolean foundNewProperties=false;
	    if (options.AutoDiscoverProperties) {
		DataTable columns = GetTableColumns(entity, connection);
		foreach (DataRow objDataRow in columns.Rows) {
		    if (objDataRow["COLUMN_COMPUTED"].ToString() == "0") {
			if (entity.FindFieldBySqlName(objDataRow["COLUMN_NAME"].ToString()) == null) {
			    Field field = new Field();
			    field.Name = objDataRow["COLUMN_NAME"].ToString();
			    field.SqlName = field.Name;

			    field.SqlType.Name = objDataRow["DATA_TYPE"].ToString();
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

			    field.SqlType.Length = objDataRow["CHARACTER_MAXIMUM_LENGTH"].ToString().Length > 0 ? (Int32)objDataRow["CHARACTER_MAXIMUM_LENGTH"] : (Int32)(Int16)objDataRow["COLUMN_LENGTH"];
			    if (!System.DBNull.Value.Equals(objDataRow["NUMERIC_PRECISION"])) field.SqlType.Precision = (Int32)(Byte)objDataRow["NUMERIC_PRECISION"];
			    if (!System.DBNull.Value.Equals(objDataRow["NUMERIC_SCALE"])) field.SqlType.Scale = (Int32)objDataRow["NUMERIC_SCALE"];
			    field.IsIdentity = objDataRow["IsIdentity"].ToString() == "1";
			    field.IsPrimaryKey = objDataRow["IsPrimaryKey"].ToString() == "1";
			    field.IsRowGuidCol = objDataRow["IsRowGuidCol"].ToString() == "1";
			    field.IsForeignKey = objDataRow["IsForeignKey"].ToString() == "1";
			    field.IsViewColumn = objDataRow["IsViewColumn"].ToString() == "1";

			    // Check for unicode columns
			    if (field.SqlType.Name.ToLower() == "nchar" || field.SqlType.Name.ToLower() == "nvarchar" || field.SqlType.Name.ToLower() == "ntext") {
				field.SqlType.Length = field.SqlType.Length / 2;
			    }
					
			    // Check for text or ntext columns, which require a different length from what SQL Server reports
			    if (field.SqlType.Name.ToLower() == "text") {
				field.SqlType.Length = 2147483647;
			    } else if (field.SqlType.Name.ToLower() == "ntext") {
				field.SqlType.Length = 1073741823;
			    }
					
			    // Append the array to the array list
			    fields.Add(field);

			    if (!foundNewProperties) {
				Console.Out.WriteLine(entity.ToXml());
				foundNewProperties=true;
			    }
			    Console.Out.WriteLine("\t" + field.ToXml(true));

			}
		    }
		}
	    }
	    if (foundNewProperties) {
		Console.Out.WriteLine("</entity>\n");
	    }

	    return fields;
	}


	private DataTable GetTableColumns(Entity entity, SqlConnection connection) {
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
	    sql = sql + "  	INNER JOIN syscolumns ON INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME = syscolumns.name  AND syscolumns.id = OBJECT_ID('" + entity.SqlObject + "') ";
	    sql = sql + "	left join syscolumns vc on INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME = vc.name AND vc.id = OBJECT_ID('" + entity.SqlObject + "') ";
	    sql = sql + "  	WHERE INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = '" + entity.SqlObject + "' ";

	    // if basing data objects on views, get additional fields found in the corresponding view (by naming convention of vw + tablename) -- should be configuration option
	    if (options.UseViews && entity.SqlView.Length>1) {
		sql = sql + "union ";
		sql = sql + "	SELECT	INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, ";
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
		sql = sql + " 	WHERE INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = '" + entity.SqlView + "' AND syscolumns.id = OBJECT_ID('" + entity.SqlView + "') ";
		sql = sql + " 	and INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME not in (select INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = '" + entity.SqlObject + "') ";
	    }

	    sql = sql + "order by column_id ";

	    // Fill the dataset with the information for the current table
	    DataTable objDataTable = new DataTable();
	    SqlDataAdapter objDataAdapter = new SqlDataAdapter(sql, connection);
	    objDataAdapter.Fill(objDataTable);
	    //objDataTable.R
	    return objDataTable;
	}

    }
}

