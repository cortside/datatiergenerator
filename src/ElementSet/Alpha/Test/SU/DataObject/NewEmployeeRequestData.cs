using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class NewEmployeeRequestData : Spring2.Core.DataObject.DataObject
    {

	private IdType id = IdType.DEFAULT;
	private DateType hireDate = DateType.DEFAULT;
	private StringType text = StringType.DEFAULT;
	private IntegerType employeeID = IntegerType.DEFAULT;

	public static readonly String ID = "Id";
	public static readonly String HIREDATE = "HireDate";
	public static readonly String TEXT = "Text";
	public static readonly String EMPLOYEEID = "EmployeeID";

	public IdType Id
	{
	    get
	    {
		return this.id;
	    }
	    set
	    {
		this.id = value;
	    }
	}

	public DateType HireDate
	{
	    get
	    {
		return this.hireDate;
	    }
	    set
	    {
		this.hireDate = value;
	    }
	}

	public StringType Text
	{
	    get
	    {
		return this.text;
	    }
	    set
	    {
		this.text = value;
	    }
	}

	public IntegerType EmployeeID
	{
	    get
	    {
		return this.employeeID;
	    }
	    set
	    {
		this.employeeID = value;
	    }
	}
    }
}
