using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class CalendarsItemsLocationsData : Spring2.Core.DataObject.DataObject
    {

	private IdType calendarsItemsLocationsID = IdType.DEFAULT;
	private IntegerType calendarsItemsID = IntegerType.DEFAULT;
	private IntegerType orgLocationsID = IntegerType.DEFAULT;

	public static readonly String CALENDARSITEMSLOCATIONSID = "CalendarsItemsLocationsID";
	public static readonly String CALENDARSITEMSID = "CalendarsItemsID";
	public static readonly String ORGLOCATIONSID = "OrgLocationsID";

	public IdType CalendarsItemsLocationsID
	{
	    get
	    {
		return this.calendarsItemsLocationsID;
	    }
	    set
	    {
		this.calendarsItemsLocationsID = value;
	    }
	}

	public IntegerType CalendarsItemsID
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

	public IntegerType OrgLocationsID
	{
	    get
	    {
		return this.orgLocationsID;
	    }
	    set
	    {
		this.orgLocationsID = value;
	    }
	}
    }
}
