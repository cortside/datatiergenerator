using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator.Element;
using Spring2.DataTierGenerator.Generator;

namespace Spring2.DataTierGenerator.Parser {

    /// <summary>
    /// Summary description for DatabaseParser.
    /// </summary>
    public class DatabaseParser : AbstractParser {
	
//	public DatabaseParser(ParserElement parser, ConfigurationElement options, XmlDocument doc) : base(parser, options, doc) {
//	    this.options = options;
//	    this.sqltypes = sqltypes;
//	    this.types = types;
//	    enumtypes = EnumElement.ParseFromXml(options, doc, sqltypes, types, this);
//	    collections = CollectionElement.ParseFromXml(options, doc, sqltypes, types, this);
//
//	    if (parser.FindArgumentByName("server") == null || parser.FindArgumentByName("database") == null || parser.FindArgumentByName("user") == null || parser.FindArgumentByName("password") == null) {
//		this.AddValidationMessage(ParserValidationMessage.NewError("expected to find the following arguments, but didn't: server, database, user, password."));
//	    } else {
//		String connectionString = "server=" + parser.FindArgumentByName("server").Value + ";databse=" + parser.FindArgumentByName("database").Value + ";user=" + parser.FindArgumentByName("user").Value + ";password=" + parser.FindArgumentByName("password").Value + ";";
//		
//		DatabaseElement db = new DatabaseElement();
//		db.Name = "db";
//		db.Server = parser.FindArgumentByName("server").Value;
//		db.Database = parser.FindArgumentByName("database").Value;
//		db.User = parser.FindArgumentByName("user").Value;
//		db.Password = parser.FindArgumentByName("password").Value;
//		connectionString = db.ConnectionString;
//
//		SqlConnection conn = new SqlConnection(connectionString);
//		db.SqlEntities = DiscoverSqlEntities(conn, this);
//		databases.Add(db);
//		entities = GetEntities(doc, conn, new ArrayList(), this);
//	    }
//
//	    Validate();
//	}

	private ArrayList GetEntities(XmlDocument doc, SqlConnection connection, ArrayList sqlentities, IParser vd) {
	    ArrayList entities = new ArrayList();

	    // Get a list of the entities in the database
	    DataTable objDataTable = new DataTable();
	    SqlDataAdapter objDataAdapter = new SqlDataAdapter("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG = '" + connection.Database + "'", connection);
	    objDataAdapter.Fill(objDataTable);
	    foreach (DataRow row in objDataTable.Rows) {
	    	if (row["TABLE_TYPE"].ToString() == "BASE TABLE" && row["TABLE_NAME"].ToString() != "dtproperties") {
	    	    EntityElement entity = new EntityElement();
	    	    entity.Name = row["TABLE_NAME"].ToString();
	    	    entity.SqlEntity.Name = row["TABLE_NAME"].ToString();
	    	    entity.SqlEntity.View = "vw" + entity.SqlEntity.Name;
	    	    entity.Properties = GetFields(entity, connection, doc, sqltypes, types);
	    	    entities.Add(entity);
	    	}
	    }	    

	    return entities;
	}


