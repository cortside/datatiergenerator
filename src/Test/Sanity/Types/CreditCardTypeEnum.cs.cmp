using System;
using System.Collections;

namespace Golf.Tournament.Types {

    public class CreditCardTypeEnum : Spring2.Core.Types.EnumDataType {

	private static readonly ArrayList OPTIONS = new ArrayList();

	public static readonly new CreditCardTypeEnum DEFAULT = new CreditCardTypeEnum();
	public static readonly new CreditCardTypeEnum UNSET = new CreditCardTypeEnum();

	public static readonly CreditCardTypeEnum AMERICAN_EXPRESS = new CreditCardTypeEnum("AMEX", "American Express");
	public static readonly CreditCardTypeEnum CARTE_BLANCHE = new CreditCardTypeEnum("CBLN", "Carte Blanche");
	public static readonly CreditCardTypeEnum DINERS_CLUB = new CreditCardTypeEnum("DCCB", "Diners Club");
	public static readonly CreditCardTypeEnum DISCOVER = new CreditCardTypeEnum("DISC", "Discover");
	public static readonly CreditCardTypeEnum ENROUTE = new CreditCardTypeEnum("ENRT", "Enroute");
	public static readonly CreditCardTypeEnum JAL = new CreditCardTypeEnum("JAL", "JAL");
	public static readonly CreditCardTypeEnum JCB = new CreditCardTypeEnum("JCB", "JCB");
	public static readonly CreditCardTypeEnum MASTERCARD = new CreditCardTypeEnum("MC", "MasterCard");
	public static readonly CreditCardTypeEnum VISA = new CreditCardTypeEnum("VISA", "Visa");

	public static CreditCardTypeEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (CreditCardTypeEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private CreditCardTypeEnum() {}

	private CreditCardTypeEnum(String code, String name) {
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
