using System;

using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;

using Spring2.Core.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Element;

namespace Spring2.DataTierGenerator.Parser {
    /// <summary>
    /// Summary description for XMLParser.
    /// </summary>
    public class XmlParser : ParserSkeleton, IParser {

	private XmlDocument doc = null;
	private SqlConnection connection = null;

	public XmlParser(String filename) {
	    FileInfo file = new FileInfo(filename);
	    if (!file.Exists) {
		throw new FileNotFoundException("Could not load config file", filename);
	    }
	    isValid = true;
	    ValidateSchema(filename);

	    doc = new XmlDocument();
	    doc.Load(XIncludeReader.GetStream(filename));

	    if (doc==null) {
		throw new InvalidOperationException("Could not parse xml document: " + filename);
	    } 

	    // event handler for all of the ParseFromXml methods
	    ParserValidationDelegate vd = new ParserValidationDelegate(ParserValidationEventHandler);

	    XmlNode root = doc.DocumentElement["config"];
	    if (root != null) {
		this.options = new Configuration(root, vd);
	    } else {
		this.options = new Configuration();
	    }

	    // if the root directory is not specified, make it the directory the config file is loaded from
	    if (options.RootDirectory.Equals(String.Empty)) {
		options.RootDirectory = file.DirectoryName + "\\";
	    }
	    if (!options.RootDirectory.EndsWith("\\")) {
		options.RootDirectory += "\\";
	    }

	    sqltypes = SqlType.ParseFromXml(doc, vd);
	    types = Spring2.DataTierGenerator.Element.Type.ParseFromXml(options, doc, vd);
	    databases = GetDatabases(doc, vd);
	    entities = GetEntities(doc, connection, Database.GetAllSqlEntities(databases), vd);
	    enumtypes = EnumType.ParseFromXml(options,doc,sqltypes,types, vd);
	    collections = Collection.ParseFromXml(options,doc,sqltypes,types, vd);

	    Validate();
	}

	private void Validate() {
	    //TODO: walk through collection to make sure that cross relations are correct.

//	    if (entity.SqlEntity.Name.Length>0 && !entity.SqlEntity.HasUpdatableColumns()) {
//		Console.Out.WriteLine("WARNING: entity " + entity.Name + " does not have any editable fields and does not have x specified.  No update stored procedures or update DAO methods will be generated.");
//	    }
	}


	private void ValidateSchema(String filename) {
	    try {
		XmlTextReader xml = XIncludeReader.GetXmlTextReader(filename);
		XmlValidatingReader vr = new XmlValidatingReader(xml);
		// TODO: this assumes that the xsd is moved to the build directory
		vr.Schemas.Add(null, AppDomain.CurrentDomain.BaseDirectory + "\\config.xsd");
		vr.ValidationType = ValidationType.Schema;

		//and validation errors events go to...
		vr.ValidationEventHandler += new ValidationEventHandler(SchemaValidationEventHandler);
				
		//wait until the read is over, its occuring in a different thread - kinda like when your walking to get a cup of coffee and your mind is in Hawaii
		while (vr.Read());
		vr.Close();
	    }
	    catch(UnauthorizedAccessException ex) {
		//dont have access permission
		isValid=false;
		errors.Add(ParserValidationArgs.NewError(ex.Message));
	    }
	    catch(Exception ex) {
		//and other things that could go wrong
		isValid=false;
		errors.Add(ParserValidationArgs.NewError(ex.Message));
	    }
	}

	//handle XML validation errors
	internal void SchemaValidationEventHandler(object sender, ValidationEventArgs args) {
	    if (args.Severity.Equals(XmlSeverityType.Error)) {
		isValid = false;
		errors.Add(ParserValidationArgs.NewError(args.Message));
	    } else {
		errors.Add(ParserValidationArgs.NewWarning(args.Message));
	    }
	}

	internal void ParserValidationEventHandler(ParserValidationArgs args) {
	    if (args.Severity.Equals(ParserValidationSeverity.ERROR)) {
		isValid = false;
	    }
	    errors.Add(args);
	}

	private ArrayList GetEntities(XmlDocument doc, SqlConnection connection, ArrayList sqlentities, ParserValidationDelegate vd) {
	    ArrayList entities = new ArrayList();

	    if (doc != null) {
		entities.AddRange(Entity.ParseFromXml(options, doc, sqltypes, types, sqlentities, vd));

//		if (options.AutoDiscoverProperties) {
//		    foreach (Entity entity in entities) {
//			entity.Fields = GetFields(entity, connection, doc, sqltypes, types);
//		    }
//		}
	    }

//	    if (options.AutoDiscoverEntities) {
//		// Get a list of the entities in the database
//		DataTable objDataTable = new DataTable();
//		SqlDataAdapter objDataAdapter = new SqlDataAdapter("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG = '" + connection.Database + "'", connection);
//		objDataAdapter.Fill(objDataTable);
//		foreach (DataRow row in objDataTable.Rows) {
//		    if (row["TABLE_TYPE"].ToString() == "BASE TABLE" && row["TABLE_NAME"].ToString() != "dtproperties") {
//			if (Entity.FindEntityBySqlEntity(entities, row["TABLE_NAME"].ToString()) == null) {
//			    Entity entity = new Entity();
//			    entity.Name = row["TABLE_NAME"].ToString();
//			    entity.SqlEntity.Name = row["TABLE_NAME"].ToString();
//			    if (options.UseViews) {
//				entity.SqlEntity.View = "vw" + entity.SqlEntity.Name;
//			    }
//			    entity.Fields = GetFields(entity, connection, doc, sqltypes, types);
//			    entities.Add(entity);
//			}
//		    }
//		}	    
//	    }

	    return entities;
	}


	private ArrayList GetDatabases(XmlDocument doc, ParserValidationDelegate vd) {
	    ArrayList list = new ArrayList();

	    if (doc != null) {
		list.AddRange(Database.ParseFromXml(options, doc, sqltypes, types, vd));
	    }
	    return list;
	}


    }
}
