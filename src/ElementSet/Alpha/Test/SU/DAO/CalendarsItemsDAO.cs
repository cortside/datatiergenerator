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
    
    
    public class CalendarsItemsDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[CalendarsItems]";
        
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
        static CalendarsItemsDAO()
        {
            propertyToSqlMap.Add("CalendarsItemsID",@"CalendarsItemsID");
	    propertyToSqlMap.Add("Description",@"Description");
	    propertyToSqlMap.Add("Summary",@"Summary");
	    propertyToSqlMap.Add("EventDateStart",@"EventDateStart");
	    propertyToSqlMap.Add("EventDateEnd",@"EventDateEnd");
	    propertyToSqlMap.Add("IsActive",@"IsActive");
	    propertyToSqlMap.Add("CalendarsID",@"CalendarsID");
	    propertyToSqlMap.Add("IsPublic",@"IsPublic");
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
        /// Returns a list of all CalendarsItems rows.
        /// </summary>
        /// <returns>List of CalendarsItemsData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static CalendarsItemsList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of CalendarsItems rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of CalendarsItemsData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static CalendarsItemsList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of CalendarsItems rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of CalendarsItemsData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static CalendarsItemsList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of CalendarsItems rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of CalendarsItemsData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static CalendarsItemsList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, whereClause, orderByClause, true);
	    CalendarsItemsList list = new CalendarsItemsList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a CalendarsItems entity using it's primary key.
        /// </summary>
        /// <param name="CalendarsItemsID">A key field.</param>
        /// <returns>A CalendarsItemsData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static CalendarsItemsData Load(IdType calendarsItemsID)
        {
            WhereClause w = new WhereClause();
	    w.And("CalendarsItemsID", calendarsItemsID.DBValue);
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for CalendarsItems.");
	    }
	    CalendarsItemsData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static CalendarsItemsData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            CalendarsItemsData data = new CalendarsItemsData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("CalendarsItemsID")))
	    {
		data.CalendarsItemsID = IdType.UNSET;
	    }
	    else
	    {
		data.CalendarsItemsID = new IdType (dataReader.GetInt32(dataReader.GetOrdinal("CalendarsItemsID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Description")))
	    {
		data.Description = StringType.UNSET;
	    }
	    else
	    {
		data.Description = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Description")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Summary")))
	    {
		data.Summary = StringType.UNSET;
	    }
	    else
	    {
		data.Summary = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Summary")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("EventDateStart")))
	    {
		data.EventDateStart = DateType.UNSET;
	    }
	    else
	    {
		data.EventDateStart = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("EventDateStart")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("EventDateEnd")))
	    {
		data.EventDateEnd = DateType.UNSET;
	    }
	    else
	    {
		data.EventDateEnd = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("EventDateEnd")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("IsActive")))
	    {
		data.IsActive = BooleanType.UNSET;
	    }
	    else
	    {
		data.IsActive = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("IsActive")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("CalendarsID")))
	    {
		data.CalendarsID = IntegerType.UNSET;
	    }
	    else
	    {
		data.CalendarsID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("CalendarsID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("IsPublic")))
	    {
		data.IsPublic = BooleanType.UNSET;
	    }
	    else
	    {
		data.IsPublic = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("IsPublic")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the CalendarsItems table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static IdType Insert(CalendarsItemsData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "Description,"
	    + "Summary,"
	    + "EventDateStart,"
	    + "EventDateEnd,"
	    + "IsActive,"
	    + "CalendarsID,"
	    + "IsPublic,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@Description,"
	    + "@Summary,"
	    + "@EventDateStart,"
	    + "@EventDateEnd,"
	    + "@IsActive,"
	    + "@CalendarsID,"
	    + "@IsPublic,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ");select Scope_Identity() Id";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Summary", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Summary", DataRowVersion.Proposed, data.Summary.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@EventDateStart", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "EventDateStart", DataRowVersion.Proposed, data.EventDateStart.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@EventDateEnd", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "EventDateEnd", DataRowVersion.Proposed, data.EventDateEnd.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsActive", DataRowVersion.Proposed, !data.IsActive.IsValid ? data.IsActive.DBValue : data.IsActive.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@CalendarsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "CalendarsID", DataRowVersion.Proposed, data.CalendarsID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@IsPublic", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsPublic", DataRowVersion.Proposed, !data.IsPublic.IsValid ? data.IsPublic.DBValue : data.IsPublic.DBValue.Equals ("Y") ? 1 : 0));

	    // Execute the query
	    SqlDataReader returnValue = cmd.ExecuteReader();
	    returnValue.Read();
	    int returnId = (int)(returnValue.GetDecimal(0));
	    returnValue.Close();
	    // Set the output paramter value(s)
	    return new IdType (returnId);
        }
        
        /// <summary>
        /// Updates a record in the CalendarsItems table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(CalendarsItemsData data)
        {
            // Create and execute the command
	    CalendarsItemsData oldData = Load ( data.CalendarsItemsID);
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.Description.Equals(data.Description))
	    {
		sql = sql + "Description=@Description,";
	    }
	    if (!oldData.Summary.Equals(data.Summary))
	    {
		sql = sql + "Summary=@Summary,";
	    }
	    if (!oldData.EventDateStart.Equals(data.EventDateStart))
	    {
		sql = sql + "EventDateStart=@EventDateStart,";
	    }
	    if (!oldData.EventDateEnd.Equals(data.EventDateEnd))
	    {
		sql = sql + "EventDateEnd=@EventDateEnd,";
	    }
	    if (!oldData.IsActive.Equals(data.IsActive))
	    {
		sql = sql + "IsActive=@IsActive,";
	    }
	    if (!oldData.CalendarsID.Equals(data.CalendarsID))
	    {
		sql = sql + "CalendarsID=@CalendarsID,";
	    }
	    if (!oldData.IsPublic.Equals(data.IsPublic))
	    {
		sql = sql + "IsPublic=@IsPublic,";
	    }
	    WhereClause w = new WhereClause();
	    w.And("CalendarsItemsID", data.CalendarsItemsID.DBValue);
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.CalendarsItemsID.Equals(data.CalendarsItemsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@CalendarsItemsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "CalendarsItemsID", DataRowVersion.Proposed, data.CalendarsItemsID.DBValue));

	    }
	    if (!oldData.Description.Equals(data.Description))
	    {
		cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));

	    }
	    if (!oldData.Summary.Equals(data.Summary))
	    {
		cmd.Parameters.Add(new SqlParameter("@Summary", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Summary", DataRowVersion.Proposed, data.Summary.DBValue));

	    }
	    if (!oldData.EventDateStart.Equals(data.EventDateStart))
	    {
		cmd.Parameters.Add(new SqlParameter("@EventDateStart", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "EventDateStart", DataRowVersion.Proposed, data.EventDateStart.DBValue));

	    }
	    if (!oldData.EventDateEnd.Equals(data.EventDateEnd))
	    {
		cmd.Parameters.Add(new SqlParameter("@EventDateEnd", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "EventDateEnd", DataRowVersion.Proposed, data.EventDateEnd.DBValue));

	    }
	    if (!oldData.IsActive.Equals(data.IsActive))
	    {
		cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsActive", DataRowVersion.Proposed, !data.IsActive.IsValid ? data.IsActive.DBValue : data.IsActive.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.CalendarsID.Equals(data.CalendarsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@CalendarsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "CalendarsID", DataRowVersion.Proposed, data.CalendarsID.DBValue));

	    }
	    if (!oldData.IsPublic.Equals(data.IsPublic))
	    {
		cmd.Parameters.Add(new SqlParameter("@IsPublic", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsPublic", DataRowVersion.Proposed, !data.IsPublic.IsValid ? data.IsPublic.DBValue : data.IsPublic.DBValue.Equals ("Y") ? 1 : 0));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the CalendarsItems table by CalendarsItemsID.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Delete(IdType calendarsItemsID)
        {
            // Create and execute the command
	    string sql = "Delete From " + TABLE;
	    WhereClause w = new WhereClause();
	    w.And("CalendarsItemsID", calendarsItemsID.DBValue);
	    sql += w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();
        }
    }
}