using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class OrgPhoneNumbersData : Spring2.Core.DataObject.DataObject
    {

	private IdType orgPhoneNumbersID = IdType.DEFAULT;
	private IntegerType orgDepartmentsID = IntegerType.DEFAULT;
	private IntegerType orgLocationsID = IntegerType.DEFAULT;
	private IntegerType orgEmployeesID = IntegerType.DEFAULT;
	private StringType description = StringType.DEFAULT;
	private StringType phoneNumber = StringType.DEFAULT;

	public static readonly String ORGPHONENUMBERSID = "OrgPhoneNumbersID";
	public static readonly String ORGDEPARTMENTSID = "OrgDepartmentsID";
	public static readonly String ORGLOCATIONSID = "OrgLocationsID";
	public static readonly String ORGEMPLOYEESID = "OrgEmployeesID";
	public static readonly String DESCRIPTION = "Description";
	public static readonly String PHONENUMBER = "PhoneNumber";

	public IdType OrgPhoneNumbersID
	{
	    get
	    {
		return this.orgPhoneNumbersID;
	    }
	    set
	    {
		this.orgPhoneNumbersID = value;
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
    }
}
