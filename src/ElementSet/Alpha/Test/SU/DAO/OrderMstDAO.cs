using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Spring2.Core.DAO;
using Spring2.Core.Types;
using StampinUp.Core.Types;
using StampinUp.DataObject;
using StampinUp.Types;
using Spring2.DataTierGenerator.Attribute;
using StampinUp.Core.DAO;
using StampinUp.Core.Util;

namespace StampinUp.Dao
{
    
    
    public class OrderMstDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[OrderMst]";
        
        [Generate()]
        private static readonly Int32 COMMAND_TIMEOUT = 30;
        
        /// <summary>
        /// Hash table mapping entity property names to sql code.
        /// </summary>
        [Generate()]
        private static Hashtable propertyToSqlMap = new Hashtable();
        
        /// <summary>
        /// Initializes the static map of property names to sql expressions.
        /// </summary>
        static OrderMstDAO()
        {
            propertyToSqlMap.Add("OrderNo",@"OrderNo");
	    propertyToSqlMap.Add("OrdStart",@"OrdStart");
	    propertyToSqlMap.Add("OrdDate",@"OrdDate");
	    propertyToSqlMap.Add("TimeSpend",@"TimeSpend");
	    propertyToSqlMap.Add("OrdType",@"OrdType");
	    propertyToSqlMap.Add("OrdStatus",@"OrdStatus");
	    propertyToSqlMap.Add("OrdSource",@"OrdSource");
	    propertyToSqlMap.Add("DemoId",@"Demo_Id");
	    propertyToSqlMap.Add("HostessId",@"Hostess_Id");
	    propertyToSqlMap.Add("ShipToFname",@"Ship_To_Fname");
	    propertyToSqlMap.Add("ShipToLname",@"Ship_To_Lname");
	    propertyToSqlMap.Add("ShipToAddress1",@"Ship_To_Address1");
	    propertyToSqlMap.Add("ShipToAddress2",@"Ship_To_Address2");
	    propertyToSqlMap.Add("ShipToCity",@"Ship_To_City");
	    propertyToSqlMap.Add("ShipToState",@"Ship_To_State");
	    propertyToSqlMap.Add("ShipToZip",@"Ship_To_Zip");
	    propertyToSqlMap.Add("ShipDate",@"ShipDate");
	    propertyToSqlMap.Add("OrderNote",@"Order_Note");
	    propertyToSqlMap.Add("OperatorId",@"Operator_Id");
	    propertyToSqlMap.Add("OrderSubtotal",@"Order_Subtotal");
	    propertyToSqlMap.Add("TaxRate",@"Tax_Rate");
	    propertyToSqlMap.Add("TaxTotal",@"Tax_Total");
	    propertyToSqlMap.Add("OrderTotal",@"Order_Total");
	    propertyToSqlMap.Add("ShippingHandling",@"Shipping_Handling");
	    propertyToSqlMap.Add("DiscAmount",@"Disc_Amount");
	    propertyToSqlMap.Add("AmountPaid",@"Amount_Paid");
	    propertyToSqlMap.Add("Balance",@"Balance");
	    propertyToSqlMap.Add("Continious",@"Continious");
	    propertyToSqlMap.Add("WorkshopDate",@"Workshop_Date");
	    propertyToSqlMap.Add("AllowHostFree",@"Allow_Host_Free");
	    propertyToSqlMap.Add("AllowHostDisc",@"Allow_Host_Disc");
	    propertyToSqlMap.Add("TotalHostFree",@"Total_Host_Free");
	    propertyToSqlMap.Add("TotalHostDisc",@"Total_Host_Disc");
	    propertyToSqlMap.Add("Difference",@"Difference");
	    propertyToSqlMap.Add("HostShipHandl",@"Host_Ship_Handl");
	    propertyToSqlMap.Add("HostTax",@"Host_Tax");
	    propertyToSqlMap.Add("HostTotalDue",@"Host_Total_Due");
	    propertyToSqlMap.Add("GuestsNo",@"Guests_No");
	    propertyToSqlMap.Add("CommDate",@"Comm_Date");
	    propertyToSqlMap.Add("LastShipDate",@"Last_Ship_Date");
	    propertyToSqlMap.Add("LastShippingNo",@"Last_Shipping_No");
	    propertyToSqlMap.Add("DateCanceled",@"Date_Canceled");
	    propertyToSqlMap.Add("BackOrderCount",@"Back_Order_Count");
	    propertyToSqlMap.Add("NetUnshippedAmt",@"Net_Unshipped_Amt");
	    propertyToSqlMap.Add("FefundAmt",@"Refund_Amt");
	    propertyToSqlMap.Add("CommAmt",@"Comm_Amt");
	    propertyToSqlMap.Add("BatchId",@"Batch_Id");
	    propertyToSqlMap.Add("DPCTaxDate",@"DPCTaxDate");
	    propertyToSqlMap.Add("GeoCode",@"GeoCode");
	    propertyToSqlMap.Add("InOut",@"InOut");
	    propertyToSqlMap.Add("LocalCode1",@"LocalCode1");
	    propertyToSqlMap.Add("LocalCode2",@"LocalCode2");
	    propertyToSqlMap.Add("LocalCode3",@"LocalCode3");
	    propertyToSqlMap.Add("LocalCode4",@"LocalCode4");
	    propertyToSqlMap.Add("LocalCode5",@"LocalCode5");
	    propertyToSqlMap.Add("HostessTaxableAmount",@"HostessTaxableAmount");
	    propertyToSqlMap.Add("TaxableAmount",@"TaxableAmount");
	    propertyToSqlMap.Add("SendToDemo",@"SendToDemo");
	    propertyToSqlMap.Add("FirstSaveDate",@"FirstSaveDate");
	    propertyToSqlMap.Add("LastSaveDate",@"LastSaveDate");
	    propertyToSqlMap.Add("ShipToEmail",@"Ship_To_Email");
	    propertyToSqlMap.Add("PromoKit",@"Promo_Kit");
	    propertyToSqlMap.Add("ExpShipping",@"Exp_Shipping");
	    propertyToSqlMap.Add("ShipMeth",@"ShipMeth");
	    propertyToSqlMap.Add("ApoOrder",@"ApoOrder");
	    propertyToSqlMap.Add("PstQst",@"PST_QST");
	    propertyToSqlMap.Add("GstHst",@"GST_HST");
	    propertyToSqlMap.Add("PstQstRate",@"PST_QST_Rate");
	    propertyToSqlMap.Add("GstHstRate",@"GST_HST_Rate");
	    propertyToSqlMap.Add("PstQstTaxableAmount",@"PST_QST_TaxableAmount");
	    propertyToSqlMap.Add("GstHstTaxableAmount",@"GST_HST_TaxableAmount");
	    propertyToSqlMap.Add("PstQstHostessTaxableAmount",@"PST_QST_HostessTaxableAmount");
	    propertyToSqlMap.Add("GstHstHostessTaxableAmount",@"GST_HST_HostessTaxableAmount");
	    propertyToSqlMap.Add("PstQstHostess",@"PST_QST_Hostess");
	    propertyToSqlMap.Add("GstHstHostess",@"GST_HST_Hostess");
	    propertyToSqlMap.Add("CountryOfSale",@"CountryOfSale");
	    propertyToSqlMap.Add("GLPostedDate",@"GLPostedDate");
	    propertyToSqlMap.Add("OriginalOrderNo",@"OriginalOrderNo");
	    propertyToSqlMap.Add("RestockFee",@"RestockFee");
	    propertyToSqlMap.Add("PickAfter",@"PickAfter");
	    propertyToSqlMap.Add("AllWaitReturn",@"AllWaitReturn");
	    propertyToSqlMap.Add("FreeShipping",@"FreeShipping");
	    propertyToSqlMap.Add("IsSupplyLineOrder",@"IsSupplyLineOrder");
	    propertyToSqlMap.Add("IsPreOrder",@"IsPreOrder");
        }
        
        /// <summary>
        /// Creates a where clause object by mapping the given where clause text.  The text may reference
        /// entity properties which will be mapped to sql code by enclosing the property names in braces.
        /// </summary>
        /// <param name="whereText">Text to be mapped</param>
        /// <returns>WhereClause object.</returns>
        /// <exception cref="ApplicationException">When property name found in braces is not found in the entity.</exception>
        public static IWhere Where(String whereText)
        {
            return new WhereClause(ProcessExpression(propertyToSqlMap, whereText));
        }
        
        /// <summary>
        /// Creates a where clause object that can be used to create sql to find objects whose entity property value
        /// matches the value passed.  Note that the propertyName passed is an entity property name and will be mapped
        /// to the appropriate sql.
        /// </summary>
        /// <param name="propertyName">Entity property to be matched.</param>
        /// <param name="value">Value to match the property with</param>
        /// <returns>A WhereClause object.</returns>
        /// <exception cref="ApplicationException">When the property name passed is not found in the entity.</exception>
        public static IWhere Where(String propertyName, String value)
        {
            return new WhereClause(GetPropertyMapping(propertyToSqlMap, propertyName), value);
        }
        
        /// <summary>
        /// Creates a where clause object that can be used to create sql to find objects whose entity property value
        /// matches the value passed.  Note that the propertyName passed is an entity property name and will be mapped
        /// to the appropriate sql.
        /// </summary>
        /// <param name="propertyName">Entity property to be matched.</param>
        /// <param name="value">Value to match the property with</param>
        /// <returns>A WhereClause object.</returns>
        /// <exception cref="ApplicationException">When the property name passed is not found in the entity.</exception>
        public static IWhere Where(String propertyName, Int32 value)
        {
            return new WhereClause(GetPropertyMapping(propertyToSqlMap, propertyName), value);
        }
        
