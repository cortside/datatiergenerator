using System;
using System.Data;
using System.IO;
using System.Collections;
using System.Text;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Attribute;
using Spring2.DataTierGenerator.Element;
using Spring2.DataTierGenerator.Util;

namespace Spring2.DataTierGenerator.Generator {

    public abstract class GeneratorSkeleton : IGenerator {

	protected Configuration options;
	private IList log = new ArrayList();

	public GeneratorSkeleton() {
	}

	public GeneratorSkeleton(Configuration options) {
	    this.options = options;
	}

	public abstract void Generate();

	protected void WriteToLog(String s) {
	    log.Add(s);
	}

	protected void WriteToLog(IList list) {
	    foreach(String s in list) {
		log.Add(s);
	    }
	}

	public IList Log {
	    get { return log; }
	}

    }
}
