using System;
using NUnit.Framework;

using Seamless.Manhattan.Types;

namespace Golf.Tournament.Test {

    public class GolferStatusEnumTest : TestCase {

	public GolferStatusEnumTest(String name) : base(name) { }

	protected override void SetUp() {
	}

	public void Test_GolferStatusEnum() {
	    foreach(GolferStatusEnum o in GolferStatusEnum.Options) {
		Assert("GolferStatusEnum could not be retrieved by Code)", o.Equals(GolferStatusEnum.GetInstance(o.Code)));
	    }
	    Assert("Function should have been invalid", !GolferStatusEnum.GetInstance("xxx").IsValid);
	}

    }
}
