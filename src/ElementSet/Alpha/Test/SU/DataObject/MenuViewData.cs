using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class MenuViewData : Spring2.Core.DataObject.DataObject
    {

	private IntegerType parentMenuItemsID = IntegerType.DEFAULT;
	private StringType description = StringType.DEFAULT;
	private StringType url = StringType.DEFAULT;
	private IntegerType sortOrder = IntegerType.DEFAULT;
	private StringType target = StringType.DEFAULT;
	private DateType dateStart = DateType.DEFAULT;
	private DateType dateEnd = DateType.DEFAULT;
	private StringType nodeImgSrc = StringType.DEFAULT;
	private IntegerType menuItemsID = IntegerType.DEFAULT;

	public static readonly String PARENTMENUITEMSID = "ParentMenuItemsID";
	public static readonly String DESCRIPTION = "Description";
	public static readonly String URL = "url";
	public static readonly String SORTORDER = "SortOrder";
	public static readonly String TARGET = "Target";
	public static readonly String DATESTART = "DateStart";
	public static readonly String DATEEND = "DateEnd";
	public static readonly String NODEIMGSRC = "NodeImgSrc";
	public static readonly String MENUITEMSID = "MenuItemsID";

	public IntegerType ParentMenuItemsID
	{
	    get
	    {
		return this.parentMenuItemsID;
	    }
	    set
	    {
		this.parentMenuItemsID = value;
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

	public StringType Url
	{
	    get
	    {
		return this.url;
	    }
	    set
	    {
		this.url = value;
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

	public StringType Target
	{
	    get
	    {
		return this.target;
	    }
	    set
	    {
		this.target = value;
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

	public StringType NodeImgSrc
	{
	    get
	    {
		return this.nodeImgSrc;
	    }
	    set
	    {
		this.nodeImgSrc = value;
	    }
	}

	public IntegerType MenuItemsID
	{
	    get
	    {
		return this.menuItemsID;
	    }
	    set
	    {
		this.menuItemsID = value;
	    }
	}
    }
}
