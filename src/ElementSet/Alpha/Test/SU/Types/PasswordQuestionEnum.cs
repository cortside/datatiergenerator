using System;
using System.Collections;

namespace StampinUp.Types
{

    public class PasswordQuestionEnum : Spring2.Core.Types.EnumDataType
    {

	private static readonly ArrayList OPTIONS = new ArrayList();

	public static readonly new PasswordQuestionEnum DEFAULT = new PasswordQuestionEnum();
	public static readonly new PasswordQuestionEnum UNSET = new PasswordQuestionEnum();

	/// <summary>
	/// What is your favorite vacation spot?
	/// </summary>
	public static readonly PasswordQuestionEnum PASSWORDQUESTIONVACATION = new PasswordQuestionEnum("1", "PasswordQuestionVacation");
	/// <summary>
	/// What is/was the last name of your first grade teacher?
	/// </summary>
	public static readonly PasswordQuestionEnum PASSWORDQUESTIONTEACHER = new PasswordQuestionEnum("2", "PasswordQuestionTeacher");
	/// <summary>
	/// What is/was your grandfathers occupation?
	/// </summary>
	public static readonly PasswordQuestionEnum PASSWORDQUESTIONGRANDFATHER = new PasswordQuestionEnum("3", "PasswordQuestionGrandfather");
	/// <summary>
	/// What is the name of the place where you were born?
	/// </summary>
	public static readonly PasswordQuestionEnum PASSWORDQUESTIONBIRTHPLACE = new PasswordQuestionEnum("4", "PasswordQuestionBirthPlace");
	/// <summary>
	/// What was the model of your first car?
	/// </summary>
	public static readonly PasswordQuestionEnum PASSWORDQUESTIONCAR = new PasswordQuestionEnum("5", "PasswordQuestionCar");

	public static PasswordQuestionEnum GetInstance(Object value)
	{
	    if (value is String)
	    {
		foreach (PasswordQuestionEnum t in OPTIONS)
		{
		    if (t.Value.Equals(value))
		    {
			return t;
		    }
		}
	    }
	    if (value is Int32)
	    {
		foreach (PasswordQuestionEnum t in OPTIONS)
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

	private PasswordQuestionEnum() {}

	private PasswordQuestionEnum(String code, String name)
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
	/// Convert a PasswordQuestionEnum instance to an Int32;
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
