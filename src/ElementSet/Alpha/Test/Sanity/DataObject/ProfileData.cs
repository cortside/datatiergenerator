using System;

using Spring2.Core.Types;

using Golf.Tournament.DataObject;

namespace Golf.Tournament.DataObject {
    public class ProfileData : Spring2.Core.DataObject.DataObject {

	private IdType profileId = IdType.DEFAULT;
	private StringType userName = StringType.DEFAULT;
	private StringType password = StringType.DEFAULT;
	private GolferData golfer = new GolferData();

	public static readonly String PROFILEID = "ProfileId";
	public static readonly String USERNAME = "UserName";
	public static readonly String PASSWORD = "Password";
	public static readonly String GOLFER = "Golfer";
	public static readonly String GOLFER_GOLFERID = "Golfer.GolferId";
	public static readonly String GOLFER_FIRSTNAME = "Golfer.FirstName";
	public static readonly String GOLFER_MIDDLEINITIAL = "Golfer.MiddleInitial";
	public static readonly String GOLFER_LASTNAME = "Golfer.LastName";
	public static readonly String GOLFER_PHONE = "Golfer.Phone";
	public static readonly String GOLFER_EMAIL = "Golfer.Email";
	public static readonly String GOLFER_ADDRESS = "Golfer.Address";
	public static readonly String GOLFER_ADDRESS_ADDRESS1 = "Golfer.Address.Address1";
	public static readonly String GOLFER_ADDRESS_ADDRESS2 = "Golfer.Address.Address2";
	public static readonly String GOLFER_ADDRESS_CITY = "Golfer.Address.City";
	public static readonly String GOLFER_ADDRESS_STATE = "Golfer.Address.State";
	public static readonly String GOLFER_ADDRESS_COUNTRY = "Golfer.Address.Country";
	public static readonly String GOLFER_ADDRESS_POSTALCODE = "Golfer.Address.PostalCode";
	public static readonly String GOLFER_DATEOFBIRTH = "Golfer.DateOfBirth";
	public static readonly String GOLFER_HANDICAP = "Golfer.Handicap";
	public static readonly String GOLFER_COURSENUMBER = "Golfer.CourseNumber";
	public static readonly String GOLFER_PLAYERNUMBER = "Golfer.PlayerNumber";
	public static readonly String GOLFER_GENDER = "Golfer.Gender";
	public static readonly String GOLFER_GOLFERSTATUS = "Golfer.GolferStatus";

	public IdType ProfileId {
	    get { return this.profileId; }
	    set { this.profileId = value; }
	}

	public StringType UserName {
	    get { return this.userName; }
	    set { this.userName = value; }
	}

	public StringType Password {
	    get { return this.password; }
	    set { this.password = value; }
	}

	public GolferData Golfer {
	    get { return this.golfer; }
	    set { this.golfer = value; }
	}

    }
}
