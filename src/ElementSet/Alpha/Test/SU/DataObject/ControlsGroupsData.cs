using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class ControlsGroupsData : Spring2.Core.DataObject.DataObject
    {

	private IdType controlsGroupsID = IdType.DEFAULT;
	private IntegerType controlsID = IntegerType.DEFAULT;
	private IntegerType orgGroupsID = IntegerType.DEFAULT;
	private IntegerType sortOrder = IntegerType.DEFAULT;
	private BooleanType displayOnHomePage = BooleanType.DEFAULT;

	public static readonly String CONTROLSGROUPSID = "ControlsGroupsID";
	public static readonly String CONTROLSID = "ControlsID";
	public static readonly String ORGGROUPSID = "OrgGroupsID";
	public static readonly String SORTORDER = "SortOrder";
	public static readonly String DISPLAYONHOMEPAGE = "DisplayOnHomePage";

	public IdType ControlsGroupsID
	{
	    get
	    {
		return this.controlsGroupsID;
	    }
	    set
	    {
		this.controlsGroupsID = value;
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

	public IntegerType SortOrder
	{
	    get
	    {
		return this.sortOrder;
	    }
	    set
	    {
		this.sortOrder = value;
	    }
	}

	public BooleanType DisplayOnHomePage
	{
	    get
	    {
		return this.displayOnHomePage;
	    }
	    set
	    {
		this.displayOnHomePage = value;
	    }
	}
    }
}
