using System;
using System.Collections;

namespace Golf.Tournament.Types {

    public class USStateEnum : Spring2.Core.Types.EnumDataType {

	private static readonly ArrayList OPTIONS = new ArrayList();

	public static readonly new USStateEnum DEFAULT = new USStateEnum();
	public static readonly new USStateEnum UNSET = new USStateEnum();

	public static readonly USStateEnum ALABAMA = new USStateEnum("AL", "Alabama");
	public static readonly USStateEnum ALASKA = new USStateEnum("AK", "Alaska");
	public static readonly USStateEnum ARIZONA = new USStateEnum("AZ", "Arizona");
	public static readonly USStateEnum ARKANSAS = new USStateEnum("AR", "Arkansas");
	public static readonly USStateEnum CALIFORNIA = new USStateEnum("CA", "California");
	public static readonly USStateEnum COLORADO = new USStateEnum("CO", "Colorado");
	public static readonly USStateEnum CONNECTICUT = new USStateEnum("CT", "Connecticut");
	public static readonly USStateEnum DELAWARE = new USStateEnum("DE", "Delaware");
	public static readonly USStateEnum DISTRICT_OF_COLUMBIA = new USStateEnum("DC", "District of Columbia");
	public static readonly USStateEnum FLORIDA = new USStateEnum("FL", "Florida");
	public static readonly USStateEnum GEORGIA = new USStateEnum("GA", "Georgia");
	public static readonly USStateEnum HAWAII = new USStateEnum("HI", "Hawaii");
	public static readonly USStateEnum IDAHO = new USStateEnum("ID", "Idaho");
	public static readonly USStateEnum ILLINOIS = new USStateEnum("IL", "Illinois");
	public static readonly USStateEnum INDIANA = new USStateEnum("IN", "Indiana");
	public static readonly USStateEnum IOWA = new USStateEnum("IA", "Iowa");
	public static readonly USStateEnum KANSAS = new USStateEnum("KS", "Kansas");
	public static readonly USStateEnum KENTUCKY = new USStateEnum("KY", "Kentucky");
	public static readonly USStateEnum LOUISIANA = new USStateEnum("LA", "Louisiana");
	public static readonly USStateEnum MAINE = new USStateEnum("ME", "Maine");
	public static readonly USStateEnum MARYLAND = new USStateEnum("MD", "Maryland");
	public static readonly USStateEnum MASSACHUSETTS = new USStateEnum("MA", "Massachusetts");
	public static readonly USStateEnum MICHIGAN = new USStateEnum("MI", "Michigan");
	public static readonly USStateEnum MINNESOTA = new USStateEnum("MN", "Minnesota");
	public static readonly USStateEnum MISSISSIPPI = new USStateEnum("MS", "Mississippi");
	public static readonly USStateEnum MISSOURI = new USStateEnum("MO", "Missouri");
	public static readonly USStateEnum MONTANA = new USStateEnum("MT", "Montana");
	public static readonly USStateEnum NEBRASKA = new USStateEnum("NE", "Nebraska");
	public static readonly USStateEnum NEVADA = new USStateEnum("NV", "Nevada");
	public static readonly USStateEnum NEW_HAMPSHIRE = new USStateEnum("NH", "New Hampshire");
	public static readonly USStateEnum NEW_JERSEY = new USStateEnum("NJ", "New Jersey");
	public static readonly USStateEnum NEW_MEXICO = new USStateEnum("NM", "New Mexico");
	public static readonly USStateEnum NEW_YORK = new USStateEnum("NY", "New York");
	public static readonly USStateEnum NORTH_CAROLINA = new USStateEnum("NC", "North Carolina");
	public static readonly USStateEnum NORTH_DAKOTA = new USStateEnum("ND", "North Dakota");
	public static readonly USStateEnum OHIO = new USStateEnum("OH", "Ohio");
	public static readonly USStateEnum OKLAHOMA = new USStateEnum("OK", "Oklahoma");
	public static readonly USStateEnum OREGON = new USStateEnum("OR", "Oregon");
	public static readonly USStateEnum PENNSYLVANIA = new USStateEnum("PA", "Pennsylvania");
	public static readonly USStateEnum RHODE_ISLAND = new USStateEnum("RI", "Rhode Island");
	public static readonly USStateEnum SOUTH_CAROLINA = new USStateEnum("SC", "South Carolina");
	public static readonly USStateEnum SOUTH_DAKOTA = new USStateEnum("SD", "South Dakota");
	public static readonly USStateEnum TENNESSEE = new USStateEnum("TN", "Tennessee");
	public static readonly USStateEnum TEXAS = new USStateEnum("TX", "Texas");
	public static readonly USStateEnum UTAH = new USStateEnum("UT", "Utah");
	public static readonly USStateEnum VERMONT = new USStateEnum("VT", "Vermont");
	public static readonly USStateEnum VIRGINIA = new USStateEnum("VA", "Virginia");
	public static readonly USStateEnum WASHINGTON = new USStateEnum("WA", "Washington");
	public static readonly USStateEnum WEST_VIRGINIA = new USStateEnum("WV", "West Virginia");
	public static readonly USStateEnum WISCONSIN = new USStateEnum("WI", "Wisconsin");
	public static readonly USStateEnum WYOMING = new USStateEnum("WY", "Wyoming");

	public static USStateEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (USStateEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private USStateEnum() {}

	private USStateEnum(String code, String name) {
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
