using System;
using System.Collections;

namespace StampinUp.Types
{

    public class PermitDenyEnum : Spring2.Core.Types.EnumDataType
    {

	private static readonly ArrayList OPTIONS = new ArrayList();

	public static readonly new PermitDenyEnum DEFAULT = new PermitDenyEnum();
	public static readonly new PermitDenyEnum UNSET = new PermitDenyEnum();

	public static readonly PermitDenyEnum PERMIT = new PermitDenyEnum("1", "Permit");
	public static readonly PermitDenyEnum DENY = new PermitDenyEnum("0", "Deny");

	public static PermitDenyEnum GetInstance(Object value)
	{
	    if (value is String)
	    {
		foreach (PermitDenyEnum t in OPTIONS)
		{
		    if (t.Value.Equals(value))
		    {
			return t;
		    }
		}
	    }
	    if (value is Int32)
	    {
		foreach (PermitDenyEnum t in OPTIONS)
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

	private PermitDenyEnum() {}

	private PermitDenyEnum(String code, String name)
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
	/// Convert a PermitDenyEnum instance to an Int32;
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
