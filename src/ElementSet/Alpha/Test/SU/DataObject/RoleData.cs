using System;

using Spring2.Core.Types;

using StampinUp.Types;

namespace StampinUp.DataObject
{
    public class RoleData : Spring2.Core.DataObject.DataObject
    {

	private RoleEnum roleId = RoleEnum.DEFAULT;
	private StringType roleName = StringType.DEFAULT;

	public static readonly String ROLEID = "RoleId";
	public static readonly String ROLENAME = "RoleName";

	/// <summary>
	/// Unique id for the role.
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
	/// Name of the role.
	/// </summary>
	public StringType RoleName
	{
	    get
	    {
		return this.roleName;
	    }
	    set
	    {
		this.roleName = value;
	    }
	}
    }
}
