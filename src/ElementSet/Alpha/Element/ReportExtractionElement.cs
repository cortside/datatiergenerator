using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element 
{

    /// <summary>
    /// Class to implement "joining" of multiple entities into a single view for read-only display.
    /// </summary>
    public class ReportExtractionElement : ElementSkeleton, IPropertyContainer, ICollectable, IExpressionContainer
    {

	private static readonly String HINTS = "hints";
	private static readonly String HAVING = "having";
	private static readonly String ENTITY_REFERENCES = "entityreferences";
	private static readonly String COMPUTED_PROPERTIES = "computedproperties";
	private static readonly String COMPARERS = "comparers";
	private static readonly String FINDERS = "finders";

	public static readonly ReportExtractionElement EMPTY = new ReportExtractionElement();

	private ArrayList entityReferences = new ArrayList();
	private ArrayList computedProperties = new ArrayList();
	private ArrayList comparers = new ArrayList();
	private ArrayList finders = new ArrayList();
	private string hints = String.Empty;
	private string having = String.Empty;

	/// <summary>
	/// SQL Server hints to place, by default, on each table when building a query.
	/// </summary>
	public string Hints
	{
	    get { return this.hints; }
	    set { this.hints = value; }
	}

	/// <summary>
	/// A string representing the having clause for the query.
	/// </summary>
	public string Having
	{
	    get { return this.having; }
	    set { this.having = value; }
	}

	/// <summary>
	/// The entities to be joined.
	/// </summary>
	public ArrayList EntityReferences 
	{
	    get { return this.entityReferences; }
	    set { this.entityReferences = value; }
	}

	/// <summary>
	/// List of computed fields in the query.
	/// </summary>
	public ArrayList ComputedProperties 
	{
	    get { return this.computedProperties; }
	    set { this.computedProperties = value; }
	}

	/// <summary>
	/// Comparer classes to be generated in the collection for the data object.
	/// </summary>
	public ArrayList Comparers 
	{
	    get { return this.comparers; }
	    set { this.comparers = value; }
	}

	/// <summary>
	/// Finder definitions for the report extraction.
	/// </summary>
	public ArrayList Finders
	{
	    get { return this.finders; }
	    set { this.finders = value; }
	}

	/// <summary>
	/// Returns all property references and computed properties.
	/// </summary>
	public ArrayList Fields
	{
	    get
	    {
		ArrayList ret = new ArrayList(ComputedProperties);
		foreach(PropertyElement pre in GetAllPropertyReferences())
		{
		    ret.Add(pre);
		}

		return ret;
	    }
	}

	/// <summary>
	/// Unused
	/// </summary>
	/// <returns>a very minimal xml representation.</returns>
	public String ToXml() 
	{
	    StringBuilder sb = new StringBuilder();
	    sb.Append("<reportextraction name=\"").Append(name).Append("\"");
	    sb.Append(" />");

	    return sb.ToString();
	}

	/// <summary>
	/// Find a report extraction.
	/// </summary>
	/// <param name="reportExtractions">List of report extractions to search</param>
	/// <param name="name">Name of the report extraction.</param>
	/// <returns>The report extraction element found or null.</returns>
	public static ReportExtractionElement FindReportExtractionByName(ArrayList reportExtractions, String name) 
	{
	    foreach (ReportExtractionElement reportExtraction in reportExtractions) 
	    {
		if (reportExtraction.Name.Equals(name)) 
		{
		    return reportExtraction;
		}
	    }
	    return null;
	}

	/// <summary>
	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
	/// list.
	/// </summary>
	/// <param name="node">Node to look in for the report extractions.</param>
	/// <param name="ReportExtractionElements">List of report extractions created (returned)</param>
	public static void ParseFromXml(XmlNode node, IList ReportExtractionElements) 
	{

	    if (node != null && ReportExtractionElements != null) 
	    {

		foreach (XmlNode entityNode in node.ChildNodes) 
		{
		    if (entityNode.NodeType.Equals(XmlNodeType.Element)) 
		    {
			ReportExtractionElement reportExtractionElement = new ReportExtractionElement();

			reportExtractionElement.Name = GetAttributeValue(entityNode, NAME, reportExtractionElement.Name);
			reportExtractionElement.Hints = GetAttributeValue(entityNode, HINTS, reportExtractionElement.Name);
			reportExtractionElement.Having = GetAttributeValue(entityNode, HAVING, reportExtractionElement.Name);

			EntityReferenceElement.ParseFromXml(GetChildNodeByName(entityNode, ENTITY_REFERENCES), reportExtractionElement.entityReferences);
			PropertyElement.ParseFromXml(GetChildNodeByName(entityNode, COMPUTED_PROPERTIES), reportExtractionElement.ComputedProperties);
			ComparerElement.ParseFromXml(GetChildNodeByName(entityNode, COMPARERS), reportExtractionElement.Comparers);
			ReportExtractionFinderElement.ParseFromXml(GetChildNodeByName(entityNode, FINDERS), reportExtractionElement.Finders);
			ReportExtractionElements.Add(reportExtractionElement);
		    }
		}
	    }
	}

	/// <summary>
	/// Second pass parse and validation.
	/// </summary>
	/// <param name="options">Configuration options.</param>
	/// <param name="doc">Document being parsed.</param>
	/// <param name="sqltypes">List of sql types defined.</param>
	/// <param name="types">List of .Net types defined.</param>
	/// <param name="entities">List of EntityElement objects defined.</param>
	/// <param name="vd">Validation delegate for error reporting.</param>
	/// <returns>Validated list of report extractions.</returns>
	public static ArrayList ParseFromXml(Configuration options, XmlDocument doc, Hashtable sqltypes, Hashtable types, IList entities, ParserValidationDelegate vd) 
	{
	    ArrayList reportExtractions = new ArrayList();
	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("reportextraction");
	    foreach (XmlNode node in elements) 
	    {
		if (node.NodeType == XmlNodeType.Comment)
		{
		    continue;
		}
		ReportExtractionElement reportExtraction = new ReportExtractionElement();
		if (node.Attributes[NAME] != null)
		{
		    reportExtraction.Name = node.Attributes[NAME].Value;
		}
		else
		{
		    vd(ParserValidationArgs.NewError("A report extraction must have a name."));
		}
		if (node.Attributes[HINTS] != null) 
		{
		    reportExtraction.Hints = (string)(node.Attributes[HINTS].Value);
		}
		if (node.Attributes[HAVING] != null) 
		{
		    reportExtraction.Having = (string)(node.Attributes[HAVING].Value);
		}

		reportExtraction.EntityReferences = EntityReferenceElement.ParseFromXml(options, GetChildNodeByName(node, ENTITY_REFERENCES), reportExtraction, types, sqltypes, entities, vd);
		XmlNode computedProperties = GetChildNodeByName(node, COMPUTED_PROPERTIES);
		if (computedProperties != null)
		{
		    reportExtraction.ComputedProperties = PropertyElement.ParseFromXml(computedProperties, entities, reportExtraction, sqltypes, types, false, vd);
		}
		reportExtraction.ValidateFilters(vd);
		reportExtraction.ValidateExpressions(vd);
		reportExtraction.ValidateUniqueNames(vd);
		reportExtraction.ValidateDatabases(vd);
		reportExtractions.Add(reportExtraction);
		reportExtraction.Having = Configuration.PrepareExpression(reportExtraction.Having, reportExtraction, "having attribute in report extraction " + reportExtraction.Name, vd);

		// Make sure finders get prepared expressions!
		reportExtraction.Comparers = ComparerElement.ParseFromXml(node, entities, reportExtraction, sqltypes, types, vd);
		reportExtraction.Finders = ReportExtractionFinderElement.ParseFromXml(node, entities, reportExtraction, sqltypes, types, vd);
	    }
	    return reportExtractions;
	}

	/// <summary>
	/// Validates that names are unique across property references and computed properties.
	/// </summary>
	/// <param name="vd">Validation delegate for error reporting.</param>
	private void ValidateUniqueNames(ParserValidationDelegate vd)
	{
	    ArrayList names = new ArrayList();
	    foreach(EntityReferenceElement e in EntityReferences)
	    {
		foreach(PropertyElement p in e.PropertyReferences)
		{
		    names.Add(p.GetSqlAlias());
		}
	    }

	    foreach (PropertyElement p in ComputedProperties)
	    {
		names.Add(p.Name);
	    }

	    names.Sort();

	    string prevName = String.Empty;
	    int errorCount = 1;
	    for(int i = 0; i < names.Count; i++)
	    {
		string name = (string)(names[i]);
		if (name == prevName)
		{
		    errorCount++;
		}
		if (errorCount > 1 && (name != prevName || i >= names.Count - 1))
		{
		    vd(ParserValidationArgs.NewError("The property name " + prevName + " occurs " + errorCount.ToString() + " times in the report extraction " + this.Name));
		    errorCount = 1;
		}

		prevName = name;
	    }

	    names = new ArrayList();
	    foreach(EntityReferenceElement e in EntityReferences)
	    {
		names.Add(e.Name);
	    }
	    names.Sort();
	    prevName = String.Empty;
	    errorCount = 1;
	    for(int i = 0; i < names.Count; i++)
	    {
		string name = (string)(names[i]);
		if (name == prevName)
		{
		    errorCount++;
		}
		if (errorCount > 1 && (name != prevName || i >= names.Count - 1))
		{
		    vd(ParserValidationArgs.NewError("The entity reference name " + prevName + " occurs " + errorCount.ToString() + " times in the report extraction " + this.Name));
		    errorCount = 1;
		}

		prevName = name;
	    }


	}

	/// <summary>
	/// Validates the filters on the entity reference elements refer to embedded properties correctly.
	/// </summary>
	/// <param name="vd">Validation delegate for error reporting.</param>
	private void ValidateFilters(ParserValidationDelegate vd)
	{
	    foreach(EntityReferenceElement e in EntityReferences)
	    {
		e.Filter = Configuration.PrepareExpression(e.Filter, this, "filter for entity reference " + e.Name + " in report extraction " + this.Name, vd);
	    }	
	}

	/// <summary>
	/// Validates that the expressions for the computed properties refer to embedded properties correctly.
	/// Also sets up the SQL Alias's
	/// </summary>
	/// <param name="vd">Validation delegate for error reporting.</param>
	private void ValidateExpressions(ParserValidationDelegate vd)
	{
	    foreach(PropertyElement p in ComputedProperties)
	    {
		p.Column.Name = p.Name;
		if (p.Expression == String.Empty)
		{
		    vd(ParserValidationArgs.NewError("The computed property " + p.Name + " in report extraction " + this.Name + " has no expression."));
		}
		else
		{
		    p.Expression = Configuration.PrepareExpression(p.Expression, this, "expression for property " + p.Name + " in report extraction " + this.Name, vd);
		}

		if (p.Column.SqlType.Name == String.Empty)
		{
		    vd(ParserValidationArgs.NewError("The computed property " + p.Name + " in report extraction " + this.Name + " has no sql type defined."));
		}
	    }
	}

	/// <summary>
	/// Validates that the report extraction does not cross database boundaries.
	/// </summary>
	/// <param name="vd">Validation delegate for error reporting.</param>
	private void ValidateDatabases(ParserValidationDelegate vd)
	{
	    string firstKey = String.Empty;
	    foreach(EntityReferenceElement e in EntityReferences)
	    {
		if (e.Entity.SqlEntity.Name != String.Empty)
		{
		    if (firstKey == String.Empty)
		    {
			firstKey = e.Entity.SqlEntity.Key;
		    }
		    else if (e.Entity.SqlEntity.Name != String.Empty && e.Entity.SqlEntity.Key != firstKey)
		    {
			vd(ParserValidationArgs.NewError("All entity references in the report extraction" + this.Name + " must refer to entities whose sql entities all have the same key."));
			break;
		    }
		}
	    }
	}

	/*
	/// <summary>
	/// Replaces embedded property references in the string with sql cocde.
	/// </summary>
	/// <param name="withEmbedded">Expression to be replaced.</param>
	/// <param name="idString">Id to use when reporting errors.</param>
	/// <param name="vd">Validation delegate for error reporting.</param>
	/// <returns>String with embedded references replaced.</returns>
	private string PrepareExpression(string withEmbedded, string idString, ParserValidationDelegate vd)
	{
	    string checkExpression = withEmbedded;
	    string retVal = String.Empty;
	    int leftBrace = 0;
	    int startPos = 0;
	    for(leftBrace=checkExpression.IndexOf("{", startPos); startPos >=0; leftBrace=checkExpression.IndexOf("{", startPos))
	    {
		if (leftBrace == -1)
		{
		    // No more strings to replace.
		    retVal += checkExpression.Substring(startPos, checkExpression.Length - startPos);
		    break;
		}
		else
		{
		    // Concatenate portion of string without embedded references.
		    retVal += checkExpression.Substring(startPos, leftBrace - startPos);
		}
		int rightBrace = checkExpression.IndexOf("}", leftBrace);
		if (rightBrace == -1)
		{
		    if (vd != null)
		    {
			vd(ParserValidationArgs.NewError("The " + idString + " has a left brace({} with no corresonding right brace(}}"));
		    }
		    return "";
		}

		// Isolate the property reference.
		string expression = checkExpression.Substring(leftBrace+1, rightBrace - leftBrace - 1);

		// Create list of property references seen to check for circular references.
		ArrayList repeList = new ArrayList();

		string[] parts = expression.Split('.');
		string leftPart = "";
		string rightPart = "";
		if (parts.Length == 1)
		{
		    // Default the left part to this report extraction if not specified.
		    leftPart = this.Name;
		    rightPart = parts[0];
		}
		else if (parts.Length == 2)
		{
		    leftPart = parts[0];
		    rightPart = parts[1];
		}
		else
		{
		    if (vd != null)
		    {
			vd(ParserValidationArgs.NewError("The expression " + expression + " in the " + idString + " does not contain exactly one dot(.)."));
		    }
		    return "";
		}


		if (leftPart.ToLower() == this.Name.ToLower())
		{
		    // Refers to a property in this report extraction.
		    ReportExtractionPropertyElement repe = this.FindComputedFieldByName(rightPart);
		    if (repe != null)
		    {
			// Refers to a computed property.  Check for circular references.
			foreach(ReportExtractionPropertyElement repel in repeList)
			{
			    if(repel.Name == repe.Name)
			    {
				if (vd != null)
				{
				    vd(ParserValidationArgs.NewError("The expression " + expression + " in the " + idString + " has a circular reference."));
				}
				return "";
			    }
			}
			
			// Add in the expansion of this property.e
			retVal += "(" + repe.GetSqlExpression() + ")";
		    }
		    else
		    {
			EntityReferencePropertyElement pre = this.FindReferenceFieldByName(rightPart);
			if (pre != null)
			{
			    // Refers to a property reference.  Get the expression.
			    retVal += pre.GetSqlExpression();
			}
			else
			{
			    if (vd != null)
			    {
				vd(ParserValidationArgs.NewError("The expression " + expression + " in the " + idString + " refers to a property in the report extraction that does not exist."));
			    }
			    return "";
			}
		    }
		}
		else
		{
		    EntityReferenceElement er = FindEntityReferenceByName(leftPart);
		    if (er == null)
		    {
			if (vd != null)
			{
			    vd(ParserValidationArgs.NewError("The expression " + expression + " in the " + idString + " refers to an entity that is not an entity reference in the extraction nor the extraction itself."));
			}
			return "";
		    }
		    else
		    {
			// Refers to an entity reference.
			EntityReferencePropertyElement pre = er.FindFieldBySqlAlias(rightPart);
			if (pre != null)
			{
			    // Refers to a property reference in the entity.
			    retVal += pre.GetSqlExpression();
			}
			else
			{
			    PropertyElement p = er.Entity.FindFieldByName(rightPart);
			    if (p != null)
			    {
				// Refers to a property in the entity referenced that is not in the property referenced.
				retVal += "[" + er.Name + "].[" + p.Column.Name + "]";
			    }
			    else
			    {
				if (vd != null)
				{
				    vd(ParserValidationArgs.NewError("The expression " + expression + " in the " + idString + " refers to a property that is not in the referenced entity reference nor it's associated entity."));
				}
				else
				{
				    return "";
				}
			    }
			}
		    }
		}
		
		startPos = rightBrace + 1;
	    }

	    return retVal;
	}*/

	public string GetExpressionSubstitution(string substitutionExpression, string idString, ParserValidationDelegate vd)
	{
	    // Create list of property references seen to check for circular references.
	    ArrayList repeList = new ArrayList();

	    string[] parts = substitutionExpression.Split('.');
	    string leftPart = "";
	    string rightPart = "";
	    if (parts.Length == 1)
	    {
		// Default the left part to this report extraction if not specified.
		leftPart = this.Name;
		rightPart = parts[0];
	    }
	    else if (parts.Length == 2)
	    {
		leftPart = parts[0];
		rightPart = parts[1];
	    }
	    else
	    {
		if (vd != null)
		{
		    vd(ParserValidationArgs.NewError("The expression " + substitutionExpression + " in the " + idString + " does not contain exactly one dot(.)."));
		}
		return "";
	    }


	    if (leftPart.ToLower() == this.Name.ToLower())
	    {
		// Refers to a property in this report extraction.
		PropertyElement repe = this.FindComputedFieldByName(rightPart);
		if (repe != null)
		{
		    // Refers to a computed property.  Check for circular references.
		    foreach(PropertyElement repel in repeList)
		    {
			if(repel.Name == repe.Name)
			{
			    if (vd != null)
			    {
				vd(ParserValidationArgs.NewError("The expression " + substitutionExpression + " in the " + idString + " has a circular reference."));
			    }
			    return "";
			}
		    }
			
		    // Add in the expansion of this property.e
		    return "(" + repe.GetSqlExpression() + ")";
		}
		else
		{
		    PropertyElement pre = this.FindReferenceFieldByName(rightPart);
		    if (pre != null)
		    {
			// Refers to a property reference.  Get the expression.
			return pre.GetSqlExpression();
		    }
		    else
		    {
			if (vd != null)
			{
			    vd(ParserValidationArgs.NewError("The expression " + substitutionExpression + " in the " + idString + " refers to a property in the report extraction that does not exist."));
			}
			return "";
		    }
		}
	    }
	    else
	    {
		EntityReferenceElement er = FindEntityReferenceByName(leftPart);
		if (er == null)
		{
		    if (vd != null)
		    {
			vd(ParserValidationArgs.NewError("The expression " + substitutionExpression + " in the " + idString + " refers to an entity that is not an entity reference in the extraction nor the extraction itself."));
		    }
		    return "";
		}
		else
		{
		    // Refers to an entity reference.
		    PropertyElement pre = er.FindFieldBySqlAlias(rightPart);
		    if (pre != null)
		    {
			// Refers to a property reference in the entity.
			return pre.GetSqlExpression();
		    }
		    else
		    {
			PropertyElement p = er.Entity.FindFieldByName(rightPart);
			if (p != null)
			{
			    // Refers to a property in the entity referenced that is not in the property referenced.
			    return "[" + er.Name + "].[" + p.Column.Name + "]";
			}
			else
			{
			    if (vd != null)
			    {
				vd(ParserValidationArgs.NewError("The expression " + substitutionExpression + " in the " + idString + " refers to a property that is not in the referenced entity reference nor it's associated entity."));
			    }
			    return "";
			}		    
		    }
		}
	    }
	}

	/// <summary>
	/// Finds an entity reference.
	/// </summary>
	/// <param name="name">Name of entity reference to find.</param>
	/// <returns>Entity reference found.</returns>
	public EntityReferenceElement FindEntityReferenceByName(String name) 
	{
	    foreach (EntityReferenceElement entityReference in entityReferences) 
	    {
		if (entityReference.Name == name) 
		{
		    return entityReference;
		}
	    }
	    return null;
	}

	/// <summary>
	/// Finds a property reference or computed property.
	/// </summary>
	/// <param name="name">Name of property to find.</param>
	/// <returns>property reference or computed property element found.</returns>
	public PropertyElement FindFieldByName(String name)
	{
	    PropertyElement retVal = null;
	    PropertyElement rp = this.FindComputedFieldByName(name);
	    if (rp != null)
	    {
		retVal = rp;
	    }
	    else
	    {
		PropertyElement pr = this.FindReferenceFieldByName(name);
		if (pr != null)
		{
		    retVal = pr;
		}
	    }

	    return retVal;
	}

	/// <summary>
	/// Finds a computed property.
	/// </summary>
	/// <param name="name">Name of computed property.</param>
	/// <returns>Computed property element found.</returns>
	public PropertyElement FindComputedFieldByName(String name)
	{
	    foreach (PropertyElement p in ComputedProperties)
	    {
		if (p.Name == name)
		{
		    return p;
		}
	    }

	    return null;
	}

	/// <summary>
	/// Finds a property referfence.
	/// </summary>
	/// <param name="name">Name of property reference.</param>
	/// <returns>Property reference element found.</returns>
	public PropertyElement FindReferenceFieldByName(String name)
	{
	    foreach (EntityReferenceElement e in this.EntityReferences)
	    {
		foreach(PropertyElement p in e.PropertyReferences)
		{
		    if (p.GetSqlAlias() == name)
		    {
			return p;
		    }
		}
	    }

	    return null;
	}

	/// <summary>
	/// gets the first entity reference.
	/// </summary>
	/// <returns>The first entity reference element in the list.</returns>
	public EntityReferenceElement GetFirstEntityReference()
	{
	    return ((EntityReferenceElement)(entityReferences[0]));
	}

	/// <summary>
	/// Gets proeprty references.
	/// </summary>
	/// <returns>Returns list of all property references in the report extraction.</returns>
	public IList GetAllPropertyReferences()
	{
	    ArrayList l = new ArrayList();
	    foreach(EntityReferenceElement e in EntityReferences)
	    {
		foreach(PropertyElement p in e.PropertyReferences)
		{
		    l.Add(p);
		}
	    }
	    return l;
	}

	/// <summary>
	/// Gets grouping strings.
	/// </summary>
	/// <returns>List of sql code to use for each groupby property in the report extraction.</returns>
	public ArrayList GetGroupPropertyStrings()
	{
	    ArrayList l = new ArrayList();
	    foreach(EntityReferenceElement e in EntityReferences)
	    {
		foreach(PropertyElement p in e.PropertyReferences)
		{
		    if (p.GroupBy)
		    {
			l.Add(p.GetSqlExpression());
		    }
		}
	    }

	    foreach(PropertyElement p in this.ComputedProperties)
	    {
		if (p.GroupBy)
		{
		    l.Add(p.GetSqlExpression());
		}
	    }

	    return l;
	}

	/// <summary>
	/// Gets property names for the static readonly property names in the generated code.
	/// </summary>
	/// <param name="options">Options defined.</param>
	/// <param name="entities">List of all enties int he system.</param>
	/// <returns></returns>
	public ArrayList GetPropertyNames(Configuration options, IList entities) 
	{
	    ArrayList propertyNames = new ArrayList();

	    foreach (EntityReferenceElement entityReference in this.EntityReferences) 
	    {
		foreach (PropertyElement field in entityReference.PropertyReferences)
		{
		    EntityElement.PropertyName propertyName = new EntityElement.PropertyName("", field.GetSqlAlias());
		    propertyNames.Add(propertyName);
		} // end of loop through property references
	    } // end of loop through entity references

	    foreach (PropertyElement reportExtractionPropertyElement in this.ComputedProperties)
	    {
		EntityElement.PropertyName propertyName = new EntityElement.PropertyName("", reportExtractionPropertyElement.Name);
		propertyNames.Add(propertyName);
	    } // end of loop through property references.

	    return propertyNames;
	}
    }
}
