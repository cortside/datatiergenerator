using System.Collections;
using Spring2.Core.DAO;
using Spring2.Core.Types;
using Seamless.Manhattan.DAO;
using System;
using Seamless.Manhattan.DataObject;
using Seamless.Manhattan.Types;
using Spring2.DataTierGenerator.Attribute;

namespace Seamless.Manhattan.BusinessLogic {
    
    
    public class Vendor {
        
	// the type should be VendorData
	[Generate()]
	private Foo internalState;
        
	[Generate()]
	private Vendor(IdType vendorID) {
	    internalState = VendorDAO.Load(vendorID);
	}
        
	/// <summary>
	/// Creates and persists a new Vendor
	/// </summary>
	/// <param name="data"></param>
	[Generate()]
	public Vendor(VendorData data) {
	    // initialize the internal state
	    SetInitialState();

	    // copy the applicable properties, if they are valid
	    UpdateInternalState(data);

	    // insert new record
	    IdType id = VendorDAO.Insert(internalState);
	    internalState.VendorID = id;
	}
        
	/// <summary>
	/// Returns fully populated internal state
	/// </summary>
	[Generate()]
	public VendorData Data {
	    get {
		return internalState;
	    }
	}
        
	/// <summary>
	/// Retrieves an existing instance of a Vendor with the specified id
	/// </summary>
	[Generate()]
	public static Vendor GetInstance(IdType VendorId) {
	    return new Vendor(VendorId);
	}
        
	/// <summary>
	/// Update current instance from passed in data object
	/// </summary>
	/// <param name="data"></param>
	[Generate()]
	public void Update(VendorData data) {
	    // copy the applicable properties, if they are valid
	    UpdateInternalState(data);
	    VendorDAO.Update(internalState);
	}

	[Generate()]
	public void NoLongerGeneratedMethod() {
	    // method that used to be generated, and should be removed
	}
        
	private void SetInitialState() {
	    internalState = new VendorData();
	    internalState.VendorID = IdType.DEFAULT;
	    internalState.VendorName = StringType.DEFAULT;
	    internalState.VendorTypeID = IdType.DEFAULT;
	    internalState.PhoneNumber = StringType.DEFAULT;
	    internalState.Description = StringType.DEFAULT;
	    internalState.OrderLeadMins = IntegerType.DEFAULT;
	    internalState.MinimumOrderAmt = CurrencyType.DEFAULT;
	    internalState.GratuityOrderAmt = CurrencyType.DEFAULT;
	    internalState.GratuityPct = DecimalType.DEFAULT;
	    internalState.TaxRate = DecimalType.DEFAULT;
	    internalState.AccountNumber = StringType.DEFAULT;
	    internalState.OrderEventTypeID = IdType.DEFAULT;
	    internalState.EffectiveDate = DateType.DEFAULT;
	    internalState.ExpirationDate = DateType.DEFAULT;
	    internalState.ContactUserID = IdType.DEFAULT;
	    internalState.LogoFile = StringType.DEFAULT;
	    internalState.StatusNormal = IntegerType.DEFAULT;
	    internalState.StatusBusy = IntegerType.DEFAULT;
	    internalState.StatusVeryBusy = IntegerType.DEFAULT;
	    internalState.DiscountRate = CurrencyType.DEFAULT;
	    internalState.IsPercent = StringType.DEFAULT;
	    internalState.DefaultTerms = IntegerType.DEFAULT;
	    internalState.IsCreditTranFeeDeducted = StringType.DEFAULT;
	    internalState.BasisID = IdType.DEFAULT;
	    internalState.IsOverrideLowerLevel = StringType.DEFAULT;
	    internalState.VendorDescription = StringType.DEFAULT;
	    internalState.DeliveryFee = CurrencyType.DEFAULT;
	    internalState.DeliveryFeeIsPercent = StringType.DEFAULT;
	    internalState.DeliveryFeeOnTax = StringType.DEFAULT;
	    internalState.LocationCount = IntegerType.DEFAULT;
	    internalState.DefaultOrderEventTypeID = IdType.DEFAULT;
	}
        
