using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class WebConfigData : Spring2.Core.DataObject.DataObject
    {

	private IdType webConfigID = IdType.DEFAULT;
	private DateType createDate = DateType.DEFAULT;
	private StringType leftNavBGColor = StringType.DEFAULT;
	private StringType headerTextColor = StringType.DEFAULT;
	private StringType mainPic = StringType.DEFAULT;
	private StringType leftNavPic = StringType.DEFAULT;
	private BooleanType active = BooleanType.DEFAULT;

	public static readonly String WEBCONFIGID = "WebConfigID";
	public static readonly String CREATEDATE = "CreateDate";
	public static readonly String LEFTNAVBGCOLOR = "LeftNavBGColor";
	public static readonly String HEADERTEXTCOLOR = "HeaderTextColor";
	public static readonly String MAINPIC = "MainPic";
	public static readonly String LEFTNAVPIC = "LeftNavPic";
	public static readonly String ACTIVE = "Active";

	public IdType WebConfigID
	{
	    get
	    {
		return this.webConfigID;
	    }
	    set
	    {
		this.webConfigID = value;
	    }
	}

	public DateType CreateDate
	{
	    get
	    {
		return this.createDate;
	    }
	    set
	    {
		this.createDate = value;
	    }
	}

	public StringType LeftNavBGColor
	{
	    get
	    {
		return this.leftNavBGColor;
	    }
	    set
	    {
		this.leftNavBGColor = value;
	    }
	}

	public StringType HeaderTextColor
	{
	    get
	    {
		return this.headerTextColor;
	    }
	    set
	    {
		this.headerTextColor = value;
	    }
	}

	public StringType MainPic
	{
	    get
	    {
		return this.mainPic;
	    }
	    set
	    {
		this.mainPic = value;
	    }
	}

	public StringType LeftNavPic
	{
	    get
	    {
		return this.leftNavPic;
	    }
	    set
	    {
		this.leftNavPic = value;
	    }
	}

	public BooleanType Active
	{
	    get
	    {
		return this.active;
	    }
	    set
	    {
		this.active = value;
	    }
	}
    }
}
