using System;

using System.Collections;
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
    public class ConfigParser : ParserSkeleton {

	private XmlDocument doc = null;

	public ConfigParser(String filename) {
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

	    // If the root directory is not specified, make it the directory the config file is loaded from.
	    if (options.RootDirectory.Equals(String.Empty)) {
		options.RootDirectory = file.DirectoryName + "\\";
	    }
	    if (!options.RootDirectory.EndsWith("\\")) {
		options.RootDirectory += "\\";
	    }

	    parser = ParserElement.ParseFromXml(options, doc, vd);
	    generator = GeneratorElement.ParseFromXml(options, doc, vd);
	    sqltypes = SqlTypeElement.ParseFromXml(doc, vd);
	    types = TypeElement.ParseFromXml(options, doc, vd);

	    if (parser.Class.Equals(String.Empty)) {
		enumtypes = EnumElement.ParseFromXml(options,doc,sqltypes,types, vd);
		databases = DatabaseElement.ParseFromXml(options, doc, sqltypes, types, vd);
		entities = EntityElement.ParseFromXml(options, doc, sqltypes, types, DatabaseElement.GetAllSqlEntities(databases), vd);
		collections = CollectionElement.ParseFromXml(options,doc,sqltypes,types,vd, (ArrayList)entities);
	    } else {
		Object o = null;
		try {
		    System.Type clazz = System.Type.GetType(parser.Class, true);
		    Object[] args = { parser, options, doc, sqltypes, types, vd };
		    o = System.Activator.CreateInstance(clazz, args);
		} catch (Exception ex) {
		    Console.Out.WriteLine("ERROR: could not create class " + parser.Class + ".\n" + ex.ToString());
		}

		if (o is IParser) {
		    IParser p = (IParser) o;
		    enumtypes = (ArrayList)p.Enums;
		    collections = (ArrayList)p.Collections;
		    databases = (ArrayList)p.Databases;
		    entities = (ArrayList)p.Entities;
		} else  {
		    Console.Out.WriteLine("ERROR: class " + parser.Class + " does not support IParser interface.\n");
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
		ResourceManager rm = new ResourceManager();
		ValidationEventHandler veh = new ValidationEventHandler(SchemaValidationEventHandler);
		XmlSchema xsd = XmlSchema.Read(rm.ConfigSchema, veh);

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
	    catch(UnauthorizedAccessException ex) {
		//dont have access permission
		isValid=false;
		WriteToLog(ParserValidationArgs.NewError(ex.Message).ToString());
	    }
	    catch(Exception ex) {
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
