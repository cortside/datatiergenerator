using System;
using System.Collections;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class SqlTypeElement : ElementSkeleton {

	public static readonly String SQLTYPE = "sqltype";
	private static readonly String TYPE = "type";
	private static readonly String READER_METHOD_FORMAT = "readermethodformat";
	private static readonly String DECLARATION_FORMAT = "declarationformat";
	private static readonly String SQL_DB_TYPE = "sqldbtype";
	private static readonly String LENGTH = "length";
	private static readonly String SCALE = "scale";
	private static readonly String PRECISION = "precision";

	private String type = String.Empty;
	private String readerMethodFormat = String.Empty;
	private String declarationFormat = "{0}";
	private String sqlDbType = String.Empty;
	private Int32 length = 0;
	private Int32 scale = 0;
	private Int32 precision = 0;

	public String Type {
	    get { return this.type; }
	    set { this.type = value; }
	}

	public String ReaderMethodFormat {
	    get { return this.readerMethodFormat; }
	    set { this.readerMethodFormat = value; }
	}

	public String DeclarationFormat {
	    get { return this.declarationFormat; }
	    set { this.declarationFormat = value; }
	}

	public String SqlDbType {
	    get { return this.sqlDbType; }
	    set { this.sqlDbType = value; }
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

	public String Declaration {
	    get { return String.Format(declarationFormat, name, length, precision, scale); }
	}

	public SqlTypeElement() {}

	public SqlTypeElement(XmlNode sqlTypeNode) {

	    if (sqlTypeNode != null && SQLTYPE.Equals(sqlTypeNode.Name)) {
		name = GetAttributeValue(sqlTypeNode, NAME, name);
		type = GetAttributeValue(sqlTypeNode, TYPE, type);
		readerMethodFormat = GetAttributeValue(sqlTypeNode, READER_METHOD_FORMAT, readerMethodFormat);
		declarationFormat = GetAttributeValue(sqlTypeNode, DECLARATION_FORMAT, declarationFormat);
		sqlDbType = GetAttributeValue(sqlTypeNode, SQL_DB_TYPE, sqlDbType);
		length = Int32.Parse(GetAttributeValue(sqlTypeNode, LENGTH, length.ToString()));
		scale = Int32.Parse(GetAttributeValue(sqlTypeNode, SCALE, scale.ToString()));
		precision = Int32.Parse(GetAttributeValue(sqlTypeNode, PRECISION, precision.ToString()));
	    } else {
		throw new ArgumentException("The XmlNode argument is not a SQL type node.");
	    }
	}

	public override void Validate(IParser parser) {
	    // Capitalize sql type name ?
	    // Avoid duplicate types.
	}

	public static Hashtable ParseFromXml(XmlDocument doc, IParser parser) {

	    Hashtable sqltypes = new Hashtable();
	    XmlNodeList elements = doc.DocumentElement.GetElementsByTagName("sqltype");

	    foreach (XmlNode node in elements) {
		SqlTypeElement sqltype = new SqlTypeElement();
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
		    parser.AddValidationMessage(ParserValidationMessage.NewWarning("Ignoring duplicate definition of sqltype: " + sqltype.Name));
		} else {
		    sqltypes.Add(sqltype.Name, sqltype);
		}
	    }

	    return sqltypes;
	}
    }
}
