using System;
using System.IO;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Spring2.Core.Util;
using Spring2.DataTierGenerator.Generator.Styler;

namespace Spring2.DataTierGenerator.Test.Test {
    /// <summary>
    /// Summary description for StylerTest.
    /// </summary>
    [TestFixture]
    public class StylerTest {

	[Test]
	public void RegexPerformance() {
	    String src = "        public static Invoice GetInstance(IdType invoiceId) {";

	    Timer timer = new Timer();
	    for(Int32 i=0; i<10000; i++) {
		Regex.IsMatch(src, @"(^|\s+|\}+)else if(\s+|\(+|$)");
	    }
	    timer.Stop();
	    Console.Out.WriteLine(timer.TimeSpan.TotalMilliseconds);

	    timer.Start();
	    Regex regex = new Regex(@"(^|\s+|\}+)else if(\s+|\(+|$)");
	    for(Int32 i=0; i<10000; i++) {
		regex.IsMatch(src);
	    }
	    timer.Stop();
	    Console.Out.WriteLine(timer.TimeSpan.TotalMilliseconds);

	    timer.Start();
	    for(Int32 i=0; i<10000; i++) {
		src.Trim().StartsWith("}");
	    }
	    timer.Stop();
	    Console.Out.WriteLine(timer.TimeSpan.TotalMilliseconds);

	    StylerLine line = new StylerLine(src, null);
	    timer.Start();
	    for(Int32 i=0; i<10000; i++) {
		Boolean foo = line.IsClosingBrace;
	    }
	    timer.Stop();
	    Console.Out.WriteLine(timer.TimeSpan.TotalMilliseconds);
	}

	[Test]
	public void StylePerformance() {
	    FileInfo file = new FileInfo(@"c:\data\work\seamlessweb\manhattan\src\businesslogic\invoice.cs");
	    String contents = file.OpenText().ReadToEnd();
	    CSharpStyler styler = new CSharpStyler();
	    Timer timer = new Timer();
	    for(Int32 i=0; i< 10; i++) {
		styler.Style(contents);
	    }
	    timer.Stop();
	
	    // before changes, best I saw for 10 iterations was 13500ms
	    Console.Out.WriteLine(timer.TimeSpan.TotalMilliseconds);
	}
    }
}
