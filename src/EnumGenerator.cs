using System;
using System.Data;
using System.IO;
using System.Collections;

namespace Spring2.DataTierGenerator {

    public class EnumGenerator : GeneratorBase {
	private EnumType type;

	public EnumGenerator(Configuration options, EnumType type) : base(options) {
	    this.type = type;
	}

	public void Generate() {
	    IndentableStringWriter writer = new IndentableStringWriter();

	    writer.WriteLine("using System;");
	    writer.WriteLine("using System.Collections;");
	    writer.WriteLine();
	    writer.WriteLine("namespace " + options.GetTypeNameSpace(type.Name) + " {");
	    writer.WriteLine();
	    writer.WriteLine(1, "public class " + type.Name + " : " + options.EnumBaseClass + " {");
	    writer.WriteLine();
	    writer.WriteLine(2, "private static readonly IList OPTIONS = new ArrayList();");
	    writer.WriteLine();
	    writer.WriteLine(2, "public static readonly new " + type.Name + " DEFAULT = new " + type.Name + "();");
	    writer.WriteLine(2, "public static readonly new " + type.Name + " UNSET = new " + type.Name + "();");
	    writer.WriteLine();

	    foreach (EnumValue value in type.Values) {
		writer.WriteLine(2, "public static readonly " + type.Name + " " + value.Name.Replace(' ','_').ToUpper() + " = new " + type.Name + "(\"" + value.Code + "\", \"" + value.Name + "\");");
	    }

	    writer.WriteLine();
	    writer.WriteLine(2, "public static " + type.Name + " GetInstance(Object value) {");
	    writer.WriteLine(3, "if (value is String) {");
	    writer.WriteLine(4, "foreach (" + type.Name + " t in OPTIONS) {");
	    writer.WriteLine(5, "if (t.Value.Equals(value)) {");
	    writer.WriteLine(6, "return t;");
	    writer.WriteLine(5, "}");
	    writer.WriteLine(4, "}");
	    writer.WriteLine(3, "}");
	    writer.WriteLine();
	    writer.WriteLine(3, "return UNSET;");
	    writer.WriteLine(2, "}");
	    writer.WriteLine();
	    writer.WriteLine(2, "private " + type.Name + "() {}");
	    writer.WriteLine();
	    writer.WriteLine(2, "private " + type.Name + "(String code, String name) {");
	    writer.WriteLine(3, "this.code = code;");
	    writer.WriteLine(3, "this.name = name;");
	    writer.WriteLine(3, "OPTIONS.Add(this);");
	    writer.WriteLine(2, "}");
	    writer.WriteLine();
	    writer.WriteLine(2, "public override Boolean IsDefault {");
	    writer.WriteLine(3, "get { return Object.ReferenceEquals(this, DEFAULT); }");
	    writer.WriteLine(2, "}");
	    writer.WriteLine();
	    writer.WriteLine(2, "public override Boolean IsUnset {");
	    writer.WriteLine(3, "get { return Object.ReferenceEquals(this, UNSET); }");
	    writer.WriteLine(2, "}");
	    writer.WriteLine(1, "}");
	    writer.WriteLine("}");

	    FileInfo file = new FileInfo(options.RootDirectory + options.TypesClassDirectory + "\\" + options.GetTypeClassName(type.Name) + ".cs");
	    WriteToFile(file, writer.ToString(), false);
	}

    }
}
