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
    
    
    public class KnowledgebasesArticleTextDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[KnowledgebasesArticleText]";
        
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
        static KnowledgebasesArticleTextDAO()
        {
            propertyToSqlMap.Add("KnowledgebasesArticlesTextID",@"KnowledgebasesArticlesTextID");
	    propertyToSqlMap.Add("KnowledgebasesSectionsArticlesID",@"KnowledgebasesSectionsArticlesID");
	    propertyToSqlMap.Add("KnowledgebasesID",@"KnowledgebasesID");
	    propertyToSqlMap.Add("Text",@"Text");
	    propertyToSqlMap.Add("StartDate",@"StartDate");
	    propertyToSqlMap.Add("EndDate",@"EndDate");
	    propertyToSqlMap.Add("Keywords",@"Keywords");
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
        /// Returns a list of all KnowledgebasesArticleText rows.
        /// </summary>
        /// <returns>List of KnowledgebasesArticleTextData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static KnowledgebasesArticleTextList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of KnowledgebasesArticleText rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of KnowledgebasesArticleTextData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static KnowledgebasesArticleTextList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of KnowledgebasesArticleText rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of KnowledgebasesArticleTextData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static KnowledgebasesArticleTextList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of KnowledgebasesArticleText rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of KnowledgebasesArticleTextData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static KnowledgebasesArticleTextList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, whereClause, orderByClause, true);
	    KnowledgebasesArticleTextList list = new KnowledgebasesArticleTextList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a KnowledgebasesArticleText entity using it's primary key.
        /// </summary>
        /// <param name="KnowledgebasesArticlesTextID">A key field.</param>
        /// <returns>A KnowledgebasesArticleTextData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static KnowledgebasesArticleTextData Load(IdType knowledgebasesArticlesTextID)
        {
            WhereClause w = new WhereClause();
	    w.And("KnowledgebasesArticlesTextID", knowledgebasesArticlesTextID.DBValue);
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for KnowledgebasesArticleText.");
	    }
	    KnowledgebasesArticleTextData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static KnowledgebasesArticleTextData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            KnowledgebasesArticleTextData data = new KnowledgebasesArticleTextData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("KnowledgebasesArticlesTextID")))
	    {
		data.KnowledgebasesArticlesTextID = IdType.UNSET;
	    }
	    else
	    {
		data.KnowledgebasesArticlesTextID = new IdType (dataReader.GetInt32(dataReader.GetOrdinal("KnowledgebasesArticlesTextID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("KnowledgebasesSectionsArticlesID")))
	    {
		data.KnowledgebasesSectionsArticlesID = IntegerType.UNSET;
	    }
	    else
	    {
		data.KnowledgebasesSectionsArticlesID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("KnowledgebasesSectionsArticlesID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("KnowledgebasesID")))
	    {
		data.KnowledgebasesID = IntegerType.UNSET;
	    }
	    else
	    {
		data.KnowledgebasesID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("KnowledgebasesID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Text")))
	    {
		data.Text = StringType.UNSET;
	    }
	    else
	    {
		data.Text = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Text")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("StartDate")))
	    {
		data.StartDate = DateType.UNSET;
	    }
	    else
	    {
		data.StartDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("StartDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("EndDate")))
	    {
		data.EndDate = DateType.UNSET;
	    }
	    else
	    {
		data.EndDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("EndDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Keywords")))
	    {
		data.Keywords = StringType.UNSET;
	    }
	    else
	    {
		data.Keywords = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Keywords")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the KnowledgebasesArticleText table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static IdType Insert(KnowledgebasesArticleTextData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "KnowledgebasesSectionsArticlesID,"
	    + "KnowledgebasesID,"
	    + "Text,"
	    + "StartDate,"
	    + "EndDate,"
	    + "Keywords,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@KnowledgebasesSectionsArticlesID,"
	    + "@KnowledgebasesID,"
	    + "@Text,"
	    + "@StartDate,"
	    + "@EndDate,"
	    + "@Keywords,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ");select Scope_Identity() Id";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@KnowledgebasesSectionsArticlesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "KnowledgebasesSectionsArticlesID", DataRowVersion.Proposed, data.KnowledgebasesSectionsArticlesID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@KnowledgebasesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "KnowledgebasesID", DataRowVersion.Proposed, data.KnowledgebasesID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Text", SqlDbType.Text, 0, ParameterDirection.Input, false, 0, 0, "Text", DataRowVersion.Proposed, data.Text.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "StartDate", DataRowVersion.Proposed, data.StartDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "EndDate", DataRowVersion.Proposed, data.EndDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Keywords", SqlDbType.VarChar, 300, ParameterDirection.Input, false, 0, 0, "Keywords", DataRowVersion.Proposed, data.Keywords.DBValue));

	    // Execute the query
	    SqlDataReader returnValue = cmd.ExecuteReader();
	    returnValue.Read();
	    int returnId = (int)(returnValue.GetDecimal(0));
	    returnValue.Close();
	    // Set the output paramter value(s)
	    return new IdType (returnId);
        }
        
        /// <summary>
        /// Updates a record in the KnowledgebasesArticleText table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(KnowledgebasesArticleTextData data)
        {
            // Create and execute the command
	    KnowledgebasesArticleTextData oldData = Load ( data.KnowledgebasesArticlesTextID);
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.KnowledgebasesSectionsArticlesID.Equals(data.KnowledgebasesSectionsArticlesID))
	    {
		sql = sql + "KnowledgebasesSectionsArticlesID=@KnowledgebasesSectionsArticlesID,";
	    }
	    if (!oldData.KnowledgebasesID.Equals(data.KnowledgebasesID))
	    {
		sql = sql + "KnowledgebasesID=@KnowledgebasesID,";
	    }
	    if (!oldData.Text.Equals(data.Text))
	    {
		sql = sql + "Text=@Text,";
	    }
	    if (!oldData.StartDate.Equals(data.StartDate))
	    {
		sql = sql + "StartDate=@StartDate,";
	    }
	    if (!oldData.EndDate.Equals(data.EndDate))
	    {
		sql = sql + "EndDate=@EndDate,";
	    }
	    if (!oldData.Keywords.Equals(data.Keywords))
	    {
		sql = sql + "Keywords=@Keywords,";
	    }
	    WhereClause w = new WhereClause();
	    w.And("KnowledgebasesArticlesTextID", data.KnowledgebasesArticlesTextID.DBValue);
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.KnowledgebasesArticlesTextID.Equals(data.KnowledgebasesArticlesTextID))
	    {
		cmd.Parameters.Add(new SqlParameter("@KnowledgebasesArticlesTextID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "KnowledgebasesArticlesTextID", DataRowVersion.Proposed, data.KnowledgebasesArticlesTextID.DBValue));

	    }
	    if (!oldData.KnowledgebasesSectionsArticlesID.Equals(data.KnowledgebasesSectionsArticlesID))
	    {
		cmd.Parameters.Add(new SqlParameter("@KnowledgebasesSectionsArticlesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "KnowledgebasesSectionsArticlesID", DataRowVersion.Proposed, data.KnowledgebasesSectionsArticlesID.DBValue));

	    }
	    if (!oldData.KnowledgebasesID.Equals(data.KnowledgebasesID))
	    {
		cmd.Parameters.Add(new SqlParameter("@KnowledgebasesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "KnowledgebasesID", DataRowVersion.Proposed, data.KnowledgebasesID.DBValue));

	    }
	    if (!oldData.Text.Equals(data.Text))
	    {
		cmd.Parameters.Add(new SqlParameter("@Text", SqlDbType.Text, 0, ParameterDirection.Input, false, 0, 0, "Text", DataRowVersion.Proposed, data.Text.DBValue));

	    }
	    if (!oldData.StartDate.Equals(data.StartDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "StartDate", DataRowVersion.Proposed, data.StartDate.DBValue));

	    }
	    if (!oldData.EndDate.Equals(data.EndDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "EndDate", DataRowVersion.Proposed, data.EndDate.DBValue));

	    }
	    if (!oldData.Keywords.Equals(data.Keywords))
	    {
		cmd.Parameters.Add(new SqlParameter("@Keywords", SqlDbType.VarChar, 300, ParameterDirection.Input, false, 0, 0, "Keywords", DataRowVersion.Proposed, data.Keywords.DBValue));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the KnowledgebasesArticleText table by KnowledgebasesArticlesTextID.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Delete(IdType knowledgebasesArticlesTextID)
        {
            // Create and execute the command
	    string sql = "Delete From " + TABLE;
	    WhereClause w = new WhereClause();
	    w.And("KnowledgebasesArticlesTextID", knowledgebasesArticlesTextID.DBValue);
	    sql += w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();
        }
    }
}