using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class GroupEmployeeViewData : Spring2.Core.DataObject.DataObject
    {

	private StringType firstName = StringType.DEFAULT;
	private StringType lastName = StringType.DEFAULT;
	private StringType departmentName = StringType.DEFAULT;
	private BooleanType groupIsActive = BooleanType.DEFAULT;
	private IntegerType orgGroupsID = IntegerType.DEFAULT;
	private StringType locationName = StringType.DEFAULT;
	private StringType employeeTitle = StringType.DEFAULT;
	private BooleanType employeeIsActive = BooleanType.DEFAULT;
	private IntegerType orgGroupsEmployeesID = IntegerType.DEFAULT;
	private IntegerType orgEmployeesID = IntegerType.DEFAULT;
	private StringType email = StringType.DEFAULT;

	public static readonly String FIRSTNAME = "FirstName";
	public static readonly String LASTNAME = "LastName";
	public static readonly String DEPARTMENTNAME = "DepartmentName";
	public static readonly String GROUPISACTIVE = "GroupIsActive";
	public static readonly String ORGGROUPSID = "OrgGroupsID";
	public static readonly String LOCATIONNAME = "LocationName";
	public static readonly String EMPLOYEETITLE = "EmployeeTitle";
	public static readonly String EMPLOYEEISACTIVE = "EmployeeIsActive";
	public static readonly String ORGGROUPSEMPLOYEESID = "OrgGroupsEmployeesID";
	public static readonly String ORGEMPLOYEESID = "OrgEmployeesID";
	public static readonly String EMAIL = "Email";

	public StringType FirstName
	{
	    get
	    {
		return this.firstName;
	    }
	    set
	    {
		this.firstName = value;
	    }
	}

	public StringType LastName
	{
	    get
	    {
		return this.lastName;
	    }
	    set
	    {
		this.lastName = value;
	    }
	}

	public StringType DepartmentName
	{
	    get
	    {
		return this.departmentName;
	    }
	    set
	    {
		this.departmentName = value;
	    }
	}

	public BooleanType GroupIsActive
	{
	    get
	    {
		return this.groupIsActive;
	    }
	    set
	    {
		this.groupIsActive = value;
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

	public StringType LocationName
	{
	    get
	    {
		return this.locationName;
	    }
	    set
	    {
		this.locationName = value;
	    }
	}

	public StringType EmployeeTitle
	{
	    get
	    {
		return this.employeeTitle;
	    }
	    set
	    {
		this.employeeTitle = value;
	    }
	}

	public BooleanType EmployeeIsActive
	{
	    get
	    {
		return this.employeeIsActive;
	    }
	    set
	    {
		this.employeeIsActive = value;
	    }
	}

	public IntegerType OrgGroupsEmployeesID
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
    }
}
