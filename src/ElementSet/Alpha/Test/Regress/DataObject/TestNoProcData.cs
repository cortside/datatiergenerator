using System;

using Spring2.Core.Types;
using Spring2.DataTierGenerator.DataObject;
using Spring2.DataTierGenerator.Types;

namespace Spring2.DataTierGenerator.DataObject
{
    public class TestNoProcData : Spring2.Core.DataObject.DataObject
    {

	private StringType stringColumn = StringType.DEFAULT;
	private IdType int32Column = IdType.DEFAULT;
	private FormatType emailFormat = FormatType.DEFAULT;
	private AddressData address = new AddressData();

	public static readonly String STRINGCOLUMN = "StringColumn";
	public static readonly String INT32COLUMN = "Int32Column";
	public static readonly String EMAILFORMAT = "EmailFormat";
	public static readonly String ADDRESS = "Address";
	public static readonly String ADDRESS_ADDRESS1 = "Address.Address1";
	public static readonly String ADDRESS_ADDRESS2 = "Address.Address2";
	public static readonly String ADDRESS_CITY = "Address.City";
	public static readonly String ADDRESS_STATE = "Address.State";
	public static readonly String ADDRESS_POSTALCODE = "Address.PostalCode";
	public static readonly String ADDRESS_COUNTRY = "Address.Country";

	public StringType StringColumn
	{
	    get
	    {
		return this.stringColumn;
	    }
	    set
	    {
		this.stringColumn = value;
	    }
	}

	public IdType Int32Column
	{
	    get
	    {
		return this.int32Column;
	    }
	    set
	    {
		this.int32Column = value;
	    }
	}

	public FormatType EmailFormat
	{
	    get
	    {
		return this.emailFormat;
	    }
	    set
	    {
		this.emailFormat = value;
	    }
	}

	public AddressData Address
	{
	    get
	    {
		return this.address;
	    }
	    set
	    {
		this.address = value;
	    }
	}





    }
}