	private ArrayList GetFields(EntityElement entity, SqlConnection connection, XmlDocument doc, Hashtable sqltypes, Hashtable types) {
	    ArrayList fields = entity.Properties;

	    if (entity.SqlEntity.AutoDiscoverProperties) {
		DataTable columns = GetTableColumns(entity.SqlEntity, connection);
		foreach (DataRow objDataRow in columns.Rows) {
		    if (objDataRow["COLUMN_COMPUTED"].ToString() == "0") {
			if (entity.SqlEntity.FindColumnByName(objDataRow["COLUMN_NAME"].ToString()) == null) {
			    PropertyElement field = new PropertyElement();
			    field.Name = objDataRow["COLUMN_NAME"].ToString();
			    field.Column.Name = field.Name;

			    field.Column.SqlType.Name = objDataRow["DATA_TYPE"].ToString();
			    // if the sql type is defined, default to all values defined in it
			    if (sqltypes.ContainsKey(field.Column.SqlType.Name)) {
				field.Column.SqlType = (SqlTypeElement)((SqlTypeElement)sqltypes[field.Column.SqlType.Name]).Clone();
				if (types.Contains(field.Column.SqlType.Type)) {
				    field.Type = (TypeElement)((TypeElement)types[field.Column.SqlType.Type]).Clone();
				} else {
				    WriteToLog("Type " + field.Column.SqlType.Type + " was not defined");
				}
			    } else {
				WriteToLog("SqlType " + field.Column.SqlType.Name + " was not defined");
			    }

			    field.Column.SqlType.Length = objDataRow["CHARACTER_MAXIMUM_LENGTH"].ToString().Length > 0 ? (Int32)objDataRow["CHARACTER_MAXIMUM_LENGTH"] : (Int32)(Int16)objDataRow["COLUMN_LENGTH"];
			    if (!System.DBNull.Value.Equals(objDataRow["NUMERIC_PRECISION"])) field.Column.SqlType.Precision = (Int32)(Byte)objDataRow["NUMERIC_PRECISION"];
			    if (!System.DBNull.Value.Equals(objDataRow["NUMERIC_SCALE"])) field.Column.SqlType.Scale = (Int32)objDataRow["NUMERIC_SCALE"];
			    field.Column.Identity = objDataRow["IsIdentity"].ToString() == "1";
			    field.Column.RowGuidCol = objDataRow["IsRowGuidCol"].ToString() == "1";

			    // Check for unicode columns
			    if (field.Column.SqlType.Name.ToLower() == "nchar" || field.Column.SqlType.Name.ToLower() == "nvarchar" || field.Column.SqlType.Name.ToLower() == "ntext") {
				field.Column.SqlType.Length = field.Column.SqlType.Length / 2;
			    }
					
			    // Check for text or ntext columns, which require a different length from what SQL Server reports
			    if (field.Column.SqlType.Name.ToLower() == "text") {
				field.Column.SqlType.Length = 2147483647;
			    } else if (field.Column.SqlType.Name.ToLower() == "ntext") {
				field.Column.SqlType.Length = 1073741823;
			    }
					
			    // Append the array to the array list
			    fields.Add(field);
			}
		    }
		}
	    }

	    return fields;
	}


	private DataTable GetTableColumns(SqlEntityElement sqlentity, SqlConnection connection) {
	    String sql = "	SELECT	INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, \n";
	    sql = sql + " 		INFORMATION_SCHEMA.COLUMNS.DATA_TYPE, \n";
	    sql = sql + " 		INFORMATION_SCHEMA.COLUMNS.CHARACTER_MAXIMUM_LENGTH, \n";
	    sql = sql + " 		INFORMATION_SCHEMA.COLUMNS.NUMERIC_SCALE, \n";
	    sql = sql + " 		INFORMATION_SCHEMA.COLUMNS.NUMERIC_PRECISION, \n";
	    sql = sql + " 		systypes.length AS COLUMN_LENGTH, \n";
	    sql = sql + " 		syscolumns.iscomputed AS COLUMN_COMPUTED, \n";
	    sql = sql + "		'0' IsViewColumn, \n";
	    sql = sql + "		coalesce(VC.colid, 1000+ORDINAL_POSITION) COLUMN_ID, \n";
	    sql = sql + "		ColumnProperty(OBJECT_ID(INFORMATION_SCHEMA.COLUMNS.TABLE_NAME), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsIdentity') AS IsIdentity, \n";
	    sql = sql + "		ColumnProperty(OBJECT_ID(INFORMATION_SCHEMA.COLUMNS.TABLE_NAME), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsRowGuidCol') AS IsRowGuidCol, \n";
	    sql = sql + " 		INFORMATION_SCHEMA.COLUMNS.IS_NULLABLE \n";
	    sql = sql + " 	FROM INFORMATION_SCHEMA.COLUMNS \n";
	    sql = sql + "  	INNER JOIN systypes ON INFORMATION_SCHEMA.COLUMNS.DATA_TYPE = systypes.name \n";
	    sql = sql + "  	INNER JOIN syscolumns ON INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME = syscolumns.name  AND syscolumns.id = OBJECT_ID('" + sqlentity.Name + "') \n";
	    sql = sql + "	left join syscolumns vc on INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME = vc.name AND vc.id = OBJECT_ID('" + sqlentity.Name + "') \n";
	    sql = sql + "  	WHERE INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = '" + sqlentity.Name + "' \n";

	    // if basing data objects on views, get additional fields found in the corresponding view (by naming convention of vw + tablename) -- should be configuration option
	    if (sqlentity.UseViews && sqlentity.View.Length>1) {
		sql = sql + "union \n";
		sql = sql + "	SELECT	INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, \n";
		sql = sql + " 		INFORMATION_SCHEMA.COLUMNS.DATA_TYPE, \n";
		sql = sql + " 		INFORMATION_SCHEMA.COLUMNS.CHARACTER_MAXIMUM_LENGTH, \n";
		sql = sql + " 		INFORMATION_SCHEMA.COLUMNS.NUMERIC_SCALE, \n";
		sql = sql + " 		INFORMATION_SCHEMA.COLUMNS.NUMERIC_PRECISION, \n";
		sql = sql + "  		systypes.length AS COLUMN_LENGTH, \n";
		sql = sql + "  		syscolumns.iscomputed AS COLUMN_COMPUTED, \n";
		sql = sql + " 		'1' IsViewColumn, \n";
		sql = sql + "		ORDINAL_POSITION COLUMN_ID, \n";
		sql = sql + "		ColumnProperty(OBJECT_ID(INFORMATION_SCHEMA.COLUMNS.TABLE_NAME), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsIdentity') AS IsIdentity, \n";
		sql = sql + "		ColumnProperty(OBJECT_ID(INFORMATION_SCHEMA.COLUMNS.TABLE_NAME), INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME, 'IsRowGuidCol') AS IsRowGuidCol, \n";
		sql = sql + " 		INFORMATION_SCHEMA.COLUMNS.IS_NULLABLE \n";
		sql = sql + " 	FROM INFORMATION_SCHEMA.COLUMNS \n";
		sql = sql + " 	INNER JOIN systypes ON INFORMATION_SCHEMA.COLUMNS.DATA_TYPE = systypes.name \n";
		sql = sql + " 	INNER JOIN syscolumns ON INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME = syscolumns.name \n";
		sql = sql + " 	WHERE INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = '" + sqlentity.View + "' AND syscolumns.id = OBJECT_ID('" + sqlentity.View + "') \n";
		sql = sql + " 	and INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME not in (select INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where INFORMATION_SCHEMA.COLUMNS.TABLE_NAME = '" + sqlentity.Name + "') \n";
	    }
	    sql = sql + "order by column_id \n";

	    // Fill the dataset with the information for the current table
	    DataTable table = new DataTable();
	    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
	    adapter.Fill(table);
	    return table;
	}


