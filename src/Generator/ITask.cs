using System;
using System.Collections;

namespace Spring2.DataTierGenerator.Generator {

    /// <summary>
    /// Summary description for IGenerator.
    /// </summary>
    public interface ITask {

	ElementList Elements {
	    get;
	}

	String FileNameFormat {
	    get;
	}

	String Directory {
	    get;
	}

	String BackupDirectory 
	{
	    get;
	}
	Hashtable Parameters 
	{
	    get;
	}

	String Writer {
	    get;
	}

	String Template {
	    get;
	}

	String Styler {
	    get;
	}

    }
}
