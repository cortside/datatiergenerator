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
    
    
    public class NewsArticlesDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[NewsArticles]";
        
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
        static NewsArticlesDAO()
        {
            propertyToSqlMap.Add("NewsArticlesID",@"NewsArticlesID");
	    propertyToSqlMap.Add("OrgGroupsID",@"OrgGroupsID");
	    propertyToSqlMap.Add("Title",@"Title");
	    propertyToSqlMap.Add("Summary",@"Summary");
	    propertyToSqlMap.Add("Text",@"Text");
	    propertyToSqlMap.Add("DateCreated",@"DateCreated");
	    propertyToSqlMap.Add("DateModified",@"DateModified");
	    propertyToSqlMap.Add("DateStart",@"DateStart");
	    propertyToSqlMap.Add("DateEnd",@"DateEnd");
	    propertyToSqlMap.Add("OrgEmployeesID",@"OrgEmployeesID");
	    propertyToSqlMap.Add("IsArchived",@"IsArchived");
	    propertyToSqlMap.Add("AllowDiscussion",@"AllowDiscussion");
	    propertyToSqlMap.Add("IsNew",@"IsNew");
	    propertyToSqlMap.Add("IsPublic",@"IsPublic");
	    propertyToSqlMap.Add("LastModifiedOrgEmployeesID",@"LastModifiedOrgEmployeesID");
	    propertyToSqlMap.Add("NewsImage",@"NewsImage");
	    propertyToSqlMap.Add("TemplateType",@"TemplateType");
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
        /// Returns a list of all NewsArticles rows.
        /// </summary>
        /// <returns>List of NewsArticlesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static NewsArticlesList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of NewsArticles rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of NewsArticlesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static NewsArticlesList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of NewsArticles rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of NewsArticlesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static NewsArticlesList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of NewsArticles rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of NewsArticlesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static NewsArticlesList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, whereClause, orderByClause, true);
	    NewsArticlesList list = new NewsArticlesList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a NewsArticles entity using it's primary key.
        /// </summary>
        /// <param name="NewsArticlesID">A key field.</param>
        /// <returns>A NewsArticlesData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static NewsArticlesData Load(IdType newsArticlesID)
        {
            WhereClause w = new WhereClause();
	    w.And("NewsArticlesID", newsArticlesID.DBValue);
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for NewsArticles.");
	    }
	    NewsArticlesData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static NewsArticlesData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            NewsArticlesData data = new NewsArticlesData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("NewsArticlesID")))
	    {
		data.NewsArticlesID = IdType.UNSET;
	    }
	    else
	    {
		data.NewsArticlesID = new IdType (dataReader.GetInt32(dataReader.GetOrdinal("NewsArticlesID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrgGroupsID")))
	    {
		data.OrgGroupsID = IntegerType.UNSET;
	    }
	    else
	    {
		data.OrgGroupsID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("OrgGroupsID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Title")))
	    {
		data.Title = StringType.UNSET;
	    }
	    else
	    {
		data.Title = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Title")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Summary")))
	    {
		data.Summary = StringType.UNSET;
	    }
	    else
	    {
		data.Summary = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Summary")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Text")))
	    {
		data.Text = StringType.UNSET;
	    }
	    else
	    {
		data.Text = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Text")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("DateCreated")))
	    {
		data.DateCreated = DateType.UNSET;
	    }
	    else
	    {
		data.DateCreated = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("DateCreated")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("DateModified")))
	    {
		data.DateModified = DateType.UNSET;
	    }
	    else
	    {
		data.DateModified = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("DateModified")));
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
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrgEmployeesID")))
	    {
		data.OrgEmployeesID = IntegerType.UNSET;
	    }
	    else
	    {
		data.OrgEmployeesID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("OrgEmployeesID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("IsArchived")))
	    {
		data.IsArchived = BooleanType.UNSET;
	    }
	    else
	    {
		data.IsArchived = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("IsArchived")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("AllowDiscussion")))
	    {
		data.AllowDiscussion = BooleanType.UNSET;
	    }
	    else
	    {
		data.AllowDiscussion = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("AllowDiscussion")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("IsNew")))
	    {
		data.IsNew = BooleanType.UNSET;
	    }
	    else
	    {
		data.IsNew = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("IsNew")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("IsPublic")))
	    {
		data.IsPublic = BooleanType.UNSET;
	    }
	    else
	    {
		data.IsPublic = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("IsPublic")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("LastModifiedOrgEmployeesID")))
	    {
		data.LastModifiedOrgEmployeesID = IntegerType.UNSET;
	    }
	    else
	    {
		data.LastModifiedOrgEmployeesID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("LastModifiedOrgEmployeesID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("NewsImage")))
	    {
		data.NewsImage = StringType.UNSET;
	    }
	    else
	    {
		data.NewsImage = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("NewsImage")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("TemplateType")))
	    {
		data.TemplateType = IntegerType.UNSET;
	    }
	    else
	    {
		data.TemplateType = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("TemplateType")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the NewsArticles table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static IdType Insert(NewsArticlesData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "OrgGroupsID,"
	    + "Title,"
	    + "Summary,"
	    + "Text,"
	    + "DateCreated,"
	    + "DateModified,"
	    + "DateStart,"
	    + "DateEnd,"
	    + "OrgEmployeesID,"
	    + "IsArchived,"
	    + "AllowDiscussion,"
	    + "IsNew,"
	    + "IsPublic,"
	    + "LastModifiedOrgEmployeesID,"
	    + "NewsImage,"
	    + "TemplateType,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@OrgGroupsID,"
	    + "@Title,"
	    + "@Summary,"
	    + "@Text,"
	    + "@DateCreated,"
	    + "@DateModified,"
	    + "@DateStart,"
	    + "@DateEnd,"
	    + "@OrgEmployeesID,"
	    + "@IsArchived,"
	    + "@AllowDiscussion,"
	    + "@IsNew,"
	    + "@IsPublic,"
	    + "@LastModifiedOrgEmployeesID,"
	    + "@NewsImage,"
	    + "@TemplateType,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ");select Scope_Identity() Id";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@OrgGroupsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgGroupsID", DataRowVersion.Proposed, data.OrgGroupsID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Title", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Title", DataRowVersion.Proposed, data.Title.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Summary", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "Summary", DataRowVersion.Proposed, data.Summary.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Text", SqlDbType.Text, 0, ParameterDirection.Input, false, 0, 0, "Text", DataRowVersion.Proposed, data.Text.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DateCreated", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DateCreated", DataRowVersion.Proposed, data.DateCreated.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DateModified", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DateModified", DataRowVersion.Proposed, data.DateModified.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DateStart", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DateStart", DataRowVersion.Proposed, data.DateStart.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DateEnd", DataRowVersion.Proposed, data.DateEnd.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrgEmployeesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgEmployeesID", DataRowVersion.Proposed, data.OrgEmployeesID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@IsArchived", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsArchived", DataRowVersion.Proposed, !data.IsArchived.IsValid ? data.IsArchived.DBValue : data.IsArchived.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@AllowDiscussion", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "AllowDiscussion", DataRowVersion.Proposed, !data.AllowDiscussion.IsValid ? data.AllowDiscussion.DBValue : data.AllowDiscussion.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@IsNew", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsNew", DataRowVersion.Proposed, !data.IsNew.IsValid ? data.IsNew.DBValue : data.IsNew.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@IsPublic", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsPublic", DataRowVersion.Proposed, !data.IsPublic.IsValid ? data.IsPublic.DBValue : data.IsPublic.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@LastModifiedOrgEmployeesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "LastModifiedOrgEmployeesID", DataRowVersion.Proposed, data.LastModifiedOrgEmployeesID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@NewsImage", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "NewsImage", DataRowVersion.Proposed, data.NewsImage.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@TemplateType", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "TemplateType", DataRowVersion.Proposed, data.TemplateType.DBValue));

	    // Execute the query
	    SqlDataReader returnValue = cmd.ExecuteReader();
	    returnValue.Read();
	    int returnId = (int)(returnValue.GetDecimal(0));
	    returnValue.Close();
	    // Set the output paramter value(s)
	    return new IdType (returnId);
        }
        
        /// <summary>
        /// Updates a record in the NewsArticles table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(NewsArticlesData data)
        {
            // Create and execute the command
	    NewsArticlesData oldData = Load ( data.NewsArticlesID);
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.OrgGroupsID.Equals(data.OrgGroupsID))
	    {
		sql = sql + "OrgGroupsID=@OrgGroupsID,";
	    }
	    if (!oldData.Title.Equals(data.Title))
	    {
		sql = sql + "Title=@Title,";
	    }
	    if (!oldData.Summary.Equals(data.Summary))
	    {
		sql = sql + "Summary=@Summary,";
	    }
	    if (!oldData.Text.Equals(data.Text))
	    {
		sql = sql + "Text=@Text,";
	    }
	    if (!oldData.DateCreated.Equals(data.DateCreated))
	    {
		sql = sql + "DateCreated=@DateCreated,";
	    }
	    if (!oldData.DateModified.Equals(data.DateModified))
	    {
		sql = sql + "DateModified=@DateModified,";
	    }
	    if (!oldData.DateStart.Equals(data.DateStart))
	    {
		sql = sql + "DateStart=@DateStart,";
	    }
	    if (!oldData.DateEnd.Equals(data.DateEnd))
	    {
		sql = sql + "DateEnd=@DateEnd,";
	    }
	    if (!oldData.OrgEmployeesID.Equals(data.OrgEmployeesID))
	    {
		sql = sql + "OrgEmployeesID=@OrgEmployeesID,";
	    }
	    if (!oldData.IsArchived.Equals(data.IsArchived))
	    {
		sql = sql + "IsArchived=@IsArchived,";
	    }
	    if (!oldData.AllowDiscussion.Equals(data.AllowDiscussion))
	    {
		sql = sql + "AllowDiscussion=@AllowDiscussion,";
	    }
	    if (!oldData.IsNew.Equals(data.IsNew))
	    {
		sql = sql + "IsNew=@IsNew,";
	    }
	    if (!oldData.IsPublic.Equals(data.IsPublic))
	    {
		sql = sql + "IsPublic=@IsPublic,";
	    }
	    if (!oldData.LastModifiedOrgEmployeesID.Equals(data.LastModifiedOrgEmployeesID))
	    {
		sql = sql + "LastModifiedOrgEmployeesID=@LastModifiedOrgEmployeesID,";
	    }
	    if (!oldData.NewsImage.Equals(data.NewsImage))
	    {
		sql = sql + "NewsImage=@NewsImage,";
	    }
	    if (!oldData.TemplateType.Equals(data.TemplateType))
	    {
		sql = sql + "TemplateType=@TemplateType,";
	    }
	    WhereClause w = new WhereClause();
	    w.And("NewsArticlesID", data.NewsArticlesID.DBValue);
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.NewsArticlesID.Equals(data.NewsArticlesID))
	    {
		cmd.Parameters.Add(new SqlParameter("@NewsArticlesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "NewsArticlesID", DataRowVersion.Proposed, data.NewsArticlesID.DBValue));

	    }
	    if (!oldData.OrgGroupsID.Equals(data.OrgGroupsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgGroupsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgGroupsID", DataRowVersion.Proposed, data.OrgGroupsID.DBValue));

	    }
	    if (!oldData.Title.Equals(data.Title))
	    {
		cmd.Parameters.Add(new SqlParameter("@Title", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Title", DataRowVersion.Proposed, data.Title.DBValue));

	    }
	    if (!oldData.Summary.Equals(data.Summary))
	    {
		cmd.Parameters.Add(new SqlParameter("@Summary", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "Summary", DataRowVersion.Proposed, data.Summary.DBValue));

	    }
	    if (!oldData.Text.Equals(data.Text))
	    {
		cmd.Parameters.Add(new SqlParameter("@Text", SqlDbType.Text, 0, ParameterDirection.Input, false, 0, 0, "Text", DataRowVersion.Proposed, data.Text.DBValue));

	    }
	    if (!oldData.DateCreated.Equals(data.DateCreated))
	    {
		cmd.Parameters.Add(new SqlParameter("@DateCreated", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DateCreated", DataRowVersion.Proposed, data.DateCreated.DBValue));

	    }
	    if (!oldData.DateModified.Equals(data.DateModified))
	    {
		cmd.Parameters.Add(new SqlParameter("@DateModified", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DateModified", DataRowVersion.Proposed, data.DateModified.DBValue));

	    }
	    if (!oldData.DateStart.Equals(data.DateStart))
	    {
		cmd.Parameters.Add(new SqlParameter("@DateStart", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DateStart", DataRowVersion.Proposed, data.DateStart.DBValue));

	    }
	    if (!oldData.DateEnd.Equals(data.DateEnd))
	    {
		cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DateEnd", DataRowVersion.Proposed, data.DateEnd.DBValue));

	    }
	    if (!oldData.OrgEmployeesID.Equals(data.OrgEmployeesID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgEmployeesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgEmployeesID", DataRowVersion.Proposed, data.OrgEmployeesID.DBValue));

	    }
	    if (!oldData.IsArchived.Equals(data.IsArchived))
	    {
		cmd.Parameters.Add(new SqlParameter("@IsArchived", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsArchived", DataRowVersion.Proposed, !data.IsArchived.IsValid ? data.IsArchived.DBValue : data.IsArchived.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.AllowDiscussion.Equals(data.AllowDiscussion))
	    {
		cmd.Parameters.Add(new SqlParameter("@AllowDiscussion", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "AllowDiscussion", DataRowVersion.Proposed, !data.AllowDiscussion.IsValid ? data.AllowDiscussion.DBValue : data.AllowDiscussion.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.IsNew.Equals(data.IsNew))
	    {
		cmd.Parameters.Add(new SqlParameter("@IsNew", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsNew", DataRowVersion.Proposed, !data.IsNew.IsValid ? data.IsNew.DBValue : data.IsNew.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.IsPublic.Equals(data.IsPublic))
	    {
		cmd.Parameters.Add(new SqlParameter("@IsPublic", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsPublic", DataRowVersion.Proposed, !data.IsPublic.IsValid ? data.IsPublic.DBValue : data.IsPublic.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.LastModifiedOrgEmployeesID.Equals(data.LastModifiedOrgEmployeesID))
	    {
		cmd.Parameters.Add(new SqlParameter("@LastModifiedOrgEmployeesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "LastModifiedOrgEmployeesID", DataRowVersion.Proposed, data.LastModifiedOrgEmployeesID.DBValue));

	    }
	    if (!oldData.NewsImage.Equals(data.NewsImage))
	    {
		cmd.Parameters.Add(new SqlParameter("@NewsImage", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "NewsImage", DataRowVersion.Proposed, data.NewsImage.DBValue));

	    }
	    if (!oldData.TemplateType.Equals(data.TemplateType))
	    {
		cmd.Parameters.Add(new SqlParameter("@TemplateType", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "TemplateType", DataRowVersion.Proposed, data.TemplateType.DBValue));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the NewsArticles table by NewsArticlesID.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Delete(IdType newsArticlesID)
        {
            // Create and execute the command
	    string sql = "Delete From " + TABLE;
	    WhereClause w = new WhereClause();
	    w.And("NewsArticlesID", newsArticlesID.DBValue);
	    sql += w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();
        }
    }
}