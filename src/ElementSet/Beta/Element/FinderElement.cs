using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class FinderElement : ElementSkeleton {

	public static readonly String FINDER = "finder";
	public static readonly String PROPERTIES = "properties";
	public static readonly String SORT = "sort";
	public static readonly String UNIQUE = "unique";

	private EntityElement entity = null;
	private String sort = String.Empty;
	private Boolean unique = false;
//	private ArrayList properties = new ArrayList();
	private ArrayList columnMaps = new ArrayList();

	public String Sort {
	    get { return this.sort; }
	    set { this.sort = value; }
	}

	public Boolean Unique {
	    get { return this.unique; }
	    set { this.unique = value; }
	}

//	public ArrayList Fields {
//	    get { return this.properties; }
//	    set { this.properties = value; }
//	}

	public ArrayList ColumnMaps {
	    get { return this.columnMaps; }
	}

	public ArrayList Properties {
	    get {
		ArrayList properties = new ArrayList();
		foreach (ColumnMapElement columnMap in this.columnMaps) {
		    properties.Add(columnMap.Property);
		}
		return properties;
	    }
	}

	public FinderElement() {}

	public FinderElement(XmlNode finderNode, EntityElement entity) : base(finderNode) {
	    if (FINDER.Equals(finderNode.Name)) {
		this.entity = entity;
		sort = GetAttributeValue(finderNode, SORT, sort);
		unique = Boolean.Parse(GetAttributeValue(finderNode, UNIQUE, unique.ToString()));

		foreach (XmlNode node in GetChildNodes(finderNode, PROPERTIES, PropertyElement.PROPERTY)) {
//		    columnMaps.Add(new PropertyElement(node, entity));
		    columnMaps.Add(new ColumnMapElement(node));
		}
	    } else {
		throw new ArgumentException("The XmlNode argument is not a finder node.");
	    }
	}

	public override void Validate(RootElement root) {

	    String sort = String.Empty;
	    IList columnMaps = new ArrayList();
	    foreach (ColumnMapElement columnMap in this.ColumnMaps) {
		ColumnMapElement columnMapElement = this.entity.FindColumnMap(columnMap.Name);
		if (columnMapElement == null) {
		    root.AddValidationMessage(ParserValidationMessage.NewError("Finder column element (" + columnMap.Name + ") was not found for entity (" + this.entity.Name + ")"));
		} else {
		    columnMaps.Add(columnMap);
		    if (!this.sort.Equals(String.Empty)) {
			sort += ", ";
		    }
		    sort += columnMap.Column.Name;
		}
	    }
	    if (this.sort.Equals(String.Empty)) {
		this.sort = sort;
	    }
	}

//	public static ArrayList ParseFromXml(XmlNode root, EntityElement entity, IParser parser) {
//	    ArrayList finders = new ArrayList();
////	    XmlNodeList elements=null;
////	    foreach (XmlNode n in root.ChildNodes) {
////		if (n.Name.Equals("finders")) {
////		    elements = n.ChildNodes;
////		    break;
////		}
////	    }
////	    if (elements != null) {
////		foreach (XmlNode node in elements) {
////		    FinderElement finder = new FinderElement();
////		    finder.Name = node.Attributes["name"].Value;
////		    if (node.Attributes["sort"] != null) {
////			finder.Sort = node.Attributes["sort"].Value;
////		    }
////		    if (node.Attributes["unique"] != null) {
////			finder.Unique = Boolean.Parse(node.Attributes["unique"].Value);
////		    }
////		    String sort = String.Empty;
////		    foreach (XmlNode n in node.ChildNodes[0].ChildNodes) {
////			PropertyElement field = entity.FindFieldByName(n.Attributes["name"].Value);
////			if (field != null) {
////			    field = (PropertyElement)field.Clone();
////			    if (!sort.Equals(String.Empty)) {
////				sort += ", ";
////			    }
////			    sort += field.Column.Name;
////			} else {
////			    field = new PropertyElement();
////			    field.Name = n.Attributes["name"].Value;
////			    parser.AddValidationMessage(ParserValidationMessage.NewError("property (" + field.Name + ") defined in finder method " + finder.Name + " was not defined as a property"));
////			}
////			finder.Fields.Add(field.Clone());
////		    }
////		    if (finder.Sort.Equals(String.Empty) && !sort.Equals(String.Empty)) {
////			finder.Sort = sort;
////		    }
////
////		    finders.Add(finder);
////		}
////	    }
//	    return finders;
//	}
    }
}
