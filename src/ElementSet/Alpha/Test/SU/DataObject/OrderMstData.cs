using System;

using Spring2.Core.Types;

using StampinUp.Core.Types;
using StampinUp.Types;

namespace StampinUp.DataObject
{
    public class OrderMstData : Spring2.Core.DataObject.DataObject
    {

	private StringType orderNo = StringType.DEFAULT;
	private DateType ordStart = DateType.DEFAULT;
	private DateType ordDate = DateType.DEFAULT;
	private IntegerType timeSpend = IntegerType.DEFAULT;
	private StringType ordType = StringType.DEFAULT;
	private StringType ordStatus = StringType.DEFAULT;
	private StringType ordSource = StringType.DEFAULT;
	private DemoIdType demoId = DemoIdType.DEFAULT;
	private StringType hostessId = StringType.DEFAULT;
	private StringType shipToFname = StringType.DEFAULT;
	private StringType shipToLname = StringType.DEFAULT;
	private StringType shipToAddress1 = StringType.DEFAULT;
	private StringType shipToAddress2 = StringType.DEFAULT;
	private StringType shipToCity = StringType.DEFAULT;
	private StringType shipToState = StringType.DEFAULT;
	private StringType shipToZip = StringType.DEFAULT;
	private DateType shipDate = DateType.DEFAULT;
	private StringType orderNote = StringType.DEFAULT;
	private StringType operatorId = StringType.DEFAULT;
	private DecimalType orderSubtotal = DecimalType.DEFAULT;
	private DecimalType taxRate = DecimalType.DEFAULT;
	private DecimalType taxTotal = DecimalType.DEFAULT;
	private DecimalType orderTotal = DecimalType.DEFAULT;
	private DecimalType shippingHandling = DecimalType.DEFAULT;
	private DecimalType discAmount = DecimalType.DEFAULT;
	private DecimalType amountPaid = DecimalType.DEFAULT;
	private DecimalType balance = DecimalType.DEFAULT;
	private IntegerType continious = IntegerType.DEFAULT;
	private DateType workshopDate = DateType.DEFAULT;
	private DecimalType allowHostFree = DecimalType.DEFAULT;
	private DecimalType allowHostDisc = DecimalType.DEFAULT;
	private DecimalType totalHostFree = DecimalType.DEFAULT;
	private DecimalType totalHostDisc = DecimalType.DEFAULT;
	private DecimalType difference = DecimalType.DEFAULT;
	private DecimalType hostShipHandl = DecimalType.DEFAULT;
	private DecimalType hostTax = DecimalType.DEFAULT;
	private DecimalType hostTotalDue = DecimalType.DEFAULT;
	private IntegerType guestsNo = IntegerType.DEFAULT;
	private DateType commDate = DateType.DEFAULT;
	private DateType lastShipDate = DateType.DEFAULT;
	private DecimalType lastShippingNo = DecimalType.DEFAULT;
	private DateType dateCanceled = DateType.DEFAULT;
	private IntegerType backOrderCount = IntegerType.DEFAULT;
	private DecimalType netUnshippedAmt = DecimalType.DEFAULT;
	private DecimalType fefundAmt = DecimalType.DEFAULT;
	private DecimalType commAmt = DecimalType.DEFAULT;
	private IntegerType batchId = IntegerType.DEFAULT;
	private DateType dPCTaxDate = DateType.DEFAULT;
	private StringType geoCode = StringType.DEFAULT;
	private StringType inOut = StringType.DEFAULT;
	private StringType localCode1 = StringType.DEFAULT;
	private StringType localCode2 = StringType.DEFAULT;
	private StringType localCode3 = StringType.DEFAULT;
	private StringType localCode4 = StringType.DEFAULT;
	private StringType localCode5 = StringType.DEFAULT;
	private DecimalType hostessTaxableAmount = DecimalType.DEFAULT;
	private DecimalType taxableAmount = DecimalType.DEFAULT;
	private StringType sendToDemo = StringType.DEFAULT;
	private DateType firstSaveDate = DateType.DEFAULT;
	private DateType lastSaveDate = DateType.DEFAULT;
	private StringType shipToEmail = StringType.DEFAULT;
	private StringType promoKit = StringType.DEFAULT;
	private DecimalType expShipping = DecimalType.DEFAULT;
	private IntegerType shipMeth = IntegerType.DEFAULT;
	private StringType apoOrder = StringType.DEFAULT;
	private DecimalType pstQst = DecimalType.DEFAULT;
	private DecimalType gstHst = DecimalType.DEFAULT;
	private DecimalType pstQstRate = DecimalType.DEFAULT;
	private DecimalType gstHstRate = DecimalType.DEFAULT;
	private DecimalType pstQstTaxableAmount = DecimalType.DEFAULT;
	private DecimalType gstHstTaxableAmount = DecimalType.DEFAULT;
	private DecimalType pstQstHostessTaxableAmount = DecimalType.DEFAULT;
	private DecimalType gstHstHostessTaxableAmount = DecimalType.DEFAULT;
	private DecimalType pstQstHostess = DecimalType.DEFAULT;
	private DecimalType gstHstHostess = DecimalType.DEFAULT;
	private CountryEnum countryOfSale = CountryEnum.DEFAULT;
	private DateType gLPostedDate = DateType.DEFAULT;
	private StringType originalOrderNo = StringType.DEFAULT;
	private DecimalType restockFee = DecimalType.DEFAULT;
	private DateType pickAfter = DateType.DEFAULT;
	private StringType allWaitReturn = StringType.DEFAULT;
	private StringType freeShipping = StringType.DEFAULT;
	private BooleanType isSupplyLineOrder = BooleanType.DEFAULT;
	private StringType isPreOrder = StringType.DEFAULT;

