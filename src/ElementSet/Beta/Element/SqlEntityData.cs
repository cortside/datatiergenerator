using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator.Element {
    /// <summary>
    /// Summary description for SqlEntityData.
    /// </summary>
    public class SqlEntityData : SqlElementSkeleton {

	protected static readonly String KEY = "key";
	protected static readonly String SCRIPT_SINGLE_FILE = "scriptsinglefile";
	protected static readonly String SERVER = "server";
	public static readonly String DATABASE = "database";
	protected static readonly String USER = "user";
	protected static readonly String PASSWORD = "password";
	protected static readonly String SCRIPT_DIRECTORY = "scriptdirectory";
	protected static readonly String STORED_PROC_NAME_FORMAT = "storedprocnameformat";
	protected static readonly String GENERATE_VIEW_SCRIPT = "generateviewscript";
	protected static readonly String GENERATE_TABLE_SCRIPT = "generatetablescript";
	protected static readonly String GENERATE_INSERT_STORED_PROC_SCRIPT = "generateinsertstoredprocscript";
	protected static readonly String GENERATE_UPDATE_STORED_PROC_SCRIPT = "generateupdatestoredprocscript";
	protected static readonly String GENERATE_DELETE_STORED_PROC_SCRIPT = "generatedeletestoredprocscript";
	protected static readonly String GENERATE_SELECT_STORED_PROC_SCRIPT = "generateselectstoredprocscript";
	protected static readonly String SCRIPT_DROP_STATEMENT = "scriptdropstatement";
	protected static readonly String USE_VIEW = "useview";
	protected static readonly String GENERATE_PROCS_FOR_FOREIGN_KEYS = "generateprocsforforeignkeys";
	protected static readonly String GENERATE_ONLY_PRIMARY_DELETE_STORED_PROC = "generateonlyprimarydeletestoredproc";
	protected static readonly String ALLOW_UPDATE_OF_PRIMARY_KEY = "allowupdateofprimarykey";
	protected static readonly String COMMAND_TIMEOUT = "commandtimeout";
	protected static readonly String SCRIPT_FOR_INDEXED_VIEWS = "scriptforindexedviews";
			    
	protected String key = "ConnectionString";
	protected String server = String.Empty;
	protected String database = String.Empty;
	protected String user = String.Empty;
	protected String password = String.Empty;
	protected String sqlScriptDirectory = "Sql";
	protected String storedProcNameFormat = String.Empty;
	protected Boolean singleFile = false;
	protected Boolean generateSqlViewScripts = false;
	protected Boolean generateSqlTableScripts = false;
	protected Boolean useViews = true;
	protected Boolean scriptDropStatement = true;
	protected Boolean generateProcsForForeignKey = false;
	protected Boolean generateInsertStoredProcScript = false;
	protected Boolean generateUpdateStoredProcScript = false;
	protected Boolean generateDeleteStoredProcScript = false;
	protected Boolean generateSelectStoredProcScript = false;
	protected Boolean generateOnlyPrimaryDeleteStoredProc = true;
	protected Boolean allowUpdateOfPrimaryKey = false;
	protected Boolean autoDiscoverEntities = true;
	protected Boolean autoDiscoverProperties = true;
	protected Boolean autoDiscoverAttributes = true;
	protected Int32 commandTimeout = 30;
	protected Boolean scriptForIndexedViews = false;

	public SqlEntityData() {}

	public SqlEntityData(XmlNode databaseNode, SqlEntityData defaults) {
	    name = GetAttributeValue(databaseNode, NAME, defaults.name);
	    singleFile = Boolean.Parse(GetAttributeValue(databaseNode, SCRIPT_SINGLE_FILE, defaults.singleFile.ToString()));
	    server = GetAttributeValue(databaseNode, SERVER, defaults.server);
	    database = GetAttributeValue(databaseNode, DATABASE, defaults.database);
	    user = GetAttributeValue(databaseNode, USER, defaults.user);
	    password = GetAttributeValue(databaseNode, PASSWORD, defaults.password);
	    sqlScriptDirectory = GetAttributeValue(databaseNode, SCRIPT_DIRECTORY, defaults.sqlScriptDirectory);
	    storedProcNameFormat = GetAttributeValue(databaseNode, STORED_PROC_NAME_FORMAT, defaults.storedProcNameFormat);
	    generateSqlViewScripts = Boolean.Parse(GetAttributeValue(databaseNode, GENERATE_VIEW_SCRIPT, defaults.generateSqlViewScripts.ToString()));
	    generateSqlTableScripts = Boolean.Parse(GetAttributeValue(databaseNode, GENERATE_TABLE_SCRIPT, defaults.generateSqlTableScripts.ToString()));
	    generateInsertStoredProcScript = Boolean.Parse(GetAttributeValue(databaseNode, GENERATE_INSERT_STORED_PROC_SCRIPT, defaults.generateInsertStoredProcScript.ToString()));
	    generateUpdateStoredProcScript = Boolean.Parse(GetAttributeValue(databaseNode, GENERATE_UPDATE_STORED_PROC_SCRIPT, defaults.generateUpdateStoredProcScript.ToString()));
	    generateDeleteStoredProcScript = Boolean.Parse(GetAttributeValue(databaseNode, GENERATE_DELETE_STORED_PROC_SCRIPT, defaults.generateDeleteStoredProcScript.ToString()));
	    generateSelectStoredProcScript = Boolean.Parse(GetAttributeValue(databaseNode, GENERATE_SELECT_STORED_PROC_SCRIPT, defaults.generateSelectStoredProcScript.ToString()));
	    scriptDropStatement = Boolean.Parse(GetAttributeValue(databaseNode, SCRIPT_DROP_STATEMENT, defaults.scriptDropStatement.ToString()));
	    useViews = Boolean.Parse(GetAttributeValue(databaseNode, USE_VIEW, defaults.useViews.ToString()));
	    generateProcsForForeignKey = Boolean.Parse(GetAttributeValue(databaseNode, GENERATE_PROCS_FOR_FOREIGN_KEYS, defaults.generateProcsForForeignKey.ToString()));
	    generateOnlyPrimaryDeleteStoredProc = Boolean.Parse(GetAttributeValue(databaseNode, GENERATE_ONLY_PRIMARY_DELETE_STORED_PROC, defaults.generateOnlyPrimaryDeleteStoredProc.ToString()));
	    allowUpdateOfPrimaryKey = Boolean.Parse(GetAttributeValue(databaseNode, ALLOW_UPDATE_OF_PRIMARY_KEY, defaults.allowUpdateOfPrimaryKey.ToString()));
	    commandTimeout = Int32.Parse(GetAttributeValue(databaseNode, COMMAND_TIMEOUT, defaults.commandTimeout.ToString()));
	    scriptForIndexedViews = Boolean.Parse(GetAttributeValue(databaseNode, SCRIPT_FOR_INDEXED_VIEWS, defaults.scriptForIndexedViews.ToString()));
	}

	public String Key {
	    get { return this.key; }
	    set { this.key = value; }
	}

	public String Server {
	    get { return this.server; }
	    set { this.server = value; }
	}

	public String Database {
	    get { return this.database; }
	    set { this.database = value; }
	}

	public String User {
	    get { return this.user; }
	    set { this.user = value; }
	}

	public String Password {
	    get { return this.password; }
	    set { this.password = value; }
	}

	public String SqlScriptDirectory {
	    get { return this.sqlScriptDirectory; }
	    set { this.sqlScriptDirectory = value; }
	}

	public String StoredProcNameFormat {
	    get { return this.storedProcNameFormat; }
	    set { this.storedProcNameFormat = value; }
	}

	public Boolean SingleFile {
	    get { return this.singleFile; }
	    set { this.singleFile = value; }
	}

	public Boolean GenerateSqlViewScripts {
	    get { return this.generateSqlViewScripts; }
	    set { this.generateSqlViewScripts = value; }
	}

	public Boolean GenerateSqlTableScripts {
	    get { return this.generateSqlTableScripts; }
	    set { this.generateSqlTableScripts = value; }
	}

	public Boolean UseViews {
	    get { return this.useViews; }
	    set { this.useViews = value; }
	}

	public Boolean ScriptDropStatement {
	    get { return this.scriptDropStatement; }
	    set { this.scriptDropStatement = value; }
	}

	public Boolean GenerateProcsForForeignKey {
	    get { return this.generateProcsForForeignKey; }
	    set { this.generateProcsForForeignKey = value; }
	}

	public Boolean GenerateInsertStoredProcScript {
	    get { return this.generateInsertStoredProcScript; }
	    set { this.generateInsertStoredProcScript = value; }
	}
	public Boolean GenerateUpdateStoredProcScript {
	    get { return this.generateUpdateStoredProcScript; }
	    set { this.generateUpdateStoredProcScript = value; }
	}
	public Boolean GenerateDeleteStoredProcScript {
	    get { return this.generateDeleteStoredProcScript; }
	    set { this.generateDeleteStoredProcScript = value; }
	}
	public Boolean GenerateSelectStoredProcScript {
	    get { return this.generateSelectStoredProcScript; }
	    set { this.generateSelectStoredProcScript = value; }
	}

	public Boolean GenerateOnlyPrimaryDeleteStoredProc {
	    get { return this.generateOnlyPrimaryDeleteStoredProc; }
	    set { this.generateOnlyPrimaryDeleteStoredProc = value; }
	}

	public Boolean AllowUpdateOfPrimaryKey {
	    get { return this.allowUpdateOfPrimaryKey; }
	    set { this.allowUpdateOfPrimaryKey = value; }
	}

	public Boolean AutoDiscoverEntities {
	    get { return this.autoDiscoverEntities; }
	    set { this.autoDiscoverEntities = value; }
	}

	public Boolean AutoDiscoverProperties {
	    get { return this.autoDiscoverProperties; }
	    set { this.autoDiscoverProperties = value; }
	}

	public Boolean AutoDiscoverAttributes {
	    get { return this.autoDiscoverAttributes; }
	    set { this.autoDiscoverAttributes = value; }
	}

	public Int32 CommandTimeout {
	    get { return this.commandTimeout; }
	    set { this.commandTimeout = value; }
	}

	public Boolean ScriptForIndexedViews {
	    get { return this.scriptForIndexedViews; }
	    set { this.scriptForIndexedViews = value; }
	}

	public String ConnectionString {
	    get { 			
		StringBuilder objStringBuilder = new StringBuilder(255);
		objStringBuilder.Append("Data Source = " + server + ";");
		objStringBuilder.Append("Initial Catalog = " + database + ";");
		objStringBuilder.Append("User ID = " + user + ";");
		objStringBuilder.Append("Password = " + password + ";");
		return objStringBuilder.ToString();
	    }
	}

	public override void Validate(RootElement root) {}
    }
}
