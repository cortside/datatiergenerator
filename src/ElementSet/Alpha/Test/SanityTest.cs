using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;

using NUnit.Framework;

using Spring2.Core.Util;

using Spring2.DataTierGenerator.Parser;
using Spring2.DataTierGenerator.Generator;

namespace Spring2.DataTierGenerator.Test {

    /// <summary>
    /// Tests that should provide confidence that the DTG is generating source as expected
    /// </summary>
    [TestFixture]
    public class SanityTest {

	[Test]
	public void TestSanity() {
	    Execute(@"..\..\src\ElementSet\Alpha\Test\Sanity", @"..\..\src\ElementSet\Alpha\Test\sanity.xml", @".\SanityTest");
	}

	[Test]
	public void TestRegressSanity() {
	    Execute(@"..\..\src\ElementSet\Alpha\Test\Regress", @"..\..\src\ElementSet\Alpha\Test\regress.xml", @".\RegressTest");
	}

	[Test]
	public void TestSUSanity() {
	    Execute(@"..\..\src\ElementSet\Alpha\Test\SU", @"..\..\src\ElementSet\Alpha\Test\su.xml", @".\SU");
	}

	private void Execute(String root, String configFilename, String outputPath) {
	    IParser parser = new XmlParser();
	    parser.Parse(configFilename);
	    if (!parser.IsValid) {
		String message = String.Empty;
		foreach(String s in parser.Log) {
		    Console.Out.WriteLine(s);
		    message += s + "\n";
		}
		Assert.Fail(message);
	    } else {
		IGenerator gen = new NVelocityGenerator();
		if (Directory.Exists(outputPath)) {
		    Directory.Delete(outputPath, true);
		}
		gen.Generate(parser);
		String message = String.Empty;
		foreach(String s in gen.Log) {
		    Console.Out.WriteLine(s);
		    message += s + "\n";
		}

		if (message.IndexOf("ERROR") >=0) {
		    Assert.Fail(message);
		}

		CompareResults(root, outputPath);
	    }
	}

	private void CompareResults(String compareRoot, String outputPath) {
	    Boolean pass = true;
	    String message = String.Empty;

	    IList files = new StringCollection();
	    // get list of files from compare root and add to list
	    // get list of files from build root and add to list if not already in list

	    // add new files from compare directory
	    FindFiles(compareRoot, compareRoot, files);
	    FindFiles(outputPath, outputPath, files);

	    if (files.Count == 0) {
		throw new FileNotFoundException("No files to compare were found.");
	    }

	    foreach(String filename in files) {
		Boolean p = CompareFile(outputPath, compareRoot, filename);
		pass = pass & p;
		if (!p) {
		    message += filename + "...FAIL" + Environment.NewLine;
		}
	    }

	    if (!pass) {
		Assert.Fail("more than 1 output file did not match it's compare file.  Sanity is just an illusion.\n" + message);
	    }
	}


	/// <summary>
	/// recursive method to find files
	/// </summary>
	/// <param name="root"></param>
	/// <param name="directory"></param>
	/// <param name="files"></param>
	private void FindFiles(String root, String directory, IList files) {
	    foreach(String filename in Directory.GetFiles(directory)) {
		String f = filename.Substring(root.Length+1);
		if (!files.Contains(f)) {
		    files.Add(f);
		}
	    }

	    foreach(String d in Directory.GetDirectories(directory)) {
		if (!d.EndsWith("CVS")) {
		    FindFiles(root, d, files);
		}
	    }
	}

	private Boolean CompareFile(String outputPath, String compareRoot, String filename) {
	    if (!isMatch(outputPath + "\\" + filename, compareRoot + "\\" + filename)) {
		Console.Out.WriteLine(filename + "...FAIL");
		return false;
	    }
	    Console.Out.WriteLine(filename + "...PASS");
	    return true;
	}


	protected internal virtual bool isMatch(String filename1, String filename2) {
	    Boolean SHOW_RESULTS = true;

	    System.String result = StringUtil.fileContentsToString(filename1);
	    System.String compare = StringUtil.fileContentsToString(filename2);
			
	    Boolean equals = result.Equals(compare);
	    if (!equals && SHOW_RESULTS) {
		Console.Out.WriteLine(filename1 + "/" + filename2 + " :: ");
		String[] cmp = compare.Split(Environment.NewLine.ToCharArray());
		String[] res = result.Split(Environment.NewLine.ToCharArray());

		IEnumerator cmpi = cmp.GetEnumerator();
		IEnumerator resi = res.GetEnumerator();
		Int32 line =0;
		while (cmpi.MoveNext() && resi.MoveNext()) {
		    line++;
		    String s1 = resi.Current.ToString().Replace("\t", "        ").Trim();
		    String s2 = cmpi.Current.ToString().Replace("\t", "        ").Trim();
		    if (!s1.Equals(s2)) {
			Console.Out.WriteLine(line.ToString() + " : " + cmpi.Current.ToString());
			Console.Out.WriteLine(line.ToString() + " : " + resi.Current.ToString());
		    }
		}
	    }
	    return equals;
	}


    }
}
