using System;
using System.Collections;

using Spring2.Core.Types;

using Golf.Tournament.DataObject;
using Golf.Tournament.Types;

namespace Golf.Tournament.DataObject {
    public class ParticipantData : Spring2.Core.DataObject.DataObject {

	private IdType participantId = IdType.DEFAULT;
	private GolferData golfer = new GolferData();
	private IList aLaCarteItems = new ArrayList();
	private BooleanType isValid = BooleanType.DEFAULT;
	private CurrencyType registrationFee = CurrencyType.DEFAULT;
	private TournamentData tournament = new TournamentData();
	private TeamData team = new TeamData();
	private GolferData golfer = new GolferData();
	private PaymentData payment = new PaymentData();

	public static readonly String PARTICIPANTID = "ParticipantId";
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
	public static readonly String ALACARTEITEMS = "ALaCarteItems";
	public static readonly String ISVALID = "IsValid";
	public static readonly String REGISTRATIONFEE = "RegistrationFee";
	public static readonly String TOURNAMENT = "Tournament";
	public static readonly String TOURNAMENT_TOURNAMENTID = "Tournament.TournamentId";
	public static readonly String TOURNAMENT_NAME = "Tournament.Name";
	public static readonly String TOURNAMENT_DESCRIPTION = "Tournament.Description";
	public static readonly String TOURNAMENT_NUMBEROFTEAMS = "Tournament.NumberOfTeams";
	public static readonly String TOURNAMENT_TEAMSIZE = "Tournament.TeamSize";
	public static readonly String TOURNAMENT_FORMAT = "Tournament.Format";
	public static readonly String TOURNAMENT_REGISTRATIONBEGINDATE = "Tournament.RegistrationBeginDate";
	public static readonly String TOURNAMENT_REGISTRATIONENDDATE = "Tournament.RegistrationEndDate";
	public static readonly String TOURNAMENT_CANCELLATIONCUTOFFDATE = "Tournament.CancellationCutoffDate";
	public static readonly String TOURNAMENT_REGISTRATIONFEE = "Tournament.RegistrationFee";
	public static readonly String TOURNAMENT_REGISTRATIONFEEDESCRIPTION = "Tournament.RegistrationFeeDescription";
	public static readonly String TOURNAMENT_DATESTEXT = "Tournament.DatesText";
	public static readonly String TOURNAMENT_PRIZESTEXT = "Tournament.PrizesText";
	public static readonly String TOURNAMENT_SPONSORSTEXT = "Tournament.SponsorsText";
	public static readonly String TOURNAMENT_LOCATIONSTEXT = "Tournament.LocationsText";
	public static readonly String TOURNAMENT_ORGANIZER = "Tournament.Organizer";
	public static readonly String TOURNAMENT_ORGANIZER_ORGANIZERID = "Tournament.Organizer.OrganizerId";
	public static readonly String TOURNAMENT_ORGANIZER_NAME = "Tournament.Organizer.Name";
	public static readonly String TOURNAMENT_ORGANIZER_ADDRESS = "Tournament.Organizer.Address";
	public static readonly String TOURNAMENT_ORGANIZER_ADDRESS_ADDRESS1 = "Tournament.Organizer.Address.Address1";
	public static readonly String TOURNAMENT_ORGANIZER_ADDRESS_ADDRESS2 = "Tournament.Organizer.Address.Address2";
	public static readonly String TOURNAMENT_ORGANIZER_ADDRESS_CITY = "Tournament.Organizer.Address.City";
	public static readonly String TOURNAMENT_ORGANIZER_ADDRESS_STATE = "Tournament.Organizer.Address.State";
	public static readonly String TOURNAMENT_ORGANIZER_ADDRESS_COUNTRY = "Tournament.Organizer.Address.Country";
	public static readonly String TOURNAMENT_ORGANIZER_ADDRESS_POSTALCODE = "Tournament.Organizer.Address.PostalCode";
	public static readonly String TOURNAMENT_ORGANIZER_ORGANIZERCONTACT = "Tournament.Organizer.OrganizerContact";
	public static readonly String TOURNAMENT_ORGANIZER_ORGANIZERCONTACT_NAME = "Tournament.Organizer.OrganizerContact.Name";
	public static readonly String TOURNAMENT_ORGANIZER_ORGANIZERCONTACT_PHONE = "Tournament.Organizer.OrganizerContact.Phone";
	public static readonly String TOURNAMENT_ORGANIZER_ORGANIZERCONTACT_EMAIL = "Tournament.Organizer.OrganizerContact.Email";
	public static readonly String TOURNAMENT_ORGANIZER_TECHNICALCONTACT = "Tournament.Organizer.TechnicalContact";
	public static readonly String TOURNAMENT_ORGANIZER_TECHNICALCONTACT_NAME = "Tournament.Organizer.TechnicalContact.Name";
	public static readonly String TOURNAMENT_ORGANIZER_TECHNICALCONTACT_PHONE = "Tournament.Organizer.TechnicalContact.Phone";
	public static readonly String TOURNAMENT_ORGANIZER_TECHNICALCONTACT_EMAIL = "Tournament.Organizer.TechnicalContact.Email";
	public static readonly String TOURNAMENT_REGISTEREDPARTICIPANTS = "Tournament.RegisteredParticipants";
	public static readonly String TOURNAMENT_MAXIMUMHANDICAP = "Tournament.MaximumHandicap";
	public static readonly String TOURNAMENT_TEAMS = "Tournament.Teams";
	public static readonly String TOURNAMENT_PARTICIPANTS = "Tournament.Participants";
	public static readonly String TOURNAMENT_HOST = "Tournament.Host";
	public static readonly String TOURNAMENT_SHOWPERCENTFULL = "Tournament.ShowPercentFull";
	public static readonly String TOURNAMENT_SHOWPARTICIPANTS = "Tournament.ShowParticipants";
	public static readonly String TOURNAMENT_FEES = "Tournament.Fees";
	public static readonly String TEAM = "Team";
	public static readonly String TEAM_TEAMID = "Team.TeamId";
	public static readonly String TEAM_REGISTRATIONKEY = "Team.RegistrationKey";
	public static readonly String TEAM_STATUS = "Team.Status";
	public static readonly String TEAM_TOURNAMENTID = "Team.TournamentId";
	public static readonly String TEAM_PARTICIPANTS = "Team.Participants";
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
	public static readonly String PAYMENT = "Payment";
	public static readonly String PAYMENT_PAYMENTID = "Payment.PaymentId";
	public static readonly String PAYMENT_TOURNAMENT = "Payment.Tournament";
	public static readonly String PAYMENT_TOURNAMENT_TOURNAMENTID = "Payment.Tournament.TournamentId";
	public static readonly String PAYMENT_TOURNAMENT_NAME = "Payment.Tournament.Name";
	public static readonly String PAYMENT_TOURNAMENT_DESCRIPTION = "Payment.Tournament.Description";
	public static readonly String PAYMENT_TOURNAMENT_NUMBEROFTEAMS = "Payment.Tournament.NumberOfTeams";
	public static readonly String PAYMENT_TOURNAMENT_TEAMSIZE = "Payment.Tournament.TeamSize";
	public static readonly String PAYMENT_TOURNAMENT_FORMAT = "Payment.Tournament.Format";
	public static readonly String PAYMENT_TOURNAMENT_REGISTRATIONBEGINDATE = "Payment.Tournament.RegistrationBeginDate";
	public static readonly String PAYMENT_TOURNAMENT_REGISTRATIONENDDATE = "Payment.Tournament.RegistrationEndDate";
	public static readonly String PAYMENT_TOURNAMENT_CANCELLATIONCUTOFFDATE = "Payment.Tournament.CancellationCutoffDate";
	public static readonly String PAYMENT_TOURNAMENT_REGISTRATIONFEE = "Payment.Tournament.RegistrationFee";
	public static readonly String PAYMENT_TOURNAMENT_REGISTRATIONFEEDESCRIPTION = "Payment.Tournament.RegistrationFeeDescription";
	public static readonly String PAYMENT_TOURNAMENT_DATESTEXT = "Payment.Tournament.DatesText";
	public static readonly String PAYMENT_TOURNAMENT_PRIZESTEXT = "Payment.Tournament.PrizesText";
	public static readonly String PAYMENT_TOURNAMENT_SPONSORSTEXT = "Payment.Tournament.SponsorsText";
	public static readonly String PAYMENT_TOURNAMENT_LOCATIONSTEXT = "Payment.Tournament.LocationsText";
	public static readonly String PAYMENT_TOURNAMENT_ORGANIZER = "Payment.Tournament.Organizer";
	public static readonly String PAYMENT_TOURNAMENT_ORGANIZER_ORGANIZERID = "Payment.Tournament.Organizer.OrganizerId";
	public static readonly String PAYMENT_TOURNAMENT_ORGANIZER_NAME = "Payment.Tournament.Organizer.Name";
	public static readonly String PAYMENT_TOURNAMENT_ORGANIZER_ADDRESS = "Payment.Tournament.Organizer.Address";
	public static readonly String PAYMENT_TOURNAMENT_ORGANIZER_ADDRESS_ADDRESS1 = "Payment.Tournament.Organizer.Address.Address1";
	public static readonly String PAYMENT_TOURNAMENT_ORGANIZER_ADDRESS_ADDRESS2 = "Payment.Tournament.Organizer.Address.Address2";
	public static readonly String PAYMENT_TOURNAMENT_ORGANIZER_ADDRESS_CITY = "Payment.Tournament.Organizer.Address.City";
	public static readonly String PAYMENT_TOURNAMENT_ORGANIZER_ADDRESS_STATE = "Payment.Tournament.Organizer.Address.State";
	public static readonly String PAYMENT_TOURNAMENT_ORGANIZER_ADDRESS_COUNTRY = "Payment.Tournament.Organizer.Address.Country";
	public static readonly String PAYMENT_TOURNAMENT_ORGANIZER_ADDRESS_POSTALCODE = "Payment.Tournament.Organizer.Address.PostalCode";
	public static readonly String PAYMENT_TOURNAMENT_ORGANIZER_ORGANIZERCONTACT = "Payment.Tournament.Organizer.OrganizerContact";
	public static readonly String PAYMENT_TOURNAMENT_ORGANIZER_ORGANIZERCONTACT_NAME = "Payment.Tournament.Organizer.OrganizerContact.Name";
	public static readonly String PAYMENT_TOURNAMENT_ORGANIZER_ORGANIZERCONTACT_PHONE = "Payment.Tournament.Organizer.OrganizerContact.Phone";
	public static readonly String PAYMENT_TOURNAMENT_ORGANIZER_ORGANIZERCONTACT_EMAIL = "Payment.Tournament.Organizer.OrganizerContact.Email";
	public static readonly String PAYMENT_TOURNAMENT_ORGANIZER_TECHNICALCONTACT = "Payment.Tournament.Organizer.TechnicalContact";
	public static readonly String PAYMENT_TOURNAMENT_ORGANIZER_TECHNICALCONTACT_NAME = "Payment.Tournament.Organizer.TechnicalContact.Name";
	public static readonly String PAYMENT_TOURNAMENT_ORGANIZER_TECHNICALCONTACT_PHONE = "Payment.Tournament.Organizer.TechnicalContact.Phone";
	public static readonly String PAYMENT_TOURNAMENT_ORGANIZER_TECHNICALCONTACT_EMAIL = "Payment.Tournament.Organizer.TechnicalContact.Email";
	public static readonly String PAYMENT_TOURNAMENT_REGISTEREDPARTICIPANTS = "Payment.Tournament.RegisteredParticipants";
	public static readonly String PAYMENT_TOURNAMENT_MAXIMUMHANDICAP = "Payment.Tournament.MaximumHandicap";
	public static readonly String PAYMENT_TOURNAMENT_TEAMS = "Payment.Tournament.Teams";
	public static readonly String PAYMENT_TOURNAMENT_PARTICIPANTS = "Payment.Tournament.Participants";
	public static readonly String PAYMENT_TOURNAMENT_HOST = "Payment.Tournament.Host";
	public static readonly String PAYMENT_TOURNAMENT_SHOWPERCENTFULL = "Payment.Tournament.ShowPercentFull";
	public static readonly String PAYMENT_TOURNAMENT_SHOWPARTICIPANTS = "Payment.Tournament.ShowParticipants";
	public static readonly String PAYMENT_TOURNAMENT_FEES = "Payment.Tournament.Fees";
	public static readonly String PAYMENT_AUTHORIZATIONNUMBER = "Payment.AuthorizationNumber";
	public static readonly String PAYMENT_REFERENCENUMBER = "Payment.ReferenceNumber";
	public static readonly String PAYMENT_TRANSACTIONNUMBER = "Payment.TransactionNumber";
	public static readonly String PAYMENT_AMOUNT = "Payment.Amount";
	public static readonly String PAYMENT_PROCESSDATE = "Payment.ProcessDate";
	public static readonly String PAYMENT_PAYMENTSTATUS = "Payment.PaymentStatus";
	public static readonly String PAYMENT_GOLFER = "Payment.Golfer";
	public static readonly String PAYMENT_GOLFER_GOLFERID = "Payment.Golfer.GolferId";
	public static readonly String PAYMENT_GOLFER_FIRSTNAME = "Payment.Golfer.FirstName";
	public static readonly String PAYMENT_GOLFER_MIDDLEINITIAL = "Payment.Golfer.MiddleInitial";
	public static readonly String PAYMENT_GOLFER_LASTNAME = "Payment.Golfer.LastName";
	public static readonly String PAYMENT_GOLFER_PHONE = "Payment.Golfer.Phone";
	public static readonly String PAYMENT_GOLFER_EMAIL = "Payment.Golfer.Email";
	public static readonly String PAYMENT_GOLFER_ADDRESS = "Payment.Golfer.Address";
	public static readonly String PAYMENT_GOLFER_ADDRESS_ADDRESS1 = "Payment.Golfer.Address.Address1";
	public static readonly String PAYMENT_GOLFER_ADDRESS_ADDRESS2 = "Payment.Golfer.Address.Address2";
	public static readonly String PAYMENT_GOLFER_ADDRESS_CITY = "Payment.Golfer.Address.City";
	public static readonly String PAYMENT_GOLFER_ADDRESS_STATE = "Payment.Golfer.Address.State";
	public static readonly String PAYMENT_GOLFER_ADDRESS_COUNTRY = "Payment.Golfer.Address.Country";
	public static readonly String PAYMENT_GOLFER_ADDRESS_POSTALCODE = "Payment.Golfer.Address.PostalCode";
	public static readonly String PAYMENT_GOLFER_DATEOFBIRTH = "Payment.Golfer.DateOfBirth";
	public static readonly String PAYMENT_GOLFER_HANDICAP = "Payment.Golfer.Handicap";
	public static readonly String PAYMENT_GOLFER_COURSENUMBER = "Payment.Golfer.CourseNumber";
	public static readonly String PAYMENT_GOLFER_PLAYERNUMBER = "Payment.Golfer.PlayerNumber";
	public static readonly String PAYMENT_GOLFER_GENDER = "Payment.Golfer.Gender";
	public static readonly String PAYMENT_GOLFER_GOLFERSTATUS = "Payment.Golfer.GolferStatus";
	public static readonly String PAYMENT_CREDITCARD = "Payment.CreditCard";
	public static readonly String PAYMENT_CREDITCARD_NUMBER = "Payment.CreditCard.Number";
	public static readonly String PAYMENT_CREDITCARD_EXPIRATIONDATE = "Payment.CreditCard.ExpirationDate";
	public static readonly String PAYMENT_CREDITCARD_NAME = "Payment.CreditCard.Name";
	public static readonly String PAYMENT_CREDITCARD_ADDRESS = "Payment.CreditCard.Address";
	public static readonly String PAYMENT_CREDITCARD_ADDRESS_ADDRESS1 = "Payment.CreditCard.Address.Address1";
	public static readonly String PAYMENT_CREDITCARD_ADDRESS_ADDRESS2 = "Payment.CreditCard.Address.Address2";
	public static readonly String PAYMENT_CREDITCARD_ADDRESS_CITY = "Payment.CreditCard.Address.City";
	public static readonly String PAYMENT_CREDITCARD_ADDRESS_STATE = "Payment.CreditCard.Address.State";
	public static readonly String PAYMENT_CREDITCARD_ADDRESS_COUNTRY = "Payment.CreditCard.Address.Country";
	public static readonly String PAYMENT_CREDITCARD_ADDRESS_POSTALCODE = "Payment.CreditCard.Address.PostalCode";
	public static readonly String PAYMENT_PARTICIPANTS = "Payment.Participants";
	public static readonly String PAYMENT_CONFIRMATIONCODE = "Payment.ConfirmationCode";
	public static readonly String PAYMENT_PAYMENTDATE = "Payment.PaymentDate";

	public IdType ParticipantId {
	    get { return this.participantId; }
	    set { this.participantId = value; }
	}

	public GolferData Golfer {
	    get { return this.golfer; }
	    set { this.golfer = value; }
	}




	public IList ALaCarteItems {
	    get { return this.aLaCarteItems; }
	    set { this.aLaCarteItems = value; }
	}


	public BooleanType IsValid {
	    get { return this.isValid; }
	    set { this.isValid = value; }
	}

	public CurrencyType RegistrationFee {
	    get { return this.registrationFee; }
	    set { this.registrationFee = value; }
	}


























	public TournamentData Tournament {
	    get { return this.tournament; }
	    set { this.tournament = value; }
	}






	public TeamData Team {
	    get { return this.team; }
	    set { this.team = value; }
	}




















	public GolferData Golfer {
	    get { return this.golfer; }
	    set { this.golfer = value; }
	}

























	public PaymentData Payment {
	    get { return this.payment; }
	    set { this.payment = value; }
	}
    }
}
