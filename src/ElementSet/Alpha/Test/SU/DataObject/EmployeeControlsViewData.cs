using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class EmployeeControlsViewData : Spring2.Core.DataObject.DataObject
    {

	private StringType groupDescription = StringType.DEFAULT;
	private StringType controlDescription = StringType.DEFAULT;
	private IntegerType orgGroupsID = IntegerType.DEFAULT;
	private IntegerType controlsID = IntegerType.DEFAULT;

	public static readonly String GROUPDESCRIPTION = "GroupDescription";
	public static readonly String CONTROLDESCRIPTION = "ControlDescription";
	public static readonly String ORGGROUPSID = "OrgGroupsID";
	public static readonly String CONTROLSID = "ControlsID";

	public StringType GroupDescription
	{
	    get
	    {
		return this.groupDescription;
	    }
	    set
	    {
		this.groupDescription = value;
	    }
	}

	public StringType ControlDescription
	{
	    get
	    {
		return this.controlDescription;
	    }
	    set
	    {
		this.controlDescription = value;
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
    }
}
