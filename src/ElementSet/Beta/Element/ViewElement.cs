using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator.Element {

    public class ViewElement : SqlElementSkeleton  {
    
	public static readonly String VIEW = "view";

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

	public ViewElement() {}

	public ViewElement(XmlNode viewNode) {

	    if (viewNode != null && VIEW.Equals(viewNode.Name)) {
		name = GetAttributeValue(viewNode, NAME, name);

		foreach (XmlNode node in GetChildNodes(viewNode, ConstraintElement.CONSTRAINT)) {
		    constraints.Add(new ConstraintElement(node));
		}
	    } else {
		throw new ArgumentException("The XmlNode argument is not a view node.");
	    }
	}

	public override void Validate(RootElement root) {
//	    // Find all the constraints in the sql entity.
//	    // this.Constraints;
//	    // Verify that the type of the constraint is a foreign key constraint.
//	    // Do prefix and foreign entity stuff.
//	    // View columns
//	    ViewElement view = new ViewElement();
//
//	    foreach (XmlNode n in node.ChildNodes) {
//		ConstraintElement constraint = sqlentity.FindConstraintByName(n.Attributes["name"].Value);
//		if (constraint == null) {
//		    vd(ParserValidationMessage.NewError("constraint specified (" + n.Attributes["name"].Value + ") in view (" + view.Name + ") not found."));
//		    constraint = new ConstraintElement();
//		    constraint.Name = n.Attributes["name"].Value;
//		} else {
//		    if (!constraint.Type.Equals(ConstraintElement.FOREIGN_KEY)) {
//			vd(ParserValidationMessage.NewError("View [" + view.Name + "] references a constraint that is not a foreign key constraint: " + constraint.Name));
//		    }
//		}
//
//		constraint.Prefix = ParseStringAttribute(n, "prefix", constraint.ForeignEntity + "_");
//		SqlEntityElement foreignEntity = database.FindSqlEntityByName(constraint.ForeignEntity);
//		if (foreignEntity == null) {
//		    vd(ParserValidationMessage.NewError("View [" + view.Name + "] references a constraint that references an sql entity that was not defined (or was not defined before this sql entity): " + constraint.ForeignEntity));
//		} else {
//		    foreach(ColumnElement column in foreignEntity.Columns) {
//			ColumnElement viewColumn = (ColumnElement)column.Clone();
//			if (!constraint.Prefix.Equals(String.Empty)) {
//			    viewColumn.Name = constraint.Prefix + viewColumn.Name;
//			}
//			viewColumn.Prefix = constraint.Prefix;
//			viewColumn.ForeignSqlEntity = constraint.ForeignEntity;
//			viewColumn.ViewColumn = true;
//			sqlentity.Columns.Add(viewColumn);
//		    }
//		}
//		view.Constraints.Add(constraint);
//	    }
	}

//	public static ArrayList ParseFromXml(XmlNode root, DatabaseElement database, SqlEntityElement sqlentity, Hashtable sqltypes, Hashtable types, IParser parser) {
//	    ArrayList list = new ArrayList();
//
//	    XmlNodeList elements=root.SelectNodes("views/view");
//	    if (elements != null) {
//		foreach (XmlNode node in elements) {
//		    ViewElement view = new ViewElement();
//		    view.Name = ParseStringAttribute(node, "name", String.Empty);
//
//		    foreach (XmlNode n in node.ChildNodes) {
//			ConstraintElement constraint = sqlentity.FindConstraintByName(n.Attributes["name"].Value);
//			if (constraint == null) {
//			    parser.AddValidationMessage(ParserValidationMessage.NewError("constraint specified (" + n.Attributes["name"].Value + ") in view (" + view.Name + ") not found."));
//			    constraint = new ConstraintElement();
//			    constraint.Name = n.Attributes["name"].Value;
//			} else {
//			    if (!constraint.Type.Equals(ConstraintElement.FOREIGN_KEY)) {
//				parser.AddValidationMessage(ParserValidationMessage.NewError("View [" + view.Name + "] references a constraint that is not a foreign key constraint: " + constraint.Name));
//			    }
//			}
////			constraint.Prefix = ParseStringAttribute(n, "prefix", constraint.ForeignEntity + "_");
////			SqlEntityElement foreignEntity = database.FindSqlEntityByName(constraint.ForeignEntity);
////			if (foreignEntity == null) {
////			    parser.AddValidationMessage(ParserValidationMessage.NewError("View [" + view.Name + "] references a constraint that references an sql entity that was not defined (or was not defined before this sql entity): " + constraint.ForeignEntity));
////			} else {
////			    foreach(ColumnElement column in foreignEntity.Columns) {
////				ColumnElement viewColumn = (ColumnElement)column.Clone();
////				if (!constraint.Prefix.Equals(String.Empty)) {
////				    viewColumn.Name = constraint.Prefix + viewColumn.Name;
////				}
////				viewColumn.Prefix = constraint.Prefix;
////				viewColumn.ForeignSqlEntity = constraint.ForeignEntity;
////				viewColumn.ViewColumn = true;
////				sqlentity.Columns.Add(viewColumn);
////			    }
////			}
//			view.Constraints.Add(constraint);
//		    }
//
//		    // validation
//		    if (view.Name.Equals(String.Empty)) {
//			parser.AddValidationMessage(ParserValidationMessage.NewError("View does not have a name:\n " + node.OuterXml));
//		    }
//
//		    list.Add(view);
//		}
//	    }
//	    return list;
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
