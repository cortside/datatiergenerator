using System;
using System.Collections;

namespace Golf.Tournament.Types {

    public class ExpMonthEnum : Spring2.Core.Types.EnumDataType {

	private static readonly ArrayList OPTIONS = new ArrayList();

	public static readonly new ExpMonthEnum DEFAULT = new ExpMonthEnum();
	public static readonly new ExpMonthEnum UNSET = new ExpMonthEnum();

	public static readonly ExpMonthEnum JAN = new ExpMonthEnum("01", "Jan");
	public static readonly ExpMonthEnum FEB = new ExpMonthEnum("02", "Feb");
	public static readonly ExpMonthEnum MAR = new ExpMonthEnum("03", "Mar");
	public static readonly ExpMonthEnum APR = new ExpMonthEnum("04", "Apr");
	public static readonly ExpMonthEnum MAY = new ExpMonthEnum("05", "May");
	public static readonly ExpMonthEnum JUN = new ExpMonthEnum("06", "Jun");
	public static readonly ExpMonthEnum JUL = new ExpMonthEnum("07", "Jul");
	public static readonly ExpMonthEnum AUG = new ExpMonthEnum("08", "Aug");
	public static readonly ExpMonthEnum SEP = new ExpMonthEnum("09", "Sep");
	public static readonly ExpMonthEnum OCT = new ExpMonthEnum("10", "Oct");
	public static readonly ExpMonthEnum NOV = new ExpMonthEnum("11", "Nov");
	public static readonly ExpMonthEnum DEC = new ExpMonthEnum("12", "Dec");

	public static ExpMonthEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (ExpMonthEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private ExpMonthEnum() {}

	private ExpMonthEnum(String code, String name) {
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
