using System;
using System.Collections;

using Spring2.DataTierGenerator.Generator.Styler;

namespace Spring2.DataTierGenerator.Generator.Styler {

    /// <summary>
    /// Styler that does not modify the original input
    /// </summary>
    public class NoStyler : BaseStyler, IStyler {

	/// <summary>
	/// Create an instance of NoStyler with the default options.
	/// </summary>
	public NoStyler() {
	}

	/// <summary>
	/// Create an instance of NoStyler specifying some or all of the options.
	/// </summary>
	/// <param name="options"></param>
	public NoStyler(Hashtable options) {
	}

	/// <summary>
	/// Style the input.
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public String Style(String input) {
	    return input;
	}

    }
}
