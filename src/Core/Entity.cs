using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator.Core {
    public class Entity : Spring2.Core.DataObject.DataObject, ICloneable {

	private String name = String.Empty;
	private SqlEntity sqlEntity = new SqlEntity();
	private ArrayList fields = new ArrayList();
	private ArrayList finders = new ArrayList();

	public String Name {
	    get { return this.name; }
	    set { this.name = value; }
	}

	public SqlEntity SqlEntity {
	    get { return this.sqlEntity; }
	    set { this.sqlEntity = value; }
	}

	public ArrayList Fields {
	    get { return this.fields; }
	    set { this.fields = value; }
	}

	public ArrayList Finders {
	    get { return this.finders; }
	    set { this.finders = value; }
	}

	public String ToXml() {
	    StringBuilder sb = new StringBuilder();
	    sb.Append("<entity name=\"").Append(name).Append("\"");
	    sb.Append(" sqlentity.name=\"").Append(sqlEntity.Name).Append("\"");
	    sb.Append(" sqlentity.view=\"").Append(sqlEntity.View).Append("\"");
	    sb.Append(" />");

	    return sb.ToString();
	}

	public static Entity FindEntityBySqlEntity(ArrayList entities, String name) {
	    foreach (Entity entity in entities) {
		if (entity.SqlEntity.Name.Equals(name)) {
		    return entity;
		}
	    }
	    return null;
	}

	public static Entity FindEntityByName(ArrayList entities, String name) {
	    foreach (Entity entity in entities) {
		if (entity.Name.Equals(name)) {
		    return entity;
		}
	    }
	    return null;
	}

	public static ArrayList ParseFromXml(Configuration options, XmlDocument doc, Hashtable sqltypes, Hashtable types, ArrayList sqlentities) {
	    ArrayList entities = new ArrayList();
	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("entity");
	    foreach (XmlNode node in elements) {
		Entity entity = new Entity();
		entity.Name = node.Attributes["name"].Value;
		if (node.Attributes["sqlentity"] != null) {
		    SqlEntity sqlentity = SqlEntity.FindByName(sqlentities, node.Attributes["sqlentity"].Value);
		    if (sqlentity!=null) {
			entity.SqlEntity = (SqlEntity)sqlentity.Clone();
		    } else {
			entity.SqlEntity.Name = node.Attributes["sqlentity"].Value;
			Console.Out.WriteLine("ERROR: sqlentity (" + entity.SqlEntity.Name + ") specified in entity " + entity.Name + " could not be found as an defined sql entity");
		    }
		}
		entity.Fields = Field.ParseFromXml(doc, entity, sqltypes, types);
		entity.Finders = Finder.ParseFromXml(node, entity);
		entities.Add(entity);
	    }
	    return entities;
	}

//	public Boolean HasUpdatableFields() {
//	    Boolean has = false;
//	    foreach (Field field in fields) {
//		if (!field.IsIdentity && !field.IsPrimaryKey && !field.IsRowGuidCol) {
//		    has=true;	
//		}
//	    }
//	    return has;
//	}
//
//	public IList GetPrimaryKeyColumns() {
//	    ArrayList list = new ArrayList();
//	    Field id = GetIdentityColumn();
//	    if (id != null && id.Name.Length>0) {
//		list.Add(id);
//	    } else {
//		foreach (Field field in fields) {
//		    if (field.IsPrimaryKey) {
//			list.Add(field);
//		    }
//		}
//	    }
//	    return list;
//	}
//
//	// static helper method
//	public Field GetIdentityColumn() {
//	    foreach (Field field in fields) {
//		if (field.IsIdentity) {
//		    return field;
//		}
//	    }
//	    return new Field();   // this should not return this - should return null
//	}
//
//	public Field FindFieldBySqlName(String name) {
//	    foreach (Field field in fields) {
//		if (field.SqlName == name) {
//		    return field;
//		}
//	    }
//	    return null;
//	}
//

	public Field GetIdentityField() {
	    foreach (Field field in fields) {
		if (field.Column.Identity) {
		    return field;
		}
	    }
	    return null;
	}

	public Field FindFieldByName(String name) {
	    foreach (Field field in fields) {
		if (field.Name == name) {
		    return field;
		}
	    }
	    return null;
	}

	public IList GetPrimaryKeyFields() {
	    ArrayList list = new ArrayList();
	    Field id = GetIdentityField();
	    if (id != null) {
		list.Add(id);
	    } else {
		foreach (Field field in fields) {
		    if (sqlEntity.IsPrimaryKeyColumn(field.Column.Name)) {
			list.Add(field);
		    }
		}
	    }
	    return list;
	}

	public Object Clone() {
	    return MemberwiseClone();
	}

	public Finder FindFinderByName(String name) {
	    foreach (Finder finder in finders) {
		if (finder.Name == name) {
		    return finder;
		}
	    }
	    return null;
	}


    }
}