	private ArrayList DiscoverSqlEntities(SqlConnection connection, IParser vd) {
	    ArrayList list = new ArrayList();

	    // Get a list of the entities in the database
	    DataTable table = new DataTable();
	    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG = '" + connection.Database + "'", connection);
	    adapter.Fill(table);

	    foreach (DataRow row in table.Rows) {
		if (row["TABLE_TYPE"].ToString() == "BASE TABLE" && row["TABLE_NAME"].ToString() != "dtproperties") {
		    SqlEntityElement sqlentity = new SqlEntityElement();
		    sqlentity.Name = row["TABLE_NAME"].ToString();
		    sqlentity.View = "vw" + sqlentity.Name;
		    sqlentity.Columns = DiscoverColumns(sqlentity, connection);
		    sqlentity.Constraints = DiscoverConstraints(sqlentity, connection);
		    sqlentity.Indexes = DiscoverIndexes(sqlentity, connection);
		    list.Add(sqlentity);
		}
	    }	    

	    return list;
	}

	private ArrayList DiscoverConstraints(SqlEntityElement sqlentity, SqlConnection connection) {
	    ArrayList list = new ArrayList();

	    DataTable table = new DataTable();
	    String sql = "SELECT c.*, coalesce((i.status & 16),0) ClusteredIndex, coalesce(rt.TABLE_NAME,'') ForeignEntity, coalesce(cc.CHECK_CLAUSE, '') CheckClause ";
	    sql += "FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS c ";
	    sql += "left join sysindexes i on c.CONSTRAINT_NAME=i.name ";
	    sql += "left join INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS r on c.CONSTRAINT_NAME=r.CONSTRAINT_NAME ";
	    sql += "left join INFORMATION_SCHEMA.TABLE_CONSTRAINTS rt on r.UNIQUE_CONSTRAINT_NAME=rt.CONSTRAINT_NAME ";
	    sql += "left join INFORMATION_SCHEMA.CHECK_CONSTRAINTS cc on c.CONSTRAINT_NAME=cc.CONSTRAINT_NAME ";
	    sql += "where c.table_name='" + sqlentity.Name + "' ";

	    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
	    adapter.Fill(table);
	    
	    foreach (DataRow row in table.Rows) {
		ConstraintElement constraint = new ConstraintElement();
		constraint.Name = row["CONSTRAINT_NAME"].ToString();
		constraint.Type = row["CONSTRAINT_TYPE"].ToString();
		constraint.Clustered = Int32.Parse(row["ClusteredIndex"].ToString())!=0;
//		constraint.ForeignEntity = row["ForeignEntity"].ToString();
		constraint.CheckClause = row["CheckClause"].ToString();

		if (constraint.Type.ToUpper().Equals("PRIMARY KEY") || constraint.Type.ToUpper().Equals("UNIQUE")) {
		    constraint.Columns = DiscoverPrimaryKeyColumns(sqlentity, constraint, connection);
		}
		if (constraint.Type.ToUpper().Equals("FOREIGN KEY")) {
		    constraint.Columns = DiscoverForeignKeyColumns(sqlentity, constraint, connection);
		}

		list.Add(constraint);
	    }	    
	    return list;
	}

