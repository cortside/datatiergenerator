using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class SqlEntityElement : SqlEntityData, ICloneable {

	private static readonly String COLUMNS = "columns";
	private static readonly String CONSTRAINTS = "constraints";
	private static readonly String INDEXES = "indexes";
	private static readonly String VIEWS = "views";
	private static readonly String VIEW = "view";
									    
	private String view = String.Empty;
	private ArrayList columns = new ArrayList();
	private ArrayList constraints = new ArrayList();
	private ArrayList indexes = new ArrayList();
	private ArrayList views = new ArrayList();

	public SqlEntityElement() {}

	public SqlEntityElement(DatabaseElement data) {
	    this.Key = data.Key;
	    this.AllowUpdateOfPrimaryKey = data.AllowUpdateOfPrimaryKey;
	    this.AutoDiscoverAttributes = data.AutoDiscoverAttributes;
	    this.AutoDiscoverEntities = data.AutoDiscoverEntities;
	    this.AutoDiscoverProperties = data.AutoDiscoverProperties;
	    this.GenerateDeleteStoredProcScript = data.GenerateDeleteStoredProcScript;
	    this.GenerateInsertStoredProcScript = data.GenerateInsertStoredProcScript;
	    this.AllowInsert = data.AllowInsert || this.GenerateInsertStoredProcScript;
	    this.AllowUpdate = data.AllowUpdate || this.GenerateUpdateStoredProcScript;
	    this.AllowDelete = data.AllowDelete || this.GenerateDeleteStoredProcScript;
	    this.DefaultDirtyRead = data.DefaultDirtyRead;
	    this.UpdateChangedOnly = data.UpdateChangedOnly && this.AllowUpdate;
	    this.GenerateOnlyPrimaryDeleteStoredProc = data.GenerateOnlyPrimaryDeleteStoredProc;
	    this.GenerateProcsForForeignKey = data.GenerateProcsForForeignKey;
	    this.GenerateSelectStoredProcScript = data.GenerateSelectStoredProcScript;
	    this.GenerateSqlTableScripts = data.GenerateSqlTableScripts;
	    this.GenerateSqlViewScripts = data.GenerateSqlViewScripts;
	    this.GenerateUpdateStoredProcScript = data.GenerateUpdateStoredProcScript;
	    this.Password = data.Password;
	    this.ScriptDropStatement = data.ScriptDropStatement;
	    this.Server = data.Server;
	    this.SingleFile = data.SingleFile;
	    this.SqlScriptDirectory = data.SqlScriptDirectory;
	    this.StoredProcNameFormat = data.StoredProcNameFormat;
	    this.User = data.User;
	    this.UseView = data.UseView;
	    this.CommandTimeout = data.CommandTimeout;
	    this.ScriptForIndexedViews = data.ScriptForIndexedViews;
	}

	public String View {
	    get { return this.view; }
	    set { this.view = value; }
	}

	public ArrayList Columns {
	    get { return this.columns; }
	    set { this.columns = value; }
	}

	public ArrayList Constraints {
	    get { return this.constraints; }
	    set { this.constraints = value; }
	}

	public ArrayList Indexes {
	    get { return this.indexes; }
	    set { this.indexes = value; }
	}

	public ArrayList Views {
	    get { return this.views; }
	    set { this.views = value; }
	}

	public static SqlEntityElement FindByName(IList sqlentities, String name) {
	    foreach (SqlEntityElement sqlentity in sqlentities) {
		if (sqlentity.Name.ToLower().Equals(name.ToLower())) {
		    return sqlentity;
		}
	    }
	    return null;
	}

	public ColumnElement FindColumnByName(String name) {
	    foreach (ColumnElement column in columns) {
		if (column.Name.ToLower().Equals(name.ToLower())) {
		    return column;
		}
	    }
	    return null;
	}

	public IndexElement FindIndexByName(String name) {
	    foreach (IndexElement index in indexes) {
		if (index.Name.ToLower().Equals(name.ToLower())) {
		    return index;
		}
	    }
	    return null;
	}


	public ConstraintElement FindConstraintByName(String name) {
	    foreach (ConstraintElement constraint in constraints) {
		if (constraint.Name.ToLower().Equals(name.ToLower())) {
		    return constraint;
		}
	    }
	    return null;
	}

	/// <summary>
	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
	/// list.
	/// </summary>
	/// <param name="node"></param>
	/// <param name="sqlEntityElements"></param>
	public static void ParseFromXml(XmlNode node, IList sqlEntityElements) {

	    if (node != null && sqlEntityElements != null) {

		foreach (XmlNode sqlEntityNode in node.ChildNodes) {
		    if (sqlEntityNode.NodeType.Equals(XmlNodeType.Element)) {
			SqlEntityElement sqlEntityElement = new SqlEntityElement();

			sqlEntityElement.Name = GetAttributeValue(sqlEntityNode, NAME, sqlEntityElement.Name);
			sqlEntityElement.View = GetAttributeValue(sqlEntityNode, VIEW, sqlEntityElement.View);
			sqlEntityElement.SingleFile = Boolean.Parse(GetAttributeValue(sqlEntityNode, SCRIPT_SINGLE_FILE, sqlEntityElement.SingleFile.ToString()));
			sqlEntityElement.Server = GetAttributeValue(sqlEntityNode, SERVER, sqlEntityElement.Server);
			sqlEntityElement.Database = GetAttributeValue(sqlEntityNode, DATABASE, sqlEntityElement.Database);
			sqlEntityElement.User = GetAttributeValue(sqlEntityNode, USER, sqlEntityElement.User);
			sqlEntityElement.Password = GetAttributeValue(sqlEntityNode, PASSWORD, sqlEntityElement.Password);
			sqlEntityElement.SqlScriptDirectory = GetAttributeValue(sqlEntityNode, SCRIPT_DIRECTORY, sqlEntityElement.SqlScriptDirectory);
			sqlEntityElement.StoredProcNameFormat = GetAttributeValue(sqlEntityNode, STORED_PROC_NAME_FORMAT, sqlEntityElement.StoredProcNameFormat);
			sqlEntityElement.GenerateSqlViewScripts = Boolean.Parse(GetAttributeValue(sqlEntityNode, GENERATE_VIEW_SCRIPT, sqlEntityElement.GenerateSqlViewScripts.ToString()));
			sqlEntityElement.GenerateSqlTableScripts = Boolean.Parse(GetAttributeValue(sqlEntityNode, GENERATE_TABLE_SCRIPT, sqlEntityElement.GenerateSqlTableScripts.ToString()));
			sqlEntityElement.GenerateInsertStoredProcScript = Boolean.Parse(GetAttributeValue(sqlEntityNode, GENERATE_INSERT_STORED_PROC_SCRIPT, sqlEntityElement.GenerateInsertStoredProcScript.ToString()));
			sqlEntityElement.GenerateUpdateStoredProcScript = Boolean.Parse(GetAttributeValue(sqlEntityNode, GENERATE_UPDATE_STORED_PROC_SCRIPT, sqlEntityElement.GenerateUpdateStoredProcScript.ToString()));
			sqlEntityElement.GenerateDeleteStoredProcScript = Boolean.Parse(GetAttributeValue(sqlEntityNode, GENERATE_DELETE_STORED_PROC_SCRIPT, sqlEntityElement.GenerateDeleteStoredProcScript.ToString()));
			sqlEntityElement.GenerateSelectStoredProcScript = Boolean.Parse(GetAttributeValue(sqlEntityNode, GENERATE_SELECT_STORED_PROC_SCRIPT, sqlEntityElement.GenerateSelectStoredProcScript.ToString()));
			sqlEntityElement.AllowInsert = Boolean.Parse(GetAttributeValue(sqlEntityNode, ALLOW_INSERT, sqlEntityElement.AllowInsert.ToString()))
							|| sqlEntityElement.GenerateInsertStoredProcScript;
			sqlEntityElement.AllowUpdate = Boolean.Parse(GetAttributeValue(sqlEntityNode, ALLOW_UPDATE, sqlEntityElement.AllowUpdate.ToString()))
							|| sqlEntityElement.GenerateUpdateStoredProcScript;
			sqlEntityElement.AllowDelete = Boolean.Parse(GetAttributeValue(sqlEntityNode, ALLOW_DELETE, sqlEntityElement.AllowDelete.ToString()))
							|| sqlEntityElement.GenerateDeleteStoredProcScript;
			sqlEntityElement.DefaultDirtyRead = Boolean.Parse(GetAttributeValue(sqlEntityNode, DEFAULT_DIRTY_READ, sqlEntityElement.DefaultDirtyRead.ToString()));
			sqlEntityElement.UpdateChangedOnly = Boolean.Parse(GetAttributeValue(sqlEntityNode, UPDATE_CHANGED_ONLY, sqlEntityElement.UpdateChangedOnly.ToString()))
								&& sqlEntityElement.AllowUpdate;
			sqlEntityElement.ScriptDropStatement = Boolean.Parse(GetAttributeValue(sqlEntityNode, SCRIPT_DROP_STATEMENT, sqlEntityElement.ScriptDropStatement.ToString()));
			sqlEntityElement.UseView = Boolean.Parse(GetAttributeValue(sqlEntityNode, USE_VIEW, sqlEntityElement.UseView.ToString()));
			sqlEntityElement.GenerateProcsForForeignKey = Boolean.Parse(GetAttributeValue(sqlEntityNode, GENERATE_PROCS_FOR_FOREIGN_KEYS, sqlEntityElement.GenerateProcsForForeignKey.ToString()));
			sqlEntityElement.GenerateOnlyPrimaryDeleteStoredProc = Boolean.Parse(GetAttributeValue(sqlEntityNode, GENERATE_ONLY_PRIMARY_DELETE_STORED_PROC, sqlEntityElement.GenerateOnlyPrimaryDeleteStoredProc.ToString()));
			sqlEntityElement.AllowUpdateOfPrimaryKey = Boolean.Parse(GetAttributeValue(sqlEntityNode, ALLOW_UPDATE_OF_PRIMARY_KEY, sqlEntityElement.AllowUpdateOfPrimaryKey.ToString()));
			sqlEntityElement.CommandTimeout = Int32.Parse(GetAttributeValue(sqlEntityNode, COMMAND_TIMEOUT, sqlEntityElement.CommandTimeout.ToString()));
			sqlEntityElement.ScriptForIndexedViews = Boolean.Parse(GetAttributeValue(sqlEntityNode, SCRIPT_FOR_INDEXED_VIEWS, sqlEntityElement.ScriptForIndexedViews.ToString()));

			ColumnElement.ParseFromXml(GetChildNodeByName(sqlEntityNode, COLUMNS), sqlEntityElement.Columns);
			ConstraintElement.ParseFromXml(GetChildNodeByName(sqlEntityNode, CONSTRAINTS), sqlEntityElement.Constraints);
			IndexElement.ParseFromXml(GetChildNodeByName(sqlEntityNode, INDEXES), sqlEntityElement.Indexes);
			ViewElement.ParseFromXml(GetChildNodeByName(sqlEntityNode, VIEWS), sqlEntityElement.Views);
		
			sqlEntityElements.Add(sqlEntityElement);
		    }
		}
	    }
	}

	public static ArrayList ParseFromXml(DatabaseElement database, XmlNode databaseNode, Hashtable sqltypes, Hashtable types, ParserValidationDelegate vd) {
	    ArrayList sqlentities = new ArrayList();
	    foreach (XmlNode node in databaseNode.ChildNodes) {
		if (node.Name.Equals("sqlentity")) {
		    SqlEntityElement sqlentity = new SqlEntityElement(database);
		    ParseNodeAttributes(node, sqlentity);
		    sqlentity.View = "vw" + sqlentity.Name;
		    if (node.Attributes["view"] != null) {
			sqlentity.View = node.Attributes["view"].Value;
		    }
		    sqlentity.Columns = ColumnElement.ParseFromXml(node, sqlentity, sqltypes, types, vd);
		    sqlentity.Constraints = ConstraintElement.ParseFromXml(node, sqlentity, sqltypes, types, vd);
		    sqlentity.Indexes = IndexElement.ParseFromXml(node, sqlentity, sqltypes, types, vd);

		    // TODO: this is a hack as many things need to be restructured.  the Elements all need to be parsed first, then 
		    // relationships and links need to be created.  Otherwise, the config file becomes order dependant.
		    DatabaseElement d = new DatabaseElement();
		    d.SqlEntities = sqlentities;
		    sqlentity.Views = ViewElement.ParseFromXml(node, d, sqlentity, sqltypes, types, vd);

		    sqlentities.Add(sqlentity);
		}
	    }
	    return sqlentities;
	}

	public Boolean HasUpdatableColumns() {
	    Boolean has = false;
	    foreach (ColumnElement column in columns) {
		if (!column.Identity && !column.RowGuidCol && !IsPrimaryKeyColumn(column.Name)) {
		    has=true;	
		}
	    }
	    return has;
	}

	public Boolean IsPrimaryKeyColumn(String name) {
	    foreach (ConstraintElement constraint in constraints) {
		if (constraint.Type.ToUpper().Equals("PRIMARY KEY")) {
		    foreach (ColumnElement column in constraint.Columns) {
			if (column.Name.Equals(name)) {
			    return true;
			}
		    }
		}
	    }
	    return false;
	}

	public Boolean IsForeignKeyColumn(String name) {
	    foreach (ConstraintElement constraint in constraints) {
		if (constraint.Type.ToUpper().Equals("FOREIGN KEY")) {
		    foreach (ColumnElement column in constraint.Columns) {
			if (column.Name.Equals(name)) {
			    return true;
			}
		    }
		}
	    }
	    return false;
	}

	/// <summary>
	/// return the identity column if it exists, or the primary key columns if they exist
	/// </summary>
	/// <returns></returns>
	public IList GetPrimaryKeyColumns() {
	    ArrayList list = new ArrayList();
	    ColumnElement id = GetIdentityColumn();
	    if (id != null) {
		list.Add(id);
	    } else {
		foreach (ConstraintElement constraint in constraints) {
		    if (constraint.Type.ToUpper().Equals("PRIMARY KEY")) {
			list.AddRange(constraint.Columns);
		    }
		}
	    }
	    return list;
	}

	/// <summary>
	/// Gets the "primary key" columns, which may include identity columns depending on the argument passed in
	/// </summary>
	/// <param name="includeIdentity"></param>
	/// <returns></returns>
	public IList GetPrimaryKeyColumns(Boolean includeIdentity) {
	    if (includeIdentity) {
		return GetPrimaryKeyColumns();
	    }

	    ArrayList list = new ArrayList();
	    foreach (ConstraintElement constraint in constraints) {
		if (constraint.Type.ToUpper().Equals("PRIMARY KEY")) {
		    list.AddRange(constraint.Columns);
		}
	    }

	    return list;
	}

	public ColumnElement GetIdentityColumn() {
	    foreach (ColumnElement column in columns) {
		if (column.Identity && !column.ViewColumn) {
		    return column;
		}
	    }
	    return null;
	}

	public static void ParseNodeAttributes(XmlNode node, SqlEntityData data) {
	    data.Name = ParseStringAttribute(node, "name", String.Empty);
	    data.Description = node.InnerText;
	    if (node.Attributes["server"] != null) {
		data.Server = node.Attributes["server"].Value;
	    }
	    if (node.Attributes["database"] != null) {
		data.Database = node.Attributes["database"].Value;
	    }
	    if (node.Attributes["user"] != null) {
		data.User = node.Attributes["user"].Value;
	    }
	    if (node.Attributes["password"] != null) {
		data.Password = node.Attributes["password"].Value;
	    }

	    if (node.Attributes["scriptdirectory"] != null) {
		data.SqlScriptDirectory = node.Attributes["scriptdirectory"].Value;
	    }
	    if (node.Attributes["storedprocformatname"] != null) {
		data.StoredProcNameFormat = node.Attributes["storedprocformatname"].Value;
	    }

	    if (node.Attributes["scriptsinglefile"] != null) {
		data.SingleFile = Boolean.Parse(node.Attributes["scriptsinglefile"].Value);
	    }
	    if (node.Attributes["generateviewscript"] != null) {
		data.GenerateSqlViewScripts = Boolean.Parse(node.Attributes["generateviewscript"].Value);
	    }
	    if (node.Attributes["generatetablescript"] != null) {
		data.GenerateSqlTableScripts = Boolean.Parse(node.Attributes["generatetablescript"].Value);
	    }
	    if (node.Attributes["generateinsertstoredprocscript"] != null) {
		data.GenerateInsertStoredProcScript = Boolean.Parse(node.Attributes["generateinsertstoredprocscript"].Value);
	    }
	    if (node.Attributes["generateupdatestoredprocscript"] != null) {
		data.GenerateUpdateStoredProcScript = Boolean.Parse(node.Attributes["generateupdatestoredprocscript"].Value);
	    }
	    if (node.Attributes["generatedeletestoredprocscript"] != null) {
		data.GenerateDeleteStoredProcScript = Boolean.Parse(node.Attributes["generatedeletestoredprocscript"].Value);
	    }
	    if (node.Attributes["generateselectstoredprocscript"] != null) {
		data.GenerateSelectStoredProcScript = Boolean.Parse(node.Attributes["generateselectstoredprocscript"].Value);
	    }
	    if (node.Attributes["allowinsert"] != null) 
	    {
		data.AllowInsert = Boolean.Parse(node.Attributes["allowinsert"].Value)
				    || data.GenerateInsertStoredProcScript;
	    }
	    if (node.Attributes["allowupdate"] != null) 
	    {
		data.AllowUpdate = Boolean.Parse(node.Attributes["allowupdate"].Value)
				    || data.GenerateUpdateStoredProcScript;
	    }
	    if (node.Attributes["allowdelete"] != null) 
	    {
		data.AllowDelete = Boolean.Parse(node.Attributes["allowdelete"].Value)
				    || data.GenerateDeleteStoredProcScript;
	    }
	    if (node.Attributes["defaultdirtyread"] != null) 
	    {
		data.DefaultDirtyRead = Boolean.Parse(node.Attributes["defaultdirtyread"].Value);
	    }
	    if (node.Attributes["updatechangedonly"] != null) 
	    {
		data.UpdateChangedOnly = Boolean.Parse(node.Attributes["updatechangedonly"].Value)
					    && data.AllowUpdate;
	    }
	    if (node.Attributes["scriptdropstatement"] != null) {
		data.ScriptDropStatement = Boolean.Parse(node.Attributes["scriptdropstatement"].Value);
	    }
	    if (node.Attributes["useview"] != null) {
		data.UseView = Boolean.Parse(node.Attributes["useview"].Value);
	    }
	    if (node.Attributes["generateprocsforforeignkeys"] != null) {
		data.GenerateProcsForForeignKey = Boolean.Parse(node.Attributes["generateprocsforforeignkeys"].Value);
	    }
	    if (node.Attributes["generateprocsforforeignkeys"] != null) {
		data.GenerateProcsForForeignKey = Boolean.Parse(node.Attributes["generateprocsforforeignkeys"].Value);
	    }
	    if (node.Attributes["generateonlyprimarydeletestoredproc"] != null) {
		data.GenerateOnlyPrimaryDeleteStoredProc = Boolean.Parse(node.Attributes["generateonlyprimarydeletestoredproc"].Value);
	    }
	    if (node.Attributes["allowupdateofprimarykey"] != null) {
		data.AllowUpdateOfPrimaryKey = Boolean.Parse(node.Attributes["allowupdateofprimarykey"].Value);
	    }
	    if (node.Attributes["commandtimeout"] != null) {
		data.CommandTimeout = Int32.Parse(node.Attributes["commandtimeout"].Value);
	    }
	    if (node.Attributes["scriptforindexedviews"] != null) {
		data.ScriptForIndexedViews = Boolean.Parse(node.Attributes["scriptforindexedviews"].Value);
	    }

	}

    }
}
