using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element 
{

    public class ComparerElement : ElementSkeleton 
    {
	private static readonly String COMPARERS = "comparers";

	private static readonly String PROPERTIES = "properties";

	private ArrayList properties = new ArrayList();

	public ArrayList Fields 
	{
	    get { return this.properties; }
	    set { this.properties = value; }
	}

	/// <summary>
	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
	/// list.
	/// </summary>
	/// <param name="node"></param>
	/// <param name="entityElements"></param>
	public static void ParseFromXml(XmlNode node, IList comparerElements) 
	{

	    if (node != null && comparerElements != null) 
	    {

		foreach (XmlNode comparerNode in node.ChildNodes) 
		{
		    if (comparerNode.NodeType.Equals(XmlNodeType.Element)) 
		    {
			ComparerElement comparerElement = new ComparerElement();

			comparerElement.Name = GetAttributeValue(comparerNode, NAME, comparerElement.Name);

			PropertyElement.ParseFromXml(GetChildNodeByName(comparerNode, PROPERTIES), comparerElement.Fields);
		
			comparerElements.Add(comparerElement);
		    }
		}
	    }
	}

	public static ArrayList ParseFromXml(XmlNode root, IList entities, IPropertyContainer container, Hashtable sqltypes, Hashtable types, ParserValidationDelegate vd) 
	{
	    ArrayList comparers = new ArrayList();
	    XmlNode comparerNode = GetChildNodeByName(root, COMPARERS);
	    if (comparerNode != null) 
	    {
		foreach (XmlNode node in comparerNode.ChildNodes) 
		{
		    if (node.NodeType == XmlNodeType.Comment)
		    {
			continue;
		    }
		    ComparerElement comparer = new ComparerElement();
		    if (node.Attributes["name"] == null)
		    {
			vd(ParserValidationArgs.NewError("ComparerElement in " + container.Name + " has no name attribute."));
			continue;
		    }
		    comparer.Name = node.Attributes["name"].Value;
		    comparer.Fields = PropertyElement.ParseFromXml(GetChildNodeByName(node, PROPERTIES), new ArrayList(), container, sqltypes, types, true, vd);
		    comparers.Add(comparer);
		}
	    }
	    return comparers;
	}
    }
}
