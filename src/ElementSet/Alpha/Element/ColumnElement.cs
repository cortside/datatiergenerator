using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class ColumnElement : SqlElementSkeleton {

	private static readonly String SQL_TYPE = "sqltype";
	private static readonly String IDENTITY = "identity";
	private static readonly String LENGTH = "length";
	private static readonly String REQUIRED = "required";
	private static readonly String VIEW_COLUMN = "viewcolumn";
	private static readonly String PRECISION = "precision";
	private static readonly String SCALE = "scale";
	private static readonly String EXPRESSION = "expression";
	private static readonly String DEFAULT = "default";
	private static readonly String INCREMENT = "increment";
	private static readonly String SEED = "seed";
	private static readonly String FOREIGN_COLUMN = "foreigncolumn";
	private static readonly String OBSOLETE = "obsolete";
	private static readonly String COLLATE = "collate";

	private SqlTypeElement sqlType = new SqlTypeElement();
	private Boolean identity = false;
	private Int32 increment = 1;
	private Int32 seed = 1;
	private Boolean rowguidcol = false;
	private String formula = String.Empty;	    // for computed columns
	private Boolean required = false;
	private String defaultvalue = String.Empty;
	private String foreignColumn = String.Empty;
	private String sortDirection = String.Empty;
	private Boolean viewColumn = false;
	private String expression = String.Empty;
	private String prefix = String.Empty;
	private String foreignSqlEntity = String.Empty;
	private Int32 length = 0;
	private Int32 precision = 0;
	private Int32 scale = 0;
	private Boolean obsolete = false;
	private String collate = String.Empty;

	public String Formula {
	    get { return this.formula; }
	    set { this.formula = value; }
	}

	public String Default {
	    get { return this.defaultvalue; }
	    set { this.defaultvalue = value; }
	}

	public SqlTypeElement SqlType {
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

	public String EscapedForeignColumn {
	    get { return EscapeSqlName(this.foreignColumn); }
	}

	public String SortDirection {
	    get { return this.sortDirection; }
	    set { this.sortDirection = value; }
	}

	public Boolean ViewColumn {
	    get { return this.viewColumn; }
	    set { this.viewColumn = value; }
	}

	public String Prefix {
	    get { return this.prefix; }
	    set { this.prefix = value; }
	}

	public String ForeignSqlEntity {
	    get { return this.foreignSqlEntity; }
	    set { this.foreignSqlEntity = value; }
	}

	public String EscapedForeignSqlEntity { 
	    get { return EscapeSqlName(this.foreignSqlEntity); }
	}

	public Int32 Length 
	{
	    get { return length; }
	    set { length = value; }
	}

	public Int32 Precision {
	    get { return precision; }
	    set { precision = value; }
	}
	
	public Int32 Scale {
	    get { return scale; }
	    set { scale = value; }
	}
	
	public Boolean Obsolete {
	    get { return this.obsolete; }
	    set { this.obsolete = value; }
	}

	/// <summary>
	/// This is a sql expression for the use in creating views.  Is is not allowed if ViewColumn is false;
	/// </summary>
	public String Expression {
	    get { return this.expression; }
	    set { this.expression = value; }
	}

	/// <summary>
	/// This is the SQL Collation to use on the column.
	/// </summary>
	public String Collate {
	    get { return this.collate; }
	    set { this.collate = value; }
	}

	/// <summary>
	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
	/// list.
	/// </summary>
	/// <param name="node"></param>
	/// <param name="columnElements"></param>
	public static void ParseFromXml(XmlNode node, IList columnElements) {
	    if (node != null && columnElements != null) {
		foreach (XmlNode columnNode in node.ChildNodes) {
		    if (columnNode.NodeType.Equals(XmlNodeType.Element)) {
			ColumnElement columnElement = new ColumnElement();

			columnElement.Name = GetAttributeValue(columnNode, NAME, columnElement.Name);
			columnElement.SqlType.Name = GetAttributeValue(columnNode, SQL_TYPE, columnElement.SqlType.Name);
			columnElement.Identity = Boolean.Parse(GetAttributeValue(columnNode, IDENTITY, columnElement.Identity.ToString()));
			columnElement.Length = Int32.Parse(GetAttributeValue(columnNode, LENGTH, columnElement.Length.ToString()));
			columnElement.Required = Boolean.Parse(GetAttributeValue(columnNode, REQUIRED, columnElement.Required.ToString()));
			columnElement.ViewColumn = Boolean.Parse(GetAttributeValue(columnNode, VIEW_COLUMN, columnElement.ViewColumn.ToString()));
			columnElement.Precision = Int32.Parse(GetAttributeValue(columnNode, PRECISION, columnElement.Precision.ToString()));
			columnElement.Scale = Int32.Parse(GetAttributeValue(columnNode, SCALE, columnElement.Scale.ToString()));
			columnElement.Expression = GetAttributeValue(columnNode, EXPRESSION, columnElement.Expression);
			columnElement.Default = GetAttributeValue(columnNode, DEFAULT, columnElement.Default);
			columnElement.Increment = Int32.Parse(GetAttributeValue(columnNode, INCREMENT, columnElement.Increment.ToString()));
			columnElement.Seed = Int32.Parse(GetAttributeValue(columnNode, SEED, columnElement.Seed.ToString()));
			columnElement.ForeignColumn = GetAttributeValue(columnNode, FOREIGN_COLUMN, columnElement.ForeignColumn);
			columnElement.obsolete = Boolean.Parse(GetAttributeValue(columnNode, OBSOLETE, columnElement.Obsolete.ToString()));
			columnElement.Description = columnNode.InnerText.Trim();
			columnElement.Collate = GetAttributeValue(columnNode, COLLATE, columnElement.Collate);

			columnElements.Add(columnElement);
		    }
		}
	    }
	}

	public static ArrayList ParseFromXml(XmlNode root, SqlEntityElement sqlentity, Hashtable sqltypes, Hashtable types, ParserValidationDelegate vd) {
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
		    if (node.NodeType == XmlNodeType.Comment)
		    {
			continue;
		    }
		    if (node.Name.Equals("column")) {
			ColumnElement column = new ColumnElement();
			column.Name = GetAttributeValue(node, NAME, String.Empty);
			if (column.Name.Equals(String.Empty)) {
			    vd(ParserValidationArgs.NewError("SqlEntity " + sqlentity.Name + " has a column that a name was not specified or was blank"));
			}
			column.Description = node.InnerText.Trim();

			if (node.Attributes["sqltype"] != null) {
			    column.SqlType.Name = node.Attributes["sqltype"].Value;
			    if (sqltypes.ContainsKey(column.SqlType.Name)) {
				column.SqlType = (SqlTypeElement)((SqlTypeElement)sqltypes[column.SqlType.Name]).Clone();
			    } else {
				vd(ParserValidationArgs.NewError("SqlType " + column.SqlType.Name + " was not defined [column=" + sqlentity.Name + "." + column.Name + "]"));
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
			if (node.Attributes["expression"] != null) {
			    column.Expression = node.Attributes["expression"].Value;
			}
			if (node.Attributes["obsolete"] != null) {
			    column.Obsolete = Boolean.Parse(node.Attributes[OBSOLETE].Value);
			}
			if (node.Attributes[COLLATE] != null) {
			    column.Collate = node.Attributes[COLLATE].Value;
			}

			column.Description = node.InnerText.Trim();

			columns.Add(column);
		    }
		}
	    }
	    return columns;
	}

	public String SqlParameter {
	    get { return "@" + Name + "\t" + sqlType.Declaration; }
	}
   
	public String ToXml() {
	    StringBuilder sb = new StringBuilder();
	    sb.Append("<column");
	    sb.Append(" name=\"").Append(Name).Append("\"");

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
	    if (!Collate.Equals(String.Empty)) {
		sb.Append(" collate=\"").Append(Collate).Append("\"");
	    }

	    sb.Append(" />");

	    return sb.ToString();
	}
    }
}
