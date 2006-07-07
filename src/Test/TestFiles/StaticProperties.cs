using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using log4net;
using NVelocity;
using NVelocity.App;
using Spring2.Core.BusinessEntity;
using Spring2.Core.Configuration;
using Spring2.Core.DAO;
using Spring2.Core.Geocode;
using Spring2.Core.Mail.BusinessLogic;
using Spring2.Core.Mail.Types;
using Spring2.Core.Message;
using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;
using UppercaseLiving.Dss.Dao;
using UppercaseLiving.Dss.DataObject;
using UppercaseLiving.Dss.Exceptions;
using UppercaseLiving.Dss.Types;

namespace UppercaseLiving.Dss.BusinessLogic {
    public class Referral : BusinessEntity, IReferral {

	// comment 1

	/// <summary>
	/// xml comment
	/// </summary>
	private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

	[Generate()]
	private IdType referralId = IdType.DEFAULT;

	[Generate()]
	private IdType directSalesAgentId = IdType.DEFAULT;

	[Generate()]
	private StringType streetAddress = StringType.DEFAULT;

	[Generate()]
	private StringType city = StringType.DEFAULT;

	[Generate()]
	private StringType state = StringType.DEFAULT;

	[Generate()]
	private StringType postalCode = StringType.DEFAULT;

	[Generate()]
	private DecimalType latitude = DecimalType.DEFAULT;

	[Generate()]
	private DecimalType longitude = DecimalType.DEFAULT;

	[Generate()]
	private StringType email = StringType.DEFAULT;

	private DirectSalesAgent directSalesAgent = null;

	[Generate()]
	private StringType firstName = StringType.DEFAULT;

	[Generate()]
	private StringType middleName = StringType.DEFAULT;

	[Generate()]
	private StringType lastName = StringType.DEFAULT;

	[Generate()]
	private PhoneNumberType phoneNumber = PhoneNumberType.DEFAULT;

	[Generate()]
	private StringType messageBody = StringType.DEFAULT;

	[Generate()]
	private StringType preferredContactMethod = StringType.DEFAULT;

	[Generate()]
	private DecimalType distance = DecimalType.DEFAULT;

	[Generate()]
	private DateTimeType referralDate = DateTimeType.DEFAULT;

	private static VelocityEngine velocity = null;

	[Generate()]
	private ReferralActionList referralActions = ReferralActionList.DEFAULT;

	[Generate()]
	private BooleanType demonstratorNotified = BooleanType.DEFAULT;

	[Generate()]
	private StringType iPAddress = StringType.DEFAULT;

	[Generate()]
	private IdType referralSourceId = IdType.DEFAULT;

	[Generate()]
	private ReferralReferralInterestList referralInterests = ReferralReferralInterestList.DEFAULT;

	private ReferralSource referralSource = null;

	static Referral() {
	    velocity = new VelocityEngine();
	    velocity.SetProperty("runtime.log.logsystem.log4net.category", "NVelocity");
	    velocity.Init();
	}

	[Generate()]
	internal Referral() {
	}

	[Generate()]
	internal Referral(Boolean isNew) {
	    this.isNew = isNew;
	}

	[Generate()]
	public IdType ReferralId {
	    get {
		return this.referralId;
	    }
	    set {
		this.referralId = value;
	    }
	}

	[Generate()]
	public IdType DirectSalesAgentId {
	    get {
		return this.directSalesAgentId;
	    }
	    set {
		this.directSalesAgentId = value;
	    }
	}

	[Generate()]
	public StringType StreetAddress {
	    get {
		return this.streetAddress;
	    }
	    set {
		this.streetAddress = value;
	    }
	}

	[Generate()]
	public StringType City {
	    get {
		return this.city;
	    }
	    set {
		this.city = value;
	    }
	}

	[Generate()]
	public StringType State {
	    get {
		return this.state;
	    }
	    set {
		this.state = value;
	    }
	}

	[Generate()]
	public StringType PostalCode {
	    get {
		return this.postalCode;
	    }
	    set {
		this.postalCode = value;
	    }
	}

	[Generate()]
	public DecimalType Latitude {
	    get {
		return this.latitude;
	    }
	    set {
		this.latitude = value;
	    }
	}

	[Generate()]
	public DecimalType Longitude {
	    get {
		return this.longitude;
	    }
	    set {
		this.longitude = value;
	    }
	}

	[Generate()]
	public StringType Email {
	    get {
		return this.email;
	    }
	    set {
		this.email = value;
	    }
	}

