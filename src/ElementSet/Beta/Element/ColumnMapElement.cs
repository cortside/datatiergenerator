using System;
//using System.Drawing;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {
 
    /// <summary>
    /// Summary description for ColumnMapElement.
    /// </summary>
    public class ColumnMapElement : ElementSkeleton {

	private DataMapElement dataMap = null;
	private PropertyElement property = new PropertyElement();
	private ColumnElement column = new ColumnElement();

	public ColumnMapElement(XmlNode propertyNode, String columnName, DataMapElement dataMap) : base(propertyNode) {
	    if (PropertyElement.PROPERTY.Equals(propertyNode.Name)) {
		this.dataMap = dataMap;
		this.column.Name = columnName;
	    } else {
		throw new ArgumentException("The XmlNode argument is not a property node.");
	    }
	}

	public ColumnMapElement(XmlNode propertyNode) : base(propertyNode) {
	    if (!PropertyElement.PROPERTY.Equals(propertyNode.Name)) {
		throw new ArgumentException("The XmlNode argument is not a property node.");
	    }
	}

	public PropertyElement Property {
	    get { return property; }
	}

	public ColumnElement Column {
	    get { return column; }
	}

	public override void Validate(IParser parser) {
	    // Look up the property from the property path.
	    PropertyElement propertyElement = this.dataMap.Entity.FindProperty(this.Name);
	    if (propertyElement == null) {
		parser.AddValidationMessage(ParserValidationMessage.NewError(String.Format("ColumnMap: property ({0}) not found.", this.Name)));
	    } else {
		this.property = propertyElement;
	    }

	    // Look up the column in the sql entity.
	    ColumnElement columnElement = this.dataMap.SqlEntity.FindColumnByName(this.column.Name);
	    if (columnElement == null) {
		parser.AddValidationMessage(ParserValidationMessage.NewError(String.Format("ColumnMap: column ({0}) not found.", this.column.Name)));		
	    } else {
		this.column = columnElement;

		// Make sure the column and property types are compatible.
//		if (!this.property.Type.Equals(String.Empty) && !columnElement.SqlType.Type.Equals(this.property.Type)) {
//		    this.AddValidationMessage(ParserValidationMessage.NewError(String.Format("The type of column ({0}) is ({1}) and is not compatible with the type ({2}) of property ({3}).", this.column.Name, this.column.SqlType.Type, this.Property.Type, this.property.Name)));
//		}
	    }
	}

	public String CreateSqlParameter(Boolean output, Boolean useDataObject) {

	    StringBuilder sb = new StringBuilder();
	    sb.Append("cmd.Parameters.Add(new SqlParameter(\"@" + column.Name + "\", SqlDbType." + column.SqlType.SqlDbType + ", " + column.Length + ", ParameterDirection.");
	    if (output) {
		sb.Append("Output"); 
	    } else {
		sb.Append("Input");
	    }
	    sb.Append(", false, " + column.Precision + ", " + column.Scale + ", \"" + name + "\", DataRowVersion.Proposed, ");
	    if (useDataObject) {
		sb.Append(String.Format(property.Type.ConvertToSqlTypeFormat, "data", "data." + Name, "", "", property.GetMethodFormat()));
	    } else {
		sb.Append(String.Format(property.Type.ConvertToSqlTypeFormat, "", property.GetFieldFormat(), "", "", property.GetFieldFormat()));
	    }
	    sb.Append("));" + Environment.NewLine);
	    return sb.ToString();
	}
    }
}
