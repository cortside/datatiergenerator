using System;

using Spring2.Core.Types;

using Golf.Tournament.DataObject;

namespace Golf.Tournament.DataObject {
    public class CreditCardData : Spring2.Core.DataObject.DataObject {

	private StringType number = StringType.DEFAULT;
	private DateType expirationDate = DateType.DEFAULT;
	private StringType name = StringType.DEFAULT;
	private AddressData address = new AddressData();

	public static readonly String NUMBER = "Number";
	public static readonly String EXPIRATIONDATE = "ExpirationDate";
	public static readonly String NAME = "Name";
	public static readonly String ADDRESS = "Address";
	public static readonly String ADDRESS_ADDRESS1 = "Address.Address1";
	public static readonly String ADDRESS_ADDRESS2 = "Address.Address2";
	public static readonly String ADDRESS_CITY = "Address.City";
	public static readonly String ADDRESS_STATE = "Address.State";
	public static readonly String ADDRESS_COUNTRY = "Address.Country";
	public static readonly String ADDRESS_POSTALCODE = "Address.PostalCode";

	public StringType Number {
	    get { return this.number; }
	    set { this.number = value; }
	}

	public DateType ExpirationDate {
	    get { return this.expirationDate; }
	    set { this.expirationDate = value; }
	}

	public StringType Name {
	    get { return this.name; }
	    set { this.name = value; }
	}

	public AddressData Address {
	    get { return this.address; }
	    set { this.address = value; }
	}
    }
}
