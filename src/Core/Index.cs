using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator.Core {

    public class Index : Spring2.Core.DataObject.DataObject, ICloneable {

	private String name = String.Empty;
	private Boolean unique = false;
	private Boolean clustered = false;
	private ArrayList columns = new ArrayList();

	public String Name {
	    get { return this.name; }
	    set { this.name = value; }
	}

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

	public Object Clone() {
	    return MemberwiseClone();
	}

	public static ArrayList ParseFromXml(XmlNode root, SqlEntity sqlentity, Hashtable sqltypes, Hashtable types) {
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
		    Index index = new Index();
		    index.Name = node.Attributes["name"].Value;

		    if (node.Attributes["clustered"] != null) {
			index.Clustered = Boolean.Parse(node.Attributes["clustered"].Value);
		    }
		    if (node.Attributes["unique"] != null) {
			index.Unique = Boolean.Parse(node.Attributes["unique"].Value);
		    }

		    foreach (XmlNode n in node.ChildNodes) {
			Column column = sqlentity.FindColumnByName(n.Attributes["name"].Value);
			if (column == null) {
			    Console.Out.WriteLine("ERROR: column specified (" + n.Attributes["name"].Value + ") in index (" + index.Name + ") not found as column.\n");
			    column = new Column();
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
		sb.Append(">\n");
		foreach (Column column in columns) {
		    sb.Append("        ").Append(column.ToXml()).Append("\n");
		}
		sb.Append("      </index>");

	    return sb.ToString();
	}

    
    }
}
