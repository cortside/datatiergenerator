using System;
using System.IO;

namespace Spring2.DataTierGenerator.Util {
    /// <summary>
    /// Class that handles writing indented strings for Data Tier generator.
    /// </summary>
    public class IndentableStringWriter : StringWriter 
    {

	private static readonly String TABS = "\t\t\t\t\t\t\t\t\t\t";

	/// <summary>
	/// Writes out a line with specified indention level
	/// </summary>
	/// <param name="level">Indention Level</param>
	/// <param name="s">String to write</param>
	public void WriteLine(Int32 level, String s) 
	{
	    WriteLine(Indent(level) + s);
	}

	/// <summary>
	/// Writes out a string (no line feed) with specified indention level.
	/// </summary>
	/// <param name="level">Indention level</param>
	/// <param name="s">Line to write.</param>
	public void Write(Int32 level, String s) 
	{
	    Write(Indent(level) + s);
	}

	/// <summary>
	///  Writes out the indention
	/// </summary>
	/// <param name="level">Number of levels of indention</param>
	/// <returns>Spaces for indention</returns>
	private String Indent(Int32 level) 
	{
	    String indent = TABS.Substring(0, level / 2);
	    if (level % 2 == 1) 
	    {
		indent += "    ";
	    }

	    return indent;
	}

	/// <summary>
	/// Writes out a single line summary comment
	/// </summary>
	/// <param name="level">Indention level</param>
	/// <param name="s">String for comment</param>
	public void WriteSummaryComment(Int32 level, String s) 
	{
	    String[] a = {s};
	    WriteSummaryComment(level, a);
	}

	/// <summary>
	/// Writes out a multi line summary comment.
	/// </summary>
	/// <param name="level">Indention level</param>
	/// <param name="s">Array of strings for comment.</param>
	public void WriteSummaryComment(Int32 level, String[] s)
	{
	    WriteLine(level, "/// <summary>");
	    for (int i = 0; i < s.Length; i++) 
	    {
		WriteLine(level, "/// " + s[i]);
	    }
	    WriteLine(level, "/// </summary>");
	}

	/// <summary>
	///  Writes a parameter comment
	/// </summary>
	/// <param name="level">Indention level.</param>
	/// <param name="paramName">Name of the parameter.</param>
	/// <param name="comment">Comment for the parameter.</param>
	/// <exception cref="System.">Thrown when</exception>
	public void WriteParameterComment(Int32 level, String paramName, String comment)
	{
	    WriteLine(level, "/// <param name=\"" + paramName + "\">" + comment + "</param>");
	}

	/// <summary>
	/// Writes a returns comment
	/// </summary>
	/// <param name="level">Indention level</param>
	/// <param name="comment">comment</param>
	public void WriteReturnsComment(Int32 level, String comment)
	{
	    WriteLine(level, "/// <returns>" + comment + "</returns>");
	}

	/// <summary>
	/// Writes an exception comment
	/// </summary>
	/// <param name="level">Indention level</param>
	/// <param name="exceptionName">Exception name.</param>
	/// <param name="comment">Comment.</param>
	public void WriteExceptionComment(Int32 level, String exceptionName, String comment)
	{
	    WriteLine(level, "/// <exception cref=\"" + exceptionName + "\">" + comment + "</exception>");
	}
    }
}
