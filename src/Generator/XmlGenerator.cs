using System;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Element;
using Spring2.DataTierGenerator.Parser;
using Spring2.DataTierGenerator.Util;

namespace Spring2.DataTierGenerator.Generator {
    /// <summary>
    /// Summary description for XMLGenerator.
    /// </summary>
    public class XMLGenerator : GeneratorSkeleton, IGenerator {

	private IParser parser;

	public XMLGenerator(IParser parser) {
	    this.parser = parser;
	}

	public override void Generate() {
	    Console.Out.Write("\n");
	    Console.Out.WriteLine(String.Empty.PadLeft(20,'='));
	    Console.Out.WriteLine("Parse/Load entities and properties");
	    Console.Out.WriteLine(String.Empty.PadLeft(20,'='));

	    if (parser.IsValid) {
		// do actual work here
	    } else {
		Console.Out.WriteLine("Parser was not in a valid state and reported the following errors:\n" + parser.Errors);
	    }

	    Console.Out.WriteLine(String.Empty.PadLeft(20,'='));

//	    //	    SqlConnection connection = new SqlConnection(options.ConnectionString);
//	    //	    connection.Open();
//
//	    //form.Clear();
//	    //form.Show();
//	    ArrayList entities = DiscoverSqlEntities(connection);
//	    StringBuilder sb = new StringBuilder();
//	    sb.Append("<sqlentities>\n");
//	    foreach (SqlEntity sqlentity in entities) {
//		sb.Append("  <sqlentity name=\"").Append(sqlentity.Name).Append("\"");
//		sb.Append(" view=\"").Append(sqlentity.View).Append("\"");
//		sb.Append(">\n");
//
//		// columns
//		sb.Append("    <columns>\n");
//		foreach (Column column in sqlentity.Columns) {
//		    sb.Append("      ").Append(column.ToXml()).Append("\n");
//		}
//		sb.Append("    </columns>\n");
//
//		// constraints
//		if (sqlentity.Constraints.Count>0) {
//		    sb.Append("    <constraints>\n");
//		    foreach (Constraint constraint in sqlentity.Constraints) {
//			sb.Append("      ").Append(constraint.ToXml()).Append("\n");
//		    }
//		    sb.Append("    </constraints>\n");
//		}
//
//		// indexes
//		if (sqlentity.Indexes.Count>0) {
//		    sb.Append("    <indexes>\n");
//		    foreach (Index index in sqlentity.Indexes) {
//			sb.Append("      ").Append(index.ToXml()).Append("\n");
//		    }
//		    sb.Append("    </indexes>\n");
//		}
//
//		sb.Append("  </sqlentity>\n");
//	    }
//	    sb.Append("</sqlentities>\n");
//
//	    Console.Out.WriteLine(sb.ToString());
//	    //form.Hide();
	}

    }
}
