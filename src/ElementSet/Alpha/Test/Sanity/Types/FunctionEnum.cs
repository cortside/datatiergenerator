using System;
using System.Collections;

namespace Golf.Tournament.Types {

    public class FunctionEnum : Spring2.Core.Types.EnumDataType {

	private static readonly ArrayList OPTIONS = new ArrayList();

	public static readonly new FunctionEnum DEFAULT = new FunctionEnum();
	public static readonly new FunctionEnum UNSET = new FunctionEnum();

	/// <summary>
	/// Place an Order
	/// </summary>
	public static readonly FunctionEnum ORDER = new FunctionEnum("1", "Order");
	/// <summary>
	/// Monitor/Acknowledge New Vendor Orders
	/// </summary>
	public static readonly FunctionEnum VENDOR_MONITOR_ORDERS = new FunctionEnum("2", "Vendor Monitor Orders");
	/// <summary>
	/// Edit Vendor Information
	/// </summary>
	public static readonly FunctionEnum VENDOR_EDIT = new FunctionEnum("3", "Vendor Edit");
	/// <summary>
	/// View a vendor's invoices
	/// </summary>
	public static readonly FunctionEnum VENDOR_VIEW_INVOICES = new FunctionEnum("4", "Vendor View Invoices");

	public static FunctionEnum GetInstance(Object value) {
	    if (value is String) {
		foreach (FunctionEnum t in OPTIONS) {
		    if (t.Value.Equals(value)) {
			return t;
		    }
		}
	    }
	    if (value is Int32) {
		foreach (FunctionEnum t in OPTIONS) {
		    try {
			if (Int32.Parse(t.Code).Equals(value)) {
			    return t;
			}
		    } catch (Exception) {
			// parse exception - continue
		    }
		}
	    }

	    return UNSET;
	}

	private FunctionEnum() {}

	private FunctionEnum(String code, String name) {
	    this.code = code;
	    this.name = name;
	    OPTIONS.Add(this);
	}

	public override Boolean IsDefault {
	    get { return Object.ReferenceEquals(this, DEFAULT); }
	}

	public override Boolean IsUnset {
	    get { return Object.ReferenceEquals(this, UNSET); }
	}

	public static IList Options {
	    get { return OPTIONS; }
	}

	/// <summary>
	/// Convert a FunctionEnum instance to an Int32;
	/// </summary>
	/// <returns>the Int32 representation for the enum instance.</returns>
	/// <exception cref="InvalidCastException">when converting DEFAULT or UNSET to an Int32.</exception>
	public Int32 ToInt32() {
	    if (IsValid) {
		try {
		    return Int32.Parse(code);
		} catch (Exception) {
		    // parse error  - don't do anything - an acceptable exception will be thrown below
		}
	    }

	    // instance was !IsValid or there was a parser error
	    throw new InvalidCastException();
	}
    }
}
