using System;
using NUnit.Framework;

using Seamless.Manhattan.Types;

namespace Golf.Tournament.Test {

    public class TeamSizeEnumTest : TestCase {

	public TeamSizeEnumTest(String name) : base(name) { }

	protected override void SetUp() {
	}

	public void Test_TeamSizeEnum() {
	    foreach(TeamSizeEnum o in TeamSizeEnum.Options) {
		Assert("TeamSizeEnum could not be retrieved by Code)", o.Equals(TeamSizeEnum.GetInstance(o.Code)));
	    }
	    Assert("Function should have been invalid", !TeamSizeEnum.GetInstance("xxx").IsValid);
	}

    }
}