	public static readonly String ORDERNO = "OrderNo";
	public static readonly String ORDSTART = "OrdStart";
	public static readonly String ORDDATE = "OrdDate";
	public static readonly String TIMESPEND = "TimeSpend";
	public static readonly String ORDTYPE = "OrdType";
	public static readonly String ORDSTATUS = "OrdStatus";
	public static readonly String ORDSOURCE = "OrdSource";
	public static readonly String DEMOID = "DemoId";
	public static readonly String HOSTESSID = "HostessId";
	public static readonly String SHIPTOFNAME = "ShipToFname";
	public static readonly String SHIPTOLNAME = "ShipToLname";
	public static readonly String SHIPTOADDRESS1 = "ShipToAddress1";
	public static readonly String SHIPTOADDRESS2 = "ShipToAddress2";
	public static readonly String SHIPTOCITY = "ShipToCity";
	public static readonly String SHIPTOSTATE = "ShipToState";
	public static readonly String SHIPTOZIP = "ShipToZip";
	public static readonly String SHIPDATE = "ShipDate";
	public static readonly String ORDERNOTE = "OrderNote";
	public static readonly String OPERATORID = "OperatorId";
	public static readonly String ORDERSUBTOTAL = "OrderSubtotal";
	public static readonly String TAXRATE = "TaxRate";
	public static readonly String TAXTOTAL = "TaxTotal";
	public static readonly String ORDERTOTAL = "OrderTotal";
	public static readonly String SHIPPINGHANDLING = "ShippingHandling";
	public static readonly String DISCAMOUNT = "DiscAmount";
	public static readonly String AMOUNTPAID = "AmountPaid";
	public static readonly String BALANCE = "Balance";
	public static readonly String CONTINIOUS = "Continious";
	public static readonly String WORKSHOPDATE = "WorkshopDate";
	public static readonly String ALLOWHOSTFREE = "AllowHostFree";
	public static readonly String ALLOWHOSTDISC = "AllowHostDisc";
	public static readonly String TOTALHOSTFREE = "TotalHostFree";
	public static readonly String TOTALHOSTDISC = "TotalHostDisc";
	public static readonly String DIFFERENCE = "Difference";
	public static readonly String HOSTSHIPHANDL = "HostShipHandl";
	public static readonly String HOSTTAX = "HostTax";
	public static readonly String HOSTTOTALDUE = "HostTotalDue";
	public static readonly String GUESTSNO = "GuestsNo";
	public static readonly String COMMDATE = "CommDate";
	public static readonly String LASTSHIPDATE = "LastShipDate";
	public static readonly String LASTSHIPPINGNO = "LastShippingNo";
	public static readonly String DATECANCELED = "DateCanceled";
	public static readonly String BACKORDERCOUNT = "BackOrderCount";
	public static readonly String NETUNSHIPPEDAMT = "NetUnshippedAmt";
	public static readonly String FEFUNDAMT = "FefundAmt";
	public static readonly String COMMAMT = "CommAmt";
	public static readonly String BATCHID = "BatchId";
	public static readonly String DPCTAXDATE = "DPCTaxDate";
	public static readonly String GEOCODE = "GeoCode";
	public static readonly String INOUT = "InOut";
	public static readonly String LOCALCODE1 = "LocalCode1";
	public static readonly String LOCALCODE2 = "LocalCode2";
	public static readonly String LOCALCODE3 = "LocalCode3";
	public static readonly String LOCALCODE4 = "LocalCode4";
	public static readonly String LOCALCODE5 = "LocalCode5";
	public static readonly String HOSTESSTAXABLEAMOUNT = "HostessTaxableAmount";
	public static readonly String TAXABLEAMOUNT = "TaxableAmount";
	public static readonly String SENDTODEMO = "SendToDemo";
	public static readonly String FIRSTSAVEDATE = "FirstSaveDate";
	public static readonly String LASTSAVEDATE = "LastSaveDate";
	public static readonly String SHIPTOEMAIL = "ShipToEmail";
	public static readonly String PROMOKIT = "PromoKit";
	public static readonly String EXPSHIPPING = "ExpShipping";
	public static readonly String SHIPMETH = "ShipMeth";
	public static readonly String APOORDER = "ApoOrder";
	public static readonly String PSTQST = "PstQst";
	public static readonly String GSTHST = "GstHst";
	public static readonly String PSTQSTRATE = "PstQstRate";
	public static readonly String GSTHSTRATE = "GstHstRate";
	public static readonly String PSTQSTTAXABLEAMOUNT = "PstQstTaxableAmount";
	public static readonly String GSTHSTTAXABLEAMOUNT = "GstHstTaxableAmount";
	public static readonly String PSTQSTHOSTESSTAXABLEAMOUNT = "PstQstHostessTaxableAmount";
	public static readonly String GSTHSTHOSTESSTAXABLEAMOUNT = "GstHstHostessTaxableAmount";
	public static readonly String PSTQSTHOSTESS = "PstQstHostess";
	public static readonly String GSTHSTHOSTESS = "GstHstHostess";
	public static readonly String COUNTRYOFSALE = "CountryOfSale";
	public static readonly String GLPOSTEDDATE = "GLPostedDate";
	public static readonly String ORIGINALORDERNO = "OriginalOrderNo";
	public static readonly String RESTOCKFEE = "RestockFee";
	public static readonly String PICKAFTER = "PickAfter";
	public static readonly String ALLWAITRETURN = "AllWaitReturn";
	public static readonly String FREESHIPPING = "FreeShipping";
	public static readonly String ISSUPPLYLINEORDER = "IsSupplyLineOrder";
	public static readonly String ISPREORDER = "IsPreOrder";

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