	public DirectSalesAgent DirectSalesAgent {
	    get {
		if (directSalesAgent == null && this.directSalesAgentId.IsValid) {
		    directSalesAgent = Demonstrator.GetInstance(DirectSalesAgentId);
		}
		return this.directSalesAgent as DirectSalesAgent;
	    }
	}

	[Generate()]
	IDirectSalesAgent IReferral.DirectSalesAgent {
	    get {
		return this.DirectSalesAgent;
	    }
	}

	[Generate()]
	public StringType FirstName {
	    get {
		return this.firstName;
	    }
	    set {
		this.firstName = value;
	    }
	}

	[Generate()]
	public StringType MiddleName {
	    get {
		return this.middleName;
	    }
	    set {
		this.middleName = value;
	    }
	}

	[Generate()]
	public StringType LastName {
	    get {
		return this.lastName;
	    }
	    set {
		this.lastName = value;
	    }
	}

	public StringType FullName {
	    get {
		StringBuilder buffer = new StringBuilder();
		if (FirstName.IsValid) {
		    buffer.Append(FirstName.ToString());
		}
		if (MiddleName.IsValid && !MiddleName.IsEmpty) {
		    if (buffer.Length > 0) {
			buffer.AppendFormat(" {0}", MiddleName.ToString());
		    } else {
			buffer.Append(MiddleName.ToString());
		    }
		}
		if (LastName.IsValid && !LastName.IsEmpty) {
		    if (buffer.Length > 0) {
			buffer.AppendFormat(" {0}", LastName.ToString());
		    } else {
			buffer.Append(LastName.ToString());
		    }
		}

		return StringType.Parse(buffer.ToString());
	    }
	}

	[Generate()]
	public PhoneNumberType PhoneNumber {
	    get {
		return this.phoneNumber;
	    }
	    set {
		this.phoneNumber = value;
	    }
	}

	[Generate()]
	public StringType MessageBody {
	    get {
		return this.messageBody;
	    }
	    set {
		this.messageBody = value;
	    }
	}

	[Generate()]
	public StringType PreferredContactMethod {
	    get {
		return this.preferredContactMethod;
	    }
	    set {
		this.preferredContactMethod = value;
	    }
	}

	[Generate()]
	public DecimalType Distance {
	    get {
		return this.distance;
	    }
	    set {
		this.distance = value;
	    }
	}

	[Generate()]
	public DateTimeType ReferralDate {
	    get {
		return this.referralDate;
	    }
	    set {
		this.referralDate = value;
	    }
	}

	public ReferralSource ReferralSource {
	    get {
		if (referralSource == null || !referralSource.ReferralSourceId.IsValid) {
		    referralSource = ReferralSource.GetInstance(ReferralSourceId);
		}
		return this.referralSource as ReferralSource;
	    }
	}

	public BooleanType NeedsFollowUp {
	    get {
		foreach (ReferralAction action in this.ReferralActions) {
		    if (action.ReferralActionType.Equals(ReferralActionTypeEnum.FOLLOW_UP)) {
			return BooleanType.FALSE;
		    }
		}
		return BooleanType.TRUE;
	    }
	}

	public ReferralActionList ReferralActions {
	    get {
		if (referralActions.IsDefault) {
		    referralActions = new ReferralActionList();
		    referralActions.AddRange(ReferralActionDAO.DAO.FindByReferralId(this.ReferralId));
		}
		return this.referralActions;
	    }
	}

	[Generate()]
	public BooleanType DemonstratorNotified {
	    get {
		return this.demonstratorNotified;
	    }
	    set {
		this.demonstratorNotified = value;
	    }
	}

	[Generate()]
	public StringType IPAddress {
	    get {
		return this.iPAddress;
	    }
	    set {
		this.iPAddress = value;
	    }
	}

	[Generate()]
	public IdType ReferralSourceId {
	    get {
		return this.referralSourceId;
	    }
	    set {
		this.referralSourceId = value;
	    }
	}

	public ReferralReferralInterestList ReferralInterests {
	    get {
		if (referralInterests.IsDefault) {
		    referralInterests = new ReferralReferralInterestList();
		    referralInterests.AddRange(ReferralReferralInterestDAO.DAO.FindByReferralId(this.ReferralId));
		}
		return this.referralInterests;
	    }
	}

	[Generate()]
	IReferralSource IReferral.ReferralSource {
	    get {
		return this.ReferralSource;
	    }
	}

	private bool ReferDemonstrator {
	    get {
		if (this.ReferralInterests.Count == 0) {
		    return false;
		}

		foreach (IReferralReferralInterest interest in this.ReferralInterests) {
		    if (interest.ReferralInterest.ReferDemonstrator.IsFalse) {
			return false;
		    }
		}
		return true;
	    }
	}

