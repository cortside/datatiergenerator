using System;

namespace Spring2.DataTierGenerator.Parser {
    /// <summary>
    /// Summary description for ParserValidationArgs.
    /// </summary>
    public class ParserValidationMessage {

	private ParserValidationSeverity severity;
	private String message;

	public ParserValidationMessage(ParserValidationSeverity severity, String message) {
	    this.severity = severity;
	    this.message = message;
	}

	public static ParserValidationMessage NewError(String message) {
	    return new ParserValidationMessage(ParserValidationSeverity.ERROR, message);
	}

	public static ParserValidationMessage NewWarning(String message) {
	    return new ParserValidationMessage(ParserValidationSeverity.WARNING, message);
	}

	public static ParserValidationMessage NewInfo(String message) {
	    return new ParserValidationMessage(ParserValidationSeverity.INFO, message);
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
