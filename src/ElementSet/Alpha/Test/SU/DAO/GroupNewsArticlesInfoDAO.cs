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
    
    
    public class GroupNewsArticlesInfoDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[GroupNewsArticlesInfo]";
        
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
        static GroupNewsArticlesInfoDAO()
        {
            propertyToSqlMap.Add("OrgGroupsID",@"OrgGroupsID");
	    propertyToSqlMap.Add("ArticleCount",@"ArticleCount");
	    propertyToSqlMap.Add("LastModified",@"LastModified");
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
        /// Returns a list of all GroupNewsArticlesInfo rows.
        /// </summary>
        /// <returns>List of GroupNewsArticlesInfoData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static GroupNewsArticlesInfoList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of GroupNewsArticlesInfo rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of GroupNewsArticlesInfoData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static GroupNewsArticlesInfoList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of GroupNewsArticlesInfo rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of GroupNewsArticlesInfoData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static GroupNewsArticlesInfoList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of GroupNewsArticlesInfo rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of GroupNewsArticlesInfoData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static GroupNewsArticlesInfoList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, whereClause, orderByClause, true);
	    GroupNewsArticlesInfoList list = new GroupNewsArticlesInfoList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a GroupNewsArticlesInfo entity using it's primary key.
        /// </summary>
        /// <returns>A GroupNewsArticlesInfoData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static GroupNewsArticlesInfoData Load()
        {
            WhereClause w = new WhereClause();
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for GroupNewsArticlesInfo.");
	    }
	    GroupNewsArticlesInfoData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static GroupNewsArticlesInfoData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            GroupNewsArticlesInfoData data = new GroupNewsArticlesInfoData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrgGroupsID")))
	    {
		data.OrgGroupsID = IntegerType.UNSET;
	    }
	    else
	    {
		data.OrgGroupsID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("OrgGroupsID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ArticleCount")))
	    {
		data.ArticleCount = IntegerType.UNSET;
	    }
	    else
	    {
		data.ArticleCount = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("ArticleCount")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("LastModified")))
	    {
		data.LastModified = DateType.UNSET;
	    }
	    else
	    {
		data.LastModified = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("LastModified")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the GroupNewsArticlesInfo table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Insert(GroupNewsArticlesInfoData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "OrgGroupsID,"
	    + "ArticleCount,"
	    + "LastModified,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@OrgGroupsID,"
	    + "@ArticleCount,"
	    + "@LastModified,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ")";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@OrgGroupsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgGroupsID", DataRowVersion.Proposed, data.OrgGroupsID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ArticleCount", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ArticleCount", DataRowVersion.Proposed, data.ArticleCount.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@LastModified", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "LastModified", DataRowVersion.Proposed, data.LastModified.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Updates a record in the GroupNewsArticlesInfo table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(GroupNewsArticlesInfoData data)
        {
            // Create and execute the command
	    GroupNewsArticlesInfoData oldData = Load ();
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.OrgGroupsID.Equals(data.OrgGroupsID))
	    {
		sql = sql + "OrgGroupsID=@OrgGroupsID,";
	    }
	    if (!oldData.ArticleCount.Equals(data.ArticleCount))
	    {
		sql = sql + "ArticleCount=@ArticleCount,";
	    }
	    if (!oldData.LastModified.Equals(data.LastModified))
	    {
		sql = sql + "LastModified=@LastModified,";
	    }
	    WhereClause w = new WhereClause();
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.OrgGroupsID.Equals(data.OrgGroupsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgGroupsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgGroupsID", DataRowVersion.Proposed, data.OrgGroupsID.DBValue));

	    }
	    if (!oldData.ArticleCount.Equals(data.ArticleCount))
	    {
		cmd.Parameters.Add(new SqlParameter("@ArticleCount", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ArticleCount", DataRowVersion.Proposed, data.ArticleCount.DBValue));

	    }
	    if (!oldData.LastModified.Equals(data.LastModified))
	    {
		cmd.Parameters.Add(new SqlParameter("@LastModified", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "LastModified", DataRowVersion.Proposed, data.LastModified.DBValue));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the GroupNewsArticlesInfo table by a composite primary key.
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