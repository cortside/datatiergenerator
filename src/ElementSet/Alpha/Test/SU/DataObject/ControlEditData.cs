using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class ControlEditData : Spring2.Core.DataObject.DataObject
    {

	private BooleanType isBasic = BooleanType.DEFAULT;
	private StringType summary = StringType.DEFAULT;
	private StringType description = StringType.DEFAULT;
	private StringType src = StringType.DEFAULT;
	private StringType iconImage = StringType.DEFAULT;
	private BooleanType displayOnHomePage = BooleanType.DEFAULT;
	private IntegerType sortOrder = IntegerType.DEFAULT;
	private IntegerType controlsID = IntegerType.DEFAULT;
	private IntegerType controlsGroupsID = IntegerType.DEFAULT;
	private IntegerType orgGroupID = IntegerType.DEFAULT;
	private StringType managePage = StringType.DEFAULT;
	private BooleanType entireCompany = BooleanType.DEFAULT;
	private BooleanType mustShow = BooleanType.DEFAULT;

	public static readonly String ISBASIC = "IsBasic";
	public static readonly String SUMMARY = "Summary";
	public static readonly String DESCRIPTION = "Description";
	public static readonly String SRC = "Src";
	public static readonly String ICONIMAGE = "IconImage";
	public static readonly String DISPLAYONHOMEPAGE = "DisplayOnHomePage";
	public static readonly String SORTORDER = "SortOrder";
	public static readonly String CONTROLSID = "ControlsID";
	public static readonly String CONTROLSGROUPSID = "ControlsGroupsID";
	public static readonly String ORGGROUPID = "OrgGroupID";
	public static readonly String MANAGEPAGE = "ManagePage";
	public static readonly String ENTIRECOMPANY = "EntireCompany";
	public static readonly String MUSTSHOW = "MustShow";

	public BooleanType IsBasic
	{
	    get
	    {
		return this.isBasic;
	    }
	    set
	    {
		this.isBasic = value;
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

	public StringType Src
	{
	    get
	    {
		return this.src;
	    }
	    set
	    {
		this.src = value;
	    }
	}

	public StringType IconImage
	{
	    get
	    {
		return this.iconImage;
	    }
	    set
	    {
		this.iconImage = value;
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

	public IntegerType ControlsGroupsID
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

	public IntegerType OrgGroupID
	{
	    get
	    {
		return this.orgGroupID;
	    }
	    set
	    {
		this.orgGroupID = value;
	    }
	}

	public StringType ManagePage
	{
	    get
	    {
		return this.managePage;
	    }
	    set
	    {
		this.managePage = value;
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
