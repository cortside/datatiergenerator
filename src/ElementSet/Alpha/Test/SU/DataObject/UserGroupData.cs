using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class UserGroupData : Spring2.Core.DataObject.DataObject
    {

	private IdType userGroupId = IdType.DEFAULT;
	private IdType groupId = IdType.DEFAULT;
	private IdType userId = IdType.DEFAULT;

	public static readonly String USERGROUPID = "UserGroupId";
	public static readonly String GROUPID = "GroupId";
	public static readonly String USERID = "UserId";

	/// <summary>
	/// Unique id for the user/group combination
	/// </summary>
	public IdType UserGroupId
	{
	    get
	    {
		return this.userGroupId;
	    }
	    set
	    {
		this.userGroupId = value;
	    }
	}

	/// <summary>
	/// Group user belongs to.
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
	/// User belonging to the group.
	/// </summary>
	public IdType UserId
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
    }
}
