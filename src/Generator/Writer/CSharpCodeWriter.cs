using System;
using System.Collections;
using System.IO;
using System.CodeDom.Compiler;

namespace Spring2.DataTierGenerator.Generator.Writer {

    /// <summary>
    /// Writer for C# code that is merged using CodeDom
    /// </summary>
    public class CSharpCodeWriter : AbstractWriter, IWriter {

	private CodeGeneratorOptions cgOptions = new CodeGeneratorOptions ();
	private String backupFilePath = "";

	public CSharpCodeWriter() {
	}

	public CSharpCodeWriter(Hashtable options) {
	    if (options.Contains("BraceStyle")) {
		// expect: block, c
		String bstyle = options["BraceStyle"].ToString();
		if (bstyle == "c") {
		    cgOptions.BracingStyle = "C";
		} else if (bstyle == "block") {
		    cgOptions.BracingStyle = "Block";
		} else if (bstyle == "mono") {
		    cgOptions.BracingStyle = "Block";
		}
	    }
	}

	/// <summary>
	/// String to backup the file to before it is overwritten.  Blank means no backup.
	/// </summary>
	public String BackupFilePath {
	    get { return backupFilePath; }
	    set { backupFilePath = value; }
	}

	public Boolean Write(FileInfo file, String contents) {
	    // Create the directory if it doesn't exist.
	    if (!file.Directory.Exists) {
		file.Directory.Create();
	    }

	    MemoryStream stream = new MemoryStream();
	    StreamWriter sw = new StreamWriter(stream);
	    sw.Write(contents);
	    sw.Flush();
	    stream.Position = 0;

	    if (!file.Exists) {
		//CodeUnit unit1 = null;
		try {
		    //unit1 = new CodeUnit(file.Name, stream, Log, cgOptions);
		    //String mergedContent = unit1.Generate();
		    String mergedContent = contents;
		    StreamWriter writer = new StreamWriter(file.FullName, false);
		    writer.Write(mergedContent);
		    writer.Close();
		    return true;
		} catch (Exception ex) {
		    Console.Out.WriteLine("Error in File Name " + file.Name);
		    Console.Out.WriteLine(ex);
		    Log.Add("Error in File Name " + file.Name + ":" + ex.ToString());
		    return false;
		}
	    } else {
		//FileStream fs = null;
		//CodeUnit unit1 = null;
		//CodeUnit unit2 = null;
		try {
		    //fs = file.OpenRead();
		    //unit1 = new CodeUnit(file.Name, fs, Log, cgOptions);
		    //fs.Close();
		    //fs = null;
		    //unit2 = new CodeUnit(file.Name, stream, Log, cgOptions);
		    //unit1.Merge(unit2);

		    StreamReader sr = file.OpenText();
		    String exitingContents = sr.ReadToEnd();
		    sr.Close();

		    //String mergedContent = unit1.Generate();
		    String mergedContent = NRefactoryUtil.Merge(exitingContents, contents);

		    // only write out if the formatted contents of both are different (avoids the "DTG reformatting" commit messages, at least some of the time)
		    if (!mergedContent.Equals(NRefactoryUtil.FixSourceFormatting(exitingContents))) {
			// make backup
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

			StreamWriter writer = new StreamWriter(file.FullName, false);
			writer.Write(mergedContent);
			writer.Close();
			return true;
		    }
		} catch (Exception ex) {
		    Console.Out.WriteLine("Error in File Name " + file.Name);
		    Console.Out.WriteLine(ex);
		    Log.Add("Error in File Name " + file.Name + ":" + ex.ToString());
		} finally {
		    //if (fs != null) {
		    //    fs.Close();
		    //}
		}

		return false;
	    }
	}

    }
}
