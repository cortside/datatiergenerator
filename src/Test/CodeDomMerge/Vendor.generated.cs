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
        
	[Generate()]
	private VendorData internalState;
        
	[Generate()]
	private Vendor(IdType vendorID) {
	    internalState = VendorDAO.Load(vendorID);
	    // from generated code
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
	    // from generated code
	}
        
	/// <summary>
	/// Returns fully populated internal state
	/// </summary>
	[Generate()]
	public VendorData Data {
	    get {
		return internalState;
		// from generated code
	    }
	}
        
	/// <summary>
	/// Retrieves an existing instance of a Vendor with the specified id
	/// </summary>
	[Generate()]
	public static Vendor GetInstance(IdType VendorId) {
	    return new Vendor(VendorId);
	    // from generated code
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
	    // from generated code
	}
        
	private void SetInitialState() {
	    // from generated code
	    // to to make sure that methods without the Generate attribute will not be overwritten
	}
        
	private void UpdateInternalState(VendorData data) {
	    // from generated code
	    // to to make sure that methods without the Generate attribute will not be overwritten
	}
        
	private ErrorMessageList Validate() {
	    // from generated code
	    // to to make sure that methods without the Generate attribute will not be overwritten
	}

	private void NewMethod() {
	    // from generated code
	    // to to make sure that new methods are inserted
	}
    }
}