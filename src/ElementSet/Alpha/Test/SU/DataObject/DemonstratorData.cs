using System;

using Spring2.Core.Types;

using StampinUp.Core.Types;
using StampinUp.Types;

namespace StampinUp.DataObject
{
    public class DemonstratorData : Spring2.Core.DataObject.DataObject
    {

	private DemoIdType demoId = DemoIdType.DEFAULT;
	private DemoIdType sponsorId = DemoIdType.DEFAULT;
	private StringType busEntityName = StringType.DEFAULT;
	private StringType tin = StringType.DEFAULT;
	private IdType busEntityTypeID = IdType.DEFAULT;
	private CountryEnum countryOfOrigin = CountryEnum.DEFAULT;
	private StringType stateOfOrigin = StringType.DEFAULT;
	private DateType busEntityEffectDate = DateType.DEFAULT;
	private DateType appToOpDate = DateType.DEFAULT;
	private DateType noticeOfIntentDate = DateType.DEFAULT;
	private DateType duePerformDate = DateType.DEFAULT;
	private DateType authorizationDate = DateType.DEFAULT;
	private DateType formationDate = DateType.DEFAULT;
	private StringType shipToAddress1 = StringType.DEFAULT;
	private StringType shipToAddress2 = StringType.DEFAULT;
	private StringType shipToCity = StringType.DEFAULT;
	private StringType shipToState = StringType.DEFAULT;
	private StringType shipToCounty = StringType.DEFAULT;
	private StringType shipToZip = StringType.DEFAULT;
	private StringType shipToZip4 = StringType.DEFAULT;
	private StringType demoLvl = StringType.DEFAULT;
	private StringType status = StringType.DEFAULT;
	private DateType statusDate = DateType.DEFAULT;
	private StringType lastEmpName = StringType.DEFAULT;
	private DateType lastChangeDate = DateType.DEFAULT;
	private StringType fax = StringType.DEFAULT;
	private BooleanType starterKitOrdered = BooleanType.DEFAULT;
	private IntegerType starterKit = IntegerType.DEFAULT;
	private DateType startdate = DateType.DEFAULT;
	private StringType promoFlag = StringType.DEFAULT;
	private BooleanType isOrderingDisabled = BooleanType.DEFAULT;
	private StringType password = StringType.DEFAULT;
	private StringType geocode = StringType.DEFAULT;
	private StringType inout = StringType.DEFAULT;
	private StringType localcode1 = StringType.DEFAULT;
	private StringType localcode2 = StringType.DEFAULT;
	private StringType localcode3 = StringType.DEFAULT;
	private StringType localcode4 = StringType.DEFAULT;
	private StringType localcode5 = StringType.DEFAULT;
	private DemoIdType sponsorUp2 = DemoIdType.DEFAULT;
	private DemoIdType sponsorUp3 = DemoIdType.DEFAULT;
	private StringType email = StringType.DEFAULT;
	private DemoIdType sponsorUp4 = DemoIdType.DEFAULT;
	private DemoIdType sponsorUp5 = DemoIdType.DEFAULT;
	private DateType origStartDate = DateType.DEFAULT;
	private DemoIdType origDemoId = DemoIdType.DEFAULT;
	private BooleanType referral = BooleanType.DEFAULT;
	private DateType lastReferralDate = DateType.DEFAULT;
	private DecimalType latitude = DecimalType.DEFAULT;
	private DecimalType longitude = DecimalType.DEFAULT;
	private StringType accountVerified = StringType.DEFAULT;
	private StringType allElectronic = StringType.DEFAULT;
	private DateType accountVerifiedDate = DateType.DEFAULT;
	private DateType aPOStatusDate = DateType.DEFAULT;
	private BooleanType isPasswordDisabled = BooleanType.DEFAULT;
	private BooleanType isPasswordTemporary = BooleanType.DEFAULT;
	private BooleanType aPOFlag = BooleanType.DEFAULT;
	private BooleanType isSmallSupplier = BooleanType.DEFAULT;
	private BooleanType receivedFreeShip = BooleanType.DEFAULT;
	private BooleanType wantPrintedReports = BooleanType.DEFAULT;
	private PasswordQuestionEnum questionId = PasswordQuestionEnum.DEFAULT;
	private StringType answer = StringType.DEFAULT;

