using System;
using System.Data;
using System.IO;
using System.Collections;
using System.Text;

namespace Spring2.DataTierGenerator.Core {

    public class DOGenerator : GeneratorBase {
	private Entity entity;

	public DOGenerator(Configuration options, Entity entity) : base(options) {
	    this.entity = entity;
	}

	public void CreateDataObjectClass() {
	    
	    IndentableStringWriter writer = new IndentableStringWriter();
			
	    // Create the header for the class.
	    GetUsingNamespaces(writer, entity.Fields, false);

	    // Class definition.
	    writer.WriteLine(0, "namespace " + options.GetDONameSpace(entity.Name) + " {");
	    writer.Write(1, "public class " + options.GetDOClassName(entity.Name));
	    if (options.DataObjectBaseClass.Length>0) {
		writer.Write(" : " + options.DataObjectBaseClass);
	    }
	    writer.WriteLine(" {");
	    writer.WriteLine();

	    // Declaration of private member variables.
	    foreach (Field field in entity.Fields) {
		if (field.Name.IndexOf('.')<0) {
		    writer.Write(2, field.AccessModifier + " ");
		    writer.Write(field.Type.Name);
		    writer.Write(" " + field.GetFieldFormat() + " = ");
		    writer.WriteLine(field.Type.NewInstanceFormat + ";");
		}
	    }

	    // Properties.
	    foreach (Field field in entity.Fields) {
		writer.WriteLine();
		if (field.Name.IndexOf('.')<0) {
		    if (field.Description.Length>0) {
			writer.WriteLine(2, "/// <summary>");
			writer.WriteLine(2, "/// " + field.Description);
			writer.WriteLine(2, "/// </summary>");
		    }

		    writer.Write(2, "public ");
		    writer.Write(field.Type.Name);
		    writer.WriteLine(" " + field.GetMethodFormat() + " {");
		    writer.WriteLine(3, "get { return this." + field.GetFieldFormat() + "; }");
		    writer.WriteLine(3, "set { this." +  field.GetFieldFormat() + " = value; }");
		    writer.WriteLine(2, "}");
		}
	    }
		
	    // Close out the class and namespace.
	    writer.WriteLine(1, "}");
	    writer.WriteLine("}");

	    FileInfo file = new FileInfo(options.RootDirectory + options.DoClassDirectory + "\\" + options.GetDOClassName(entity.Name) + ".cs");
	    WriteToFile(file, writer.ToString(), false);
	}
    }
}
