using System;
using System.Collections;

namespace Golf.Tournament.Types {

    public class ExpYearEnum : Spring2.Core.Types.EnumDataType {

	private static readonly ArrayList OPTIONS = new ArrayList();

	public static readonly new ExpYearEnum DEFAULT = new ExpYearEnum();
	public static readonly new ExpYearEnum UNSET = new ExpYearEnum();

	public static readonly ExpYearEnum YEAR_2002 = new ExpYearEnum("2002", "YEAR_2002");
	public static readonly ExpYearEnum YEAR_2003 = new ExpYearEnum("2003", "YEAR_2003");
	public static readonly ExpYearEnum YEAR_2004 = new ExpYearEnum("2004", "YEAR_2004");
	public static readonly ExpYearEnum YEAR_2005 = new ExpYearEnum("2005", "YEAR_2005");
	public static readonly ExpYearEnum YEAR_2006 = new ExpYearEnum("2006", "YEAR_2006");
	public static readonly ExpYearEnum YEAR_2007 = new ExpYearEnum("2007", "YEAR_2007");
	public static readonly ExpYearEnum YEAR_2008 = new ExpYearEnum("2008", "YEAR_2008");

	public static ExpYearEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (ExpYearEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private ExpYearEnum() {}

	private ExpYearEnum(String code, String name) {
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
