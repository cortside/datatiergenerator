using System;
using System.Collections;

namespace Spring2.DataTierGenerator.Types
{

    public class OrderType : Spring2.Core.Types.EnumDataType
    {

	private static readonly ArrayList OPTIONS = new ArrayList();

	public static readonly new OrderType DEFAULT = new OrderType();
	public static readonly new OrderType UNSET = new OrderType();

	public static readonly OrderType PERSONAL = new OrderType("P", "Personal");
	public static readonly OrderType FIRM = new OrderType("F", "Firm");

	public static OrderType GetInstance(Object value)
	{
	    if (value is String)
	    {
		foreach (OrderType t in OPTIONS)
		{
		    if (t.Value.Equals(value))
		    {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private OrderType() {}

	private OrderType(String code, String name)
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
