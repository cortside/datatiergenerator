using System;
using System.Collections;

using Spring2.DataTierGenerator.Generator.Styler;

namespace Spring2.DataTierGenerator.Generator.Styler {

    /// <summary>
    /// Styler that does not modify the original input
    /// </summary>
    public class BaseStyler {

	private ArrayList log = new ArrayList();

	public IList Log {
	    get { return log; }
	}

	protected void WriteToLog(String s) {
	    log.Add(s);
	}

    }
}
