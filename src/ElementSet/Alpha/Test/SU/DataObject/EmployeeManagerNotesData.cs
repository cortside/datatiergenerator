using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class EmployeeManagerNotesData : Spring2.Core.DataObject.DataObject
    {

	private IdType id = IdType.DEFAULT;
	private StringType changeEmployeeName = StringType.DEFAULT;
	private BooleanType isAutomated = BooleanType.DEFAULT;
	private DateType noteDate = DateType.DEFAULT;
	private StringType notes = StringType.DEFAULT;
	private IntegerType type = IntegerType.DEFAULT;
	private IntegerType employeeID = IntegerType.DEFAULT;

	public static readonly String ID = "Id";
	public static readonly String CHANGEEMPLOYEENAME = "ChangeEmployeeName";
	public static readonly String ISAUTOMATED = "IsAutomated";
	public static readonly String NOTEDATE = "NoteDate";
	public static readonly String NOTES = "Notes";
	public static readonly String TYPE = "Type";
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

	public StringType ChangeEmployeeName
	{
	    get
	    {
		return this.changeEmployeeName;
	    }
	    set
	    {
		this.changeEmployeeName = value;
	    }
	}

	public BooleanType IsAutomated
	{
	    get
	    {
		return this.isAutomated;
	    }
	    set
	    {
		this.isAutomated = value;
	    }
	}

	public DateType NoteDate
	{
	    get
	    {
		return this.noteDate;
	    }
	    set
	    {
		this.noteDate = value;
	    }
	}

	public StringType Notes
	{
	    get
	    {
		return this.notes;
	    }
	    set
	    {
		this.notes = value;
	    }
	}

	public IntegerType Type
	{
	    get
	    {
		return this.type;
	    }
	    set
	    {
		this.type = value;
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
