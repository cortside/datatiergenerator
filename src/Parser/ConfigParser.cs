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
    public class ConfigParser : ParserSkeleton, IParser {

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

	    // if the root directory is not specified, make it the directory the config file is loaded from
	    if (options.RootDirectory.Equals(String.Empty)) {
		options.RootDirectory = file.DirectoryName + "\\";
	    }
	    if (!options.RootDirectory.EndsWith("\\")) {
		options.RootDirectory += "\\";
	    }

	    parser = Element.Parser.ParseFromXml(options, doc, vd);
	    generator = Element.Generator.ParseFromXml(options, doc, vd);
	    sqltypes = SqlType.ParseFromXml(doc, vd);
	    types = Spring2.DataTierGenerator.Element.Type.ParseFromXml(options, doc, vd);

	    if (parser.Class.Equals(String.Empty)) {
		enumtypes = EnumType.ParseFromXml(options,doc,sqltypes,types, vd);
		collections = Collection.ParseFromXml(options,doc,sqltypes,types, vd);
		databases = Database.ParseFromXml(options, doc, sqltypes, types, vd);
		entities = Entity.ParseFromXml(options, doc, sqltypes, types, Database.GetAllSqlEntities(databases), vd);
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
		errors.Add(ParserValidationArgs.NewError(ex.Message));
	    }
	    catch(Exception ex) {
		//and other things that could go wrong
		isValid=false;
		errors.Add(ParserValidationArgs.NewError(ex.Message));
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
		errors.Add(ParserValidationArgs.NewError(args.Message));
	    } else {
		errors.Add(ParserValidationArgs.NewWarning(args.Message));
	    }
	}

    }
}