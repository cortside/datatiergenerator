using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class GroupData : Spring2.Core.DataObject.DataObject
    {

	private IdType groupId = IdType.DEFAULT;
	private StringType groupName = StringType.DEFAULT;

	public static readonly String GROUPID = "GroupId";
	public static readonly String GROUPNAME = "GroupName";

	/// <summary>
	/// Unique id for the group.
	/// </summary>
	public IdType GroupId
	{
	    get
	    {
		return this.groupId;
	    }
	    set
	    {
		this.groupId = value;
	    }
	}

	/// <summary>
	/// Name of the group.
	/// </summary>
	public StringType GroupName
	{
	    get
	    {
		return this.groupName;
	    }
	    set
	    {
		this.groupName = value;
	    }
	}
    }
}
