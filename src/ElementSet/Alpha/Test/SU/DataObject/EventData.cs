using System;

using Spring2.Core.Types;

using StampinUp.Types;

namespace StampinUp.DataObject
{
    public class EventData : Spring2.Core.DataObject.DataObject
    {

	private IdType eventId = IdType.DEFAULT;
	private StringType description = StringType.DEFAULT;
	private IntegerType internalAccess = IntegerType.DEFAULT;
	private IntegerType externalAccess = IntegerType.DEFAULT;
	private DateType closeDate = DateType.DEFAULT;
	private DateType startDate = DateType.DEFAULT;
	private StringType demoItemId = StringType.DEFAULT;
	private StringType guestItemId = StringType.DEFAULT;
	private IntegerType maxRegistrations = IntegerType.DEFAULT;
	private StringType warning = StringType.DEFAULT;
	private IntegerType minDemoLevel = IntegerType.DEFAULT;
	private IntegerType spouseOnly = IntegerType.DEFAULT;
	private IntegerType maxGuests = IntegerType.DEFAULT;
	private StringType askTerms = StringType.DEFAULT;
	private StringType askColor = StringType.DEFAULT;
	private StringType demoFirstNameOnly = StringType.DEFAULT;
	private StringType askDemoMobility = StringType.DEFAULT;
	private StringType askDemoHearing = StringType.DEFAULT;
	private StringType jointDemoOnly = StringType.DEFAULT;
	private StringType showFinalSplash = StringType.DEFAULT;
	private StringType noGuestMeal = StringType.DEFAULT;
	private IdType dependentEvent = IdType.DEFAULT;
	private StringType allowCheckPayment = StringType.DEFAULT;
	private IntegerType noGuestPeriod = IntegerType.DEFAULT;
	private CountryEnum countryCode = CountryEnum.DEFAULT;
	private StringType eventType = StringType.DEFAULT;
	private DateType eventDate = DateType.DEFAULT;
	private DateType gLPostedDate = DateType.DEFAULT;
	private StringType cancelFeeItemId = StringType.DEFAULT;
	private StringType guestCancelFeeItemId = StringType.DEFAULT;

	public static readonly String EVENTID = "EventId";
	public static readonly String DESCRIPTION = "Description";
	public static readonly String INTERNALACCESS = "InternalAccess";
	public static readonly String EXTERNALACCESS = "ExternalAccess";
	public static readonly String CLOSEDATE = "CloseDate";
	public static readonly String STARTDATE = "StartDate";
	public static readonly String DEMOITEMID = "DemoItemId";
	public static readonly String GUESTITEMID = "GuestItemId";
	public static readonly String MAXREGISTRATIONS = "MaxRegistrations";
	public static readonly String WARNING = "Warning";
	public static readonly String MINDEMOLEVEL = "MinDemoLevel";
	public static readonly String SPOUSEONLY = "SpouseOnly";
	public static readonly String MAXGUESTS = "MaxGuests";
	public static readonly String ASKTERMS = "AskTerms";
	public static readonly String ASKCOLOR = "AskColor";
	public static readonly String DEMOFIRSTNAMEONLY = "DemoFirstNameOnly";
	public static readonly String ASKDEMOMOBILITY = "AskDemoMobility";
	public static readonly String ASKDEMOHEARING = "AskDemoHearing";
	public static readonly String JOINTDEMOONLY = "JointDemoOnly";
	public static readonly String SHOWFINALSPLASH = "ShowFinalSplash";
	public static readonly String NOGUESTMEAL = "NoGuestMeal";
	public static readonly String DEPENDENTEVENT = "DependentEvent";
	public static readonly String ALLOWCHECKPAYMENT = "AllowCheckPayment";
	public static readonly String NOGUESTPERIOD = "NoGuestPeriod";
	public static readonly String COUNTRYCODE = "CountryCode";
	public static readonly String EVENTTYPE = "EventType";
	public static readonly String EVENTDATE = "EventDate";
	public static readonly String GLPOSTEDDATE = "GLPostedDate";
	public static readonly String CANCELFEEITEMID = "CancelFeeItemId";
	public static readonly String GUESTCANCELFEEITEMID = "GuestCancelFeeItemId";

	public IdType EventId
	{
	    get
	    {
		return this.eventId;
	    }
	    set
	    {
		this.eventId = value;
	    }
	}

	public StringType Description
	{
	    get
	    {
		return this.description;
	    }
	    set
	    {
		this.description = value;
	    }
	}

	public IntegerType InternalAccess
	{
	    get
	    {
		return this.internalAccess;
	    }
	    set
	    {
		this.internalAccess = value;
	    }
	}

