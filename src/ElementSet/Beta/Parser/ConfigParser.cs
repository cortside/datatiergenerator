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

//	private Boolean isValid = false;
	private IList errors = new ArrayList();

	public IParser Parse(String filename) {

	    // See if the given file exists and validate the schema if it does.
	    FileInfo file = new FileInfo(filename);
	    if (!file.Exists) {
		throw new FileNotFoundException("Could not load config file", filename);
	    }
	    ValidateSchema(filename);

	    // Load the document.
	    XmlDocument doc = new XmlDocument();
	    Stream filestream = XIncludeReader.GetStream(file.FullName);
	    doc.Load(filestream);
	    filestream.Close();

	    if (doc == null) {
		throw new InvalidOperationException("Could not parse xml document: " + file.Name);
	    } 

	    ConfigurationElement options = null;
	    XmlNode root = doc.DocumentElement["config"];
	    if (root != null) {
		options = new ConfigurationElement(root);
	    } else {
		options = new ConfigurationElement();
	    }

	    // If the root directory is not specified, make it the directory the config file is loaded from.
	    if (options.RootDirectory.Equals(String.Empty)) {
		options.RootDirectory = file.DirectoryName + "\\";
	    }
	    if (!options.RootDirectory.EndsWith("\\")) {
		options.RootDirectory += "\\";
	    }

	    ParserElement parserElement = ParserElement.ParseFromXml(options, doc);
	    String parserClass = parserElement.Class.Equals(String.Empty) ? "Spring2.DataTierGenerator.Parser.XmlParser" : parserElement.Class;

	    Object o = null;
	    try {
		System.Type parserType = System.Type.GetType(parserClass, true);
		Object[] args = { parserElement, options, doc };
		o = System.Activator.CreateInstance(parserType, args);
	    } catch (Exception ex) {
		Console.Out.WriteLine("ERROR: could not create class " + parserClass + ".\n" + ex.ToString());
		return null;
	    }

	    IParser parser = o as IParser;
	    if (parser == null) {
		Console.Out.WriteLine("ERROR: class " + parserElement.Class + " does not support IParser interface.\n");
		return null;
	    }

	    parser.Validate();
		
//	    parser.IsValid = isValid;
//	    foreach (ParserValidationMessage message in errors) {
//		//parser.Errors.Add(message);
//	    }
	    errors.Clear();
	    return parser;
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
	    } catch (UnauthorizedAccessException ex) {
		//dont have access permission
		IsValid = false;
		WriteToLog(ParserValidationMessage.NewError(ex.Message).ToString());
	    }
	    catch(Exception ex) {

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

	/// <summary>
	/// Event handler for parser validation events
	/// </summary>
	/// <param name="args"></param>
	protected void ParserValidationEventHandler(ParserValidationMessage message) {
	    if (message.Severity.Equals(ParserValidationSeverity.ERROR)) {
		IsValid = false;
	    }
	    errors.Add(message);
	}
    }
}
