using System;
using NUnit.Framework;

using Seamless.Manhattan.Types;

namespace Golf.Tournament.Test {

    public class USStateEnumTest : TestCase {

	public USStateEnumTest(String name) : base(name) { }

	protected override void SetUp() {
	}

	public void Test_USStateEnum() {
	    foreach(USStateEnum o in USStateEnum.Options) {
		Assert("USStateEnum could not be retrieved by Code)", o.Equals(USStateEnum.GetInstance(o.Code)));
	    }
	    Assert("Function should have been invalid", !USStateEnum.GetInstance("xxx").IsValid);
	}

    }
}
