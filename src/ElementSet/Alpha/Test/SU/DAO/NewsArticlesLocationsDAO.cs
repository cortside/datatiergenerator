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
    
    
    public class NewsArticlesLocationsDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[NewsArticlesLocations]";
        
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
        static NewsArticlesLocationsDAO()
        {
            propertyToSqlMap.Add("NewsArticlesLocationsID",@"NewsArticlesLocationsID");
	    propertyToSqlMap.Add("NewsArticlesID",@"NewsArticlesID");
	    propertyToSqlMap.Add("OrgLocationsID",@"OrgLocationsID");
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
        /// Returns a list of all NewsArticlesLocations rows.
        /// </summary>
        /// <returns>List of NewsArticlesLocationsData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static NewsArticlesLocationsList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of NewsArticlesLocations rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of NewsArticlesLocationsData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static NewsArticlesLocationsList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of NewsArticlesLocations rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of NewsArticlesLocationsData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static NewsArticlesLocationsList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of NewsArticlesLocations rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of NewsArticlesLocationsData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static NewsArticlesLocationsList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, whereClause, orderByClause, true);
	    NewsArticlesLocationsList list = new NewsArticlesLocationsList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a NewsArticlesLocations entity using it's primary key.
        /// </summary>
        /// <param name="NewsArticlesLocationsID">A key field.</param>
        /// <returns>A NewsArticlesLocationsData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static NewsArticlesLocationsData Load(IdType newsArticlesLocationsID)
        {
            WhereClause w = new WhereClause();
	    w.And("NewsArticlesLocationsID", newsArticlesLocationsID.DBValue);
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for NewsArticlesLocations.");
	    }
	    NewsArticlesLocationsData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static NewsArticlesLocationsData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            NewsArticlesLocationsData data = new NewsArticlesLocationsData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("NewsArticlesLocationsID")))
	    {
		data.NewsArticlesLocationsID = IdType.UNSET;
	    }
	    else
	    {
		data.NewsArticlesLocationsID = new IdType (dataReader.GetInt32(dataReader.GetOrdinal("NewsArticlesLocationsID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("NewsArticlesID")))
	    {
		data.NewsArticlesID = IntegerType.UNSET;
	    }
	    else
	    {
		data.NewsArticlesID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("NewsArticlesID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrgLocationsID")))
	    {
		data.OrgLocationsID = IntegerType.UNSET;
	    }
	    else
	    {
		data.OrgLocationsID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("OrgLocationsID")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the NewsArticlesLocations table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static IdType Insert(NewsArticlesLocationsData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "NewsArticlesID,"
	    + "OrgLocationsID,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@NewsArticlesID,"
	    + "@OrgLocationsID,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ");select Scope_Identity() Id";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@NewsArticlesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "NewsArticlesID", DataRowVersion.Proposed, data.NewsArticlesID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrgLocationsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgLocationsID", DataRowVersion.Proposed, data.OrgLocationsID.DBValue));

	    // Execute the query
	    SqlDataReader returnValue = cmd.ExecuteReader();
	    returnValue.Read();
	    int returnId = (int)(returnValue.GetDecimal(0));
	    returnValue.Close();
	    // Set the output paramter value(s)
	    return new IdType (returnId);
        }
        
        /// <summary>
        /// Updates a record in the NewsArticlesLocations table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(NewsArticlesLocationsData data)
        {
            // Create and execute the command
	    NewsArticlesLocationsData oldData = Load ( data.NewsArticlesLocationsID);
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.NewsArticlesID.Equals(data.NewsArticlesID))
	    {
		sql = sql + "NewsArticlesID=@NewsArticlesID,";
	    }
	    if (!oldData.OrgLocationsID.Equals(data.OrgLocationsID))
	    {
		sql = sql + "OrgLocationsID=@OrgLocationsID,";
	    }
	    WhereClause w = new WhereClause();
	    w.And("NewsArticlesLocationsID", data.NewsArticlesLocationsID.DBValue);
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.NewsArticlesLocationsID.Equals(data.NewsArticlesLocationsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@NewsArticlesLocationsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "NewsArticlesLocationsID", DataRowVersion.Proposed, data.NewsArticlesLocationsID.DBValue));

	    }
	    if (!oldData.NewsArticlesID.Equals(data.NewsArticlesID))
	    {
		cmd.Parameters.Add(new SqlParameter("@NewsArticlesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "NewsArticlesID", DataRowVersion.Proposed, data.NewsArticlesID.DBValue));

	    }
	    if (!oldData.OrgLocationsID.Equals(data.OrgLocationsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgLocationsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgLocationsID", DataRowVersion.Proposed, data.OrgLocationsID.DBValue));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the NewsArticlesLocations table by NewsArticlesLocationsID.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Delete(IdType newsArticlesLocationsID)
        {
            // Create and execute the command
	    string sql = "Delete From " + TABLE;
	    WhereClause w = new WhereClause();
	    w.And("NewsArticlesLocationsID", newsArticlesLocationsID.DBValue);
	    sql += w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Returns a list of objects which match the values for the fields specified.
        /// </summary>
        /// <param name="NewsArticlesID">A field value to be matched.</param>
        /// <returns>The list of NewsArticlesLocationsDAO objects found.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static NewsArticlesLocationsList FindByNewsArticlesID(IntegerType newsArticlesID)
        {
            OrderByClause sort = new OrderByClause("NewsArticlesID");
	    WhereClause filter = new WhereClause();
	    filter.And("NewsArticlesID", newsArticlesID.DBValue);

	    return GetList(filter, sort);
        }
    }
}