	public DateType OrdStart
	{
	    get
	    {
		return this.ordStart;
	    }
	    set
	    {
		this.ordStart = value;
	    }
	}

	public DateType OrdDate
	{
	    get
	    {
		return this.ordDate;
	    }
	    set
	    {
		this.ordDate = value;
	    }
	}

	public IntegerType TimeSpend
	{
	    get
	    {
		return this.timeSpend;
	    }
	    set
	    {
		this.timeSpend = value;
	    }
	}

	public StringType OrdType
	{
	    get
	    {
		return this.ordType;
	    }
	    set
	    {
		this.ordType = value;
	    }
	}

	public StringType OrdStatus
	{
	    get
	    {
		return this.ordStatus;
	    }
	    set
	    {
		this.ordStatus = value;
	    }
	}

	public StringType OrdSource
	{
	    get
	    {
		return this.ordSource;
	    }
	    set
	    {
		this.ordSource = value;
	    }
	}

	public DemoIdType DemoId
	{
	    get
	    {
		return this.demoId;
	    }
	    set
	    {
		this.demoId = value;
	    }
	}

	public StringType HostessId
	{
	    get
	    {
		return this.hostessId;
	    }
	    set
	    {
		this.hostessId = value;
	    }
	}

	public StringType ShipToFname
	{
	    get
	    {
		return this.shipToFname;
	    }
	    set
	    {
		this.shipToFname = value;
	    }
	}

