using System;
using System.Collections;
using System.IO;

namespace Spring2.DataTierGenerator.Generator.Writer {

    /// <summary>
    /// Interface for all writers
    /// </summary>
    public interface IWriter {

	/// <summary>
	/// Write contents to disk.  Writer may merge existing file contents with new contents.
	/// </summary>
	/// <returns>boolean denoting whether file need to be updated or not</returns>
	Boolean Write(FileInfo file, String text);

	/// <summary>
	/// List of log messages (String) that were created durring generation
	/// </summary>
	IList Log {
	    get;
	}

    }
}