	private bool NotifyDemonstrator {
	    get {
		foreach (IReferralReferralInterest interest in this.ReferralInterests) {
		    if (interest.ReferralInterest.NotifyDemonstrator.IsFalse) {
			return false;
		    }
		}
		return true;
	    }
	}

	public StringType FullAddress {
	    get {
		StringBuilder buffer = new StringBuilder();
		if (!StreetAddress.IsEmpty) {
		    buffer.Append(StreetAddress.ToString());
		}

		if (!City.IsEmpty) {
		    if (buffer.Length > 0) {
			buffer.AppendFormat(" {0},", City.ToString());
		    } else {
			buffer.Append(City.ToString());
		    }
		}

		if (!State.IsEmpty) {
		    if (buffer.Length > 0) {
			buffer.AppendFormat(" {0}", State.ToString());
		    } else {
			buffer.Append(State.ToString());
		    }
		}

		if (!PostalCode.IsEmpty) {
		    if (buffer.Length > 0) {
			buffer.AppendFormat(" {0}", PostalCode.ToString());
		    } else {
			buffer.Append(PostalCode.ToString());
		    }
		}

		return StringType.Parse(buffer.ToString());
	    }
	}

	[Generate()]
	public static Referral NewInstance() {
	    return new Referral();
	}

	[Generate()]
	public static Referral GetInstance(IdType referralId) {
	    return ReferralDAO.DAO.Load(referralId);
	}

	[Generate()]
	public void Update(ReferralData data) {
	    firstName = data.FirstName.IsDefault ? firstName : data.FirstName;
	    middleName = data.MiddleName.IsDefault ? middleName : data.MiddleName;
	    lastName = data.LastName.IsDefault ? lastName : data.LastName;
	    directSalesAgentId = data.DirectSalesAgentId.IsDefault ? directSalesAgentId : data.DirectSalesAgentId;
	    streetAddress = data.StreetAddress.IsDefault ? streetAddress : data.StreetAddress;
	    city = data.City.IsDefault ? city : data.City;
	    state = data.State.IsDefault ? state : data.State;
	    postalCode = data.PostalCode.IsDefault ? postalCode : data.PostalCode;
	    latitude = data.Latitude.IsDefault ? latitude : data.Latitude;
	    longitude = data.Longitude.IsDefault ? longitude : data.Longitude;
	    email = data.Email.IsDefault ? email : data.Email;
	    phoneNumber = data.PhoneNumber.IsDefault ? phoneNumber : data.PhoneNumber;
	    messageBody = data.MessageBody.IsDefault ? messageBody : data.MessageBody;
	    preferredContactMethod = data.PreferredContactMethod.IsDefault ? preferredContactMethod : data.PreferredContactMethod;
	    distance = data.Distance.IsDefault ? distance : data.Distance;
	    referralDate = data.ReferralDate.IsDefault ? referralDate : data.ReferralDate;
	    demonstratorNotified = data.DemonstratorNotified.IsDefault ? demonstratorNotified : data.DemonstratorNotified;
	    iPAddress = data.IPAddress.IsDefault ? iPAddress : data.IPAddress;
	    referralSourceId = data.ReferralSourceId.IsDefault ? referralSourceId : data.ReferralSourceId;
	    Store();
	}

	public void Store() {
	    MessageList errors = Validate();

	    if (errors.Count > 0) {
		if (!isNew) {
		    this.Reload();
		}
		throw new MessageListException(errors);
	    }

	    UpdateDistance();
	    if (!preferredContactMethod.IsValid) {
		this.preferredContactMethod = "Unspecified";
	    }
	    if (isNew) {
		this.ReferralId = ReferralDAO.DAO.Insert(this);
		isNew = false;
	    } else {
		ReferralDAO.DAO.Update(this);
	    }
	}

