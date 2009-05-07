using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class FinderElement : ElementSkeleton, IExpressionContainer {

	protected static readonly String PROPERTIES = "properties";
	private static readonly String SORT = "sort";
	private static readonly String UNIQUE = "unique";
	private static readonly String EXPRESSION = "expression";
	private static readonly String LIMIT = "limit";

	private String sort = String.Empty;
	private Boolean unique = false;
	private String expression = String.Empty;
	private ArrayList fields = new ArrayList();
	private ArrayList properties = new ArrayList();
	private IPropertyContainer container = new EntityElement();
	private Boolean limit = false;

	public String Sort {
	    get { return this.sort; }
	    set { this.sort = value; }
	}

	public Boolean Unique {
	    get { return this.unique; }
	    set { this.unique = value; }
	}

	public String Expression {
	    get { return this.expression; }
	    set { this.expression = value; }
	}

	public ArrayList Fields {
	    get { return this.fields; }
	    set { this.fields = value; }
	}

	public ArrayList Properties {
	    get { return this.properties; }
	    set { this.properties = value; }
	}


	public Boolean Limit {
	    get { return this.limit; }
	    set { this.limit = value; }
	}

	/// <summary>
	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
	/// list.
	/// </summary>
	/// <param name="node"></param>
	/// <param name="finderElements"></param>
	public static void ParseFromXml(XmlNode node, IList finderElements) {

	    if (node != null && finderElements != null) {

		foreach (XmlNode finderNode in node.ChildNodes) {
		    if (finderNode.NodeType.Equals(XmlNodeType.Element)) {
			FinderElement finderElement = new FinderElement();
			BuildElement(finderNode, finderElement);
			finderElements.Add(finderElement);
		    }
		}
	    }
	}

	public static void BuildElement(XmlNode finderNode, FinderElement finderElement) {
	    finderElement.Name = GetAttributeValue(finderNode, NAME, finderElement.Name);
	    finderElement.Sort = GetAttributeValue(finderNode, SORT, finderElement.Sort);
	    finderElement.Expression = GetAttributeValue(finderNode, EXPRESSION, finderElement.Expression);
	    finderElement.Unique = Boolean.Parse(GetAttributeValue(finderNode, UNIQUE, finderElement.Unique.ToString()));
	    finderElement.Limit = Boolean.Parse(GetAttributeValue(finderNode, LIMIT, finderElement.Limit.ToString()));

	    PropertyElement.ParseFromXml(GetChildNodeByName(finderNode, PROPERTIES), finderElement.Fields);
	}

	public static ArrayList ParseFromXml(XmlDocument doc, XmlNode root, IList entities, IPropertyContainer entity, Hashtable sqltypes, Hashtable types, ParserValidationDelegate vd) {
	    ArrayList finders = new ArrayList();
	    XmlNodeList elements=null;
	    foreach (XmlNode n in root.ChildNodes) {
		if (n.Name.Equals("finders")) {
		    elements = n.ChildNodes;
		    break;
		}
	    }
	    if (elements != null) {
		foreach (XmlNode node in elements) {
		    if (node.NodeType == XmlNodeType.Comment) {
			continue;
		    }
		    FinderElement finder = new FinderElement();
		    finder.Name = node.Attributes["name"].Value;
		    finder.Fields = PropertyElement.ParseFromXml(GetChildNodeByName(node, PROPERTIES), entities, entity, sqltypes, types, true, vd);
		    BuildElement(node, entity, finder, vd);
		    finders.Add(finder);
		}
	    }
	    return finders;
	}

	public static void BuildElement(XmlNode node, IPropertyContainer entity, FinderElement finder, ParserValidationDelegate vd) {
	    if (node.Attributes["sort"] != null) {
		finder.Sort = node.Attributes["sort"].Value;
	    }
	    if (node.Attributes[EXPRESSION] != null) {
		finder.Expression = node.Attributes[EXPRESSION].Value;
	    }
	    if (node.Attributes["unique"] != null) {
		finder.Unique = Boolean.Parse(node.Attributes["unique"].Value);
	    }
	    if (node.Attributes[LIMIT] != null) {
		finder.Limit = Boolean.Parse(node.Attributes[LIMIT].Value);
	    }

	    // Default sort to all finder properties if none specified.
	    if (finder.Sort == String.Empty) {
		foreach(PropertyElement p in finder.Fields) {
		    if (finder.Sort != String.Empty) {
			finder.Sort += ", ";
		    }
		    finder.Sort += p.GetSqlAlias();
		}
	    }

            //Adds all attributes including all non defined by element class 
            foreach (XmlAttribute attribute in node.Attributes) {
                if (!finder.Attributes.ContainsKey(attribute.Name)) {
                    finder.Attributes.Add(attribute.Name, attribute.Value);
                }
            }

	    finder.container = entity;

	    // Make sure the parameter names are specified and unique.unique.
	    for(int nameChanging=0;nameChanging < finder.Fields.Count;nameChanging++) {
		PropertyElement field = (PropertyElement)(finder.Fields[nameChanging]);
		if(field.ParameterName == String.Empty) {
		    field.ParameterName = field.Name;
		}
		int sequence = 1;
		string pName = field.ParameterName;
		bool changedName = true;
		while(changedName) {
		    changedName = false;
		    for(int nameChecking=0;nameChecking < nameChanging;nameChecking++) {
			PropertyElement p = (PropertyElement)(finder.Fields[nameChecking]);
			if (pName.Equals(p.ParameterName)) {
			    sequence++;
			    pName = pName + sequence.ToString();
			    changedName = true;
			}
		    }
		}

		field.ParameterName = pName;
	    }

	    // Validate the expression, if any.
	    if (finder.Expression != String.Empty) {
		finder.Expression = Configuration.PrepareExpression(finder.Expression, finder, " finder " + finder.Name + " in " + entity.Name + " ", vd);
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
			vd(ParserValidationArgs.NewError("Substitution string " + substitutionExpression + " in " + idString + " refers to a property that does not occur in the finder's entity."));
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
