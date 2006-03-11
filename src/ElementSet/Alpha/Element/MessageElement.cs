using System;
using System.Collections;
using System.Text;
using System.Xml;
using System.Collections.Specialized;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {
    /// <summary>
    /// Summary description for MessageElement.
    /// </summary>
    public class MessageElement : ElementSkeleton, IPropertyContainer {

	protected static readonly String PROPERTIES = "properties";
	protected static readonly String TEXT = "text";

	private ArrayList fields = new ArrayList();
	private ArrayList properties = new ArrayList();
	private IPropertyContainer container = new EntityElement();
	private String text = String.Empty;


	public ArrayList Fields {
	    get { return this.fields; }
	    set { this.fields = value; }
	}

	public ArrayList Properties {
	    get { return this.properties; }
	    set { this.properties = value; }
	}

	public String Text {
	    get { return this.text; }
	    set { this.text = value; }
	}


	public PropertyElement FindFieldByName(String name) {
	    foreach (PropertyElement field in Fields) {
		if (field.Name == name) {
		    return field;
		}
	    }
	    return null;
	}

	/// <summary>
	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
	/// list.
	/// </summary>
	/// <param name="node"></param>
	/// <param name="messageElements"></param>
	public static void ParseFromXml(XmlNode node, IList messageElements) {

	    if (node != null && messageElements != null) {

		foreach (XmlNode messageNode in node.ChildNodes) {
		    if (messageNode.NodeType.Equals(XmlNodeType.Element)) {
			MessageElement messageElement = new MessageElement();
			BuildElement(messageNode, messageElement);
			messageElements.Add(messageElement);
		    }
		}
	    }
	}

	public static void BuildElement(XmlNode messageNode, MessageElement messageElement) {
	    messageElement.Name = GetAttributeValue(messageNode, NAME, messageElement.Name);
	    messageElement.Text = GetAttributeValue(messageNode, TEXT, messageElement.Text);
	    PropertyElement.ParseFromXml(GetChildNodeByName(messageNode, PROPERTIES), messageElement.Fields);
	}

	public static ArrayList ParseFromXml(Configuration options, XmlDocument doc, Hashtable sqltypes, Hashtable types, ArrayList sqlentities, ParserValidationDelegate vd) {
	    ArrayList messages = new ArrayList();
	    XmlNodeList entities = doc.DocumentElement.GetElementsByTagName("message");
	    foreach (XmlNode node in entities) {
		MessageElement message = new MessageElement();
		message.Name = node.Attributes["name"].Value;
		message.Text = node.Attributes["text"].Value;
		message.fields = PropertyElement.ParseFromXml(GetChildNodeByName(node, PROPERTIES), messages, message, sqltypes, types,false,vd);
		messages.Add(message);
	    }

	    StringCollection names = new StringCollection();
	    foreach (MessageElement message in messages) {
		if (names.Contains(message.Name)) {
		    vd(new ParserValidationArgs(ParserValidationSeverity.ERROR, "duplicate message definition for " + message.Name));	    	    	
		} 
		else {
		    names.Add(message.Name);
		}
	    }   
	    return messages;
	}

	public static void BuildElement(XmlNode node, IPropertyContainer entity, MessageElement message, ParserValidationDelegate vd) {
	    if (node.Attributes["text"] != null) {
		message.Text = node.Attributes["text"].Value;
	    }

	    message.container = entity;

	    // Make sure the parameter names are specified and unique.unique.
	    for(int nameChanging=0;nameChanging < message.Fields.Count;nameChanging++) {
		PropertyElement field = (PropertyElement)(message.Fields[nameChanging]);
		if(field.ParameterName == String.Empty) {
		    field.ParameterName = field.Name;
		}
		int sequence = 1;
		string pName = field.ParameterName;
		bool changedName = true;
		while(changedName) {
		    changedName = false;
		    for(int nameChecking=0;nameChecking < nameChanging;nameChecking++) {
			PropertyElement p = (PropertyElement)(message.Fields[nameChecking]);
			if (pName.Equals(p.ParameterName)) {
			    sequence++;
			    pName = pName + sequence.ToString();
			    changedName = true;
			}
		    }
		}

		field.ParameterName = pName;
	    }

	}

	public string GetExpressionSubstitution(string substitutionExpression, string idString, ParserValidationDelegate vd) {
	    int parm = -1;
	    try {
		parm = Int32.Parse(substitutionExpression);
	    } catch(Exception) {
	    }

	    if (parm > -1) {
		if (parm >= fields.Count) {
		    if (vd != null) {
			vd(ParserValidationArgs.NewError("Substitution string " + substitutionExpression + " in " + idString + " refers to a parameter number larger that the number of parameters."));
		    }
		    return "";
		}

		String param = "@" + ((PropertyElement)(this.Fields[parm])).GetPropertyName();
		return param.Replace(".", "_");
	    }
	    else {
		PropertyElement p = this.container.FindFieldByName(substitutionExpression);
		if (p == null) {
		    if (vd != null) {
			vd(ParserValidationArgs.NewError("Substitution string " + substitutionExpression + " in " + idString + " refers to a property that does not occur in the message's entity."));
		    }
		    return "";
		} else {
		    return GetSqlExpression(p);
		}
	    }
	}

	protected virtual string GetSqlExpression(PropertyElement p) {
	    return p.GetSqlExpression();
	}
    }
}
