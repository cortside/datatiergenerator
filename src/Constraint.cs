using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator {

    public class Constraint : Spring2.Core.DataObject.DataObject, ICloneable {

	private String name = String.Empty;
	private String type = String.Empty;
	private String foreignEntity = String.Empty;
	private Boolean clustered = false;
	private ArrayList columns = new ArrayList();

	public String Name {
	    get { return this.name; }
	    set { this.name = value; }
	}

	public String Type {
	    get { return this.type; }
	    set { this.type = value; }
	}

	public String ForeignEntity {
	    get { return this.foreignEntity; }
	    set { this.foreignEntity = value; }
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
	    ArrayList constraints = new ArrayList();
	    XmlNodeList elements=null;
	    foreach (XmlNode n in root.ChildNodes) {
		if (n.Name.Equals("constraints")) {
		    elements = n.ChildNodes;
		    break;
		}
	    }
	    if (elements != null) {
		foreach (XmlNode node in elements) {
		    Constraint constraint = new Constraint();
		    constraint.Name = node.Attributes["name"].Value;
		    constraint.Type = node.Attributes["type"].Value;

		    if (node.Attributes["clustered"] != null) {
			constraint.Clustered = Boolean.Parse(node.Attributes["clustered"].Value);
		    }
		    if (node.Attributes["foreignentity"] != null) {
			constraint.ForeignEntity = node.Attributes["foreignentity"].Value;
		    }
		    foreach (XmlNode n in node.ChildNodes) {
			Column column = sqlentity.FindColumnByName(n.Attributes["name"].Value);
			if (column == null) {
			    Console.Out.WriteLine("ERROR: column specified (" + n.Attributes["name"].Value + ") in constraint (" + constraint.Name + ") not found as column.\n");
			    column = new Column();
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

    
    }
}
