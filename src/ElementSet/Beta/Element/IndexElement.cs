using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator.Element {

    public class IndexElement : SqlElementSkeleton {

	public static readonly String INDEX = "index";
	private static readonly String UNIQUE = "unique";
	private static readonly String CLUSTERED = "clustered";

	private SqlEntityElement sqlEntity = null;
	private Boolean unique = false;
	private Boolean clustered = false;
	private ArrayList columns = new ArrayList();

	public Boolean Unique {
	    get { return this.unique; }
	    set { this.unique = value; }
	}

	public Boolean Clustered {
	    get { return this.clustered; }
	    set { this.clustered = value; }
	}

	public ArrayList Columns {
	    get { return this.columns; }
	    set { this.columns = value; }
	}

	public IndexElement() {}

	public IndexElement(XmlNode indexNode, SqlEntityElement sqlEntity) {

	    this.sqlEntity = sqlEntity;
	    if (indexNode != null && INDEX.Equals(indexNode.Name)) {
		name = GetAttributeValue(indexNode, NAME, name);
		unique = Boolean.Parse(GetAttributeValue(indexNode, UNIQUE, unique.ToString()));
		clustered = Boolean.Parse(GetAttributeValue(indexNode, CLUSTERED, clustered.ToString()));

		foreach (XmlNode node in GetChildNodes(indexNode, ColumnElement.COLUMN)) {
		    columns.Add(new ColumnElement(node, null));
		}
	    } else {
		throw new ArgumentException("The XmlNode argument is not an index node.");
	    }
	}

	public override void Validate(RootElement root) {

	    ArrayList columns = new ArrayList();
	    foreach (ColumnElement column in this.columns) {
		ColumnElement columnElement = this.sqlEntity.FindColumnByName(column.Name);
		if (columnElement == null) {
		} else {
		    columns.Add(columnElement);
		}
	    }
	    this.columns = columns;
	}
  
//	public static ArrayList ParseFromXml(XmlNode root, SqlEntityElement sqlentity, Hashtable sqltypes, Hashtable types, IParser parser) {
//	    ArrayList indexes = new ArrayList();
//	    XmlNodeList elements=null;
//	    foreach (XmlNode n in root.ChildNodes) {
//		if (n.Name.Equals("indexes")) {
//		    elements = n.ChildNodes;
//		    break;
//		}
//	    }
//	    if (elements != null) {
//		foreach (XmlNode node in elements) {
//		    IndexElement index = new IndexElement();
//		    index.Name = node.Attributes["name"].Value;
//
//		    if (node.Attributes["clustered"] != null) {
//			index.Clustered = Boolean.Parse(node.Attributes["clustered"].Value);
//		    }
//		    if (node.Attributes["unique"] != null) {
//			index.Unique = Boolean.Parse(node.Attributes["unique"].Value);
//		    }
//
//		    foreach (XmlNode n in node.ChildNodes) {
//			ColumnElement column = sqlentity.FindColumnByName(n.Attributes["name"].Value);
//			if (column == null) {
//			    parser.AddValidationMessage(ParserValidationMessage.NewError("column specified (" + n.Attributes["name"].Value + ") in index (" + index.Name + ") not found as column."));
//			    column = new ColumnElement();
//			    column.Name = n.Attributes["name"].Value;
//			}
//			if (n.Attributes["sortdirection"] != null) {
//			    column.SortDirection = n.Attributes["sortdirection"].Value;
//			}
//			index.Columns.Add(column);
//		    }
//		    indexes.Add(index);
//		}
//	    }
//	    return indexes;
//	}

	public String ToXml() {
	    StringBuilder sb = new StringBuilder();
	    sb.Append("<index");
	    sb.Append(" name=\"").Append(name).Append("\"");

	    if (unique) {
		sb.Append(" unique=\"True\"");
	    }
	    if (clustered) {
		sb.Append(" clustered=\"True\"");
	    }
		sb.Append(">").Append(Environment.NewLine);
		foreach (ColumnElement column in columns) {
		    sb.Append("        ").Append(column.ToXml()).Append(Environment.NewLine);
		}
		sb.Append("      </index>");

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
