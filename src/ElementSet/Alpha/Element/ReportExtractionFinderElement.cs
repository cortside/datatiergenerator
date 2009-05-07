using System;
using System.Collections;
using System.Xml;

using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element 
{
    /// <summary>
    /// Summary description for ReportExtractionFinderElement.
    /// </summary>
    public class ReportExtractionFinderElement : FinderElement
    {
	/// <summary>
	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
	/// list.
	/// </summary>
	/// <param name="node"></param>
	/// <param name="finderElements"></param>
	public static new void ParseFromXml(XmlNode node, IList finderElements) 
	{

	    if (node != null && finderElements != null) 
	    {

		foreach (XmlNode finderNode in node.ChildNodes) 
		{
		    if (finderNode.NodeType.Equals(XmlNodeType.Element)) 
		    {
			ReportExtractionFinderElement finderElement = new ReportExtractionFinderElement();
			BuildElement(finderNode, finderElement);
			finderElements.Add(finderElement);
		    }
		}
	    }
	}

	public static ArrayList ParseFromXml(XmlNode root, IList entities, IPropertyContainer entity, Hashtable sqltypes, Hashtable types, ParserValidationDelegate vd) 
	{
	    ArrayList finders = new ArrayList();
	    XmlNodeList elements=null;
	    foreach (XmlNode n in root.ChildNodes) 
	    {
		if (n.Name.Equals("finders")) 
		{
		    elements = n.ChildNodes;
		    break;
		}
	    }
	    if (elements != null) 
	    {
		foreach (XmlNode node in elements) 
		{
		    if (node.NodeType == XmlNodeType.Comment)
		    {
			continue;
		    }
		    ReportExtractionFinderElement finder = new ReportExtractionFinderElement();
		    finder.Name = node.Attributes["name"].Value;
		    finder.Fields = PropertyElement.ParseFromXml(GetChildNodeByName(node, PROPERTIES), entities, entity, sqltypes, types, true, vd);
		    BuildElement(node, entity, finder, vd);
		    finders.Add(finder);
		}
	    }

	    return finders;
	}

	protected override string GetSqlExpression(PropertyElement p)
	{
	    return p.GetSqlExpression(true);
	}
    }
}
