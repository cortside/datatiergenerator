using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator {

    public class Column : Spring2.Core.DataObject.DataObject, ICloneable {

	private String name = String.Empty;
	private SqlType sqlType = new SqlType();
	private Boolean identity = false;
	private Int32 increment = 1;
	private Int32 seed = 1;
	private Boolean rowguidcol = false;
	private String formula = String.Empty;	    // for computed columns
	private Boolean required = false;
	private String defaultvalue = String.Empty;
	private String description = String.Empty;
	private String foreignColumn = String.Empty;
	private String sortDirection = String.Empty;
	private Boolean viewColumn = false;

	public String Name {
	    get { return this.name; }
	    set { this.name = value; }
	}

	public String Description {
	    get { return this.description; }
	    set { this.description = value; }
	}

	public String Formula {
	    get { return this.formula; }
	    set { this.formula = value; }
	}

	public String Default {
	    get { return this.defaultvalue; }
	    set { this.defaultvalue = value; }
	}

	public SqlType SqlType {
	    get { return this.sqlType; }
	    set { this.sqlType = value; }
	}

	public Boolean Identity {
	    get { return this.identity; }
	    set { this.identity = value; }
	}

	public Boolean RowGuidCol {
	    get { return this.rowguidcol; }
	    set { this.rowguidcol = value; }
	}

	public Boolean Required {
	    get { return this.required; }
	    set { this.required = value; }
	}

	public Int32 Increment {
	    get { return this.increment; }
	    set { this.increment = value; }
	}

	public Int32 Seed {
	    get { return this.seed; }
	    set { this.seed = value; }
	}

	public String ForeignColumn {
	    get { return this.foreignColumn; }
	    set { this.foreignColumn = value; }
	}

	public String SortDirection {
	    get { return this.sortDirection; }
	    set { this.sortDirection = value; }
	}

	public Boolean ViewColumn {
	    get { return this.viewColumn; }
	    set { this.viewColumn = value; }
	}

	public Object Clone() {
	    return MemberwiseClone();
	}

	public static ArrayList ParseFromXml(XmlNode root, SqlEntity sqlentity, Hashtable sqltypes, Hashtable types) {
	    ArrayList columns = new ArrayList();
	    XmlNodeList elements=null;
	    foreach (XmlNode n in root.ChildNodes) {
		if (n.Name.Equals("columns")) {
		    elements = n.ChildNodes;
		    break;
		}
	    }
	    if (elements != null) {
		foreach (XmlNode node in elements) {
		    Column column = new Column();
		    column.Name = node.Attributes["name"].Value;
		    column.Description = node.InnerText.Trim();

		    if (node.Attributes["sqltype"] != null) {
			column.SqlType.Name = node.Attributes["sqltype"].Value;
			if (sqltypes.ContainsKey(column.SqlType.Name)) {
			    column.SqlType = (SqlType)((SqlType)sqltypes[column.SqlType.Name]).Clone();
			} else {
			    Console.Out.WriteLine("ERROR: SqlType " + column.SqlType.Name + " was not defined [column=" + sqlentity.Name + "." + column.name + "]");
			}
		    }

		    if (node.Attributes["required"] != null) {
			column.Required = Boolean.Parse(node.Attributes["required"].Value);
		    }
		    if (node.Attributes["identity"] != null) {
			column.Identity = Boolean.Parse(node.Attributes["identity"].Value);
		    }
		    if (node.Attributes["rowguidcol"] != null) {
			column.RowGuidCol = Boolean.Parse(node.Attributes["rowguidcol"].Value);
		    }
		    if (node.Attributes["viewcolumn"] != null) {
			column.ViewColumn = Boolean.Parse(node.Attributes["viewcolumn"].Value);
		    }

		    if (node.Attributes["increment"] != null) {
			column.Increment = Int32.Parse(node.Attributes["increment"].Value);
		    }
		    if (node.Attributes["seed"] != null) {
			column.Seed = Int32.Parse(node.Attributes["seed"].Value);
		    }
		    if (node.Attributes["default"] != null) {
			column.Default = node.Attributes["default"].Value;
		    }
		    if (node.Attributes["formula"] != null) {
			column.Formula = node.Attributes["formula"].Value;
		    }
		    if (node.Attributes["length"] != null) {
			column.SqlType.Length = Int32.Parse(node.Attributes["length"].Value);
		    }
		    if (node.Attributes["scale"] != null) {
			column.SqlType.Scale = Int32.Parse(node.Attributes["scale"].Value);
		    }
		    if (node.Attributes["precision"] != null) {
			column.SqlType.Precision = Int32.Parse(node.Attributes["precision"].Value);
		    }

		    columns.Add(column);
		}
	    }
	    return columns;
	}

	public String SqlParameter {
	    get { return "@" + name + "\t" + sqlType.Declaration; }
	}
   
	public String ToXml() {
	    StringBuilder sb = new StringBuilder();
	    sb.Append("<column");
	    sb.Append(" name=\"").Append(name).Append("\"");

	    if (sqlType.Name.Length>0) {
		sb.Append(" sqltype=\"").Append(sqlType.Name).Append("\"");
	    }
	    if (sqlType.Length!=0) {
		sb.Append(" length=\"").Append(sqlType.Length.ToString()).Append("\"");
	    }
	    if (sqlType.Scale!=0) {
		sb.Append(" scale=\"").Append(sqlType.Scale.ToString()).Append("\"");
	    }
	    if (sqlType.Precision!=0) {
		sb.Append(" precision=\"").Append(sqlType.Precision.ToString()).Append("\"");
	    }
	    if (identity) {
		sb.Append(" identity=\"True\"");
		if (increment!=1) {
		    sb.Append(" increment=\"").Append(increment.ToString()).Append("\"");
		}
		if (seed!=1) {
		    sb.Append(" seed=\"").Append(seed.ToString()).Append("\"");
		}
	    }
	    if (rowguidcol) {
		sb.Append(" rowguidcol=\"True\"");
	    }
	    if (!formula.Equals(String.Empty)) {
		sb.Append(" formula=\"").Append(formula).Append("\"");
	    }
	    if (!defaultvalue.Equals(String.Empty)) {
		sb.Append(" default=\"").Append(defaultvalue).Append("\"");
	    }
	    if (required) {
		sb.Append(" required=\"True\"");
	    }
	    if (viewColumn) {
		sb.Append(" viewcolumn=\"True\"");
	    }

	    if (!foreignColumn.Equals(String.Empty)) {
		sb.Append(" foreigncolumn=\"").Append(foreignColumn).Append("\"");
	    }
	    if (!sortDirection.Equals(String.Empty)) {
		sb.Append(" sortdirection=\"").Append(sortDirection).Append("\"");
	    }


	    sb.Append(" />");

	    return sb.ToString();
	}
    }
}
