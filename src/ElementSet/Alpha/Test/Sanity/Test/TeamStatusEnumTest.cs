using System;
using NUnit.Framework;

using Seamless.Manhattan.Types;

namespace Golf.Tournament.Test {

    public class TeamStatusEnumTest : TestCase {

	public TeamStatusEnumTest(String name) : base(name) { }

	protected override void SetUp() {
	}

	public void Test_TeamStatusEnum() {
	    foreach(TeamStatusEnum o in TeamStatusEnum.Options) {
		Assert("TeamStatusEnum could not be retrieved by Code)", o.Equals(TeamStatusEnum.GetInstance(o.Code)));
	    }
	    Assert("Function should have been invalid", !TeamStatusEnum.GetInstance("xxx").IsValid);
	}

    }
}
