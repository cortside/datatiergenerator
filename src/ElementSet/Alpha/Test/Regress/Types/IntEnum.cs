using System;
using System.Collections;

namespace Spring2.DataTierGenerator.Types
{

    public class IntEnum : Spring2.Core.Types.EnumDataType
    {

	private static readonly ArrayList OPTIONS = new ArrayList();

	public static readonly new IntEnum DEFAULT = new IntEnum();
	public static readonly new IntEnum UNSET = new IntEnum();

	public static readonly IntEnum ONE = new IntEnum("1", "One");
	public static readonly IntEnum TWO = new IntEnum("2", "Two");

	public static IntEnum GetInstance(Object value)
	{
	    if (value is String)
	    {
		foreach (IntEnum t in OPTIONS)
		{
		    if (t.Value.Equals(value))
		    {
			return t;
		    }
		}
	    }
	    if (value is Int32)
	    {
		foreach (IntEnum t in OPTIONS)
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

	private IntEnum() {}

	private IntEnum(String code, String name)
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
	/// Convert a IntEnum instance to an Int32;
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
