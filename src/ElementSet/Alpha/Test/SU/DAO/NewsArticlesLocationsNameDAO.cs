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
    
    
    public class NewsArticlesLocationsNameDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[NewsArticlesLocationsName]";
        
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
        static NewsArticlesLocationsNameDAO()
        {
            propertyToSqlMap.Add("Description",@"Description");
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
        /// Returns a list of all NewsArticlesLocationsName rows.
        /// </summary>
        /// <returns>List of NewsArticlesLocationsNameData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static NewsArticlesLocationsNameList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of NewsArticlesLocationsName rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of NewsArticlesLocationsNameData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static NewsArticlesLocationsNameList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of NewsArticlesLocationsName rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of NewsArticlesLocationsNameData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static NewsArticlesLocationsNameList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of NewsArticlesLocationsName rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of NewsArticlesLocationsNameData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static NewsArticlesLocationsNameList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, whereClause, orderByClause, true);
	    NewsArticlesLocationsNameList list = new NewsArticlesLocationsNameList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a NewsArticlesLocationsName entity using it's primary key.
        /// </summary>
        /// <returns>A NewsArticlesLocationsNameData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static NewsArticlesLocationsNameData Load()
        {
            WhereClause w = new WhereClause();
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for NewsArticlesLocationsName.");
	    }
	    NewsArticlesLocationsNameData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static NewsArticlesLocationsNameData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            NewsArticlesLocationsNameData data = new NewsArticlesLocationsNameData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Description")))
	    {
		data.Description = StringType.UNSET;
	    }
	    else
	    {
		data.Description = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Description")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("NewsArticlesLocationsID")))
	    {
		data.NewsArticlesLocationsID = IntegerType.UNSET;
	    }
	    else
	    {
		data.NewsArticlesLocationsID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("NewsArticlesLocationsID")));
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
        /// Inserts a record into the NewsArticlesLocationsName table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Insert(NewsArticlesLocationsNameData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "Description,"
	    + "NewsArticlesLocationsID,"
	    + "NewsArticlesID,"
	    + "OrgLocationsID,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@Description,"
	    + "@NewsArticlesLocationsID,"
	    + "@NewsArticlesID,"
	    + "@OrgLocationsID,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ")";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@NewsArticlesLocationsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "NewsArticlesLocationsID", DataRowVersion.Proposed, data.NewsArticlesLocationsID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@NewsArticlesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "NewsArticlesID", DataRowVersion.Proposed, data.NewsArticlesID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrgLocationsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgLocationsID", DataRowVersion.Proposed, data.OrgLocationsID.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Updates a record in the NewsArticlesLocationsName table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(NewsArticlesLocationsNameData data)
        {
            // Create and execute the command
	    NewsArticlesLocationsNameData oldData = Load ();
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.Description.Equals(data.Description))
	    {
		sql = sql + "Description=@Description,";
	    }
	    if (!oldData.NewsArticlesLocationsID.Equals(data.NewsArticlesLocationsID))
	    {
		sql = sql + "NewsArticlesLocationsID=@NewsArticlesLocationsID,";
	    }
	    if (!oldData.NewsArticlesID.Equals(data.NewsArticlesID))
	    {
		sql = sql + "NewsArticlesID=@NewsArticlesID,";
	    }
	    if (!oldData.OrgLocationsID.Equals(data.OrgLocationsID))
	    {
		sql = sql + "OrgLocationsID=@OrgLocationsID,";
	    }
	    WhereClause w = new WhereClause();
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.Description.Equals(data.Description))
	    {
		cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));

	    }
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
        /// Deletes a record from the NewsArticlesLocationsName table by a composite primary key.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Delete()
        {
            // Create and execute the command
	    string sql = "Delete From " + TABLE;
	    WhereClause w = new WhereClause();
	    sql += w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();
        }
    }
}