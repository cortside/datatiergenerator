using System;

using Spring2.Core.Types;

using StampinUp.Types;

namespace StampinUp.DataObject
{
    public class UserRoleData : Spring2.Core.DataObject.DataObject
    {

	private IdType userRoleId = IdType.DEFAULT;
	private IdType userId = IdType.DEFAULT;
	private RoleEnum roleId = RoleEnum.DEFAULT;
	private PermitDenyEnum permitDeny = PermitDenyEnum.DEFAULT;

	public static readonly String USERROLEID = "UserRoleId";
	public static readonly String USERID = "UserId";
	public static readonly String ROLEID = "RoleId";
	public static readonly String PERMITDENY = "PermitDeny";

	public IdType UserRoleId
	{
	    get
	    {
		return this.userRoleId;
	    }
	    set
	    {
		this.userRoleId = value;
	    }
	}

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
