using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator {

    public class Finder : Spring2.Core.DataObject.DataObject {
	private String name = String.Empty;
	private String sort = String.Empty;
	private Boolean unique = false;
	private ArrayList fields = new ArrayList();

	public String Name {
	    get { return this.name; }
	    set { this.name = value; }
	}

	public String Sort {
	    get { return this.sort; }
	    set { this.sort = value; }
	}

	public Boolean Unique {
	    get { return this.unique; }
	    set { this.unique = value; }
	}

	public ArrayList Fields {
	    get { return this.fields; }
	    set { this.fields = value; }
	}

	public static ArrayList ParseFromXml(XmlNode root, Entity entity) {
	    ArrayList finders = new ArrayList();
	    XmlNodeList elements=null;
	    foreach (XmlNode n in root.ChildNodes) {
		if (n.Name.Equals("finders")) {
		    elements = n.ChildNodes;
		    break;
		}
	    }
	    if (elements != null) {
		foreach (XmlNode node in elements) {
		    Finder finder = new Finder();
		    finder.Name = node.Attributes["name"].Value;
		    if (node.Attributes["sort"] != null) {
			finder.Sort = node.Attributes["sort"].Value;
		    }
		    if (node.Attributes["unique"] != null) {
			finder.Unique = Boolean.Parse(node.Attributes["unique"].Value);
		    }
		    String sort = String.Empty;
		    foreach (XmlNode n in node.ChildNodes[0].ChildNodes) {
			Field field = (Field)entity.FindFieldByName(n.Attributes["name"].Value).Clone();
			if (!sort.Equals(String.Empty)) {
			    sort += ", ";
			}
			sort += field.SqlName;
			finder.Fields.Add(entity.FindFieldByName(n.Attributes["name"].Value).Clone());
		    }
		    if (finder.Sort.Equals(String.Empty) && !sort.Equals(String.Empty)) {
			finder.Sort = sort;
		    }

		    finders.Add(finder);
		}
	    }
	    return finders;
	}


    }
}
