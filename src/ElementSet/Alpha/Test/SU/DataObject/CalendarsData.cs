using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class CalendarsData : Spring2.Core.DataObject.DataObject
    {

	private IdType calendarsID = IdType.DEFAULT;
	private StringType description = StringType.DEFAULT;
	private IntegerType orgGroupsID = IntegerType.DEFAULT;

	public static readonly String CALENDARSID = "CalendarsID";
	public static readonly String DESCRIPTION = "Description";
	public static readonly String ORGGROUPSID = "OrgGroupsID";

	public IdType CalendarsID
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

	public IntegerType OrgGroupsID
	{
	    get
	    {
		return this.orgGroupsID;
	    }
	    set
	    {
		this.orgGroupsID = value;
	    }
	}
    }
}
