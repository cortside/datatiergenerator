using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class KnowledgebasesArticlesCountViewData : Spring2.Core.DataObject.DataObject
    {

	private IntegerType knowledgebasesID = IntegerType.DEFAULT;
	private StringType description = StringType.DEFAULT;
	private IntegerType articlesCount = IntegerType.DEFAULT;
	private IntegerType orgGroupsID = IntegerType.DEFAULT;
	private BooleanType isPublic = BooleanType.DEFAULT;

	public static readonly String KNOWLEDGEBASESID = "KnowledgebasesID";
	public static readonly String DESCRIPTION = "Description";
	public static readonly String ARTICLESCOUNT = "ArticlesCount";
	public static readonly String ORGGROUPSID = "OrgGroupsID";
	public static readonly String ISPUBLIC = "IsPublic";

	public IntegerType KnowledgebasesID
	{
	    get
	    {
		return this.knowledgebasesID;
	    }
	    set
	    {
		this.knowledgebasesID = value;
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

	public IntegerType ArticlesCount
	{
	    get
	    {
		return this.articlesCount;
	    }
	    set
	    {
		this.articlesCount = value;
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
    }
}
