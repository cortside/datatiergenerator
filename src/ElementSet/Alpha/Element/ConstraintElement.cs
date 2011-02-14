using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class ConstraintElement : SqlElementSkeleton {

	public static readonly String FOREIGN_KEY = "FOREIGN KEY";
	public static readonly String PRIMARY_KEY = "PRIMARY KEY";

	private static readonly String TYPE = "type";
	private static readonly String CLUSTERED = "clustered";
	private static readonly String FOREIGN_ENTITY = "foreignentity";
	private static readonly String CHECK_CLAUSE = "checkclause";
	private static readonly String PREFIX = "prefix";
	private static readonly String CHECK_ENUM = "checkenum";

	private String type = String.Empty;
	private SqlEntityElement foreignEntity = new SqlEntityElement();
	private Boolean clustered = false;
	private String checkClause = String.Empty;
	private ArrayList columns = new ArrayList();
	private String prefix = String.Empty;
	private EnumElement checkEnum = new EnumElement();

	public String Type {
	    get { return this.type; }
	    set { this.type = value; }
	}

	public SqlEntityElement ForeignEntity {
	    get { return this.foreignEntity; }
	    set { this.foreignEntity = value; }
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

	public EnumElement CheckEnum {
	    get { return checkEnum; }
	    set { checkEnum = value; }
	}

	/// <summary>
	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
	/// list.
	/// </summary>
	/// <param name="node"></param>
	/// <param name="constraintElements"></param>
	public static void ParseFromXml(XmlNode node, IList constraintElements) {

	    if (node != null && constraintElements != null) {

		foreach (XmlNode constraintNode in node.ChildNodes) {
		    if (constraintNode.NodeType.Equals(XmlNodeType.Element)) {
			ConstraintElement constraintElement = new ConstraintElement();

			constraintElement.Name = GetAttributeValue(constraintNode, NAME, constraintElement.Name);
			constraintElement.Type = GetAttributeValue(constraintNode, TYPE, constraintElement.Type);
			constraintElement.Clustered = Boolean.Parse(GetAttributeValue(constraintNode, CLUSTERED, constraintElement.Clustered.ToString()));
			constraintElement.ForeignEntity.Name = GetAttributeValue(constraintNode, FOREIGN_ENTITY, constraintElement.ForeignEntity.Name);
			constraintElement.CheckClause = GetAttributeValue(constraintNode, CHECK_CLAUSE, constraintElement.CheckClause);
			constraintElement.Prefix = GetAttributeValue(constraintNode, PREFIX, constraintElement.Prefix);
			constraintElement.CheckEnum.Name = GetAttributeValue(constraintNode, CHECK_ENUM, constraintElement.CheckEnum.Name);

			ColumnElement.ParseFromXml(constraintNode, constraintElement.Columns);

			constraintElements.Add(constraintElement);
		    }
		}
	    }
	}

	public static ArrayList ParseFromXml(XmlNode root, SqlEntityElement sqlentity, Hashtable sqltypes, Hashtable types, ParserValidationDelegate vd) {
	    ArrayList constraints = new ArrayList();
	    XmlNodeList elements = null;
	    foreach (XmlNode n in root.ChildNodes) {
		if (n.Name.Equals("constraints")) {
		    elements = n.ChildNodes;
		    break;
		}
	    }
	    if (elements != null) {
		foreach (XmlNode node in elements) {
		    if (node.NodeType == XmlNodeType.Comment) {
			continue;
		    }
		    ConstraintElement constraint = new ConstraintElement();
		    constraint.Name = node.Attributes["name"].Value;
		    constraint.Type = node.Attributes["type"].Value;

		    if (node.Attributes["clustered"] != null) {
			constraint.Clustered = Boolean.Parse(node.Attributes["clustered"].Value);
		    }
		    if (node.Attributes["foreignentity"] != null) {
			constraint.ForeignEntity.Name = node.Attributes["foreignentity"].Value;
		    }
		    if (node.Attributes["checkclause"] != null) {
			constraint.CheckClause = node.Attributes["checkclause"].Value;
		    }
		    if (node.Attributes["checkenum"] != null) {
			constraint.CheckEnum.Name = node.Attributes["checkenum"].Value;
		    }
		    foreach (XmlNode n in node.ChildNodes) {
			if (n.NodeType == XmlNodeType.Comment) {
			    continue;
			}
			ColumnElement column = sqlentity.FindColumnByName(n.Attributes["name"].Value);
			if (column == null) {
			    vd(ParserValidationArgs.NewError("column specified (" + n.Attributes["name"].Value + ") in constraint (" + constraint.Name + ") not found as column."));
			    column = new ColumnElement();
			    column.Name = n.Attributes["name"].Value;
			}
			if (n.Attributes["foreigncolumn"] != null) {
			    column.ForeignColumn = n.Attributes["foreigncolumn"].Value;
			}
			constraint.Columns.Add(column);
		    }
		    constraints.Add(constraint);
		}
	    }
	    return constraints;
	}

	public String ToXml() {
	    StringBuilder sb = new StringBuilder();
	    sb.Append("<constraint");
	    sb.Append(" name=\"").Append(name).Append("\"");
	    sb.Append(" type=\"").Append(type).Append("\"");

	    if (clustered) {
		sb.Append(" clustered=\"True\"");
	    }

	    if (foreignEntity.Name.Length != 0) {
		sb.Append(" foreignentity=\"").Append(foreignEntity).Append("\"");
	    }
	    if (checkClause.Length != 0) {
		sb.Append(" checkclause=\"").Append(checkClause).Append("\"");
	    }
	    if (checkEnum.Name.Length != 0) {
		sb.Append(" checkenum=\"").Append(checkEnum).Append("\"");
	    }

	    if (columns.Count > 0) {
		sb.Append(">").Append(Environment.NewLine);
		foreach (ColumnElement column in columns) {
		    sb.Append("        ").Append(column.ToXml()).Append(Environment.NewLine);
		}
		sb.Append("      </constraint>");
	    } else {
		sb.Append(" />");
	    }

	    return sb.ToString();
	}

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
