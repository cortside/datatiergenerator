using System;
using System.Collections;

namespace Golf.Tournament.Types {

    public class TeamSizeEnum : Spring2.Core.Types.EnumDataType {

	private static readonly ArrayList OPTIONS = new ArrayList();

	public static readonly new TeamSizeEnum DEFAULT = new TeamSizeEnum();
	public static readonly new TeamSizeEnum UNSET = new TeamSizeEnum();

	public static readonly TeamSizeEnum INDIVIDUAL = new TeamSizeEnum("1", "Individual");
	public static readonly TeamSizeEnum TWO_PERSON = new TeamSizeEnum("2", "Two Person");
	public static readonly TeamSizeEnum FOUR_PERSON = new TeamSizeEnum("4", "Four Person");

	public static TeamSizeEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (TeamSizeEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private TeamSizeEnum() {}

	private TeamSizeEnum(String code, String name) {
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
