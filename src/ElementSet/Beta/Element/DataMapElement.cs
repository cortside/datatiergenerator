using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {
 
    /// <summary>
    /// Summary description for SqlEntityMapElement.
    /// </summary>
    public class DataMapElement : ElementSkeleton {

	private EntityElement entity = null;
	private SqlEntityElement sqlEntity = new SqlEntityElement();
	private IList columnMaps = new ArrayList();

	public DataMapElement(XmlNode entityNode, String sqlEntityName, EntityElement entity) : base(entityNode) {
	    this.entity = entity;
	    if (EntityElement.ENTITY.Equals(entityNode.Name)) {
		this.sqlEntity.Name = sqlEntityName;
		foreach (XmlNode node in GetChildNodes(entityNode, EntityElement.PROPERTIES, PropertyElement.PROPERTY)) {
		    String columnName = GetAttributeValue(node, PropertyElement.COLUMN, String.Empty);
		    columnName = columnName.Equals("*") ? GetAttributeValue(node, NAME, String.Empty) : columnName;
		    if (!String.Empty.Equals(columnName)) {
			columnMaps.Add(new ColumnMapElement(node, columnName, this));
		    }
		}
	    } else {
		throw new ArgumentException("The XmlNode argument is not an entity node.");
	    }
	}


	public override void Validate(RootElement root) {
	    // Find the sql entity.
	    SqlEntityElement sqlEntity = root.FindSqlEntity(this.sqlEntity.Name);
	    if (sqlEntity == null) {
		root.AddValidationMessage(ParserValidationMessage.NewError(String.Format("Sql Entity ({0}) not found.", this.sqlEntity.Name)));
	    } else {
		this.sqlEntity = sqlEntity;

		// Validate each of the column map entries.
		foreach (ColumnMapElement columnMap in this.columnMaps) {
		    columnMap.Validate(root);
		}
	    }
	}

	public EntityElement Entity {
	    get { return entity; }
	}

	public SqlEntityElement SqlEntity {
	    get { return sqlEntity; }
	}

	public IList ColumnMaps {
	    get { return columnMaps; }
	}

	public override String ToString() {
	    StringBuilder buffer = new StringBuilder("DATA MAP\n");
	    foreach (ColumnMapElement columnMap in columnMaps) {
		buffer.Append(columnMap.ToString());
	    }
	    return buffer.ToString();
	}
    }
}
