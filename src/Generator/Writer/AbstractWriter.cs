using System;
using System.Collections;

namespace Spring2.DataTierGenerator.Generator.Writer {
    /// <summary>
    /// Summary description for AbstractWriter.
    /// </summary>
    public abstract class AbstractWriter {

	private ArrayList log = new ArrayList();

	protected void WriteToLog(String s) {
	    log.Add(s);
	}

	public IList Log {
	    get { return log; }
	}

    }
}
