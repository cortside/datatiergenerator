using System;
using System.Collections;

using Spring2.DataTierGenerator.Element;
using Spring2.DataTierGenerator.Generator;

namespace Spring2.DataTierGenerator.Parser {

    public class Task : ITask {

	AbstractParser parser;
	TaskElement task;

	public Task(AbstractParser parser, TaskElement task) {
	    this.parser = parser;
	    this.task = task;
	}

	public IList Elements {
	    get { 
		ArrayList list = new ArrayList();

		if (task.Element.Equals("sqlentity")) {
		    foreach (DatabaseElement database in parser.Databases) {
			foreach (SqlEntityElement sqlentity in database.SqlEntities) {
			    if (task.IsIncluded(sqlentity.Name)) {
				list.Add(sqlentity);
			    }
			}
		    }
		}

		if (task.Element.Equals("entity")) {
		    foreach (EntityElement entity in parser.Entities) {
			if (task.IsIncluded(entity.Name)) {
			    list.Add(entity);
			}
		    }
		}

		if (task.Element.Equals("enum")) {
		    foreach (EnumElement type in parser.Enums) {
			if (task.IsIncluded(type.Name)) {
			    list.Add(type);
			}
		    }
		}

		if (task.Element.Equals("collection")) {
		    foreach (CollectionElement collection in parser.Collections) {
			if (task.IsIncluded(collection.Name)) {
			    list.Add(collection);
			}
		    }
		}

		//		tasks = parser.Generator.FindTasksByElement("sqlentities");
		//		foreach(TaskElement task in tasks) {
		//		    Hashtable parameters = new Hashtable();
		//		    foreach(ParameterElement parameter in task.Parameters) {
		//			parameters.Add(parameter.Name, parameter.Value);
		//		    }
		//
		//		    GenerateFile(DatabaseElement.GetAllSqlEntities(parser.Databases), null, task, parameters);;
		//		}
		//
		//		tasks = parser.Generator.FindTasksByElement("entities");
		//		foreach(TaskElement task in tasks) {
		//		    Hashtable parameters = new Hashtable();
		//		    foreach(ParameterElement parameter in task.Parameters) {
		//			parameters.Add(parameter.Name, parameter.Value);
		//		    }
		//
		//		    GenerateFile(parser.Entities, null, task, parameters);
		//		}
		//

		return list;
	    }
	}

	public String FileNameFormat {
	    get { return task.FileNameFormat; }
	}

	public String Directory {
	    get { return ((Configuration)parser.Configuration).RootDirectory + "\\" + task.Directory; }
	}

	public Hashtable Parameters {
	    get { 
		Hashtable parameters = new Hashtable();
		foreach(ParameterElement parameter in task.Parameters) {
		    parameters.Add(parameter.Name, parameter.Value);
		}
		return parameters;
	    }
	}

	public String Writer {
	    get { return task.Writer; }
	}

	public String Template {
	    get { return task.Template; }
	}


    }
}