	public IntegerType ExternalAccess
	{
	    get
	    {
		return this.externalAccess;
	    }
	    set
	    {
		this.externalAccess = value;
	    }
	}

	public DateType CloseDate
	{
	    get
	    {
		return this.closeDate;
	    }
	    set
	    {
		this.closeDate = value;
	    }
	}

	public DateType StartDate
	{
	    get
	    {
		return this.startDate;
	    }
	    set
	    {
		this.startDate = value;
	    }
	}

	public StringType DemoItemId
	{
	    get
	    {
		return this.demoItemId;
	    }
	    set
	    {
		this.demoItemId = value;
	    }
	}

	public StringType GuestItemId
	{
	    get
	    {
		return this.guestItemId;
	    }
	    set
	    {
		this.guestItemId = value;
	    }
	}

	public IntegerType MaxRegistrations
	{
	    get
	    {
		return this.maxRegistrations;
	    }
	    set
	    {
		this.maxRegistrations = value;
	    }
	}

	public StringType Warning
	{
	    get
	    {
		return this.warning;
	    }
	    set
	    {
		this.warning = value;
	    }
	}

	public IntegerType MinDemoLevel
	{
	    get
	    {
		return this.minDemoLevel;
	    }
	    set
	    {
		this.minDemoLevel = value;
	    }
	}

	public IntegerType SpouseOnly
	{
	    get
	    {
		return this.spouseOnly;
	    }
	    set
	    {
		this.spouseOnly = value;
	    }
	}

	public IntegerType MaxGuests
	{
	    get
	    {
		return this.maxGuests;
	    }
	    set
	    {
		this.maxGuests = value;
	    }
	}

	public StringType AskTerms
	{
	    get
	    {
		return this.askTerms;
	    }
	    set
	    {
		this.askTerms = value;
	    }
	}

	public StringType AskColor
	{
	    get
	    {
		return this.askColor;
	    }
	    set
	    {
		this.askColor = value;
	    }
	}

	public StringType DemoFirstNameOnly
	{
	    get
	    {
		return this.demoFirstNameOnly;
	    }
	    set
	    {
		this.demoFirstNameOnly = value;
	    }
	}

	public StringType AskDemoMobility
	{
	    get
	    {
		return this.askDemoMobility;
	    }
	    set
	    {
		this.askDemoMobility = value;
	    }
	}

	public StringType AskDemoHearing
	{
	    get
	    {
		return this.askDemoHearing;
	    }
	    set
	    {
		this.askDemoHearing = value;
	    }
	}

	public StringType JointDemoOnly
	{
	    get
	    {
		return this.jointDemoOnly;
	    }
	    set
	    {
		this.jointDemoOnly = value;
	    }
	}

	public StringType ShowFinalSplash
	{
	    get
	    {
		return this.showFinalSplash;
	    }
	    set
	    {
		this.showFinalSplash = value;
	    }
	}

	public StringType NoGuestMeal
	{
	    get
	    {
		return this.noGuestMeal;
	    }
	    set
	    {
		this.noGuestMeal = value;
	    }
	}

	public IdType DependentEvent
	{
	    get
	    {
		return this.dependentEvent;
	    }
	    set
	    {
		this.dependentEvent = value;
	    }
	}

	public StringType AllowCheckPayment
	{
	    get
	    {
		return this.allowCheckPayment;
	    }
	    set
	    {
		this.allowCheckPayment = value;
	    }
	}

	public IntegerType NoGuestPeriod
	{
	    get
	    {
		return this.noGuestPeriod;
	    }
	    set
	    {
		this.noGuestPeriod = value;
	    }
	}

	public CountryEnum CountryCode
	{
	    get
	    {
		return this.countryCode;
	    }
	    set
	    {
		this.countryCode = value;
	    }
	}

	public StringType EventType
	{
	    get
	    {
		return this.eventType;
	    }
	    set
	    {
		this.eventType = value;
	    }
	}

	public DateType EventDate
	{
	    get
	    {
		return this.eventDate;
	    }
	    set
	    {
		this.eventDate = value;
	    }
	}

	public DateType GLPostedDate
	{
	    get
	    {
		return this.gLPostedDate;
	    }
	    set
	    {
		this.gLPostedDate = value;
	    }
	}

	public StringType CancelFeeItemId
	{
	    get
	    {
		return this.cancelFeeItemId;
	    }
	    set
	    {
		this.cancelFeeItemId = value;
	    }
	}

	public StringType GuestCancelFeeItemId
	{
	    get
	    {
		return this.guestCancelFeeItemId;
	    }
	    set
	    {
		this.guestCancelFeeItemId = value;
	    }
	}
    }
}
