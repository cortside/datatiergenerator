using System;
using System.Data;
using System.IO;
using System.Collections;

namespace Spring2.DataTierGenerator {

    public class CollectionGenerator : GeneratorBase {
	private Collection collection;

	public CollectionGenerator(Configuration options, Collection collection) : base(options) {
	    this.collection = collection;
	}

	public void Generate() {
	    IndentableStringWriter writer = new IndentableStringWriter();

	    writer.WriteLine("using System;");
	    writer.WriteLine();
	    writer.WriteLine("namespace " + options.GetCollectionNameSpace(collection.Name) + " {");
	    writer.WriteLine(1, "/// <summary>");
	    writer.WriteLine(1, "/// " + collection.Type + " generic collection");
	    writer.WriteLine(1, "/// </summary>");
	    writer.WriteLine(1, "public class " + collection.Name + " : System.Collections.CollectionBase {");

	    writer.WriteLine(2, "public static readonly " + collection.Name + " UNSET = new " + collection.Name + "(true);");
	    writer.WriteLine(2, "public static readonly " + collection.Name + " DEFAULT = new " + collection.Name + "(true);");
	    writer.WriteLine();
	    writer.WriteLine(2, "private Boolean immutable = false;");
	    writer.WriteLine();
	    writer.WriteLine(2, "private " + collection.Name + " (Boolean immutable) {");
	    writer.WriteLine(3, "    this.immutable = immutable;");
	    writer.WriteLine(2, "}");
	    writer.WriteLine();
	    writer.WriteLine(2, "public " + collection.Name + "() {");
	    writer.WriteLine(2, "}");
	    writer.WriteLine();
	    writer.WriteLine(2, "// Indexer implementation.");
	    writer.WriteLine(2, "public " + collection.Type + " this[int index] {");
	    writer.WriteLine(3, "get { return (" + collection.Type + ") List[index]; }");
	    writer.WriteLine(2, "}");
	    writer.WriteLine();
	    writer.WriteLine(2, "public void Add(" + collection.Type + " a) {");
	    writer.WriteLine(3, "if (!immutable) {");
	    writer.WriteLine(4, "List.Add(a);");
	    writer.WriteLine(3, "} else {");
	    writer.WriteLine(4, "throw new System.Data.ReadOnlyException();");
	    writer.WriteLine(3, "}");
	    writer.WriteLine(2, "}");
	    writer.WriteLine();
	    writer.WriteLine(2, "public void Remove(int index) {");
	    writer.WriteLine(3, "if (!immutable) {");
	    writer.WriteLine(4, "if (index > Count - 1 || index < 0) {");
	    writer.WriteLine(5, "throw new IndexOutOfRangeException();");
	    writer.WriteLine(4, "} else {");
	    writer.WriteLine(5, "List.RemoveAt(index); ");
	    writer.WriteLine(4, "}");
	    writer.WriteLine(3, "} else {");
	    writer.WriteLine(4, "throw new System.Data.ReadOnlyException();");
	    writer.WriteLine(3, "}");
	    writer.WriteLine(2, "}");
	    writer.WriteLine();
	    writer.WriteLine(1, "}");
	    writer.WriteLine("}");
	    writer.WriteLine();

	    FileInfo file = new FileInfo(options.RootDirectory + options.CollectionClassDirectory + "\\" + options.GetCollectionClassName(collection.Name) + ".cs");
	    WriteToFile(file, writer.ToString(), false);
	}

    }
}
