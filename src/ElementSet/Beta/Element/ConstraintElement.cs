using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class ConstraintElement : SqlElementSkeleton {

	public const String FOREIGN_KEY = "FOREIGN KEY";
	public const String PRIMARY_KEY = "PRIMARY KEY";
	public const String UNIQUE = "UNIQUE";
	public const String CHECK = "CHECK";

	public static readonly String CONSTRAINT = "constraint";
	private static readonly String TYPE = "type";
	protected static readonly String CLUSTERED = "clustered";
    	protected static readonly String FOREIGN_ENTITY = "foreignentity";
	protected static readonly String CHECK_CLAUSE = "checkclause";
	private static readonly String PREFIX = "prefix";

	private SqlEntityElement sqlEntity = null;
	private String type = String.Empty;
	private Boolean clustered = false;
	private String checkClause = String.Empty;
	private ArrayList columns = new ArrayList();
	private String prefix = String.Empty;

	public ConstraintElement() {}

	public ConstraintElement(XmlNode constraintNode) : base(constraintNode) {}

	protected ConstraintElement(XmlNode constraintNode, SqlEntityElement sqlEntity) : base(constraintNode) {

	    this.sqlEntity = sqlEntity;
	    if (CONSTRAINT.Equals(constraintNode.Name)) {
		type = GetAttributeValue(constraintNode, TYPE, type);
		prefix = GetAttributeValue(constraintNode, PREFIX, prefix);

		foreach (XmlNode node in GetChildNodes(constraintNode, ColumnElement.COLUMN)) {
		    columns.Add(new ColumnElement(node, null));
		}
	    } else {
		throw new ArgumentException("The XmlNode argument is not a constraint node.");
	    }
	}

	public static ConstraintElement NewInstance(XmlNode constraintNode, SqlEntityElement sqlEntity) {
	    String type = GetAttributeValue(constraintNode, TYPE, String.Empty);
	    switch (type) {
		case UNIQUE:
		    return new UniqueConstraintElement(constraintNode, sqlEntity);
		case PRIMARY_KEY:
		    return new PrimaryKeyConstraintElement(constraintNode, sqlEntity);
		case FOREIGN_KEY:
		    return new ForeignKeyConstraintElement(constraintNode, sqlEntity);
		case CHECK:
		    return new CheckConstraintElement(constraintNode, sqlEntity);
		default:
		    throw new ArgumentException("The XmlNode argument does not contain a valid constraint type.");
	    }
	}

	public String Type {
	    get { return this.type; }
	    set { this.type = value; }
	}

	public Boolean Clustered {
	    get { return this.clustered; }
	    set { this.clustered = value; }
	}

	public String CheckClause {
	    get { return this.checkClause; }
	    set { this.checkClause = value; }
	}

	public ArrayList Columns {
	    get { return this.columns; }
	    set { this.columns = value; }
	}

	public String Prefix {
	    get { return this.prefix; }
	    set { this.prefix = value; }
	}

	public override void Validate(RootElement root) {

	    // Replace each column placeholder with the column from the sql entity.
	    ArrayList columns = new ArrayList();
	    foreach (ColumnElement column in this.columns) {
		ColumnElement columnElement = this.sqlEntity.FindColumnByName(column.Name);
		if (columnElement == null) {
		    root.AddValidationMessage(ParserValidationMessage.NewError(String.Format("Column specified ({0}) in constraint ({1}) not found as column.", column.Name, this.Name)));		
		} else {
		    columns.Add(columnElement);
		}
	    }
	    this.columns = columns;
	}

//	public static ArrayList ParseFromXml(XmlNode root, SqlEntityElement sqlentity, Hashtable sqltypes, Hashtable types, IParser parser) {
//	    ArrayList constraints = new ArrayList();
//	    XmlNodeList elements=null;
//	    foreach (XmlNode n in root.ChildNodes) {
//		if (n.Name.Equals("constraints")) {
//		    elements = n.ChildNodes;
//		    break;
//		}
//	    }
//	    if (elements != null) {
//		foreach (XmlNode node in elements) {
//		    ConstraintElement constraint = new ConstraintElement();
//		    constraint.Name = node.Attributes["name"].Value;
//		    constraint.Type = node.Attributes["type"].Value;
//
//		    if (node.Attributes["clustered"] != null) {
//			constraint.Clustered = Boolean.Parse(node.Attributes["clustered"].Value);
//		    }
// 		    if (node.Attributes["foreignentity"] != null) {
// 			constraint.ForeignEntity = node.Attributes["foreignentity"].Value;
// 		    }
//		    if (node.Attributes["checkclause"] != null) {
//			constraint.CheckClause = node.Attributes["checkclause"].Value;
//		    }
//		    foreach (XmlNode n in node.ChildNodes) {
//			ColumnElement column = sqlentity.FindColumnByName(n.Attributes["name"].Value);
//			if (column == null) {
//			    parser.AddValidationMessage(ParserValidationMessage.NewError("column specified (" + n.Attributes["name"].Value + ") in constraint (" + constraint.Name + ") not found as column."));
//			    column = new ColumnElement();
//			    column.Name = n.Attributes["name"].Value;
//			}
//			if (n.Attributes["foreigncolumn"] != null) {
//			    column.ForeignColumn = n.Attributes["foreigncolumn"].Value;
//			}
//			constraint.columns.Add(column);
//		    }
//		    constraints.Add(constraint);
//		}
//	    }
//	    return constraints;
//	    return null;
//	}

//	public String ToXml() {
//	    StringBuilder sb = new StringBuilder();
//	    sb.Append("<constraint");
//	    sb.Append(" name=\"").Append(name).Append("\"");
//	    sb.Append(" type=\"").Append(type).Append("\"");
//
//	    if (clustered) {
//		sb.Append(" clustered=\"True\"");
//	    }
//
// 	    if (foreignEntity.Length!=0) {
// 		sb.Append(" foreignentity=\"").Append(foreignEntity).Append("\"");
// 	    }
//	    if (checkClause.Length!=0) {
//		sb.Append(" checkclause=\"").Append(checkClause).Append("\"");
//	    }
//
//	    if (columns.Count>0) {
//		sb.Append(">").Append(Environment.NewLine);
//		foreach (ColumnElement column in columns) {
//		    sb.Append("        ").Append(column.ToXml()).Append(Environment.NewLine);
//		}
//		sb.Append("      </constraint>");
//	    } else {
//		sb.Append(" />");
//	    }
//
//	    return sb.ToString();
//	}

	public ColumnElement FindColumnByName(String name) {
	    foreach (ColumnElement column in columns) {
		if (column.Name.ToLower().Equals(name.ToLower())) {
		    return column;
		}
	    }
	    return null;
	}
    }
}
