using System;
using System.Data;
using System.IO;
using System.Collections;

using org.apache.velocity;
using org.apache.velocity.app;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Element;
using Spring2.DataTierGenerator.Generator;
using Spring2.DataTierGenerator.Util;

namespace Spring2.DataTierGenerator.Generator.Task {

    internal class VelocityTask : GeneratorSkeleton, IGenerator {
	private Object element;
	private Element.Task task;
	private String name;

	// NOTES:  instead of a single object, a velocity context could be passed or a hashtable.
	// Might want to support an ITask interface that has an execute and uses a writer

	public VelocityTask(Configuration options, Object element, Element.Task task, String name) : base(options) {
	    this.element = element;
	    this.task = task;
	    this.name = name;
	}

	public override void Generate() {
	    IndentableStringWriter writer = new IndentableStringWriter();

	    VelocityContext vc = new VelocityContext();
	    vc.put("dtgversion", this.GetType().Assembly.FullName);
	    vc.put("options", options);
	    vc.put("element", element);

	    Template template = Velocity.GetTemplate(task.Template);
	    template.merge(vc, writer);

	    FileInfo file = new FileInfo(options.RootDirectory + task.Directory + "\\" + String.Format(task.FileNameFormat, name));
	    if (writer.ToString().Length > 0) {
		WriteToFile(file, writer.ToString(), false);
	    }
	}

    }
}
