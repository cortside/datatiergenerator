using System;

namespace Spring2.DataTierGenerator {
    public class FieldData : Spring2.Core.DataObject.DataObject {

	protected String name = String.Empty;
	protected String sqlName = String.Empty;
	protected SqlType sqlType = new SqlType();
	protected Type type = new Type();
	protected Boolean isRowGuidCol = false;
	protected Boolean isIdentity = false;
	protected Boolean isPrimaryKey = false;
	protected Boolean isForeignKey = false;
	protected Boolean isViewColumn = false;
	protected String accessModifier = "public";
	protected String description = String.Empty;

	public String Name {
	    get { return this.name; }
	    set { this.name = value; }
	}

	public String SqlName {
	    get { return this.sqlName; }
	    set { this.sqlName = value; }
	}

	public SqlType SqlType {
	    get { return this.sqlType; }
	    set { this.sqlType = value; }
	}

	public Type Type {
	    get { return this.type; }
	    set { this.type = value; }
	}

	public Boolean IsRowGuidCol {
	    get { return this.isRowGuidCol; }
	    set { this.isRowGuidCol = value; }
	}

	public Boolean IsIdentity {
	    get { return this.isIdentity; }
	    set { this.isIdentity = value; }
	}

	public Boolean IsPrimaryKey {
	    get { return this.isPrimaryKey; }
	    set { this.isPrimaryKey = value; }
	}

	public Boolean IsForeignKey {
	    get { return this.isForeignKey; }
	    set { this.isForeignKey = value; }
	}

	public Boolean IsViewColumn {
	    get { return this.isViewColumn; }
	    set { this.isViewColumn = value; }
	}

	public String AccessModifier {
	    get { return this.accessModifier; }
	    set { this.accessModifier = value; }
	}

	public String Description {
	    get { return this.description; }
	    set { this.description = value; }
	}

    }
}
