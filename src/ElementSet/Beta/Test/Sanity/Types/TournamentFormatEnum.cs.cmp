using System;
using System.Collections;

namespace Golf.Tournament.Types {

    public class TournamentFormatEnum : Spring2.Core.Types.EnumDataType {

	private static readonly ArrayList OPTIONS = new ArrayList();

	public static readonly new TournamentFormatEnum DEFAULT = new TournamentFormatEnum();
	public static readonly new TournamentFormatEnum UNSET = new TournamentFormatEnum();

	public static readonly TournamentFormatEnum INDIVIDUAL = new TournamentFormatEnum("I", "Individual");
	public static readonly TournamentFormatEnum SCRAMBLE = new TournamentFormatEnum("S", "Scramble");
	public static readonly TournamentFormatEnum BEST_BALL = new TournamentFormatEnum("B", "Best Ball");

	public static TournamentFormatEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (TournamentFormatEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private TournamentFormatEnum() {}

	private TournamentFormatEnum(String code, String name) {
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
