using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.IO;
using System.Text;
using NUnit.Framework;
using Spring2.DataTierGenerator.Generator.Writer;

namespace Spring2.DataTierGenerator.Test {

    /// <summary>
    /// Test for CodeUnit functionality
    /// </summary>
    [TestFixture]
    public class CodeUnitTest {

    	[Test]
	public void ShouldIgnoreQuotedCurlyBraces() {
    	    ParseAndCompare("..\\..\\src\\Test\\TestFiles\\RegEx.cs");
    	}
    	

    	[Test]
	public void ShouldHandleInternalClasses() {
	    ParseAndCompare("..\\..\\src\\Test\\TestFiles\\InternalClass.cs");
	}
    	

    	[Test]
	public void ShouldHandleLastMemberThatEndsInMultipleCurlyBraces() {
	    ParseAndCompare("..\\..\\src\\Test\\TestFiles\\LastMemberEndsInMultipleCurlyBraces.cs");
	}
    	

	[Test]
	public void ShouldParseClassComments() {
	    ParseAndCompare("..\\..\\src\\Test\\TestFiles\\ClassComment.cs");
	}
    	
    	[Test]
	[Ignore()]
	public void ShouldMergeNewClassComments() {
	    FileInfo file = new FileInfo("..\\..\\src\\Test\\TestFiles\\ClassComment.cs");
	    TextReader reader = file.OpenText();
	    String source = reader.ReadToEnd();
	    reader.Close();
    	
    	    MemoryStream ms2 = new MemoryStream(System.Text.Encoding.Default.GetBytes(source));
	    MemoryStream ms1 = new MemoryStream(System.Text.Encoding.Default.GetBytes(source.Replace("Summary description for ClassComment.", "Hello, World.")));
    		
	    CodeGeneratorOptions cgOptions = new CodeGeneratorOptions ();
	    ArrayList log = new ArrayList();
	    CodeUnit unit1 = new CodeUnit(file.Name, ms1, log, cgOptions);
    	    CodeUnit unit2 = new CodeUnit(file.Name, ms2, log, cgOptions);
    	    unit1.Merge(unit2);
	    OutputLog(log);
	
	    Assert.AreEqual(Strip(source), Strip(unit1.Generate()));
    	}
	
    	
    	/// <summary>
    	/// Tests to make sure that all of the files in a folder can be parsed and have the same output after parsing.
    	/// If the files have not previously been "styled", the output may be different because they may be reordered.
    	/// </summary>
    	[Test]
    	[Ignore("This is meant to test changes against a current dev folder")]
    	public void ProcessAllFilesInFolder() {
    	    String[] files = Directory.GetFiles(@"c:\data\work\UppercaseLiving\Dss\src\BusinessLogic");
    	    foreach(String filename in files) {
		if (filename.EndsWith(".cs")) {
		    try {
		    	ParseAndCompare(filename);
		    } catch (Exception ex) {
		    	Console.Out.WriteLine(filename + ": " + ex.Message);
		    }
		}
    	    }
    	}


    	private void ParseAndCompare(String filename) {
	    FileInfo file = new FileInfo(filename);
	    TextReader reader = file.OpenText();
	    String source = reader.ReadToEnd();
	    reader.Close();

	    FileStream fs = file.OpenRead();
	    CodeGeneratorOptions cgOptions = new CodeGeneratorOptions ();
	    ArrayList log = new ArrayList();
	    CodeUnit unit = new CodeUnit(file.Name, fs, log, cgOptions);
	    fs.Close();
    	    OutputLog(log);
	
    	    if (Strip(source) != Strip(unit.Generate())) {
    	    	Console.Out.WriteLine(unit.Generate());
    	    }
    	
	    Assert.AreEqual(Strip(source), Strip(unit.Generate()));
	}
    	
    	
    	private void OutputLog(IList log) {
    	    foreach(String s in log) {
    		Console.Out.WriteLine(s);
    	    }
    	}
    	
    	
    	private String Strip(String s) {
    	    StringBuilder sb = new StringBuilder();
    	    String[] lines = s.Split(Environment.NewLine.ToCharArray());
	    foreach(String line in lines) {
	    	String t = line.Replace("\t", "        ").Trim();
		if (t.Length > 0) {
			sb.Append(t).Append(Environment.NewLine);
		}
	    }
    	    return sb.ToString();
    	}
    	
    }
}
