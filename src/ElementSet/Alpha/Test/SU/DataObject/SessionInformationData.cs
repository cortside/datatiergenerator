using System;

using Spring2.Core.Types;

using StampinUp.Core.Types;

namespace StampinUp.DataObject
{
    public class SessionInformationData : Spring2.Core.DataObject.DataObject
    {

	private IdType sessionId = IdType.DEFAULT;
	private DemoIdType demoId = DemoIdType.DEFAULT;
	private StringType userId = StringType.DEFAULT;
	private DateType dateCreated = DateType.DEFAULT;

	public static readonly String SESSIONID = "SessionId";
	public static readonly String DEMOID = "DemoId";
	public static readonly String USERID = "UserId";
	public static readonly String DATECREATED = "DateCreated";

	public IdType SessionId
	{
	    get
	    {
		return this.sessionId;
	    }
	    set
	    {
		this.sessionId = value;
	    }
	}

	public DemoIdType DemoId
	{
	    get
	    {
		return this.demoId;
	    }
	    set
	    {
		this.demoId = value;
	    }
	}

	public StringType UserId
	{
	    get
	    {
		return this.userId;
	    }
	    set
	    {
		this.userId = value;
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
    }
}
