using System;
using System.Xml;

using System.CodeDom;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.IO;
using System.Text;
using System.Collections;

using NUnit.Framework;

using Spring2.Core.Util;

using Spring2.DataTierGenerator.Parser;
using Spring2.DataTierGenerator.Generator;
using Spring2.DataTierGenerator.Generator.Writer;

namespace Spring2.DataTierGenerator.Test {

    [TestFixture]
    public class ParserTest {

	[Test]
	public void Writers() {
	    // make sure output directory exists
	    Directory.CreateDirectory(".\\SanityTest\\CodeDomMerge");

	    CSharpWriter("Vendor.cs", "Vendor.generated.cs", "Vendor.cs.cmp");
	    CSharpWriter("GolferDAO.cs", "GolferDAO.generated.cs", "GolferDAO.cs.cmp");
	}

	private void CSharpWriter(String original, String generated, String compare) {
	    // copy "original" to ouput directory
	    FileInfo file1 = new FileInfo("..\\..\\src\\Test\\CodeDomMerge\\" + original);
	    file1.CopyTo(".\\SanityTest\\CodeDomMerge\\" + original, true);

	    // get handle to file
	    file1 = new FileInfo(".\\SanityTest\\CodeDomMerge\\" + original);
	    FileInfo file2 = new FileInfo("..\\..\\src\\Test\\CodeDomMerge\\" + generated);
	    FileInfo file3 = new FileInfo("..\\..\\src\\Test\\CodeDomMerge\\" + compare);

	    // write/merge files
	    CSharpCodeWriter writer = new CSharpCodeWriter();
	    writer.Write(file1, file2.OpenText().ReadToEnd());

	    // make sure new file matches compare file
	    if (!file3.OpenText().ReadToEnd().Equals(file1.OpenText().ReadToEnd())) {
		Console.Out.WriteLine(Environment.NewLine + file1.OpenText().ReadToEnd());
		Assertion.Fail();
	    }
	}

    }
}