	public virtual MessageList Validate() {
	    MessageList errors = new MessageList();

	    if (this.FirstName.IsEmpty) {
		errors.Add(new MissingRequiredFieldError("First Name"));
	    }

	    if (this.LastName.IsEmpty) {
		errors.Add(new MissingRequiredFieldError("Last Name"));
	    }

	    if (this.Email.IsEmpty) {
		errors.Add(new MissingRequiredFieldError("E-Mail"));
	    } else {
		if (
		    !
		    Regex.IsMatch(this.Email.ToString(),
		    @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
		    ) {
		    errors.Add(new InvalidFormatError("E-Mail", "yourname@domain.com"));
		}
	    }

	    if (this.StreetAddress.IsEmpty) {
		errors.Add(new MissingRequiredFieldError("Street Address"));
	    }

	    if (this.City.IsEmpty) {
		errors.Add(new MissingRequiredFieldError("City"));
	    }

	    if (this.State.IsEmpty) {
		errors.Add(new MissingRequiredFieldError("State"));
	    }

	    if (this.PostalCode.IsEmpty) {
		errors.Add(new MissingRequiredFieldError("Postal Code"));
	    }

	    if (!this.ReferralSourceId.IsValid) {
		errors.Add(new MissingRequiredFieldError("How did you hear about us?"));
	    }

	    return errors;
	}

	public void Reload() {
	    ReferralDAO.DAO.Reload(this);
	    directSalesAgent = null;
	}

	public static Referral Create(ReferralData data, IdTypeList referralInterestIds) {
	    Referral referral = new Referral();
	    data.ReferralDate = DateTimeType.Now;
	    UpdateGeocode(data);
	    referral.Update(data);
	    try {
		referral.AddReferralInterests(referralInterestIds);
		if (referral.ReferDemonstrator) {
		    IdType id = IdType.UNSET;
		    bool recycled = referral.FindReferral(ref id);
		    referral.DirectSalesAgentId = id;
		    if (referral.directSalesAgentId.IsValid) {
			if (!recycled) {
			    ((Demonstrator)referral.DirectSalesAgent).UpdateReferralCount();
			}
			referral.SendReferralEmailToProspect();
		    }
		}

		if (referral.DirectSalesAgentId.IsValid) {
		    referral.SendDemonstratorNotification();
		}
	    } catch (Exception ex) {
		log.Error(ex);
		throw;
	    }


	    referral.Store();
	    return referral;
	}

	private void SendDemonstratorNotification() {
	    SendDemonstratorNotification(false);
	}

	public void SendDemonstratorNotification(bool forced) {
	    if (!this.DirectSalesAgentId.IsValid) {
		throw new CannotNotifyDemonstratorOnUnassignedReferral();
	    }

	    if (this.DirectSalesAgentId.IsValid && (this.NotifyDemonstrator || forced)) {
		((Demonstrator)this.DirectSalesAgent).SendReferralEmail(this);
		this.DemonstratorNotified = BooleanType.TRUE;

		if (forced) {
		    this.Store();
		}
	    }
	}

	private static void UpdateGeocode(ReferralData data) {
	    GeocodeData geocode = Address.GeocodeAddress(data.StreetAddress, data.City, data.State, data.PostalCode);
	    if (geocode == null) {
		SendGeocodeNotFoundEmail(data);
		throw new GeocodeNotFoundError();
	    }
	    data.Latitude = geocode.MatchLatitude;
	    data.Longitude = geocode.MatchLongitude;
	}

	private void AddReferralInterests(IdTypeList referralInterestIds) {
	    foreach (IdType id in referralInterestIds) {
		AddReferralInterest(id);
	    }
	}

	private void AddReferralInterest(IdType referralInterestId) {
	    ReferralInterest interest = null;
	    try {
		interest = ReferralInterest.GetInstance(referralInterestId);
	    } catch (Exception) {
		throw new InvalidReferralInterestError();
	    }

	    ReferralReferralInterest newInterest = ReferralReferralInterest.Create(this.ReferralId, referralInterestId);
	    ReferralReferralInterestList list = new ReferralReferralInterestList();
	    list.AddRange(this.ReferralInterests);
	    list.Add(newInterest);
	    this.referralInterests = list;
	}

	public void SendReferralEmailToProspect() {
	    StringType subjectTemplate;
	    StringType bodyTemplate;
	    MailMessageTypeEnum mailMessage;

	    mailMessage = MailMessageTypeEnum.REFERRAL_PROSPECT_EMAIL;
	    subjectTemplate = Resource.GetResource(ResourceKeyEnum.REFERRAL_PROSPECT_EMAIL_SUBJECT).ResourceText;
	    bodyTemplate = Resource.GetResource(ResourceKeyEnum.REFERRAL_PROSPECT_EMAIL_BODY).ResourceText;

	    // create an NVelocity context with the Referral
	    VelocityContext context =
		new VelocityContext(new Hashtable(new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer()));
	    context.Put("Referral", this);

	    // merge the template and context
	    StringWriter writer = new StringWriter();
	    velocity.Evaluate(context, writer, "Referral.SendReferralEmailToProspect", bodyTemplate.ToString());
	    StringType body = writer.ToString();

	    writer = new StringWriter();
	    velocity.Evaluate(context, writer, "Referral.SendReferralEmailToProspect", subjectTemplate.ToString());
	    StringType subject = writer.ToString();

	    MailMessage.Create(mailMessage.Code, this.Email, subject, body, MailBodyFormatEnum.TEXT);
	}

	private static void SendGeocodeNotFoundEmail(ReferralData data) {
	    StringType subjectTemplate;
	    StringType bodyTemplate;
	    MailMessageTypeEnum mailMessage;

	    mailMessage = MailMessageTypeEnum.REFERRAL_GEOCODE_NOT_FOUND;
	    subjectTemplate = Resource.GetResource(ResourceKeyEnum.REFERRAL_GEOCODE_NOT_FOUND_EMAIL_SUBJECT).ResourceText;
	    bodyTemplate = Resource.GetResource(ResourceKeyEnum.REFERRAL_GEOCODE_NOT_FOUND_EMAIL_BODY).ResourceText;

	    // create an NVelocity context with the Referral
	    VelocityContext context =
		new VelocityContext(new Hashtable(new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer()));
	    context.Put("Referral", data);

	    // merge the template and context
	    StringWriter writer = new StringWriter();
	    velocity.Evaluate(context, writer, "Referral.SendGeocodeNotFoundEmail", bodyTemplate.ToString());
	    StringType body = writer.ToString();

	    writer = new StringWriter();
	    velocity.Evaluate(context, writer, "Referral.SendGeocodeNotFoundEmail", subjectTemplate.ToString());
	    StringType subject = writer.ToString();

	    MailMessage.Create(mailMessage.Code, subject, body, MailBodyFormatEnum.TEXT);
	}

	private IReferral FindByEmail() {
	    try {
		return ReferralDAO.DAO.FindByEmail(email, this.ReferralId);
	    } catch (FinderException) {
		return null;
	    }
	}

	private IReferral FindByGeocode() {
	    try {
		return ReferralDAO.DAO.FindByGeocode(latitude, longitude, this.ReferralId);
	    } catch (FinderException) {
		return null;
	    }
	}

	private bool FindReferral(ref IdType id) {
	    return FindReferral(ref id, false);
	}

	private bool FindReferral(ref IdType id, bool forceNew) {
	    if (!forceNew) {
		IReferral recycled = FindByEmail();
		if (recycled != null) {
		    id = recycled.DirectSalesAgentId;
		    return true;
		}
		recycled = FindByGeocode();
		if (recycled != null) {
		    id = recycled.DirectSalesAgentId;
		    return true;
		}
	    }

	    DirectSalesAgentList list = FindReferrals(this.Latitude, this.Longitude);
	    if (list.Count > 0) {
		IdType x = IdType.UNSET;

		for (int i = 0; i < list.Count; i++) {
		    x = list[i].DirectSalesAgentId;
		    if (!x.Equals(this.DirectSalesAgentId)) {
			break;
		    }
		}
		id = x;
	    }

	    return false;
	}

	private void UpdateDistance() {
	    if (this.directSalesAgentId.IsValid) {
		this.distance =
		    CalculateDistance(DirectSalesAgent.MailingAddress.Latitude, DirectSalesAgent.MailingAddress.Longitude,
		    this.Latitude, this.Longitude);
	    }
	}

	public static DirectSalesAgentList FindReferrals(DecimalType latitude, DecimalType longitude) {
	    return FindReferrals(Convert.ToDouble(latitude.ToDecimal()), Convert.ToDouble(longitude.ToDecimal()), 0);
	}

	public static DirectSalesAgentList FindReferrals(Double latitude, Double longitude, Int32 minimum) {
	    DirectSalesAgentList referrals = new DirectSalesAgentList();

	    // TODO: change to use radicals
	    Double radius = Double.Parse(ConfigurationProvider.Instance.Settings["ReferralRadius"]);
	    Double distance = 0;

	    Int32 daysSinceReferral = Int32.Parse(ConfigurationProvider.Instance.Settings["DaysSinceReferral"]);
	    Int32 minimumReferrals = minimum == 0
		? Int32.Parse(ConfigurationProvider.Instance.Settings["MinimumReferrals"])
		: minimum;
	    Int32 maxSearches = Int32.Parse(ConfigurationProvider.Instance.Settings["ReferralMaxSearches"]);
	    IdType minimumTitleId = IdType.Parse(ConfigurationProvider.Instance.Settings["MinimumReferralTitleId"]);

	    Int32 referralsFound = 0;
	    Int32 searchCount = 0;

	    try {
		while ((referralsFound < minimumReferrals) && (searchCount < maxSearches)) {
		    searchCount++;

		    if (searchCount >= maxSearches) {
			distance = int.MaxValue;
		    } else {
			distance += radius;
		    }
		    DecimalType minLat = Convert.ToDecimal(CalculateSouthernRadicalPoint(latitude, longitude, distance).Latitude);
		    DecimalType maxLat = Convert.ToDecimal(CalculateNorthernRadicalPoint(latitude, longitude, distance).Latitude);
		    DecimalType minLong = Convert.ToDecimal(CalculateEasternRadicalPoint(latitude, longitude, distance).Longitude);
		    DecimalType maxLong = Convert.ToDecimal(CalculateWesternRadicalPoint(latitude, longitude, distance).Longitude);

		    referrals =
			DemonstratorDAO.DAO.FindWithinGeographicArea(Convert.ToDecimal(latitude), Convert.ToDecimal(longitude), minLat,
			maxLat, minLong, maxLong, minimumTitleId);
		    referralsFound = referrals.Count;
		}
	    } catch (Exception ex) {
		log.Error(ex.ToString());
		return new DirectSalesAgentList();
	    }

	    return referrals;
	}


	public void AddEmailReferralAction(EmailReferralActionData data) {
	    EmailReferralAction action = EmailReferralAction.Create(data, this);
	}

	public void AddFollowUpReferralAction(FollowUpReferralActionData data) {
	    FollowUpReferralAction action = FollowUpReferralAction.Create(data, this);
	}

	public void AddNoteReferralAction(NoteReferralActionData data) {
	    NoteReferralAction action = NoteReferralAction.Create(data, this);
	}

	public void AddTransferReferralAction(TransferReferralActionData data) {
	    TransferReferralAction action = TransferReferralAction.Create(data, this);

	    this.DirectSalesAgentId = data.ToDirectSalesAgentId;
	    this.directSalesAgent = null;
	    SendReferralEmailToProspect();
	    if (NotifyDemonstrator) {
		SendDemonstratorNotification();
	    }
	    this.Store();
	    ((Demonstrator)DirectSalesAgent).UpdateReferralCount();
	}

	public void Reassign() {
	    if (!this.DirectSalesAgentId.IsValid) {
		throw new CannotReassignUnassignedReferralError();
	    }

	    TransferReferralActionData data = new TransferReferralActionData();
	    IdType id = IdType.UNSET;
	    FindReferral(ref id, true);
	    data.ToDirectSalesAgentId = id;
	    data.Comment = "Auto Reassigned";
	    AddTransferReferralAction(data);
	}

	public void Assign() {
	    if (this.DirectSalesAgentId.IsValid) {
		throw new ReferralAlreadyAssignedError();
	    }

	    TransferReferralActionData data = new TransferReferralActionData();
	    IdType id = IdType.UNSET;
	    FindReferral(ref id, true);
	    data.ToDirectSalesAgentId = id;
	    data.Comment = "Auto Assigned";
	    AddTransferReferralAction(data);
	}

	#region Methods to be moved to Spring2.Core.Geocode

	public static DecimalType CalculateDistance(DecimalType latitude1, DecimalType longitude1, DecimalType latitude2,
	    DecimalType longitude2) {
	    if (latitude1.IsValid && longitude1.IsValid && latitude2.IsValid && longitude2.IsValid) {
		return
		    new DecimalType(
		    CalculateDistance(DecimalType.ToDouble(latitude1), DecimalType.ToDouble(longitude1),
		    DecimalType.ToDouble(latitude2), DecimalType.ToDouble(longitude2)));
	    } else {
		return -1;
	    }
	}

	public static Double CalculateDistance(Double lat1, Double lon1, Double lat2, Double lon2) {
	    double radius = EARTH_MEAN_RADIUS;
	    //convert latitudes and longitudes from degrees to radians
	    lat1 = deg2rad(lat1);
	    lon1 = deg2rad(lon1);
	    lat2 = deg2rad(lat2);
	    lon2 = deg2rad(lon2);

	    double distance = radius *
		Math.Acos(Math.Cos(lon1 - lon2) * Math.Cos(lat1) * Math.Cos(lat2) + Math.Sin(lat1) * Math.Sin(lat2));

	    return distance;
	}

	public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2, char unit) {
	    double theta = lon1 - lon2;
	    double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) +
		Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
	    dist = Math.Acos(dist);
	    dist = rad2deg(dist);
	    dist = dist * 60 * 1.1515;
	    if (unit == 'K') {
		dist = dist * 1.609344;
	    } else if (unit == 'N') {
		dist = dist * 0.8684;
	    }
	    return (dist);
	}


