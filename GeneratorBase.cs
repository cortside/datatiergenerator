using System;
using System.Data;
using System.IO;
using System.Collections;
using System.Text;
using System.Windows.Forms;

namespace Spring2.DataTierGenerator {
    public class GeneratorBase {

	protected Configuration options;
	protected Entity entity;

	protected GeneratorBase() {
	}

	public GeneratorBase(Configuration options, Entity entity) {
	    this.options = options;
	    this.entity = entity;
	}

	/// <summary>
	/// Helper method to write generated source to file.  Directory will be created if it does not already exist.
	/// </summary>
	/// <param name="fileName">name of file, including full path</param>
	/// <param name="text">what to write to the file</param>
	/// <param name="append">whether or not or overwrite the file or to append to file</param>
	protected void WriteToFile(String filename, String text, Boolean append) {
	    String directory = filename.Substring(0,filename.LastIndexOf('\\'));
	    if (!Directory.Exists(directory))
		Directory.CreateDirectory(directory);
		text.Trim();

		Boolean write = true;
		if (File.Exists(filename)) {
			StreamReader r = File.OpenText(filename);
			String s = r.ReadToEnd();
			r.Close();
			s.Trim();
			if (s.Equals(text)) {
				write=false;
			}
		}

		if (write) {
			StreamWriter writer = new StreamWriter(filename, append);
			writer.Write(text);
			writer.Close();
		}
	}


	public String GetUsingNamespaces(Boolean isDaoClass) {
	    Hashtable namespaces = new Hashtable();
	    namespaces.Add("System", "System");

	    if (isDaoClass) {
		namespaces.Add("System.Data", "System.Data");
		namespaces.Add("System.Data.SqlClient", "System.Data.SqlClient");
		namespaces.Add("System.Configuration", "System.Configuration");
		namespaces.Add("System.Collections", "System.Collections");
		namespaces.Add("Spring2.Core.DAO", "Spring2.Core.DAO");
		namespaces.Add(options.GetDONameSpace(null), options.GetDONameSpace(null));
	    }

	    foreach (Field field in entity.Fields) {
		if (field.Name.IndexOf('.')<0) {
		    if (!field.Type.Package.Equals(String.Empty) && !namespaces.Contains(field.Type.Package)) {
			namespaces.Add(field.Type.Package, field.Type.Package);
		    }
		}
	    }

	    StringBuilder sb = new StringBuilder();
	    foreach (Object o in namespaces.Keys) {
		sb.Append("using ").Append(o.ToString()).Append(";\n");
	    }
	    return sb.ToString();
	}

    }

}
