using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator.Core {
    /// <summary>
    /// Summary description for Database.
    /// </summary>
    public class Database : SqlEntityData {
	private ArrayList sqlentities = new ArrayList();

	public Database() {
	}

	public Database(Database data) {
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
	    this.Name = data.Name;
	    this.Password = data.Password;
	    this.ScriptDropStatement = data.ScriptDropStatement;
	    this.Server = data.Server;
	    this.SingleFile = data.SingleFile;
	    this.SqlScriptDirectory = data.SqlScriptDirectory;
	    this.StoredProcNameFormat = data.StoredProcNameFormat;
	    this.User = data.User;
	    this.UseViews = data.UseViews;
	}

	public ArrayList SqlEntities {
	    get { return this.sqlentities; }
	    set { this.sqlentities = value; }
	}

	public static ArrayList ParseFromXml(Configuration options, XmlDocument doc, Hashtable sqltypes, Hashtable types) {
	    Database defaults = new Database();
	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("databases");
	    if (elements.Count < 1)
	    {
		Console.Out.WriteLine("ERROR: No databases tags found.  You must have at least one databases tag.");
	    }
	    else
	    {
		SqlEntity.ParseNodeAttributes(elements[0], defaults);
	    }

	    ArrayList list = new ArrayList();
	    elements = doc.DocumentElement.GetElementsByTagName("database");
	    foreach (XmlNode node in elements) {
		Database database = new Database(defaults);
		SqlEntity.ParseNodeAttributes(node, database);
		if (node.Attributes["key"] != null) {
		    database.Key = node.Attributes["key"].Value;
		}
		database.SqlEntities = SqlEntity.ParseFromXml(database, doc, sqltypes, types);
		list.Add(database);
	    }
	    return list;
	}

	public static ArrayList GetAllSqlEntities(ArrayList databases) {
	    ArrayList list = new ArrayList();
	    foreach(Database database in databases) {
		list.AddRange(database.SqlEntities);
	    }
	    return list;
	}

	public static Database FindByName(ArrayList databases, String name) {
	    foreach (Database database in databases) {
		if (database.Name.Equals(name)) {
		    return database;
		}
	    }
	    return null;
	}

	public SqlEntity FindSqlEntityByName(String name) {
	    foreach (SqlEntity sqlentity in sqlentities) {
		if (sqlentity.Name.Equals(name)) {
		    return sqlentity;
		}
	    }
	    return null;
	}


    }
}
