using System;

using Spring2.DataTierGenerator.Generator.Writer;

namespace Spring2.DataTierGenerator.Generator.Writer {

    /// <summary>
    /// Factory that gets specific writers
    /// </summary>
    public class WriterFactory {

	private WriterFactory() {
	}

	/// <summary>
	/// Create a new writer based on language and mode
	/// </summary>
	public static IWriter GetWriter(String writer) {
	    if (writer.Equals("C#")) {
		return new CSharpCodeWriter();
	    } else if (writer.Equals("region")) {
		return new RegionMergeWriter();
	    } else if (writer.Equals("text")) {
		return new TextWriter();
	    } else {
		throw new ArgumentException("No writer defined for " + writer + ".  Valid writers are: C#, region, text.");
	    }
	}

    }
}