	/// <summary>
	/// angle_radians=(pi/180)*angle_degrees
	/// </summary>
	/// <param name="degrees"></param>
	/// <returns></returns>
	public Double ConvertDegreesToRadians(Double degrees) {
	    return (degrees * Math.PI / 180.0);
	}

	/// <summary>
	/// angle_degrees=(180/pi)*angle_radians 
	/// </summary>
	/// <param name="radians"></param>
	/// <returns></returns>
	public Double ConvertRadiansToDegrees(Double radians) {
	    return (radians / Math.PI * 180.0);
	}

	//:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
	//::  This function converts decimal degrees to radians             :::
	//:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
	public static double deg2rad(double deg) {
	    return (deg * Math.PI / 180.0);
	}

	//:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
	//::  This function converts radians to decimal degrees             :::
	//:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
	public static double rad2deg(double rad) {
	    return (rad / Math.PI * 180.0);
	}

	#region Notes

	//                  Aviation Formulary V1.30 - Ed Williams
	//  http://www.best.com/~williams/avform.htm   

	//There you will find a formula for "Lat/lon given radial and distance," 
	//which is what you seek. Actually, it's more general: given a starting 
	//point, ANY initial heading, and a distance, it calculates the latitude 
	//and longitude of your destination. I quote:

	//"A point {lat,lon} is a distance d out on the tc radial from point 1 
	//if: 

