using System;
using System.Collections;
using System.Xml;

using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {
    /// <summary>
    /// Summary description for ForeignKeyConstraintElement.
    /// </summary>
    public class ForeignKeyConstraintElement : ConstraintElement {

	private SqlEntityElement foreignEntity = new SqlEntityElement();
	private ArrayList foreignColumns = new ArrayList();

	public ForeignKeyConstraintElement(XmlNode constraintNode, SqlEntityElement sqlEntity) : base(constraintNode, sqlEntity) {
	    foreignEntity.Name = GetAttributeValue(constraintNode, FOREIGN_ENTITY, foreignEntity.Name);
	    foreach (ColumnElement column in this.Columns) {
		this.foreignColumns.Add(new ColumnElement(column.ForeignColumn));
	    }
	}

	public SqlEntityElement ForeignEntity {
	    get { return foreignEntity; }
	    set { foreignEntity = value; }
	}

	public IList ForeignColumns {
	    get { return foreignColumns; }
	}

	public override void Validate(RootElement root) {
	    base.Validate(root);
	    SqlEntityElement foreignEntity = root.FindSqlEntity(this.foreignEntity.Name);
	    if (foreignEntity == null) {
		root.AddValidationMessage(ParserValidationMessage.NewError(String.Format("Foreign entity ({0}) not found for constraint ({1}).", this.foreignEntity.Name, this.Name)));
	    } else {
		this.foreignEntity = foreignEntity;
		ArrayList columns = new ArrayList();
		foreach (ColumnElement column in foreignColumns) {
		    ColumnElement columnElement = this.foreignEntity.FindColumnByName(column.Name);
		    if (columnElement == null) {
			root.AddValidationMessage(ParserValidationMessage.NewError(String.Format("Foreign column ({0}) not found for constraint ({1}).", column.Name, this.Name)));
		    } else {
			columns.Add(columnElement);
		    }
		}
		this.foreignColumns = columns;
	    }
	}
    }
}
