using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class OrgGroupsEmployeesData : Spring2.Core.DataObject.DataObject
    {

	private IdType orgGroupsEmployeesID = IdType.DEFAULT;
	private IntegerType orgGroupsID = IntegerType.DEFAULT;
	private IntegerType orgEmployeesID = IntegerType.DEFAULT;
	private BooleanType isContentManager = BooleanType.DEFAULT;

	public static readonly String ORGGROUPSEMPLOYEESID = "OrgGroupsEmployeesID";
	public static readonly String ORGGROUPSID = "OrgGroupsID";
	public static readonly String ORGEMPLOYEESID = "OrgEmployeesID";
	public static readonly String ISCONTENTMANAGER = "IsContentManager";

	public IdType OrgGroupsEmployeesID
	{
	    get
	    {
		return this.orgGroupsEmployeesID;
	    }
	    set
	    {
		this.orgGroupsEmployeesID = value;
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

	public BooleanType IsContentManager
	{
	    get
	    {
		return this.isContentManager;
	    }
	    set
	    {
		this.isContentManager = value;
	    }
	}
    }
}
