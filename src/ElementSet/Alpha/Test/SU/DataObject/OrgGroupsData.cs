using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class OrgGroupsData : Spring2.Core.DataObject.DataObject
    {

	private IdType orgGroupsID = IdType.DEFAULT;
	private StringType description = StringType.DEFAULT;
	private BooleanType isActive = BooleanType.DEFAULT;

	public static readonly String ORGGROUPSID = "OrgGroupsID";
	public static readonly String DESCRIPTION = "Description";
	public static readonly String ISACTIVE = "IsActive";

	public IdType OrgGroupsID
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
    }
}
