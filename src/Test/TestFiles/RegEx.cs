using System;
using System.Collections;
using System.Text.RegularExpressions;
using UppercaseLiving.Dss.Exceptions;
using Spring2.Core.DAO;
using Spring2.Core.Message;
using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;
using UppercaseLiving.Dss.Dao;
using UppercaseLiving.Dss.DataObject;
using UppercaseLiving.Dss.Types;
using Spring2.Core.BusinessEntity;

namespace UppercaseLiving.Dss.BusinessLogic {
    
    
    public class Business : BusinessEntity, IBusiness {
        
	[Generate()]
	private IdType businessId = IdType.DEFAULT;
        
	[Generate()]
	private StringType name = StringType.DEFAULT;
        
	[Generate()]
	private BusinessTypeEnum businessType = BusinessTypeEnum.DEFAULT;
        
	[Generate()]
	private StringType taxIdentificationNumber = StringType.DEFAULT;
        
	[Generate()]
	private StringType state = StringType.DEFAULT;
        
	[Generate()]
	internal Business() {
            
	}
        
	[Generate()]
	internal Business(Boolean isNew) {
	    this.isNew = isNew;
	}
        
	[Generate()]
	public IdType BusinessId {
	    get {
		return this.businessId;
	    }
	    set {
		this.businessId = value;
	    }
	}
        
	[Generate()]
	public StringType Name {
	    get {
		return this.name;
	    }
	    set {
		this.name = value;
	    }
	}
        
	[Generate()]
	public BusinessTypeEnum BusinessType {
	    get {
		return this.businessType;
	    }
	    set {
		this.businessType = value;
	    }
	}
        
	[Generate()]
	public StringType TaxIdentificationNumber {
	    get {
		return this.taxIdentificationNumber;
	    }
	    set {
		this.taxIdentificationNumber = value;
	    }
	}
        
	[Generate()]
	public StringType State {
	    get {
		return this.state;
	    }
	    set {
		this.state = value;
	    }
	}
        
	[Generate()]
	public static Business NewInstance() {
	    return new Business();
	}
        
	[Generate()]
	public static Business GetInstance(IdType businessId) {
	    return BusinessDAO.DAO.Load(businessId);
	}
        
	[Generate()]
	public void Update(BusinessData data) {
	    name = data.Name.IsDefault ? name : data.Name;
	    businessType = data.BusinessType.IsDefault ? businessType : data.BusinessType;
	    taxIdentificationNumber = data.TaxIdentificationNumber.IsDefault ? taxIdentificationNumber : data.TaxIdentificationNumber;
	    state = data.State.IsDefault ? state : data.State;
	    Store();
	}
        
	[Generate()]
	public void Store() {
	    MessageList errors = Validate();

	    if (errors.Count > 0) {
		if (!isNew) {
		    this.Reload();
		}
		throw new MessageListException(errors);
	    }

	    if (isNew) {
		this.BusinessId = BusinessDAO.DAO.Insert(this);
		isNew = false;
	    } else {
		BusinessDAO.DAO.Update(this);
	    }
	}
        
	public MessageList Validate() {
	    MessageList errors = new MessageList();

	    if (this.name.Length < 0) {
		errors.Add(new MissingRequiredFieldError("Name"));
	    }
	    if (this.taxIdentificationNumber.Length < 0) {
		errors.Add(new MissingRequiredFieldError("Tax Identification Number"));
	    }
	    if (!this.businessType.IsValid) {
		errors.Add(new MissingRequiredFieldError("Business Type"));
	    } else {
		if (this.businessType == BusinessTypeEnum.SOLE_PROPRIETORSHIP && this.taxIdentificationNumber.Length > -1) {
		    string taxID = this.taxIdentificationNumber.ToString().Trim();
		    if (!((taxID.Length == 11 && Regex.IsMatch(taxID, @"^\d{3}-\d{2}-\d{4}")) || (taxID.Length != 7 && Regex.IsMatch(taxID, @"^\d{2}-\d{7}")))) {
			errors.Add(new InvalidFormatError("Tax Identification Number", "###-##-#### or ##-#######"));
		    }
		} else if (this.businessType != BusinessTypeEnum.SOLE_PROPRIETORSHIP && this.taxIdentificationNumber.Length > -1) {
		    string taxID = this.taxIdentificationNumber.ToString().Trim();
		    if (!(taxID.Length != 7 && Regex.IsMatch(taxID, @"^\d{2}-\d{7}"))) {
			errors.Add(new InvalidFormatError("Tax Identification Number", "##-#######"));
		    }
		}
	    }

	    //TODO: add state to entity and add validation for it

	    try {
		Business b = BusinessDAO.DAO.FindByTaxIdentificationNumber(this.taxIdentificationNumber);
		if (!b.BusinessId.Equals(this.BusinessId)) {
		    errors.Add(new DuplicateTaxIdentificationNumberError(this.taxIdentificationNumber));
		}
	    } catch (FinderException) {
		// not found, so this must be unique
	    }

	    return errors;
	}
        
	public static Business Create(BusinessData data) {
	    Business business = new Business();
	    business.Update(data);
	    business.Store();

	    return business;
	}
        
	public void Delete() {
	    IList dsaBusinesses = DirectSalesAgentBusinessDAO.DAO.FindByBusinessId(this.businessId);
	    foreach (DirectSalesAgentBusinessData data in dsaBusinesses) {
		DirectSalesAgentBusinessDAO.DAO.Delete(data.DirectSalesAgentBusinessId);
	    }
	    BusinessDAO.DAO.Delete(this.businessId);
	}
        
	[Generate()]
	public void Reload() {
	    BusinessDAO.DAO.Reload(this);
	}
    }
}