using System;

using Spring2.Core.Types;

using Golf.Tournament.DataObject;
using Golf.Tournament.Types;

namespace Golf.Tournament.DataObject {
    public class TournamentData : Spring2.Core.DataObject.DataObject {

	private IdType tournamentId = IdType.DEFAULT;
	private StringType name = StringType.DEFAULT;
	private StringType description = StringType.DEFAULT;
	private IntegerType numberOfTeams = IntegerType.DEFAULT;
	private TeamSizeEnum teamSize = TeamSizeEnum.DEFAULT;
	private TournamentFormatEnum format = TournamentFormatEnum.DEFAULT;
	private DateType registrationBeginDate = DateType.DEFAULT;
	private DateType registrationEndDate = DateType.DEFAULT;
	private DateType cancellationCutoffDate = DateType.DEFAULT;
	private CurrencyType registrationFee = CurrencyType.DEFAULT;
	private StringType registrationFeeDescription = StringType.DEFAULT;
	private StringType datesText = StringType.DEFAULT;
	private StringType prizesText = StringType.DEFAULT;
	private StringType sponsorsText = StringType.DEFAULT;
	private StringType locationsText = StringType.DEFAULT;
	private OrganizerData organizer = new OrganizerData();
	private IntegerType registeredParticipants = IntegerType.DEFAULT;
	private IntegerType maximumHandicap = IntegerType.DEFAULT;
	private TeamCollection teams = TeamCollection.DEFAULT;
	private ParticipantCollection participants = ParticipantCollection.DEFAULT;
	private StringType host = StringType.DEFAULT;
	private BooleanType showPercentFull = BooleanType.DEFAULT;
	private BooleanType showParticipants = BooleanType.DEFAULT;
	private TournamentFeeCollection fees = TournamentFeeCollection.DEFAULT;

	public static readonly String TOURNAMENTID = "TournamentId";
	public static readonly String NAME = "Name";
	public static readonly String DESCRIPTION = "Description";
	public static readonly String NUMBEROFTEAMS = "NumberOfTeams";
	public static readonly String TEAMSIZE = "TeamSize";
	public static readonly String FORMAT = "Format";
	public static readonly String REGISTRATIONBEGINDATE = "RegistrationBeginDate";
	public static readonly String REGISTRATIONENDDATE = "RegistrationEndDate";
	public static readonly String CANCELLATIONCUTOFFDATE = "CancellationCutoffDate";
	public static readonly String REGISTRATIONFEE = "RegistrationFee";
	public static readonly String REGISTRATIONFEEDESCRIPTION = "RegistrationFeeDescription";
	public static readonly String DATESTEXT = "DatesText";
	public static readonly String PRIZESTEXT = "PrizesText";
	public static readonly String SPONSORSTEXT = "SponsorsText";
	public static readonly String LOCATIONSTEXT = "LocationsText";
	public static readonly String ORGANIZER = "Organizer";
	public static readonly String ORGANIZER_ORGANIZERID = "Organizer.OrganizerId";
	public static readonly String ORGANIZER_NAME = "Organizer.Name";
	public static readonly String ORGANIZER_ADDRESS = "Organizer.Address";
	public static readonly String ORGANIZER_ADDRESS_ADDRESS1 = "Organizer.Address.Address1";
	public static readonly String ORGANIZER_ADDRESS_ADDRESS2 = "Organizer.Address.Address2";
	public static readonly String ORGANIZER_ADDRESS_CITY = "Organizer.Address.City";
	public static readonly String ORGANIZER_ADDRESS_STATE = "Organizer.Address.State";
	public static readonly String ORGANIZER_ADDRESS_COUNTRY = "Organizer.Address.Country";
	public static readonly String ORGANIZER_ADDRESS_POSTALCODE = "Organizer.Address.PostalCode";
	public static readonly String ORGANIZER_ORGANIZERCONTACT = "Organizer.OrganizerContact";
	public static readonly String ORGANIZER_ORGANIZERCONTACT_NAME = "Organizer.OrganizerContact.Name";
	public static readonly String ORGANIZER_ORGANIZERCONTACT_PHONE = "Organizer.OrganizerContact.Phone";
	public static readonly String ORGANIZER_ORGANIZERCONTACT_EMAIL = "Organizer.OrganizerContact.Email";
	public static readonly String ORGANIZER_TECHNICALCONTACT = "Organizer.TechnicalContact";
	public static readonly String ORGANIZER_TECHNICALCONTACT_NAME = "Organizer.TechnicalContact.Name";
	public static readonly String ORGANIZER_TECHNICALCONTACT_PHONE = "Organizer.TechnicalContact.Phone";
	public static readonly String ORGANIZER_TECHNICALCONTACT_EMAIL = "Organizer.TechnicalContact.Email";
	public static readonly String REGISTEREDPARTICIPANTS = "RegisteredParticipants";
	public static readonly String MAXIMUMHANDICAP = "MaximumHandicap";
	public static readonly String TEAMS = "Teams";
	public static readonly String PARTICIPANTS = "Participants";
	public static readonly String HOST = "Host";
	public static readonly String SHOWPERCENTFULL = "ShowPercentFull";
	public static readonly String SHOWPARTICIPANTS = "ShowParticipants";
	public static readonly String FEES = "Fees";

