using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Element;
using Spring2.DataTierGenerator.Parser;
using Spring2.DataTierGenerator.Util;

using Type = Spring2.DataTierGenerator.Element.Type;

namespace Spring2.DataTierGenerator.Generator {

    public class CodeGenerator : GeneratorSkeleton, IGenerator {
	private IParser parser;

	public CodeGenerator(IParser parser) {
	    this.parser = parser;
	    this.options = parser.Configuration;
	}

	public override void Generate() {
	    Console.Out.Write("\n");
	    Console.Out.WriteLine(String.Empty.PadLeft(20,'='));
	    Console.Out.WriteLine("Parse/Load entities and properties");
	    Console.Out.WriteLine(String.Empty.PadLeft(20,'='));

	    if (parser.Generator.Tasks.Count > 0) {
		Console.Out.WriteLine("WARNING: this generator does not support tasks.");
	    }

	    if (parser.IsValid) {
		// Process each table
		foreach (Database database in parser.Databases) {
		    foreach (SqlEntity sqlentity in database.SqlEntities) {
			IGenerator g = new SqlGenerator(options, sqlentity);
			g.Generate();
		    }
		}

		foreach (Entity entity in parser.Entities) {
		    IGenerator dogen = new DataObjectGenerator(options, entity, (ArrayList)parser.Entities);
		    dogen.Generate();
		    IGenerator daogen = new DaoGenerator(options, entity);
		    daogen.Generate();
		}

		foreach (EnumType type in parser.Enums) {
		    IGenerator g = new EnumGenerator(options, type);
		    g.Generate();
		}

		foreach (Collection collection in parser.Collections) {
		    IGenerator g = new CollectionGenerator(options, collection);
		    g.Generate();
		}
	    } else {
		Console.Out.WriteLine("Parser was not in a valid state and reported the following errors:\n" + parser.Errors);
	    }
			
	    Console.Out.WriteLine(String.Empty.PadLeft(20,'='));
	}

    }
}

