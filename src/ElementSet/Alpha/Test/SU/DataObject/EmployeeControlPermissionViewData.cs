using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class EmployeeControlPermissionViewData : Spring2.Core.DataObject.DataObject
    {

	private IntegerType orgEmployeeID = IntegerType.DEFAULT;
	private IntegerType controlsID = IntegerType.DEFAULT;
	private StringType description = StringType.DEFAULT;
	private DateType dateStart = DateType.DEFAULT;
	private DateType dateEnd = DateType.DEFAULT;
	private BooleanType entireCompany = BooleanType.DEFAULT;
	private StringType summary = StringType.DEFAULT;
	private BooleanType mustShow = BooleanType.DEFAULT;

	public static readonly String ORGEMPLOYEEID = "OrgEmployeeID";
	public static readonly String CONTROLSID = "ControlsID";
	public static readonly String DESCRIPTION = "Description";
	public static readonly String DATESTART = "DateStart";
	public static readonly String DATEEND = "DateEnd";
	public static readonly String ENTIRECOMPANY = "EntireCompany";
	public static readonly String SUMMARY = "Summary";
	public static readonly String MUSTSHOW = "MustShow";

	public IntegerType OrgEmployeeID
	{
	    get
	    {
		return this.orgEmployeeID;
	    }
	    set
	    {
		this.orgEmployeeID = value;
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

	public DateType DateStart
	{
	    get
	    {
		return this.dateStart;
	    }
	    set
	    {
		this.dateStart = value;
	    }
	}

	public DateType DateEnd
	{
	    get
	    {
		return this.dateEnd;
	    }
	    set
	    {
		this.dateEnd = value;
	    }
	}

	public BooleanType EntireCompany
	{
	    get
	    {
		return this.entireCompany;
	    }
	    set
	    {
		this.entireCompany = value;
	    }
	}

	public StringType Summary
	{
	    get
	    {
		return this.summary;
	    }
	    set
	    {
		this.summary = value;
	    }
	}

	public BooleanType MustShow
	{
	    get
	    {
		return this.mustShow;
	    }
	    set
	    {
		this.mustShow = value;
	    }
	}
    }
}
