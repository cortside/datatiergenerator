using System;
using NUnit.Framework;

using Seamless.Manhattan.Types;

namespace Golf.Tournament.Test {

    public class ExpYearEnumTest : TestCase {

	public ExpYearEnumTest(String name) : base(name) { }

	protected override void SetUp() {
	}

	public void Test_ExpYearEnum() {
	    foreach(ExpYearEnum o in ExpYearEnum.Options) {
		Assert("ExpYearEnum could not be retrieved by Code)", o.Equals(ExpYearEnum.GetInstance(o.Code)));
	    }
	    Assert("Function should have been invalid", !ExpYearEnum.GetInstance("xxx").IsValid);
	}

    }
}