	//     lat=asin(sin(lat1)*cos(d)+cos(lat1)*sin(d)*cos(tc))
	//     IF (cos(lat)=0)
	//        lon=lon1      // endpoint a pole
	//     ELSE
	//        lon=mod(lon1-asin(sin(tc)*sin(d)/cos(lat))+pi,2*pi)-pi
	//     ENDIF

	//"This algorithm is limited to distances such that dlon <pi/2, i.e 
	//those that extend around less than one quarter of the circumference of 
	//the earth in longitude. A completely general, but more complicated 
	//algorithm is necessary if greater distances are allowed: 

	//     lat =asin(sin(lat1)*cos(d)+cos(lat1)*sin(d)*cos(tc))
	//     dlon=atan2(sin(tc)*sin(d)*cos(lat1),cos(d)-sin(lat1)*sin(lat))
	//     lon=mod( lon1-dlon +pi,2*pi )-pi

	//End of quote. Here, "distance d" is a "distance" in radians (an arc, 
	//or central angle). You will need to divide your distance (5 miles) by 
	//the radius of the earth (3956 miles) to get the arc in radians.

	//You are interested in particular values of the "radial" tc, namely 0, 
	//pi/2, pi, and 3*pi/2 radians (N, E, S, W). The first equation (lat) in 
	//the general formula above reduces to these four cases:

