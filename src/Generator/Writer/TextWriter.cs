using System;
using System.Collections;
using System.IO;
using Spring2.DataTierGenerator.Generator.Styler;

namespace Spring2.DataTierGenerator.Generator.Writer {

    /// <summary>
    /// Writer that writes any text
    /// </summary>
    public class TextWriter : AbstractWriter, IWriter {

	private String backupFilePath = "";
        Spring2.DataTierGenerator.Plugins.Plugins plugins = null; 

	public TextWriter() {
            String path = System.IO.Directory.GetCurrentDirectory();
            plugins = new Spring2.DataTierGenerator.Plugins.Plugins(path);
	}

	public TextWriter(Hashtable options) {
            // currently does not support any configurable options
            String path = System.IO.Directory.GetCurrentDirectory();
            plugins = new Spring2.DataTierGenerator.Plugins.Plugins(path);
	}

	/// <summary>
	/// String to backup the file to before it is overwritten.  Blank means no backup.
	/// </summary>
	public String BackupFilePath 
	{
	    get { return backupFilePath; }
	    set { backupFilePath = value; }
	}

	/// <summary>
	/// Write contents to
	/// </summary>
	/// <param name="fileName">name of file, including full path</param>
	/// <param name="text">what to write to the file</param>
	public Boolean Write(FileInfo file, String text, IStyler styler) {
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

            Boolean isNewFile = false;
	    // Only write to the file if it has changed or does not exist.
	    if (changed) {
		// make backup if the file exists
                if (file.Exists && backupFilePath != "") {
                    DoPreProcessingExisting(file.FullName);
                    FileInfo backup = new FileInfo(backupFilePath);
                    if (!backup.Directory.Exists) {
                        backup.Directory.Create();
                    }
                    if (File.Exists(backupFilePath) && (File.GetAttributes(backupFilePath) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly) {
                        File.SetAttributes(backupFilePath, File.GetAttributes(backupFilePath) ^ FileAttributes.ReadOnly);
                    }
                    file.CopyTo(backupFilePath, true);
                    if ((File.GetAttributes(backupFilePath) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly) {
                        File.SetAttributes(backupFilePath, File.GetAttributes(backupFilePath) ^ FileAttributes.ReadOnly);
                    }
                } else {
                    isNewFile = true;
                }

                // Style the contents
                text = styler.Style(text);

                // Write the file.
		StreamWriter writer = new StreamWriter(file.FullName, false);
		writer.Write(text);
		writer.Close();

                if (isNewFile) {
                    DoPostProcessingNew(file.FullName);
                }
	    }

	    return changed;
	}

        private void DoPreProcessingExisting(String filePath) {
            if (plugins.PluginsFound == true) {
                plugins.DoPreWriteExisting(filePath);
            }
        }

        private void DoPostProcessingNew(String filePath) {
            if (plugins.PluginsFound == true) {
                plugins.DoPostWriteNew(filePath);
            }
        }

    }
}
