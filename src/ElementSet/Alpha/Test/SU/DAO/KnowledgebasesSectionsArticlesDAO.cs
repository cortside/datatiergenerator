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
    
    
    public class KnowledgebasesSectionsArticlesDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[KnowledgebasesSectionsArticles]";
        
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
        static KnowledgebasesSectionsArticlesDAO()
        {
            propertyToSqlMap.Add("KnowledgebasesSectionsArticlesID",@"KnowledgebasesSectionsArticlesID");
	    propertyToSqlMap.Add("Title",@"Title");
	    propertyToSqlMap.Add("DateStart",@"DateStart");
	    propertyToSqlMap.Add("DateEnd",@"DateEnd");
	    propertyToSqlMap.Add("ParentID",@"ParentID");
	    propertyToSqlMap.Add("KnowledgebasesID",@"KnowledgebasesID");
	    propertyToSqlMap.Add("HasChild",@"HasChild");
	    propertyToSqlMap.Add("PrevKnowledgebasesSectionsID",@"PrevKnowledgebasesSectionsID");
	    propertyToSqlMap.Add("NextKnowledgebasesSectionsID",@"NextKnowledgebasesSectionsID");
	    propertyToSqlMap.Add("ListInParentArticle",@"ListInParentArticle");
	    propertyToSqlMap.Add("IncludeSummaryinParent",@"IncludeSummaryinParent");
	    propertyToSqlMap.Add("Sort",@"Sort");
	    propertyToSqlMap.Add("IsSection",@"IsSection");
	    propertyToSqlMap.Add("IsTemp",@"IsTemp");
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
        /// Returns a list of all KnowledgebasesSectionsArticles rows.
        /// </summary>
        /// <returns>List of KnowledgebasesSectionsArticlesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static KnowledgebasesSectionsArticlesList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of KnowledgebasesSectionsArticles rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of KnowledgebasesSectionsArticlesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static KnowledgebasesSectionsArticlesList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of KnowledgebasesSectionsArticles rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of KnowledgebasesSectionsArticlesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static KnowledgebasesSectionsArticlesList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of KnowledgebasesSectionsArticles rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of KnowledgebasesSectionsArticlesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static KnowledgebasesSectionsArticlesList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, whereClause, orderByClause, true);
	    KnowledgebasesSectionsArticlesList list = new KnowledgebasesSectionsArticlesList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a KnowledgebasesSectionsArticles entity using it's primary key.
        /// </summary>
        /// <param name="KnowledgebasesSectionsArticlesID">A key field.</param>
        /// <returns>A KnowledgebasesSectionsArticlesData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static KnowledgebasesSectionsArticlesData Load(IdType knowledgebasesSectionsArticlesID)
        {
            WhereClause w = new WhereClause();
	    w.And("KnowledgebasesSectionsArticlesID", knowledgebasesSectionsArticlesID.DBValue);
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for KnowledgebasesSectionsArticles.");
	    }
	    KnowledgebasesSectionsArticlesData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static KnowledgebasesSectionsArticlesData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            KnowledgebasesSectionsArticlesData data = new KnowledgebasesSectionsArticlesData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("KnowledgebasesSectionsArticlesID")))
	    {
		data.KnowledgebasesSectionsArticlesID = IdType.UNSET;
	    }
	    else
	    {
		data.KnowledgebasesSectionsArticlesID = new IdType (dataReader.GetInt32(dataReader.GetOrdinal("KnowledgebasesSectionsArticlesID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Title")))
	    {
		data.Title = StringType.UNSET;
	    }
	    else
	    {
		data.Title = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Title")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("DateStart")))
	    {
		data.DateStart = DateType.UNSET;
	    }
	    else
	    {
		data.DateStart = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("DateStart")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("DateEnd")))
	    {
		data.DateEnd = DateType.UNSET;
	    }
	    else
	    {
		data.DateEnd = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("DateEnd")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ParentID")))
	    {
		data.ParentID = IntegerType.UNSET;
	    }
	    else
	    {
		data.ParentID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("ParentID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("KnowledgebasesID")))
	    {
		data.KnowledgebasesID = IntegerType.UNSET;
	    }
	    else
	    {
		data.KnowledgebasesID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("KnowledgebasesID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("HasChild")))
	    {
		data.HasChild = BooleanType.UNSET;
	    }
	    else
	    {
		data.HasChild = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("HasChild")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("PrevKnowledgebasesSectionsID")))
	    {
		data.PrevKnowledgebasesSectionsID = IntegerType.UNSET;
	    }
	    else
	    {
		data.PrevKnowledgebasesSectionsID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("PrevKnowledgebasesSectionsID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("NextKnowledgebasesSectionsID")))
	    {
		data.NextKnowledgebasesSectionsID = IntegerType.UNSET;
	    }
	    else
	    {
		data.NextKnowledgebasesSectionsID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("NextKnowledgebasesSectionsID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ListInParentArticle")))
	    {
		data.ListInParentArticle = BooleanType.UNSET;
	    }
	    else
	    {
		data.ListInParentArticle = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("ListInParentArticle")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("IncludeSummaryinParent")))
	    {
		data.IncludeSummaryinParent = BooleanType.UNSET;
	    }
	    else
	    {
		data.IncludeSummaryinParent = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("IncludeSummaryinParent")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Sort")))
	    {
		data.Sort = IntegerType.UNSET;
	    }
	    else
	    {
		data.Sort = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("Sort")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("IsSection")))
	    {
		data.IsSection = BooleanType.UNSET;
	    }
	    else
	    {
		data.IsSection = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("IsSection")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("IsTemp")))
	    {
		data.IsTemp = BooleanType.UNSET;
	    }
	    else
	    {
		data.IsTemp = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("IsTemp")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the KnowledgebasesSectionsArticles table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static IdType Insert(KnowledgebasesSectionsArticlesData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "Title,"
	    + "DateStart,"
	    + "DateEnd,"
	    + "ParentID,"
	    + "KnowledgebasesID,"
	    + "HasChild,"
	    + "PrevKnowledgebasesSectionsID,"
	    + "NextKnowledgebasesSectionsID,"
	    + "ListInParentArticle,"
	    + "IncludeSummaryinParent,"
	    + "Sort,"
	    + "IsSection,"
	    + "IsTemp,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@Title,"
	    + "@DateStart,"
	    + "@DateEnd,"
	    + "@ParentID,"
	    + "@KnowledgebasesID,"
	    + "@HasChild,"
	    + "@PrevKnowledgebasesSectionsID,"
	    + "@NextKnowledgebasesSectionsID,"
	    + "@ListInParentArticle,"
	    + "@IncludeSummaryinParent,"
	    + "@Sort,"
	    + "@IsSection,"
	    + "@IsTemp,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ");select Scope_Identity() Id";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@Title", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 0, 0, "Title", DataRowVersion.Proposed, data.Title.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DateStart", SqlDbType.SmallDateTime, 0, ParameterDirection.Input, false, 0, 0, "DateStart", DataRowVersion.Proposed, data.DateStart.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.SmallDateTime, 0, ParameterDirection.Input, false, 0, 0, "DateEnd", DataRowVersion.Proposed, data.DateEnd.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ParentID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ParentID", DataRowVersion.Proposed, data.ParentID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@KnowledgebasesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "KnowledgebasesID", DataRowVersion.Proposed, data.KnowledgebasesID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@HasChild", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "HasChild", DataRowVersion.Proposed, !data.HasChild.IsValid ? data.HasChild.DBValue : data.HasChild.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@PrevKnowledgebasesSectionsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "PrevKnowledgebasesSectionsID", DataRowVersion.Proposed, data.PrevKnowledgebasesSectionsID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@NextKnowledgebasesSectionsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "NextKnowledgebasesSectionsID", DataRowVersion.Proposed, data.NextKnowledgebasesSectionsID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ListInParentArticle", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "ListInParentArticle", DataRowVersion.Proposed, !data.ListInParentArticle.IsValid ? data.ListInParentArticle.DBValue : data.ListInParentArticle.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@IncludeSummaryinParent", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IncludeSummaryinParent", DataRowVersion.Proposed, !data.IncludeSummaryinParent.IsValid ? data.IncludeSummaryinParent.DBValue : data.IncludeSummaryinParent.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@Sort", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Sort", DataRowVersion.Proposed, data.Sort.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@IsSection", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsSection", DataRowVersion.Proposed, !data.IsSection.IsValid ? data.IsSection.DBValue : data.IsSection.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@IsTemp", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsTemp", DataRowVersion.Proposed, !data.IsTemp.IsValid ? data.IsTemp.DBValue : data.IsTemp.DBValue.Equals ("Y") ? 1 : 0));

	    // Execute the query
	    SqlDataReader returnValue = cmd.ExecuteReader();
	    returnValue.Read();
	    int returnId = (int)(returnValue.GetDecimal(0));
	    returnValue.Close();
	    // Set the output paramter value(s)
	    return new IdType (returnId);
        }
        
        /// <summary>
        /// Updates a record in the KnowledgebasesSectionsArticles table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(KnowledgebasesSectionsArticlesData data)
        {
            // Create and execute the command
	    KnowledgebasesSectionsArticlesData oldData = Load ( data.KnowledgebasesSectionsArticlesID);
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.Title.Equals(data.Title))
	    {
		sql = sql + "Title=@Title,";
	    }
	    if (!oldData.DateStart.Equals(data.DateStart))
	    {
		sql = sql + "DateStart=@DateStart,";
	    }
	    if (!oldData.DateEnd.Equals(data.DateEnd))
	    {
		sql = sql + "DateEnd=@DateEnd,";
	    }
	    if (!oldData.ParentID.Equals(data.ParentID))
	    {
		sql = sql + "ParentID=@ParentID,";
	    }
	    if (!oldData.KnowledgebasesID.Equals(data.KnowledgebasesID))
	    {
		sql = sql + "KnowledgebasesID=@KnowledgebasesID,";
	    }
	    if (!oldData.HasChild.Equals(data.HasChild))
	    {
		sql = sql + "HasChild=@HasChild,";
	    }
	    if (!oldData.PrevKnowledgebasesSectionsID.Equals(data.PrevKnowledgebasesSectionsID))
	    {
		sql = sql + "PrevKnowledgebasesSectionsID=@PrevKnowledgebasesSectionsID,";
	    }
	    if (!oldData.NextKnowledgebasesSectionsID.Equals(data.NextKnowledgebasesSectionsID))
	    {
		sql = sql + "NextKnowledgebasesSectionsID=@NextKnowledgebasesSectionsID,";
	    }
	    if (!oldData.ListInParentArticle.Equals(data.ListInParentArticle))
	    {
		sql = sql + "ListInParentArticle=@ListInParentArticle,";
	    }
	    if (!oldData.IncludeSummaryinParent.Equals(data.IncludeSummaryinParent))
	    {
		sql = sql + "IncludeSummaryinParent=@IncludeSummaryinParent,";
	    }
	    if (!oldData.Sort.Equals(data.Sort))
	    {
		sql = sql + "Sort=@Sort,";
	    }
	    if (!oldData.IsSection.Equals(data.IsSection))
	    {
		sql = sql + "IsSection=@IsSection,";
	    }
	    if (!oldData.IsTemp.Equals(data.IsTemp))
	    {
		sql = sql + "IsTemp=@IsTemp,";
	    }
	    WhereClause w = new WhereClause();
	    w.And("KnowledgebasesSectionsArticlesID", data.KnowledgebasesSectionsArticlesID.DBValue);
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.KnowledgebasesSectionsArticlesID.Equals(data.KnowledgebasesSectionsArticlesID))
	    {
		cmd.Parameters.Add(new SqlParameter("@KnowledgebasesSectionsArticlesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "KnowledgebasesSectionsArticlesID", DataRowVersion.Proposed, data.KnowledgebasesSectionsArticlesID.DBValue));

	    }
	    if (!oldData.Title.Equals(data.Title))
	    {
		cmd.Parameters.Add(new SqlParameter("@Title", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 0, 0, "Title", DataRowVersion.Proposed, data.Title.DBValue));

	    }
	    if (!oldData.DateStart.Equals(data.DateStart))
	    {
		cmd.Parameters.Add(new SqlParameter("@DateStart", SqlDbType.SmallDateTime, 0, ParameterDirection.Input, false, 0, 0, "DateStart", DataRowVersion.Proposed, data.DateStart.DBValue));

	    }
	    if (!oldData.DateEnd.Equals(data.DateEnd))
	    {
		cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.SmallDateTime, 0, ParameterDirection.Input, false, 0, 0, "DateEnd", DataRowVersion.Proposed, data.DateEnd.DBValue));

	    }
	    if (!oldData.ParentID.Equals(data.ParentID))
	    {
		cmd.Parameters.Add(new SqlParameter("@ParentID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ParentID", DataRowVersion.Proposed, data.ParentID.DBValue));

	    }
	    if (!oldData.KnowledgebasesID.Equals(data.KnowledgebasesID))
	    {
		cmd.Parameters.Add(new SqlParameter("@KnowledgebasesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "KnowledgebasesID", DataRowVersion.Proposed, data.KnowledgebasesID.DBValue));

	    }
	    if (!oldData.HasChild.Equals(data.HasChild))
	    {
		cmd.Parameters.Add(new SqlParameter("@HasChild", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "HasChild", DataRowVersion.Proposed, !data.HasChild.IsValid ? data.HasChild.DBValue : data.HasChild.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.PrevKnowledgebasesSectionsID.Equals(data.PrevKnowledgebasesSectionsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@PrevKnowledgebasesSectionsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "PrevKnowledgebasesSectionsID", DataRowVersion.Proposed, data.PrevKnowledgebasesSectionsID.DBValue));

	    }
	    if (!oldData.NextKnowledgebasesSectionsID.Equals(data.NextKnowledgebasesSectionsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@NextKnowledgebasesSectionsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "NextKnowledgebasesSectionsID", DataRowVersion.Proposed, data.NextKnowledgebasesSectionsID.DBValue));

	    }
	    if (!oldData.ListInParentArticle.Equals(data.ListInParentArticle))
	    {
		cmd.Parameters.Add(new SqlParameter("@ListInParentArticle", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "ListInParentArticle", DataRowVersion.Proposed, !data.ListInParentArticle.IsValid ? data.ListInParentArticle.DBValue : data.ListInParentArticle.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.IncludeSummaryinParent.Equals(data.IncludeSummaryinParent))
	    {
		cmd.Parameters.Add(new SqlParameter("@IncludeSummaryinParent", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IncludeSummaryinParent", DataRowVersion.Proposed, !data.IncludeSummaryinParent.IsValid ? data.IncludeSummaryinParent.DBValue : data.IncludeSummaryinParent.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.Sort.Equals(data.Sort))
	    {
		cmd.Parameters.Add(new SqlParameter("@Sort", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Sort", DataRowVersion.Proposed, data.Sort.DBValue));

	    }
	    if (!oldData.IsSection.Equals(data.IsSection))
	    {
		cmd.Parameters.Add(new SqlParameter("@IsSection", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsSection", DataRowVersion.Proposed, !data.IsSection.IsValid ? data.IsSection.DBValue : data.IsSection.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.IsTemp.Equals(data.IsTemp))
	    {
		cmd.Parameters.Add(new SqlParameter("@IsTemp", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsTemp", DataRowVersion.Proposed, !data.IsTemp.IsValid ? data.IsTemp.DBValue : data.IsTemp.DBValue.Equals ("Y") ? 1 : 0));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the KnowledgebasesSectionsArticles table by KnowledgebasesSectionsArticlesID.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Delete(IdType knowledgebasesSectionsArticlesID)
        {
            // Create and execute the command
	    string sql = "Delete From " + TABLE;
	    WhereClause w = new WhereClause();
	    w.And("KnowledgebasesSectionsArticlesID", knowledgebasesSectionsArticlesID.DBValue);
	    sql += w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();
        }
    }
}