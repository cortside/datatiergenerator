using System;
using System.Collections;

using Spring2.DataTierGenerator.Element;

namespace Spring2.DataTierGenerator.Parser {
    /// <summary>
    /// Summary description for IParser.
    /// </summary>
    public interface IParser {

	Configuration Configuration {
	    get;
	}

	Boolean IsValid {
	    get;
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

	/// <summary>
	/// List of log messages (String) that were created durring parse
	/// </summary>
	IList Log {
	    get;
	}

    }
}
