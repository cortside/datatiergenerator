using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class ColumnElement : SqlElementSkeleton {

	public static readonly String COLUMN = "column";
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

	private SqlEntityElement sqlEntity = null;
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

	public ColumnElement() {}

	public ColumnElement(String name) {
	    this.Name = name;
	}

	public ColumnElement(XmlNode columnNode, SqlEntityElement sqlEntity) : base(columnNode) {

	    if (COLUMN.Equals(columnNode.Name)) {
		this.sqlEntity = sqlEntity;
		sqlType.Name = GetAttributeValue(columnNode, SQL_TYPE, sqlType.Name);
		identity = Boolean.Parse(GetAttributeValue(columnNode, IDENTITY, identity.ToString()));
		length = Int32.Parse(GetAttributeValue(columnNode, LENGTH, length.ToString()));
		required = Boolean.Parse(GetAttributeValue(columnNode, REQUIRED, required.ToString()));
		viewColumn = Boolean.Parse(GetAttributeValue(columnNode, VIEW_COLUMN, viewColumn.ToString()));
		precision = Int32.Parse(GetAttributeValue(columnNode, PRECISION, precision.ToString()));
		scale = Int32.Parse(GetAttributeValue(columnNode, SCALE, scale.ToString()));
		expression = GetAttributeValue(columnNode, EXPRESSION, expression);
		Default = GetAttributeValue(columnNode, DEFAULT, Default);
		increment = Int32.Parse(GetAttributeValue(columnNode, INCREMENT, increment.ToString()));
		seed = Int32.Parse(GetAttributeValue(columnNode, SEED, seed.ToString()));
		foreignColumn = GetAttributeValue(columnNode, FOREIGN_COLUMN, foreignColumn);
	    } else {
		throw new ArgumentException("The XmlNode argument is not a column node.");
	    }
	}

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
	
	public String Declaration {
	    get { return String.Format(sqlType.DeclarationFormat, sqlType.Name, length, precision, scale); }
	}

	/// <summary>
	/// This is a sql expression for the use in creating views.  Is is not allowed if ViewColumn is false;
	/// </summary>
	public String Expression {
	    get { return this.expression; }
	    set { this.expression = value; }
	}

	public override void Validate(IParser parser) {
	    SqlTypeElement sqlType = parser.FindSqlType(this.sqlType.Name);
	    if (sqlType == null) {
		parser.AddValidationMessage(ParserValidationMessage.NewError(String.Format("SqlType ({0}) was not defined [column=({1}.{2})]", this.sqlType.Name, this.sqlEntity, this.Name)));
	    } else {
		this.sqlType = sqlType;
	    }
	}
	

	public static ArrayList ParseFromXml(XmlNode root, SqlEntityElement sqlentity, Hashtable sqltypes, Hashtable types, IParser parser) {
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
		    ColumnElement column = new ColumnElement();
		    column.Name = node.Attributes["name"].Value;
		    column.Description = node.InnerText.Trim();

		    if (node.Attributes["sqltype"] != null) {
			column.SqlType.Name = node.Attributes["sqltype"].Value;
			if (sqltypes.ContainsKey(column.SqlType.Name)) {
			    column.SqlType = (SqlTypeElement)((SqlTypeElement)sqltypes[column.SqlType.Name]).Clone();
			} else {
			    parser.AddValidationMessage(ParserValidationMessage.NewError("SqlType " + column.SqlType.Name + " was not defined [column=" + sqlentity.Name + "." + column.Name + "]"));
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

		    columns.Add(column);
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


	    sb.Append(" />");

	    return sb.ToString();
	}
    }
}
