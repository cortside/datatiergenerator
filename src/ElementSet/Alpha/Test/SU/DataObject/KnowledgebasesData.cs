using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class KnowledgebasesData : Spring2.Core.DataObject.DataObject
    {

	private IdType knowledgebasesID = IdType.DEFAULT;
	private StringType description = StringType.DEFAULT;
	private DateType dateStart = DateType.DEFAULT;
	private DateType dateEnd = DateType.DEFAULT;
	private BooleanType isPublic = BooleanType.DEFAULT;
	private IntegerType orgGroupsID = IntegerType.DEFAULT;

	public static readonly String KNOWLEDGEBASESID = "KnowledgebasesID";
	public static readonly String DESCRIPTION = "Description";
	public static readonly String DATESTART = "DateStart";
	public static readonly String DATEEND = "DateEnd";
	public static readonly String ISPUBLIC = "IsPublic";
	public static readonly String ORGGROUPSID = "OrgGroupsID";

	public IdType KnowledgebasesID
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
    }
}
