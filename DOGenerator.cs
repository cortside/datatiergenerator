using System;
using System.Data;
using System.IO;
using System.Collections;
using System.Text;

namespace Spring2.DataTierGenerator {

    public class DOGenerator : GeneratorBase {

	public DOGenerator(Configuration options, Entity entity) : base(options, entity) {
	}

	public void CreateDataObjectClass() {
	    StringBuilder sb = new StringBuilder(4096);
			
	    // Create the header for the class
	    sb.Append(GetUsingNamespaces(false)).Append("\n");

	    // class definition
	    sb.Append("namespace " + options.GetDONameSpace(entity.Name) + " {\n");
	    sb.Append("\tpublic class " + options.GetDOClassName(entity.Name) + " : Spring2.Core.DataObject.DataObject {\n\n");

	    // declaration of private member variables
	    foreach (Field field in entity.Fields) {
		if (field.Name.IndexOf('.')<0) {
		    sb.Append("\t\t").Append(field.AccessModifier).Append(" ");
		    sb.Append(field.Type.Name);
		    sb.Append(" ").Append(field.GetFieldFormat()).Append(" = ");
		    sb.Append(field.Type.NewInstanceFormat).Append(";\n");
		}
	    }
	    sb.Append("\n");

	    // accessor methods
	    foreach (Field field in entity.Fields) {
		if (field.Name.IndexOf('.')<0) {
		    if (field.Description.Length>0) {
			sb.Append("\t\t/// <summary>\n");
			sb.Append("\t\t/// ").Append(field.Description).Append("\n");
			sb.Append("\t\t/// </summary>\n");
		    }

		    sb.Append("\t\tpublic ");
		    sb.Append(field.Type.Name);
		    sb.Append(" ").Append(field.GetMethodFormat()).Append(" {\n");
		    sb.Append("\t\t\tget { return this.").Append(field.GetFieldFormat()).Append("; }\n");
		    sb.Append("\t\t\tset { this.").Append(field.GetFieldFormat()).Append(" = value; }\n");
		    sb.Append("\t\t}\n\n");
		}
	    }
		
	    // Close out the class and namespace
	    sb.Append("\t}\n");
	    sb.Append("}\n");

	    WriteToFile(options.RootDirectory + options.DoClassDirectory + "\\" + options.GetDOClassName(entity.Name) + ".cs", sb.ToString(), false);
	}

    }
}