	public IdType TournamentId {
	    get { return this.tournamentId; }
	    set { this.tournamentId = value; }
	}

	public StringType Name {
	    get { return this.name; }
	    set { this.name = value; }
	}

	public StringType Description {
	    get { return this.description; }
	    set { this.description = value; }
	}

	public IntegerType NumberOfTeams {
	    get { return this.numberOfTeams; }
	    set { this.numberOfTeams = value; }
	}

	public TeamSizeEnum TeamSize {
	    get { return this.teamSize; }
	    set { this.teamSize = value; }
	}

	public TournamentFormatEnum Format {
	    get { return this.format; }
	    set { this.format = value; }
	}

	public DateType RegistrationBeginDate {
	    get { return this.registrationBeginDate; }
	    set { this.registrationBeginDate = value; }
	}

	public DateType RegistrationEndDate {
	    get { return this.registrationEndDate; }
	    set { this.registrationEndDate = value; }
	}

	public DateType CancellationCutoffDate {
	    get { return this.cancellationCutoffDate; }
	    set { this.cancellationCutoffDate = value; }
	}

	public CurrencyType RegistrationFee {
	    get { return this.registrationFee; }
	    set { this.registrationFee = value; }
	}

	public StringType RegistrationFeeDescription {
	    get { return this.registrationFeeDescription; }
	    set { this.registrationFeeDescription = value; }
	}

	public StringType DatesText {
	    get { return this.datesText; }
	    set { this.datesText = value; }
	}

	public StringType PrizesText {
	    get { return this.prizesText; }
	    set { this.prizesText = value; }
	}

	public StringType SponsorsText {
	    get { return this.sponsorsText; }
	    set { this.sponsorsText = value; }
	}

	public StringType LocationsText {
	    get { return this.locationsText; }
	    set { this.locationsText = value; }
	}


	public OrganizerData Organizer {
	    get { return this.organizer; }
	    set { this.organizer = value; }
	}

	public IntegerType RegisteredParticipants {
	    get { return this.registeredParticipants; }
	    set { this.registeredParticipants = value; }
	}

	public IntegerType MaximumHandicap {
	    get { return this.maximumHandicap; }
	    set { this.maximumHandicap = value; }
	}

	/// <summary>
	/// List of TeamData objects.
	/// </summary>
	public TeamCollection Teams {
	    get { return this.teams; }
	    set { this.teams = value; }
	}

	/// <summary>
	/// List of ParticipantData objects.
	/// </summary>
	public ParticipantCollection Participants {
	    get { return this.participants; }
	    set { this.participants = value; }
	}

	/// <summary>
	/// This is the string that will be parsed from the request to determine which tournament is being asked for.
	/// </summary>
	public StringType Host {
	    get { return this.host; }
	    set { this.host = value; }
	}

	public BooleanType ShowPercentFull {
	    get { return this.showPercentFull; }
	    set { this.showPercentFull = value; }
	}

	public BooleanType ShowParticipants {
	    get { return this.showParticipants; }
	    set { this.showParticipants = value; }
	}

	public TournamentFeeCollection Fees {
	    get { return this.fees; }
	    set { this.fees = value; }
	}
    }
}