	public static readonly String DEMOID = "DemoId";
	public static readonly String SPONSORID = "SponsorId";
	public static readonly String BUSENTITYNAME = "BusEntityName";
	public static readonly String TIN = "Tin";
	public static readonly String BUSENTITYTYPEID = "BusEntityTypeID";
	public static readonly String COUNTRYOFORIGIN = "CountryOfOrigin";
	public static readonly String STATEOFORIGIN = "StateOfOrigin";
	public static readonly String BUSENTITYEFFECTDATE = "BusEntityEffectDate";
	public static readonly String APPTOOPDATE = "AppToOpDate";
	public static readonly String NOTICEOFINTENTDATE = "NoticeOfIntentDate";
	public static readonly String DUEPERFORMDATE = "DuePerformDate";
	public static readonly String AUTHORIZATIONDATE = "AuthorizationDate";
	public static readonly String FORMATIONDATE = "FormationDate";
	public static readonly String SHIPTOADDRESS1 = "ShipToAddress1";
	public static readonly String SHIPTOADDRESS2 = "ShipToAddress2";
	public static readonly String SHIPTOCITY = "ShipToCity";
	public static readonly String SHIPTOSTATE = "ShipToState";
	public static readonly String SHIPTOCOUNTY = "ShipToCounty";
	public static readonly String SHIPTOZIP = "ShipToZip";
	public static readonly String SHIPTOZIP4 = "ShipToZip4";
	public static readonly String DEMOLVL = "DemoLvl";
	public static readonly String STATUS = "Status";
	public static readonly String STATUSDATE = "StatusDate";
	public static readonly String LASTEMPNAME = "LastEmpName";
	public static readonly String LASTCHANGEDATE = "LastChangeDate";
	public static readonly String FAX = "Fax";
	public static readonly String STARTERKITORDERED = "StarterKitOrdered";
	public static readonly String STARTERKIT = "StarterKit";
	public static readonly String STARTDATE = "Startdate";
	public static readonly String PROMOFLAG = "PromoFlag";
	public static readonly String ISORDERINGDISABLED = "IsOrderingDisabled";
	public static readonly String PASSWORD = "Password";
	public static readonly String GEOCODE = "Geocode";
	public static readonly String INOUT = "Inout";
	public static readonly String LOCALCODE1 = "Localcode1";
	public static readonly String LOCALCODE2 = "Localcode2";
	public static readonly String LOCALCODE3 = "Localcode3";
	public static readonly String LOCALCODE4 = "Localcode4";
	public static readonly String LOCALCODE5 = "Localcode5";
	public static readonly String SPONSORUP2 = "SponsorUp2";
	public static readonly String SPONSORUP3 = "SponsorUp3";
	public static readonly String EMAIL = "Email";
	public static readonly String SPONSORUP4 = "SponsorUp4";
	public static readonly String SPONSORUP5 = "SponsorUp5";
	public static readonly String ORIGSTARTDATE = "OrigStartDate";
	public static readonly String ORIGDEMOID = "OrigDemoId";
	public static readonly String REFERRAL = "Referral";
	public static readonly String LASTREFERRALDATE = "LastReferralDate";
	public static readonly String LATITUDE = "Latitude";
	public static readonly String LONGITUDE = "Longitude";
	public static readonly String ACCOUNTVERIFIED = "AccountVerified";
	public static readonly String ALLELECTRONIC = "AllElectronic";
	public static readonly String ACCOUNTVERIFIEDDATE = "AccountVerifiedDate";
	public static readonly String APOSTATUSDATE = "APOStatusDate";
	public static readonly String ISPASSWORDDISABLED = "IsPasswordDisabled";
	public static readonly String ISPASSWORDTEMPORARY = "IsPasswordTemporary";
	public static readonly String APOFLAG = "APOFlag";
	public static readonly String ISSMALLSUPPLIER = "IsSmallSupplier";
	public static readonly String RECEIVEDFREESHIP = "ReceivedFreeShip";
	public static readonly String WANTPRINTEDREPORTS = "WantPrintedReports";
	public static readonly String QUESTIONID = "QuestionId";
	public static readonly String ANSWER = "Answer";

	public DemoIdType DemoId
	{
	    get
	    {
		return this.demoId;
	    }
	    set
	    {
		this.demoId = value;
	    }
	}

	public DemoIdType SponsorId
	{
	    get
	    {
		return this.sponsorId;
	    }
	    set
	    {
		this.sponsorId = value;
	    }
	}

	public StringType BusEntityName
	{
	    get
	    {
		return this.busEntityName;
	    }
	    set
	    {
		this.busEntityName = value;
	    }
	}

	public StringType Tin
	{
	    get
	    {
		return this.tin;
	    }
	    set
	    {
		this.tin = value;
	    }
	}

	public IdType BusEntityTypeID
	{
	    get
	    {
		return this.busEntityTypeID;
	    }
	    set
	    {
		this.busEntityTypeID = value;
	    }
	}

	public CountryEnum CountryOfOrigin
	{
	    get
	    {
		return this.countryOfOrigin;
	    }
	    set
	    {
		this.countryOfOrigin = value;
	    }
	}

