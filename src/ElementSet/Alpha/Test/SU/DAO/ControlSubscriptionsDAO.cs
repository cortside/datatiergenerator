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
    
    
    public class ControlSubscriptionsDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[ControlSubscriptions]";
        
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
        static ControlSubscriptionsDAO()
        {
            propertyToSqlMap.Add("ControlSubscriptionsID",@"ControlSubscriptionsID");
	    propertyToSqlMap.Add("OrgEmployeesID",@"OrgEmployeesID");
	    propertyToSqlMap.Add("OrgGroupsID",@"OrgGroupsID");
	    propertyToSqlMap.Add("ControlsID",@"ControlsID");
	    propertyToSqlMap.Add("SortOrder",@"SortOrder");
	    propertyToSqlMap.Add("AllowUnsubscribe",@"AllowUnsubscribe");
	    propertyToSqlMap.Add("DateSubscribed",@"DateSubscribed");
	    propertyToSqlMap.Add("ControlsClassesID",@"ControlsClassesID");
	    propertyToSqlMap.Add("ItemID",@"ItemID");
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
        /// Returns a list of all ControlSubscriptions rows.
        /// </summary>
        /// <returns>List of ControlSubscriptionsData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static ControlSubscriptionsList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of ControlSubscriptions rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of ControlSubscriptionsData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static ControlSubscriptionsList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of ControlSubscriptions rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of ControlSubscriptionsData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static ControlSubscriptionsList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of ControlSubscriptions rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of ControlSubscriptionsData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static ControlSubscriptionsList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, whereClause, orderByClause, true);
	    ControlSubscriptionsList list = new ControlSubscriptionsList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a ControlSubscriptions entity using it's primary key.
        /// </summary>
        /// <param name="ControlSubscriptionsID">A key field.</param>
        /// <returns>A ControlSubscriptionsData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static ControlSubscriptionsData Load(IdType controlSubscriptionsID)
        {
            WhereClause w = new WhereClause();
	    w.And("ControlSubscriptionsID", controlSubscriptionsID.DBValue);
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for ControlSubscriptions.");
	    }
	    ControlSubscriptionsData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static ControlSubscriptionsData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            ControlSubscriptionsData data = new ControlSubscriptionsData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ControlSubscriptionsID")))
	    {
		data.ControlSubscriptionsID = IdType.UNSET;
	    }
	    else
	    {
		data.ControlSubscriptionsID = new IdType (dataReader.GetInt32(dataReader.GetOrdinal("ControlSubscriptionsID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrgEmployeesID")))
	    {
		data.OrgEmployeesID = IntegerType.UNSET;
	    }
	    else
	    {
		data.OrgEmployeesID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("OrgEmployeesID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrgGroupsID")))
	    {
		data.OrgGroupsID = IntegerType.UNSET;
	    }
	    else
	    {
		data.OrgGroupsID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("OrgGroupsID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ControlsID")))
	    {
		data.ControlsID = IntegerType.UNSET;
	    }
	    else
	    {
		data.ControlsID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("ControlsID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("SortOrder")))
	    {
		data.SortOrder = IntegerType.UNSET;
	    }
	    else
	    {
		data.SortOrder = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("SortOrder")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("AllowUnsubscribe")))
	    {
		data.AllowUnsubscribe = BooleanType.UNSET;
	    }
	    else
	    {
		data.AllowUnsubscribe = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("AllowUnsubscribe")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("DateSubscribed")))
	    {
		data.DateSubscribed = DateType.UNSET;
	    }
	    else
	    {
		data.DateSubscribed = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("DateSubscribed")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ControlsClassesID")))
	    {
		data.ControlsClassesID = IntegerType.UNSET;
	    }
	    else
	    {
		data.ControlsClassesID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("ControlsClassesID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ItemID")))
	    {
		data.ItemID = IntegerType.UNSET;
	    }
	    else
	    {
		data.ItemID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("ItemID")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the ControlSubscriptions table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static IdType Insert(ControlSubscriptionsData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "OrgEmployeesID,"
	    + "OrgGroupsID,"
	    + "ControlsID,"
	    + "SortOrder,"
	    + "AllowUnsubscribe,"
	    + "DateSubscribed,"
	    + "ControlsClassesID,"
	    + "ItemID,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@OrgEmployeesID,"
	    + "@OrgGroupsID,"
	    + "@ControlsID,"
	    + "@SortOrder,"
	    + "@AllowUnsubscribe,"
	    + "@DateSubscribed,"
	    + "@ControlsClassesID,"
	    + "@ItemID,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ");select Scope_Identity() Id";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@OrgEmployeesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgEmployeesID", DataRowVersion.Proposed, data.OrgEmployeesID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrgGroupsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgGroupsID", DataRowVersion.Proposed, data.OrgGroupsID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ControlsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ControlsID", DataRowVersion.Proposed, data.ControlsID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@SortOrder", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "SortOrder", DataRowVersion.Proposed, data.SortOrder.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@AllowUnsubscribe", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "AllowUnsubscribe", DataRowVersion.Proposed, !data.AllowUnsubscribe.IsValid ? data.AllowUnsubscribe.DBValue : data.AllowUnsubscribe.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@DateSubscribed", SqlDbType.SmallDateTime, 0, ParameterDirection.Input, false, 0, 0, "DateSubscribed", DataRowVersion.Proposed, data.DateSubscribed.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ControlsClassesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ControlsClassesID", DataRowVersion.Proposed, data.ControlsClassesID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ItemID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ItemID", DataRowVersion.Proposed, data.ItemID.DBValue));

	    // Execute the query
	    SqlDataReader returnValue = cmd.ExecuteReader();
	    returnValue.Read();
	    int returnId = (int)(returnValue.GetDecimal(0));
	    returnValue.Close();
	    // Set the output paramter value(s)
	    return new IdType (returnId);
        }
        
        /// <summary>
        /// Updates a record in the ControlSubscriptions table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(ControlSubscriptionsData data)
        {
            // Create and execute the command
	    ControlSubscriptionsData oldData = Load ( data.ControlSubscriptionsID);
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.OrgEmployeesID.Equals(data.OrgEmployeesID))
	    {
		sql = sql + "OrgEmployeesID=@OrgEmployeesID,";
	    }
	    if (!oldData.OrgGroupsID.Equals(data.OrgGroupsID))
	    {
		sql = sql + "OrgGroupsID=@OrgGroupsID,";
	    }
	    if (!oldData.ControlsID.Equals(data.ControlsID))
	    {
		sql = sql + "ControlsID=@ControlsID,";
	    }
	    if (!oldData.SortOrder.Equals(data.SortOrder))
	    {
		sql = sql + "SortOrder=@SortOrder,";
	    }
	    if (!oldData.AllowUnsubscribe.Equals(data.AllowUnsubscribe))
	    {
		sql = sql + "AllowUnsubscribe=@AllowUnsubscribe,";
	    }
	    if (!oldData.DateSubscribed.Equals(data.DateSubscribed))
	    {
		sql = sql + "DateSubscribed=@DateSubscribed,";
	    }
	    if (!oldData.ControlsClassesID.Equals(data.ControlsClassesID))
	    {
		sql = sql + "ControlsClassesID=@ControlsClassesID,";
	    }
	    if (!oldData.ItemID.Equals(data.ItemID))
	    {
		sql = sql + "ItemID=@ItemID,";
	    }
	    WhereClause w = new WhereClause();
	    w.And("ControlSubscriptionsID", data.ControlSubscriptionsID.DBValue);
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.ControlSubscriptionsID.Equals(data.ControlSubscriptionsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@ControlSubscriptionsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ControlSubscriptionsID", DataRowVersion.Proposed, data.ControlSubscriptionsID.DBValue));

	    }
	    if (!oldData.OrgEmployeesID.Equals(data.OrgEmployeesID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgEmployeesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgEmployeesID", DataRowVersion.Proposed, data.OrgEmployeesID.DBValue));

	    }
	    if (!oldData.OrgGroupsID.Equals(data.OrgGroupsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgGroupsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgGroupsID", DataRowVersion.Proposed, data.OrgGroupsID.DBValue));

	    }
	    if (!oldData.ControlsID.Equals(data.ControlsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@ControlsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ControlsID", DataRowVersion.Proposed, data.ControlsID.DBValue));

	    }
	    if (!oldData.SortOrder.Equals(data.SortOrder))
	    {
		cmd.Parameters.Add(new SqlParameter("@SortOrder", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "SortOrder", DataRowVersion.Proposed, data.SortOrder.DBValue));

	    }
	    if (!oldData.AllowUnsubscribe.Equals(data.AllowUnsubscribe))
	    {
		cmd.Parameters.Add(new SqlParameter("@AllowUnsubscribe", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "AllowUnsubscribe", DataRowVersion.Proposed, !data.AllowUnsubscribe.IsValid ? data.AllowUnsubscribe.DBValue : data.AllowUnsubscribe.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.DateSubscribed.Equals(data.DateSubscribed))
	    {
		cmd.Parameters.Add(new SqlParameter("@DateSubscribed", SqlDbType.SmallDateTime, 0, ParameterDirection.Input, false, 0, 0, "DateSubscribed", DataRowVersion.Proposed, data.DateSubscribed.DBValue));

	    }
	    if (!oldData.ControlsClassesID.Equals(data.ControlsClassesID))
	    {
		cmd.Parameters.Add(new SqlParameter("@ControlsClassesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ControlsClassesID", DataRowVersion.Proposed, data.ControlsClassesID.DBValue));

	    }
	    if (!oldData.ItemID.Equals(data.ItemID))
	    {
		cmd.Parameters.Add(new SqlParameter("@ItemID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ItemID", DataRowVersion.Proposed, data.ItemID.DBValue));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the ControlSubscriptions table by ControlSubscriptionsID.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Delete(IdType controlSubscriptionsID)
        {
            // Create and execute the command
	    string sql = "Delete From " + TABLE;
	    WhereClause w = new WhereClause();
	    w.And("ControlSubscriptionsID", controlSubscriptionsID.DBValue);
	    sql += w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();
        }
    }
}