using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class OrgLocationsData : Spring2.Core.DataObject.DataObject
    {

	private IdType orgLocationsID = IdType.DEFAULT;
	private StringType description = StringType.DEFAULT;
	private StringType address1 = StringType.DEFAULT;
	private StringType address2 = StringType.DEFAULT;
	private StringType city = StringType.DEFAULT;
	private StringType state = StringType.DEFAULT;
	private StringType country = StringType.DEFAULT;
	private StringType postCode = StringType.DEFAULT;
	private StringType prefix = StringType.DEFAULT;
	private StringType image = StringType.DEFAULT;

	public static readonly String ORGLOCATIONSID = "OrgLocationsID";
	public static readonly String DESCRIPTION = "Description";
	public static readonly String ADDRESS1 = "Address1";
	public static readonly String ADDRESS2 = "Address2";
	public static readonly String CITY = "City";
	public static readonly String STATE = "State";
	public static readonly String COUNTRY = "Country";
	public static readonly String POSTCODE = "PostCode";
	public static readonly String PREFIX = "Prefix";
	public static readonly String IMAGE = "Image";

	public IdType OrgLocationsID
	{
	    get
	    {
		return this.orgLocationsID;
	    }
	    set
	    {
		this.orgLocationsID = value;
	    }
	}

	public StringType Description
	{
	    get
	    {
		return this.description;
	    }
	    set
	    {
		this.description = value;
	    }
	}

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

	public StringType PostCode
	{
	    get
	    {
		return this.postCode;
	    }
	    set
	    {
		this.postCode = value;
	    }
	}

	public StringType Prefix
	{
	    get
	    {
		return this.prefix;
	    }
	    set
	    {
		this.prefix = value;
	    }
	}

	public StringType Image
	{
	    get
	    {
		return this.image;
	    }
	    set
	    {
		this.image = value;
	    }
	}
    }
}
