using System;
using System.Collections;

namespace Golf.Tournament.Types {

    public class TeamStatusEnum : Spring2.Core.Types.EnumDataType {

	private static readonly ArrayList OPTIONS = new ArrayList();

	public static readonly new TeamStatusEnum DEFAULT = new TeamStatusEnum();
	public static readonly new TeamStatusEnum UNSET = new TeamStatusEnum();

	public static readonly TeamStatusEnum NEW = new TeamStatusEnum("N", "New");
	public static readonly TeamStatusEnum CONFIRMED = new TeamStatusEnum("C", "Confirmed");
	public static readonly TeamStatusEnum CANCELLED = new TeamStatusEnum("X", "Cancelled");

	public static TeamStatusEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (TeamStatusEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private TeamStatusEnum() {}

	private TeamStatusEnum(String code, String name) {
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
