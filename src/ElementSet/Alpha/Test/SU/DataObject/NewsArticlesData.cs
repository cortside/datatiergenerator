using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class NewsArticlesData : Spring2.Core.DataObject.DataObject
    {

	private IdType newsArticlesID = IdType.DEFAULT;
	private IntegerType orgGroupsID = IntegerType.DEFAULT;
	private StringType title = StringType.DEFAULT;
	private StringType summary = StringType.DEFAULT;
	private StringType text = StringType.DEFAULT;
	private DateType dateCreated = DateType.DEFAULT;
	private DateType dateModified = DateType.DEFAULT;
	private DateType dateStart = DateType.DEFAULT;
	private DateType dateEnd = DateType.DEFAULT;
	private IntegerType orgEmployeesID = IntegerType.DEFAULT;
	private BooleanType isArchived = BooleanType.DEFAULT;
	private BooleanType allowDiscussion = BooleanType.DEFAULT;
	private BooleanType isNew = BooleanType.DEFAULT;
	private BooleanType isPublic = BooleanType.DEFAULT;
	private IntegerType lastModifiedOrgEmployeesID = IntegerType.DEFAULT;
	private StringType newsImage = StringType.DEFAULT;
	private IntegerType templateType = IntegerType.DEFAULT;

	public static readonly String NEWSARTICLESID = "NewsArticlesID";
	public static readonly String ORGGROUPSID = "OrgGroupsID";
	public static readonly String TITLE = "Title";
	public static readonly String SUMMARY = "Summary";
	public static readonly String TEXT = "Text";
	public static readonly String DATECREATED = "DateCreated";
	public static readonly String DATEMODIFIED = "DateModified";
	public static readonly String DATESTART = "DateStart";
	public static readonly String DATEEND = "DateEnd";
	public static readonly String ORGEMPLOYEESID = "OrgEmployeesID";
	public static readonly String ISARCHIVED = "IsArchived";
	public static readonly String ALLOWDISCUSSION = "AllowDiscussion";
	public static readonly String ISNEW = "IsNew";
	public static readonly String ISPUBLIC = "IsPublic";
	public static readonly String LASTMODIFIEDORGEMPLOYEESID = "LastModifiedOrgEmployeesID";
	public static readonly String NEWSIMAGE = "NewsImage";
	public static readonly String TEMPLATETYPE = "TemplateType";

	public IdType NewsArticlesID
	{
	    get
	    {
		return this.newsArticlesID;
	    }
	    set
	    {
		this.newsArticlesID = value;
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

	public StringType Title
	{
	    get
	    {
		return this.title;
	    }
	    set
	    {
		this.title = value;
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

	public DateType DateCreated
	{
	    get
	    {
		return this.dateCreated;
	    }
	    set
	    {
		this.dateCreated = value;
	    }
	}

	public DateType DateModified
	{
	    get
	    {
		return this.dateModified;
	    }
	    set
	    {
		this.dateModified = value;
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

	public BooleanType IsArchived
	{
	    get
	    {
		return this.isArchived;
	    }
	    set
	    {
		this.isArchived = value;
	    }
	}

	public BooleanType AllowDiscussion
	{
	    get
	    {
		return this.allowDiscussion;
	    }
	    set
	    {
		this.allowDiscussion = value;
	    }
	}

	public BooleanType IsNew
	{
	    get
	    {
		return this.isNew;
	    }
	    set
	    {
		this.isNew = value;
	    }
	}

	public BooleanType IsPublic
	{
	    get
	    {
		return this.isPublic;
	    }
	    set
	    {
		this.isPublic = value;
	    }
	}

	public IntegerType LastModifiedOrgEmployeesID
	{
	    get
	    {
		return this.lastModifiedOrgEmployeesID;
	    }
	    set
	    {
		this.lastModifiedOrgEmployeesID = value;
	    }
	}

	public StringType NewsImage
	{
	    get
	    {
		return this.newsImage;
	    }
	    set
	    {
		this.newsImage = value;
	    }
	}

	public IntegerType TemplateType
	{
	    get
	    {
		return this.templateType;
	    }
	    set
	    {
		this.templateType = value;
	    }
	}
    }
}
