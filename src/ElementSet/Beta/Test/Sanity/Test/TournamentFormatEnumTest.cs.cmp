using System;
using NUnit.Framework;

using Seamless.Manhattan.Types;

namespace Golf.Tournament.Test {

    public class TournamentFormatEnumTest : TestCase {

	public TournamentFormatEnumTest(String name) : base(name) { }

	protected override void SetUp() {
	}

	public void Test_TournamentFormatEnum() {
	    foreach(TournamentFormatEnum o in TournamentFormatEnum.Options) {
		Assert("TournamentFormatEnum could not be retrieved by Code)", o.Equals(TournamentFormatEnum.GetInstance(o.Code)));
	    }
	    Assert("Function should have been invalid", !TournamentFormatEnum.GetInstance("xxx").IsValid);
	}

    }
}
