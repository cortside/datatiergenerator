using System;
using System.Data;
using System.IO;
using System.Collections;

using NVelocity;
using NVelocity.App;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Element;
using Spring2.DataTierGenerator.Generator;
using Spring2.DataTierGenerator.Util;

namespace Spring2.DataTierGenerator.Generator.Task {

    internal class VelocityTask : GeneratorSkeleton, IGenerator {
	private IList elements;
	private Object element;
	private TaskElement task;
	private String name;

	// NOTES:  instead of a single object, a velocity context could be passed or a hashtable.
	// Might want to support an ITask interface that has an execute and uses a writer

	public VelocityTask(Configuration options, IList elements, Object element, TaskElement task, String name) : base(options) {
	    this.element = element;
	    this.task = task;
	    this.name = name;
	    this.elements = elements;
	}

	public override void Generate() {
	    IndentableStringWriter writer = new IndentableStringWriter();

	    VelocityContext vc = new VelocityContext();
	    vc.Put("dtgversion", this.GetType().Assembly.FullName);
	    vc.Put("options", options);
	    vc.Put("element", element);
	    vc.Put("elements", elements);

	    Template template = Velocity.GetTemplate("Template\\dtg_csharp_library.vm");
	    template = Velocity.GetTemplate("Template\\dtg_java_library.vm");
	    template = Velocity.GetTemplate(task.Template);
	    template.Merge(vc, writer);

	    FileInfo file = new FileInfo(options.RootDirectory + task.Directory + "\\" + String.Format(task.FileNameFormat, name));
	    if (writer.ToString().Length > 0) {
		WriteToFile(file, writer.ToString(), false);
	    }
	}

    }
}