	//N (tc=0): lat = asin(sin(lat1)*cos(d)+cos(lat1)*sin(d))
	//              = asin(sin(lat1 + d)
	//              = lat1 + d

	//S (tc=pi): lat = asin(sin(lat1)*cos(d)-cos(lat1)*sin(d))
	//               = asin(sin(lat1 - d)
	//               = lat1 - d

	//E, W (tc=pi/2 or 3*pi/2): lat = asin(sin(lat1)*cos(d))

	//In the case of N and S, the longitude is just lon = lon1 (the same 
	//longitude as the starting point), because north-south lines follow 
	//lines of longitude. In the case of E and W, use the equations for dlon 
	//and lon to complete the calculation.


	//NOTE: The introduction to the Aviation Formulary page linked above 
	//says this:

	//"For the convenience of North Americans I will take North latitudes 
	//and West longitudes as positive and South and East negative. The 
	//longitude is the opposite of the usual mathematical convention."

	//If you use the convention that east longitudes are positive, you must 
	//make the following sign changes. In the first method,

	//   lon = mod(lon1+asin(sin(tc)*sin(d)/cos(lat))+pi,2*pi)-pi

	//In the second method,

	//   lon = mod(lon1+dlon+pi,2*pi)-pi

	//- Doctor Rick, The Math Forum
	//  http://mathforum.org/dr.math/   

	#endregion

	public struct GeoPoint {
	    public Double Latitude;
	    public Double Longitude;

	    public GeoPoint(Double latitude, Double longitude) {
		Latitude = Math.Round(latitude, 8);
		Longitude = Math.Round(longitude, 8);
	    }
	}

	/// <summary>
	///  Mean radius of Earth = 6,371 km = 3,959 (statute) mile = 3,440 nautical mile
	/// </summary>
	public const Double EARTH_MEAN_RADIUS = 3959;

	// tc = True Course

	public const Double ANGLE_NORTH = 0;
	public const Double ANGLE_SOUTH = 180;
	public const Double ANGLE_EAST = 90;
	public const Double ANGLE_WEST = 270;

