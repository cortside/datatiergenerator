using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class ControlSubscriptionsData : Spring2.Core.DataObject.DataObject
    {

	private IdType controlSubscriptionsID = IdType.DEFAULT;
	private IntegerType orgEmployeesID = IntegerType.DEFAULT;
	private IntegerType orgGroupsID = IntegerType.DEFAULT;
	private IntegerType controlsID = IntegerType.DEFAULT;
	private IntegerType sortOrder = IntegerType.DEFAULT;
	private BooleanType allowUnsubscribe = BooleanType.DEFAULT;
	private DateType dateSubscribed = DateType.DEFAULT;
	private IntegerType controlsClassesID = IntegerType.DEFAULT;
	private IntegerType itemID = IntegerType.DEFAULT;

	public static readonly String CONTROLSUBSCRIPTIONSID = "ControlSubscriptionsID";
	public static readonly String ORGEMPLOYEESID = "OrgEmployeesID";
	public static readonly String ORGGROUPSID = "OrgGroupsID";
	public static readonly String CONTROLSID = "ControlsID";
	public static readonly String SORTORDER = "SortOrder";
	public static readonly String ALLOWUNSUBSCRIBE = "AllowUnsubscribe";
	public static readonly String DATESUBSCRIBED = "DateSubscribed";
	public static readonly String CONTROLSCLASSESID = "ControlsClassesID";
	public static readonly String ITEMID = "ItemID";

	public IdType ControlSubscriptionsID
	{
	    get
	    {
		return this.controlSubscriptionsID;
	    }
	    set
	    {
		this.controlSubscriptionsID = value;
	    }
	}

	public IntegerType OrgEmployeesID
	{
	    get
	    {
		return this.orgEmployeesID;
	    }
	    set
	    {
		this.orgEmployeesID = value;
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

	public IntegerType ControlsID
	{
	    get
	    {
		return this.controlsID;
	    }
	    set
	    {
		this.controlsID = value;
	    }
	}

	public IntegerType SortOrder
	{
	    get
	    {
		return this.sortOrder;
	    }
	    set
	    {
		this.sortOrder = value;
	    }
	}

	public BooleanType AllowUnsubscribe
	{
	    get
	    {
		return this.allowUnsubscribe;
	    }
	    set
	    {
		this.allowUnsubscribe = value;
	    }
	}

	public DateType DateSubscribed
	{
	    get
	    {
		return this.dateSubscribed;
	    }
	    set
	    {
		this.dateSubscribed = value;
	    }
	}

	public IntegerType ControlsClassesID
	{
	    get
	    {
		return this.controlsClassesID;
	    }
	    set
	    {
		this.controlsClassesID = value;
	    }
	}

	public IntegerType ItemID
	{
	    get
	    {
		return this.itemID;
	    }
	    set
	    {
		this.itemID = value;
	    }
	}
    }
}
