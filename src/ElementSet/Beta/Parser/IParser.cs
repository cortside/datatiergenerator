using System;
using System.Collections;

using Spring2.DataTierGenerator.Element;

namespace Spring2.DataTierGenerator.Parser {
    /// <summary>
    /// Summary description for IParser.
    /// </summary>
    public interface IParser {

	void AddValidationMessage(ParserValidationMessage message);

	void Validate();

	ConfigurationElement Configuration {
	    get;
	}

	Boolean IsValid {
	    get;
	    set;
	}

	IList Databases {
	    get;
	}

	IList Entities {
	    get;
	}

	IList Enums {
	    get;
	}

	IList Collections {
	    get;
	}

	ICollection Types {
	    get;
	}

	ICollection SqlTypes {
	    get;
	}

	GeneratorElement Generator {
	    get;
	}

	ParserElement Parser {
	    get;
	}

	SqlTypeElement FindSqlType(String typeName);
	SqlEntityElement FindSqlEntity(String name);
	EntityElement FindEntity(String name);
	TypeElement FindType(String name);


	/// <summary>
	/// List of log messages (String) that were created durring parse
	/// </summary>
	IList Log {
	    get;
	}
    }
}
