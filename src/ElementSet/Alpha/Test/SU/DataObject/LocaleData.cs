using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class LocaleData : Spring2.Core.DataObject.DataObject
    {

	private IdType localeCode = IdType.DEFAULT;
	private StringType localeDescription = StringType.DEFAULT;

	public static readonly String LOCALECODE = "LocaleCode";
	public static readonly String LOCALEDESCRIPTION = "LocaleDescription";

	/// <summary>
	/// Integer code identifying the locale
	/// </summary>
	public IdType LocaleCode
	{
	    get
	    {
		return this.localeCode;
	    }
	    set
	    {
		this.localeCode = value;
	    }
	}

	/// <summary>
	/// String identifying the locale.
	/// </summary>
	public StringType LocaleDescription
	{
	    get
	    {
		return this.localeDescription;
	    }
	    set
	    {
		this.localeDescription = value;
	    }
	}
    }
}
