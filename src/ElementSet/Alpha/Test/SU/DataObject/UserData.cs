using System;

using Spring2.Core.Types;

using StampinUp.Types;

namespace StampinUp.DataObject
{
    public class UserData : Spring2.Core.DataObject.DataObject
    {

	private IdType userId = IdType.DEFAULT;
	private StringType userLogin = StringType.DEFAULT;
	private LocaleEnum userLocale = LocaleEnum.DEFAULT;

	public static readonly String USERID = "UserId";
	public static readonly String USERLOGIN = "UserLogin";
	public static readonly String USERLOCALE = "UserLocale";

	/// <summary>
	/// Unique id for the entity
	/// </summary>
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

	/// <summary>
	/// Login name for the user.
	/// </summary>
	public StringType UserLogin
	{
	    get
	    {
		return this.userLogin;
	    }
	    set
	    {
		this.userLogin = value;
	    }
	}

	/// <summary>
	/// Id of locale the user is in.
	/// </summary>
	public LocaleEnum UserLocale
	{
	    get
	    {
		return this.userLocale;
	    }
	    set
	    {
		this.userLocale = value;
	    }
	}
    }
}
