using System;
using NUnit.Framework;

using Seamless.Manhattan.Types;

namespace Golf.Tournament.Test {

    public class CreditCardTypeEnumTest : TestCase {

	public CreditCardTypeEnumTest(String name) : base(name) { }

	protected override void SetUp() {
	}

	public void Test_CreditCardTypeEnum() {
	    foreach(CreditCardTypeEnum o in CreditCardTypeEnum.Options) {
		Assert("CreditCardTypeEnum could not be retrieved by Code)", o.Equals(CreditCardTypeEnum.GetInstance(o.Code)));
	    }
	    Assert("Function should have been invalid", !CreditCardTypeEnum.GetInstance("xxx").IsValid);
	}

    }
}
