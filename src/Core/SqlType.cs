using System;
using System.Collections;
using System.Xml;

namespace Spring2.DataTierGenerator {
    public class SqlType : Spring2.Core.DataObject.DataObject, ICloneable {

	private String name = String.Empty;
	private String type = String.Empty;
	private Int32 length = 0;
	private Int32 scale = 0;
	private Int32 precision = 0;
	private String readerMethodFormat = String.Empty;
	private String sqlDbType = String.Empty;
	private String declarationFormat = "{0}";

	public String Name {
	    get { return this.name; }
	    set { this.name = value; }
	}

	public String Type {
	    get { return this.type; }
	    set { this.type = value; }
	}

	public Int32 Length {
	    get { return this.length; }
	    set { this.length = value; }
	}

	public Int32 Scale {
	    get { return this.scale; }
	    set { this.scale = value; }
	}

	public Int32 Precision {
	    get { return this.precision; }
	    set { this.precision = value; }
	}

	public String ReaderMethodFormat {
	    get { return this.readerMethodFormat; }
	    set { this.readerMethodFormat = value; }
	}

	public String SqlDbType {
	    get { return this.sqlDbType; }
	    set { this.sqlDbType = value; }
	}

	public String DeclarationFormat {
	    get { return this.declarationFormat; }
	    set { this.declarationFormat = value; }
	}

	public String Declaration {
	    get { return String.Format(declarationFormat, name, length, precision, scale); }
	}

	public static Hashtable ParseFromXml(XmlDocument doc) {
	    Hashtable sqltypes = new Hashtable();
	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("sqltype");
	    foreach (XmlNode node in elements) {
		SqlType sqltype = new SqlType();
		sqltype.Name = node.Attributes["name"].Value;
		sqltype.SqlDbType = sqltype.Name.Substring(0, 1).ToUpper() + sqltype.Name.Substring(1);
		if (node.Attributes["type"] != null) {
		    sqltype.Type = node.Attributes["type"].Value;
		}
		if (node.Attributes["length"] != null) {
		    sqltype.Length = Int32.Parse(node.Attributes["length"].Value);
		}
		if (node.Attributes["scale"] != null) {
		    sqltype.Scale = Int32.Parse(node.Attributes["scale"].Value);
		}
		if (node.Attributes["precision"] != null) {
		    sqltype.Precision = Int32.Parse(node.Attributes["precision"].Value);
		}
		if (node.Attributes["readermethodformat"] != null) {
		    sqltype.ReaderMethodFormat = node.Attributes["readermethodformat"].Value;
		}
		if (node.Attributes["sqldbtype"] != null) {
		    sqltype.SqlDbType = node.Attributes["sqldbtype"].Value;
		}
		if (node.Attributes["declarationformat"] != null) {
		    sqltype.DeclarationFormat = node.Attributes["declarationformat"].Value;
		}
		if (sqltypes.ContainsKey(sqltype.Name)) {
		    Console.Out.WriteLine ("WARNING: ignoring duplicate definition of sqltype: " + sqltype.Name);
		} else {
		    sqltypes.Add(sqltype.Name, sqltype);
		}
	    }
	    return sqltypes;
	}

	public Object Clone() {
	    return MemberwiseClone();
	}

    }
}
