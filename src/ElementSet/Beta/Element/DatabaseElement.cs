using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {
    /// <summary>
    /// Summary description for Database.
    /// </summary>
    public class DatabaseElement : SqlEntityData {

	private static readonly String SQLENTITIES = "sqlentities";

	private ArrayList sqlentities = new ArrayList();

	public DatabaseElement() {}

	public DatabaseElement(XmlNode databaseNode, SqlEntityData defaults) : base(databaseNode, defaults) {

	    if (databaseNode != null && DATABASE.Equals(databaseNode.Name)) {
		foreach (XmlNode node in GetChildNodes(databaseNode, SQLENTITIES, SqlEntityElement.SQLENTITY)) {
		    sqlentities.Add(new SqlEntityElement(node, this));
		}
	    } else {
		throw new ArgumentException("The XmlNode argument is not a database node.");
	    }
	}

	public DatabaseElement(DatabaseElement data) {
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
	    this.CommandTimeout = data.CommandTimeout;
	    this.ScriptForIndexedViews = data.ScriptForIndexedViews;
	}

	public ArrayList SqlEntities {
	    get { return this.sqlentities; }
	    set { this.sqlentities = value; }
	}

	/// <summary>
	/// Gets the sqlentites node given a database node.
	/// </summary>
	/// <param name="node">the xml node for the database entry.</param>
	/// <returns>the xml node for the sql entities entry.</returns>
	/// <exception cref="ArgumentException">if a sql entities node is
	/// not found.</exception>
	private static XmlNode GetSqlEntitiesNode(XmlNode node) {
	    foreach (XmlNode childNode in node.ChildNodes) {
		if (childNode.Name.Equals("sqlentities")) {
		    return childNode;
		}
	    }

	    throw new ArgumentException("The given node does not contain a sql entities tag.");
	}

	public override void Validate(IParser parser) {
	    foreach (SqlEntityElement sqlEntity in this.SqlEntities) {
		sqlEntity.Validate(parser);
	    }
	}

	public static ArrayList ParseFromXml(ConfigurationElement options, XmlDocument doc, Hashtable sqltypes, Hashtable types, IParser parser) {
	    DatabaseElement defaults = new DatabaseElement();
	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("databases");
	    if (elements.Count < 1)
	    {
		parser.AddValidationMessage(ParserValidationMessage.NewError("No databases tags found.  You must have at least one databases tag."));
	    }
	    else
	    {
		SqlEntityElement.ParseNodeAttributes(elements[0], defaults);
	    }

	    ArrayList list = new ArrayList();
	    elements = doc.DocumentElement.GetElementsByTagName("database");
	    foreach (XmlNode node in elements) {
		DatabaseElement database = new DatabaseElement(defaults);
		SqlEntityElement.ParseNodeAttributes(node, database);
		if (node.Attributes["key"] != null) {
		    database.Key = node.Attributes["key"].Value;
		}
		database.SqlEntities = SqlEntityElement.ParseFromXml(database, GetSqlEntitiesNode(node), sqltypes, types, parser);
		list.Add(database);
	    }
	    return list;
	}

	public static ArrayList GetAllSqlEntities(IList databases) {
	    ArrayList list = new ArrayList();
	    foreach(DatabaseElement database in databases) {
		list.AddRange(database.SqlEntities);
	    }
	    return list;
	}

	public static DatabaseElement FindByName(IList databases, String name) {
	    foreach (DatabaseElement database in databases) {
		if (database.Name.Equals(name)) {
		    return database;
		}
	    }
	    return null;
	}

	public SqlEntityElement FindSqlEntityByName(String name) {
	    foreach (SqlEntityElement sqlentity in sqlentities) {
		if (sqlentity.Name.ToLower().Equals(name.ToLower())) {
		    return sqlentity;
		}
	    }
	    return null;
	}
    }
}
