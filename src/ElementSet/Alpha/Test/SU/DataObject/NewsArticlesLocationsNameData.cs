using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class NewsArticlesLocationsNameData : Spring2.Core.DataObject.DataObject
    {

	private StringType description = StringType.DEFAULT;
	private IntegerType newsArticlesLocationsID = IntegerType.DEFAULT;
	private IntegerType newsArticlesID = IntegerType.DEFAULT;
	private IntegerType orgLocationsID = IntegerType.DEFAULT;

	public static readonly String DESCRIPTION = "Description";
	public static readonly String NEWSARTICLESLOCATIONSID = "NewsArticlesLocationsID";
	public static readonly String NEWSARTICLESID = "NewsArticlesID";
	public static readonly String ORGLOCATIONSID = "OrgLocationsID";

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

	public IntegerType NewsArticlesLocationsID
	{
	    get
	    {
		return this.newsArticlesLocationsID;
	    }
	    set
	    {
		this.newsArticlesLocationsID = value;
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

	public IntegerType OrgLocationsID
	{
	    get
	    {
		return this.orgLocationsID;
	    }
	    set
	    {
		this.orgLocationsID = value;
	    }
	}
    }
}
