using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator {

    public class Field : Spring2.Core.DataObject.DataObject {

	protected String name = String.Empty;
	protected String sqlName = String.Empty;
	protected SqlType sqlType = new SqlType();
	protected Type type = new Type();
	protected Boolean isRowGuidCol = false;
	protected Boolean isIdentity = false;
	protected Boolean isPrimaryKey = false;
	protected Boolean isForeignKey = false;
	protected Boolean isViewColumn = false;
	protected String accessModifier = "public";
	protected String description = String.Empty;

	public String Name {
	    get { return this.name; }
	    set { this.name = value; }
	}

	public String SqlName {
	    get { return this.sqlName; }
	    set { this.sqlName = value; }
	}

	public SqlType SqlType {
	    get { return this.sqlType; }
	    set { this.sqlType = value; }
	}

	public Type Type {
	    get { return this.type; }
	    set { this.type = value; }
	}

	public Boolean IsRowGuidCol {
	    get { return this.isRowGuidCol; }
	    set { this.isRowGuidCol = value; }
	}

	public Boolean IsIdentity {
	    get { return this.isIdentity; }
	    set { this.isIdentity = value; }
	}

	public Boolean IsPrimaryKey {
	    get { return this.isPrimaryKey; }
	    set { this.isPrimaryKey = value; }
	}

	public Boolean IsForeignKey {
	    get { return this.isForeignKey; }
	    set { this.isForeignKey = value; }
	}

	public Boolean IsViewColumn {
	    get { return this.isViewColumn; }
	    set { this.isViewColumn = value; }
	}

	public String AccessModifier {
	    get { return this.accessModifier; }
	    set { this.accessModifier = value; }
	}

	public String Description {
	    get { return this.description; }
	    set { this.description = value; }
	}

	/// <summary>
	/// Creates a string for a SqlParameter representing the specified field.
	/// </summary>
	/// <param name="this">Object that stores the information for the field the parameter represents.</param>
	/// <returns>String containing SqlParameter information of the specified field for a method call.</returns>
	public string CreateSqlParameter(bool blnOutput, bool useDataObject) {
	    StringBuilder sb = new StringBuilder();
	    sb.Append("cmd.Parameters.Add(new SqlParameter(\"@" + sqlName + "\", SqlDbType." + sqlType.SqlDbType + ", " + sqlType.Length + ", ParameterDirection.");
	    if (blnOutput) {
		sb.Append("Output"); 
	    } else {
		sb.Append("Input");
	    }
	    sb.Append(", false, " + sqlType.Precision + ", " + sqlType.Scale + ", \"" + name + "\", DataRowVersion.Proposed, ");
	    if (useDataObject) {
		sb.Append(String.Format(type.ConvertToSqlTypeFormat, "data", "data."+GetMethodFormat(), "", "", GetMethodFormat()));
	    } else {
		sb.Append(String.Format(type.ConvertToSqlTypeFormat, "", GetMethodFormat(), "", "", GetMethodFormat()));
	    }
	    sb.Append("));\n");
	    return sb.ToString();
	}

	public String GetFieldFormat() {
	    return this.Name.Substring(0, 1).ToLower() + this.Name.Substring(1);
	}

	public String GetMethodFormat() {
	    return this.Name.Substring(0, 1).ToUpper() + this.Name.Substring(1);
	}

	/// <summary>
	/// Creates a string for a method parameter representing the specified field.
	/// </summary>
	/// <returns>String containing parameter information of the specified field for a method call.</returns>
	public string CreateMethodParameter() {
	    return type.Name + " " + name.Substring(0, 1).ToUpper() + name.Substring(1);
	}

	/// <summary>
	/// Creates a string containing the parameter declaration for a stored procedure based on the parameters passed in.
	/// </summary>
	/// <returns>String containing parameter information of the specified field for a stored procedure.</returns>
	public string CreateParameterString(Boolean checkForOutput) {
	    String s = "@" + name + "\t" + sqlType.Declaration;
			
	    // Is the parameter an output parameter?
	    if (checkForOutput)
		if (isRowGuidCol || isIdentity)
		    s += " output";
			
	    return s;
	}

	public String ToXml(Boolean sqlAttributesOnly) {
	    StringBuilder sb = new StringBuilder();
	    sb.Append("<property name=\"").Append(name).Append("\"");
	    sb.Append(" sqlname=\"").Append(sqlName).Append("\"");
	    sb.Append(" sqltype=\"").Append(sqlType.Name).Append("\"");
	    if (sqlType.Length>0 && sqlType.Scale==0 && sqlType.Precision==0) sb.Append(" length=\"").Append(sqlType.Length.ToString()).Append("\"");
	    if (sqlType.Scale>0) sb.Append(" scale=\"").Append(sqlType.Scale.ToString()).Append("\"");
	    if (sqlType.Precision>0) sb.Append(" precision=\"").Append(sqlType.Precision.ToString()).Append("\"");
	    if (!sqlAttributesOnly) {
		if (sqlType.ReaderMethodFormat.Length>0) sb.Append(" readermethodformat=\"").Append(sqlType.ReaderMethodFormat).Append("\"");
		if (type.Name.Length>0) sb.Append(" type=\"").Append(type.Name).Append("\"");
		if (type.ConcreteType.Length>0) sb.Append(" concretetype=\"").Append(type.ConcreteType).Append("\"");
		if (type.Package.Length>0) sb.Append(" namespace=\"").Append(type.Package).Append("\"");
		if (type.ConvertToSqlTypeFormat.Length>0) sb.Append(" converttosqltypeformat=\"").Append(type.ConvertToSqlTypeFormat).Append("\"");
		if (type.ConvertFromSqlTypeFormat.Length>0) sb.Append(" convertfromsqltypeformat=\"").Append(type.ConvertFromSqlTypeFormat).Append("\"");
		if (type.NewInstanceFormat.Length>0) sb.Append(" newinstanceformat=\"").Append(type.NewInstanceFormat).Append("\"");
		if (!accessModifier.Equals("public")) sb.Append(" accessmodifier=\"").Append(accessModifier).Append("\"");
	    }
	    if (isRowGuidCol) sb.Append(" isrowguidcol=\"True\"");
	    if (isIdentity) sb.Append(" isidentity=\"True\"");
	    if (isPrimaryKey) sb.Append(" isprimarykey=\"True\"");
	    if (isForeignKey) sb.Append(" isforeignkey=\"True\"");
	    if (IsViewColumn) sb.Append(" isviewcolumn=\"True\"");
	    if (!sqlAttributesOnly && description.Length>0) {
		sb.Append("\t\t").Append(description).Append("\n");
		sb.Append("\t</property>");
	    } else {
		sb.Append(" />");
	    }

	    return sb.ToString();
	}

	public static ArrayList ParseFromXml(XmlDocument doc, Entity entity, Hashtable sqltypes, Hashtable types) {
	    ArrayList fields = new ArrayList();
	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("entity");
	    foreach (XmlNode element in elements) {
		String name = element.Attributes["name"].Value;
		String sqlObject = (element.Attributes["sqlobject"] == null) ? "" : element.Attributes["sqlobject"].Value;
		if (((entity.SqlObject.Length>0 && sqlObject == entity.SqlObject) || (entity.SqlObject.Length==0 && name == entity.Name)) && element.HasChildNodes) {
		    // look for a properties element, if one does not exist, assume that everything under the entity is a property (for backward compatablility)
		    XmlNodeList nodes = element.ChildNodes;
		    Boolean hasProperties = false;
		    foreach(XmlNode node in element.ChildNodes) {
			if (node.Name.Equals("properties")) {
			    nodes = node.ChildNodes;
			}
		    }
		    if (!hasProperties) {
			Console.Out.WriteLine("WARNING:  <property> elements should be defined within a <properties> element.");
		    }
		    foreach (XmlNode node in nodes) {
			// for properties  - collections and nested elements to be handled seperately
			if (node.Name.ToLower().Equals("property")) {
			    Field field = new Field();
			    if (node.Attributes["name"] != null) {
				field.Name = node.Attributes["name"].Value;
				field.SqlName = field.Name;
			    }
			    if (node.Attributes["sqlname"] != null) {
				if (node.Attributes["sqlname"].Value.Equals("*")) {
				    field.SqlName = field.Name;
				} else {
				    field.SqlName = node.Attributes["sqlname"].Value;
				}
			    }
			    field.Description = node.InnerText.Trim();
			    if (node.Attributes["sqltype"] != null) {
				field.SqlType.Name = node.Attributes["sqltype"].Value;

				// if the sql type is defined, default to all values defined in it
				if (sqltypes.ContainsKey(field.SqlType.Name)) {
				    field.SqlType = (SqlType)((SqlType)sqltypes[field.SqlType.Name]).Clone();
				    if (types.Contains(field.SqlType.Type)) {
					field.Type = (Type)((Type)types[field.SqlType.Type]).Clone();
				    } else {
					Console.Out.WriteLine("ERROR: Type " + field.SqlType.Type + " was not defined [property=" + field.name + "]");
				    }
				} else {
				    Console.Out.WriteLine("ERROR: SqlType " + field.SqlType.Name + " was not defined [property=" + field.name + "]");
				}
			    }
			    if (node.Attributes["length"] != null) {
				field.SqlType.Length = Int32.Parse(node.Attributes["length"].Value);
			    }
			    if (node.Attributes["scale"] != null) {
				field.SqlType.Scale = Int32.Parse(node.Attributes["scale"].Value);
			    }
			    if (node.Attributes["precision"] != null) {
				field.SqlType.Precision = Int32.Parse(node.Attributes["precision"].Value);
			    }

			    // the concrete type is the *real* type, type can be the same or can be in interface or coersable type
			    if (node.Attributes["type"] != null) {
				String type = node.Attributes["type"].Value;
				String concreteType = type;
				if (node.Attributes["concretetype"] != null) {
				    concreteType = node.Attributes["concretetype"].Value;
				}
				// if the data type is defined, default it as the property and left be overridden
				if (types.Contains(concreteType)) {
				    field.Type = (Type)((Type)types[concreteType]).Clone();
				    field.Type.Name = type;
				} else {
				    Console.Out.WriteLine("Type " + concreteType + " was not defined");
				}
			    }

			    if (node.Attributes["accessmodifier"] != null) {
				field.AccessModifier = node.Attributes["accessmodifier"].Value;
			    }

			    if (node.Attributes["convertfromsqltypeformat"] != null) {
				field.Type.ConvertFromSqlTypeFormat = node.Attributes["convertfromsqltypeformat"].Value;
			    }

			    field.IsIdentity = (node.Attributes["isidentity"] != null);
			    field.IsPrimaryKey = (node.Attributes["isprimarykey"] != null);
			    field.IsRowGuidCol = (node.Attributes["isrowguidcol"] != null);
			    field.IsForeignKey = (node.Attributes["isforeignkey"] != null);
			    field.IsViewColumn = (node.Attributes["isviewcolumn"] != null);
			    fields.Add(field);
			}
		    }
		}
	    }
	    return fields;
	}

    }
}
