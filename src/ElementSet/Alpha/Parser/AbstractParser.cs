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
using Spring2.DataTierGenerator.Generator.Styler;
using Spring2.DataTierGenerator.Generator.Writer;

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
	protected IList reportExtractions = new ArrayList();
	protected Hashtable types = new Hashtable();
	protected Hashtable sqltypes = new Hashtable();
	protected GeneratorElement generator = new GeneratorElement();

	private Hashtable writers = new Hashtable();
	private Hashtable stylers = new Hashtable();

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
	public IList ReportExtractions {
	    get { return reportExtractions; }
	}

	public Hashtable Types {
	    get { return types; }   
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
		    if (sqlentity.GetPrimaryKeyColumns().Count==0 && (sqlentity.AllowDelete || sqlentity.AllowInsert || sqlentity.AllowUpdate)) {
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
			if (!column.Obsolete && entity.FindFieldByColumnName(column.Name)==null) {
			    vd(ParserValidationArgs.NewWarning("could not find property representing column " + column.Name + " in entity " + entity.Name + "."));
			}
		    }
		}

		foreach(PropertyElement property in entity.Fields) {
		    // make sure that obsolete columns are not mapped to properties
		    if (property.Column.Obsolete && property.Column.Name.Length > 0) {
			vd(ParserValidationArgs.NewWarning("property " + property.Name + " in entity " + entity.Name + " is mapped to column " + property.Column.Name + " which is obsolete."));
		    }

		    // have property descriptions "inherit" from a column if they are not populated
		    if (property.Column.Description.Length > 0 && property.Description.Length ==0)  {
			property.Description = property.Column.Description;
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

	    // find and assign types to collections if available (the TypeElement is needed for templates that need to add namespaces)
	    foreach(CollectionElement collection in Collections) {
		if (types.Contains(collection.Type.Name)) {
		    collection.Type = (TypeElement)types[collection.Type.Name];
		}
	    }

	    foreach (TaskElement task in generator.Tasks) {
		IWriter w = GetWriter(task.Writer);
		if (w == null) {
		    vd(ParserValidationArgs.NewError("Task specified writer '" + task.Writer + "' that was not defined."));
		}

		// check to make sure the styler exists if it is specified (optional)
		if (task.Styler.Length > 0) {
		    IStyler s = GetStyler(task.Styler);
		    if (s == null) {
			vd(ParserValidationArgs.NewError("Task specified styler '" + task.Styler + "' that was not defined."));
		    }
		}
	    }
	}

	public IWriter GetWriter(String name) {
	    if (writers.Count ==0) {
		foreach(WriterElement element in generator.Writers) {
		    try {
			Type clazz = System.Type.GetType(element.Class);
			if (clazz != null) {
			    Object o = System.Activator.CreateInstance(clazz, new Object[] { element.GetOptions() });
			    if (o is IWriter) {
				IWriter w = (IWriter)o;
				writers.Add(element.Name, w);
			    } else {
				WriteToLog(element.Name + " does not support the IWriter interface");
			    }
			} else {
			    WriteToLog("Could not find class '" + element.Class + "', '" + element.Name + "' will not be added to available writers.");
			}
		    } catch (Exception ex) {
			WriteToLog("Error trying to create writer '" + element.Name + "': " + ex.Message);
		    }
		}
	    }

	    return (IWriter)writers[name];
	}

	public IStyler GetStyler(String name) {
	    if (stylers.Count ==0) {
		foreach(StylerElement element in generator.Stylers) {
		    try {
			Type clazz = System.Type.GetType(element.Class);
			if (clazz != null) {
			    Object o = System.Activator.CreateInstance(clazz, new Object[] { element.GetOptions() });
			    if (o is IStyler) {
				IStyler s = (IStyler)o;
				stylers.Add(element.Name, s);
			    } else {
				WriteToLog(element.Name + " does not support the IStyler interface");
			    }
			} else {
			    WriteToLog("Could not find class '" + element.Class + "', '" + element.Name + "' will not be added to available stylers.");
			}
		    } catch (Exception ex) {
			WriteToLog("Error trying to create styler '" + element.Name + "': " + ex.Message);
		    }
		}
	    }

	    return (IStyler)stylers[name];
	}

    }
}
