using System;

using Spring2.Core.Types;

using Golf.Tournament.DataObject;

namespace Golf.Tournament.DataObject {
    public class OrganizerData : Spring2.Core.DataObject.DataObject {

	private IdType organizerId = IdType.DEFAULT;
	private StringType name = StringType.DEFAULT;
	private AddressData address = new AddressData();
	private ContactData organizerContact = new ContactData();
	private ContactData technicalContact = new ContactData();

	public static readonly String ORGANIZERID = "OrganizerId";
	public static readonly String NAME = "Name";
	public static readonly String ADDRESS = "Address";
	public static readonly String ADDRESS_ADDRESS1 = "Address.Address1";
	public static readonly String ADDRESS_ADDRESS2 = "Address.Address2";
	public static readonly String ADDRESS_CITY = "Address.City";
	public static readonly String ADDRESS_STATE = "Address.State";
	public static readonly String ADDRESS_COUNTRY = "Address.Country";
	public static readonly String ADDRESS_POSTALCODE = "Address.PostalCode";
	public static readonly String ORGANIZERCONTACT = "OrganizerContact";
	public static readonly String ORGANIZERCONTACT_NAME = "OrganizerContact.Name";
	public static readonly String ORGANIZERCONTACT_PHONE = "OrganizerContact.Phone";
	public static readonly String ORGANIZERCONTACT_EMAIL = "OrganizerContact.Email";
	public static readonly String TECHNICALCONTACT = "TechnicalContact";
	public static readonly String TECHNICALCONTACT_NAME = "TechnicalContact.Name";
	public static readonly String TECHNICALCONTACT_PHONE = "TechnicalContact.Phone";
	public static readonly String TECHNICALCONTACT_EMAIL = "TechnicalContact.Email";

	public IdType OrganizerId {
	    get { return this.organizerId; }
	    set { this.organizerId = value; }
	}

	public StringType Name {
	    get { return this.name; }
	    set { this.name = value; }
	}

	public AddressData Address {
	    get { return this.address; }
	    set { this.address = value; }
	}







	public ContactData OrganizerContact {
	    get { return this.organizerContact; }
	    set { this.organizerContact = value; }
	}




	public ContactData TechnicalContact {
	    get { return this.technicalContact; }
	    set { this.technicalContact = value; }
	}



    }
}