	private ArrayList DiscoverPrimaryKeyColumns(SqlEntityElement sqlentity, ConstraintElement constraint, SqlConnection connection) {
	    ArrayList list = new ArrayList();

	    DataTable table = new DataTable();
	    String sql = "SELECT * ";
	    sql += "FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE  ";
	    sql += "where table_name='" + sqlentity.Name + "' and constraint_name='" + constraint.Name + "'  ";
	    sql += "order by ORDINAL_POSITION  ";
	    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
	    adapter.Fill(table);
	    
	    foreach (DataRow row in table.Rows) {
		ColumnElement column = new ColumnElement();
		column.Name = row["COLUMN_NAME"].ToString();
		list.Add(column);
	    }

	    return list;
	}

	private ArrayList DiscoverForeignKeyColumns(SqlEntityElement sqlentity, ConstraintElement constraint, SqlConnection connection) {
	    ArrayList list = new ArrayList();

	    String s = "select 1 ORDINAL_POSITION, c.name COLUMN_NAME, rc.name FOREIGN_COLUMN ";
	    s += "from syscolumns c ";
	    s += "left join sysreferences r on c.id = r.fkeyid and c.colid=r.fkey1 and r.constid=object_id('" + constraint.Name + "') ";
	    s += "left join syscolumns rc on rc.id=r.rkeyid and rc.colid=r.rkey1 ";
	    s += "where c.id = object_id('" + sqlentity.Name + "') and r.constid is not null ";

	    DataTable table = new DataTable();
	    String sql = s;
	    for (int i=2; i<=16; i++) {
		sql += " union ";
		sql += s.Replace("1", i.ToString());
	    }
	    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
	    adapter.Fill(table);

	    foreach (DataRow row in table.Rows) {
		ColumnElement column = new ColumnElement();
		column.Name = row["COLUMN_NAME"].ToString();
		column.ForeignColumn = row["FOREIGN_COLUMN"].ToString();
		list.Add(column);
	    }

	    return list;
	}

	private ArrayList DiscoverIndexes(SqlEntityElement sqlentity, SqlConnection connection) {
	    ArrayList list = new ArrayList();

	    DataTable table = new DataTable();

	    String sql = "declare @objid int \n";
	    sql += "set @objid = object_id('" + sqlentity.Name + "') \n";
	    sql += "select INDEXPROPERTY(@objid, name, 'IsUnique') IsUnique, INDEXPROPERTY(@objid, name, 'IsClustered') IsClustered, name \n";
	    sql += "from sysindexes \n";
	    sql += "where id = @objid and indid > 0 and indid < 255 and (status & 64)=0 and name not in (select name from sysobjects) \n";
	    sql += "order by indid \n";

	    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
	    adapter.Fill(table);

	    foreach (DataRow row in table.Rows) {
		IndexElement index = new IndexElement();
		index.Name = row["NAME"].ToString();
		index.Clustered = !row["IsClustered"].ToString().Equals("0");
		index.Unique = !row["IsUnique"].ToString().Equals("0");
		index.Columns = DiscoverIndexColumns(sqlentity, index, connection);
		list.Add(index);
	    }

	    return list;
	}