	private void UpdateInternalState(VendorData data) {
	    if (data.VendorID.IsValid) {
		internalState.VendorID = data.VendorID;
	    }
	    if (data.VendorName.IsValid) {
		internalState.VendorName = data.VendorName;
	    }
	    if (data.VendorTypeID.IsValid) {
		internalState.VendorTypeID = data.VendorTypeID;
	    }
	    if (data.PhoneNumber.IsValid) {
		internalState.PhoneNumber = data.PhoneNumber;
	    }
	    if (data.Description.IsValid) {
		internalState.Description = data.Description;
	    }
	    if (data.OrderLeadMins.IsValid) {
		internalState.OrderLeadMins = data.OrderLeadMins;
	    }
	    if (data.MinimumOrderAmt.IsValid) {
		internalState.MinimumOrderAmt = data.MinimumOrderAmt;
	    }
	    if (data.GratuityOrderAmt.IsValid) {
		internalState.GratuityOrderAmt = data.GratuityOrderAmt;
	    }
	    if (data.GratuityPct.IsValid) {
		internalState.GratuityPct = data.GratuityPct;
	    }
	    if (data.TaxRate.IsValid) {
		internalState.TaxRate = data.TaxRate;
	    }
	    if (data.AccountNumber.IsValid) {
		internalState.AccountNumber = data.AccountNumber;
	    }
	    if (data.OrderEventTypeID.IsValid) {
		internalState.OrderEventTypeID = data.OrderEventTypeID;
	    }
	    if (data.EffectiveDate.IsValid) {
		internalState.EffectiveDate = data.EffectiveDate;
	    }
	    if (data.ExpirationDate.IsValid) {
		internalState.ExpirationDate = data.ExpirationDate;
	    }
	    if (data.ContactUserID.IsValid) {
		internalState.ContactUserID = data.ContactUserID;
	    }
	    if (data.LogoFile.IsValid) {
		internalState.LogoFile = data.LogoFile;
	    }
	    if (data.StatusNormal.IsValid) {
		internalState.StatusNormal = data.StatusNormal;
	    }
	    if (data.StatusBusy.IsValid) {
		internalState.StatusBusy = data.StatusBusy;
	    }
	    if (data.StatusVeryBusy.IsValid) {
		internalState.StatusVeryBusy = data.StatusVeryBusy;
	    }
	    if (data.DiscountRate.IsValid) {
		internalState.DiscountRate = data.DiscountRate;
	    }
	    if (data.IsPercent.IsValid) {
		internalState.IsPercent = data.IsPercent;
	    }
	    if (data.DefaultTerms.IsValid) {
		internalState.DefaultTerms = data.DefaultTerms;
	    }
	    if (data.IsCreditTranFeeDeducted.IsValid) {
		internalState.IsCreditTranFeeDeducted = data.IsCreditTranFeeDeducted;
	    }
	    if (data.BasisID.IsValid) {
		internalState.BasisID = data.BasisID;
	    }
	    if (data.IsOverrideLowerLevel.IsValid) {
		internalState.IsOverrideLowerLevel = data.IsOverrideLowerLevel;
	    }
	    if (data.VendorDescription.IsValid) {
		internalState.VendorDescription = data.VendorDescription;
	    }
	    if (data.DeliveryFee.IsValid) {
		internalState.DeliveryFee = data.DeliveryFee;
	    }
	    if (data.DeliveryFeeIsPercent.IsValid) {
		internalState.DeliveryFeeIsPercent = data.DeliveryFeeIsPercent;
	    }
	    if (data.DeliveryFeeOnTax.IsValid) {
		internalState.DeliveryFeeOnTax = data.DeliveryFeeOnTax;
	    }
	    if (data.LocationCount.IsValid) {
		internalState.LocationCount = data.LocationCount;
	    }
	    if (data.DefaultOrderEventTypeID.IsValid) {
		internalState.DefaultOrderEventTypeID = data.DefaultOrderEventTypeID;
	    }
	}
        
	private ErrorMessageList Validate() {
	    ErrorMessageList errors = new ErrorMessageList();

	    if (!internalState.VendorID.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.VENDORID));
	    }
	    if (!internalState.VendorName.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.VENDORNAME));
	    }
	    if (!internalState.VendorTypeID.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.VENDORTYPEID));
	    }
	    if (!internalState.PhoneNumber.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.PHONENUMBER));
	    }
	    if (!internalState.Description.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.DESCRIPTION));
	    }
	    if (!internalState.OrderLeadMins.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.ORDERLEADMINS));
	    }
	    if (!internalState.MinimumOrderAmt.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.MINIMUMORDERAMT));
	    }
	    if (!internalState.GratuityOrderAmt.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.GRATUITYORDERAMT));
	    }
	    if (!internalState.GratuityPct.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.GRATUITYPCT));
	    }
	    if (!internalState.TaxRate.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.TAXRATE));
	    }
	    if (!internalState.AccountNumber.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.ACCOUNTNUMBER));
	    }
	    if (!internalState.OrderEventTypeID.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.ORDEREVENTTYPEID));
	    }
	    if (!internalState.EffectiveDate.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.EFFECTIVEDATE));
	    }
	    if (!internalState.ExpirationDate.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.EXPIRATIONDATE));
	    }
	    if (!internalState.ContactUserID.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.CONTACTUSERID));
	    }
	    if (!internalState.LogoFile.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.LOGOFILE));
	    }
	    if (!internalState.StatusNormal.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.STATUSNORMAL));
	    }
	    if (!internalState.StatusBusy.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.STATUSBUSY));
	    }
	    if (!internalState.StatusVeryBusy.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.STATUSVERYBUSY));
	    }
	    if (!internalState.DiscountRate.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.DISCOUNTRATE));
	    }
	    if (!internalState.IsPercent.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.ISPERCENT));
	    }
	    if (!internalState.DefaultTerms.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.DEFAULTTERMS));
	    }
	    if (!internalState.IsCreditTranFeeDeducted.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.ISCREDITTRANFEEDEDUCTED));
	    }
	    if (!internalState.BasisID.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.BASISID));
	    }
	    if (!internalState.IsOverrideLowerLevel.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.ISOVERRIDELOWERLEVEL));
	    }
	    if (!internalState.VendorDescription.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.VENDORDESCRIPTION));
	    }
	    if (!internalState.DeliveryFee.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.DELIVERYFEE));
	    }
	    if (!internalState.DeliveryFeeIsPercent.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.DELIVERYFEEISPERCENT));
	    }
	    if (!internalState.DeliveryFeeOnTax.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.DELIVERYFEEONTAX));
	    }
	    if (!internalState.LocationCount.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.LOCATIONCOUNT));
	    }
	    if (!internalState.DefaultOrderEventTypeID.IsValid) {
		errors.Add(MessageConditionEnum.MISSING_REQUIRED_FIELD, StringType.Parse(VendorData.DEFAULTORDEREVENTTYPEID));
	    }
	    return errors;
	}

	#region Custom Code
	public void MethodInRegion() {
	    // this is the MethodInRegion body
	}
	#endregion
    }
}