        /// <summary>
        /// Creates a where clause object that can be used to create sql to find objects whose entity property value
        /// matches the value passed.  Note that the propertyName passed is an entity property name and will be mapped
        /// to the appropriate sql.
        /// </summary>
        /// <param name="propertyName">Entity property to be matched.</param>
        /// <param name="value">Value to match the property with</param>
        /// <returns>A WhereClause object.</returns>
        /// <exception cref="ApplicationException">When the property name passed is not found in the entity.</exception>
        public static IWhere Where(String propertyName, DateTime value)
        {
            return new WhereClause(GetPropertyMapping(propertyToSqlMap, propertyName), value);
        }
        
        /// <summary>
        /// Returns a list of all OrderMst rows.
        /// </summary>
        /// <returns>List of OrderMstData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static OrderMstList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of OrderMst rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of OrderMstData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static OrderMstList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of OrderMst rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of OrderMstData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static OrderMstList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of OrderMst rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of OrderMstData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static OrderMstList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.ORDERDB, TABLE, whereClause, orderByClause, true);
	    OrderMstList list = new OrderMstList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a OrderMst entity using it's primary key.
        /// </summary>
        /// <param name="OrderNo">A key field.</param>
        /// <returns>A OrderMstData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static OrderMstData Load(StringType orderNo)
        {
            WhereClause w = new WhereClause();
	    w.And("OrderNo", orderNo.DBValue);
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.ORDERDB, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for OrderMst.");
	    }
	    OrderMstData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static OrderMstData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            OrderMstData data = new OrderMstData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrderNo")))
	    {
		data.OrderNo = StringType.UNSET;
	    }
	    else
	    {
		data.OrderNo = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("OrderNo")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrdStart")))
	    {
		data.OrdStart = DateType.UNSET;
	    }
	    else
	    {
		data.OrdStart = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("OrdStart")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrdDate")))
	    {
		data.OrdDate = DateType.UNSET;
	    }
	    else
	    {
		data.OrdDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("OrdDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("TimeSpend")))
	    {
		data.TimeSpend = IntegerType.UNSET;
	    }
	    else
	    {
		data.TimeSpend = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("TimeSpend")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrdType")))
	    {
		data.OrdType = StringType.UNSET;
	    }
	    else
	    {
		data.OrdType = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("OrdType")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrdStatus")))
	    {
		data.OrdStatus = StringType.UNSET;
	    }
	    else
	    {
		data.OrdStatus = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("OrdStatus")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrdSource")))
	    {
		data.OrdSource = StringType.UNSET;
	    }
	    else
	    {
		data.OrdSource = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("OrdSource")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Demo_Id")))
	    {
		data.DemoId = DemoIdType.UNSET;
	    }
	    else
	    {
		data.DemoId = DemoIdType.Parse (dataReader.GetString(dataReader.GetOrdinal("Demo_Id")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Hostess_Id")))
	    {
		data.HostessId = StringType.UNSET;
	    }
	    else
	    {
		data.HostessId = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Hostess_Id")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Ship_To_Fname")))
	    {
		data.ShipToFname = StringType.UNSET;
	    }
	    else
	    {
		data.ShipToFname = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Ship_To_Fname")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Ship_To_Lname")))
	    {
		data.ShipToLname = StringType.UNSET;
	    }
	    else
	    {
		data.ShipToLname = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Ship_To_Lname")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Ship_To_Address1")))
	    {
		data.ShipToAddress1 = StringType.UNSET;
	    }
	    else
	    {
		data.ShipToAddress1 = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Ship_To_Address1")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Ship_To_Address2")))
	    {
		data.ShipToAddress2 = StringType.UNSET;
	    }
	    else
	    {
		data.ShipToAddress2 = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Ship_To_Address2")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Ship_To_City")))
	    {
		data.ShipToCity = StringType.UNSET;
	    }
	    else
	    {
		data.ShipToCity = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Ship_To_City")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Ship_To_State")))
	    {
		data.ShipToState = StringType.UNSET;
	    }
	    else
	    {
		data.ShipToState = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Ship_To_State")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Ship_To_Zip")))
	    {
		data.ShipToZip = StringType.UNSET;
	    }
	    else
	    {
		data.ShipToZip = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Ship_To_Zip")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ShipDate")))
	    {
		data.ShipDate = DateType.UNSET;
	    }
	    else
	    {
		data.ShipDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("ShipDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Order_Note")))
	    {
		data.OrderNote = StringType.UNSET;
	    }
	    else
	    {
		data.OrderNote = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Order_Note")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Operator_Id")))
	    {
		data.OperatorId = StringType.UNSET;
	    }
	    else
	    {
		data.OperatorId = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Operator_Id")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Order_Subtotal")))
	    {
		data.OrderSubtotal = DecimalType.UNSET;
	    }
	    else
	    {
		data.OrderSubtotal = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("Order_Subtotal")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Tax_Rate")))
	    {
		data.TaxRate = DecimalType.UNSET;
	    }
	    else
	    {
		data.TaxRate = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("Tax_Rate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Tax_Total")))
	    {
		data.TaxTotal = DecimalType.UNSET;
	    }
	    else
	    {
		data.TaxTotal = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("Tax_Total")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Order_Total")))
	    {
		data.OrderTotal = DecimalType.UNSET;
	    }
	    else
	    {
		data.OrderTotal = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("Order_Total")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Shipping_Handling")))
	    {
		data.ShippingHandling = DecimalType.UNSET;
	    }
	    else
	    {
		data.ShippingHandling = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("Shipping_Handling")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Disc_Amount")))
	    {
		data.DiscAmount = DecimalType.UNSET;
	    }
	    else
	    {
		data.DiscAmount = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("Disc_Amount")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Amount_Paid")))
	    {
		data.AmountPaid = DecimalType.UNSET;
	    }
	    else
	    {
		data.AmountPaid = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("Amount_Paid")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Balance")))
	    {
		data.Balance = DecimalType.UNSET;
	    }
	    else
	    {
		data.Balance = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("Balance")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Continious")))
	    {
		data.Continious = IntegerType.UNSET;
	    }
	    else
	    {
		data.Continious = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("Continious")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Workshop_Date")))
	    {
		data.WorkshopDate = DateType.UNSET;
	    }
	    else
	    {
		data.WorkshopDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("Workshop_Date")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Allow_Host_Free")))
	    {
		data.AllowHostFree = DecimalType.UNSET;
	    }
	    else
	    {
		data.AllowHostFree = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("Allow_Host_Free")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Allow_Host_Disc")))
	    {
		data.AllowHostDisc = DecimalType.UNSET;
	    }
	    else
	    {
		data.AllowHostDisc = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("Allow_Host_Disc")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Total_Host_Free")))
	    {
		data.TotalHostFree = DecimalType.UNSET;
	    }
	    else
	    {
		data.TotalHostFree = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("Total_Host_Free")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Total_Host_Disc")))
	    {
		data.TotalHostDisc = DecimalType.UNSET;
	    }
	    else
	    {
		data.TotalHostDisc = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("Total_Host_Disc")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Difference")))
	    {
		data.Difference = DecimalType.UNSET;
	    }
	    else
	    {
		data.Difference = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("Difference")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Host_Ship_Handl")))
	    {
		data.HostShipHandl = DecimalType.UNSET;
	    }
	    else
	    {
		data.HostShipHandl = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("Host_Ship_Handl")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Host_Tax")))
	    {
		data.HostTax = DecimalType.UNSET;
	    }
	    else
	    {
		data.HostTax = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("Host_Tax")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Host_Total_Due")))
	    {
		data.HostTotalDue = DecimalType.UNSET;
	    }
	    else
	    {
		data.HostTotalDue = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("Host_Total_Due")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Guests_No")))
	    {
		data.GuestsNo = IntegerType.UNSET;
	    }
	    else
	    {
		data.GuestsNo = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("Guests_No")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Comm_Date")))
	    {
		data.CommDate = DateType.UNSET;
	    }
	    else
	    {
		data.CommDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("Comm_Date")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Last_Ship_Date")))
	    {
		data.LastShipDate = DateType.UNSET;
	    }
	    else
	    {
		data.LastShipDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("Last_Ship_Date")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Last_Shipping_No")))
	    {
		data.LastShippingNo = DecimalType.UNSET;
	    }
	    else
	    {
		data.LastShippingNo = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("Last_Shipping_No")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Date_Canceled")))
	    {
		data.DateCanceled = DateType.UNSET;
	    }
	    else
	    {
		data.DateCanceled = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("Date_Canceled")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Back_Order_Count")))
	    {
		data.BackOrderCount = IntegerType.UNSET;
	    }
	    else
	    {
		data.BackOrderCount = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("Back_Order_Count")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Net_Unshipped_Amt")))
	    {
		data.NetUnshippedAmt = DecimalType.UNSET;
	    }
	    else
	    {
		data.NetUnshippedAmt = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("Net_Unshipped_Amt")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Refund_Amt")))
	    {
		data.FefundAmt = DecimalType.UNSET;
	    }
	    else
	    {
		data.FefundAmt = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("Refund_Amt")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Comm_Amt")))
	    {
		data.CommAmt = DecimalType.UNSET;
	    }
	    else
	    {
		data.CommAmt = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("Comm_Amt")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Batch_Id")))
	    {
		data.BatchId = IntegerType.UNSET;
	    }
	    else
	    {
		data.BatchId = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("Batch_Id")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("DPCTaxDate")))
	    {
		data.DPCTaxDate = DateType.UNSET;
	    }
	    else
	    {
		data.DPCTaxDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("DPCTaxDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("GeoCode")))
	    {
		data.GeoCode = StringType.UNSET;
	    }
	    else
	    {
		data.GeoCode = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("GeoCode")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("InOut")))
	    {
		data.InOut = StringType.UNSET;
	    }
	    else
	    {
		data.InOut = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("InOut")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("LocalCode1")))
	    {
		data.LocalCode1 = StringType.UNSET;
	    }
	    else
	    {
		data.LocalCode1 = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("LocalCode1")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("LocalCode2")))
	    {
		data.LocalCode2 = StringType.UNSET;
	    }
	    else
	    {
		data.LocalCode2 = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("LocalCode2")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("LocalCode3")))
	    {
		data.LocalCode3 = StringType.UNSET;
	    }
	    else
	    {
		data.LocalCode3 = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("LocalCode3")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("LocalCode4")))
	    {
		data.LocalCode4 = StringType.UNSET;
	    }
	    else
	    {
		data.LocalCode4 = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("LocalCode4")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("LocalCode5")))
	    {
		data.LocalCode5 = StringType.UNSET;
	    }
	    else
	    {
		data.LocalCode5 = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("LocalCode5")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("HostessTaxableAmount")))
	    {
		data.HostessTaxableAmount = DecimalType.UNSET;
	    }
	    else
	    {
		data.HostessTaxableAmount = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("HostessTaxableAmount")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("TaxableAmount")))
	    {
		data.TaxableAmount = DecimalType.UNSET;
	    }
	    else
	    {
		data.TaxableAmount = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("TaxableAmount")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("SendToDemo")))
	    {
		data.SendToDemo = StringType.UNSET;
	    }
	    else
	    {
		data.SendToDemo = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("SendToDemo")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("FirstSaveDate")))
	    {
		data.FirstSaveDate = DateType.UNSET;
	    }
	    else
	    {
		data.FirstSaveDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("FirstSaveDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("LastSaveDate")))
	    {
		data.LastSaveDate = DateType.UNSET;
	    }
	    else
	    {
		data.LastSaveDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("LastSaveDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Ship_To_Email")))
	    {
		data.ShipToEmail = StringType.UNSET;
	    }
	    else
	    {
		data.ShipToEmail = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Ship_To_Email")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Promo_Kit")))
	    {
		data.PromoKit = StringType.UNSET;
	    }
	    else
	    {
		data.PromoKit = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Promo_Kit")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Exp_Shipping")))
	    {
		data.ExpShipping = DecimalType.UNSET;
	    }
	    else
	    {
		data.ExpShipping = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("Exp_Shipping")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ShipMeth")))
	    {
		data.ShipMeth = IntegerType.UNSET;
	    }
	    else
	    {
		data.ShipMeth = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("ShipMeth")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ApoOrder")))
	    {
		data.ApoOrder = StringType.UNSET;
	    }
	    else
	    {
		data.ApoOrder = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("ApoOrder")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("PST_QST")))
	    {
		data.PstQst = DecimalType.UNSET;
	    }
	    else
	    {
		data.PstQst = new DecimalType (dataReader.GetDecimal(dataReader.GetOrdinal("PST_QST")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("GST_HST")))
	    {
		data.GstHst = DecimalType.UNSET;
	    }
	    else
	    {
		data.GstHst = new DecimalType (dataReader.GetDecimal(dataReader.GetOrdinal("GST_HST")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("PST_QST_Rate")))
	    {
		data.PstQstRate = DecimalType.UNSET;
	    }
	    else
	    {
		data.PstQstRate = new DecimalType (dataReader.GetDecimal(dataReader.GetOrdinal("PST_QST_Rate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("GST_HST_Rate")))
	    {
		data.GstHstRate = DecimalType.UNSET;
	    }
	    else
	    {
		data.GstHstRate = new DecimalType (dataReader.GetDecimal(dataReader.GetOrdinal("GST_HST_Rate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("PST_QST_TaxableAmount")))
	    {
		data.PstQstTaxableAmount = DecimalType.UNSET;
	    }
	    else
	    {
		data.PstQstTaxableAmount = new DecimalType (dataReader.GetDecimal(dataReader.GetOrdinal("PST_QST_TaxableAmount")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("GST_HST_TaxableAmount")))
	    {
		data.GstHstTaxableAmount = DecimalType.UNSET;
	    }
	    else
	    {
		data.GstHstTaxableAmount = new DecimalType (dataReader.GetDecimal(dataReader.GetOrdinal("GST_HST_TaxableAmount")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("PST_QST_HostessTaxableAmount")))
	    {
		data.PstQstHostessTaxableAmount = DecimalType.UNSET;
	    }
	    else
	    {
		data.PstQstHostessTaxableAmount = new DecimalType (dataReader.GetDecimal(dataReader.GetOrdinal("PST_QST_HostessTaxableAmount")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("GST_HST_HostessTaxableAmount")))
	    {
		data.GstHstHostessTaxableAmount = DecimalType.UNSET;
	    }
	    else
	    {
		data.GstHstHostessTaxableAmount = new DecimalType (dataReader.GetDecimal(dataReader.GetOrdinal("GST_HST_HostessTaxableAmount")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("PST_QST_Hostess")))
	    {
		data.PstQstHostess = DecimalType.UNSET;
	    }
	    else
	    {
		data.PstQstHostess = new DecimalType (dataReader.GetDecimal(dataReader.GetOrdinal("PST_QST_Hostess")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("GST_HST_Hostess")))
	    {
		data.GstHstHostess = DecimalType.UNSET;
	    }
	    else
	    {
		data.GstHstHostess = new DecimalType (dataReader.GetDecimal(dataReader.GetOrdinal("GST_HST_Hostess")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("CountryOfSale")))
	    {
		data.CountryOfSale = CountryEnum.UNSET;
	    }
	    else
	    {
		data.CountryOfSale = CountryEnum.GetInstance(dataReader.GetString(dataReader.GetOrdinal("CountryOfSale")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("GLPostedDate")))
	    {
		data.GLPostedDate = DateType.UNSET;
	    }
	    else
	    {
		data.GLPostedDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("GLPostedDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OriginalOrderNo")))
	    {
		data.OriginalOrderNo = StringType.UNSET;
	    }
	    else
	    {
		data.OriginalOrderNo = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("OriginalOrderNo")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("RestockFee")))
	    {
		data.RestockFee = DecimalType.UNSET;
	    }
	    else
	    {
		data.RestockFee = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("RestockFee")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("PickAfter")))
	    {
		data.PickAfter = DateType.UNSET;
	    }
	    else
	    {
		data.PickAfter = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("PickAfter")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("AllWaitReturn")))
	    {
		data.AllWaitReturn = StringType.UNSET;
	    }
	    else
	    {
		data.AllWaitReturn = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("AllWaitReturn")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("FreeShipping")))
	    {
		data.FreeShipping = StringType.UNSET;
	    }
	    else
	    {
		data.FreeShipping = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("FreeShipping")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("IsSupplyLineOrder")))
	    {
		data.IsSupplyLineOrder = BooleanType.UNSET;
	    }
	    else
	    {
		data.IsSupplyLineOrder = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("IsSupplyLineOrder")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("IsPreOrder")))
	    {
		data.IsPreOrder = StringType.UNSET;
	    }
	    else
	    {
		data.IsPreOrder = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("IsPreOrder")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the OrderMst table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Insert(OrderMstData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "OrderNo,"
	    + "OrdStart,"
	    + "OrdDate,"
	    + "TimeSpend,"
	    + "OrdType,"
	    + "OrdStatus,"
	    + "OrdSource,"
	    + "Demo_Id,"
	    + "Hostess_Id,"
	    + "Ship_To_Fname,"
	    + "Ship_To_Lname,"
	    + "Ship_To_Address1,"
	    + "Ship_To_Address2,"
	    + "Ship_To_City,"
	    + "Ship_To_State,"
	    + "Ship_To_Zip,"
	    + "ShipDate,"
	    + "Order_Note,"
	    + "Operator_Id,"
	    + "Order_Subtotal,"
	    + "Tax_Rate,"
	    + "Tax_Total,"
	    + "Order_Total,"
	    + "Shipping_Handling,"
	    + "Disc_Amount,"
	    + "Amount_Paid,"
	    + "Balance,"
	    + "Continious,"
	    + "Workshop_Date,"
	    + "Allow_Host_Free,"
	    + "Allow_Host_Disc,"
	    + "Total_Host_Free,"
	    + "Total_Host_Disc,"
	    + "Difference,"
	    + "Host_Ship_Handl,"
	    + "Host_Tax,"
	    + "Host_Total_Due,"
	    + "Guests_No,"
	    + "Comm_Date,"
	    + "Last_Ship_Date,"
	    + "Last_Shipping_No,"
	    + "Date_Canceled,"
	    + "Back_Order_Count,"
	    + "Net_Unshipped_Amt,"
	    + "Refund_Amt,"
	    + "Comm_Amt,"
	    + "Batch_Id,"
	    + "DPCTaxDate,"
	    + "GeoCode,"
	    + "InOut,"
	    + "LocalCode1,"
	    + "LocalCode2,"
	    + "LocalCode3,"
	    + "LocalCode4,"
	    + "LocalCode5,"
	    + "HostessTaxableAmount,"
	    + "TaxableAmount,"
	    + "SendToDemo,"
	    + "FirstSaveDate,"
	    + "LastSaveDate,"
	    + "Ship_To_Email,"
	    + "Promo_Kit,"
	    + "Exp_Shipping,"
	    + "ShipMeth,"
	    + "ApoOrder,"
	    + "PST_QST,"
	    + "GST_HST,"
	    + "PST_QST_Rate,"
	    + "GST_HST_Rate,"
	    + "PST_QST_TaxableAmount,"
	    + "GST_HST_TaxableAmount,"
	    + "PST_QST_HostessTaxableAmount,"
	    + "GST_HST_HostessTaxableAmount,"
	    + "PST_QST_Hostess,"
	    + "GST_HST_Hostess,"
	    + "CountryOfSale,"
	    + "GLPostedDate,"
	    + "OriginalOrderNo,"
	    + "RestockFee,"
	    + "PickAfter,"
	    + "AllWaitReturn,"
	    + "FreeShipping,"
	    + "IsSupplyLineOrder,"
	    + "IsPreOrder,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@OrderNo,"
	    + "@OrdStart,"
	    + "@OrdDate,"
	    + "@TimeSpend,"
	    + "@OrdType,"
	    + "@OrdStatus,"
	    + "@OrdSource,"
	    + "@Demo_Id,"
	    + "@Hostess_Id,"
	    + "@Ship_To_Fname,"
	    + "@Ship_To_Lname,"
	    + "@Ship_To_Address1,"
	    + "@Ship_To_Address2,"
	    + "@Ship_To_City,"
	    + "@Ship_To_State,"
	    + "@Ship_To_Zip,"
	    + "@ShipDate,"
	    + "@Order_Note,"
	    + "@Operator_Id,"
	    + "@Order_Subtotal,"
	    + "@Tax_Rate,"
	    + "@Tax_Total,"
	    + "@Order_Total,"
	    + "@Shipping_Handling,"
	    + "@Disc_Amount,"
	    + "@Amount_Paid,"
	    + "@Balance,"
	    + "@Continious,"
	    + "@Workshop_Date,"
	    + "@Allow_Host_Free,"
	    + "@Allow_Host_Disc,"
	    + "@Total_Host_Free,"
	    + "@Total_Host_Disc,"
	    + "@Difference,"
	    + "@Host_Ship_Handl,"
	    + "@Host_Tax,"
	    + "@Host_Total_Due,"
	    + "@Guests_No,"
	    + "@Comm_Date,"
	    + "@Last_Ship_Date,"
	    + "@Last_Shipping_No,"
	    + "@Date_Canceled,"
	    + "@Back_Order_Count,"
	    + "@Net_Unshipped_Amt,"
	    + "@Refund_Amt,"
	    + "@Comm_Amt,"
	    + "@Batch_Id,"
	    + "@DPCTaxDate,"
	    + "@GeoCode,"
	    + "@InOut,"
	    + "@LocalCode1,"
	    + "@LocalCode2,"
	    + "@LocalCode3,"
	    + "@LocalCode4,"
	    + "@LocalCode5,"
	    + "@HostessTaxableAmount,"
	    + "@TaxableAmount,"
	    + "@SendToDemo,"
	    + "@FirstSaveDate,"
	    + "@LastSaveDate,"
	    + "@Ship_To_Email,"
	    + "@Promo_Kit,"
	    + "@Exp_Shipping,"
	    + "@ShipMeth,"
	    + "@ApoOrder,"
	    + "@PST_QST,"
	    + "@GST_HST,"
	    + "@PST_QST_Rate,"
	    + "@GST_HST_Rate,"
	    + "@PST_QST_TaxableAmount,"
	    + "@GST_HST_TaxableAmount,"
	    + "@PST_QST_HostessTaxableAmount,"
	    + "@GST_HST_HostessTaxableAmount,"
	    + "@PST_QST_Hostess,"
	    + "@GST_HST_Hostess,"
	    + "@CountryOfSale,"
	    + "@GLPostedDate,"
	    + "@OriginalOrderNo,"
	    + "@RestockFee,"
	    + "@PickAfter,"
	    + "@AllWaitReturn,"
	    + "@FreeShipping,"
	    + "@IsSupplyLineOrder,"
	    + "@IsPreOrder,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ")";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.ORDERDB, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@OrderNo", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "OrderNo", DataRowVersion.Proposed, data.OrderNo.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrdStart", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "OrdStart", DataRowVersion.Proposed, data.OrdStart.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrdDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "OrdDate", DataRowVersion.Proposed, data.OrdDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@TimeSpend", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "TimeSpend", DataRowVersion.Proposed, data.TimeSpend.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrdType", SqlDbType.VarChar, 24, ParameterDirection.Input, false, 0, 0, "OrdType", DataRowVersion.Proposed, data.OrdType.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrdStatus", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "OrdStatus", DataRowVersion.Proposed, data.OrdStatus.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrdSource", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "OrdSource", DataRowVersion.Proposed, data.OrdSource.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Demo_Id", SqlDbType.VarChar, 7, ParameterDirection.Input, false, 0, 0, "DemoId", DataRowVersion.Proposed, data.DemoId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Hostess_Id", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "HostessId", DataRowVersion.Proposed, data.HostessId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Ship_To_Fname", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "ShipToFname", DataRowVersion.Proposed, data.ShipToFname.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Ship_To_Lname", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "ShipToLname", DataRowVersion.Proposed, data.ShipToLname.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Ship_To_Address1", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "ShipToAddress1", DataRowVersion.Proposed, data.ShipToAddress1.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Ship_To_Address2", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "ShipToAddress2", DataRowVersion.Proposed, data.ShipToAddress2.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Ship_To_City", SqlDbType.VarChar, 25, ParameterDirection.Input, false, 0, 0, "ShipToCity", DataRowVersion.Proposed, data.ShipToCity.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Ship_To_State", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "ShipToState", DataRowVersion.Proposed, data.ShipToState.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Ship_To_Zip", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "ShipToZip", DataRowVersion.Proposed, data.ShipToZip.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ShipDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "ShipDate", DataRowVersion.Proposed, data.ShipDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Order_Note", SqlDbType.Text, 0, ParameterDirection.Input, false, 0, 0, "OrderNote", DataRowVersion.Proposed, data.OrderNote.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Operator_Id", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "OperatorId", DataRowVersion.Proposed, data.OperatorId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Order_Subtotal", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "OrderSubtotal", DataRowVersion.Proposed, data.OrderSubtotal.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Tax_Rate", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "TaxRate", DataRowVersion.Proposed, data.TaxRate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Tax_Total", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "TaxTotal", DataRowVersion.Proposed, data.TaxTotal.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Order_Total", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "OrderTotal", DataRowVersion.Proposed, data.OrderTotal.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Shipping_Handling", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "ShippingHandling", DataRowVersion.Proposed, data.ShippingHandling.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Disc_Amount", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "DiscAmount", DataRowVersion.Proposed, data.DiscAmount.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Amount_Paid", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "AmountPaid", DataRowVersion.Proposed, data.AmountPaid.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Balance", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "Balance", DataRowVersion.Proposed, data.Balance.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Continious", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Continious", DataRowVersion.Proposed, data.Continious.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Workshop_Date", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "WorkshopDate", DataRowVersion.Proposed, data.WorkshopDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Allow_Host_Free", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "AllowHostFree", DataRowVersion.Proposed, data.AllowHostFree.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Allow_Host_Disc", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "AllowHostDisc", DataRowVersion.Proposed, data.AllowHostDisc.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Total_Host_Free", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "TotalHostFree", DataRowVersion.Proposed, data.TotalHostFree.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Total_Host_Disc", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "TotalHostDisc", DataRowVersion.Proposed, data.TotalHostDisc.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Difference", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "Difference", DataRowVersion.Proposed, data.Difference.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Host_Ship_Handl", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "HostShipHandl", DataRowVersion.Proposed, data.HostShipHandl.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Host_Tax", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "HostTax", DataRowVersion.Proposed, data.HostTax.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Host_Total_Due", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "HostTotalDue", DataRowVersion.Proposed, data.HostTotalDue.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Guests_No", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "GuestsNo", DataRowVersion.Proposed, data.GuestsNo.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Comm_Date", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "CommDate", DataRowVersion.Proposed, data.CommDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Last_Ship_Date", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "LastShipDate", DataRowVersion.Proposed, data.LastShipDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Last_Shipping_No", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "LastShippingNo", DataRowVersion.Proposed, data.LastShippingNo.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Date_Canceled", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DateCanceled", DataRowVersion.Proposed, data.DateCanceled.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Back_Order_Count", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "BackOrderCount", DataRowVersion.Proposed, data.BackOrderCount.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Net_Unshipped_Amt", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "NetUnshippedAmt", DataRowVersion.Proposed, data.NetUnshippedAmt.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Refund_Amt", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "FefundAmt", DataRowVersion.Proposed, data.FefundAmt.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Comm_Amt", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "CommAmt", DataRowVersion.Proposed, data.CommAmt.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Batch_Id", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "BatchId", DataRowVersion.Proposed, data.BatchId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DPCTaxDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DPCTaxDate", DataRowVersion.Proposed, data.DPCTaxDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@GeoCode", SqlDbType.Char, 10, ParameterDirection.Input, false, 0, 0, "GeoCode", DataRowVersion.Proposed, data.GeoCode.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@InOut", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "InOut", DataRowVersion.Proposed, data.InOut.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@LocalCode1", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "LocalCode1", DataRowVersion.Proposed, data.LocalCode1.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@LocalCode2", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "LocalCode2", DataRowVersion.Proposed, data.LocalCode2.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@LocalCode3", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "LocalCode3", DataRowVersion.Proposed, data.LocalCode3.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@LocalCode4", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "LocalCode4", DataRowVersion.Proposed, data.LocalCode4.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@LocalCode5", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "LocalCode5", DataRowVersion.Proposed, data.LocalCode5.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@HostessTaxableAmount", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "HostessTaxableAmount", DataRowVersion.Proposed, data.HostessTaxableAmount.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@TaxableAmount", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "TaxableAmount", DataRowVersion.Proposed, data.TaxableAmount.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@SendToDemo", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "SendToDemo", DataRowVersion.Proposed, data.SendToDemo.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@FirstSaveDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "FirstSaveDate", DataRowVersion.Proposed, data.FirstSaveDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@LastSaveDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "LastSaveDate", DataRowVersion.Proposed, data.LastSaveDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Ship_To_Email", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "ShipToEmail", DataRowVersion.Proposed, data.ShipToEmail.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Promo_Kit", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "PromoKit", DataRowVersion.Proposed, data.PromoKit.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Exp_Shipping", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "ExpShipping", DataRowVersion.Proposed, data.ExpShipping.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ShipMeth", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ShipMeth", DataRowVersion.Proposed, data.ShipMeth.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ApoOrder", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "ApoOrder", DataRowVersion.Proposed, data.ApoOrder.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@PST_QST", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "PstQst", DataRowVersion.Proposed, data.PstQst.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@GST_HST", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "GstHst", DataRowVersion.Proposed, data.GstHst.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@PST_QST_Rate", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "PstQstRate", DataRowVersion.Proposed, data.PstQstRate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@GST_HST_Rate", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "GstHstRate", DataRowVersion.Proposed, data.GstHstRate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@PST_QST_TaxableAmount", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "PstQstTaxableAmount", DataRowVersion.Proposed, data.PstQstTaxableAmount.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@GST_HST_TaxableAmount", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "GstHstTaxableAmount", DataRowVersion.Proposed, data.GstHstTaxableAmount.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@PST_QST_HostessTaxableAmount", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "PstQstHostessTaxableAmount", DataRowVersion.Proposed, data.PstQstHostessTaxableAmount.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@GST_HST_HostessTaxableAmount", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "GstHstHostessTaxableAmount", DataRowVersion.Proposed, data.GstHstHostessTaxableAmount.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@PST_QST_Hostess", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "PstQstHostess", DataRowVersion.Proposed, data.PstQstHostess.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@GST_HST_Hostess", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "GstHstHostess", DataRowVersion.Proposed, data.GstHstHostess.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@CountryOfSale", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "CountryOfSale", DataRowVersion.Proposed, data.CountryOfSale.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@GLPostedDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "GLPostedDate", DataRowVersion.Proposed, data.GLPostedDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OriginalOrderNo", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "OriginalOrderNo", DataRowVersion.Proposed, data.OriginalOrderNo.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@RestockFee", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "RestockFee", DataRowVersion.Proposed, data.RestockFee.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@PickAfter", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "PickAfter", DataRowVersion.Proposed, data.PickAfter.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@AllWaitReturn", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "AllWaitReturn", DataRowVersion.Proposed, data.AllWaitReturn.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@FreeShipping", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "FreeShipping", DataRowVersion.Proposed, data.FreeShipping.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@IsSupplyLineOrder", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsSupplyLineOrder", DataRowVersion.Proposed, !data.IsSupplyLineOrder.IsValid ? data.IsSupplyLineOrder.DBValue : data.IsSupplyLineOrder.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@IsPreOrder", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "IsPreOrder", DataRowVersion.Proposed, data.IsPreOrder.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Updates a record in the OrderMst table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(OrderMstData data)
        {
            // Create and execute the command
	    OrderMstData oldData = Load ( data.OrderNo);
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.OrderNo.Equals(data.OrderNo))
	    {
		sql = sql + "OrderNo=@OrderNo,";
	    }
	    if (!oldData.OrdStart.Equals(data.OrdStart))
	    {
		sql = sql + "OrdStart=@OrdStart,";
	    }
	    if (!oldData.OrdDate.Equals(data.OrdDate))
	    {
		sql = sql + "OrdDate=@OrdDate,";
	    }
	    if (!oldData.TimeSpend.Equals(data.TimeSpend))
	    {
		sql = sql + "TimeSpend=@TimeSpend,";
	    }
	    if (!oldData.OrdType.Equals(data.OrdType))
	    {
		sql = sql + "OrdType=@OrdType,";
	    }
	    if (!oldData.OrdStatus.Equals(data.OrdStatus))
	    {
		sql = sql + "OrdStatus=@OrdStatus,";
	    }
	    if (!oldData.OrdSource.Equals(data.OrdSource))
	    {
		sql = sql + "OrdSource=@OrdSource,";
	    }
	    if (!oldData.DemoId.Equals(data.DemoId))
	    {
		sql = sql + "Demo_Id=@Demo_Id,";
	    }
	    if (!oldData.HostessId.Equals(data.HostessId))
	    {
		sql = sql + "Hostess_Id=@Hostess_Id,";
	    }
	    if (!oldData.ShipToFname.Equals(data.ShipToFname))
	    {
		sql = sql + "Ship_To_Fname=@Ship_To_Fname,";
	    }
	    if (!oldData.ShipToLname.Equals(data.ShipToLname))
	    {
		sql = sql + "Ship_To_Lname=@Ship_To_Lname,";
	    }
	    if (!oldData.ShipToAddress1.Equals(data.ShipToAddress1))
	    {
		sql = sql + "Ship_To_Address1=@Ship_To_Address1,";
	    }
	    if (!oldData.ShipToAddress2.Equals(data.ShipToAddress2))
	    {
		sql = sql + "Ship_To_Address2=@Ship_To_Address2,";
	    }
	    if (!oldData.ShipToCity.Equals(data.ShipToCity))
	    {
		sql = sql + "Ship_To_City=@Ship_To_City,";
	    }
	    if (!oldData.ShipToState.Equals(data.ShipToState))
	    {
		sql = sql + "Ship_To_State=@Ship_To_State,";
	    }
	    if (!oldData.ShipToZip.Equals(data.ShipToZip))
	    {
		sql = sql + "Ship_To_Zip=@Ship_To_Zip,";
	    }
	    if (!oldData.ShipDate.Equals(data.ShipDate))
	    {
		sql = sql + "ShipDate=@ShipDate,";
	    }
	    if (!oldData.OrderNote.Equals(data.OrderNote))
	    {
		sql = sql + "Order_Note=@Order_Note,";
	    }
	    if (!oldData.OperatorId.Equals(data.OperatorId))
	    {
		sql = sql + "Operator_Id=@Operator_Id,";
	    }
	    if (!oldData.OrderSubtotal.Equals(data.OrderSubtotal))
	    {
		sql = sql + "Order_Subtotal=@Order_Subtotal,";
	    }
	    if (!oldData.TaxRate.Equals(data.TaxRate))
	    {
		sql = sql + "Tax_Rate=@Tax_Rate,";
	    }
	    if (!oldData.TaxTotal.Equals(data.TaxTotal))
	    {
		sql = sql + "Tax_Total=@Tax_Total,";
	    }
	    if (!oldData.OrderTotal.Equals(data.OrderTotal))
	    {
		sql = sql + "Order_Total=@Order_Total,";
	    }
	    if (!oldData.ShippingHandling.Equals(data.ShippingHandling))
	    {
		sql = sql + "Shipping_Handling=@Shipping_Handling,";
	    }
	    if (!oldData.DiscAmount.Equals(data.DiscAmount))
	    {
		sql = sql + "Disc_Amount=@Disc_Amount,";
	    }
	    if (!oldData.AmountPaid.Equals(data.AmountPaid))
	    {
		sql = sql + "Amount_Paid=@Amount_Paid,";
	    }
	    if (!oldData.Balance.Equals(data.Balance))
	    {
		sql = sql + "Balance=@Balance,";
	    }
	    if (!oldData.Continious.Equals(data.Continious))
	    {
		sql = sql + "Continious=@Continious,";
	    }
	    if (!oldData.WorkshopDate.Equals(data.WorkshopDate))
	    {
		sql = sql + "Workshop_Date=@Workshop_Date,";
	    }
	    if (!oldData.AllowHostFree.Equals(data.AllowHostFree))
	    {
		sql = sql + "Allow_Host_Free=@Allow_Host_Free,";
	    }
	    if (!oldData.AllowHostDisc.Equals(data.AllowHostDisc))
	    {
		sql = sql + "Allow_Host_Disc=@Allow_Host_Disc,";
	    }
	    if (!oldData.TotalHostFree.Equals(data.TotalHostFree))
	    {
		sql = sql + "Total_Host_Free=@Total_Host_Free,";
	    }
	    if (!oldData.TotalHostDisc.Equals(data.TotalHostDisc))
	    {
		sql = sql + "Total_Host_Disc=@Total_Host_Disc,";
	    }
	    if (!oldData.Difference.Equals(data.Difference))
	    {
		sql = sql + "Difference=@Difference,";
	    }
	    if (!oldData.HostShipHandl.Equals(data.HostShipHandl))
	    {
		sql = sql + "Host_Ship_Handl=@Host_Ship_Handl,";
	    }
	    if (!oldData.HostTax.Equals(data.HostTax))
	    {
		sql = sql + "Host_Tax=@Host_Tax,";
	    }
	    if (!oldData.HostTotalDue.Equals(data.HostTotalDue))
	    {
		sql = sql + "Host_Total_Due=@Host_Total_Due,";
	    }
	    if (!oldData.GuestsNo.Equals(data.GuestsNo))
	    {
		sql = sql + "Guests_No=@Guests_No,";
	    }
	    if (!oldData.CommDate.Equals(data.CommDate))
	    {
		sql = sql + "Comm_Date=@Comm_Date,";
	    }
	    if (!oldData.LastShipDate.Equals(data.LastShipDate))
	    {
		sql = sql + "Last_Ship_Date=@Last_Ship_Date,";
	    }
	    if (!oldData.LastShippingNo.Equals(data.LastShippingNo))
	    {
		sql = sql + "Last_Shipping_No=@Last_Shipping_No,";
	    }
	    if (!oldData.DateCanceled.Equals(data.DateCanceled))
	    {
		sql = sql + "Date_Canceled=@Date_Canceled,";
	    }
	    if (!oldData.BackOrderCount.Equals(data.BackOrderCount))
	    {
		sql = sql + "Back_Order_Count=@Back_Order_Count,";
	    }
	    if (!oldData.NetUnshippedAmt.Equals(data.NetUnshippedAmt))
	    {
		sql = sql + "Net_Unshipped_Amt=@Net_Unshipped_Amt,";
	    }
	    if (!oldData.FefundAmt.Equals(data.FefundAmt))
	    {
		sql = sql + "Refund_Amt=@Refund_Amt,";
	    }
	    if (!oldData.CommAmt.Equals(data.CommAmt))
	    {
		sql = sql + "Comm_Amt=@Comm_Amt,";
	    }
	    if (!oldData.BatchId.Equals(data.BatchId))
	    {
		sql = sql + "Batch_Id=@Batch_Id,";
	    }
	    if (!oldData.DPCTaxDate.Equals(data.DPCTaxDate))
	    {
		sql = sql + "DPCTaxDate=@DPCTaxDate,";
	    }
	    if (!oldData.GeoCode.Equals(data.GeoCode))
	    {
		sql = sql + "GeoCode=@GeoCode,";
	    }
	    if (!oldData.InOut.Equals(data.InOut))
	    {
		sql = sql + "InOut=@InOut,";
	    }
	    if (!oldData.LocalCode1.Equals(data.LocalCode1))
	    {
		sql = sql + "LocalCode1=@LocalCode1,";
	    }
	    if (!oldData.LocalCode2.Equals(data.LocalCode2))
	    {
		sql = sql + "LocalCode2=@LocalCode2,";
	    }
	    if (!oldData.LocalCode3.Equals(data.LocalCode3))
	    {
		sql = sql + "LocalCode3=@LocalCode3,";
	    }
	    if (!oldData.LocalCode4.Equals(data.LocalCode4))
	    {
		sql = sql + "LocalCode4=@LocalCode4,";
	    }
	    if (!oldData.LocalCode5.Equals(data.LocalCode5))
	    {
		sql = sql + "LocalCode5=@LocalCode5,";
	    }
	    if (!oldData.HostessTaxableAmount.Equals(data.HostessTaxableAmount))
	    {
		sql = sql + "HostessTaxableAmount=@HostessTaxableAmount,";
	    }
	    if (!oldData.TaxableAmount.Equals(data.TaxableAmount))
	    {
		sql = sql + "TaxableAmount=@TaxableAmount,";
	    }
	    if (!oldData.SendToDemo.Equals(data.SendToDemo))
	    {
		sql = sql + "SendToDemo=@SendToDemo,";
	    }
	    if (!oldData.FirstSaveDate.Equals(data.FirstSaveDate))
	    {
		sql = sql + "FirstSaveDate=@FirstSaveDate,";
	    }
	    if (!oldData.LastSaveDate.Equals(data.LastSaveDate))
	    {
		sql = sql + "LastSaveDate=@LastSaveDate,";
	    }
	    if (!oldData.ShipToEmail.Equals(data.ShipToEmail))
	    {
		sql = sql + "Ship_To_Email=@Ship_To_Email,";
	    }
	    if (!oldData.PromoKit.Equals(data.PromoKit))
	    {
		sql = sql + "Promo_Kit=@Promo_Kit,";
	    }
	    if (!oldData.ExpShipping.Equals(data.ExpShipping))
	    {
		sql = sql + "Exp_Shipping=@Exp_Shipping,";
	    }
	    if (!oldData.ShipMeth.Equals(data.ShipMeth))
	    {
		sql = sql + "ShipMeth=@ShipMeth,";
	    }
	    if (!oldData.ApoOrder.Equals(data.ApoOrder))
	    {
		sql = sql + "ApoOrder=@ApoOrder,";
	    }
	    if (!oldData.PstQst.Equals(data.PstQst))
	    {
		sql = sql + "PST_QST=@PST_QST,";
	    }
	    if (!oldData.GstHst.Equals(data.GstHst))
	    {
		sql = sql + "GST_HST=@GST_HST,";
	    }
	    if (!oldData.PstQstRate.Equals(data.PstQstRate))
	    {
		sql = sql + "PST_QST_Rate=@PST_QST_Rate,";
	    }
	    if (!oldData.GstHstRate.Equals(data.GstHstRate))
	    {
		sql = sql + "GST_HST_Rate=@GST_HST_Rate,";
	    }
	    if (!oldData.PstQstTaxableAmount.Equals(data.PstQstTaxableAmount))
	    {
		sql = sql + "PST_QST_TaxableAmount=@PST_QST_TaxableAmount,";
	    }
	    if (!oldData.GstHstTaxableAmount.Equals(data.GstHstTaxableAmount))
	    {
		sql = sql + "GST_HST_TaxableAmount=@GST_HST_TaxableAmount,";
	    }
	    if (!oldData.PstQstHostessTaxableAmount.Equals(data.PstQstHostessTaxableAmount))
	    {
		sql = sql + "PST_QST_HostessTaxableAmount=@PST_QST_HostessTaxableAmount,";
	    }
	    if (!oldData.GstHstHostessTaxableAmount.Equals(data.GstHstHostessTaxableAmount))
	    {
		sql = sql + "GST_HST_HostessTaxableAmount=@GST_HST_HostessTaxableAmount,";
	    }
	    if (!oldData.PstQstHostess.Equals(data.PstQstHostess))
	    {
		sql = sql + "PST_QST_Hostess=@PST_QST_Hostess,";
	    }
	    if (!oldData.GstHstHostess.Equals(data.GstHstHostess))
	    {
		sql = sql + "GST_HST_Hostess=@GST_HST_Hostess,";
	    }
	    if (!oldData.CountryOfSale.Equals(data.CountryOfSale))
	    {
		sql = sql + "CountryOfSale=@CountryOfSale,";
	    }
	    if (!oldData.GLPostedDate.Equals(data.GLPostedDate))
	    {
		sql = sql + "GLPostedDate=@GLPostedDate,";
	    }
	    if (!oldData.OriginalOrderNo.Equals(data.OriginalOrderNo))
	    {
		sql = sql + "OriginalOrderNo=@OriginalOrderNo,";
	    }
	    if (!oldData.RestockFee.Equals(data.RestockFee))
	    {
		sql = sql + "RestockFee=@RestockFee,";
	    }
	    if (!oldData.PickAfter.Equals(data.PickAfter))
	    {
		sql = sql + "PickAfter=@PickAfter,";
	    }
	    if (!oldData.AllWaitReturn.Equals(data.AllWaitReturn))
	    {
		sql = sql + "AllWaitReturn=@AllWaitReturn,";
	    }
	    if (!oldData.FreeShipping.Equals(data.FreeShipping))
	    {
		sql = sql + "FreeShipping=@FreeShipping,";
	    }
	    if (!oldData.IsSupplyLineOrder.Equals(data.IsSupplyLineOrder))
	    {
		sql = sql + "IsSupplyLineOrder=@IsSupplyLineOrder,";
	    }
	    if (!oldData.IsPreOrder.Equals(data.IsPreOrder))
	    {
		sql = sql + "IsPreOrder=@IsPreOrder,";
	    }
	    WhereClause w = new WhereClause();
	    w.And("OrderNo", data.OrderNo.DBValue);
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.ORDERDB, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.OrderNo.Equals(data.OrderNo))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrderNo", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "OrderNo", DataRowVersion.Proposed, data.OrderNo.DBValue));

	    }
	    if (!oldData.OrdStart.Equals(data.OrdStart))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrdStart", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "OrdStart", DataRowVersion.Proposed, data.OrdStart.DBValue));

	    }
	    if (!oldData.OrdDate.Equals(data.OrdDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrdDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "OrdDate", DataRowVersion.Proposed, data.OrdDate.DBValue));

	    }
	    if (!oldData.TimeSpend.Equals(data.TimeSpend))
	    {
		cmd.Parameters.Add(new SqlParameter("@TimeSpend", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "TimeSpend", DataRowVersion.Proposed, data.TimeSpend.DBValue));

	    }
	    if (!oldData.OrdType.Equals(data.OrdType))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrdType", SqlDbType.VarChar, 24, ParameterDirection.Input, false, 0, 0, "OrdType", DataRowVersion.Proposed, data.OrdType.DBValue));

	    }
	    if (!oldData.OrdStatus.Equals(data.OrdStatus))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrdStatus", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "OrdStatus", DataRowVersion.Proposed, data.OrdStatus.DBValue));

	    }
	    if (!oldData.OrdSource.Equals(data.OrdSource))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrdSource", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "OrdSource", DataRowVersion.Proposed, data.OrdSource.DBValue));

	    }
	    if (!oldData.DemoId.Equals(data.DemoId))
	    {
		cmd.Parameters.Add(new SqlParameter("@Demo_Id", SqlDbType.VarChar, 7, ParameterDirection.Input, false, 0, 0, "DemoId", DataRowVersion.Proposed, data.DemoId.DBValue));

	    }
	    if (!oldData.HostessId.Equals(data.HostessId))
	    {
		cmd.Parameters.Add(new SqlParameter("@Hostess_Id", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "HostessId", DataRowVersion.Proposed, data.HostessId.DBValue));

	    }
	    if (!oldData.ShipToFname.Equals(data.ShipToFname))
	    {
		cmd.Parameters.Add(new SqlParameter("@Ship_To_Fname", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "ShipToFname", DataRowVersion.Proposed, data.ShipToFname.DBValue));

	    }
	    if (!oldData.ShipToLname.Equals(data.ShipToLname))
	    {
		cmd.Parameters.Add(new SqlParameter("@Ship_To_Lname", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "ShipToLname", DataRowVersion.Proposed, data.ShipToLname.DBValue));

	    }
	    if (!oldData.ShipToAddress1.Equals(data.ShipToAddress1))
	    {
		cmd.Parameters.Add(new SqlParameter("@Ship_To_Address1", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "ShipToAddress1", DataRowVersion.Proposed, data.ShipToAddress1.DBValue));

	    }
	    if (!oldData.ShipToAddress2.Equals(data.ShipToAddress2))
	    {
		cmd.Parameters.Add(new SqlParameter("@Ship_To_Address2", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "ShipToAddress2", DataRowVersion.Proposed, data.ShipToAddress2.DBValue));

	    }
	    if (!oldData.ShipToCity.Equals(data.ShipToCity))
	    {
		cmd.Parameters.Add(new SqlParameter("@Ship_To_City", SqlDbType.VarChar, 25, ParameterDirection.Input, false, 0, 0, "ShipToCity", DataRowVersion.Proposed, data.ShipToCity.DBValue));

	    }
	    if (!oldData.ShipToState.Equals(data.ShipToState))
	    {
		cmd.Parameters.Add(new SqlParameter("@Ship_To_State", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "ShipToState", DataRowVersion.Proposed, data.ShipToState.DBValue));

	    }
	    if (!oldData.ShipToZip.Equals(data.ShipToZip))
	    {
		cmd.Parameters.Add(new SqlParameter("@Ship_To_Zip", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "ShipToZip", DataRowVersion.Proposed, data.ShipToZip.DBValue));

	    }
	    if (!oldData.ShipDate.Equals(data.ShipDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@ShipDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "ShipDate", DataRowVersion.Proposed, data.ShipDate.DBValue));

	    }
	    if (!oldData.OrderNote.Equals(data.OrderNote))
	    {
		cmd.Parameters.Add(new SqlParameter("@Order_Note", SqlDbType.Text, 0, ParameterDirection.Input, false, 0, 0, "OrderNote", DataRowVersion.Proposed, data.OrderNote.DBValue));

	    }
	    if (!oldData.OperatorId.Equals(data.OperatorId))
	    {
		cmd.Parameters.Add(new SqlParameter("@Operator_Id", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "OperatorId", DataRowVersion.Proposed, data.OperatorId.DBValue));

	    }
	    if (!oldData.OrderSubtotal.Equals(data.OrderSubtotal))
	    {
		cmd.Parameters.Add(new SqlParameter("@Order_Subtotal", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "OrderSubtotal", DataRowVersion.Proposed, data.OrderSubtotal.DBValue));

	    }
	    if (!oldData.TaxRate.Equals(data.TaxRate))
	    {
		cmd.Parameters.Add(new SqlParameter("@Tax_Rate", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "TaxRate", DataRowVersion.Proposed, data.TaxRate.DBValue));

	    }
	    if (!oldData.TaxTotal.Equals(data.TaxTotal))
	    {
		cmd.Parameters.Add(new SqlParameter("@Tax_Total", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "TaxTotal", DataRowVersion.Proposed, data.TaxTotal.DBValue));

	    }
	    if (!oldData.OrderTotal.Equals(data.OrderTotal))
	    {
		cmd.Parameters.Add(new SqlParameter("@Order_Total", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "OrderTotal", DataRowVersion.Proposed, data.OrderTotal.DBValue));

	    }
	    if (!oldData.ShippingHandling.Equals(data.ShippingHandling))
	    {
		cmd.Parameters.Add(new SqlParameter("@Shipping_Handling", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "ShippingHandling", DataRowVersion.Proposed, data.ShippingHandling.DBValue));

	    }
	    if (!oldData.DiscAmount.Equals(data.DiscAmount))
	    {
		cmd.Parameters.Add(new SqlParameter("@Disc_Amount", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "DiscAmount", DataRowVersion.Proposed, data.DiscAmount.DBValue));

	    }
	    if (!oldData.AmountPaid.Equals(data.AmountPaid))
	    {
		cmd.Parameters.Add(new SqlParameter("@Amount_Paid", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "AmountPaid", DataRowVersion.Proposed, data.AmountPaid.DBValue));

	    }
	    if (!oldData.Balance.Equals(data.Balance))
	    {
		cmd.Parameters.Add(new SqlParameter("@Balance", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "Balance", DataRowVersion.Proposed, data.Balance.DBValue));

	    }
	    if (!oldData.Continious.Equals(data.Continious))
	    {
		cmd.Parameters.Add(new SqlParameter("@Continious", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Continious", DataRowVersion.Proposed, data.Continious.DBValue));

	    }
	    if (!oldData.WorkshopDate.Equals(data.WorkshopDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@Workshop_Date", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "WorkshopDate", DataRowVersion.Proposed, data.WorkshopDate.DBValue));

	    }
	    if (!oldData.AllowHostFree.Equals(data.AllowHostFree))
	    {
		cmd.Parameters.Add(new SqlParameter("@Allow_Host_Free", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "AllowHostFree", DataRowVersion.Proposed, data.AllowHostFree.DBValue));

	    }
	    if (!oldData.AllowHostDisc.Equals(data.AllowHostDisc))
	    {
		cmd.Parameters.Add(new SqlParameter("@Allow_Host_Disc", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "AllowHostDisc", DataRowVersion.Proposed, data.AllowHostDisc.DBValue));

	    }
	    if (!oldData.TotalHostFree.Equals(data.TotalHostFree))
	    {
		cmd.Parameters.Add(new SqlParameter("@Total_Host_Free", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "TotalHostFree", DataRowVersion.Proposed, data.TotalHostFree.DBValue));

	    }
	    if (!oldData.TotalHostDisc.Equals(data.TotalHostDisc))
	    {
		cmd.Parameters.Add(new SqlParameter("@Total_Host_Disc", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "TotalHostDisc", DataRowVersion.Proposed, data.TotalHostDisc.DBValue));

	    }
	    if (!oldData.Difference.Equals(data.Difference))
	    {
		cmd.Parameters.Add(new SqlParameter("@Difference", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "Difference", DataRowVersion.Proposed, data.Difference.DBValue));

	    }
	    if (!oldData.HostShipHandl.Equals(data.HostShipHandl))
	    {
		cmd.Parameters.Add(new SqlParameter("@Host_Ship_Handl", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "HostShipHandl", DataRowVersion.Proposed, data.HostShipHandl.DBValue));

	    }
	    if (!oldData.HostTax.Equals(data.HostTax))
	    {
		cmd.Parameters.Add(new SqlParameter("@Host_Tax", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "HostTax", DataRowVersion.Proposed, data.HostTax.DBValue));

	    }
	    if (!oldData.HostTotalDue.Equals(data.HostTotalDue))
	    {
		cmd.Parameters.Add(new SqlParameter("@Host_Total_Due", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "HostTotalDue", DataRowVersion.Proposed, data.HostTotalDue.DBValue));

	    }
	    if (!oldData.GuestsNo.Equals(data.GuestsNo))
	    {
		cmd.Parameters.Add(new SqlParameter("@Guests_No", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "GuestsNo", DataRowVersion.Proposed, data.GuestsNo.DBValue));

	    }
	    if (!oldData.CommDate.Equals(data.CommDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@Comm_Date", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "CommDate", DataRowVersion.Proposed, data.CommDate.DBValue));

	    }
	    if (!oldData.LastShipDate.Equals(data.LastShipDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@Last_Ship_Date", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "LastShipDate", DataRowVersion.Proposed, data.LastShipDate.DBValue));

	    }
	    if (!oldData.LastShippingNo.Equals(data.LastShippingNo))
	    {
		cmd.Parameters.Add(new SqlParameter("@Last_Shipping_No", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "LastShippingNo", DataRowVersion.Proposed, data.LastShippingNo.DBValue));

	    }
	    if (!oldData.DateCanceled.Equals(data.DateCanceled))
	    {
		cmd.Parameters.Add(new SqlParameter("@Date_Canceled", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DateCanceled", DataRowVersion.Proposed, data.DateCanceled.DBValue));

	    }
	    if (!oldData.BackOrderCount.Equals(data.BackOrderCount))
	    {
		cmd.Parameters.Add(new SqlParameter("@Back_Order_Count", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "BackOrderCount", DataRowVersion.Proposed, data.BackOrderCount.DBValue));

	    }
	    if (!oldData.NetUnshippedAmt.Equals(data.NetUnshippedAmt))
	    {
		cmd.Parameters.Add(new SqlParameter("@Net_Unshipped_Amt", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "NetUnshippedAmt", DataRowVersion.Proposed, data.NetUnshippedAmt.DBValue));

	    }
	    if (!oldData.FefundAmt.Equals(data.FefundAmt))
	    {
		cmd.Parameters.Add(new SqlParameter("@Refund_Amt", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "FefundAmt", DataRowVersion.Proposed, data.FefundAmt.DBValue));

	    }
	    if (!oldData.CommAmt.Equals(data.CommAmt))
	    {
		cmd.Parameters.Add(new SqlParameter("@Comm_Amt", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "CommAmt", DataRowVersion.Proposed, data.CommAmt.DBValue));

	    }
	    if (!oldData.BatchId.Equals(data.BatchId))
	    {
		cmd.Parameters.Add(new SqlParameter("@Batch_Id", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "BatchId", DataRowVersion.Proposed, data.BatchId.DBValue));

	    }
	    if (!oldData.DPCTaxDate.Equals(data.DPCTaxDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@DPCTaxDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DPCTaxDate", DataRowVersion.Proposed, data.DPCTaxDate.DBValue));

	    }
	    if (!oldData.GeoCode.Equals(data.GeoCode))
	    {
		cmd.Parameters.Add(new SqlParameter("@GeoCode", SqlDbType.Char, 10, ParameterDirection.Input, false, 0, 0, "GeoCode", DataRowVersion.Proposed, data.GeoCode.DBValue));

	    }
	    if (!oldData.InOut.Equals(data.InOut))
	    {
		cmd.Parameters.Add(new SqlParameter("@InOut", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "InOut", DataRowVersion.Proposed, data.InOut.DBValue));

	    }
	    if (!oldData.LocalCode1.Equals(data.LocalCode1))
	    {
		cmd.Parameters.Add(new SqlParameter("@LocalCode1", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "LocalCode1", DataRowVersion.Proposed, data.LocalCode1.DBValue));

	    }
	    if (!oldData.LocalCode2.Equals(data.LocalCode2))
	    {
		cmd.Parameters.Add(new SqlParameter("@LocalCode2", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "LocalCode2", DataRowVersion.Proposed, data.LocalCode2.DBValue));

	    }
	    if (!oldData.LocalCode3.Equals(data.LocalCode3))
	    {
		cmd.Parameters.Add(new SqlParameter("@LocalCode3", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "LocalCode3", DataRowVersion.Proposed, data.LocalCode3.DBValue));

	    }
	    if (!oldData.LocalCode4.Equals(data.LocalCode4))
	    {
		cmd.Parameters.Add(new SqlParameter("@LocalCode4", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "LocalCode4", DataRowVersion.Proposed, data.LocalCode4.DBValue));

	    }
	    if (!oldData.LocalCode5.Equals(data.LocalCode5))
	    {
		cmd.Parameters.Add(new SqlParameter("@LocalCode5", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "LocalCode5", DataRowVersion.Proposed, data.LocalCode5.DBValue));

	    }
	    if (!oldData.HostessTaxableAmount.Equals(data.HostessTaxableAmount))
	    {
		cmd.Parameters.Add(new SqlParameter("@HostessTaxableAmount", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "HostessTaxableAmount", DataRowVersion.Proposed, data.HostessTaxableAmount.DBValue));

	    }
	    if (!oldData.TaxableAmount.Equals(data.TaxableAmount))
	    {
		cmd.Parameters.Add(new SqlParameter("@TaxableAmount", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "TaxableAmount", DataRowVersion.Proposed, data.TaxableAmount.DBValue));

	    }
	    if (!oldData.SendToDemo.Equals(data.SendToDemo))
	    {
		cmd.Parameters.Add(new SqlParameter("@SendToDemo", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "SendToDemo", DataRowVersion.Proposed, data.SendToDemo.DBValue));

	    }
	    if (!oldData.FirstSaveDate.Equals(data.FirstSaveDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@FirstSaveDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "FirstSaveDate", DataRowVersion.Proposed, data.FirstSaveDate.DBValue));

	    }
	    if (!oldData.LastSaveDate.Equals(data.LastSaveDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@LastSaveDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "LastSaveDate", DataRowVersion.Proposed, data.LastSaveDate.DBValue));

	    }
	    if (!oldData.ShipToEmail.Equals(data.ShipToEmail))
	    {
		cmd.Parameters.Add(new SqlParameter("@Ship_To_Email", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "ShipToEmail", DataRowVersion.Proposed, data.ShipToEmail.DBValue));

	    }
	    if (!oldData.PromoKit.Equals(data.PromoKit))
	    {
		cmd.Parameters.Add(new SqlParameter("@Promo_Kit", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "PromoKit", DataRowVersion.Proposed, data.PromoKit.DBValue));

	    }
	    if (!oldData.ExpShipping.Equals(data.ExpShipping))
	    {
		cmd.Parameters.Add(new SqlParameter("@Exp_Shipping", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "ExpShipping", DataRowVersion.Proposed, data.ExpShipping.DBValue));

	    }
	    if (!oldData.ShipMeth.Equals(data.ShipMeth))
	    {
		cmd.Parameters.Add(new SqlParameter("@ShipMeth", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ShipMeth", DataRowVersion.Proposed, data.ShipMeth.DBValue));

	    }
	    if (!oldData.ApoOrder.Equals(data.ApoOrder))
	    {
		cmd.Parameters.Add(new SqlParameter("@ApoOrder", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "ApoOrder", DataRowVersion.Proposed, data.ApoOrder.DBValue));

	    }
	    if (!oldData.PstQst.Equals(data.PstQst))
	    {
		cmd.Parameters.Add(new SqlParameter("@PST_QST", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "PstQst", DataRowVersion.Proposed, data.PstQst.DBValue));

	    }
	    if (!oldData.GstHst.Equals(data.GstHst))
	    {
		cmd.Parameters.Add(new SqlParameter("@GST_HST", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "GstHst", DataRowVersion.Proposed, data.GstHst.DBValue));

	    }
	    if (!oldData.PstQstRate.Equals(data.PstQstRate))
	    {
		cmd.Parameters.Add(new SqlParameter("@PST_QST_Rate", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "PstQstRate", DataRowVersion.Proposed, data.PstQstRate.DBValue));

	    }
	    if (!oldData.GstHstRate.Equals(data.GstHstRate))
	    {
		cmd.Parameters.Add(new SqlParameter("@GST_HST_Rate", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "GstHstRate", DataRowVersion.Proposed, data.GstHstRate.DBValue));

	    }
	    if (!oldData.PstQstTaxableAmount.Equals(data.PstQstTaxableAmount))
	    {
		cmd.Parameters.Add(new SqlParameter("@PST_QST_TaxableAmount", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "PstQstTaxableAmount", DataRowVersion.Proposed, data.PstQstTaxableAmount.DBValue));

	    }
	    if (!oldData.GstHstTaxableAmount.Equals(data.GstHstTaxableAmount))
	    {
		cmd.Parameters.Add(new SqlParameter("@GST_HST_TaxableAmount", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "GstHstTaxableAmount", DataRowVersion.Proposed, data.GstHstTaxableAmount.DBValue));

	    }
	    if (!oldData.PstQstHostessTaxableAmount.Equals(data.PstQstHostessTaxableAmount))
	    {
		cmd.Parameters.Add(new SqlParameter("@PST_QST_HostessTaxableAmount", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "PstQstHostessTaxableAmount", DataRowVersion.Proposed, data.PstQstHostessTaxableAmount.DBValue));

	    }
	    if (!oldData.GstHstHostessTaxableAmount.Equals(data.GstHstHostessTaxableAmount))
	    {
		cmd.Parameters.Add(new SqlParameter("@GST_HST_HostessTaxableAmount", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "GstHstHostessTaxableAmount", DataRowVersion.Proposed, data.GstHstHostessTaxableAmount.DBValue));

	    }
	    if (!oldData.PstQstHostess.Equals(data.PstQstHostess))
	    {
		cmd.Parameters.Add(new SqlParameter("@PST_QST_Hostess", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "PstQstHostess", DataRowVersion.Proposed, data.PstQstHostess.DBValue));

	    }
	    if (!oldData.GstHstHostess.Equals(data.GstHstHostess))
	    {
		cmd.Parameters.Add(new SqlParameter("@GST_HST_Hostess", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "GstHstHostess", DataRowVersion.Proposed, data.GstHstHostess.DBValue));

	    }
	    if (!oldData.CountryOfSale.Equals(data.CountryOfSale))
	    {
		cmd.Parameters.Add(new SqlParameter("@CountryOfSale", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "CountryOfSale", DataRowVersion.Proposed, data.CountryOfSale.DBValue));

	    }
	    if (!oldData.GLPostedDate.Equals(data.GLPostedDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@GLPostedDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "GLPostedDate", DataRowVersion.Proposed, data.GLPostedDate.DBValue));

	    }
	    if (!oldData.OriginalOrderNo.Equals(data.OriginalOrderNo))
	    {
		cmd.Parameters.Add(new SqlParameter("@OriginalOrderNo", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "OriginalOrderNo", DataRowVersion.Proposed, data.OriginalOrderNo.DBValue));

	    }
	    if (!oldData.RestockFee.Equals(data.RestockFee))
	    {
		cmd.Parameters.Add(new SqlParameter("@RestockFee", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "RestockFee", DataRowVersion.Proposed, data.RestockFee.DBValue));

	    }
	    if (!oldData.PickAfter.Equals(data.PickAfter))
	    {
		cmd.Parameters.Add(new SqlParameter("@PickAfter", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "PickAfter", DataRowVersion.Proposed, data.PickAfter.DBValue));

	    }
	    if (!oldData.AllWaitReturn.Equals(data.AllWaitReturn))
	    {
		cmd.Parameters.Add(new SqlParameter("@AllWaitReturn", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "AllWaitReturn", DataRowVersion.Proposed, data.AllWaitReturn.DBValue));

	    }
	    if (!oldData.FreeShipping.Equals(data.FreeShipping))
	    {
		cmd.Parameters.Add(new SqlParameter("@FreeShipping", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "FreeShipping", DataRowVersion.Proposed, data.FreeShipping.DBValue));

	    }
	    if (!oldData.IsSupplyLineOrder.Equals(data.IsSupplyLineOrder))
	    {
		cmd.Parameters.Add(new SqlParameter("@IsSupplyLineOrder", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsSupplyLineOrder", DataRowVersion.Proposed, !data.IsSupplyLineOrder.IsValid ? data.IsSupplyLineOrder.DBValue : data.IsSupplyLineOrder.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.IsPreOrder.Equals(data.IsPreOrder))
	    {
		cmd.Parameters.Add(new SqlParameter("@IsPreOrder", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "IsPreOrder", DataRowVersion.Proposed, data.IsPreOrder.DBValue));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the OrderMst table by a composite primary key.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Delete(StringType orderNo)
        {
            // Create and execute the command
	    string sql = "Delete From " + TABLE;
	    WhereClause w = new WhereClause();
	    w.And("OrderNo", orderNo.DBValue);
	    sql += w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.ORDERDB, sql, CommandType.Text, COMMAND_TIMEOUT);

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();
        }
    }
}