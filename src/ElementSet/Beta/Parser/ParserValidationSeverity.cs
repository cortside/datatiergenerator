using System;

namespace Spring2.DataTierGenerator.Parser {
    /// <summary>
    /// Summary description for ParserValidationSeverity.
    /// </summary>
    public class ParserValidationSeverity {

	public static readonly ParserValidationSeverity ERROR = new ParserValidationSeverity("ERROR");
	public static readonly ParserValidationSeverity WARNING = new ParserValidationSeverity("WARNING");
	public static readonly ParserValidationSeverity INFO = new ParserValidationSeverity("INFO");

	private String s;

	private ParserValidationSeverity(String s) {
	    this.s = s;
	}

	public override String ToString() {
	    return s;
	}

    }
}
