using System;

namespace Spring2.DataTierGenerator.Parser {
    /// <summary>
    /// Summary description for ParserValidationArgs.
    /// </summary>
    public class ParserValidationArgs {

	private ParserValidationSeverity severity;
	private String message;

	public ParserValidationArgs(ParserValidationSeverity severity, String message) {
	    this.severity = severity;
	    this.message = message;
	}

	public static ParserValidationArgs NewError(String message) {
	    return new ParserValidationArgs(ParserValidationSeverity.ERROR, message);
	}

	public static ParserValidationArgs NewWarning(String message) {
	    return new ParserValidationArgs(ParserValidationSeverity.WARNING, message);
	}

	public static ParserValidationArgs NewInfo(String message) {
	    return new ParserValidationArgs(ParserValidationSeverity.INFO, message);
	}

	public ParserValidationSeverity Severity {
	    get { return this.severity; }
	}

	public String Message {
	    get { return this.message; }
	}

	public override String ToString() {
	    return severity.ToString() + " :: " + message;
	}

    }
}
