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
	    if (parser.IsValid) {

		if (parser.Log.Count>0) {
		    WriteToLog("The parser is in a valid state, but reported the following issues:");
		    foreach(String s in parser.Log) {
			WriteToLog(s);
		    }
		}

		Hashtable tools = new Hashtable();
		foreach(ToolElement tool in parser.Generator.Tools) {
		    try {
			Type clazz = System.Type.GetType(tool.Class);
			if (clazz != null) {
			    Object o = System.Activator.CreateInstance(clazz);
			    tools.Add(tool.Name, o);
			} else {
			    WriteToLog("Could not find class '" + tool.Class + "', '" + tool.Name + "' will not be added to toolbox.");
			}
		    } catch (Exception ex) {
			WriteToLog("Error trying to create tool '" + tool.Name + "': " + ex.Message);
		    }
		}

		IList tasks = parser.Generator.FindTasksByElement("sqlentity");
		if (tasks.Count > 0) {
		    foreach (DatabaseElement database in parser.Databases) {
			foreach (SqlEntityElement sqlentity in database.SqlEntities) {
			    foreach(TaskElement task in tasks) {
				if (task.IsIncluded(sqlentity.Name)) {
				    Hashtable parameters = new Hashtable();
				    foreach(ParameterElement parameter in task.Parameters) {
					parameters.Add(parameter.Name, parameter.Value);
				    }

				    IGenerator g = new VelocityTask(options, database.SqlEntities, sqlentity, task, sqlentity.Name, tools, parameters);
				    g.Generate();
				    ((ArrayList)Log).AddRange(g.Log);
				}
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
			    if (task.IsIncluded(entity.Name)) {
				Hashtable parameters = new Hashtable();
				foreach(ParameterElement parameter in task.Parameters) {
				    parameters.Add(parameter.Name, parameter.Value);
				}

				IGenerator g = new VelocityTask(options, parser.Entities, entity, task, entity.Name, tools, parameters);
				g.Generate();
				((ArrayList)Log).AddRange(g.Log);
			    }
			}

			// foreach finder
		    }
		}

		tasks = parser.Generator.FindTasksByElement("enum");
		if (tasks.Count > 0) {
		    foreach (EnumElement type in parser.Enums) {
			foreach(TaskElement task in tasks) {
			    if (task.IsIncluded(type.Name)) {
				Hashtable parameters = new Hashtable();
				foreach(ParameterElement parameter in task.Parameters) {
				    parameters.Add(parameter.Name, parameter.Value);
				}

				IGenerator g = new VelocityTask(options, parser.Enums, type, task, type.Name, tools, parameters);
				g.Generate();
				((ArrayList)Log).AddRange(g.Log);
			    }
			}
		    }
		}

		tasks = parser.Generator.FindTasksByElement("collection");
		if (tasks.Count > 0) {
		    foreach (CollectionElement collection in parser.Collections) {
			foreach(TaskElement task in tasks) {
			    if (task.IsIncluded(collection.Name)) {
				Hashtable parameters = new Hashtable();
				foreach(ParameterElement parameter in task.Parameters) {
				    parameters.Add(parameter.Name, parameter.Value);
				}

				IGenerator g = new VelocityTask(options, parser.Collections, collection, task, collection.Name, tools, parameters);
				g.Generate();
				((ArrayList)Log).AddRange(g.Log);
			    }
			}
		    }
		}

		tasks = parser.Generator.FindTasksByElement("sqlentities");
		foreach(TaskElement task in tasks) {
		    Hashtable parameters = new Hashtable();
		    foreach(ParameterElement parameter in task.Parameters) {
			parameters.Add(parameter.Name, parameter.Value);
		    }

		    IGenerator g = new VelocityTask(options, DatabaseElement.GetAllSqlEntities(parser.Databases), null, task, "sqlentities", tools, parameters);;
		    g.Generate();
		    ((ArrayList)Log).AddRange(g.Log);
		}

		tasks = parser.Generator.FindTasksByElement("entities");
		foreach(TaskElement task in tasks) {
		    Hashtable parameters = new Hashtable();
		    foreach(ParameterElement parameter in task.Parameters) {
			parameters.Add(parameter.Name, parameter.Value);
		    }

		    IGenerator g = new VelocityTask(options, parser.Entities, null, task, "entities", tools, parameters);
		    g.Generate();
		    ((ArrayList)Log).AddRange(g.Log);
		}

	    } else {
		WriteToLog("Parser was not in a valid state and reported the following errors:");
		foreach(String s in parser.Log) {
		    WriteToLog(s);
		}
	    }
			
	}

    }
}

