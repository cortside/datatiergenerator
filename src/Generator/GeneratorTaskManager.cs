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
using Spring2.DataTierGenerator.Generator.Task;

namespace Spring2.DataTierGenerator.Generator {

    public class GeneratorTaskManager : GeneratorSkeleton, IGenerator {
	private IParser parser;

	public GeneratorTaskManager(IParser parser) {
	    this.parser = parser;
	    this.options = parser.Configuration;
	}

	public override void Generate() {
	    //Console.Out.Write("\n");
	    //Console.Out.WriteLine(String.Empty.PadLeft(20,'='));

	    if (parser.IsValid) {

		if (parser.HasWarnings) {
		    Console.Out.WriteLine("The parser is in a valid state, but reported the following issues:\n" + parser.ErrorDescription);
		}

		IList tasks = parser.Generator.FindTasksByElement("sqlentity");
		if (tasks.Count > 0) {
		    foreach (DatabaseElement database in parser.Databases) {
			foreach (SqlEntityElement sqlentity in database.SqlEntities) {
			    foreach(TaskElement task in tasks) {
				IGenerator g = new VelocityTask(options, database.SqlEntities, sqlentity, task, sqlentity.Name);
				g.Generate();
			    }

			    // foreach database
			    // foreach index
			    // foreach constraint
			}
		    }
		}

		tasks = parser.Generator.FindTasksByElement("entity");
		if (tasks.Count > 0) {
		    foreach (EntityElement entity in parser.Entities) {
			foreach(TaskElement task in tasks) {
			    IGenerator g = new VelocityTask(options, parser.Entities, entity, task, entity.Name);
			    g.Generate();
			}

			// foreach finder
		    }
		}

		tasks = parser.Generator.FindTasksByElement("enum");
		if (tasks.Count > 0) {
		    foreach (EnumElement type in parser.Enums) {
			foreach(TaskElement task in tasks) {
			    IGenerator g = new VelocityTask(options, parser.Enums, type, task, type.Name);
			    g.Generate();
			}
		    }
		}

		tasks = parser.Generator.FindTasksByElement("collection");
		if (tasks.Count > 0) {
		    foreach (CollectionElement collection in parser.Collections) {
			foreach(TaskElement task in tasks) {
			    IGenerator g = new VelocityTask(options, parser.Collections, collection, task, collection.Name);
			    g.Generate();
			}
		    }
		}

		tasks = parser.Generator.FindTasksByElement("sqlentities");
		foreach(TaskElement task in tasks) {
		    IGenerator g = new VelocityTask(options, DatabaseElement.GetAllSqlEntities(parser.Databases), null, task, "sqlentities");
		    g.Generate();
		}

		tasks = parser.Generator.FindTasksByElement("entities");
		foreach(TaskElement task in tasks) {
		    IGenerator g = new VelocityTask(options, parser.Entities, null, task, "entities");
		    g.Generate();
		}

	    } else {
		Console.Out.WriteLine("Parser was not in a valid state and reported the following errors:\n" + parser.ErrorDescription);
	    }
			
	    //Console.Out.WriteLine(String.Empty.PadLeft(20,'='));
	}

    }
}

