using System;

using Spring2.Core.Types;

namespace Spring2.DataTierGenerator.DataObject
{
    public class AddressData : Spring2.Core.DataObject.DataObject
    {

	private StringType address1 = StringType.DEFAULT;
	private StringType address2 = StringType.DEFAULT;
	private StringType city = StringType.DEFAULT;
	private StringType state = StringType.DEFAULT;
	private StringType postalCode = StringType.DEFAULT;
	private StringType country = StringType.DEFAULT;

	public static readonly String ADDRESS1 = "Address1";
	public static readonly String ADDRESS2 = "Address2";
	public static readonly String CITY = "City";
	public static readonly String STATE = "State";
	public static readonly String POSTALCODE = "PostalCode";
	public static readonly String COUNTRY = "Country";

	public StringType Address1
	{
	    get
	    {
		return this.address1;
	    }
	    set
	    {
		this.address1 = value;
	    }
	}

	public StringType Address2
	{
	    get
	    {
		return this.address2;
	    }
	    set
	    {
		this.address2 = value;
	    }
	}

	public StringType City
	{
	    get
	    {
		return this.city;
	    }
	    set
	    {
		this.city = value;
	    }
	}

	public StringType State
	{
	    get
	    {
		return this.state;
	    }
	    set
	    {
		this.state = value;
	    }
	}

	public StringType PostalCode
	{
	    get
	    {
		return this.postalCode;
	    }
	    set
	    {
		this.postalCode = value;
	    }
	}

	public StringType Country
	{
	    get
	    {
		return this.country;
	    }
	    set
	    {
		this.country = value;
	    }
	}
    }
}
