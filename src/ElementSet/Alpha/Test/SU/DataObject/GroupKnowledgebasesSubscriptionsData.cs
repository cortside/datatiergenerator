using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class GroupKnowledgebasesSubscriptionsData : Spring2.Core.DataObject.DataObject
    {

	private IdType id = IdType.DEFAULT;
	private IntegerType orgGroupID = IntegerType.DEFAULT;
	private IntegerType knowledgebasesID = IntegerType.DEFAULT;

	public static readonly String ID = "Id";
	public static readonly String ORGGROUPID = "OrgGroupID";
	public static readonly String KNOWLEDGEBASESID = "KnowledgebasesID";

	public IdType Id
	{
	    get
	    {
		return this.id;
	    }
	    set
	    {
		this.id = value;
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
    }
}
