using System;
using NUnit.Framework;

using Seamless.Manhattan.Types;

namespace Golf.Tournament.Test {

    public class ExpMonthEnumTest : TestCase {

	public ExpMonthEnumTest(String name) : base(name) { }

	protected override void SetUp() {
	}

	public void Test_ExpMonthEnum() {
	    foreach(ExpMonthEnum o in ExpMonthEnum.Options) {
		Assert("ExpMonthEnum could not be retrieved by Code)", o.Equals(ExpMonthEnum.GetInstance(o.Code)));
	    }
	    Assert("Function should have been invalid", !ExpMonthEnum.GetInstance("xxx").IsValid);
	}

    }
}
