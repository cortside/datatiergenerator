using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class CalendarsItemsData : Spring2.Core.DataObject.DataObject
    {

	private IdType calendarsItemsID = IdType.DEFAULT;
	private StringType description = StringType.DEFAULT;
	private StringType summary = StringType.DEFAULT;
	private DateType eventDateStart = DateType.DEFAULT;
	private DateType eventDateEnd = DateType.DEFAULT;
	private BooleanType isActive = BooleanType.DEFAULT;
	private IntegerType calendarsID = IntegerType.DEFAULT;
	private BooleanType isPublic = BooleanType.DEFAULT;

	public static readonly String CALENDARSITEMSID = "CalendarsItemsID";
	public static readonly String DESCRIPTION = "Description";
	public static readonly String SUMMARY = "Summary";
	public static readonly String EVENTDATESTART = "EventDateStart";
	public static readonly String EVENTDATEEND = "EventDateEnd";
	public static readonly String ISACTIVE = "IsActive";
	public static readonly String CALENDARSID = "CalendarsID";
	public static readonly String ISPUBLIC = "IsPublic";

	public IdType CalendarsItemsID
	{
	    get
	    {
		return this.calendarsItemsID;
	    }
	    set
	    {
		this.calendarsItemsID = value;
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

	public StringType Summary
	{
	    get
	    {
		return this.summary;
	    }
	    set
	    {
		this.summary = value;
	    }
	}

	public DateType EventDateStart
	{
	    get
	    {
		return this.eventDateStart;
	    }
	    set
	    {
		this.eventDateStart = value;
	    }
	}

	public DateType EventDateEnd
	{
	    get
	    {
		return this.eventDateEnd;
	    }
	    set
	    {
		this.eventDateEnd = value;
	    }
	}

	public BooleanType IsActive
	{
	    get
	    {
		return this.isActive;
	    }
	    set
	    {
		this.isActive = value;
	    }
	}

	public IntegerType CalendarsID
	{
	    get
	    {
		return this.calendarsID;
	    }
	    set
	    {
		this.calendarsID = value;
	    }
	}

	public BooleanType IsPublic
	{
	    get
	    {
		return this.isPublic;
	    }
	    set
	    {
		this.isPublic = value;
	    }
	}
    }
}
