using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Spring2.Core.DAO;
using Spring2.Core.Types;
using StampinUp.DataObject;
using Spring2.DataTierGenerator.Attribute;
using StampinUp.Core.DAO;
using StampinUp.Core.Types;
using StampinUp.Core.Util;

namespace StampinUp.Dao
{
    
    
    public class OrderDtlDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[OrderDtl]";
        
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
        static OrderDtlDAO()
        {
            propertyToSqlMap.Add("OrderDetailId",@"Order_Detail_Id");
	    propertyToSqlMap.Add("OrderNo",@"Orderno");
	    propertyToSqlMap.Add("TypeOrd",@"Type_Ord");
	    propertyToSqlMap.Add("ItemId",@"Item_Id");
	    propertyToSqlMap.Add("ItemDescrip",@"Item_Descrip");
	    propertyToSqlMap.Add("BinNo",@"Bin_No");
	    propertyToSqlMap.Add("QtyOrdered",@"Qty_Ordered");
	    propertyToSqlMap.Add("QtyPicked",@"Qty_Picked");
	    propertyToSqlMap.Add("QtyShipped",@"Qty_Shipped");
	    propertyToSqlMap.Add("ShippingNo",@"Shipping_No");
	    propertyToSqlMap.Add("ItemPrice",@"Item_Price");
	    propertyToSqlMap.Add("Subtotal",@"Subtotal");
	    propertyToSqlMap.Add("Personal",@"Personal");
	    propertyToSqlMap.Add("TaxExempt",@"Tax_Exempt");
	    propertyToSqlMap.Add("TaxExemptNo",@"Tax_Exempt_No");
	    propertyToSqlMap.Add("ShipDate",@"Ship_Date");
	    propertyToSqlMap.Add("Kit",@"Kit");
	    propertyToSqlMap.Add("GuestIdOld",@"Guest_Id_Old");
	    propertyToSqlMap.Add("ComponentId",@"Component_Id");
	    propertyToSqlMap.Add("GuestId",@"Guest_Id");
	    propertyToSqlMap.Add("Xxxsave",@"Xxxsave");
	    propertyToSqlMap.Add("StartkitCategory",@"Startkit_Category");
	    propertyToSqlMap.Add("GLPostedDate",@"GLPostedDate");
	    propertyToSqlMap.Add("Revenue",@"Revenue");
	    propertyToSqlMap.Add("DiscountAmount",@"DiscountAmount");
	    propertyToSqlMap.Add("QtyCanceled",@"QtyCanceled");
	    propertyToSqlMap.Add("CanceledDate",@"CanceledDate");
	    propertyToSqlMap.Add("ChargeForItem",@"ChargeForItem");
	    propertyToSqlMap.Add("ErrorCode",@"ErrorCode");
	    propertyToSqlMap.Add("ErrorDepartment",@"ErrorDepartment");
	    propertyToSqlMap.Add("WaitReturnBeginDate",@"WaitReturnBeginDate");
	    propertyToSqlMap.Add("WaitReturnDueDate",@"WaitReturnDueDate");
	    propertyToSqlMap.Add("WaitReturnReceivedDate",@"WaitReturnReceivedDate");
	    propertyToSqlMap.Add("ItemNotes",@"ItemNotes");
	    propertyToSqlMap.Add("SaveAsShipped",@"SaveAsShipped");
	    propertyToSqlMap.Add("PurchasedDate",@"PurchasedDate");
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
        /// Returns a list of all OrderDtl rows.
        /// </summary>
        /// <returns>List of OrderDtlData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static OrderDtlList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of OrderDtl rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of OrderDtlData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static OrderDtlList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of OrderDtl rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of OrderDtlData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static OrderDtlList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of OrderDtl rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of OrderDtlData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static OrderDtlList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.ORDERDB, TABLE, whereClause, orderByClause, true);
	    OrderDtlList list = new OrderDtlList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a OrderDtl entity using it's primary key.
        /// </summary>
        /// <param name="Order_Detail_Id">A key field.</param>
        /// <returns>A OrderDtlData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static OrderDtlData Load(StringType orderDetailId)
        {
            WhereClause w = new WhereClause();
	    w.And("Order_Detail_Id", orderDetailId.DBValue);
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.ORDERDB, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for OrderDtl.");
	    }
	    OrderDtlData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static OrderDtlData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            OrderDtlData data = new OrderDtlData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Order_Detail_Id")))
	    {
		data.OrderDetailId = StringType.UNSET;
	    }
	    else
	    {
		data.OrderDetailId = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Order_Detail_Id")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Orderno")))
	    {
		data.OrderNo = StringType.UNSET;
	    }
	    else
	    {
		data.OrderNo = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Orderno")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Type_Ord")))
	    {
		data.TypeOrd = StringType.UNSET;
	    }
	    else
	    {
		data.TypeOrd = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Type_Ord")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Item_Id")))
	    {
		data.ItemId = StringType.UNSET;
	    }
	    else
	    {
		data.ItemId = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Item_Id")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Item_Descrip")))
	    {
		data.ItemDescrip = StringType.UNSET;
	    }
	    else
	    {
		data.ItemDescrip = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Item_Descrip")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Bin_No")))
	    {
		data.BinNo = StringType.UNSET;
	    }
	    else
	    {
		data.BinNo = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Bin_No")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Qty_Ordered")))
	    {
		data.QtyOrdered = IntegerType.UNSET;
	    }
	    else
	    {
		data.QtyOrdered = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("Qty_Ordered")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Qty_Picked")))
	    {
		data.QtyPicked = IntegerType.UNSET;
	    }
	    else
	    {
		data.QtyPicked = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("Qty_Picked")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Qty_Shipped")))
	    {
		data.QtyShipped = IntegerType.UNSET;
	    }
	    else
	    {
		data.QtyShipped = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("Qty_Shipped")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Shipping_No")))
	    {
		data.ShippingNo = DecimalType.UNSET;
	    }
	    else
	    {
		data.ShippingNo = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("Shipping_No")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Item_Price")))
	    {
		data.ItemPrice = DecimalType.UNSET;
	    }
	    else
	    {
		data.ItemPrice = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("Item_Price")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Subtotal")))
	    {
		data.Subtotal = DecimalType.UNSET;
	    }
	    else
	    {
		data.Subtotal = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("Subtotal")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Personal")))
	    {
		data.Personal = BooleanType.UNSET;
	    }
	    else
	    {
		data.Personal = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("Personal")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Tax_Exempt")))
	    {
		data.TaxExempt = BooleanType.UNSET;
	    }
	    else
	    {
		data.TaxExempt = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("Tax_Exempt")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Tax_Exempt_No")))
	    {
		data.TaxExemptNo = StringType.UNSET;
	    }
	    else
	    {
		data.TaxExemptNo = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Tax_Exempt_No")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Ship_Date")))
	    {
		data.ShipDate = DateType.UNSET;
	    }
	    else
	    {
		data.ShipDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("Ship_Date")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Kit")))
	    {
		data.Kit = IntegerType.UNSET;
	    }
	    else
	    {
		data.Kit = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("Kit")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Guest_Id_Old")))
	    {
		data.GuestIdOld = IntegerType.UNSET;
	    }
	    else
	    {
		data.GuestIdOld = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("Guest_Id_Old")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Component_Id")))
	    {
		data.ComponentId = StringType.UNSET;
	    }
	    else
	    {
		data.ComponentId = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Component_Id")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Guest_Id")))
	    {
		data.GuestId = StringType.UNSET;
	    }
	    else
	    {
		data.GuestId = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Guest_Id")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Xxxsave")))
	    {
		data.Xxxsave = IntegerType.UNSET;
	    }
	    else
	    {
		data.Xxxsave = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("Xxxsave")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Startkit_Category")))
	    {
		data.StartkitCategory = IntegerType.UNSET;
	    }
	    else
	    {
		data.StartkitCategory = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("Startkit_Category")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("GLPostedDate")))
	    {
		data.GLPostedDate = DateType.UNSET;
	    }
	    else
	    {
		data.GLPostedDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("GLPostedDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Revenue")))
	    {
		data.Revenue = DecimalType.UNSET;
	    }
	    else
	    {
		data.Revenue = new DecimalType (dataReader.GetDecimal(dataReader.GetOrdinal("Revenue")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("DiscountAmount")))
	    {
		data.DiscountAmount = DecimalType.UNSET;
	    }
	    else
	    {
		data.DiscountAmount = new DecimalType (dataReader.GetDecimal(dataReader.GetOrdinal("DiscountAmount")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("QtyCanceled")))
	    {
		data.QtyCanceled = IntegerType.UNSET;
	    }
	    else
	    {
		data.QtyCanceled = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("QtyCanceled")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("CanceledDate")))
	    {
		data.CanceledDate = DateType.UNSET;
	    }
	    else
	    {
		data.CanceledDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("CanceledDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ChargeForItem")))
	    {
		data.ChargeForItem = BooleanType.UNSET;
	    }
	    else
	    {
		data.ChargeForItem = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("ChargeForItem")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ErrorCode")))
	    {
		data.ErrorCode = StringType.UNSET;
	    }
	    else
	    {
		data.ErrorCode = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("ErrorCode")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ErrorDepartment")))
	    {
		data.ErrorDepartment = StringType.UNSET;
	    }
	    else
	    {
		data.ErrorDepartment = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("ErrorDepartment")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("WaitReturnBeginDate")))
	    {
		data.WaitReturnBeginDate = DateType.UNSET;
	    }
	    else
	    {
		data.WaitReturnBeginDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("WaitReturnBeginDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("WaitReturnDueDate")))
	    {
		data.WaitReturnDueDate = DateType.UNSET;
	    }
	    else
	    {
		data.WaitReturnDueDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("WaitReturnDueDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("WaitReturnReceivedDate")))
	    {
		data.WaitReturnReceivedDate = DateType.UNSET;
	    }
	    else
	    {
		data.WaitReturnReceivedDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("WaitReturnReceivedDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ItemNotes")))
	    {
		data.ItemNotes = StringType.UNSET;
	    }
	    else
	    {
		data.ItemNotes = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("ItemNotes")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("SaveAsShipped")))
	    {
		data.SaveAsShipped = BooleanType.UNSET;
	    }
	    else
	    {
		data.SaveAsShipped = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("SaveAsShipped")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("PurchasedDate")))
	    {
		data.PurchasedDate = DateType.UNSET;
	    }
	    else
	    {
		data.PurchasedDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("PurchasedDate")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the OrderDtl table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Insert(OrderDtlData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "Order_Detail_Id,"
	    + "Orderno,"
	    + "Type_Ord,"
	    + "Item_Id,"
	    + "Item_Descrip,"
	    + "Bin_No,"
	    + "Qty_Ordered,"
	    + "Qty_Picked,"
	    + "Qty_Shipped,"
	    + "Shipping_No,"
	    + "Item_Price,"
	    + "Subtotal,"
	    + "Personal,"
	    + "Tax_Exempt,"
	    + "Tax_Exempt_No,"
	    + "Ship_Date,"
	    + "Kit,"
	    + "Guest_Id_Old,"
	    + "Component_Id,"
	    + "Guest_Id,"
	    + "Xxxsave,"
	    + "Startkit_Category,"
	    + "GLPostedDate,"
	    + "Revenue,"
	    + "DiscountAmount,"
	    + "QtyCanceled,"
	    + "CanceledDate,"
	    + "ChargeForItem,"
	    + "ErrorCode,"
	    + "ErrorDepartment,"
	    + "WaitReturnBeginDate,"
	    + "WaitReturnDueDate,"
	    + "WaitReturnReceivedDate,"
	    + "ItemNotes,"
	    + "SaveAsShipped,"
	    + "PurchasedDate,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@Order_Detail_Id,"
	    + "@Orderno,"
	    + "@Type_Ord,"
	    + "@Item_Id,"
	    + "@Item_Descrip,"
	    + "@Bin_No,"
	    + "@Qty_Ordered,"
	    + "@Qty_Picked,"
	    + "@Qty_Shipped,"
	    + "@Shipping_No,"
	    + "@Item_Price,"
	    + "@Subtotal,"
	    + "@Personal,"
	    + "@Tax_Exempt,"
	    + "@Tax_Exempt_No,"
	    + "@Ship_Date,"
	    + "@Kit,"
	    + "@Guest_Id_Old,"
	    + "@Component_Id,"
	    + "@Guest_Id,"
	    + "@Xxxsave,"
	    + "@Startkit_Category,"
	    + "@GLPostedDate,"
	    + "@Revenue,"
	    + "@DiscountAmount,"
	    + "@QtyCanceled,"
	    + "@CanceledDate,"
	    + "@ChargeForItem,"
	    + "@ErrorCode,"
	    + "@ErrorDepartment,"
	    + "@WaitReturnBeginDate,"
	    + "@WaitReturnDueDate,"
	    + "@WaitReturnReceivedDate,"
	    + "@ItemNotes,"
	    + "@SaveAsShipped,"
	    + "@PurchasedDate,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ")";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.ORDERDB, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@Order_Detail_Id", SqlDbType.VarChar, 11, ParameterDirection.Input, false, 0, 0, "OrderDetailId", DataRowVersion.Proposed, data.OrderDetailId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Orderno", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "OrderNo", DataRowVersion.Proposed, data.OrderNo.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Type_Ord", SqlDbType.VarChar, 4, ParameterDirection.Input, false, 0, 0, "TypeOrd", DataRowVersion.Proposed, data.TypeOrd.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Item_Id", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "ItemId", DataRowVersion.Proposed, data.ItemId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Item_Descrip", SqlDbType.VarChar, 60, ParameterDirection.Input, false, 0, 0, "ItemDescrip", DataRowVersion.Proposed, data.ItemDescrip.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Bin_No", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "BinNo", DataRowVersion.Proposed, data.BinNo.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Qty_Ordered", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "QtyOrdered", DataRowVersion.Proposed, data.QtyOrdered.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Qty_Picked", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "QtyPicked", DataRowVersion.Proposed, data.QtyPicked.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Qty_Shipped", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "QtyShipped", DataRowVersion.Proposed, data.QtyShipped.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Shipping_No", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "ShippingNo", DataRowVersion.Proposed, data.ShippingNo.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Item_Price", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "ItemPrice", DataRowVersion.Proposed, data.ItemPrice.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Subtotal", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "Subtotal", DataRowVersion.Proposed, data.Subtotal.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Personal", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "Personal", DataRowVersion.Proposed, !data.Personal.IsValid ? data.Personal.DBValue : data.Personal.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@Tax_Exempt", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "TaxExempt", DataRowVersion.Proposed, !data.TaxExempt.IsValid ? data.TaxExempt.DBValue : data.TaxExempt.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@Tax_Exempt_No", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "TaxExemptNo", DataRowVersion.Proposed, data.TaxExemptNo.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Ship_Date", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "ShipDate", DataRowVersion.Proposed, data.ShipDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Kit", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Kit", DataRowVersion.Proposed, data.Kit.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Guest_Id_Old", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "GuestIdOld", DataRowVersion.Proposed, data.GuestIdOld.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Component_Id", SqlDbType.Char, 11, ParameterDirection.Input, false, 0, 0, "ComponentId", DataRowVersion.Proposed, data.ComponentId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Guest_Id", SqlDbType.VarChar, 7, ParameterDirection.Input, false, 0, 0, "GuestId", DataRowVersion.Proposed, data.GuestId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Xxxsave", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Xxxsave", DataRowVersion.Proposed, data.Xxxsave.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Startkit_Category", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "StartkitCategory", DataRowVersion.Proposed, data.StartkitCategory.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@GLPostedDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "GLPostedDate", DataRowVersion.Proposed, data.GLPostedDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Revenue", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "Revenue", DataRowVersion.Proposed, data.Revenue.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DiscountAmount", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "DiscountAmount", DataRowVersion.Proposed, data.DiscountAmount.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@QtyCanceled", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "QtyCanceled", DataRowVersion.Proposed, data.QtyCanceled.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@CanceledDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "CanceledDate", DataRowVersion.Proposed, data.CanceledDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ChargeForItem", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "ChargeForItem", DataRowVersion.Proposed, !data.ChargeForItem.IsValid ? data.ChargeForItem.DBValue : data.ChargeForItem.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.VarChar, 4, ParameterDirection.Input, false, 0, 0, "ErrorCode", DataRowVersion.Proposed, data.ErrorCode.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ErrorDepartment", SqlDbType.VarChar, 3, ParameterDirection.Input, false, 0, 0, "ErrorDepartment", DataRowVersion.Proposed, data.ErrorDepartment.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@WaitReturnBeginDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "WaitReturnBeginDate", DataRowVersion.Proposed, data.WaitReturnBeginDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@WaitReturnDueDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "WaitReturnDueDate", DataRowVersion.Proposed, data.WaitReturnDueDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@WaitReturnReceivedDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "WaitReturnReceivedDate", DataRowVersion.Proposed, data.WaitReturnReceivedDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ItemNotes", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "ItemNotes", DataRowVersion.Proposed, data.ItemNotes.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@SaveAsShipped", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "SaveAsShipped", DataRowVersion.Proposed, !data.SaveAsShipped.IsValid ? data.SaveAsShipped.DBValue : data.SaveAsShipped.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@PurchasedDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "PurchasedDate", DataRowVersion.Proposed, data.PurchasedDate.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Updates a record in the OrderDtl table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(OrderDtlData data)
        {
            // Create and execute the command
	    OrderDtlData oldData = Load ( data.OrderDetailId);
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.OrderDetailId.Equals(data.OrderDetailId))
	    {
		sql = sql + "Order_Detail_Id=@Order_Detail_Id,";
	    }
	    if (!oldData.OrderNo.Equals(data.OrderNo))
	    {
		sql = sql + "Orderno=@Orderno,";
	    }
	    if (!oldData.TypeOrd.Equals(data.TypeOrd))
	    {
		sql = sql + "Type_Ord=@Type_Ord,";
	    }
	    if (!oldData.ItemId.Equals(data.ItemId))
	    {
		sql = sql + "Item_Id=@Item_Id,";
	    }
	    if (!oldData.ItemDescrip.Equals(data.ItemDescrip))
	    {
		sql = sql + "Item_Descrip=@Item_Descrip,";
	    }
	    if (!oldData.BinNo.Equals(data.BinNo))
	    {
		sql = sql + "Bin_No=@Bin_No,";
	    }
	    if (!oldData.QtyOrdered.Equals(data.QtyOrdered))
	    {
		sql = sql + "Qty_Ordered=@Qty_Ordered,";
	    }
	    if (!oldData.QtyPicked.Equals(data.QtyPicked))
	    {
		sql = sql + "Qty_Picked=@Qty_Picked,";
	    }
	    if (!oldData.QtyShipped.Equals(data.QtyShipped))
	    {
		sql = sql + "Qty_Shipped=@Qty_Shipped,";
	    }
	    if (!oldData.ShippingNo.Equals(data.ShippingNo))
	    {
		sql = sql + "Shipping_No=@Shipping_No,";
	    }
	    if (!oldData.ItemPrice.Equals(data.ItemPrice))
	    {
		sql = sql + "Item_Price=@Item_Price,";
	    }
	    if (!oldData.Subtotal.Equals(data.Subtotal))
	    {
		sql = sql + "Subtotal=@Subtotal,";
	    }
	    if (!oldData.Personal.Equals(data.Personal))
	    {
		sql = sql + "Personal=@Personal,";
	    }
	    if (!oldData.TaxExempt.Equals(data.TaxExempt))
	    {
		sql = sql + "Tax_Exempt=@Tax_Exempt,";
	    }
	    if (!oldData.TaxExemptNo.Equals(data.TaxExemptNo))
	    {
		sql = sql + "Tax_Exempt_No=@Tax_Exempt_No,";
	    }
	    if (!oldData.ShipDate.Equals(data.ShipDate))
	    {
		sql = sql + "Ship_Date=@Ship_Date,";
	    }
	    if (!oldData.Kit.Equals(data.Kit))
	    {
		sql = sql + "Kit=@Kit,";
	    }
	    if (!oldData.GuestIdOld.Equals(data.GuestIdOld))
	    {
		sql = sql + "Guest_Id_Old=@Guest_Id_Old,";
	    }
	    if (!oldData.ComponentId.Equals(data.ComponentId))
	    {
		sql = sql + "Component_Id=@Component_Id,";
	    }
	    if (!oldData.GuestId.Equals(data.GuestId))
	    {
		sql = sql + "Guest_Id=@Guest_Id,";
	    }
	    if (!oldData.Xxxsave.Equals(data.Xxxsave))
	    {
		sql = sql + "Xxxsave=@Xxxsave,";
	    }
	    if (!oldData.StartkitCategory.Equals(data.StartkitCategory))
	    {
		sql = sql + "Startkit_Category=@Startkit_Category,";
	    }
	    if (!oldData.GLPostedDate.Equals(data.GLPostedDate))
	    {
		sql = sql + "GLPostedDate=@GLPostedDate,";
	    }
	    if (!oldData.Revenue.Equals(data.Revenue))
	    {
		sql = sql + "Revenue=@Revenue,";
	    }
	    if (!oldData.DiscountAmount.Equals(data.DiscountAmount))
	    {
		sql = sql + "DiscountAmount=@DiscountAmount,";
	    }
	    if (!oldData.QtyCanceled.Equals(data.QtyCanceled))
	    {
		sql = sql + "QtyCanceled=@QtyCanceled,";
	    }
	    if (!oldData.CanceledDate.Equals(data.CanceledDate))
	    {
		sql = sql + "CanceledDate=@CanceledDate,";
	    }
	    if (!oldData.ChargeForItem.Equals(data.ChargeForItem))
	    {
		sql = sql + "ChargeForItem=@ChargeForItem,";
	    }
	    if (!oldData.ErrorCode.Equals(data.ErrorCode))
	    {
		sql = sql + "ErrorCode=@ErrorCode,";
	    }
	    if (!oldData.ErrorDepartment.Equals(data.ErrorDepartment))
	    {
		sql = sql + "ErrorDepartment=@ErrorDepartment,";
	    }
	    if (!oldData.WaitReturnBeginDate.Equals(data.WaitReturnBeginDate))
	    {
		sql = sql + "WaitReturnBeginDate=@WaitReturnBeginDate,";
	    }
	    if (!oldData.WaitReturnDueDate.Equals(data.WaitReturnDueDate))
	    {
		sql = sql + "WaitReturnDueDate=@WaitReturnDueDate,";
	    }
	    if (!oldData.WaitReturnReceivedDate.Equals(data.WaitReturnReceivedDate))
	    {
		sql = sql + "WaitReturnReceivedDate=@WaitReturnReceivedDate,";
	    }
	    if (!oldData.ItemNotes.Equals(data.ItemNotes))
	    {
		sql = sql + "ItemNotes=@ItemNotes,";
	    }
	    if (!oldData.SaveAsShipped.Equals(data.SaveAsShipped))
	    {
		sql = sql + "SaveAsShipped=@SaveAsShipped,";
	    }
	    if (!oldData.PurchasedDate.Equals(data.PurchasedDate))
	    {
		sql = sql + "PurchasedDate=@PurchasedDate,";
	    }
	    WhereClause w = new WhereClause();
	    w.And("Order_Detail_Id", data.OrderDetailId.DBValue);
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.ORDERDB, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.OrderDetailId.Equals(data.OrderDetailId))
	    {
		cmd.Parameters.Add(new SqlParameter("@Order_Detail_Id", SqlDbType.VarChar, 11, ParameterDirection.Input, false, 0, 0, "OrderDetailId", DataRowVersion.Proposed, data.OrderDetailId.DBValue));

	    }
	    if (!oldData.OrderNo.Equals(data.OrderNo))
	    {
		cmd.Parameters.Add(new SqlParameter("@Orderno", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "OrderNo", DataRowVersion.Proposed, data.OrderNo.DBValue));

	    }
	    if (!oldData.TypeOrd.Equals(data.TypeOrd))
	    {
		cmd.Parameters.Add(new SqlParameter("@Type_Ord", SqlDbType.VarChar, 4, ParameterDirection.Input, false, 0, 0, "TypeOrd", DataRowVersion.Proposed, data.TypeOrd.DBValue));

	    }
	    if (!oldData.ItemId.Equals(data.ItemId))
	    {
		cmd.Parameters.Add(new SqlParameter("@Item_Id", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "ItemId", DataRowVersion.Proposed, data.ItemId.DBValue));

	    }
	    if (!oldData.ItemDescrip.Equals(data.ItemDescrip))
	    {
		cmd.Parameters.Add(new SqlParameter("@Item_Descrip", SqlDbType.VarChar, 60, ParameterDirection.Input, false, 0, 0, "ItemDescrip", DataRowVersion.Proposed, data.ItemDescrip.DBValue));

	    }
	    if (!oldData.BinNo.Equals(data.BinNo))
	    {
		cmd.Parameters.Add(new SqlParameter("@Bin_No", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "BinNo", DataRowVersion.Proposed, data.BinNo.DBValue));

	    }
	    if (!oldData.QtyOrdered.Equals(data.QtyOrdered))
	    {
		cmd.Parameters.Add(new SqlParameter("@Qty_Ordered", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "QtyOrdered", DataRowVersion.Proposed, data.QtyOrdered.DBValue));

	    }
	    if (!oldData.QtyPicked.Equals(data.QtyPicked))
	    {
		cmd.Parameters.Add(new SqlParameter("@Qty_Picked", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "QtyPicked", DataRowVersion.Proposed, data.QtyPicked.DBValue));

	    }
	    if (!oldData.QtyShipped.Equals(data.QtyShipped))
	    {
		cmd.Parameters.Add(new SqlParameter("@Qty_Shipped", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "QtyShipped", DataRowVersion.Proposed, data.QtyShipped.DBValue));

	    }
	    if (!oldData.ShippingNo.Equals(data.ShippingNo))
	    {
		cmd.Parameters.Add(new SqlParameter("@Shipping_No", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "ShippingNo", DataRowVersion.Proposed, data.ShippingNo.DBValue));

	    }
	    if (!oldData.ItemPrice.Equals(data.ItemPrice))
	    {
		cmd.Parameters.Add(new SqlParameter("@Item_Price", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "ItemPrice", DataRowVersion.Proposed, data.ItemPrice.DBValue));

	    }
	    if (!oldData.Subtotal.Equals(data.Subtotal))
	    {
		cmd.Parameters.Add(new SqlParameter("@Subtotal", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "Subtotal", DataRowVersion.Proposed, data.Subtotal.DBValue));

	    }
	    if (!oldData.Personal.Equals(data.Personal))
	    {
		cmd.Parameters.Add(new SqlParameter("@Personal", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "Personal", DataRowVersion.Proposed, !data.Personal.IsValid ? data.Personal.DBValue : data.Personal.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.TaxExempt.Equals(data.TaxExempt))
	    {
		cmd.Parameters.Add(new SqlParameter("@Tax_Exempt", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "TaxExempt", DataRowVersion.Proposed, !data.TaxExempt.IsValid ? data.TaxExempt.DBValue : data.TaxExempt.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.TaxExemptNo.Equals(data.TaxExemptNo))
	    {
		cmd.Parameters.Add(new SqlParameter("@Tax_Exempt_No", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "TaxExemptNo", DataRowVersion.Proposed, data.TaxExemptNo.DBValue));

	    }
	    if (!oldData.ShipDate.Equals(data.ShipDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@Ship_Date", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "ShipDate", DataRowVersion.Proposed, data.ShipDate.DBValue));

	    }
	    if (!oldData.Kit.Equals(data.Kit))
	    {
		cmd.Parameters.Add(new SqlParameter("@Kit", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Kit", DataRowVersion.Proposed, data.Kit.DBValue));

	    }
	    if (!oldData.GuestIdOld.Equals(data.GuestIdOld))
	    {
		cmd.Parameters.Add(new SqlParameter("@Guest_Id_Old", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "GuestIdOld", DataRowVersion.Proposed, data.GuestIdOld.DBValue));

	    }
	    if (!oldData.ComponentId.Equals(data.ComponentId))
	    {
		cmd.Parameters.Add(new SqlParameter("@Component_Id", SqlDbType.Char, 11, ParameterDirection.Input, false, 0, 0, "ComponentId", DataRowVersion.Proposed, data.ComponentId.DBValue));

	    }
	    if (!oldData.GuestId.Equals(data.GuestId))
	    {
		cmd.Parameters.Add(new SqlParameter("@Guest_Id", SqlDbType.VarChar, 7, ParameterDirection.Input, false, 0, 0, "GuestId", DataRowVersion.Proposed, data.GuestId.DBValue));

	    }
	    if (!oldData.Xxxsave.Equals(data.Xxxsave))
	    {
		cmd.Parameters.Add(new SqlParameter("@Xxxsave", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Xxxsave", DataRowVersion.Proposed, data.Xxxsave.DBValue));

	    }
	    if (!oldData.StartkitCategory.Equals(data.StartkitCategory))
	    {
		cmd.Parameters.Add(new SqlParameter("@Startkit_Category", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "StartkitCategory", DataRowVersion.Proposed, data.StartkitCategory.DBValue));

	    }
	    if (!oldData.GLPostedDate.Equals(data.GLPostedDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@GLPostedDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "GLPostedDate", DataRowVersion.Proposed, data.GLPostedDate.DBValue));

	    }
	    if (!oldData.Revenue.Equals(data.Revenue))
	    {
		cmd.Parameters.Add(new SqlParameter("@Revenue", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "Revenue", DataRowVersion.Proposed, data.Revenue.DBValue));

	    }
	    if (!oldData.DiscountAmount.Equals(data.DiscountAmount))
	    {
		cmd.Parameters.Add(new SqlParameter("@DiscountAmount", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "DiscountAmount", DataRowVersion.Proposed, data.DiscountAmount.DBValue));

	    }
	    if (!oldData.QtyCanceled.Equals(data.QtyCanceled))
	    {
		cmd.Parameters.Add(new SqlParameter("@QtyCanceled", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "QtyCanceled", DataRowVersion.Proposed, data.QtyCanceled.DBValue));

	    }
	    if (!oldData.CanceledDate.Equals(data.CanceledDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@CanceledDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "CanceledDate", DataRowVersion.Proposed, data.CanceledDate.DBValue));

	    }
	    if (!oldData.ChargeForItem.Equals(data.ChargeForItem))
	    {
		cmd.Parameters.Add(new SqlParameter("@ChargeForItem", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "ChargeForItem", DataRowVersion.Proposed, !data.ChargeForItem.IsValid ? data.ChargeForItem.DBValue : data.ChargeForItem.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.ErrorCode.Equals(data.ErrorCode))
	    {
		cmd.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.VarChar, 4, ParameterDirection.Input, false, 0, 0, "ErrorCode", DataRowVersion.Proposed, data.ErrorCode.DBValue));

	    }
	    if (!oldData.ErrorDepartment.Equals(data.ErrorDepartment))
	    {
		cmd.Parameters.Add(new SqlParameter("@ErrorDepartment", SqlDbType.VarChar, 3, ParameterDirection.Input, false, 0, 0, "ErrorDepartment", DataRowVersion.Proposed, data.ErrorDepartment.DBValue));

	    }
	    if (!oldData.WaitReturnBeginDate.Equals(data.WaitReturnBeginDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@WaitReturnBeginDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "WaitReturnBeginDate", DataRowVersion.Proposed, data.WaitReturnBeginDate.DBValue));

	    }
	    if (!oldData.WaitReturnDueDate.Equals(data.WaitReturnDueDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@WaitReturnDueDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "WaitReturnDueDate", DataRowVersion.Proposed, data.WaitReturnDueDate.DBValue));

	    }
	    if (!oldData.WaitReturnReceivedDate.Equals(data.WaitReturnReceivedDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@WaitReturnReceivedDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "WaitReturnReceivedDate", DataRowVersion.Proposed, data.WaitReturnReceivedDate.DBValue));

	    }
	    if (!oldData.ItemNotes.Equals(data.ItemNotes))
	    {
		cmd.Parameters.Add(new SqlParameter("@ItemNotes", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "ItemNotes", DataRowVersion.Proposed, data.ItemNotes.DBValue));

	    }
	    if (!oldData.SaveAsShipped.Equals(data.SaveAsShipped))
	    {
		cmd.Parameters.Add(new SqlParameter("@SaveAsShipped", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "SaveAsShipped", DataRowVersion.Proposed, !data.SaveAsShipped.IsValid ? data.SaveAsShipped.DBValue : data.SaveAsShipped.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.PurchasedDate.Equals(data.PurchasedDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@PurchasedDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "PurchasedDate", DataRowVersion.Proposed, data.PurchasedDate.DBValue));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the OrderDtl table by a composite primary key.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Delete(StringType orderDetailId)
        {
            // Create and execute the command
	    string sql = "Delete From " + TABLE;
	    WhereClause w = new WhereClause();
	    w.And("Order_Detail_Id", orderDetailId.DBValue);
	    sql += w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.ORDERDB, sql, CommandType.Text, COMMAND_TIMEOUT);

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Returns a list of objects which match the values for the fields specified.
        /// </summary>
        /// <param name="Orderno">A field value to be matched.</param>
        /// <returns>The list of OrderDtlDAO objects found.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static OrderDtlList FindByOrderNo(StringType orderNo)
        {
            OrderByClause sort = new OrderByClause("Orderno");
	    WhereClause filter = new WhereClause();
	    filter.And("Orderno", orderNo.DBValue);

	    return GetList(filter, sort);
        }
    }
}