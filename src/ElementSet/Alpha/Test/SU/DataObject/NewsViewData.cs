using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class NewsViewData : Spring2.Core.DataObject.DataObject
    {

	private StringType title = StringType.DEFAULT;
	private StringType summary = StringType.DEFAULT;
	private DateType dateStart = DateType.DEFAULT;
	private DateType dateEnd = DateType.DEFAULT;
	private BooleanType isPublic = BooleanType.DEFAULT;
	private DateType dateModified = DateType.DEFAULT;
	private StringType lastModifiedFirstName = StringType.DEFAULT;
	private StringType lastModifiedLastName = StringType.DEFAULT;
	private StringType createFirstName = StringType.DEFAULT;
	private StringType createLastName = StringType.DEFAULT;
	private IntegerType orgGroupsID = IntegerType.DEFAULT;
	private StringType newsImage = StringType.DEFAULT;
	private IntegerType templateType = IntegerType.DEFAULT;
	private IntegerType newsArticlesID = IntegerType.DEFAULT;
	private StringType text = StringType.DEFAULT;
	private IntegerType lastModifiedID = IntegerType.DEFAULT;

	public static readonly String TITLE = "Title";
	public static readonly String SUMMARY = "Summary";
	public static readonly String DATESTART = "DateStart";
	public static readonly String DATEEND = "DateEnd";
	public static readonly String ISPUBLIC = "IsPublic";
	public static readonly String DATEMODIFIED = "DateModified";
	public static readonly String LASTMODIFIEDFIRSTNAME = "LastModifiedFirstName";
	public static readonly String LASTMODIFIEDLASTNAME = "LastModifiedLastName";
	public static readonly String CREATEFIRSTNAME = "CreateFirstName";
	public static readonly String CREATELASTNAME = "CreateLastName";
	public static readonly String ORGGROUPSID = "OrgGroupsID";
	public static readonly String NEWSIMAGE = "NewsImage";
	public static readonly String TEMPLATETYPE = "TemplateType";
	public static readonly String NEWSARTICLESID = "NewsArticlesID";
	public static readonly String TEXT = "Text";
	public static readonly String LASTMODIFIEDID = "LastModifiedID";

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

	public StringType LastModifiedFirstName
	{
	    get
	    {
		return this.lastModifiedFirstName;
	    }
	    set
	    {
		this.lastModifiedFirstName = value;
	    }
	}

	public StringType LastModifiedLastName
	{
	    get
	    {
		return this.lastModifiedLastName;
	    }
	    set
	    {
		this.lastModifiedLastName = value;
	    }
	}

	public StringType CreateFirstName
	{
	    get
	    {
		return this.createFirstName;
	    }
	    set
	    {
		this.createFirstName = value;
	    }
	}

	public StringType CreateLastName
	{
	    get
	    {
		return this.createLastName;
	    }
	    set
	    {
		this.createLastName = value;
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

	public IntegerType NewsArticlesID
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

	public IntegerType LastModifiedID
	{
	    get
	    {
		return this.lastModifiedID;
	    }
	    set
	    {
		this.lastModifiedID = value;
	    }
	}
    }
}