	private ArrayList DiscoverIndexColumns(SqlEntityElement sqlentity, IndexElement index, SqlConnection connection) {
	    ArrayList list = new ArrayList();

	    DataTable table = new DataTable();

	    String sql = "DECLARE @indid smallint, \n";
	    sql += "	@indname sysname,  \n";
	    sql += "   	@indkey int,  \n";
	    sql += "	@name varchar(30) \n";
	    sql += " \n";
	    sql += "SET NOCOUNT ON \n";
	    sql += " \n";
	    sql += "set @name='" + sqlentity.Name + "' \n";
	    sql += "set @indname='" + index.Name + "' \n";
	    sql += " \n";
	    sql += "select @indid=indid from sysindexes where id=object_id(@name) and name=@indname \n";
	    sql += "   \n";
	    sql += "	create table #spindtab \n";
	    sql += "	( \n";
	    sql += "		TABLE_NAME			sysname	collate database_default NULL, \n";
	    sql += "		INDEX_NAME			sysname	collate database_default NULL, \n";
	    sql += "		COLUMN_NAME			sysname	collate database_default NULL, \n";
	    sql += "		SORT_DIRECTION			varchar(50) NULL, \n";
	    sql += "		ORDINAL_POSITION		int \n";
	    sql += "	) \n";
	    sql += " \n";
	    sql += "     SET @indkey = 1 \n";
	    sql += "     WHILE @indkey <= 16 and INDEX_COL(@name, @indid, @indkey) is not null \n";
	    sql += "      BEGIN \n";
	    sql += "	insert into #spindtab(table_name, index_name, column_name, ordinal_position, sort_direction) values(@name, @indname, INDEX_COL(@name, @indid, @indkey), @indkey, case when indexkey_property(object_id(@name), @indid, 1, 'isdescending')=1 then 'DESC' else '' end) \n";
	    sql += "        SET @indkey = @indkey + 1 \n";
	    sql += "      END \n";
	    sql += " \n";
	    sql += "select * from #spindtab order by ordinal_position \n";
	    sql += " \n";
	    sql += "drop table #spindtab \n";
	    sql += " \n";
	    sql += "SET NOCOUNT OFF \n";

	    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
	    adapter.Fill(table);

	    foreach (DataRow row in table.Rows) {
		ColumnElement column = new ColumnElement();
		column.Name = row["COLUMN_NAME"].ToString();
		column.SortDirection = row["SORT_DIRECTION"].ToString();
		list.Add(column);
	    }

	    return list;
	}

	private ArrayList DiscoverColumns(SqlEntityElement sqlentity, SqlConnection connection) {
	    ArrayList list = new ArrayList();

	    DataTable columns = GetTableColumns(sqlentity, connection);
	    foreach (DataRow row in columns.Rows) {
		if (row["COLUMN_COMPUTED"].ToString() == "0") {
		    ColumnElement column = new ColumnElement();
		    column.Name = row["COLUMN_NAME"].ToString();

		    column.SqlType.Name = row["DATA_TYPE"].ToString();
		    // if the sql type is defined, default to all values defined in it
		    if (sqltypes.ContainsKey(column.SqlType.Name)) {
			column.SqlType = (SqlTypeElement)((SqlTypeElement)sqltypes[column.SqlType.Name]).Clone();
		    } else {
			WriteToLog("SqlType " + column.SqlType.Name + " was not defined");
		    }

		    column.SqlType.Length = row["CHARACTER_MAXIMUM_LENGTH"].ToString().Length > 0 ? (Int32)row["CHARACTER_MAXIMUM_LENGTH"] : (Int32)(Int16)row["COLUMN_LENGTH"];
		    if (!System.DBNull.Value.Equals(row["NUMERIC_PRECISION"])) column.SqlType.Precision = (Int32)(Byte)row["NUMERIC_PRECISION"];
		    if (!System.DBNull.Value.Equals(row["NUMERIC_SCALE"])) column.SqlType.Scale = (Int32)row["NUMERIC_SCALE"];
		    column.Identity = row["IsIdentity"].ToString() == "1";
		    column.RowGuidCol = row["IsRowGuidCol"].ToString() == "1";
		    column.ViewColumn = row["IsViewColumn"].ToString() == "1";
		    column.Required = row["IS_NULLABLE"].ToString().Trim().ToUpper().Equals("NO");

		    // Check for unicode columns
		    if (column.SqlType.Name.ToLower() == "nchar" || column.SqlType.Name.ToLower() == "nvarchar" || column.SqlType.Name.ToLower() == "ntext") {
			column.SqlType.Length = column.SqlType.Length / 2;
		    }
					
		    // Check for text or ntext columns, which require a different length from what SQL Server reports
		    if (column.SqlType.Name.ToLower() == "text") {
			column.SqlType.Length = 2147483647;
		    } else if (column.SqlType.Name.ToLower() == "ntext") {
			column.SqlType.Length = 1073741823;
		    }

		    // Append the array to the array list
		    list.Add(column);
		}
	    }

	    return list;
	}
    }
}
