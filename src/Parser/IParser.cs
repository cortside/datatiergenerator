using System;
using System.Collections;


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

	IList Errors {
	    get;
	}

	String ErrorDescription {
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

	Element.Generator Generator {
	    get;
	}

	Element.Parser Parser {
	    get;
	}

    }
}
