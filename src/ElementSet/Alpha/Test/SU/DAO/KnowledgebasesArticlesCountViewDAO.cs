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
    
    
    public class KnowledgebasesArticlesCountViewDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[KnowledgebasesArticlesCountView]";
        
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
        static KnowledgebasesArticlesCountViewDAO()
        {
            propertyToSqlMap.Add("KnowledgebasesID",@"KnowledgebasesID");
	    propertyToSqlMap.Add("Description",@"Description");
	    propertyToSqlMap.Add("ArticlesCount",@"ArticlesCount");
	    propertyToSqlMap.Add("OrgGroupsID",@"OrgGroupsID");
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
        /// Returns a list of all KnowledgebasesArticlesCountView rows.
        /// </summary>
        /// <returns>List of KnowledgebasesArticlesCountViewData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static KnowledgebasesArticlesCountViewList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of KnowledgebasesArticlesCountView rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of KnowledgebasesArticlesCountViewData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static KnowledgebasesArticlesCountViewList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of KnowledgebasesArticlesCountView rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of KnowledgebasesArticlesCountViewData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static KnowledgebasesArticlesCountViewList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of KnowledgebasesArticlesCountView rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of KnowledgebasesArticlesCountViewData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static KnowledgebasesArticlesCountViewList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, whereClause, orderByClause, true);
	    KnowledgebasesArticlesCountViewList list = new KnowledgebasesArticlesCountViewList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a KnowledgebasesArticlesCountView entity using it's primary key.
        /// </summary>
        /// <returns>A KnowledgebasesArticlesCountViewData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static KnowledgebasesArticlesCountViewData Load()
        {
            WhereClause w = new WhereClause();
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for KnowledgebasesArticlesCountView.");
	    }
	    KnowledgebasesArticlesCountViewData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static KnowledgebasesArticlesCountViewData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            KnowledgebasesArticlesCountViewData data = new KnowledgebasesArticlesCountViewData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("KnowledgebasesID")))
	    {
		data.KnowledgebasesID = IntegerType.UNSET;
	    }
	    else
	    {
		data.KnowledgebasesID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("KnowledgebasesID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Description")))
	    {
		data.Description = StringType.UNSET;
	    }
	    else
	    {
		data.Description = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Description")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ArticlesCount")))
	    {
		data.ArticlesCount = IntegerType.UNSET;
	    }
	    else
	    {
		data.ArticlesCount = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("ArticlesCount")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrgGroupsID")))
	    {
		data.OrgGroupsID = IntegerType.UNSET;
	    }
	    else
	    {
		data.OrgGroupsID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("OrgGroupsID")));
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
        /// Inserts a record into the KnowledgebasesArticlesCountView table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Insert(KnowledgebasesArticlesCountViewData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "KnowledgebasesID,"
	    + "Description,"
	    + "ArticlesCount,"
	    + "OrgGroupsID,"
	    + "IsPublic,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@KnowledgebasesID,"
	    + "@Description,"
	    + "@ArticlesCount,"
	    + "@OrgGroupsID,"
	    + "@IsPublic,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ")";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@KnowledgebasesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "KnowledgebasesID", DataRowVersion.Proposed, data.KnowledgebasesID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 150, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ArticlesCount", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ArticlesCount", DataRowVersion.Proposed, data.ArticlesCount.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrgGroupsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgGroupsID", DataRowVersion.Proposed, data.OrgGroupsID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@IsPublic", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsPublic", DataRowVersion.Proposed, !data.IsPublic.IsValid ? data.IsPublic.DBValue : data.IsPublic.DBValue.Equals ("Y") ? 1 : 0));

	    // Execute the query
	    cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Updates a record in the KnowledgebasesArticlesCountView table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(KnowledgebasesArticlesCountViewData data)
        {
            // Create and execute the command
	    KnowledgebasesArticlesCountViewData oldData = Load ();
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.KnowledgebasesID.Equals(data.KnowledgebasesID))
	    {
		sql = sql + "KnowledgebasesID=@KnowledgebasesID,";
	    }
	    if (!oldData.Description.Equals(data.Description))
	    {
		sql = sql + "Description=@Description,";
	    }
	    if (!oldData.ArticlesCount.Equals(data.ArticlesCount))
	    {
		sql = sql + "ArticlesCount=@ArticlesCount,";
	    }
	    if (!oldData.OrgGroupsID.Equals(data.OrgGroupsID))
	    {
		sql = sql + "OrgGroupsID=@OrgGroupsID,";
	    }
	    if (!oldData.IsPublic.Equals(data.IsPublic))
	    {
		sql = sql + "IsPublic=@IsPublic,";
	    }
	    WhereClause w = new WhereClause();
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.KnowledgebasesID.Equals(data.KnowledgebasesID))
	    {
		cmd.Parameters.Add(new SqlParameter("@KnowledgebasesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "KnowledgebasesID", DataRowVersion.Proposed, data.KnowledgebasesID.DBValue));

	    }
	    if (!oldData.Description.Equals(data.Description))
	    {
		cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 150, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));

	    }
	    if (!oldData.ArticlesCount.Equals(data.ArticlesCount))
	    {
		cmd.Parameters.Add(new SqlParameter("@ArticlesCount", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ArticlesCount", DataRowVersion.Proposed, data.ArticlesCount.DBValue));

	    }
	    if (!oldData.OrgGroupsID.Equals(data.OrgGroupsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgGroupsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgGroupsID", DataRowVersion.Proposed, data.OrgGroupsID.DBValue));

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
        /// Deletes a record from the KnowledgebasesArticlesCountView table by a composite primary key.
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