using System;
using NUnit.Framework;

using Seamless.Manhattan.Types;

namespace Golf.Tournament.Test {

    public class FunctionEnumTest : TestCase {

	public FunctionEnumTest(String name) : base(name) { }

	protected override void SetUp() {
	}

	public void Test_FunctionEnum() {
	    foreach(FunctionEnum o in FunctionEnum.Options) {
		Assert("FunctionEnum could not be retrieved by ToInt32() value)", o.Equals(FunctionEnum.GetInstance(o.ToInt32())));
		Assert("FunctionEnum could not be retrieved by Code)", o.Equals(FunctionEnum.GetInstance(o.Code)));
	    }
	    Assert("Function should have been invalid", !FunctionEnum.GetInstance("xxx").IsValid);
	    Assert("Function should have been invalid", !FunctionEnum.GetInstance(-69).IsValid);
	}

    }
}
