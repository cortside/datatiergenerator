using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;

using NVelocity;
using NVelocity.App;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Element;
using Spring2.DataTierGenerator.Generator.Writer;
using Spring2.DataTierGenerator.Parser;
using Spring2.DataTierGenerator.Util;

namespace Spring2.DataTierGenerator.Generator {

    public class GeneratorTaskManager : GeneratorSkeleton, IGenerator {
	private IParser parser;
	private Hashtable tools = new Hashtable();

	public GeneratorTaskManager(IParser parser) {
	    this.parser = parser;
	    this.options = parser.Configuration;

	    InitNVelocity();

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
	}


	/// <summary>
	/// Init the NVelocity singleton
	/// </summary>
	private void InitNVelocity() {
	    Velocity.SetProperty(NVelocity.Runtime.RuntimeConstants_Fields.FILE_RESOURCE_LOADER_CACHE, true);
	    Velocity.Init();
	}

	
	public override void Generate() {
	    if (parser.IsValid) {

		if (parser.Log.Count>0) {
		    WriteToLog("The parser is in a valid state, but reported the following issues:");
		    foreach(String s in parser.Log) {
			WriteToLog(s);
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

				    GenerateFile(database.SqlEntities, sqlentity, task, parameters);
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

				GenerateFile(parser.Entities, entity, task, parameters);
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

				GenerateFile(parser.Enums, type, task, parameters);
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

				GenerateFile(parser.Collections, collection, task, parameters);
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

		    GenerateFile(DatabaseElement.GetAllSqlEntities(parser.Databases), null, task, parameters);;
		}

		tasks = parser.Generator.FindTasksByElement("entities");
		foreach(TaskElement task in tasks) {
		    Hashtable parameters = new Hashtable();
		    foreach(ParameterElement parameter in task.Parameters) {
			parameters.Add(parameter.Name, parameter.Value);
		    }

		    GenerateFile(parser.Entities, null, task, parameters);
		}
	    } else {
		WriteToLog("Parser was not in a valid state and reported the following errors:");
		foreach(String s in parser.Log) {
		    WriteToLog(s);
		}
	    }
	}


	private void GenerateFile(IList elements, ElementSkeleton element, TaskElement task, Hashtable parameters) {
	    IndentableStringWriter writer = new IndentableStringWriter();

	    VelocityContext vc = new VelocityContext();
	    foreach(Object key in tools.Keys) {
		vc.Put(key.ToString(), tools[key]);
	    }
	    foreach(Object key in parameters.Keys) {
		vc.Put(key.ToString(), parameters[key]);
	    }
	    vc.Put("dtgversion", this.GetType().Assembly.FullName);
	    vc.Put("options", options);
	    vc.Put("element", element);
	    vc.Put("elements", elements);

	    Template template = Velocity.GetTemplate("Template\\dtg_csharp_library.vm");
	    template = Velocity.GetTemplate("Template\\dtg_java_library.vm");
	    template = Velocity.GetTemplate(task.Template);
	    template.Merge(vc, writer);

	    FileInfo file = new FileInfo(options.RootDirectory + task.Directory + "\\" + String.Format(task.FileNameFormat, element.Name));
	    String content = writer.ToString();
	    if (content.Length > 0) {
		IWriter w = WriterFactory.GetWriter(task.Writer);
		if (w.Write(file, content)) {
		    WriteToLog(w.Log);
		    WriteToLog("generating " + file.FullName);
		}
	    }
	}

    }
}

