using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class OrderDtlData : Spring2.Core.DataObject.DataObject
    {

	private StringType orderDetailId = StringType.DEFAULT;
	private StringType orderNo = StringType.DEFAULT;
	private StringType typeOrd = StringType.DEFAULT;
	private StringType itemId = StringType.DEFAULT;
	private StringType itemDescrip = StringType.DEFAULT;
	private StringType binNo = StringType.DEFAULT;
	private IntegerType qtyOrdered = IntegerType.DEFAULT;
	private IntegerType qtyPicked = IntegerType.DEFAULT;
	private IntegerType qtyShipped = IntegerType.DEFAULT;
	private DecimalType shippingNo = DecimalType.DEFAULT;
	private DecimalType itemPrice = DecimalType.DEFAULT;
	private DecimalType subtotal = DecimalType.DEFAULT;
	private BooleanType personal = BooleanType.DEFAULT;
	private BooleanType taxExempt = BooleanType.DEFAULT;
	private StringType taxExemptNo = StringType.DEFAULT;
	private DateType shipDate = DateType.DEFAULT;
	private IntegerType kit = IntegerType.DEFAULT;
	private IntegerType guestIdOld = IntegerType.DEFAULT;
	private StringType componentId = StringType.DEFAULT;
	private StringType guestId = StringType.DEFAULT;
	private IntegerType xxxsave = IntegerType.DEFAULT;
	private IntegerType startkitCategory = IntegerType.DEFAULT;
	private DateType gLPostedDate = DateType.DEFAULT;
	private DecimalType revenue = DecimalType.DEFAULT;
	private DecimalType discountAmount = DecimalType.DEFAULT;
	private IntegerType qtyCanceled = IntegerType.DEFAULT;
	private DateType canceledDate = DateType.DEFAULT;
	private BooleanType chargeForItem = BooleanType.DEFAULT;
	private StringType errorCode = StringType.DEFAULT;
	private StringType errorDepartment = StringType.DEFAULT;
	private DateType waitReturnBeginDate = DateType.DEFAULT;
	private DateType waitReturnDueDate = DateType.DEFAULT;
	private DateType waitReturnReceivedDate = DateType.DEFAULT;
	private StringType itemNotes = StringType.DEFAULT;
	private BooleanType saveAsShipped = BooleanType.DEFAULT;
	private DateType purchasedDate = DateType.DEFAULT;

	public static readonly String ORDERDETAILID = "OrderDetailId";
	public static readonly String ORDERNO = "OrderNo";
	public static readonly String TYPEORD = "TypeOrd";
	public static readonly String ITEMID = "ItemId";
	public static readonly String ITEMDESCRIP = "ItemDescrip";
	public static readonly String BINNO = "BinNo";
	public static readonly String QTYORDERED = "QtyOrdered";
	public static readonly String QTYPICKED = "QtyPicked";
	public static readonly String QTYSHIPPED = "QtyShipped";
	public static readonly String SHIPPINGNO = "ShippingNo";
	public static readonly String ITEMPRICE = "ItemPrice";
	public static readonly String SUBTOTAL = "Subtotal";
	public static readonly String PERSONAL = "Personal";
	public static readonly String TAXEXEMPT = "TaxExempt";
	public static readonly String TAXEXEMPTNO = "TaxExemptNo";
	public static readonly String SHIPDATE = "ShipDate";
	public static readonly String KIT = "Kit";
	public static readonly String GUESTIDOLD = "GuestIdOld";
	public static readonly String COMPONENTID = "ComponentId";
	public static readonly String GUESTID = "GuestId";
	public static readonly String XXXSAVE = "Xxxsave";
	public static readonly String STARTKITCATEGORY = "StartkitCategory";
	public static readonly String GLPOSTEDDATE = "GLPostedDate";
	public static readonly String REVENUE = "Revenue";
	public static readonly String DISCOUNTAMOUNT = "DiscountAmount";
	public static readonly String QTYCANCELED = "QtyCanceled";
	public static readonly String CANCELEDDATE = "CanceledDate";
	public static readonly String CHARGEFORITEM = "ChargeForItem";
	public static readonly String ERRORCODE = "ErrorCode";
	public static readonly String ERRORDEPARTMENT = "ErrorDepartment";
	public static readonly String WAITRETURNBEGINDATE = "WaitReturnBeginDate";
	public static readonly String WAITRETURNDUEDATE = "WaitReturnDueDate";
	public static readonly String WAITRETURNRECEIVEDDATE = "WaitReturnReceivedDate";
	public static readonly String ITEMNOTES = "ItemNotes";
	public static readonly String SAVEASSHIPPED = "SaveAsShipped";
	public static readonly String PURCHASEDDATE = "PurchasedDate";

	public StringType OrderDetailId
	{
	    get
	    {
		return this.orderDetailId;
	    }
	    set
	    {
		this.orderDetailId = value;
	    }
	}

	public StringType OrderNo
	{
	    get
	    {
		return this.orderNo;
	    }
	    set
	    {
		this.orderNo = value;
	    }
	}

	public StringType TypeOrd
	{
	    get
	    {
		return this.typeOrd;
	    }
	    set
	    {
		this.typeOrd = value;
	    }
	}

	public StringType ItemId
	{
	    get
	    {
		return this.itemId;
	    }
	    set
	    {
		this.itemId = value;
	    }
	}

	public StringType ItemDescrip
	{
	    get
	    {
		return this.itemDescrip;
	    }
	    set
	    {
		this.itemDescrip = value;
	    }
	}

	public StringType BinNo
	{
	    get
	    {
		return this.binNo;
	    }
	    set
	    {
		this.binNo = value;
	    }
	}

	public IntegerType QtyOrdered
	{
	    get
	    {
		return this.qtyOrdered;
	    }
	    set
	    {
		this.qtyOrdered = value;
	    }
	}

	public IntegerType QtyPicked
	{
	    get
	    {
		return this.qtyPicked;
	    }
	    set
	    {
		this.qtyPicked = value;
	    }
	}

	public IntegerType QtyShipped
	{
	    get
	    {
		return this.qtyShipped;
	    }
	    set
	    {
		this.qtyShipped = value;
	    }
	}

	public DecimalType ShippingNo
	{
	    get
	    {
		return this.shippingNo;
	    }
	    set
	    {
		this.shippingNo = value;
	    }
	}

	public DecimalType ItemPrice
	{
	    get
	    {
		return this.itemPrice;
	    }
	    set
	    {
		this.itemPrice = value;
	    }
	}

	public DecimalType Subtotal
	{
	    get
	    {
		return this.subtotal;
	    }
	    set
	    {
		this.subtotal = value;
	    }
	}

	public BooleanType Personal
	{
	    get
	    {
		return this.personal;
	    }
	    set
	    {
		this.personal = value;
	    }
	}

	public BooleanType TaxExempt
	{
	    get
	    {
		return this.taxExempt;
	    }
	    set
	    {
		this.taxExempt = value;
	    }
	}

	public StringType TaxExemptNo
	{
	    get
	    {
		return this.taxExemptNo;
	    }
	    set
	    {
		this.taxExemptNo = value;
	    }
	}

	public DateType ShipDate
	{
	    get
	    {
		return this.shipDate;
	    }
	    set
	    {
		this.shipDate = value;
	    }
	}

	public IntegerType Kit
	{
	    get
	    {
		return this.kit;
	    }
	    set
	    {
		this.kit = value;
	    }
	}

	public IntegerType GuestIdOld
	{
	    get
	    {
		return this.guestIdOld;
	    }
	    set
	    {
		this.guestIdOld = value;
	    }
	}

	public StringType ComponentId
	{
	    get
	    {
		return this.componentId;
	    }
	    set
	    {
		this.componentId = value;
	    }
	}

	public StringType GuestId
	{
	    get
	    {
		return this.guestId;
	    }
	    set
	    {
		this.guestId = value;
	    }
	}

	public IntegerType Xxxsave
	{
	    get
	    {
		return this.xxxsave;
	    }
	    set
	    {
		this.xxxsave = value;
	    }
	}

	public IntegerType StartkitCategory
	{
	    get
	    {
		return this.startkitCategory;
	    }
	    set
	    {
		this.startkitCategory = value;
	    }
	}

	public DateType GLPostedDate
	{
	    get
	    {
		return this.gLPostedDate;
	    }
	    set
	    {
		this.gLPostedDate = value;
	    }
	}

	public DecimalType Revenue
	{
	    get
	    {
		return this.revenue;
	    }
	    set
	    {
		this.revenue = value;
	    }
	}

	public DecimalType DiscountAmount
	{
	    get
	    {
		return this.discountAmount;
	    }
	    set
	    {
		this.discountAmount = value;
	    }
	}

	public IntegerType QtyCanceled
	{
	    get
	    {
		return this.qtyCanceled;
	    }
	    set
	    {
		this.qtyCanceled = value;
	    }
	}

	public DateType CanceledDate
	{
	    get
	    {
		return this.canceledDate;
	    }
	    set
	    {
		this.canceledDate = value;
	    }
	}

	public BooleanType ChargeForItem
	{
	    get
	    {
		return this.chargeForItem;
	    }
	    set
	    {
		this.chargeForItem = value;
	    }
	}

	public StringType ErrorCode
	{
	    get
	    {
		return this.errorCode;
	    }
	    set
	    {
		this.errorCode = value;
	    }
	}

	public StringType ErrorDepartment
	{
	    get
	    {
		return this.errorDepartment;
	    }
	    set
	    {
		this.errorDepartment = value;
	    }
	}

	public DateType WaitReturnBeginDate
	{
	    get
	    {
		return this.waitReturnBeginDate;
	    }
	    set
	    {
		this.waitReturnBeginDate = value;
	    }
	}

	public DateType WaitReturnDueDate
	{
	    get
	    {
		return this.waitReturnDueDate;
	    }
	    set
	    {
		this.waitReturnDueDate = value;
	    }
	}

	public DateType WaitReturnReceivedDate
	{
	    get
	    {
		return this.waitReturnReceivedDate;
	    }
	    set
	    {
		this.waitReturnReceivedDate = value;
	    }
	}

	public StringType ItemNotes
	{
	    get
	    {
		return this.itemNotes;
	    }
	    set
	    {
		this.itemNotes = value;
	    }
	}

	public BooleanType SaveAsShipped
	{
	    get
	    {
		return this.saveAsShipped;
	    }
	    set
	    {
		this.saveAsShipped = value;
	    }
	}

	public DateType PurchasedDate
	{
	    get
	    {
		return this.purchasedDate;
	    }
	    set
	    {
		this.purchasedDate = value;
	    }
	}
    }
}
