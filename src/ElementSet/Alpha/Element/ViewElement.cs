using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class ViewElement : SqlElementSkeleton {

	private ArrayList constraints = new ArrayList();
	private ArrayList columns = new ArrayList();

	public ArrayList Constraints {
	    get { return this.constraints; }
	    set { this.constraints = value; }
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
	/// <param name="viewElements"></param>
	public static void ParseFromXml(XmlNode node, IList viewElements) {

	    if (node != null && viewElements != null) {

		foreach (XmlNode viewNode in node.ChildNodes) {
		    if (viewNode.NodeType.Equals(XmlNodeType.Element)) {
			ViewElement viewElement = new ViewElement();

			viewElement.Name = GetAttributeValue(viewNode, NAME, viewElement.Name);
			ConstraintElement.ParseFromXml(viewNode, viewElement.Constraints);
		
			viewElements.Add(viewElement);
		    }
		}
	    }
	}

	public static ArrayList ParseFromXml(XmlNode root, DatabaseElement database, SqlEntityElement sqlentity, Hashtable sqltypes, Hashtable types, ParserValidationDelegate vd) {
	    ArrayList list = new ArrayList();

	    XmlNodeList elements=root.SelectNodes("views/view");
	    if (elements != null) {
		foreach (XmlNode node in elements) {
		    ViewElement view = new ViewElement();
		    view.Name = ParseStringAttribute(node, "name", String.Empty);

		    foreach (XmlNode n in node.ChildNodes) {
			ConstraintElement constraint = sqlentity.FindConstraintByName(n.Attributes["name"].Value);
			if (constraint == null) {
			    vd(ParserValidationArgs.NewError("constraint specified (" + n.Attributes["name"].Value + ") in view (" + view.Name + ") not found."));
			    constraint = new ConstraintElement();
			    constraint.Name = n.Attributes["name"].Value;
			} else {
			    if (!constraint.Type.Equals(ConstraintElement.FOREIGN_KEY)) {
				vd(ParserValidationArgs.NewError("View [" + view.Name + "] references a constraint that is not a foreign key constraint: " + constraint.Name));
			    }
			}
			constraint.Prefix = ParseStringAttribute(n, "prefix", constraint.ForeignEntity + "_");
			SqlEntityElement foreignEntity = database.FindSqlEntityByName(constraint.ForeignEntity);
			if (foreignEntity == null) {
			    vd(ParserValidationArgs.NewError("View [" + view.Name + "] references a constraint that references an sql entity that was not defined (or was not defined before this sql entity): " + constraint.ForeignEntity));
			} else {
			    foreach(ColumnElement column in foreignEntity.Columns) {
				ColumnElement viewColumn = (ColumnElement)column.Clone();
				if (!constraint.Prefix.Equals(String.Empty)) {
				    viewColumn.Name = constraint.Prefix + viewColumn.Name;
				}
				viewColumn.Prefix = constraint.Prefix;
				viewColumn.ForeignSqlEntity = constraint.ForeignEntity;
				viewColumn.ViewColumn = true;
				sqlentity.Columns.Add(viewColumn);
			    }
			}
			view.Constraints.Add(constraint);
		    }

		    // validation
		    if (view.Name.Equals(String.Empty)) {
			vd(ParserValidationArgs.NewError("View does not have a name: " + Environment.NewLine + node.OuterXml));
		    }

		    list.Add(view);
		}
	    }
	    return list;
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
