using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class IndexElement : SqlElementSkeleton {

	private static readonly String UNIQUE = "unique";
	private static readonly String CLUSTERED = "clustered";

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

	/// <summary>
	/// Parse only method. Parses and adds all entities found in the given node and adds them to the given
	/// list.
	/// </summary>
	/// <param name="node"></param>
	/// <param name="indexElements"></param>
	public static void ParseFromXml(XmlNode node, IList indexElements) {

	    if (node != null && indexElements != null) {

		foreach (XmlNode indexNode in node.ChildNodes) {
		    if (indexNode.NodeType.Equals(XmlNodeType.Element)) {
			IndexElement indexElement = new IndexElement();

			indexElement.Name = GetAttributeValue(indexNode, NAME, indexElement.Name);
			indexElement.Unique = Boolean.Parse(GetAttributeValue(indexNode, UNIQUE, indexElement.Unique.ToString()));
			indexElement.Clustered = Boolean.Parse(GetAttributeValue(indexNode, CLUSTERED, indexElement.Clustered.ToString()));

			ColumnElement.ParseFromXml(indexNode, indexElement.Columns);
		
			indexElements.Add(indexElement);
		    }
		}
	    }
	}

	public static ArrayList ParseFromXml(XmlNode root, SqlEntityElement sqlentity, Hashtable sqltypes, Hashtable types, ParserValidationDelegate vd) {
	    ArrayList indexes = new ArrayList();
	    XmlNodeList elements=null;
	    foreach (XmlNode n in root.ChildNodes) {
		if (n.Name.Equals("indexes")) {
		    elements = n.ChildNodes;
		    break;
		}
	    }
	    if (elements != null) {
		foreach (XmlNode node in elements) {
		    IndexElement index = new IndexElement();
		    index.Name = node.Attributes["name"].Value;

		    if (node.Attributes["clustered"] != null) {
			index.Clustered = Boolean.Parse(node.Attributes["clustered"].Value);
		    }
		    if (node.Attributes["unique"] != null) {
			index.Unique = Boolean.Parse(node.Attributes["unique"].Value);
		    }

		    foreach (XmlNode n in node.ChildNodes) {
			ColumnElement column = sqlentity.FindColumnByName(n.Attributes["name"].Value);
			if (column == null) {
			    vd(ParserValidationArgs.NewError("column specified (" + n.Attributes["name"].Value + ") in index (" + index.Name + ") not found as column."));
			    column = new ColumnElement();
			    column.Name = n.Attributes["name"].Value;
			}
			if (n.Attributes["sortdirection"] != null) {
			    column.SortDirection = n.Attributes["sortdirection"].Value;
			}
			index.Columns.Add(column);
		    }
		    indexes.Add(index);
		}
	    }
	    return indexes;
	}

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
