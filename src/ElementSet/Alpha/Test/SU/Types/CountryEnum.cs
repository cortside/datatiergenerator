using System;
using System.Collections;

namespace StampinUp.Types
{

    public class CountryEnum : Spring2.Core.Types.EnumDataType
    {

	private static readonly ArrayList OPTIONS = new ArrayList();

	public static readonly new CountryEnum DEFAULT = new CountryEnum();
	public static readonly new CountryEnum UNSET = new CountryEnum();

	public static readonly CountryEnum US = new CountryEnum("US", "US");
	public static readonly CountryEnum CA = new CountryEnum("CA", "CA");

	public static CountryEnum GetInstance(Object value)
	{
	    if (value is String)
	    {
		foreach (CountryEnum t in OPTIONS)
		{
		    if (t.Value.Equals(value))
		    {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private CountryEnum() {}

	private CountryEnum(String code, String name)
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
    }
}
