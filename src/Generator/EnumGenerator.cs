using System;
using System.Data;
using System.IO;
using System.Collections;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Element;
using Spring2.DataTierGenerator.Util;

namespace Spring2.DataTierGenerator.Generator {

    internal class EnumGenerator : GeneratorSkeleton, IGenerator {
	private EnumElement type;

	public EnumGenerator(Configuration options, EnumElement type) : base(options) {
	    this.type = type;
	}

	/// <summary>
	/// 
	/// </summary>
	public override void Generate() {
	    IndentableStringWriter writer = new IndentableStringWriter();

	    writer.WriteLine("using System;");
	    writer.WriteLine("using System.Collections;");
	    writer.WriteLine();
	    writer.WriteLine("namespace " + options.GetTypeNameSpace(type.Name) + " {");
	    writer.WriteLine();
	    writer.WriteLine(1, "public class " + type.Name + " : " + options.EnumBaseClass + " {");
	    writer.WriteLine();
	    writer.WriteLine(2, "private static readonly ArrayList OPTIONS = new ArrayList();");
	    writer.WriteLine();
	    writer.WriteLine(2, "public static readonly new " + type.Name + " DEFAULT = new " + type.Name + "();");
	    writer.WriteLine(2, "public static readonly new " + type.Name + " UNSET = new " + type.Name + "();");
	    writer.WriteLine();

	    foreach (EnumValueElement value in type.Values) {
		if (!value.Description.Equals(String.Empty)) {
		    writer.WriteLine(2, "/// <summary>");
		    writer.WriteLine(2, "/// " + value.Description);
		    writer.WriteLine(2, "/// </summary>");
		}
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

	    if (type.IntegerBased) {
		writer.WriteLine(3, "if (value is Int32) {");
		writer.WriteLine(4, "foreach (" + type.Name + " t in OPTIONS) {");
		writer.WriteLine(5, "try {");
		writer.WriteLine(6, "if (Int32.Parse(t.Code).Equals(value)) {");
		writer.WriteLine(7, "return t;");
		writer.WriteLine(6, "}");
		writer.WriteLine(5, "} catch (Exception) {");
		writer.WriteLine(6, "// parse exception - continue");
		writer.WriteLine(5, "}");
		writer.WriteLine(4, "}");
		writer.WriteLine(3, "}");
	    }

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
	    writer.WriteLine();
	    writer.WriteLine(2, "public static IList Options {");
	    writer.WriteLine(3, "get { return OPTIONS; }");
	    writer.WriteLine(2, "}");

	    if (type.IntegerBased) {
		writer.WriteLine();
		writer.WriteLine(2, "/// <summary>");
		writer.WriteLine(2, "/// Convert a " + type.Name + " instance to an Int32;");
		writer.WriteLine(2, "/// </summary>");
		writer.WriteLine(2, "/// <returns>the Int32 representation for the enum instance.</returns>");
		writer.WriteLine(2, "/// <exception cref=\"InvalidCastException\">when converting DEFAULT or UNSET to an Int32.</exception>");
		writer.WriteLine(2, "public Int32 ToInt32() {");
		writer.WriteLine(3, "if (IsValid) {");
		writer.WriteLine(4, "try {");
		writer.WriteLine(5, "return Int32.Parse(code);");
		writer.WriteLine(4, "} catch (Exception) {");
		writer.WriteLine(5, "// parse error  - don't do anything - an acceptable exception will be thrown below");
		writer.WriteLine(4, "}");
		writer.WriteLine(3, "}");
		writer.WriteLine();
		writer.WriteLine(3, "// instance was !IsValid or there was a parser error");
		writer.WriteLine(3, "throw new InvalidCastException(); ");
		writer.WriteLine(2, "}");
	    }

	    writer.WriteLine(1, "}");
	    writer.WriteLine("}");

	    FileInfo file = new FileInfo(options.RootDirectory + options.TypesClassDirectory + "\\" + options.GetTypeClassName(type.Name) + ".cs");
	    WriteToFile(file, writer.ToString(), false);
	}

    }
}
