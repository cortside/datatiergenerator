using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class SqlEntityElement : SqlEntityData, ICloneable {

	public static readonly String SQLENTITY = "sqlentity";
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
	    this.UseViews = data.UseViews;
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

	public SqlEntityElement(XmlNode sqlEntityNode, SqlEntityData defaults) : base(sqlEntityNode, defaults) {

	    if (sqlEntityNode != null && SQLENTITY.Equals(sqlEntityNode.Name)) {

		view = GetAttributeValue(sqlEntityNode, VIEW, view);
		
		foreach (XmlNode node in GetChildNodes(sqlEntityNode, COLUMNS, ColumnElement.COLUMN)) {
		    columns.Add(new ColumnElement(node, this));
		}
		foreach (XmlNode node in GetChildNodes(sqlEntityNode, CONSTRAINTS, ConstraintElement.CONSTRAINT)) {
		    constraints.Add(ConstraintElement.NewInstance(node, this));
		}
		foreach (XmlNode node in GetChildNodes(sqlEntityNode, INDEXES, IndexElement.INDEX)) {
		    indexes.Add(new IndexElement(node, this));
		}
		foreach (XmlNode node in GetChildNodes(sqlEntityNode, VIEWS, ViewElement.VIEW)) {
		    views.Add(new ViewElement(node));
		}
	    } else {
		throw new ArgumentException("The XmlNode argument is not a sql entity node.");
	    }
	}

	public override void Validate(IParser parser) {
	    // View name defaults to vw + sqlentity name.
	    this.View = this.View.Equals(String.Empty) ? "vw" + this.Name : this.View;

	    foreach (ColumnElement column in this.Columns) {
		column.Validate(parser);
	    }
	    foreach (ConstraintElement constraint in this.Constraints) {
		constraint.Validate(parser);
	    }
	    foreach (IndexElement index in this.Indexes) {
		index.Validate(parser);
	    }
	    foreach (ViewElement view in this.Views) {
		view.Validate(parser);
	    }
	}	

	public static ArrayList ParseFromXml(DatabaseElement database, XmlNode databaseNode, Hashtable sqltypes, Hashtable types, IParser vd) {
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
	    if (node.Attributes["scriptdropstatement"] != null) {
		data.ScriptDropStatement = Boolean.Parse(node.Attributes["scriptdropstatement"].Value);
	    }
	    if (node.Attributes["useview"] != null) {
		data.UseViews = Boolean.Parse(node.Attributes["useview"].Value);
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
