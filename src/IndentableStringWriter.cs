using System;
using System.IO;

namespace Spring2.DataTierGenerator {
    /// <summary>
    /// Summary description for FileWriter.
    /// </summary>
    public class IndentableStringWriter : StringWriter {

	private static readonly String TABS = "\t\t\t\t\t\t\t\t\t\t";

	public void WriteLine(Int32 level, String s) {
	    WriteLine(Indent(level) + s);
	}

	public void Write(Int32 level, String s) {
	    Write(Indent(level) + s);
	}

	private String Indent(Int32 level) {
	    String indent = TABS.Substring(0, level / 2);
	    if (level % 2 == 1) {
		indent += "    ";
	    }

	    return indent;
	}
    }
}
