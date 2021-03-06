using System;
using System.Collections;
using System.IO;
using Spring2.DataTierGenerator.Generator.Styler;

namespace Spring2.DataTierGenerator.Generator.Writer {

    /// <summary>
    /// Interface for all writers
    /// </summary>
    public interface IWriter {


	/// <summary>
	/// Write contents to disk.  Writer may merge existing file contents with new contents.
	/// </summary>
	/// <returns>boolean denoting whether file need to be updated or not</returns>
	Boolean Write(FileInfo file, String text, IStyler styler);

	/// <summary>
	/// If set a copy of the file before generation is written to the specified path.
	/// </summary>
	String BackupFilePath { get; set; }

	/// <summary>
	/// List of log messages (String) that were created durring generation
	/// </summary>
	IList Log {
	    get;
	}

    }
}
