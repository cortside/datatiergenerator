using System;
using System.Collections;

namespace Spring2.DataTierGenerator.Types
{

    public class FormatType : Spring2.Core.Types.EnumDataType
    {

	private static readonly ArrayList OPTIONS = new ArrayList();

	public static readonly new FormatType DEFAULT = new FormatType();
	public static readonly new FormatType UNSET = new FormatType();

	/// <summary>
	/// This is the HTML type
	/// </summary>
	public static readonly FormatType HTML = new FormatType("H", "HTML");
	public static readonly FormatType TEXT = new FormatType("T", "Text");
	public static readonly FormatType JAVASCRIPT = new FormatType("J", "JavaScript");
	public static readonly FormatType RTF = new FormatType("R", "RTF");
	public static readonly FormatType NONE_NADA = new FormatType("N", "None Nada");

	public static FormatType GetInstance(Object value)
	{
	    if (value is String)
	    {
		foreach (FormatType t in OPTIONS)
		{
		    if (t.Value.Equals(value))
		    {
			return t;
		    }
		}
	    }

	    return UNSET;
	}

	private FormatType() {}

	private FormatType(String code, String name)
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