	/// <summary>
	/// Calculate a point that is <para>distance</para> statute miles from <para>latitude</para> and <para>longitude</para> at an angle of <para>angle</para>.
	/// Uses the convention that east longitudes are positive.
	/// </summary>
	/// <param name="latitude">latitude in decimal degrees</param>
	/// <param name="longitude">longitude in deimal degrees</param>
	/// <param name="angle">angle in decimal degrees</param>
	/// <param name="distance">distance in statute miles</param>
	/// <returns></returns>
	public static GeoPoint CalculateRadicalPoint(Double latitude, Double longitude, Double angle, Double distance) {
	    Double d = ConvertStatuteToNauticalMiles(distance) * Math.PI / (180 * 60);
	    Double tc = deg2rad(angle);

	    Double lat1 = deg2rad(latitude);
	    Double lon1 = deg2rad(longitude);

	    Double lat = Math.Asin(Math.Sin(lat1) * Math.Cos(d) + Math.Cos(lat1) * Math.Sin(d) * Math.Cos(tc));
	    Double lon = ((lon1 + Math.Asin(Math.Sin(tc) * Math.Sin(d) / Math.Cos(lat)) + Math.PI) % (2 * Math.PI)) - Math.PI;

	    return new GeoPoint(rad2deg(lat), rad2deg(lon));
	}


	/// <summary>
	/// Calculate a point <para>distance</para> miles north of <para>lattitude</para> and <para>longitude</para>.
	/// </summary>
	/// <param name="latitude"></param>
	/// <param name="longitude"></param>
	/// <param name="distance">Statute miles</param>
	/// <returns></returns>
	public static GeoPoint CalculateNorthernRadicalPoint(Double latitude, Double longitude, Double distance) {
	    return CalculateRadicalPoint(latitude, longitude, ANGLE_NORTH, distance);
	}

	/// <summary>
	/// Calculate a point <para>distance</para> miles south of <para>lattitude</para> and <para>longitude</para>.
	/// </summary>
	/// <param name="latitude"></param>
	/// <param name="longitude"></param>
	/// <param name="distance">Statute miles</param>
	/// <returns></returns>
	public static GeoPoint CalculateSouthernRadicalPoint(Double latitude, Double longitude, Double distance) {
	    return CalculateRadicalPoint(latitude, longitude, ANGLE_SOUTH, distance);
	}

	/// <summary>
	/// Calculate a point <para>distance</para> miles east of <para>lattitude</para> and <para>longitude</para>.
	/// </summary>
	/// <param name="latitude"></param>
	/// <param name="longitude"></param>
	/// <param name="distance">Statute miles</param>
	/// <returns></returns>
	public static GeoPoint CalculateEasternRadicalPoint(Double latitude, Double longitude, Double distance) {
	    return CalculateRadicalPoint(latitude, longitude, ANGLE_EAST, distance);
	}

	/// <summary>
	/// Calculate a point <para>distance</para> miles west of <para>lattitude</para> and <para>longitude</para>.
	/// </summary>
	/// <param name="latitude"></param>
	/// <param name="longitude"></param>
	/// <param name="distance">Statute miles</param>
	/// <returns></returns>
	public static GeoPoint CalculateWesternRadicalPoint(Double latitude, Double longitude, Double distance) {
	    return CalculateRadicalPoint(latitude, longitude, ANGLE_WEST, distance);
	}

	/// <summary>
	/// Convert statute miles to nautical miles.
	/// </summary>
	/// <seealso cref="ConvertNauticalToStatuteMiles"/>
	/// <param name="statuteMiles"></param>
	/// <returns></returns>
	public static Double ConvertStatuteToNauticalMiles(Double statuteMiles) {
	    return statuteMiles / 1.150779;
	}

	/// <summary>
	/// Convert nautical miles to statute miles.
	/// </summary>
	/// <remarks>
	/// A statute mile is 5,280 feet in length.
	/// A nautical mile is 6,076.11549... feet in length.
	///
	/// The international standard definition is: 1 nautical mile = 1852 metres exactly.
	/// 1 nautical mile converts to:
	///     * 1.852 km (exact)
	///     * 1.150779 mile (statute)
	///     * 2025.372 yard
	///     * 6076.1155 feet
	///
	///  1 foot = 1200/3937 meters		
	/// </remarks>
	/// <param name="nauticalMiles"></param>
	/// <returns></returns>
	public static Double ConvertNauticalToStatuteMiles(Double nauticalMiles) {
	    return nauticalMiles * 1.150779;
	}

	#endregion
    }
}