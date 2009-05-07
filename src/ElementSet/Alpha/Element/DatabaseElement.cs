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
	    this.AllowInsert = data.AllowInsert || data.GenerateInsertStoredProcScript;
	    this.AllowUpdate = data.AllowUpdate || data.GenerateUpdateStoredProcScript;
	    this.AllowDelete = data.AllowDelete || data.GenerateDeleteStoredProcScript;
	    this.DefaultDirtyRead = data.DefaultDirtyRead;
	    this.UpdateChangedOnly = data.UpdateChangedOnly && this.AllowUpdate;
	    this.Name = data.Name;
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
	    this.Audit = data.Audit;
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

	/// <summary>
	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
	/// list.
	/// </summary>
	/// <param name="node"></param>
	/// <param name="databaseElements"></param>
	public static void ParseFromXml(XmlNode node, IList databaseElements) {

	    if (node != null && databaseElements != null) {

		foreach (XmlNode databaseNode in node.ChildNodes) {
		    if (databaseNode.NodeType.Equals(XmlNodeType.Element)) {
			DatabaseElement databaseElement = new DatabaseElement();

			databaseElement.Name = GetAttributeValue(databaseNode, NAME, databaseElement.Name);
			databaseElement.Audit = Boolean.Parse(GetAttributeValue(databaseNode, AUDIT, databaseElement.Audit.ToString()));
			databaseElement.SingleFile = Boolean.Parse(GetAttributeValue(databaseNode, SCRIPT_SINGLE_FILE, databaseElement.SingleFile.ToString()));
			databaseElement.Server = GetAttributeValue(databaseNode, SERVER, databaseElement.Server);
			databaseElement.Database = GetAttributeValue(databaseNode, DATABASE, databaseElement.Database);
			databaseElement.User = GetAttributeValue(databaseNode, USER, databaseElement.User);
			databaseElement.Password = GetAttributeValue(databaseNode, PASSWORD, databaseElement.Password);
			databaseElement.SqlScriptDirectory = GetAttributeValue(databaseNode, SCRIPT_DIRECTORY, databaseElement.SqlScriptDirectory);
			databaseElement.StoredProcNameFormat = GetAttributeValue(databaseNode, STORED_PROC_NAME_FORMAT, databaseElement.StoredProcNameFormat);
			databaseElement.GenerateSqlViewScripts = Boolean.Parse(GetAttributeValue(databaseNode, GENERATE_VIEW_SCRIPT, databaseElement.GenerateSqlViewScripts.ToString()));
			databaseElement.GenerateSqlTableScripts = Boolean.Parse(GetAttributeValue(databaseNode, GENERATE_TABLE_SCRIPT, databaseElement.GenerateSqlTableScripts.ToString()));
			databaseElement.GenerateInsertStoredProcScript = Boolean.Parse(GetAttributeValue(databaseNode, GENERATE_INSERT_STORED_PROC_SCRIPT, databaseElement.GenerateInsertStoredProcScript.ToString()));
			databaseElement.GenerateUpdateStoredProcScript = Boolean.Parse(GetAttributeValue(databaseNode, GENERATE_UPDATE_STORED_PROC_SCRIPT, databaseElement.GenerateUpdateStoredProcScript.ToString()));
			databaseElement.GenerateDeleteStoredProcScript = Boolean.Parse(GetAttributeValue(databaseNode, GENERATE_DELETE_STORED_PROC_SCRIPT, databaseElement.GenerateDeleteStoredProcScript.ToString()));
			databaseElement.GenerateSelectStoredProcScript = Boolean.Parse(GetAttributeValue(databaseNode, GENERATE_SELECT_STORED_PROC_SCRIPT, databaseElement.GenerateSelectStoredProcScript.ToString()));
			databaseElement.AllowInsert = Boolean.Parse(GetAttributeValue(databaseNode, ALLOW_INSERT, databaseElement.AllowInsert.ToString()))
							|| databaseElement.GenerateInsertStoredProcScript;
			databaseElement.AllowUpdate = Boolean.Parse(GetAttributeValue(databaseNode, ALLOW_UPDATE, databaseElement.AllowUpdate.ToString()))
							|| databaseElement.GenerateUpdateStoredProcScript;
			databaseElement.AllowDelete = Boolean.Parse(GetAttributeValue(databaseNode, ALLOW_DELETE, databaseElement.AllowDelete.ToString()))
							|| databaseElement.GenerateDeleteStoredProcScript;
			databaseElement.DefaultDirtyRead = Boolean.Parse(GetAttributeValue(databaseNode, DEFAULT_DIRTY_READ, databaseElement.DefaultDirtyRead.ToString()));
			databaseElement.UpdateChangedOnly = Boolean.Parse(GetAttributeValue(databaseNode, UPDATE_CHANGED_ONLY, databaseElement.UpdateChangedOnly.ToString()))
							    && databaseElement.AllowUpdate;
			databaseElement.ScriptDropStatement = Boolean.Parse(GetAttributeValue(databaseNode, SCRIPT_DROP_STATEMENT, databaseElement.ScriptDropStatement.ToString()));
			databaseElement.UseView = Boolean.Parse(GetAttributeValue(databaseNode, USE_VIEW, databaseElement.UseView.ToString()));
			databaseElement.GenerateProcsForForeignKey = Boolean.Parse(GetAttributeValue(databaseNode, GENERATE_PROCS_FOR_FOREIGN_KEYS, databaseElement.GenerateProcsForForeignKey.ToString()));
			databaseElement.GenerateOnlyPrimaryDeleteStoredProc = Boolean.Parse(GetAttributeValue(databaseNode, GENERATE_ONLY_PRIMARY_DELETE_STORED_PROC, databaseElement.GenerateOnlyPrimaryDeleteStoredProc.ToString()));
			databaseElement.AllowUpdateOfPrimaryKey = Boolean.Parse(GetAttributeValue(databaseNode, ALLOW_UPDATE_OF_PRIMARY_KEY, databaseElement.AllowUpdateOfPrimaryKey.ToString()));
			databaseElement.CommandTimeout = Int32.Parse(GetAttributeValue(databaseNode, COMMAND_TIMEOUT, databaseElement.CommandTimeout.ToString()));
			databaseElement.ScriptForIndexedViews = Boolean.Parse(GetAttributeValue(databaseNode, SCRIPT_FOR_INDEXED_VIEWS, databaseElement.ScriptForIndexedViews.ToString()));
		
			SqlEntityElement.ParseFromXml(GetChildNodeByName(databaseNode, SQLENTITIES), databaseElement.SqlEntities);

			databaseElements.Add(databaseElement);
		    }
		}
	    }
	}

	public static ArrayList ParseFromXml(Configuration options, XmlDocument doc, Hashtable sqltypes, Hashtable types, ParserValidationDelegate vd) {
	    DatabaseElement defaults = new DatabaseElement();
	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("databases");
	    if (elements.Count < 1)
	    {
		vd(ParserValidationArgs.NewError("No databases tags found.  You must have at least one databases tag."));
	    }
	    else
	    {
		SqlEntityElement.ParseNodeAttributes(elements[0], defaults);
	    }

	    // loop through each 'database' tag in the xml file (ex: file=dtg-databases.xml)
	    ArrayList list = new ArrayList();
	    elements = doc.DocumentElement.GetElementsByTagName("database");
	    foreach (XmlNode node in elements) {
		if (node.NodeType == XmlNodeType.Comment)
		{
		    continue;
		}
		DatabaseElement database = new DatabaseElement(defaults);
		SqlEntityElement.ParseNodeAttributes(node, database);
		if (node.Attributes["key"] != null) {
		    database.Key = node.Attributes["key"].Value;
		}

		database.SqlEntities = SqlEntityElement.ParseFromXml(database, GetSqlEntitiesNode(node), sqltypes, types, vd);
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
