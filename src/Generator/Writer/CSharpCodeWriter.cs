using System;
using System.IO;

namespace Spring2.DataTierGenerator.Generator.Writer {

    /// <summary>
    /// Writer for C# code that is merged using CodeDom
    /// </summary>
    public class CSharpCodeWriter : AbstractWriter, IWriter {

	public CSharpCodeWriter() {
	}

	public Boolean Write(FileInfo file, String contents) {
	    MemoryStream stream = new MemoryStream();
	    StreamWriter sw = new StreamWriter(stream);
	    sw.Write(contents);
	    sw.Flush();
	    stream.Position = 0;

	    if (!file.Exists) {
		CodeUnit unit1 = null;
		try {
		    unit1 = new CodeUnit(file.Name, stream, Log);
		    StreamWriter writer = new StreamWriter(file.FullName, false);
		    writer.Write(unit1.Generate());
		    writer.Close();
		    return true;
		} catch (Exception ex) {
		    Console.Out.WriteLine(ex);
		    Log.Add(ex.ToString());
		    return false;
		}
	    } else {
		FileStream fs = null;
		CodeUnit unit1 = null;
		CodeUnit unit2 = null;
		try {
		    fs = file.OpenRead();
		    unit1 = new CodeUnit(file.Name, fs, Log);
		    fs.Close();
		    fs = null;
		    unit2 = new CodeUnit(file.Name, stream, Log);
		    unit1.Merge(unit2);

		    StreamReader sr = file.OpenText();
		    String exitingContents = sr.ReadToEnd();
		    sr.Close();
		    if (!unit1.Generate().Equals(exitingContents)) {
			// make backup
			file.CopyTo(file.FullName + "~", true);

			// 
			StreamWriter writer = new StreamWriter(file.FullName, false);
			writer.Write(unit1.Generate());
			writer.Close();
			return true;
		    }
		} catch (Exception ex) {
		    Console.Out.WriteLine(ex);
		    Log.Add(ex.ToString());
		} finally {
		    if (fs != null) {
			fs.Close();
		    }
		}

		return false;
	    }
	}

    }
}
