using System;

using Spring2.Core.Types;

using StampinUp.Types;

namespace StampinUp.DataObject
{
    public class GroupRoleData : Spring2.Core.DataObject.DataObject
    {

	private IdType groupRoleId = IdType.DEFAULT;
	private IdType groupId = IdType.DEFAULT;
	private RoleEnum roleId = RoleEnum.DEFAULT;
	private PermitDenyEnum permitDeny = PermitDenyEnum.DEFAULT;

	public static readonly String GROUPROLEID = "GroupRoleId";
	public static readonly String GROUPID = "GroupId";
	public static readonly String ROLEID = "RoleId";
	public static readonly String PERMITDENY = "PermitDeny";

	/// <summary>
	/// Unique id for the group/role association.
	/// </summary>
	public IdType GroupRoleId
	{
	    get
	    {
		return this.groupRoleId;
	    }
	    set
	    {
		this.groupRoleId = value;
	    }
	}

	/// <summary>
	/// Id of the group.
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
	/// Id of the role
	/// </summary>
	public RoleEnum RoleId
	{
	    get
	    {
		return this.roleId;
	    }
	    set
	    {
		this.roleId = value;
	    }
	}

	/// <summary>
	/// Indicate if role is given or removed.
	/// </summary>
	public PermitDenyEnum PermitDeny
	{
	    get
	    {
		return this.permitDeny;
	    }
	    set
	    {
		this.permitDeny = value;
	    }
	}
    }
}
