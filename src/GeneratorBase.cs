using System;
using System.Data;
using System.IO;
using System.Collections;
using System.Text;
using System.Windows.Forms;

namespace Spring2.DataTierGenerator {
    public abstract class GeneratorBase {

	private readonly String REGION = "#region";
	private readonly String END_REGION = "#endregion";

	protected Configuration options;

	public GeneratorBase() {
	}

	public GeneratorBase(Configuration options) {
	    this.options = options;
	}

	/// <summary>
	/// Helper method to write generated source to file.  Directory will be created if it does not already exist.
	/// </summary>
	/// <param name="fileName">name of file, including full path</param>
	/// <param name="text">what to write to the file</param>
	/// <param name="append">whether or not or overwrite the file or to append to file</param>
	protected void WriteToFile(FileInfo file, String text, Boolean append) {

	    // Create the directory if it doesn't exist.
	    if (!file.Directory.Exists) {
		file.Directory.Create();
	    }

	    text.Trim();

	    Boolean changed = false;
	    StringWriter regions = new StringWriter();

	    // Check that either file does not exist or that it has changed
	    // before writing to the file.  This is so that the generated file's
	    // timestamp won't change unless the contents have changed.
	    if (file.Exists) {

		// Create a reader for the newly generated text and the existing file.
		StringReader textReader = new StringReader(text);
		StreamReader fileReader = file.OpenText();
		
		Boolean inRegion = false;
		String fileLine = fileReader.ReadLine();
		String textLine = textReader.ReadLine();
		while (fileLine != null) {
		    // Check to see if we have entered a region.
		    inRegion = inRegion || fileLine.Trim().StartsWith(REGION);

		    // If we are in a region, save the line.  Otherwise, check
		    // to see if the line in the file is different than the line
		    // in the generated text.
		    if (inRegion) {
			regions.WriteLine(fileLine);
		    } else {
			changed = changed || !fileLine.Equals(textLine);
			textLine = textReader.ReadLine();
		    }

		    // Check to see if we have left a region.
		    inRegion = inRegion && !fileLine.Trim().StartsWith(END_REGION);

		    // Read the next line from both the file and the generated text.
		    fileLine = fileReader.ReadLine();
		}

		// Handle the case where the generated text has more lines than the
		// existing file but up to that point they were identical.  Don't
		// know how that could happen, but this should handle it.
		changed = changed || textLine != null;

		fileReader.Close();
		textReader.Close();
	    } else {
		changed = true;
	    }

	    // Only write to the file if it has changed or does not exist.
	    if (changed) {
		    StreamWriter writer = new StreamWriter(file.FullName, append);

		// If any #region tags were found, append the regions to the end
		// of the class.  Otherwise, write the generated text to the file.
		String regionsString = regions.ToString();
		if (regionsString != String.Empty) {
		    Int32 index = text.Substring(0, text.LastIndexOf('}')).LastIndexOf('}');
		    writer.Write(text.Substring(0, index));
		    writer.Write(regionsString);
		    writer.WriteLine("    }");
		    writer.Write('}');
		} else {
		    writer.Write(text);
		}
		writer.Close();
	    }
	}

	public void GetUsingNamespaces(TextWriter writer, ArrayList fields, Boolean isDaoClass) {

	    ArrayList namespaces = new ArrayList();
	    namespaces.Add("System");

	    if (isDaoClass) {
		namespaces.Add("System.Collections");
		namespaces.Add("System.Configuration");
		namespaces.Add("System.Data");
		namespaces.Add("System.Data.SqlClient");
		namespaces.Add("Spring2.Core.DAO");
		namespaces.Add(options.GetDONameSpace(null));
	    }

	    foreach (Field field in fields) {
		if (field.Name.IndexOf('.') < 0) {
		    if (!field.Type.Package.Equals(String.Empty) && !namespaces.Contains(field.Type.Package)) {
			namespaces.Add(field.Type.Package);
		    }
		}
	    }

	    Array names = namespaces.ToArray(typeof(String));
	    Array.Sort(names);

	    // Append system.
	    Boolean added = false;
	    foreach (String s in names) {
		if (s.StartsWith("System")) {
		    added = true;
		    writer.WriteLine("using " + s + ";");
		}
	    }
	    if (added) {
		writer.WriteLine();
	    }

	    added = false;
	    foreach (String s in names) {
		if (s.StartsWith("Spring2")) {
		    added = true;
		    writer.WriteLine("using " + s + ";");
		}
	    }
	    if (added) {
		writer.WriteLine();
	    }

	    added = false;
	    foreach (String s in names) {
		if (!s.StartsWith("Spring2") && !s.StartsWith("System")) {
		    added = true;
		    writer.WriteLine("using " + s + ";");
		}
	    }
	    if (added) {
		writer.WriteLine();
	    }
	}
    }
}
