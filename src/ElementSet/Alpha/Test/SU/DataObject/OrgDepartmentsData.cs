using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class OrgDepartmentsData : Spring2.Core.DataObject.DataObject
    {

	private IdType orgDepartmentsID = IdType.DEFAULT;
	private StringType description = StringType.DEFAULT;
	private BooleanType active = BooleanType.DEFAULT;
	private IntegerType orgDivisionsID = IntegerType.DEFAULT;
	private StringType gLAccount = StringType.DEFAULT;

	public static readonly String ORGDEPARTMENTSID = "OrgDepartmentsID";
	public static readonly String DESCRIPTION = "Description";
	public static readonly String ACTIVE = "Active";
	public static readonly String ORGDIVISIONSID = "OrgDivisionsID";
	public static readonly String GLACCOUNT = "GLAccount";

	public IdType OrgDepartmentsID
	{
	    get
	    {
		return this.orgDepartmentsID;
	    }
	    set
	    {
		this.orgDepartmentsID = value;
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

	public BooleanType Active
	{
	    get
	    {
		return this.active;
	    }
	    set
	    {
		this.active = value;
	    }
	}

	public IntegerType OrgDivisionsID
	{
	    get
	    {
		return this.orgDivisionsID;
	    }
	    set
	    {
		this.orgDivisionsID = value;
	    }
	}

	public StringType GLAccount
	{
	    get
	    {
		return this.gLAccount;
	    }
	    set
	    {
		this.gLAccount = value;
	    }
	}
    }
}
