using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Spring2.Core.DAO;
using Spring2.Core.Types;
using StampinUp.Core.Types;
using StampinUp.DataObject;
using Spring2.DataTierGenerator.Attribute;
using StampinUp.Core.DAO;
using StampinUp.Core.Util;

namespace StampinUp.Dao
{
    
    
    public class SessionInformationDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[SessionInformation]";
        
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
        static SessionInformationDAO()
        {
            propertyToSqlMap.Add("SessionId",@"SessionId");
	    propertyToSqlMap.Add("DemoId",@"DemoId");
	    propertyToSqlMap.Add("UserId",@"UserId");
	    propertyToSqlMap.Add("DateCreated",@"DateCreated");
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
        /// Returns a list of all SessionInformation rows.
        /// </summary>
        /// <returns>List of SessionInformationData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static SessionInformationList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of SessionInformation rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of SessionInformationData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static SessionInformationList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of SessionInformation rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of SessionInformationData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static SessionInformationList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of SessionInformation rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of SessionInformationData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static SessionInformationList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.ORDERDB, TABLE, whereClause, orderByClause, true);
	    SessionInformationList list = new SessionInformationList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a SessionInformation entity using it's primary key.
        /// </summary>
        /// <param name="SessionId">A key field.</param>
        /// <returns>A SessionInformationData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static SessionInformationData Load(IdType sessionId)
        {
            WhereClause w = new WhereClause();
	    w.And("SessionId", sessionId.DBValue);
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.ORDERDB, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for SessionInformation.");
	    }
	    SessionInformationData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static SessionInformationData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            SessionInformationData data = new SessionInformationData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("SessionId")))
	    {
		data.SessionId = IdType.UNSET;
	    }
	    else
	    {
		data.SessionId = new IdType (dataReader.GetInt32(dataReader.GetOrdinal("SessionId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("DemoId")))
	    {
		data.DemoId = DemoIdType.UNSET;
	    }
	    else
	    {
		data.DemoId = DemoIdType.Parse (dataReader.GetString(dataReader.GetOrdinal("DemoId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("UserId")))
	    {
		data.UserId = StringType.UNSET;
	    }
	    else
	    {
		data.UserId = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("UserId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("DateCreated")))
	    {
		data.DateCreated = DateType.UNSET;
	    }
	    else
	    {
		data.DateCreated = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("DateCreated")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the SessionInformation table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static IdType Insert(SessionInformationData data)
        {
            // Create and execute the command
	    data = PrepareForInsert (data);
	    string sql = "Insert Into " + TABLE + "("
	    + "SessionId,"
	    + "DemoId,"
	    + "UserId,"
	    + "DateCreated,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@SessionId,"
	    + "@DemoId,"
	    + "@UserId,"
	    + "@DateCreated,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ")";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.ORDERDB, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@SessionId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "SessionId", DataRowVersion.Proposed, data.SessionId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DemoId", SqlDbType.Char, 7, ParameterDirection.Input, false, 0, 0, "DemoId", DataRowVersion.Proposed, data.DemoId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Char, 15, ParameterDirection.Input, false, 0, 0, "UserId", DataRowVersion.Proposed, data.UserId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DateCreated", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DateCreated", DataRowVersion.Proposed, data.DateCreated.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();
	    // Return field designated
	    return data.SessionId;
        }
        
        /// <summary>
        /// Updates a record in the SessionInformation table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(SessionInformationData data)
        {
            // Create and execute the command
	    SessionInformationData oldData = Load ( data.SessionId);
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.SessionId.Equals(data.SessionId))
	    {
		sql = sql + "SessionId=@SessionId,";
	    }
	    if (!oldData.DemoId.Equals(data.DemoId))
	    {
		sql = sql + "DemoId=@DemoId,";
	    }
	    if (!oldData.UserId.Equals(data.UserId))
	    {
		sql = sql + "UserId=@UserId,";
	    }
	    if (!oldData.DateCreated.Equals(data.DateCreated))
	    {
		sql = sql + "DateCreated=@DateCreated,";
	    }
	    WhereClause w = new WhereClause();
	    w.And("SessionId", data.SessionId.DBValue);
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.ORDERDB, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.SessionId.Equals(data.SessionId))
	    {
		cmd.Parameters.Add(new SqlParameter("@SessionId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "SessionId", DataRowVersion.Proposed, data.SessionId.DBValue));

	    }
	    if (!oldData.DemoId.Equals(data.DemoId))
	    {
		cmd.Parameters.Add(new SqlParameter("@DemoId", SqlDbType.Char, 7, ParameterDirection.Input, false, 0, 0, "DemoId", DataRowVersion.Proposed, data.DemoId.DBValue));

	    }
	    if (!oldData.UserId.Equals(data.UserId))
	    {
		cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Char, 15, ParameterDirection.Input, false, 0, 0, "UserId", DataRowVersion.Proposed, data.UserId.DBValue));

	    }
	    if (!oldData.DateCreated.Equals(data.DateCreated))
	    {
		cmd.Parameters.Add(new SqlParameter("@DateCreated", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DateCreated", DataRowVersion.Proposed, data.DateCreated.DBValue));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the SessionInformation table by a composite primary key.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Delete(IdType sessionId)
        {
            // Create and execute the command
	    string sql = "Delete From " + TABLE;
	    WhereClause w = new WhereClause();
	    w.And("SessionId", sessionId.DBValue);
	    sql += w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.ORDERDB, sql, CommandType.Text, COMMAND_TIMEOUT);

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();
        }
        
        private static SessionInformationData PrepareForInsert(SessionInformationData data)
        {
            return data;
        }
    }
}