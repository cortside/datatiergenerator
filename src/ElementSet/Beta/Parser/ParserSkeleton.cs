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
    /// Summary description for XMLParser.
    /// </summary>
    public abstract class ParserSkeleton : IParser {

	protected ConfigurationElement options = new ConfigurationElement();
	protected IList entities = new ArrayList();
	protected IList enumtypes = new ArrayList();
	protected IList collections = new ArrayList();
	protected IList databases = new ArrayList();
	protected Hashtable types = new Hashtable();
	protected Hashtable sqltypes = new Hashtable();
	protected GeneratorElement generator = new GeneratorElement();
	protected ParserElement parser = new ParserElement();

	private Boolean isValid = true;
	private IList errors = new ArrayList();
	private IList log = new ArrayList();

	protected ParserSkeleton() {}

	protected ParserSkeleton(ParserElement parser, ConfigurationElement options, XmlDocument doc) {
	    this.parser = parser;
	    this.options = options;
	    sqltypes = SqlTypeElement.ParseFromXml(doc, this);
	    types = TypeElement.ParseFromXml(options, doc, this);
	}

	public void AddValidationMessage(ParserValidationMessage message) {
	    if (message.Severity.Equals(ParserValidationSeverity.ERROR)) {
		isValid = false;
	    }
	    errors.Add(message);
	}

	public ConfigurationElement Configuration {
	    get { return options; }
	}

	protected void WriteToLog(String s) {
	    log.Add(s);
	}

	public Boolean IsValid {
	    get { return isValid; }
	    set { isValid = value; }
	}

	public IList Log {
	    get { return log; }
	}

	public IList Databases {
	    get { return (ArrayList)((ArrayList)databases).Clone(); }
	}

	public IList Entities {
	    get { return (ArrayList)((ArrayList)entities).Clone(); }
	}

	public IList Enums {
	    get { return (ArrayList)((ArrayList)enumtypes).Clone(); }
	}

	public IList Collections {
	    get { return (ArrayList)((ArrayList)collections).Clone(); }
	}

	public ICollection Types {
	    get { return types.Values; }
	}

	public ICollection SqlTypes {
	    get { return sqltypes.Values; }
	}

	public GeneratorElement Generator {
	    get { return generator; }
	}

	public ParserElement Parser {
	    get { return parser; }
	}

	public SqlTypeElement FindSqlType(String name) {
	    foreach (SqlTypeElement sqlType in SqlTypes) {
		if (sqlType.Name.Equals(name)) {
		    return sqlType;
		}
	    }
	    return null;
	}

	public SqlEntityElement FindSqlEntity(String name) {
	    foreach (DatabaseElement database in Databases) {
		foreach (SqlEntityElement sqlEntity in database.SqlEntities) {
		    if (sqlEntity.Name.Equals(name)) {
			return sqlEntity;
		    }
		}
	    }
	    return null;
	}

	public EntityElement FindEntity(String name) {
	    foreach (EntityElement entity in Entities) {
		if (entity.Name.Equals(name)) {
		    return entity;
		}
	    }
	    return null;
	}

	public TypeElement FindType(String name) {
	    foreach (TypeElement type in Types) {
		if (type.Name.Equals(name)) {
		    return type;
		}
	    }
	    return null;
	}

	/// <summary>
	/// Post-parse validations
	/// </summary>
	/// <param name="vd"></param>
	public void Validate() {
	    //TODO: walk through collection to make sure that cross relations are correct.

	    foreach (DatabaseElement database in databases) {
		foreach(SqlEntityElement sqlentity in database.SqlEntities) {
		    if (sqlentity.GetPrimaryKeyColumns().Count==0 && (sqlentity.GenerateDeleteStoredProcScript || sqlentity.GenerateUpdateStoredProcScript || sqlentity.GenerateInsertStoredProcScript)) {
			this.AddValidationMessage(ParserValidationMessage.NewWarning("SqlEntity " + sqlentity.Name + " does not have any primary key columns defined."));
		    }

		    if (!sqlentity.HasUpdatableColumns() && sqlentity.GenerateUpdateStoredProcScript) {
			this.AddValidationMessage(ParserValidationMessage.NewWarning("SqlEntity " + sqlentity.Name + " does not have any editable columns and does not have generateupdatestoredprocscript=\"false\" specified."));
		    }
		}
	    }

	    // make sure that all columns are represented in entities
	    foreach(EntityElement entity in entities) {
		if (entity.SqlEntity.Name.Length>0) {
		    foreach(ColumnElement column in entity.SqlEntity.Columns) {
			if (entity.FindFieldByColumnName(column.Name)==null) {
			    this.AddValidationMessage(ParserValidationMessage.NewWarning("could not find field representing column " + column.Name + " in entity " + entity.Name + "."));
			}
		    }
		}
	    }
	}
    }
}
