using System;

using System.Collections;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;

using Spring2.Core.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Element;
using Spring2.DataTierGenerator.Generator;

namespace Spring2.DataTierGenerator.Parser {

    /// <summary>
    /// Summary description for XMLParser.
    /// </summary>
    public class XmlParser : AbstractParser, IParser {

	private XmlDocument doc = null;

	public TaskList Tasks {
	    get { 
		TaskList tasks = new TaskList();

		foreach(TaskElement task in generator.Tasks) {
		    tasks.Add(new Task(this, task));
		}
		return tasks;
	    }
	}

	public Hashtable Tools {
	    get {
		Hashtable tools = new Hashtable();
		foreach(ToolElement tool in generator.Tools) {
		    try {
			Type clazz = System.Type.GetType(tool.Class);
			if (clazz != null) {
			    Object o = System.Activator.CreateInstance(clazz);
			    tools.Add(tool.Name, o);
			} else {
			    WriteToLog("Could not find class '" + tool.Class + "', '" + tool.Name + "' will not be added to toolbox.");
			}
		    } catch (Exception ex) {
			WriteToLog("Error trying to create tool '" + tool.Name + "': " + ex.Message);
		    }
		}
		return tools;
	    }
	}

	public void Parse(String filename) {
	    FileInfo file = new FileInfo(filename);
	    if (!file.Exists) {
		throw new FileNotFoundException("Could not load config file", filename);
	    }
	    isValid = true;
	    ValidateSchema(filename);

	    doc = new XmlDocument();
	    Stream filestream = XIncludeReader.GetStream(filename);
	    doc.Load(filestream);
	    filestream.Close();

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

	    sqltypes = SqlTypeElement.ParseFromXml(doc, vd);
	    types = TypeElement.ParseFromXml(options, doc, vd);

	    // parse generate/task information so that type registration will happen before other types are loaded
	    generator = GeneratorElement.ParseFromXml(options, doc, vd);
	    TaskElement.RegisterTypes(doc, options, generator.Tasks, types);

	    // see if we want to generate collections for all entities
	    XmlNodeList collectionElement = doc.DocumentElement.GetElementsByTagName ("collections");
	    XmlNode collectionNode = collectionElement[0];
	    if (collectionNode.Attributes["generateall"] == null) {
		options.GenerateAllCollections = false;
	    } else {
		options.GenerateAllCollections = Boolean.Parse (collectionNode.Attributes["generateall"].Value.ToString ());
	    }

	    // if the root directory is not specified, make it the directory the config file is loaded from
	    if (options.RootDirectory.Equals(String.Empty)) {
		options.RootDirectory = file.DirectoryName + "\\";
	    }
	    if (!options.RootDirectory.EndsWith("\\")) {
		options.RootDirectory += "\\";
	    }

	    enumtypes = EnumElement.ParseFromXml(options,doc,sqltypes,types, vd);
	    databases = DatabaseElement.ParseFromXml(options, doc, sqltypes, types, vd);
	    entities = EntityElement.ParseFromXml(options, doc, sqltypes, types, DatabaseElement.GetAllSqlEntities(databases), vd);
	    messages = MessageElement.ParseFromXml(options, doc, sqltypes, types, DatabaseElement.GetAllSqlEntities(databases), vd);
	    reportExtractions = ReportExtractionElement.ParseFromXml(options, doc, sqltypes, types, entities, vd);
	    ArrayList collectableClasses = new ArrayList();
	    ArrayList autoGenerateClasses = new ArrayList();
	    collectableClasses.AddRange(entities);
	    collectableClasses.AddRange(reportExtractions);
	    autoGenerateClasses.AddRange(entities);
	    autoGenerateClasses.AddRange(reportExtractions);
	    collections = CollectionElement.ParseFromXml(options,doc,sqltypes,types, vd, collectableClasses, autoGenerateClasses, (ArrayList)entities);

	    // find and assign the foreign entity and EnumElement now that they are parsed
	    foreach(DatabaseElement database in databases) {
		foreach(SqlEntityElement sqlEntity in database.SqlEntities) {
		    foreach(ConstraintElement constraint in sqlEntity.Constraints) {
			if (constraint.ForeignEntity.Name.Length > 0) {
			    SqlEntityElement entity = SqlEntityElement.FindByName(DatabaseElement.GetAllSqlEntities(databases), constraint.ForeignEntity.Name);
			    if (entity != null) {
				constraint.ForeignEntity = (SqlEntityElement)entity.Clone();
			    } else {
				vd(ParserValidationArgs.NewError("ForeignEntity (" + constraint.ForeignEntity.Name + ") specified in constraint " + constraint.Name + " could not be found as an defined entity"));
			    }    
			}
			if (constraint.CheckEnum.Name.Length > 0) {
			    EnumElement entity = EnumElement.FindByName(enumtypes as ArrayList, constraint.CheckEnum.Name);
			    if (entity != null) {
				constraint.CheckEnum = (EnumElement)entity.Clone();
			    } else {
				vd(ParserValidationArgs.NewError("CheckEnum (" + constraint.CheckEnum.Name + ") specified in constraint " + constraint.Name + " could not be found as an defined entity"));
			    }
			}
		    }
		}
	    }

	    foreach(EntityElement entity in entities) {
	    	foreach (PropertyElement property in entity.Fields) {
	    	    if (property.Entity.Name.Length > 0) {
	    	    	EntityElement e = EntityElement.FindEntityByName(entities, property.Entity.Name);
			if (e != null) {
			    property.Entity = e;
			} else {
			    vd(ParserValidationArgs.NewError("Property (" + property.Name + ") specifies an entity " + property.Entity.Name + " that could not be found as an defined entity"));			
			}
	    	    }
	    	}
	    }

	    Validate(vd);
	}

	/// <summary>
	/// validates xml file against embedded schema file
	/// </summary>
	/// <param name="filename"></param>
	private void ValidateSchema(String filename) {
	    try {
		ValidationEventHandler veh = new ValidationEventHandler(SchemaValidationEventHandler);
		System.IO.Stream stream = this.GetType().Assembly.GetManifestResourceStream("config.xsd");
		if (stream == null) {
		    isValid=false;
		    WriteToLog(ParserValidationArgs.NewError("unable to locate config.xsd as assembly resource").ToString());
		} else {
		    XmlSchema schema = XmlSchema.Read(stream, veh);
		    stream.Close();

		    XmlReaderSettings settings = new XmlReaderSettings();
		    settings.ValidationType = ValidationType.Schema;
		    settings.Schemas.Add(schema);
		    settings.ValidationEventHandler += veh;
		    XmlReader reader = XmlReader.Create(filename, settings);
				
		    // wait until the read is over, it's occurring in a different thread - kinda like 
		    // when you're walking to get a cup of coffee and your mind is in Hawaii
		    while (reader.Read());
		}
	    } catch(UnauthorizedAccessException ex) {
		//dont have access permission
		isValid=false;
		WriteToLog(ParserValidationArgs.NewError(ex.Message).ToString());
	    } catch(Exception ex) {
		//and other things that could go wrong
		isValid=false;
		WriteToLog(ParserValidationArgs.NewError(ex.Message).ToString());
	    }
	}

	/// <summary>
	/// handle XML validation errors
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="args"></param>
	internal void SchemaValidationEventHandler(object sender, ValidationEventArgs args) {
	    if (args.Severity.Equals(XmlSeverityType.Error)) {
		isValid = false;
		WriteToLog(ParserValidationArgs.NewError(args.Message).ToString());
	    } else {
		WriteToLog(ParserValidationArgs.NewWarning(args.Message).ToString());
	    }
	}
    }
}
