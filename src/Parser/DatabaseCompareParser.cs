using System;

using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Element;

namespace Spring2.DataTierGenerator.Parser {
    /// <summary>
    /// A parser that will compare information that can be parsed from a database PLUS the information that can be pulled from the parser passed in
    /// </summary>
    public class DatabaseCompareParser : ParserSkeleton, IParser {

	public DatabaseCompareParser(ParserElement parser, Configuration options, XmlDocument doc, Hashtable sqltypes, Hashtable types, ParserValidationDelegate vd) {
	    // parse entity/sqlentity elements
	    DatabaseParser db = new DatabaseParser(parser, options, doc, sqltypes, types, vd);
	    IList databases = DatabaseElement.ParseFromXml(options, doc, sqltypes, types, vd);
	    IList entities = EntityElement.ParseFromXml(options, doc, sqltypes, types, DatabaseElement.GetAllSqlEntities(databases), vd);
	    Validate(vd);

	    this.databases = new ArrayList();
	    DatabaseElement database = new DatabaseElement();
	    this.databases.Add(database);
	    this.entities = new ArrayList();

	    foreach (EntityElement dbe in db.Entities) {
		EntityElement e = EntityElement.FindEntityBySqlEntity((ArrayList)entities, dbe.SqlEntity.Name);
		if (e != null) {
		    IList fields = new ArrayList();
		    foreach (PropertyElement dbf in dbe.Fields) {
			PropertyElement f = PropertyElement.FindByColumnName(e.Fields, dbf.Column.Name);
			if (f == null) {
			    fields.Add(dbf);
			} else {
			    // not sure if this is a good idea - the assumption that the config file is most correct might be a better way
//			    if (!dbf.Column.Name.Equals(f.Column.Name) || !dbf.Type.Name.Equals(f.Type.Name)) {
//				fields.Add(dbf);
//				Console.Out.WriteLine("new field:" + dbe.Name + "." + dbf.Name);
//			    } else {
//				Console.Out.WriteLine("found " + dbe.Name + "." + dbf.Name + ": " + f.Equals(dbf));
//			    }
			}
		    }
		    // if there were any new or different fields, create a clone of the db.Entity and replace the fields collection
		    if (fields.Count > 0) {
			EntityElement ne = (EntityElement)dbe.Clone();
			ne.Fields = (ArrayList)fields;
			this.entities.Add(ne);
			Console.Out.WriteLine("adding partial entity: " + ne.Name);
		    }
		} else {
		    this.entities.Add(dbe);
		}
	    }

	    foreach (SqlEntityElement dbse in ((DatabaseElement)db.Databases[0]).SqlEntities) {
		SqlEntityElement e = SqlEntityElement.FindByName(DatabaseElement.GetAllSqlEntities(databases), dbse.Name);
		if (e != null) {
		    // columns, constraints, indexes
		    ArrayList columns = new ArrayList();
		    ArrayList indexes = new ArrayList();
		    ArrayList constraints = new ArrayList();

		    foreach (ColumnElement dbc in dbse.Columns) {
			if (e.FindColumnByName(dbc.Name) == null) {
			    columns.Add(dbc);
			}
		    }

		    foreach (IndexElement dbi in dbse.Indexes) {
			IndexElement i = e.FindIndexByName(dbi.Name);
			if (i == null) {
			    indexes.Add(dbi);
			} else {
			    Boolean diff = false;
			    foreach (ColumnElement dbic in dbi.Columns) {
				if (i.FindColumnByName(dbic.Name) == null) {
				    diff = true;
				}
			    }
			    if (diff) {
				indexes.Add(dbi);
			    }
			}
		    }

		    foreach (ConstraintElement dbc in dbse.Constraints) {
			ConstraintElement c = e.FindConstraintByName(dbc.Name);
			if (c == null) {
			    constraints.Add(dbc);
			} else {
			    Boolean diff = false;
			    foreach (ColumnElement dbcc in dbc.Columns) {
				if (c.FindColumnByName(dbcc.Name) == null) {
				    diff = true;
				}
			    }
			    if (diff) {
				indexes.Add(dbc);
			    }
			}
		    }

		    if (columns.Count>0 || indexes.Count>0 || constraints.Count>0) {
			SqlEntityElement nse = (SqlEntityElement)dbse.Clone();
			nse.Indexes = indexes;
			nse.Columns = columns;
			nse.Constraints = constraints;
			database.SqlEntities.Add(nse);
			Console.Out.WriteLine("adding partial sql entity: " + nse.Name);
		    }
		} else {
		    database.SqlEntities.Add(dbse);
		}
	    }
	}

    }
}
