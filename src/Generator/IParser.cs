using System;
using System.Collections;
using System.Xml;

using Spring2.DataTierGenerator.Generator;


namespace Spring2.DataTierGenerator.Generator {

    /// <summary>
    /// Summary description for IParser.
    /// </summary>
    public interface IParser {

	/// <summary>
	/// Parse config file 
	/// </summary>
	void Parse(String filename);

	Object Configuration {
	    get;
	}

	Boolean IsValid {
	    get;
	}

	TaskList Tasks {
	    get;
	}
	
	Hashtable Tools {
	    get;
	}

	String Generator {
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
