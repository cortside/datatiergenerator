using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class EmployeeControlSubscriptionsViewData : Spring2.Core.DataObject.DataObject
    {

	private StringType src = StringType.DEFAULT;
	private StringType description = StringType.DEFAULT;
	private StringType classSrc = StringType.DEFAULT;
	private StringType classDescription = StringType.DEFAULT;
	private BooleanType allowUnsubscribe = BooleanType.DEFAULT;
	private IntegerType sortOrder = IntegerType.DEFAULT;
	private IntegerType orgEmployeesID = IntegerType.DEFAULT;
	private IntegerType orgGroupsID = IntegerType.DEFAULT;
	private IntegerType controlsID = IntegerType.DEFAULT;
	private IntegerType controlSubscriptionsID = IntegerType.DEFAULT;
	private IntegerType itemID = IntegerType.DEFAULT;
	private IntegerType controlsClassesID = IntegerType.DEFAULT;

	public static readonly String SRC = "Src";
	public static readonly String DESCRIPTION = "Description";
	public static readonly String CLASSSRC = "ClassSrc";
	public static readonly String CLASSDESCRIPTION = "ClassDescription";
	public static readonly String ALLOWUNSUBSCRIBE = "AllowUnsubscribe";
	public static readonly String SORTORDER = "SortOrder";
	public static readonly String ORGEMPLOYEESID = "OrgEmployeesID";
	public static readonly String ORGGROUPSID = "OrgGroupsID";
	public static readonly String CONTROLSID = "ControlsID";
	public static readonly String CONTROLSUBSCRIPTIONSID = "ControlSubscriptionsID";
	public static readonly String ITEMID = "ItemID";
	public static readonly String CONTROLSCLASSESID = "ControlsClassesID";

	public StringType Src
	{
	    get
	    {
		return this.src;
	    }
	    set
	    {
		this.src = value;
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

	public StringType ClassSrc
	{
	    get
	    {
		return this.classSrc;
	    }
	    set
	    {
		this.classSrc = value;
	    }
	}

	public StringType ClassDescription
	{
	    get
	    {
		return this.classDescription;
	    }
	    set
	    {
		this.classDescription = value;
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

	public IntegerType ControlSubscriptionsID
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
    }
}
