using System;
using NUnit.Framework;

using Seamless.Manhattan.Types;

namespace Golf.Tournament.Test {

    public class PaymentStatusEnumTest : TestCase {

	public PaymentStatusEnumTest(String name) : base(name) { }

	protected override void SetUp() {
	}

	public void Test_PaymentStatusEnum() {
	    foreach(PaymentStatusEnum o in PaymentStatusEnum.Options) {
		Assert("PaymentStatusEnum could not be retrieved by Code)", o.Equals(PaymentStatusEnum.GetInstance(o.Code)));
	    }
	    Assert("Function should have been invalid", !PaymentStatusEnum.GetInstance("xxx").IsValid);
	}

    }
}
