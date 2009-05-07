using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element 
{

    /// <summary>
    /// Element which references an entity from a report extraction.
    /// </summary>
    public class EntityReferenceElement : ElementSkeleton
    {
	private static readonly String PROPERTY_REFERENCES = "propertyreferences";
	private static readonly String ENTITY = "entity";
	private static readonly String ALIAS_PREFIX = "aliasprefix";
	private static readonly String HINTS = "hints";
	private static readonly String FILTER = "filter";
	private static readonly String JOIN_MODIFIER = "joinmodifier";

	private static readonly string JOIN_LEFT = "LEFT";
	private static readonly string JOIN_RIGHT = "RIGHT";
	private static readonly string JOIN_FULL = "FULL";

	public static readonly EntityReferenceElement EMPTY = new EntityReferenceElement();

	private ArrayList propertyReferences = new ArrayList();
	private EntityElement entity = EntityElement.EMPTY;
	private String aliasPrefix = String.Empty;
	private string hints = String.Empty;
	private string filter = String.Empty;
	private string joinModifier = String.Empty;
	private ReportExtractionElement reportExtraction = new ReportExtractionElement();

	public EntityReferenceElement() {}

	/// <summary>
	/// Constructor to default values from the report extraction.
	/// </summary>
	/// <param name="data">Report extraction containing the entity reference.</param>
	public EntityReferenceElement(ReportExtractionElement data)
	{
	    this.Hints = data.Hints;
	}

	/// <summary>
	/// Prefix to be added to all property names in the entity reference.
	/// </summary>
	public string  AliasPrefix
	{
	    get { return this.aliasPrefix; }
	    set { this.aliasPrefix = value; }
	}

	/// <summary>
	/// Entity being referenced.
	/// </summary>
	public EntityElement Entity
	{
	    get { return this.entity; }
	    set { this.entity = value; }
	}
	
	/// <summary>
	/// SQL server hints to be added to the sql statement.
	/// </summary>
	public string Hints
	{
	    get { return this.hints; }
	    set { this.hints = value; }
	}
        
	/// <summary>
	/// For first entity reference in the list, this is the where clause.
	/// For subsequent entity references it is the join clause.
	/// </summary>
	public string Filter
	{
	    get { return this.filter; }
	    set { this.filter = value; }
	}

	/// <summary>
	/// Report extraction containing this entity reference.
	/// </summary>
	public ReportExtractionElement ReportExtraction
	{
	    get { return reportExtraction; }
	    set { reportExtraction = value; }
	}
	
	/// <summary>
	/// Join modifier (i.e. Left, Right, Full) for the join statement.
	/// </summary>
	public string JoinModifier
	{
	    get { return joinModifier; }
	    set { joinModifier = value; }
	}

	/// <summary>
	/// List of property references in the entity reference.
	/// </summary>
	public ArrayList PropertyReferences 
	{
	    get { return this.propertyReferences; }
	    set { this.propertyReferences = value; }
	}

	/// <summary>
	/// unused.
	/// </summary>
	/// <returns>XML representation of the object.</returns>
	public String ToXml() 
	{
	    StringBuilder sb = new StringBuilder();
	    sb.Append("<entityreference name=\"").Append(name).Append("\"");
	    sb.Append(" />");

	    return sb.ToString();
	}

	/// <summary>
	/// Find an entity refernence.
	/// </summary>
	/// <param name="entityReferences">List of entity references to search.</param>
	/// <param name="name">Name of entity reference to found.</param>
	/// <returns>EntityReferenceElement for the entity referenc efound.</returns>
	public static EntityReferenceElement FindEntityReferenceByName(ArrayList entityReferences, String name) 
	{
	    foreach (EntityReferenceElement entityReference in entityReferences) 
	    {
		if (entityReference.Name.Equals(name)) 
		{
		    return entityReference;
		}
	    }
	    return null;
	}

	/// <summary>
	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
	/// list.
	/// </summary>
	/// <param name="node">Node containing the enty reference elements.</param>
	/// <param name="EntityReferenceElements">returned list of entity reference elements parsed.</param>
	public static void ParseFromXml(XmlNode node, IList entityReferenceElements) 
	{

	    if (node != null && entityReferenceElements != null) 
	    {

		foreach (XmlNode refNode in node.ChildNodes) 
		{
		    if (refNode.NodeType.Equals(XmlNodeType.Element)) 
		    {
			EntityReferenceElement entityReferenceElement = new EntityReferenceElement();

			entityReferenceElement.Name = GetAttributeValue(refNode, NAME, entityReferenceElement.Name);
			entityReferenceElement.Entity.Name = GetAttributeValue(refNode, ENTITY, entityReferenceElement.Entity.Name);
			entityReferenceElement.AliasPrefix = GetAttributeValue(refNode, ALIAS_PREFIX, entityReferenceElement.AliasPrefix);
			entityReferenceElement.Hints = GetAttributeValue(refNode, HINTS, entityReferenceElement.Hints);
			entityReferenceElement.Filter = GetAttributeValue(refNode, FILTER, entityReferenceElement.Filter);
			entityReferenceElement.JoinModifier = GetAttributeValue(refNode, JOIN_MODIFIER, entityReferenceElement.JoinModifier);
			PropertyElement.ParseFromXml(GetChildNodeByName(refNode, PROPERTY_REFERENCES), entityReferenceElement.PropertyReferences);
			entityReferenceElements.Add(entityReferenceElement);
		    }
		}
	    }
	}

	/// <summary>
	/// Second pass parsing and validation.
	/// </summary>
	/// <param name="options">Configuration options</param>
	/// <param name="reportExtractionNode">Node contaiing the entity references.</param>
	/// <param name="reportExtraction">ReportExtraction element to contain the parsed entity references.</param>
	/// <param name="types">List of .Net types defined.</param>
	/// <param name="entities">List of entities defined.</param>
	/// <param name="vd">Validation delegate for error reporting.</param>
	/// <returns>List of entity references parsed.</returns>
	public static ArrayList ParseFromXml(Configuration options, XmlNode reportExtractionNode, ReportExtractionElement reportExtraction, Hashtable types, Hashtable sqltypes, IList entities, ParserValidationDelegate vd) 
	{
	    ArrayList entityReferences = new ArrayList();
	    foreach (XmlNode node in reportExtractionNode.ChildNodes) 
	    {
		if (node.NodeType == XmlNodeType.Comment)
		{
		    continue;
		}
		EntityReferenceElement entityReference = new EntityReferenceElement(reportExtraction);
		if (entityReference.Name != null)
		{
		    entityReference.Name = node.Attributes[NAME].Value;
		}
		else
		{
		    vd(ParserValidationArgs.NewError("Entity reference specified in report extraction " + reportExtraction.Name + " with no name."));
		}

		if (node.Attributes[ENTITY] != null)
		{
		    EntityElement ee = EntityElement.FindEntityByName(entities,node.Attributes[ENTITY].Value);
		    if (ee != null) 
		    {
			entityReference.Entity = ee;
			if (entityReference.Entity.SqlEntity.Name == String.Empty)
			{
			    vd(ParserValidationArgs.NewError("Entity Reference " + entityReference.Name + " refers to entity " + node.Attributes[ENTITY].Value + " which does not have an associated SQL entity."));
			}
		    } 
		    else 
		    {
			vd(ParserValidationArgs.NewError("Entity Reference " + entityReference.Name + " refers to entity " + node.Attributes[ENTITY].Value + " which does not exist."));
		    }

		}
		else
		{
		    vd(ParserValidationArgs.NewError("Entity Reference " + node.Attributes[NAME].Value + " has no entity attribute."));
		}

		if (node.Attributes[ALIAS_PREFIX] != null) 
		{
		    entityReference.aliasPrefix = node.Attributes[ALIAS_PREFIX].Value;
		}

		if (node.Attributes[HINTS] != null) 
		{
		    entityReference.Hints = node.Attributes[HINTS].Value;
		}

		if (node.Attributes[FILTER] != null) 
		{
		    entityReference.Filter = node.Attributes[FILTER].Value;
		}

		if (node.Attributes[JOIN_MODIFIER] != null) 
		{
		    entityReference.JoinModifier = node.Attributes[JOIN_MODIFIER].Value;
		    if (entityReference.JoinModifier.ToUpper() != JOIN_LEFT 
			&& entityReference.JoinModifier.ToUpper() != JOIN_RIGHT
			&& entityReference.JoinModifier.ToUpper() != JOIN_FULL)
		    {
			vd(ParserValidationArgs.NewError("Entity Reference " + node.Attributes[NAME].Value + " has join modifier other than left, right or full."));
		    }
		}

		entityReference.ReportExtraction = reportExtraction;
		// Note we pass entityReference.Entity because the fields refer to fields in the entity.
		entityReference.propertyReferences = PropertyElement.ParseFromXml(GetChildNodeByName(node, PROPERTY_REFERENCES), entities, entityReference.Entity, sqltypes, types, true, vd);
		entityReference.ProcessAsteriskName(vd);
		entityReference.AdjustSqlAlias();
		entityReferences.Add(entityReference);
	    }
	    return entityReferences;
	}

	/// <summary>
	/// Adjusts the sql alias strings for properties in the entity reference based on property alias's
	/// provided and the entity reference alias prefix.
	/// </summary>
	private void AdjustSqlAlias()
	{
	    foreach(PropertyElement p in this.PropertyReferences)
	    {
		string name = p.Column.Name;
		if (name == String.Empty)
		{
		    name = p.Name;
		}

		if (p.Alias != "")
		{
		    name = p.Alias;
		}
		else if (this.AliasPrefix != "")
		{
		    name = this.AliasPrefix + p.Column.Name;
		}

		p.Column = (ColumnElement)(p.Column.Clone());
		if (p.Column.Name == "")
		{
		    p.Column.Name = p.Name;
		}
		p.Alias = name;
		p.Expression = "[" + this.Name + "].[" + p.Column.Name + "]";
	    }
	}

	/// <summary>
	/// Adds references for all properties if * name is found.
	/// </summary>
	/// <param name="vd"></param>
	private void ProcessAsteriskName(ParserValidationDelegate vd)
	{
	    ArrayList asteriskNames = new ArrayList();
	    foreach(PropertyElement p in propertyReferences)
	    {
		if(p.Name == "*")
		{
		    asteriskNames.Add(p);
		}
	    }

	    if (asteriskNames.Count > 1)
	    {
		vd(ParserValidationArgs.NewError("More than one name of asterisk(*) was specified for Entity Reference " + this.Name + " in " + this.ReportExtraction.Name + "."));
	    }

	    if (asteriskNames.Count > 0)
	    {
		foreach (PropertyElement p in asteriskNames)
		{
		    propertyReferences.Remove(p);
		}

		PropertyElement allReference = (PropertyElement)asteriskNames[0];

		if(allReference.GroupFunction != String.Empty
		    && allReference.GroupFunction.ToLower() != PropertyElement.GROUP_FUNCTION_MIN
		    && allReference.GroupFunction.ToLower() != PropertyElement.GROUP_FUNCTION_MAX)
		{
		    vd(ParserValidationArgs.NewError("The property with name of * may only have group functions of min or max.  Error in entity refeence " + this.Name + " in report extraction " + this.ReportExtraction.Name + "."));
		}

		foreach(PropertyElement p in this.Entity.Fields)
		{
		    bool found = false;
		    foreach(PropertyElement pr in this.PropertyReferences)
		    {
			if (pr.Name == p.Name)
			{
			    found = true;
			    break;
			}
		    }

		    if (!found)
		    {
			PropertyElement entityReferencePropertyElement = new PropertyElement();
			entityReferencePropertyElement.GroupFunction = allReference.GroupFunction;
			entityReferencePropertyElement.Name = p.Name;
			entityReferencePropertyElement.AccessModifier = p.AccessModifier;
			entityReferencePropertyElement.Column = (ColumnElement)(p.Column.Clone());
			entityReferencePropertyElement.ConcreteType = p.ConcreteType;
			entityReferencePropertyElement.ConvertFromSqlTypeFormat = p.ConvertFromSqlTypeFormat;
			entityReferencePropertyElement.Type = p.Type;
			this.PropertyReferences.Add(entityReferencePropertyElement);
		    }
		}
	    }
	}

	/// <summary>
	/// Finds the property by its object name (i.e. alias)
	/// </summary>
	/// <param name="name">Object name to find.</param>
	/// <returns>Property found.</returns>
	public PropertyElement FindFieldBySqlAlias(String name) 
	{
	    foreach (PropertyElement field in propertyReferences) 
	    {
		if (field.GetSqlAlias() == name) 
		{
		    return field;
		}
	    }

	    return null;
	}

	/// <summary>
	/// Converts the sql filter to sql code.
	/// </summary>
	/// <returns>Converted filter.</returns>
	public string GetSqlFilterExpression()
	{
	    return this.Filter;
	}
    }
}
