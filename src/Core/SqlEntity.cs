using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator {

    public class SqlEntity : Spring2.Core.DataObject.DataObject, ICloneable {

	private String name = String.Empty;
	private String description = String.Empty;
	private String view = String.Empty;
	private ArrayList columns = new ArrayList();
	private ArrayList constraints = new ArrayList();
	private ArrayList indexes = new ArrayList();

	public String Name {
	    get { return this.name; }
	    set { this.name = value; }
	}

	public String Description {
	    get { return this.description; }
	    set { this.description = value; }
	}

	public String View {
	    get { return this.view; }
	    set { this.view = value; }
	}

	public ArrayList Columns {
	    get { return this.columns; }
	    set { this.columns = value; }
	}

	public ArrayList Constraints {
	    get { return this.constraints; }
	    set { this.constraints = value; }
	}

	public ArrayList Indexes {
	    get { return this.indexes; }
	    set { this.indexes = value; }
	}

	public static SqlEntity FindByName(ArrayList sqlentities, String name) {
	    foreach (SqlEntity sqlentity in sqlentities) {
		if (sqlentity.Name.Equals(name)) {
		    return sqlentity;
		}
	    }
	    return null;
	}

	public Column FindColumnByName(String name) {
	    foreach (Column column in columns) {
		if (column.Name.Equals(name)) {
		    return column;
		}
	    }
	    return null;
	}

	public static ArrayList ParseFromXml(Configuration options, XmlDocument doc, Hashtable sqltypes, Hashtable types) {
	    ArrayList sqlentities = new ArrayList();
	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("sqlentity");
	    foreach (XmlNode node in elements) {
		SqlEntity sqlentity = new SqlEntity();
		sqlentity.Name = node.Attributes["name"].Value;
		sqlentity.View = "vw" + sqlentity.Name;
		sqlentity.Description = node.InnerText;
		if (node.Attributes["view"] != null) {
		    sqlentity.View = node.Attributes["view"].Value;
		}
		sqlentity.Columns = Column.ParseFromXml(node, sqlentity, sqltypes, types);
		sqlentity.Constraints = Constraint.ParseFromXml(node, sqlentity, sqltypes, types);
		sqlentity.Indexes = Index.ParseFromXml(node, sqlentity, sqltypes, types);
		sqlentities.Add(sqlentity);
	    }
	    return sqlentities;
	}

	public Boolean HasUpdatableColumns() {
	    Boolean has = false;
	    foreach (Column column in columns) {
		if (!column.Identity && !column.RowGuidCol && !IsPrimaryKeyColumn(column.Name)) {
		    has=true;	
		}
	    }
	    return has;
	}

	public Boolean IsPrimaryKeyColumn(String name) {
	    foreach (Constraint constraint in constraints) {
		if (constraint.Type.ToUpper().Equals("PRIMARY KEY")) {
		    foreach (Column column in constraint.Columns) {
			if (column.Name.Equals(name)) {
			    return true;
			}
		    }
		}
	    }
	    return false;
	}

	public Boolean IsForeignKeyColumn(String name) {
	    foreach (Constraint constraint in constraints) {
		if (constraint.Type.ToUpper().Equals("FOREIGN KEY")) {
		    foreach (Column column in constraint.Columns) {
			if (column.Name.Equals(name)) {
			    return true;
			}
		    }
		}
	    }
	    return false;
	}

	public IList GetPrimaryKeyColumns() {
	    ArrayList list = new ArrayList();
	    Column id = GetIdentityColumn();
	    if (id != null) {
		list.Add(id);
	    } else {
		foreach (Constraint constraint in constraints) {
		    if (constraint.Type.ToUpper().Equals("PRIMARY KEY")) {
			list.AddRange(constraint.Columns);
		    }
		}
	    }
	    return list;
	}

	public Column GetIdentityColumn() {
	    foreach (Column column in columns) {
		if (column.Identity) {
		    return column;
		}
	    }
	    return null;
	}

//	public Field FindFieldBySqlName(String name) {
//	    foreach (Field field in fields) {
//		if (field.SqlName == name) {
//		    return field;
//		}
//	    }
//	    return null;
//	}
//
//	public Field FindFieldByName(String name) {
//	    foreach (Field field in fields) {
//		if (field.Name == name) {
//		    return field;
//		}
//	    }
//	    return null;
//	}

	public Object Clone() {
	    return MemberwiseClone();
	}


    }
}
