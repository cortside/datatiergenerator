using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator.Core {

    public class SqlEntity : SqlEntityData, ICloneable {

	private String view = String.Empty;
	private ArrayList columns = new ArrayList();
	private ArrayList constraints = new ArrayList();
	private ArrayList indexes = new ArrayList();

	public SqlEntity() {
	}

	public SqlEntity(Database data) {
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

	public static SqlEntity FindByName(ArrayList sqlentities, String name) {
	    foreach (SqlEntity sqlentity in sqlentities) {
		if (sqlentity.Name.Equals(name)) {
		    return sqlentity;
		}
	    }
	    return null;
	}

	public Column FindColumnByName(String name) {
	    foreach (Column column in columns) {
		if (column.Name.Equals(name)) {
		    return column;
		}
	    }
	    return null;
	}

	public static ArrayList ParseFromXml(Database database, XmlDocument doc, Hashtable sqltypes, Hashtable types) {
	    ArrayList sqlentities = new ArrayList();
	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("sqlentity");
	    foreach (XmlNode node in elements) {
		SqlEntity sqlentity = new SqlEntity(database);
		ParseNodeAttributes(node, sqlentity);
		sqlentity.View = "vw" + sqlentity.Name;
		if (node.Attributes["view"] != null) {
		    sqlentity.View = node.Attributes["view"].Value;
		}
		sqlentity.Columns = Column.ParseFromXml(node, sqlentity, sqltypes, types);
		sqlentity.Constraints = Constraint.ParseFromXml(node, sqlentity, sqltypes, types);
		sqlentity.Indexes = Index.ParseFromXml(node, sqlentity, sqltypes, types);
		sqlentities.Add(sqlentity);
	    }
	    return sqlentities;
	}

	public Boolean HasUpdatableColumns() {
	    Boolean has = false;
	    foreach (Column column in columns) {
		if (!column.Identity && !column.RowGuidCol && !IsPrimaryKeyColumn(column.Name)) {
		    has=true;	
		}
	    }
	    return has;
	}

	public Boolean IsPrimaryKeyColumn(String name) {
	    foreach (Constraint constraint in constraints) {
		if (constraint.Type.ToUpper().Equals("PRIMARY KEY")) {
		    foreach (Column column in constraint.Columns) {
			if (column.Name.Equals(name)) {
			    return true;
			}
		    }
		}
	    }
	    return false;
	}

	public Boolean IsForeignKeyColumn(String name) {
	    foreach (Constraint constraint in constraints) {
		if (constraint.Type.ToUpper().Equals("FOREIGN KEY")) {
		    foreach (Column column in constraint.Columns) {
			if (column.Name.Equals(name)) {
			    return true;
			}
		    }
		}
	    }
	    return false;
	}

	public IList GetPrimaryKeyColumns() {
	    ArrayList list = new ArrayList();
	    Column id = GetIdentityColumn();
	    if (id != null) {
		list.Add(id);
	    } else {
		foreach (Constraint constraint in constraints) {
		    if (constraint.Type.ToUpper().Equals("PRIMARY KEY")) {
			list.AddRange(constraint.Columns);
		    }
		}
	    }
	    return list;
	}

	public Column GetIdentityColumn() {
	    foreach (Column column in columns) {
		if (column.Identity) {
		    return column;
		}
	    }
	    return null;
	}

	//	public Field FindFieldBySqlName(String name) {
	//	    foreach (Field field in fields) {
	//		if (field.SqlName == name) {
	//		    return field;
	//		}
	//	    }
	//	    return null;
	//	}
	//
	//	public Field FindFieldByName(String name) {
	//	    foreach (Field field in fields) {
	//		if (field.Name == name) {
	//		    return field;
	//		}
	//	    }
	//	    return null;
	//	}

	public Object Clone() {
	    return MemberwiseClone();
	}

	public static void ParseNodeAttributes(XmlNode node, SqlEntityData data) {
	    if (node.Attributes["name"] != null) {
		data.Name = node.Attributes["name"].Value;
	    }
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
	}

    }
}
