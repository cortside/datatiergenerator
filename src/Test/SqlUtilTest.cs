using System;
using NUnit.Framework;
using Spring2.DataTierGenerator.Tool;

namespace Spring2.DataTierGenerator.Test {

    /// <summary>
    /// Summary description for SqlUtilTest.
    /// </summary>
    [TestFixture]
    public class SqlUtilTest {

	[Test]
	public void Escape() {
	    MSSqlServerTool tool = new MSSqlServerTool();
	    Assertion.AssertEquals("[Contains]", tool.Escape("Contains"));
	    Assertion.AssertEquals("Contain", tool.Escape("Contain"));
	    Assertion.AssertEquals("[Patient Id]", tool.Escape("Patient Id"));
	    Assertion.AssertEquals("[Foo/Bar]", tool.Escape("Foo/Bar"));
	}
    }
}
