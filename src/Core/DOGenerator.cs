using System;
using System.Data;
using System.IO;
using System.Collections;
using System.Text;

namespace Spring2.DataTierGenerator.Core {

    /// <summary>
    /// Generates the data object for an entity.
    /// </summary>
    public class DOGenerator : GeneratorBase {
	/// <summary>
	/// Handles creation of the static readonly property names and values.  Creates
	/// enttries of the form
	/// public static readonly string PROPERTY_NAME="Property.Name";
	/// </summary>
	private class PropertyName
	{
	    public String fieldName = "";
	    public String fieldValue = "";

	    private static readonly String PREFIX_SEPARATOR = "~";
	    private static readonly int MAX_FIELD_NAME_LENGTH = 50;

	    /// <summary>
	    /// Constructor
	    /// </summary>
	    /// <param name="prefix">Prefix for the property name/value.  Prefix separates classes with ~.  Like Property~Name</param>
	    /// <param name="propertyName">Property name</param>
	    public PropertyName(String prefix, String propertyName)
	    {
		fieldName = prefix.Replace(PREFIX_SEPARATOR,"_").ToUpper() + "_" + propertyName.ToUpper();

		// Keeping names a reasonable length.  This might not be the best
		// shortening algorithm but we can try it for a while.
		if (fieldName.Length > MAX_FIELD_NAME_LENGTH)
		{
		    fieldName = fieldName.Substring(fieldName.Length - MAX_FIELD_NAME_LENGTH);
		}
		if (fieldName.StartsWith("_"))
		{
		    fieldName = fieldName.Substring(1);
		}

		if (prefix.Length > 0)
		{
		    fieldValue = "\"" + prefix.Replace(PREFIX_SEPARATOR,".")
			+ "." +  propertyName + "\"";
		}
		else
		{
		    fieldValue = "\"" + propertyName + "\"";
		}
	    }

	    /// <summary>
	    /// Appends a property name to the current prefix so the new prefix
	    /// can be used for creating names for it's members.
	    /// </summary>
	    /// <param name="prefix">Old prefix in the form Property~Property...</param>
	    /// <param name="propertyName">New property to append</param>
	    /// <returns>New prefix in the same format.</returns>
	    public static String AppendPrefix(String prefix, String propertyName)
	    {
		if (prefix.Length > 0)
		{
		    return prefix + PREFIX_SEPARATOR + propertyName;
		}
		else
		{
		    return propertyName;
		}
	    }
	}

	/// <summary>
	/// Entity being generated.
	/// </summary>
	private Entity entity;

	/// <summary>
	/// List of all entities defined.
	/// </summary>
	private ArrayList entities;

	public DOGenerator(Configuration options, Entity entity, ArrayList entities) : base(options) {
	    this.entity = entity;
	    this.entities = entities;
	}

	/// <summary>
	/// Creates the data object class for this entity.
	/// </summary>
	public void CreateDataObjectClass() {
	    
	    IndentableStringWriter writer = new IndentableStringWriter();
			
	    // Create the header for the class.
	    GetUsingNamespaces(writer, entity.Fields, false);

	    // Class definition.
	    writer.WriteLine(0, "namespace " + options.GetDONameSpace(entity.Name) + " {");
	    writer.Write(1, "public class " + options.GetDOClassName(entity.Name));
	    if (options.DataObjectBaseClass.Length>0) {
		writer.Write(" : " + options.DataObjectBaseClass);
	    }
	    writer.WriteLine(" {");
	    writer.WriteLine();

	    // Declaration of private member variables.
	    foreach (Field field in entity.Fields) {
		if (field.Name.IndexOf('.')<0) {
		    writer.Write(2, field.AccessModifier + " ");
		    writer.Write(field.Type.Name);
		    writer.Write(" " + field.GetFieldFormat() + " = ");
		    writer.WriteLine(field.Type.NewInstanceFormat + ";");
		}
	    }

	    // Property Names
	    writer.WriteLine();
	    foreach (PropertyName propertyName in GetPropertyNames("", new Stack()))
	    {
		writer.WriteLine(2, 
		    "public static readonly String " + propertyName.fieldName
		    + " = " + propertyName.fieldValue + ";");
	    }
				    
	    // Properties.
	    foreach (Field field in entity.Fields) {
		writer.WriteLine();
		if (field.Name.IndexOf('.')<0) {
		    if (field.Description.Length>0) {
			writer.WriteLine(2, "/// <summary>");
			writer.WriteLine(2, "/// " + field.Description);
			writer.WriteLine(2, "/// </summary>");
		    }

		    writer.Write(2, "public ");
		    writer.Write(field.Type.Name);
		    writer.WriteLine(" " + field.GetMethodFormat() + " {");
		    writer.WriteLine(3, "get { return this." + field.GetFieldFormat() + "; }");
		    writer.WriteLine(3, "set { this." +  field.GetFieldFormat() + " = value; }");
		    writer.WriteLine(2, "}");
		}
	    }
		
	    // Close out the class and namespace.
	    writer.WriteLine(1, "}");
	    writer.WriteLine("}");

	    FileInfo file = new FileInfo(options.RootDirectory + options.DoClassDirectory + "\\" + options.GetDOClassName(entity.Name) + ".cs");
	    WriteToFile(file, writer.ToString(), false);
	}

	/// <summary>
	/// Returns a list of all properties in the class.  Note that it will create 
	/// a DOGenerator class for each DataObject entity that it finds as a property and
	/// will call that class to get it's entities.
	/// </summary>
	/// <param name="namePrefix">Prefix to add to the name representing parent entities.</param>
	/// <param name="entities">List of all entities in system.  Used to determine if property is a DataObject entity type</param>
	/// <param name="parentEntities">Stack of all parent entities.  Used to avoid loops.</param>
	/// <returns></returns>
	public ArrayList GetPropertyNames(String prefix, System.Collections.Stack parentEntities)
	{
	    ArrayList propertyNames = new ArrayList();

	    // avoid loops
	    bool matchedParent = false;
	    foreach (Entity searchEntity in parentEntities)
	    {
		if (searchEntity.Equals(entity))
		{
		    matchedParent = true;
		    break;
		}
	    }

	    // Add current entity to parent stack
	    parentEntities.Push(entity);

	    if (!matchedParent)
	    {
		foreach (Field field in entity.Fields) 
		{
		    if (field.Name.IndexOf(".") < 0)
		    {
			PropertyName propertyName = new PropertyName(prefix, field.Name);
			propertyNames.Add(propertyName);

			// Determine if this is a data object and if so append it's members.
			foreach (Entity subEntity in entities)
			{
			    if (field.Type.Name.Equals(options.GetDOClassName(subEntity.Name)))
			    {
				DOGenerator propertyGenerator = new DOGenerator(options, subEntity, entities);
				ArrayList subProperties = 
				    propertyGenerator.GetPropertyNames(
				    PropertyName.AppendPrefix(prefix, field.Name), 
				    parentEntities);
				foreach (PropertyName subProperty in subProperties)
				{
				    propertyNames.Add(subProperty);
				}
				break;
			    }
			} // end of loop through entities
		    }// end of if on indexOf(".")
		} // end of loop through fields.
	    } // end of matched parent check

	    parentEntities.Pop();
	    return propertyNames;
	} // end of GetPropertyNames

    }
}
