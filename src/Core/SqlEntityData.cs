using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator.Core {
    /// <summary>
    /// Summary description for SqlEntityData.
    /// </summary>
    public class SqlEntityData : Spring2.Core.DataObject.DataObject {

	protected String name = String.Empty;
	protected String description = String.Empty;
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

	public String Key {
	    get { return this.key; }
	    set { this.key = value; }
	}

	public String Name {
	    get { return this.name; }
	    set { this.name = value; }
	}

	public String Description {
	    get { return this.description; }
	    set { this.description = value; }
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
    }
}