	public StringType ShipToLname
	{
	    get
	    {
		return this.shipToLname;
	    }
	    set
	    {
		this.shipToLname = value;
	    }
	}

	public StringType ShipToAddress1
	{
	    get
	    {
		return this.shipToAddress1;
	    }
	    set
	    {
		this.shipToAddress1 = value;
	    }
	}

	public StringType ShipToAddress2
	{
	    get
	    {
		return this.shipToAddress2;
	    }
	    set
	    {
		this.shipToAddress2 = value;
	    }
	}

	public StringType ShipToCity
	{
	    get
	    {
		return this.shipToCity;
	    }
	    set
	    {
		this.shipToCity = value;
	    }
	}

	public StringType ShipToState
	{
	    get
	    {
		return this.shipToState;
	    }
	    set
	    {
		this.shipToState = value;
	    }
	}

	public StringType ShipToZip
	{
	    get
	    {
		return this.shipToZip;
	    }
	    set
	    {
		this.shipToZip = value;
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

	public StringType OrderNote
	{
	    get
	    {
		return this.orderNote;
	    }
	    set
	    {
		this.orderNote = value;
	    }
	}

	public StringType OperatorId
	{
	    get
	    {
		return this.operatorId;
	    }
	    set
	    {
		this.operatorId = value;
	    }
	}

	public DecimalType OrderSubtotal
	{
	    get
	    {
		return this.orderSubtotal;
	    }
	    set
	    {
		this.orderSubtotal = value;
	    }
	}

	public DecimalType TaxRate
	{
	    get
	    {
		return this.taxRate;
	    }
	    set
	    {
		this.taxRate = value;
	    }
	}

	public DecimalType TaxTotal
	{
	    get
	    {
		return this.taxTotal;
	    }
	    set
	    {
		this.taxTotal = value;
	    }
	}

	public DecimalType OrderTotal
	{
	    get
	    {
		return this.orderTotal;
	    }
	    set
	    {
		this.orderTotal = value;
	    }
	}

	public DecimalType ShippingHandling
	{
	    get
	    {
		return this.shippingHandling;
	    }
	    set
	    {
		this.shippingHandling = value;
	    }
	}

	public DecimalType DiscAmount
	{
	    get
	    {
		return this.discAmount;
	    }
	    set
	    {
		this.discAmount = value;
	    }
	}

	public DecimalType AmountPaid
	{
	    get
	    {
		return this.amountPaid;
	    }
	    set
	    {
		this.amountPaid = value;
	    }
	}

	public DecimalType Balance
	{
	    get
	    {
		return this.balance;
	    }
	    set
	    {
		this.balance = value;
	    }
	}

	public IntegerType Continious
	{
	    get
	    {
		return this.continious;
	    }
	    set
	    {
		this.continious = value;
	    }
	}

	public DateType WorkshopDate
	{
	    get
	    {
		return this.workshopDate;
	    }
	    set
	    {
		this.workshopDate = value;
	    }
	}

	public DecimalType AllowHostFree
	{
	    get
	    {
		return this.allowHostFree;
	    }
	    set
	    {
		this.allowHostFree = value;
	    }
	}

	public DecimalType AllowHostDisc
	{
	    get
	    {
		return this.allowHostDisc;
	    }
	    set
	    {
		this.allowHostDisc = value;
	    }
	}

	public DecimalType TotalHostFree
	{
	    get
	    {
		return this.totalHostFree;
	    }
	    set
	    {
		this.totalHostFree = value;
	    }
	}

	public DecimalType TotalHostDisc
	{
	    get
	    {
		return this.totalHostDisc;
	    }
	    set
	    {
		this.totalHostDisc = value;
	    }
	}

	public DecimalType Difference
	{
	    get
	    {
		return this.difference;
	    }
	    set
	    {
		this.difference = value;
	    }
	}

	public DecimalType HostShipHandl
	{
	    get
	    {
		return this.hostShipHandl;
	    }
	    set
	    {
		this.hostShipHandl = value;
	    }
	}

	public DecimalType HostTax
	{
	    get
	    {
		return this.hostTax;
	    }
	    set
	    {
		this.hostTax = value;
	    }
	}

	public DecimalType HostTotalDue
	{
	    get
	    {
		return this.hostTotalDue;
	    }
	    set
	    {
		this.hostTotalDue = value;
	    }
	}

	public IntegerType GuestsNo
	{
	    get
	    {
		return this.guestsNo;
	    }
	    set
	    {
		this.guestsNo = value;
	    }
	}

	public DateType CommDate
	{
	    get
	    {
		return this.commDate;
	    }
	    set
	    {
		this.commDate = value;
	    }
	}

	public DateType LastShipDate
	{
	    get
	    {
		return this.lastShipDate;
	    }
	    set
	    {
		this.lastShipDate = value;
	    }
	}

	public DecimalType LastShippingNo
	{
	    get
	    {
		return this.lastShippingNo;
	    }
	    set
	    {
		this.lastShippingNo = value;
	    }
	}

	public DateType DateCanceled
	{
	    get
	    {
		return this.dateCanceled;
	    }
	    set
	    {
		this.dateCanceled = value;
	    }
	}

	public IntegerType BackOrderCount
	{
	    get
	    {
		return this.backOrderCount;
	    }
	    set
	    {
		this.backOrderCount = value;
	    }
	}

	public DecimalType NetUnshippedAmt
	{
	    get
	    {
		return this.netUnshippedAmt;
	    }
	    set
	    {
		this.netUnshippedAmt = value;
	    }
	}

	public DecimalType FefundAmt
	{
	    get
	    {
		return this.fefundAmt;
	    }
	    set
	    {
		this.fefundAmt = value;
	    }
	}

	public DecimalType CommAmt
	{
	    get
	    {
		return this.commAmt;
	    }
	    set
	    {
		this.commAmt = value;
	    }
	}

	public IntegerType BatchId
	{
	    get
	    {
		return this.batchId;
	    }
	    set
	    {
		this.batchId = value;
	    }
	}

	public DateType DPCTaxDate
	{
	    get
	    {
		return this.dPCTaxDate;
	    }
	    set
	    {
		this.dPCTaxDate = value;
	    }
	}

	public StringType GeoCode
	{
	    get
	    {
		return this.geoCode;
	    }
	    set
	    {
		this.geoCode = value;
	    }
	}

	public StringType InOut
	{
	    get
	    {
		return this.inOut;
	    }
	    set
	    {
		this.inOut = value;
	    }
	}

	public StringType LocalCode1
	{
	    get
	    {
		return this.localCode1;
	    }
	    set
	    {
		this.localCode1 = value;
	    }
	}

	public StringType LocalCode2
	{
	    get
	    {
		return this.localCode2;
	    }
	    set
	    {
		this.localCode2 = value;
	    }
	}

	public StringType LocalCode3
	{
	    get
	    {
		return this.localCode3;
	    }
	    set
	    {
		this.localCode3 = value;
	    }
	}

	public StringType LocalCode4
	{
	    get
	    {
		return this.localCode4;
	    }
	    set
	    {
		this.localCode4 = value;
	    }
	}

	public StringType LocalCode5
	{
	    get
	    {
		return this.localCode5;
	    }
	    set
	    {
		this.localCode5 = value;
	    }
	}

	public DecimalType HostessTaxableAmount
	{
	    get
	    {
		return this.hostessTaxableAmount;
	    }
	    set
	    {
		this.hostessTaxableAmount = value;
	    }
	}

	public DecimalType TaxableAmount
	{
	    get
	    {
		return this.taxableAmount;
	    }
	    set
	    {
		this.taxableAmount = value;
	    }
	}

	public StringType SendToDemo
	{
	    get
	    {
		return this.sendToDemo;
	    }
	    set
	    {
		this.sendToDemo = value;
	    }
	}

	public DateType FirstSaveDate
	{
	    get
	    {
		return this.firstSaveDate;
	    }
	    set
	    {
		this.firstSaveDate = value;
	    }
	}

	public DateType LastSaveDate
	{
	    get
	    {
		return this.lastSaveDate;
	    }
	    set
	    {
		this.lastSaveDate = value;
	    }
	}

	public StringType ShipToEmail
	{
	    get
	    {
		return this.shipToEmail;
	    }
	    set
	    {
		this.shipToEmail = value;
	    }
	}

	public StringType PromoKit
	{
	    get
	    {
		return this.promoKit;
	    }
	    set
	    {
		this.promoKit = value;
	    }
	}

	public DecimalType ExpShipping
	{
	    get
	    {
		return this.expShipping;
	    }
	    set
	    {
		this.expShipping = value;
	    }
	}

	public IntegerType ShipMeth
	{
	    get
	    {
		return this.shipMeth;
	    }
	    set
	    {
		this.shipMeth = value;
	    }
	}

	public StringType ApoOrder
	{
	    get
	    {
		return this.apoOrder;
	    }
	    set
	    {
		this.apoOrder = value;
	    }
	}

	public DecimalType PstQst
	{
	    get
	    {
		return this.pstQst;
	    }
	    set
	    {
		this.pstQst = value;
	    }
	}

	public DecimalType GstHst
	{
	    get
	    {
		return this.gstHst;
	    }
	    set
	    {
		this.gstHst = value;
	    }
	}

	public DecimalType PstQstRate
	{
	    get
	    {
		return this.pstQstRate;
	    }
	    set
	    {
		this.pstQstRate = value;
	    }
	}

	public DecimalType GstHstRate
	{
	    get
	    {
		return this.gstHstRate;
	    }
	    set
	    {
		this.gstHstRate = value;
	    }
	}

	public DecimalType PstQstTaxableAmount
	{
	    get
	    {
		return this.pstQstTaxableAmount;
	    }
	    set
	    {
		this.pstQstTaxableAmount = value;
	    }
	}

	public DecimalType GstHstTaxableAmount
	{
	    get
	    {
		return this.gstHstTaxableAmount;
	    }
	    set
	    {
		this.gstHstTaxableAmount = value;
	    }
	}

	public DecimalType PstQstHostessTaxableAmount
	{
	    get
	    {
		return this.pstQstHostessTaxableAmount;
	    }
	    set
	    {
		this.pstQstHostessTaxableAmount = value;
	    }
	}

	public DecimalType GstHstHostessTaxableAmount
	{
	    get
	    {
		return this.gstHstHostessTaxableAmount;
	    }
	    set
	    {
		this.gstHstHostessTaxableAmount = value;
	    }
	}

	public DecimalType PstQstHostess
	{
	    get
	    {
		return this.pstQstHostess;
	    }
	    set
	    {
		this.pstQstHostess = value;
	    }
	}

	public DecimalType GstHstHostess
	{
	    get
	    {
		return this.gstHstHostess;
	    }
	    set
	    {
		this.gstHstHostess = value;
	    }
	}

	public CountryEnum CountryOfSale
	{
	    get
	    {
		return this.countryOfSale;
	    }
	    set
	    {
		this.countryOfSale = value;
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

	public StringType OriginalOrderNo
	{
	    get
	    {
		return this.originalOrderNo;
	    }
	    set
	    {
		this.originalOrderNo = value;
	    }
	}

	public DecimalType RestockFee
	{
	    get
	    {
		return this.restockFee;
	    }
	    set
	    {
		this.restockFee = value;
	    }
	}

	public DateType PickAfter
	{
	    get
	    {
		return this.pickAfter;
	    }
	    set
	    {
		this.pickAfter = value;
	    }
	}

	public StringType AllWaitReturn
	{
	    get
	    {
		return this.allWaitReturn;
	    }
	    set
	    {
		this.allWaitReturn = value;
	    }
	}

	public StringType FreeShipping
	{
	    get
	    {
		return this.freeShipping;
	    }
	    set
	    {
		this.freeShipping = value;
	    }
	}

	public BooleanType IsSupplyLineOrder
	{
	    get
	    {
		return this.isSupplyLineOrder;
	    }
	    set
	    {
		this.isSupplyLineOrder = value;
	    }
	}

	public StringType IsPreOrder
	{
	    get
	    {
		return this.isPreOrder;
	    }
	    set
	    {
		this.isPreOrder = value;
	    }
	}
    }
}