	public StringType StateOfOrigin
	{
	    get
	    {
		return this.stateOfOrigin;
	    }
	    set
	    {
		this.stateOfOrigin = value;
	    }
	}

	public DateType BusEntityEffectDate
	{
	    get
	    {
		return this.busEntityEffectDate;
	    }
	    set
	    {
		this.busEntityEffectDate = value;
	    }
	}

	public DateType AppToOpDate
	{
	    get
	    {
		return this.appToOpDate;
	    }
	    set
	    {
		this.appToOpDate = value;
	    }
	}

	public DateType NoticeOfIntentDate
	{
	    get
	    {
		return this.noticeOfIntentDate;
	    }
	    set
	    {
		this.noticeOfIntentDate = value;
	    }
	}

	public DateType DuePerformDate
	{
	    get
	    {
		return this.duePerformDate;
	    }
	    set
	    {
		this.duePerformDate = value;
	    }
	}

	public DateType AuthorizationDate
	{
	    get
	    {
		return this.authorizationDate;
	    }
	    set
	    {
		this.authorizationDate = value;
	    }
	}

	public DateType FormationDate
	{
	    get
	    {
		return this.formationDate;
	    }
	    set
	    {
		this.formationDate = value;
	    }
	}

	public StringType ShipToAddress1
	{
	    get
	    {
		return this.shipToAddress1;
	    }
	    set
	    {
		this.shipToAddress1 = value;
	    }
	}

	public StringType ShipToAddress2
	{
	    get
	    {
		return this.shipToAddress2;
	    }
	    set
	    {
		this.shipToAddress2 = value;
	    }
	}

	public StringType ShipToCity
	{
	    get
	    {
		return this.shipToCity;
	    }
	    set
	    {
		this.shipToCity = value;
	    }
	}

	public StringType ShipToState
	{
	    get
	    {
		return this.shipToState;
	    }
	    set
	    {
		this.shipToState = value;
	    }
	}

	public StringType ShipToCounty
	{
	    get
	    {
		return this.shipToCounty;
	    }
	    set
	    {
		this.shipToCounty = value;
	    }
	}

	public StringType ShipToZip
	{
	    get
	    {
		return this.shipToZip;
	    }
	    set
	    {
		this.shipToZip = value;
	    }
	}

	public StringType ShipToZip4
	{
	    get
	    {
		return this.shipToZip4;
	    }
	    set
	    {
		this.shipToZip4 = value;
	    }
	}

	public StringType DemoLvl
	{
	    get
	    {
		return this.demoLvl;
	    }
	    set
	    {
		this.demoLvl = value;
	    }
	}

	public StringType Status
	{
	    get
	    {
		return this.status;
	    }
	    set
	    {
		this.status = value;
	    }
	}

	public DateType StatusDate
	{
	    get
	    {
		return this.statusDate;
	    }
	    set
	    {
		this.statusDate = value;
	    }
	}

	public StringType LastEmpName
	{
	    get
	    {
		return this.lastEmpName;
	    }
	    set
	    {
		this.lastEmpName = value;
	    }
	}

	public DateType LastChangeDate
	{
	    get
	    {
		return this.lastChangeDate;
	    }
	    set
	    {
		this.lastChangeDate = value;
	    }
	}

	public StringType Fax
	{
	    get
	    {
		return this.fax;
	    }
	    set
	    {
		this.fax = value;
	    }
	}

	public BooleanType StarterKitOrdered
	{
	    get
	    {
		return this.starterKitOrdered;
	    }
	    set
	    {
		this.starterKitOrdered = value;
	    }
	}

	public IntegerType StarterKit
	{
	    get
	    {
		return this.starterKit;
	    }
	    set
	    {
		this.starterKit = value;
	    }
	}

	public DateType Startdate
	{
	    get
	    {
		return this.startdate;
	    }
	    set
	    {
		this.startdate = value;
	    }
	}

	public StringType PromoFlag
	{
	    get
	    {
		return this.promoFlag;
	    }
	    set
	    {
		this.promoFlag = value;
	    }
	}

	public BooleanType IsOrderingDisabled
	{
	    get
	    {
		return this.isOrderingDisabled;
	    }
	    set
	    {
		this.isOrderingDisabled = value;
	    }
	}

	public StringType Password
	{
	    get
	    {
		return this.password;
	    }
	    set
	    {
		this.password = value;
	    }
	}

	public StringType Geocode
	{
	    get
	    {
		return this.geocode;
	    }
	    set
	    {
		this.geocode = value;
	    }
	}

	public StringType Inout
	{
	    get
	    {
		return this.inout;
	    }
	    set
	    {
		this.inout = value;
	    }
	}

	public StringType Localcode1
	{
	    get
	    {
		return this.localcode1;
	    }
	    set
	    {
		this.localcode1 = value;
	    }
	}

