using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class AllEmployeeWithGroupInfoViewData : Spring2.Core.DataObject.DataObject
    {

	private IntegerType orgEmployeesID = IntegerType.DEFAULT;
	private StringType firstName = StringType.DEFAULT;
	private StringType lastName = StringType.DEFAULT;
	private StringType employeeTitle = StringType.DEFAULT;
	private StringType departmentName = StringType.DEFAULT;
	private StringType locationName = StringType.DEFAULT;
	private StringType phoneNumber = StringType.DEFAULT;
	private StringType phoneDescription = StringType.DEFAULT;
	private StringType email = StringType.DEFAULT;
	private DateType dateHired = DateType.DEFAULT;
	private DateType dateTerminated = DateType.DEFAULT;
	private IntegerType manager = IntegerType.DEFAULT;
	private StringType employeeNumber = StringType.DEFAULT;
	private StringType style = StringType.DEFAULT;
	private BooleanType isActive = BooleanType.DEFAULT;
	private StringType nTUserAccount = StringType.DEFAULT;

	public static readonly String ORGEMPLOYEESID = "OrgEmployeesID";
	public static readonly String FIRSTNAME = "FirstName";
	public static readonly String LASTNAME = "LastName";
	public static readonly String EMPLOYEETITLE = "EmployeeTitle";
	public static readonly String DEPARTMENTNAME = "DepartmentName";
	public static readonly String LOCATIONNAME = "LocationName";
	public static readonly String PHONENUMBER = "PhoneNumber";
	public static readonly String PHONEDESCRIPTION = "PhoneDescription";
	public static readonly String EMAIL = "Email";
	public static readonly String DATEHIRED = "DateHired";
	public static readonly String DATETERMINATED = "DateTerminated";
	public static readonly String MANAGER = "Manager";
	public static readonly String EMPLOYEENUMBER = "EmployeeNumber";
	public static readonly String STYLE = "Style";
	public static readonly String ISACTIVE = "IsActive";
	public static readonly String NTUSERACCOUNT = "NTUserAccount";

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

	public StringType PhoneNumber
	{
	    get
	    {
		return this.phoneNumber;
	    }
	    set
	    {
		this.phoneNumber = value;
	    }
	}

	public StringType PhoneDescription
	{
	    get
	    {
		return this.phoneDescription;
	    }
	    set
	    {
		this.phoneDescription = value;
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

	public DateType DateHired
	{
	    get
	    {
		return this.dateHired;
	    }
	    set
	    {
		this.dateHired = value;
	    }
	}

	public DateType DateTerminated
	{
	    get
	    {
		return this.dateTerminated;
	    }
	    set
	    {
		this.dateTerminated = value;
	    }
	}

	public IntegerType Manager
	{
	    get
	    {
		return this.manager;
	    }
	    set
	    {
		this.manager = value;
	    }
	}

	public StringType EmployeeNumber
	{
	    get
	    {
		return this.employeeNumber;
	    }
	    set
	    {
		this.employeeNumber = value;
	    }
	}

	public StringType Style
	{
	    get
	    {
		return this.style;
	    }
	    set
	    {
		this.style = value;
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

	public StringType NTUserAccount
	{
	    get
	    {
		return this.nTUserAccount;
	    }
	    set
	    {
		this.nTUserAccount = value;
	    }
	}
    }
}
