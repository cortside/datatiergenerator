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
    
    
    public class NewsViewDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[NewsView]";
        
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
        static NewsViewDAO()
        {
            propertyToSqlMap.Add("Title",@"Title");
	    propertyToSqlMap.Add("Summary",@"Summary");
	    propertyToSqlMap.Add("DateStart",@"DateStart");
	    propertyToSqlMap.Add("DateEnd",@"DateEnd");
	    propertyToSqlMap.Add("IsPublic",@"IsPublic");
	    propertyToSqlMap.Add("DateModified",@"DateModified");
	    propertyToSqlMap.Add("LastModifiedFirstName",@"LastModifiedFirstName");
	    propertyToSqlMap.Add("LastModifiedLastName",@"LastModifiedLastName");
	    propertyToSqlMap.Add("CreateFirstName",@"CreateFirstName");
	    propertyToSqlMap.Add("CreateLastName",@"CreateLastName");
	    propertyToSqlMap.Add("OrgGroupsID",@"OrgGroupsID");
	    propertyToSqlMap.Add("NewsImage",@"NewsImage");
	    propertyToSqlMap.Add("TemplateType",@"TemplateType");
	    propertyToSqlMap.Add("NewsArticlesID",@"NewsArticlesID");
	    propertyToSqlMap.Add("Text",@"Text");
	    propertyToSqlMap.Add("LastModifiedID",@"LastModifiedID");
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
        /// Returns a list of all NewsView rows.
        /// </summary>
        /// <returns>List of NewsViewData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static NewsViewList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of NewsView rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of NewsViewData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static NewsViewList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of NewsView rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of NewsViewData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static NewsViewList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of NewsView rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of NewsViewData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static NewsViewList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, whereClause, orderByClause, true);
	    NewsViewList list = new NewsViewList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a NewsView entity using it's primary key.
        /// </summary>
        /// <returns>A NewsViewData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static NewsViewData Load()
        {
            WhereClause w = new WhereClause();
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for NewsView.");
	    }
	    NewsViewData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static NewsViewData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            NewsViewData data = new NewsViewData();
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
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("IsPublic")))
	    {
		data.IsPublic = BooleanType.UNSET;
	    }
	    else
	    {
		data.IsPublic = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("IsPublic")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("DateModified")))
	    {
		data.DateModified = DateType.UNSET;
	    }
	    else
	    {
		data.DateModified = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("DateModified")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("LastModifiedFirstName")))
	    {
		data.LastModifiedFirstName = StringType.UNSET;
	    }
	    else
	    {
		data.LastModifiedFirstName = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("LastModifiedFirstName")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("LastModifiedLastName")))
	    {
		data.LastModifiedLastName = StringType.UNSET;
	    }
	    else
	    {
		data.LastModifiedLastName = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("LastModifiedLastName")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("CreateFirstName")))
	    {
		data.CreateFirstName = StringType.UNSET;
	    }
	    else
	    {
		data.CreateFirstName = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("CreateFirstName")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("CreateLastName")))
	    {
		data.CreateLastName = StringType.UNSET;
	    }
	    else
	    {
		data.CreateLastName = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("CreateLastName")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrgGroupsID")))
	    {
		data.OrgGroupsID = IntegerType.UNSET;
	    }
	    else
	    {
		data.OrgGroupsID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("OrgGroupsID")));
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
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("NewsArticlesID")))
	    {
		data.NewsArticlesID = IntegerType.UNSET;
	    }
	    else
	    {
		data.NewsArticlesID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("NewsArticlesID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Text")))
	    {
		data.Text = StringType.UNSET;
	    }
	    else
	    {
		data.Text = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Text")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("LastModifiedID")))
	    {
		data.LastModifiedID = IntegerType.UNSET;
	    }
	    else
	    {
		data.LastModifiedID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("LastModifiedID")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the NewsView table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Insert(NewsViewData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "Title,"
	    + "Summary,"
	    + "DateStart,"
	    + "DateEnd,"
	    + "IsPublic,"
	    + "DateModified,"
	    + "LastModifiedFirstName,"
	    + "LastModifiedLastName,"
	    + "CreateFirstName,"
	    + "CreateLastName,"
	    + "OrgGroupsID,"
	    + "NewsImage,"
	    + "TemplateType,"
	    + "NewsArticlesID,"
	    + "Text,"
	    + "LastModifiedID,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@Title,"
	    + "@Summary,"
	    + "@DateStart,"
	    + "@DateEnd,"
	    + "@IsPublic,"
	    + "@DateModified,"
	    + "@LastModifiedFirstName,"
	    + "@LastModifiedLastName,"
	    + "@CreateFirstName,"
	    + "@CreateLastName,"
	    + "@OrgGroupsID,"
	    + "@NewsImage,"
	    + "@TemplateType,"
	    + "@NewsArticlesID,"
	    + "@Text,"
	    + "@LastModifiedID,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ")";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@Title", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Title", DataRowVersion.Proposed, data.Title.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Summary", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "Summary", DataRowVersion.Proposed, data.Summary.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DateStart", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DateStart", DataRowVersion.Proposed, data.DateStart.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DateEnd", DataRowVersion.Proposed, data.DateEnd.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@IsPublic", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsPublic", DataRowVersion.Proposed, !data.IsPublic.IsValid ? data.IsPublic.DBValue : data.IsPublic.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@DateModified", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DateModified", DataRowVersion.Proposed, data.DateModified.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@LastModifiedFirstName", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "LastModifiedFirstName", DataRowVersion.Proposed, data.LastModifiedFirstName.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@LastModifiedLastName", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "LastModifiedLastName", DataRowVersion.Proposed, data.LastModifiedLastName.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@CreateFirstName", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "CreateFirstName", DataRowVersion.Proposed, data.CreateFirstName.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@CreateLastName", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "CreateLastName", DataRowVersion.Proposed, data.CreateLastName.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrgGroupsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgGroupsID", DataRowVersion.Proposed, data.OrgGroupsID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@NewsImage", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "NewsImage", DataRowVersion.Proposed, data.NewsImage.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@TemplateType", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "TemplateType", DataRowVersion.Proposed, data.TemplateType.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@NewsArticlesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "NewsArticlesID", DataRowVersion.Proposed, data.NewsArticlesID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Text", SqlDbType.Text, 0, ParameterDirection.Input, false, 0, 0, "Text", DataRowVersion.Proposed, data.Text.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@LastModifiedID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "LastModifiedID", DataRowVersion.Proposed, data.LastModifiedID.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Updates a record in the NewsView table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(NewsViewData data)
        {
            // Create and execute the command
	    NewsViewData oldData = Load ();
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.Title.Equals(data.Title))
	    {
		sql = sql + "Title=@Title,";
	    }
	    if (!oldData.Summary.Equals(data.Summary))
	    {
		sql = sql + "Summary=@Summary,";
	    }
	    if (!oldData.DateStart.Equals(data.DateStart))
	    {
		sql = sql + "DateStart=@DateStart,";
	    }
	    if (!oldData.DateEnd.Equals(data.DateEnd))
	    {
		sql = sql + "DateEnd=@DateEnd,";
	    }
	    if (!oldData.IsPublic.Equals(data.IsPublic))
	    {
		sql = sql + "IsPublic=@IsPublic,";
	    }
	    if (!oldData.DateModified.Equals(data.DateModified))
	    {
		sql = sql + "DateModified=@DateModified,";
	    }
	    if (!oldData.LastModifiedFirstName.Equals(data.LastModifiedFirstName))
	    {
		sql = sql + "LastModifiedFirstName=@LastModifiedFirstName,";
	    }
	    if (!oldData.LastModifiedLastName.Equals(data.LastModifiedLastName))
	    {
		sql = sql + "LastModifiedLastName=@LastModifiedLastName,";
	    }
	    if (!oldData.CreateFirstName.Equals(data.CreateFirstName))
	    {
		sql = sql + "CreateFirstName=@CreateFirstName,";
	    }
	    if (!oldData.CreateLastName.Equals(data.CreateLastName))
	    {
		sql = sql + "CreateLastName=@CreateLastName,";
	    }
	    if (!oldData.OrgGroupsID.Equals(data.OrgGroupsID))
	    {
		sql = sql + "OrgGroupsID=@OrgGroupsID,";
	    }
	    if (!oldData.NewsImage.Equals(data.NewsImage))
	    {
		sql = sql + "NewsImage=@NewsImage,";
	    }
	    if (!oldData.TemplateType.Equals(data.TemplateType))
	    {
		sql = sql + "TemplateType=@TemplateType,";
	    }
	    if (!oldData.NewsArticlesID.Equals(data.NewsArticlesID))
	    {
		sql = sql + "NewsArticlesID=@NewsArticlesID,";
	    }
	    if (!oldData.Text.Equals(data.Text))
	    {
		sql = sql + "Text=@Text,";
	    }
	    if (!oldData.LastModifiedID.Equals(data.LastModifiedID))
	    {
		sql = sql + "LastModifiedID=@LastModifiedID,";
	    }
	    WhereClause w = new WhereClause();
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.Title.Equals(data.Title))
	    {
		cmd.Parameters.Add(new SqlParameter("@Title", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Title", DataRowVersion.Proposed, data.Title.DBValue));

	    }
	    if (!oldData.Summary.Equals(data.Summary))
	    {
		cmd.Parameters.Add(new SqlParameter("@Summary", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "Summary", DataRowVersion.Proposed, data.Summary.DBValue));

	    }
	    if (!oldData.DateStart.Equals(data.DateStart))
	    {
		cmd.Parameters.Add(new SqlParameter("@DateStart", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DateStart", DataRowVersion.Proposed, data.DateStart.DBValue));

	    }
	    if (!oldData.DateEnd.Equals(data.DateEnd))
	    {
		cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DateEnd", DataRowVersion.Proposed, data.DateEnd.DBValue));

	    }
	    if (!oldData.IsPublic.Equals(data.IsPublic))
	    {
		cmd.Parameters.Add(new SqlParameter("@IsPublic", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsPublic", DataRowVersion.Proposed, !data.IsPublic.IsValid ? data.IsPublic.DBValue : data.IsPublic.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.DateModified.Equals(data.DateModified))
	    {
		cmd.Parameters.Add(new SqlParameter("@DateModified", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DateModified", DataRowVersion.Proposed, data.DateModified.DBValue));

	    }
	    if (!oldData.LastModifiedFirstName.Equals(data.LastModifiedFirstName))
	    {
		cmd.Parameters.Add(new SqlParameter("@LastModifiedFirstName", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "LastModifiedFirstName", DataRowVersion.Proposed, data.LastModifiedFirstName.DBValue));

	    }
	    if (!oldData.LastModifiedLastName.Equals(data.LastModifiedLastName))
	    {
		cmd.Parameters.Add(new SqlParameter("@LastModifiedLastName", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "LastModifiedLastName", DataRowVersion.Proposed, data.LastModifiedLastName.DBValue));

	    }
	    if (!oldData.CreateFirstName.Equals(data.CreateFirstName))
	    {
		cmd.Parameters.Add(new SqlParameter("@CreateFirstName", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "CreateFirstName", DataRowVersion.Proposed, data.CreateFirstName.DBValue));

	    }
	    if (!oldData.CreateLastName.Equals(data.CreateLastName))
	    {
		cmd.Parameters.Add(new SqlParameter("@CreateLastName", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "CreateLastName", DataRowVersion.Proposed, data.CreateLastName.DBValue));

	    }
	    if (!oldData.OrgGroupsID.Equals(data.OrgGroupsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgGroupsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgGroupsID", DataRowVersion.Proposed, data.OrgGroupsID.DBValue));

	    }
	    if (!oldData.NewsImage.Equals(data.NewsImage))
	    {
		cmd.Parameters.Add(new SqlParameter("@NewsImage", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "NewsImage", DataRowVersion.Proposed, data.NewsImage.DBValue));

	    }
	    if (!oldData.TemplateType.Equals(data.TemplateType))
	    {
		cmd.Parameters.Add(new SqlParameter("@TemplateType", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "TemplateType", DataRowVersion.Proposed, data.TemplateType.DBValue));

	    }
	    if (!oldData.NewsArticlesID.Equals(data.NewsArticlesID))
	    {
		cmd.Parameters.Add(new SqlParameter("@NewsArticlesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "NewsArticlesID", DataRowVersion.Proposed, data.NewsArticlesID.DBValue));

	    }
	    if (!oldData.Text.Equals(data.Text))
	    {
		cmd.Parameters.Add(new SqlParameter("@Text", SqlDbType.Text, 0, ParameterDirection.Input, false, 0, 0, "Text", DataRowVersion.Proposed, data.Text.DBValue));

	    }
	    if (!oldData.LastModifiedID.Equals(data.LastModifiedID))
	    {
		cmd.Parameters.Add(new SqlParameter("@LastModifiedID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "LastModifiedID", DataRowVersion.Proposed, data.LastModifiedID.DBValue));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the NewsView table by a composite primary key.
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