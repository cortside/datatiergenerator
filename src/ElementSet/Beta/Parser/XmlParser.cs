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
    public class XmlParser : ParserSkeleton {

	public XmlParser(ParserElement parser, ConfigurationElement options, XmlDocument doc) : base(parser, options, doc) {

//	    generator = GeneratorElement.ParseFromXml(options, doc, this);
	    enumtypes = EnumElement.ParseFromXml(options,doc,sqltypes,types, this);
	    collections = CollectionElement.ParseFromXml(options,doc,sqltypes,types, this);
	    databases = DatabaseElement.ParseFromXml(options, doc, sqltypes, types, this);
	    entities = EntityElement.ParseFromXml(options, doc, sqltypes, types, DatabaseElement.GetAllSqlEntities(databases), this);

	    Validate();
	}

//	public XmlParser(ParserElement parser, ConfigurationElement options, XmlDocument doc, Hashtable sqltypes, Hashtable types,  vd) {
//	    this.options = options;
//	    this.doc = doc;
//	    this.sqltypes = sqltypes;
//	    this.types = types;
//	    enumtypes = EnumElement.ParseFromXml(options,doc,sqltypes,types, vd);
//	    collections = CollectionElement.ParseFromXml(options,doc,sqltypes,types, vd);
//	    databases = DatabaseElement.ParseFromXml(options, doc, sqltypes, types, vd);
//	    entities = EntityElement.ParseFromXml(options, doc, sqltypes, types, DatabaseElement.GetAllSqlEntities(databases), vd);
//
//	    Validate(vd);
//	}


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
	    } catch(UnauthorizedAccessException ex) {
		//dont have access permission
		IsValid = false;
		WriteToLog(ParserValidationMessage.NewError(ex.Message).ToString());
	    } catch(Exception ex) {
		//and other things that could go wrong
		IsValid = false;
		WriteToLog(ParserValidationMessage.NewError(ex.Message).ToString());
	    }
	}

	/// <summary>
	/// handle XML validation errors
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="args"></param>
	internal void SchemaValidationEventHandler(object sender, ValidationEventArgs args) {
	    if (args.Severity.Equals(XmlSeverityType.Error)) {
		IsValid = false;
		WriteToLog(ParserValidationMessage.NewError(args.Message).ToString());
	    } else {
		WriteToLog(ParserValidationMessage.NewWarning(args.Message).ToString());
	    }
	}
    }
}
