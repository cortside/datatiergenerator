using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class OrgEmployeesData : Spring2.Core.DataObject.DataObject
    {

	private IdType orgEmployeesID = IdType.DEFAULT;
	private IntegerType orgDepartmentsID = IntegerType.DEFAULT;
	private IntegerType orgLocationsID = IntegerType.DEFAULT;
	private IntegerType orgWorkspacesID = IntegerType.DEFAULT;
	private StringType firstName = StringType.DEFAULT;
	private StringType lastName = StringType.DEFAULT;
	private StringType nTUserAccount = StringType.DEFAULT;
	private BooleanType isActive = BooleanType.DEFAULT;
	private StringType email = StringType.DEFAULT;
	private StringType employeeTitle = StringType.DEFAULT;
	private DateType dateHired = DateType.DEFAULT;
	private DateType dateTerminated = DateType.DEFAULT;
	private IntegerType manager = IntegerType.DEFAULT;
	private StringType employeeNumber = StringType.DEFAULT;
	private StringType style = StringType.DEFAULT;
	private StringType map = StringType.DEFAULT;
	private DecimalType mapX = DecimalType.DEFAULT;
	private DecimalType mapY = DecimalType.DEFAULT;

	public static readonly String ORGEMPLOYEESID = "OrgEmployeesID";
	public static readonly String ORGDEPARTMENTSID = "OrgDepartmentsID";
	public static readonly String ORGLOCATIONSID = "OrgLocationsID";
	public static readonly String ORGWORKSPACESID = "OrgWorkspacesID";
	public static readonly String FIRSTNAME = "FirstName";
	public static readonly String LASTNAME = "LastName";
	public static readonly String NTUSERACCOUNT = "NTUserAccount";
	public static readonly String ISACTIVE = "IsActive";
	public static readonly String EMAIL = "Email";
	public static readonly String EMPLOYEETITLE = "EmployeeTitle";
	public static readonly String DATEHIRED = "DateHired";
	public static readonly String DATETERMINATED = "DateTerminated";
	public static readonly String MANAGER = "Manager";
	public static readonly String EMPLOYEENUMBER = "EmployeeNumber";
	public static readonly String STYLE = "Style";
	public static readonly String MAP = "Map";
	public static readonly String MAPX = "MapX";
	public static readonly String MAPY = "MapY";

	public IdType OrgEmployeesID
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

	public IntegerType OrgDepartmentsID
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

	public IntegerType OrgWorkspacesID
	{
	    get
	    {
		return this.orgWorkspacesID;
	    }
	    set
	    {
		this.orgWorkspacesID = value;
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

	public StringType Map
	{
	    get
	    {
		return this.map;
	    }
	    set
	    {
		this.map = value;
	    }
	}

	public DecimalType MapX
	{
	    get
	    {
		return this.mapX;
	    }
	    set
	    {
		this.mapX = value;
	    }
	}

	public DecimalType MapY
	{
	    get
	    {
		return this.mapY;
	    }
	    set
	    {
		this.mapY = value;
	    }
	}
    }
}
