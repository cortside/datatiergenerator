using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class OtherGroupsKnowledgebasesData : Spring2.Core.DataObject.DataObject
    {

	private IntegerType knowledgebasesID = IntegerType.DEFAULT;
	private IntegerType subscriptionID = IntegerType.DEFAULT;
	private StringType description = StringType.DEFAULT;
	private BooleanType isPublic = BooleanType.DEFAULT;
	private StringType groupDescription = StringType.DEFAULT;
	private IntegerType articlesCount = IntegerType.DEFAULT;
	private IntegerType orgGroupID = IntegerType.DEFAULT;

	public static readonly String KNOWLEDGEBASESID = "KnowledgebasesID";
	public static readonly String SUBSCRIPTIONID = "SubscriptionID";
	public static readonly String DESCRIPTION = "Description";
	public static readonly String ISPUBLIC = "IsPublic";
	public static readonly String GROUPDESCRIPTION = "GroupDescription";
	public static readonly String ARTICLESCOUNT = "ArticlesCount";
	public static readonly String ORGGROUPID = "OrgGroupID";

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

	public IntegerType SubscriptionID
	{
	    get
	    {
		return this.subscriptionID;
	    }
	    set
	    {
		this.subscriptionID = value;
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
    }
}
