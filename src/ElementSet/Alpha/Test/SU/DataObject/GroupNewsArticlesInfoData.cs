using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class GroupNewsArticlesInfoData : Spring2.Core.DataObject.DataObject
    {

	private IntegerType orgGroupsID = IntegerType.DEFAULT;
	private IntegerType articleCount = IntegerType.DEFAULT;
	private DateType lastModified = DateType.DEFAULT;

	public static readonly String ORGGROUPSID = "OrgGroupsID";
	public static readonly String ARTICLECOUNT = "ArticleCount";
	public static readonly String LASTMODIFIED = "LastModified";

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

	public IntegerType ArticleCount
	{
	    get
	    {
		return this.articleCount;
	    }
	    set
	    {
		this.articleCount = value;
	    }
	}

	public DateType LastModified
	{
	    get
	    {
		return this.lastModified;
	    }
	    set
	    {
		this.lastModified = value;
	    }
	}
    }
}
