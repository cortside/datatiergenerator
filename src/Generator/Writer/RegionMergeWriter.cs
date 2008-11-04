using System;
using System.Collections;
using System.IO;
using System.Text;
using Spring2.DataTierGenerator.Generator.Styler;

namespace Spring2.DataTierGenerator.Generator.Writer {

    /// <summary>
    /// Writer that can merge existing #region sections into new content
    /// </summary>
    public class RegionMergeWriter : AbstractWriter, IWriter {

	private readonly String REGION = "#region";
	private readonly String END_REGION = "#endregion";
	private String backupFilePath = "";

	public RegionMergeWriter() 
	{
	}

	public RegionMergeWriter(Hashtable options) {
            // currently does not support any configurable options
	}

	/// <summary>
	/// String to backup the file to before it is overwritten.  Blank means no backup.
	/// </summary>
	public String BackupFilePath {
	    get { return backupFilePath; }
	    set { backupFilePath = value; }
	}

	/// <summary>
	/// Helper method to write generated source to file.  Directory will be created if it does not already exist.
	/// </summary>
	/// <param name="fileName">name of file, including full path</param>
	/// <param name="text">what to write to the file</param>
	/// <param name="append">whether or not or overwrite the file or to append to file</param>
	public Boolean Write(FileInfo file, String text, IStyler styler) {
	    // Create the directory if it doesn't exist.
	    if (!file.Directory.Exists) {
		file.Directory.Create();
	    }

	    text.Trim();

	    Boolean changed = false;
	    StringWriter regions = new StringWriter();
	    StringWriter current = new StringWriter();

	    // Check that either file does not exist or that it has changed
	    // before writing to the file.  This is so that the generated file's
	    // timestamp won't change unless the contents have changed.
	    if (file.Exists) {
		StreamReader fileReader = file.OpenText();
		
		Boolean inRegion = false;
		String fileLine = fileReader.ReadLine();
		while (fileLine != null) {
		    current.WriteLine(fileLine);

		    // Check to see if we have entered a region.
		    inRegion = inRegion || fileLine.Trim().StartsWith(RegionTag);

		    // If we are in a region, save the line.
		    if (inRegion) {
			regions.WriteLine(fileLine);
		    }

		    // Check to see if we have left a region.
		    inRegion = inRegion && !fileLine.Trim().StartsWith(EndRegionTag);

		    // Read the next line from the file.
		    fileLine = fileReader.ReadLine();
		}

		fileReader.Close();
	    }

	    // compare the current file text with the text that will be written (merging any existing regions if necessary)
	    String output; 
	    if (regions.ToString().Length > 0) {
		output = MergeRegion(text, regions.ToString());
	    } else {
		output = text;
	    }
	    // determine whether anything has actually changed in the file
	    bool fileHasChanged = false;
	    String mergedContentIgnoreWhitespace = output.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace(" ", "");
	    String exitingContentsIgnoreWhitespace = current.GetStringBuilder().ToString().Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace(" ", "");
	    if (mergedContentIgnoreWhitespace != exitingContentsIgnoreWhitespace)
	    {
		fileHasChanged = true;
	    }

	    // Only write to the file if it has changed or does not exist.
	    if (fileHasChanged) {
		// make a backup of the current file if it exists
		if (file.Exists && backupFilePath != "") {
		    FileInfo backup = new FileInfo(backupFilePath);
		    if (!backup.Directory.Exists) {
			backup.Directory.Create();
		    }
		    if (File.Exists(backupFilePath) && (File.GetAttributes(backupFilePath) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly) 
		    {
			File.SetAttributes(backupFilePath, File.GetAttributes(backupFilePath) ^ FileAttributes.ReadOnly);
		    }
		    file.CopyTo(backupFilePath, true);
		    if ((File.GetAttributes(backupFilePath) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly) {
			File.SetAttributes(backupFilePath, File.GetAttributes(backupFilePath) ^ FileAttributes.ReadOnly);
		    }
		}

                // Style the output
                string styledOutput = styler.Style(output);

                // write the new file
		StreamWriter writer = new StreamWriter(file.FullName, false);
		writer.Write(styledOutput);
		writer.Close();
	    }

	    return changed;
	}
	
	protected virtual String MergeRegion(String text, String regionsString) {
	    StringWriter writer = new StringWriter();

	    Int32 index = text.Substring(0, text.LastIndexOf('}')).LastIndexOf('}');
	    index = text.Substring(0, index).LastIndexOf('}') + 1;
	    writer.WriteLine(text.Substring(0, index));
	    writer.WriteLine();
	    writer.Write("\t");
	    writer.WriteLine(regionsString.Trim());
	    writer.WriteLine("    }");
	    writer.WriteLine('}');

	    return writer.GetStringBuilder().ToString();
	}

	protected virtual String RegionTag {
	    get { return REGION; }
	}

	protected virtual String EndRegionTag {
	    get { return END_REGION; }
	}

    }
}
