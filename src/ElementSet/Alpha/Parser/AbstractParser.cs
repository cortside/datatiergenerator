using System;

using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;

using Spring2.Core.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Element;

namespace Spring2.DataTierGenerator.Parser {

    /// <summary>
    /// base parser
    /// </summary>
    public abstract class AbstractParser {

	protected Configuration options = new Configuration();

	protected IList entities = new ArrayList();
	protected IList enumtypes = new ArrayList();
	protected IList collections = new ArrayList();
	protected IList databases = new ArrayList();
	protected Hashtable types = new Hashtable();
	protected Hashtable sqltypes = new Hashtable();
	protected GeneratorElement generator = new GeneratorElement();

	protected Boolean isValid = false;

	public IList Entities {
	    get { return entities; }
	}
	public IList Enums {
	    get { return enumtypes; }
	}
	public IList Collections {
	    get { return collections; }
	}
	public IList Databases {
	    get { return databases; }
	}

	private IList log = new ArrayList();

	protected void WriteToLog(String s) {
	    log.Add(s);
	}

	public IList Log {
	    get { return log; }
	}


	public Object Configuration {
	    get { return options; }
	}

	public Boolean IsValid {
	    get { return isValid; }
	}

	public String Generator {
	    get { return "Spring2.DataTierGenerator.Generator.NVelocityGenerator,Spring2.DataTierGenerator"; }
	}

	/// <summary>
	/// Event handler for parser validation events
	/// </summary>
	/// <param name="args"></param>
	protected void ParserValidationEventHandler(ParserValidationArgs args) {
	    if (args.Severity.Equals(ParserValidationSeverity.ERROR)) {
		isValid = false;
	    }
	    WriteToLog(args.ToString());
	}


	/// <summary>
	/// Post-parse validations
	/// </summary>
	/// <param name="vd"></param>
	protected void Validate(ParserValidationDelegate vd) {
	    //TODO: walk through collection to make sure that cross relations are correct.

	    foreach (DatabaseElement database in databases) {
		foreach(SqlEntityElement sqlentity in database.SqlEntities) {
		    if (sqlentity.GetPrimaryKeyColumns().Count==0 && (sqlentity.GenerateDeleteStoredProcScript || sqlentity.GenerateUpdateStoredProcScript || sqlentity.GenerateInsertStoredProcScript)) {
			vd(ParserValidationArgs.NewWarning("SqlEntity " + sqlentity.Name + " does not have any primary key columns defined."));
		    }

		    if (!sqlentity.HasUpdatableColumns() && sqlentity.GenerateUpdateStoredProcScript) {
			vd(ParserValidationArgs.NewWarning("SqlEntity " + sqlentity.Name + " does not have any editable columns and does not have generateupdatestoredprocscript=\"false\" specified."));
		    }

		}
	    }

	    // make sure that all columns are represented in entities
	    foreach(EntityElement entity in entities) {
		if (entity.SqlEntity.Name.Length>0) {
		    foreach(ColumnElement column in entity.SqlEntity.Columns) {
			if (entity.FindFieldByColumnName(column.Name)==null) {
			    vd(ParserValidationArgs.NewWarning("could not find field representing column " + column.Name + " in entity " + entity.Name + "."));
			}
		    }
		}
	    }

	    // make sure that enum values are unique
	    foreach(EnumElement enumtype in enumtypes) {
		Hashtable values = new Hashtable();
		foreach(EnumValueElement value in enumtype.Values) {
		    if (values.Contains(value.Code)) {
			vd(ParserValidationArgs.NewError("Enum " + enumtype.Name + " has the code '" + value.Code + "' specified more than once."));
		    } else {
			values.Add(value.Code, value.Code);
		    }
		}
	    }
	}


    }
}
