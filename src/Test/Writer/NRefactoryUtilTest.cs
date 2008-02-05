using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Spring2.DataTierGenerator.Generator.Writer;

namespace Spring2.DataTierGenerator.Test.Test.Writer {
    [TestFixture]
    public class NRefactoryUtilTest {

	#region code snippets
	private static readonly string CODE1= @"using System;
using System.Text;

/// <summary>
/// this is a namespace comment
/// </summary>
namespace Test {
    class MainClass {

	/// <summary>
	/// this is for FOO
	/// </summary>
	[Generate()]
	const string FOO = " + "\"" + "bar" + "\"" + @";

	[Generate()]
	private Int32 variable = 0;

	public MainClass() {
	}

	// This is the entry method of the application
	[Generate()]
	public static void Main() {
	    Console.WriteLine(" + "\"" + "Hello, World!" + "\"" + @");
	}
    }
}
";

	private static readonly string CODE2 = @"using System;
namespace Test {
    class MainClass {

	// this is comment for variable2
	[Generate()]
	private Int32 variable2 = 0;

	// This is comment for Method2
	[Generate()]
	public void Method2() {
	    Console.WriteLine(" + "\"" + "Hello, World!" + "\"" + @");
	}
    }
}
";

	private static readonly string CODE1_CODE2_MERGED = @"using System;
using System.Text;

/// <summary>
/// this is a namespace comment
/// </summary>
namespace Test {
    class MainClass {

	public MainClass() {
	}

	// this is comment for variable2
	[Generate()]
	private Int32 variable2 = 0;

	// This is comment for Method2
	[Generate()]
	public void Method2() {
	    Console.WriteLine(" + "\"" + "Hello, World!" + "\"" + @");
	}
    }
}
";

	private static readonly string CODE3 = @"using System;
namespace Test {
    class MainClass {

	// This is comment for Method2
	[Generate()]
	public void Method2() {
	    bool foo1 = true;
	    bool foo2 = true;
	    if (foo1 || foo2) {
		Console.WriteLine(" + "\"" + "Hello, World!" + "\"" + @");
	    }
	}
    }
}
";

	private static readonly string CODE4 = @"using System;
namespace Test {
    class MainClass {

	// This is comment for Method2
	[Generate()]
	public void Method2() {
	    // this comment is inside the method body

	    // above this comment should be a blank line
	    Console.WriteLine(" + "\"" + "Hello, World!" + "\"" + @");
	}
    }
}
";

	private static readonly string CODE5 = @"using System;
namespace Test {
    class MainClass {

	#region this is a region
	// This is comment for Method2
	[Generate()]
	public void Method2() {
	    Console.WriteLine(" + "\"" + "Hello, World!" + "\"" + @");
	}
	#endregion
    }
}
";

	private static readonly string CODE6 = @"using System;
namespace Test {
    class MainClass {

	public void Method2() {
	    if (1 == 1) {
		Console.WriteLine(" + "\"" + "Hello, World!" + "\"" + @");
	    } else {
		Console.WriteLine(" + "\"" + "Hello, World!" + "\"" + @");
	    }
	}
    }
}
";

	private static readonly string CODE7 = @"using System;
namespace Test {
    class MainClass {

	public void Method2() {
	    try {
		Console.WriteLine(" + "\"" + "Hello, World!" + "\"" + @");
	    } catch (Exception ex) {
		Console.WriteLine(ex);
	    }
	}
    }
}
";

	private static readonly string CODE8 = @"using System;
namespace Test {
    class MainClass {

	public Int32 Property1 {
	    get {
		Int32 i = 0;
		return i;
	    }
	}
    }
}
";

	private static readonly string CODE9 = @"using System;
namespace Test {
    class MainClass {

	public Int32 Property1 {
	    get {
		return 0;
	    }
	}
    }
}
";

	private static readonly string CODE9_OUTPUT = @"using System;
namespace Test {
    class MainClass {

	public Int32 Property1 {
	    get { return 0; }
	}
    }
}
";


	private static readonly string CODE10 = @"using System;
namespace Test {
    class MainClass {

	[Generate()]
	private Foo foo = 1;

	[Generate()]
	public Int32 Property1 {
	    get { return 0; }
	}

	[Generate()]
	public void GeneratedMethod() {
	    // some generated method
	}
    }
}
";

