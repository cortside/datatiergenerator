using System;
using System.Collections;

namespace StampinUp.Types
{

    public class RoleEnum : Spring2.Core.Types.EnumDataType
    {

	private static readonly ArrayList OPTIONS = new ArrayList();

	public static readonly new RoleEnum DEFAULT = new RoleEnum();
	public static readonly new RoleEnum UNSET = new RoleEnum();

	/// <summary>
	/// Grants ability to change roles for users.
	/// </summary>
	public static readonly RoleEnum CHANGEUSERRIGHTS = new RoleEnum("1", "ChangeUserRights");
	/// <summary>
	/// Grants ability to see and maintain demo credit card/eft accounts.
	/// </summary>
	public static readonly RoleEnum ACCOUNTMANAGEMENT = new RoleEnum("2", "AccountManagement");
	/// <summary>
	/// Grants ability to log on as a demonstrator on the external web site.
	/// </summary>
	public static readonly RoleEnum IMPERSONATEDEMONSTRATOR = new RoleEnum("3", "ImpersonateDemonstrator");
	/// <summary>
	/// Grants ability to place orders on the external web site.
	/// </summary>
	public static readonly RoleEnum ALLOWORDERING = new RoleEnum("4", "AllowOrdering");

	public static RoleEnum GetInstance(Object value)
	{
	    if (value is String)
	    {
		foreach (RoleEnum t in OPTIONS)
		{
		    if (t.Value.Equals(value))
		    {
			return t;
		    }
		}
	    }
	    if (value is Int32)
	    {
		foreach (RoleEnum t in OPTIONS)
		{
		    try
		    {
			if (Int32.Parse(t.Code).Equals(value))
			{
			    return t;
			}
		    }
		    catch (Exception)
		    {
			// parse exception - continue
		    }
		}
	    }

	    return UNSET;
	}

	private RoleEnum() {}

	private RoleEnum(String code, String name)
	{
	    this.code = code;
	    this.name = name;
	    OPTIONS.Add(this);
	}

	public override Boolean IsDefault
	{
	    get
	    {
		return Object.ReferenceEquals(this, DEFAULT);
	    }
	}

	public override Boolean IsUnset
	{
	    get
	    {
		return Object.ReferenceEquals(this, UNSET);
	    }
	}

	public static IList Options
	{
	    get
	    {
		return OPTIONS;
	    }
	}

	/// <summary>
	/// Convert a RoleEnum instance to an Int32;
	/// </summary>
	/// <returns>the Int32 representation for the enum instance.</returns>
	/// <exception cref="InvalidCastException">when converting DEFAULT or UNSET to an Int32.</exception>
	public Int32 ToInt32()
	{
	    if (IsValid)
	    {
		try
		{
		    return Int32.Parse(code);
		}
		catch (Exception)
		{
		    // parse error  - don't do anything - an acceptable exception will be thrown below
		}
	    }

	    // instance was !IsValid or there was a parser error
	    throw new InvalidCastException();
	}
    }
}
