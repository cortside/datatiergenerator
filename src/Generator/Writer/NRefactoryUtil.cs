using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Ast;
using ICSharpCode.NRefactory.PrettyPrinter;
using Spring2.DataTierGenerator.Generator.Styler;

namespace Spring2.DataTierGenerator.Generator.Writer {

    public class NRefactoryUtil {

	public static String Merge(String content1, String content2) {
	    SourceUnit unit1 = new SourceUnit(content1);
	    SourceUnit unit2 = new SourceUnit(content2);

	    unit1.Merge(unit2);

	    return unit1.ToCSharp();
	}

	/// <summary>
	/// Remove trailing Environment.NewLine from end of string.  Will remove multiples if they exist.
	/// </summary>
	/// <param name="source"></param>
	/// <returns></returns>
	public static String RemoveTrailingNewLine(String source) {
	    return RemoveTrailingString(source, Environment.NewLine);
	}

	/// <summary>
	/// Remove trailing string from end of a string.  Will remove multiples if they exist.
	/// </summary>
	/// <param name="source"></param>
	/// <param name="trailer"></param>
	/// <returns></returns>
	public static String RemoveTrailingString(String source, String trailer) {
	    while (source.EndsWith(trailer)) {
		source = source.Substring(0, source.Length - trailer.Length);
	    }
	    return source;
	}

	/// <summary>
	/// Remove trailing Environment.NewLine from end of string.  Spaces are trimmed at end and between Environment.NewLine.
	/// </summary>
	/// <param name="source"></param>
	/// <returns></returns>
	public static String RemoveTrailingBlankLines(String source) {
	    source = source.TrimEnd();
	    while (source.EndsWith(Environment.NewLine)) {
		source = RemoveTrailingString(source, Environment.NewLine).TrimEnd();
	    }
	    return source;
	}

	/// <summary>
	/// HACK method to replace leading spaces on a line with tabs.
	/// I am sure this could be done much better with regex.
	/// </summary>
	/// <param name="source"></param>
	/// <returns></returns>
	public static String FixSourceFormatting(String source) {
	    Char seperator = (Char)26;
	    String code = RemoveTrailingBlankLines(source).Replace(Environment.NewLine, seperator.ToString());
	    String[] lines = code.Split(seperator);

	    StringBuilder sb = new StringBuilder();
	    for (Int32 i = 0; i < lines.Length; i++) {
		String line = lines[i];

		if (i < lines.Length - 1 && line.Trim().Equals("}") && lines[i + 1].Trim().StartsWith("else")) {
		    lines[i + 1] = lines[i + 1].Insert(lines[i + 1].IndexOf("else"), "} ");
		} else if (i < lines.Length - 1 && line.Trim().Equals("}") && lines[i + 1].Trim().StartsWith("catch")) {
		    lines[i + 1] = lines[i + 1].Insert(lines[i + 1].IndexOf("catch"), "} ");
		} else {
		    sb.AppendLine(ReplaceLeadingSpacesWithTabs(line));
		}
	    }

	    return sb.ToString();
	}

	private static String ReplaceLeadingSpacesWithTabs(String line) {
	    StringBuilder sb = new StringBuilder();
	    Int32 i = 0;
	    while (i < line.Length && line[i] == ' ') {
		i++;
	    }
	    if (i < line.Length) {
		Int32 spaces;
		Int32 tabs = Math.DivRem(i, 8, out spaces);
		for (Int32 x = 0; x < tabs; x++) {
		    sb.Append("\t");
		}
		for (Int32 x = 0; x < spaces; x++) {
		    sb.Append(" ");
		}
		sb.Append(line.Trim());
	    } else {
		// line only has spaces
	    }

	    return sb.ToString();
	}

	public static void InsertSpecials(CompilationUnit unit, IList<ISpecial> specials) {
	    IOutputAstVisitor outputVisitor = new CSharpOutputVisitor();

	    using (SpecialNodesUserDataInserter.Install(specials, outputVisitor)) {
		unit.AcceptVisitor(outputVisitor, null);
	    }
	}

    }
}