	private static readonly string CODE10_OUTPUT = @"using System;
namespace Test {
    class MainClass {
    }
}
";

	private static readonly string CODE11 = @"using System;
namespace Test {
    class MainClass {

	#region some region
	public void Foo() {
	}
	#endregion
    }
}
";

	#endregion

	[Test]
	[Ignore("code 1 has generated members and non-generated -- they will be removed")]
	public void RightMergeWithBlank() {
	    String output = NRefactoryUtil.Merge(CODE1, "");
	    Assert.AreEqual(CODE1, output, output);
	}

	[Test]
	public void LeftMergeWithBlank() {
	    String output = NRefactoryUtil.Merge("", CODE1);
	    Assert.AreEqual(CODE1, output, output);
	}

	[Test]
	[Ignore ("code 1 has generated members and non-generated -- order will be different")]
	public void MergeSame() {
	    String output = NRefactoryUtil.Merge(CODE1, CODE1);
	    Assert.AreEqual(CODE1, output, output);
	}

	[Test]
	public void MergeSameClassWithDifferentMembers() {
	    String output = NRefactoryUtil.Merge(CODE1, CODE2);
	    Assert.AreEqual(CODE1_CODE2_MERGED, output, output);
	}

	[Test]
	public void ShouldParseAndOutputSameSource() {
	    SourceUnit unit = new SourceUnit(CODE1);
	    String output = unit.ToCSharp();
	    Assert.AreEqual(CODE1, output, output);
	}

	[Test]
	public void ShouldParseOrWithinIfStatement() {
	    SourceUnit unit = new SourceUnit(CODE3);
	    String output = unit.ToCSharp();
	    Assert.AreEqual(CODE3, output, output);
	}

	[Test]
	public void ShouldReformatSource() {
	    Char seperator = (Char)26;
	    String code = NRefactoryUtil.RemoveTrailingBlankLines(CODE1).Replace(Environment.NewLine, seperator.ToString());
	    String[] lines = code.Split(seperator);
	    StringBuilder sb = new StringBuilder();
	    foreach(String line in lines) {
	    	sb.AppendLine(line.Trim());
	    }
	    SourceUnit unit = new SourceUnit(sb.ToString());
	    
	    String output = unit.ToCSharp();
	    Assert.AreEqual(CODE1, output, output);
	}

	[Test]
	public void ShouldPreserveCommentInsideMethodBody() {
	    SourceUnit unit = new SourceUnit(CODE4);
	    String output = unit.ToCSharp();
	    Assert.AreEqual(CODE4, output, output);
	}

	[Test]
	public void ShouldPreserveRegionsAroundMembers() {
	    SourceUnit unit = new SourceUnit(CODE5);
	    String output = unit.ToCSharp();
	    Assert.AreEqual(CODE5, output, output);
	}

	[Test]
	public void ShouldPreserveElseFormatting() {
	    SourceUnit unit = new SourceUnit(CODE6);
	    String output = unit.ToCSharp();
	    Assert.AreEqual(CODE6, output, output);
	}

	[Test]
	public void ShouldPreserveCatchFormatting() {
	    SourceUnit unit = new SourceUnit(CODE7);
	    String output = unit.ToCSharp();
	    Assert.AreEqual(CODE7, output, output);
	}

	[Test]
	public void ShouldPreserveMultipleLineProperty() {
	    SourceUnit unit = new SourceUnit(CODE8);
	    String output = unit.ToCSharp();
	    Assert.AreEqual(CODE8, output, output);
	}

	/// <summary>
	/// This is different than how older versions of DTG output single line properties
	/// </summary>
	[Test]
	public void ShouldPreserveSingleLineProperty() {
	    SourceUnit unit = new SourceUnit(CODE9);
	    String output = unit.ToCSharp();
	    Assert.AreEqual(CODE9_OUTPUT, output, output);
	}

	[Test]
	public void ShouldRemoveGeneratedMethods() {
	    String output = NRefactoryUtil.Merge(CODE10, "");
	    Assert.AreEqual(CODE10_OUTPUT, output, output);
	}

	[Test]
	public void ShouldKeepRegion() {
	    String output = NRefactoryUtil.Merge("", CODE11);
	    Assert.AreEqual(CODE11, output, output);
	}

    }
}
