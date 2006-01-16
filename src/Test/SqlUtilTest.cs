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
	    Assert.AreEqual("[Contains]", tool.Escape("Contains"));
	    Assert.AreEqual("Contain", tool.Escape("Contain"));
	    Assert.AreEqual("[Patient Id]", tool.Escape("Patient Id"));
	    Assert.AreEqual("[Foo/Bar]", tool.Escape("Foo/Bar"));
	}
    }
}