	public StringType Localcode2
	{
	    get
	    {
		return this.localcode2;
	    }
	    set
	    {
		this.localcode2 = value;
	    }
	}

	public StringType Localcode3
	{
	    get
	    {
		return this.localcode3;
	    }
	    set
	    {
		this.localcode3 = value;
	    }
	}

	public StringType Localcode4
	{
	    get
	    {
		return this.localcode4;
	    }
	    set
	    {
		this.localcode4 = value;
	    }
	}

	public StringType Localcode5
	{
	    get
	    {
		return this.localcode5;
	    }
	    set
	    {
		this.localcode5 = value;
	    }
	}

	public DemoIdType SponsorUp2
	{
	    get
	    {
		return this.sponsorUp2;
	    }
	    set
	    {
		this.sponsorUp2 = value;
	    }
	}

	public DemoIdType SponsorUp3
	{
	    get
	    {
		return this.sponsorUp3;
	    }
	    set
	    {
		this.sponsorUp3 = value;
	    }
	}

	public StringType Email
	{
	    get
	    {
		return this.email;
	    }
	    set
	    {
		this.email = value;
	    }
	}

	public DemoIdType SponsorUp4
	{
	    get
	    {
		return this.sponsorUp4;
	    }
	    set
	    {
		this.sponsorUp4 = value;
	    }
	}

	public DemoIdType SponsorUp5
	{
	    get
	    {
		return this.sponsorUp5;
	    }
	    set
	    {
		this.sponsorUp5 = value;
	    }
	}

	public DateType OrigStartDate
	{
	    get
	    {
		return this.origStartDate;
	    }
	    set
	    {
		this.origStartDate = value;
	    }
	}

	public DemoIdType OrigDemoId
	{
	    get
	    {
		return this.origDemoId;
	    }
	    set
	    {
		this.origDemoId = value;
	    }
	}

	public BooleanType Referral
	{
	    get
	    {
		return this.referral;
	    }
	    set
	    {
		this.referral = value;
	    }
	}

	public DateType LastReferralDate
	{
	    get
	    {
		return this.lastReferralDate;
	    }
	    set
	    {
		this.lastReferralDate = value;
	    }
	}

	public DecimalType Latitude
	{
	    get
	    {
		return this.latitude;
	    }
	    set
	    {
		this.latitude = value;
	    }
	}

	public DecimalType Longitude
	{
	    get
	    {
		return this.longitude;
	    }
	    set
	    {
		this.longitude = value;
	    }
	}

	public StringType AccountVerified
	{
	    get
	    {
		return this.accountVerified;
	    }
	    set
	    {
		this.accountVerified = value;
	    }
	}

	public StringType AllElectronic
	{
	    get
	    {
		return this.allElectronic;
	    }
	    set
	    {
		this.allElectronic = value;
	    }
	}

	public DateType AccountVerifiedDate
	{
	    get
	    {
		return this.accountVerifiedDate;
	    }
	    set
	    {
		this.accountVerifiedDate = value;
	    }
	}

	public DateType APOStatusDate
	{
	    get
	    {
		return this.aPOStatusDate;
	    }
	    set
	    {
		this.aPOStatusDate = value;
	    }
	}

	public BooleanType IsPasswordDisabled
	{
	    get
	    {
		return this.isPasswordDisabled;
	    }
	    set
	    {
		this.isPasswordDisabled = value;
	    }
	}

	public BooleanType IsPasswordTemporary
	{
	    get
	    {
		return this.isPasswordTemporary;
	    }
	    set
	    {
		this.isPasswordTemporary = value;
	    }
	}

	public BooleanType APOFlag
	{
	    get
	    {
		return this.aPOFlag;
	    }
	    set
	    {
		this.aPOFlag = value;
	    }
	}

	public BooleanType IsSmallSupplier
	{
	    get
	    {
		return this.isSmallSupplier;
	    }
	    set
	    {
		this.isSmallSupplier = value;
	    }
	}

	public BooleanType ReceivedFreeShip
	{
	    get
	    {
		return this.receivedFreeShip;
	    }
	    set
	    {
		this.receivedFreeShip = value;
	    }
	}

	public BooleanType WantPrintedReports
	{
	    get
	    {
		return this.wantPrintedReports;
	    }
	    set
	    {
		this.wantPrintedReports = value;
	    }
	}

	public PasswordQuestionEnum QuestionId
	{
	    get
	    {
		return this.questionId;
	    }
	    set
	    {
		this.questionId = value;
	    }
	}

	public StringType Answer
	{
	    get
	    {
		return this.answer;
	    }
	    set
	    {
		this.answer = value;
	    }
	}
    }
}
