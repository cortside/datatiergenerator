using System;
using System.Collections;

namespace Golf.Tournament.Types {

    public class PaymentStatusEnum : Spring2.Core.Types.EnumDataType {

	private static readonly ArrayList OPTIONS = new ArrayList();

	public static readonly new PaymentStatusEnum DEFAULT = new PaymentStatusEnum();
	public static readonly new PaymentStatusEnum UNSET = new PaymentStatusEnum();

	public static readonly PaymentStatusEnum PREAUTH = new PaymentStatusEnum("PreAuth", "PreAuth");
	public static readonly PaymentStatusEnum POSTAUTH = new PaymentStatusEnum("PostAuth", "PostAuth");
	public static readonly PaymentStatusEnum SALE = new PaymentStatusEnum("Sale", "Sale");
	public static readonly PaymentStatusEnum SETTLED = new PaymentStatusEnum("Settled", "Settled");

	public static PaymentStatusEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (PaymentStatusEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private PaymentStatusEnum() {}

	private PaymentStatusEnum(String code, String name) {
	    this.code = code;
	    this.name = name;
	    OPTIONS.Add(this);
	}

	public override Boolean IsDefault {
	    get { return Object.ReferenceEquals(this, DEFAULT); }
	}

	public override Boolean IsUnset {
	    get { return Object.ReferenceEquals(this, UNSET); }
	}

	public static IList Options {
	    get { return OPTIONS; }
	}
    }
}
