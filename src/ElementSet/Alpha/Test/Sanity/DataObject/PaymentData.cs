using System;
using System.Collections;

using Spring2.Core.Types;

using Golf.Tournament.DataObject;
using Golf.Tournament.Types;

namespace Golf.Tournament.DataObject {
    public class PaymentData : Spring2.Core.DataObject.DataObject {

	private IdType paymentId = IdType.DEFAULT;
	private TournamentData tournament = new TournamentData();
	private StringType authorizationNumber = StringType.DEFAULT;
	private StringType referenceNumber = StringType.DEFAULT;
	private StringType transactionNumber = StringType.DEFAULT;
	private CurrencyType amount = CurrencyType.DEFAULT;
	private DateType processDate = DateType.DEFAULT;
	private PaymentStatusEnum paymentStatus = PaymentStatusEnum.DEFAULT;
	private GolferData golfer = new GolferData();
	private CreditCardData creditCard = new CreditCardData();
	private IList participants = new ArrayList();
	private StringType confirmationCode = StringType.DEFAULT;
	private DateType paymentDate = DateType.DEFAULT;

	public static readonly String PAYMENTID = "PaymentId";
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
	public static readonly String AUTHORIZATIONNUMBER = "AuthorizationNumber";
	public static readonly String REFERENCENUMBER = "ReferenceNumber";
	public static readonly String TRANSACTIONNUMBER = "TransactionNumber";
	public static readonly String AMOUNT = "Amount";
	public static readonly String PROCESSDATE = "ProcessDate";
	public static readonly String PAYMENTSTATUS = "PaymentStatus";
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
	public static readonly String CREDITCARD = "CreditCard";
	public static readonly String CREDITCARD_NUMBER = "CreditCard.Number";
	public static readonly String CREDITCARD_EXPIRATIONDATE = "CreditCard.ExpirationDate";
	public static readonly String CREDITCARD_NAME = "CreditCard.Name";
	public static readonly String CREDITCARD_ADDRESS = "CreditCard.Address";
	public static readonly String CREDITCARD_ADDRESS_ADDRESS1 = "CreditCard.Address.Address1";
	public static readonly String CREDITCARD_ADDRESS_ADDRESS2 = "CreditCard.Address.Address2";
	public static readonly String CREDITCARD_ADDRESS_CITY = "CreditCard.Address.City";
	public static readonly String CREDITCARD_ADDRESS_STATE = "CreditCard.Address.State";
	public static readonly String CREDITCARD_ADDRESS_COUNTRY = "CreditCard.Address.Country";
	public static readonly String CREDITCARD_ADDRESS_POSTALCODE = "CreditCard.Address.PostalCode";
	public static readonly String PARTICIPANTS = "Participants";
	public static readonly String CONFIRMATIONCODE = "ConfirmationCode";
	public static readonly String PAYMENTDATE = "PaymentDate";

	public IdType PaymentId {
	    get { return this.paymentId; }
	    set { this.paymentId = value; }
	}

	public TournamentData Tournament {
	    get { return this.tournament; }
	    set { this.tournament = value; }
	}


	public StringType AuthorizationNumber {
	    get { return this.authorizationNumber; }
	    set { this.authorizationNumber = value; }
	}

	public StringType ReferenceNumber {
	    get { return this.referenceNumber; }
	    set { this.referenceNumber = value; }
	}

	public StringType TransactionNumber {
	    get { return this.transactionNumber; }
	    set { this.transactionNumber = value; }
	}

	public CurrencyType Amount {
	    get { return this.amount; }
	    set { this.amount = value; }
	}

	public DateType ProcessDate {
	    get { return this.processDate; }
	    set { this.processDate = value; }
	}

	public PaymentStatusEnum PaymentStatus {
	    get { return this.paymentStatus; }
	    set { this.paymentStatus = value; }
	}

	public GolferData Golfer {
	    get { return this.golfer; }
	    set { this.golfer = value; }
	}


	public CreditCardData CreditCard {
	    get { return this.creditCard; }
	    set { this.creditCard = value; }
	}










	public IList Participants {
	    get { return this.participants; }
	    set { this.participants = value; }
	}

	public StringType ConfirmationCode {
	    get { return this.confirmationCode; }
	    set { this.confirmationCode = value; }
	}

	public DateType PaymentDate {
	    get { return this.paymentDate; }
	    set { this.paymentDate = value; }
	}
    }
}
