using System;
using System.Collections;
using System.IO;

namespace Spring2.DataTierGenerator.Generator.Writer {

    /// <summary>
    /// Writer that writes any text
    /// </summary>
    public class TextWriter : AbstractWriter, IWriter {

	public TextWriter() {
	}

	public TextWriter(Hashtable options) {
            // currently does not support any configurable options
	}

	/// <summary>
	/// Write contents to
	/// </summary>
	/// <param name="fileName">name of file, including full path</param>
	/// <param name="text">what to write to the file</param>
	public Boolean Write(FileInfo file, String text) {
	    // Create the directory if it doesn't exist.
	    if (!file.Directory.Exists) {
		file.Directory.Create();
	    }

	    text.Trim();

	    Boolean changed = false;

	    // Check that either file does not exist or that it has changed
	    // before writing to the file.  This is so that the generated file's
	    // timestamp won't change unless the contents have changed.
	    if (file.Exists) {
		StreamReader fileReader = file.OpenText();
		changed = fileReader.ReadToEnd().Equals(text);
	    } else {
		changed = true;
	    }

	    // Only write to the file if it has changed or does not exist.
	    if (changed) {
		StreamWriter writer = new StreamWriter(file.FullName, false);
		writer.Write(text);
		writer.Close();
	    }

	    return changed;
	}

    }
}
