using System;
using System.Collections;

namespace Golf.Tournament.Types {

    public class GolferStatusEnum : Spring2.Core.Types.EnumDataType {

	private static readonly ArrayList OPTIONS = new ArrayList();

	public static readonly new GolferStatusEnum DEFAULT = new GolferStatusEnum();
	public static readonly new GolferStatusEnum UNSET = new GolferStatusEnum();

	public static readonly GolferStatusEnum AMATUER = new GolferStatusEnum("A", "Amatuer");
	public static readonly GolferStatusEnum PRO = new GolferStatusEnum("P", "Pro");

	public static GolferStatusEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (GolferStatusEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private GolferStatusEnum() {}

	private GolferStatusEnum(String code, String name) {
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
