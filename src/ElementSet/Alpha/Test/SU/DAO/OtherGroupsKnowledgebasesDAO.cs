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
    
    
    public class OtherGroupsKnowledgebasesDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[OtherGroupsKnowledgebases]";
        
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
        static OtherGroupsKnowledgebasesDAO()
        {
            propertyToSqlMap.Add("KnowledgebasesID",@"KnowledgebasesID");
	    propertyToSqlMap.Add("SubscriptionID",@"SubscriptionID");
	    propertyToSqlMap.Add("Description",@"Description");
	    propertyToSqlMap.Add("IsPublic",@"IsPublic");
	    propertyToSqlMap.Add("GroupDescription",@"GroupDescription");
	    propertyToSqlMap.Add("ArticlesCount",@"ArticlesCount");
	    propertyToSqlMap.Add("OrgGroupID",@"OrgGroupID");
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
        /// Returns a list of all OtherGroupsKnowledgebases rows.
        /// </summary>
        /// <returns>List of OtherGroupsKnowledgebasesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static OtherGroupsKnowledgebasesList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of OtherGroupsKnowledgebases rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of OtherGroupsKnowledgebasesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static OtherGroupsKnowledgebasesList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of OtherGroupsKnowledgebases rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of OtherGroupsKnowledgebasesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static OtherGroupsKnowledgebasesList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of OtherGroupsKnowledgebases rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of OtherGroupsKnowledgebasesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static OtherGroupsKnowledgebasesList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, whereClause, orderByClause, true);
	    OtherGroupsKnowledgebasesList list = new OtherGroupsKnowledgebasesList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a OtherGroupsKnowledgebases entity using it's primary key.
        /// </summary>
        /// <returns>A OtherGroupsKnowledgebasesData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static OtherGroupsKnowledgebasesData Load()
        {
            WhereClause w = new WhereClause();
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for OtherGroupsKnowledgebases.");
	    }
	    OtherGroupsKnowledgebasesData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static OtherGroupsKnowledgebasesData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            OtherGroupsKnowledgebasesData data = new OtherGroupsKnowledgebasesData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("KnowledgebasesID")))
	    {
		data.KnowledgebasesID = IntegerType.UNSET;
	    }
	    else
	    {
		data.KnowledgebasesID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("KnowledgebasesID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("SubscriptionID")))
	    {
		data.SubscriptionID = IntegerType.UNSET;
	    }
	    else
	    {
		data.SubscriptionID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("SubscriptionID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Description")))
	    {
		data.Description = StringType.UNSET;
	    }
	    else
	    {
		data.Description = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Description")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("IsPublic")))
	    {
		data.IsPublic = BooleanType.UNSET;
	    }
	    else
	    {
		data.IsPublic = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("IsPublic")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("GroupDescription")))
	    {
		data.GroupDescription = StringType.UNSET;
	    }
	    else
	    {
		data.GroupDescription = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("GroupDescription")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ArticlesCount")))
	    {
		data.ArticlesCount = IntegerType.UNSET;
	    }
	    else
	    {
		data.ArticlesCount = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("ArticlesCount")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrgGroupID")))
	    {
		data.OrgGroupID = IntegerType.UNSET;
	    }
	    else
	    {
		data.OrgGroupID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("OrgGroupID")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the OtherGroupsKnowledgebases table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Insert(OtherGroupsKnowledgebasesData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "KnowledgebasesID,"
	    + "SubscriptionID,"
	    + "Description,"
	    + "IsPublic,"
	    + "GroupDescription,"
	    + "ArticlesCount,"
	    + "OrgGroupID,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@KnowledgebasesID,"
	    + "@SubscriptionID,"
	    + "@Description,"
	    + "@IsPublic,"
	    + "@GroupDescription,"
	    + "@ArticlesCount,"
	    + "@OrgGroupID,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ")";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@KnowledgebasesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "KnowledgebasesID", DataRowVersion.Proposed, data.KnowledgebasesID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@SubscriptionID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "SubscriptionID", DataRowVersion.Proposed, data.SubscriptionID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 150, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@IsPublic", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsPublic", DataRowVersion.Proposed, !data.IsPublic.IsValid ? data.IsPublic.DBValue : data.IsPublic.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@GroupDescription", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "GroupDescription", DataRowVersion.Proposed, data.GroupDescription.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ArticlesCount", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ArticlesCount", DataRowVersion.Proposed, data.ArticlesCount.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrgGroupID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgGroupID", DataRowVersion.Proposed, data.OrgGroupID.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Updates a record in the OtherGroupsKnowledgebases table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(OtherGroupsKnowledgebasesData data)
        {
            // Create and execute the command
	    OtherGroupsKnowledgebasesData oldData = Load ();
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.KnowledgebasesID.Equals(data.KnowledgebasesID))
	    {
		sql = sql + "KnowledgebasesID=@KnowledgebasesID,";
	    }
	    if (!oldData.SubscriptionID.Equals(data.SubscriptionID))
	    {
		sql = sql + "SubscriptionID=@SubscriptionID,";
	    }
	    if (!oldData.Description.Equals(data.Description))
	    {
		sql = sql + "Description=@Description,";
	    }
	    if (!oldData.IsPublic.Equals(data.IsPublic))
	    {
		sql = sql + "IsPublic=@IsPublic,";
	    }
	    if (!oldData.GroupDescription.Equals(data.GroupDescription))
	    {
		sql = sql + "GroupDescription=@GroupDescription,";
	    }
	    if (!oldData.ArticlesCount.Equals(data.ArticlesCount))
	    {
		sql = sql + "ArticlesCount=@ArticlesCount,";
	    }
	    if (!oldData.OrgGroupID.Equals(data.OrgGroupID))
	    {
		sql = sql + "OrgGroupID=@OrgGroupID,";
	    }
	    WhereClause w = new WhereClause();
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.KnowledgebasesID.Equals(data.KnowledgebasesID))
	    {
		cmd.Parameters.Add(new SqlParameter("@KnowledgebasesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "KnowledgebasesID", DataRowVersion.Proposed, data.KnowledgebasesID.DBValue));

	    }
	    if (!oldData.SubscriptionID.Equals(data.SubscriptionID))
	    {
		cmd.Parameters.Add(new SqlParameter("@SubscriptionID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "SubscriptionID", DataRowVersion.Proposed, data.SubscriptionID.DBValue));

	    }
	    if (!oldData.Description.Equals(data.Description))
	    {
		cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 150, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));

	    }
	    if (!oldData.IsPublic.Equals(data.IsPublic))
	    {
		cmd.Parameters.Add(new SqlParameter("@IsPublic", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsPublic", DataRowVersion.Proposed, !data.IsPublic.IsValid ? data.IsPublic.DBValue : data.IsPublic.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.GroupDescription.Equals(data.GroupDescription))
	    {
		cmd.Parameters.Add(new SqlParameter("@GroupDescription", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "GroupDescription", DataRowVersion.Proposed, data.GroupDescription.DBValue));

	    }
	    if (!oldData.ArticlesCount.Equals(data.ArticlesCount))
	    {
		cmd.Parameters.Add(new SqlParameter("@ArticlesCount", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ArticlesCount", DataRowVersion.Proposed, data.ArticlesCount.DBValue));

	    }
	    if (!oldData.OrgGroupID.Equals(data.OrgGroupID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgGroupID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgGroupID", DataRowVersion.Proposed, data.OrgGroupID.DBValue));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the OtherGroupsKnowledgebases table by a composite primary key.
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