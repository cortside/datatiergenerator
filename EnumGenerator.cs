using System;
using System.Data;
using System.IO;
using System.Collections;
using System.Text;

namespace Spring2.DataTierGenerator {

    public class EnumGenerator : GeneratorBase {
	private EnumType type;

	public EnumGenerator(Configuration options, EnumType type) : base(options, null) {
	    this.type = type;
	}

	public void Generate() {
	    StringBuilder sb = new StringBuilder();

	    sb.Append("using System;\n");
	    sb.Append("using System.Collections;\n");
	    sb.Append("\n");
	    sb.Append("namespace ").Append(options.GetTypeNameSpace(type.Name)).Append(" {\n");
	    sb.Append("\n");
	    sb.Append("\tpublic class ").Append(type.Name).Append(" : ").Append(options.EnumBaseClass).Append(" {\n");
	    sb.Append("\t\n");
	    sb.Append("\t\tprivate static readonly IList OPTIONS = new ArrayList();\n");
	    sb.Append("\t\t\n");
	    sb.Append("\t\tpublic static readonly new ").Append(type.Name).Append(" DEFAULT = new ").Append(type.Name).Append("();\n");
	    sb.Append("\t\tpublic static readonly new ").Append(type.Name).Append(" UNSET = new ").Append(type.Name).Append("();\n");
	    sb.Append("\t\t\n");

	    foreach (EnumValue value in type.Values) {
		sb.Append("\t\tpublic static readonly ").Append(type.Name).Append(" ").Append(value.Name.ToUpper()).Append(" = new ").Append(type.Name).Append("(\"").Append(value.Code).Append("\", \"").Append(value.Name).Append("\");\n");
	    }

	    sb.Append("\t\t\n");
	    sb.Append("\t\tpublic static ").Append(type.Name).Append(" GetInstance(Object value) {\n");
	    sb.Append("\t\t\tif (value is String) {\n");
	    sb.Append("\t\t\t\tforeach (").Append(type.Name).Append(" t in OPTIONS) {\n");
	    sb.Append("\t\t\t\t\tif (t.Value.Equals(value)) {\n");
	    sb.Append("\t\t\t\t\t\treturn t;\n");
	    sb.Append("\t\t\t\t\t}\n");
	    sb.Append("\t\t\t\t}\n");
	    sb.Append("\t\t\t}\n");
	    sb.Append("\t\t\t\n");
	    sb.Append("\t\t\treturn UNSET;\n");
	    sb.Append("\t\t}\n");
	    sb.Append("\t\t\n");
	    sb.Append("\t\tprivate ").Append(type.Name).Append("() {\n");
	    sb.Append("\t\t}\n");
	    sb.Append("\t\t\n");
	    sb.Append("\t\tprivate ").Append(type.Name).Append("(String code, String name) {\n");
	    sb.Append("\t\t\tthis.code = code;\n");
	    sb.Append("\t\t\tthis.name = name;\n");
	    sb.Append("\t\t\tOPTIONS.Add(this);\n");
	    sb.Append("\t\t}\n");
	    sb.Append("\t\t\n");
	    sb.Append("\t\tpublic override Boolean IsDefault {\n");
	    sb.Append("\t\t\tget { return Object.ReferenceEquals(this, DEFAULT); }\n");
	    sb.Append("\t\t}\n");
	    sb.Append("\t\t\n");
	    sb.Append("\t\tpublic override Boolean IsUnset {\n");
	    sb.Append("\t\t\tget { return Object.ReferenceEquals(this, UNSET); }\n");
	    sb.Append("\t\t}\n");
	    sb.Append("\t}\n");
	    sb.Append("}\n");

	    FileInfo file = new FileInfo(options.RootDirectory + options.TypesClassDirectory + "\\" + options.GetTypeClassName(type.Name) + ".cs");
	    WriteToFile(file, sb.ToString(), false);
	}

    }
}
