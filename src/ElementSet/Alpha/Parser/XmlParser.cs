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

	    // if the root directory is not specified, make it the directory the config file is loaded from
	    if (options.RootDirectory.Equals(String.Empty)) {
		options.RootDirectory = file.DirectoryName + "\\";
	    }
	    if (!options.RootDirectory.EndsWith("\\")) {
		options.RootDirectory += "\\";
	    }

	    generator = GeneratorElement.ParseFromXml(options, doc, vd);
	    sqltypes = SqlTypeElement.ParseFromXml(doc, vd);
	    types = TypeElement.ParseFromXml(options, doc, vd);

	    enumtypes = EnumElement.ParseFromXml(options,doc,sqltypes,types, vd);
	    databases = DatabaseElement.ParseFromXml(options, doc, sqltypes, types, vd);
	    entities = EntityElement.ParseFromXml(options, doc, sqltypes, types, DatabaseElement.GetAllSqlEntities(databases), vd);
	    collections = CollectionElement.ParseFromXml(options,doc,sqltypes,types, vd, (ArrayList)entities);

	    Validate(vd);
	}

	/// <summary>
	/// validates xml file against embedded schema file
	/// </summary>
	/// <param name="filename"></param>
	private void ValidateSchema(String filename) {
	    try {
		ValidationEventHandler veh = new ValidationEventHandler(SchemaValidationEventHandler);
		System.IO.Stream s = this.GetType().Assembly.GetManifestResourceStream("config.xsd");
		if (s == null) {
		    isValid=false;
		    WriteToLog(ParserValidationArgs.NewError("unable to locate config.xsd as assembly resource").ToString());
		} else {
		    XmlSchema xsd = XmlSchema.Read(s, veh);
		    s.Close();

		    XmlTextReader xml = XIncludeReader.GetXmlTextReader(filename);
		    XmlValidatingReader vr = new XmlValidatingReader(xml);
		    vr.Schemas.Add(xsd);
		    vr.ValidationType = ValidationType.Schema;

		    // and validation errors events go to...
		    vr.ValidationEventHandler += veh;
				
		    // wait until the read is over, its occuring in a different thread - kinda like 
		    // when your walking to get a cup of coffee and your mind is in Hawaii
		    while (vr.Read());
		    vr.Close();
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
