using System;
using System.Collections;
using System.IO;

namespace Spring2.DataTierGenerator.Generator.Styler {

    /// <summary>
    /// Interface for all stylers
    /// </summary>
    public interface IStyler {

	/// <summary>
	/// Style generated output before it is written to disk
	/// </summary>
	String Style(String text);

	/// <summary>
	/// List of log messages (String) that were created durring generation
	/// </summary>
	IList Log {
	    get;
	}

	/// <summary>
	/// File name being styled.
	/// </summary>
	String File {
	    get;
	    set;
	}

    }
}
