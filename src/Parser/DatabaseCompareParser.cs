using System;

using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Element;

namespace Spring2.DataTierGenerator.Parser {
    /// <summary>
    /// A parser that will compare information that can be parsed from a database PLUS the information that can be pulled from the parser passed in
    /// </summary>
    public class DatabaseCompareParser : ParserSkeleton, IParser {

	private String connectionString;

	public DatabaseCompareParser(String connectionString, IParser parser) {
	    this.connectionString = connectionString;
	}

    }
}
