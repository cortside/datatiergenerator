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
    
    
    public class WebConfigDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[WebConfig]";
        
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
        static WebConfigDAO()
        {
            propertyToSqlMap.Add("WebConfigID",@"WebConfigID");
	    propertyToSqlMap.Add("CreateDate",@"CreateDate");
	    propertyToSqlMap.Add("LeftNavBGColor",@"LeftNavBGColor");
	    propertyToSqlMap.Add("HeaderTextColor",@"HeaderTextColor");
	    propertyToSqlMap.Add("MainPic",@"MainPic");
	    propertyToSqlMap.Add("LeftNavPic",@"LeftNavPic");
	    propertyToSqlMap.Add("Active",@"Active");
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
        /// Returns a list of all WebConfig rows.
        /// </summary>
        /// <returns>List of WebConfigData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static WebConfigList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of WebConfig rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of WebConfigData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static WebConfigList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of WebConfig rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of WebConfigData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static WebConfigList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of WebConfig rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of WebConfigData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static WebConfigList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.INTERNET, TABLE, whereClause, orderByClause, true);
	    WebConfigList list = new WebConfigList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a WebConfig entity using it's primary key.
        /// </summary>
        /// <param name="WebConfigID">A key field.</param>
        /// <returns>A WebConfigData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static WebConfigData Load(IdType webConfigID)
        {
            WhereClause w = new WhereClause();
	    w.And("WebConfigID", webConfigID.DBValue);
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTERNET, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for WebConfig.");
	    }
	    WebConfigData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static WebConfigData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            WebConfigData data = new WebConfigData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("WebConfigID")))
	    {
		data.WebConfigID = IdType.UNSET;
	    }
	    else
	    {
		data.WebConfigID = new IdType (dataReader.GetInt32(dataReader.GetOrdinal("WebConfigID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("CreateDate")))
	    {
		data.CreateDate = DateType.UNSET;
	    }
	    else
	    {
		data.CreateDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("CreateDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("LeftNavBGColor")))
	    {
		data.LeftNavBGColor = StringType.UNSET;
	    }
	    else
	    {
		data.LeftNavBGColor = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("LeftNavBGColor")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("HeaderTextColor")))
	    {
		data.HeaderTextColor = StringType.UNSET;
	    }
	    else
	    {
		data.HeaderTextColor = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("HeaderTextColor")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("MainPic")))
	    {
		data.MainPic = StringType.UNSET;
	    }
	    else
	    {
		data.MainPic = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("MainPic")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("LeftNavPic")))
	    {
		data.LeftNavPic = StringType.UNSET;
	    }
	    else
	    {
		data.LeftNavPic = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("LeftNavPic")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Active")))
	    {
		data.Active = BooleanType.UNSET;
	    }
	    else
	    {
		data.Active = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("Active")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the WebConfig table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static IdType Insert(WebConfigData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "CreateDate,"
	    + "LeftNavBGColor,"
	    + "HeaderTextColor,"
	    + "MainPic,"
	    + "LeftNavPic,"
	    + "Active,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@CreateDate,"
	    + "@LeftNavBGColor,"
	    + "@HeaderTextColor,"
	    + "@MainPic,"
	    + "@LeftNavPic,"
	    + "@Active,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ");select Scope_Identity() Id";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTERNET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@CreateDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "CreateDate", DataRowVersion.Proposed, data.CreateDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@LeftNavBGColor", SqlDbType.NVarChar, 12, ParameterDirection.Input, false, 0, 0, "LeftNavBGColor", DataRowVersion.Proposed, data.LeftNavBGColor.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@HeaderTextColor", SqlDbType.NVarChar, 12, ParameterDirection.Input, false, 0, 0, "HeaderTextColor", DataRowVersion.Proposed, data.HeaderTextColor.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@MainPic", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "MainPic", DataRowVersion.Proposed, data.MainPic.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@LeftNavPic", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "LeftNavPic", DataRowVersion.Proposed, data.LeftNavPic.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Active", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "Active", DataRowVersion.Proposed, !data.Active.IsValid ? data.Active.DBValue : data.Active.DBValue.Equals ("Y") ? 1 : 0));

	    // Execute the query
	    SqlDataReader returnValue = cmd.ExecuteReader();
	    returnValue.Read();
	    int returnId = (int)(returnValue.GetDecimal(0));
	    returnValue.Close();
	    // Set the output paramter value(s)
	    return new IdType (returnId);
        }
        
        /// <summary>
        /// Updates a record in the WebConfig table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(WebConfigData data)
        {
            // Create and execute the command
	    WebConfigData oldData = Load ( data.WebConfigID);
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.CreateDate.Equals(data.CreateDate))
	    {
		sql = sql + "CreateDate=@CreateDate,";
	    }
	    if (!oldData.LeftNavBGColor.Equals(data.LeftNavBGColor))
	    {
		sql = sql + "LeftNavBGColor=@LeftNavBGColor,";
	    }
	    if (!oldData.HeaderTextColor.Equals(data.HeaderTextColor))
	    {
		sql = sql + "HeaderTextColor=@HeaderTextColor,";
	    }
	    if (!oldData.MainPic.Equals(data.MainPic))
	    {
		sql = sql + "MainPic=@MainPic,";
	    }
	    if (!oldData.LeftNavPic.Equals(data.LeftNavPic))
	    {
		sql = sql + "LeftNavPic=@LeftNavPic,";
	    }
	    if (!oldData.Active.Equals(data.Active))
	    {
		sql = sql + "Active=@Active,";
	    }
	    WhereClause w = new WhereClause();
	    w.And("WebConfigID", data.WebConfigID.DBValue);
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTERNET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.WebConfigID.Equals(data.WebConfigID))
	    {
		cmd.Parameters.Add(new SqlParameter("@WebConfigID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "WebConfigID", DataRowVersion.Proposed, data.WebConfigID.DBValue));

	    }
	    if (!oldData.CreateDate.Equals(data.CreateDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@CreateDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "CreateDate", DataRowVersion.Proposed, data.CreateDate.DBValue));

	    }
	    if (!oldData.LeftNavBGColor.Equals(data.LeftNavBGColor))
	    {
		cmd.Parameters.Add(new SqlParameter("@LeftNavBGColor", SqlDbType.NVarChar, 12, ParameterDirection.Input, false, 0, 0, "LeftNavBGColor", DataRowVersion.Proposed, data.LeftNavBGColor.DBValue));

	    }
	    if (!oldData.HeaderTextColor.Equals(data.HeaderTextColor))
	    {
		cmd.Parameters.Add(new SqlParameter("@HeaderTextColor", SqlDbType.NVarChar, 12, ParameterDirection.Input, false, 0, 0, "HeaderTextColor", DataRowVersion.Proposed, data.HeaderTextColor.DBValue));

	    }
	    if (!oldData.MainPic.Equals(data.MainPic))
	    {
		cmd.Parameters.Add(new SqlParameter("@MainPic", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "MainPic", DataRowVersion.Proposed, data.MainPic.DBValue));

	    }
	    if (!oldData.LeftNavPic.Equals(data.LeftNavPic))
	    {
		cmd.Parameters.Add(new SqlParameter("@LeftNavPic", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "LeftNavPic", DataRowVersion.Proposed, data.LeftNavPic.DBValue));

	    }
	    if (!oldData.Active.Equals(data.Active))
	    {
		cmd.Parameters.Add(new SqlParameter("@Active", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "Active", DataRowVersion.Proposed, !data.Active.IsValid ? data.Active.DBValue : data.Active.DBValue.Equals ("Y") ? 1 : 0));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the WebConfig table by WebConfigID.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Delete(IdType webConfigID)
        {
            // Create and execute the command
	    string sql = "Delete From " + TABLE;
	    WhereClause w = new WhereClause();
	    w.And("WebConfigID", webConfigID.DBValue);
	    sql += w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTERNET, sql, CommandType.Text, COMMAND_TIMEOUT);

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Returns an object which matches the values for the fields specified.
        /// </summary>
        /// <param name="CreateDate">A field value to be matched.</param>
        /// <param name="Active">A field value to be matched.</param>
        /// <returns>The object found.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static WebConfigData FindActiveEntry(DateType createDate, BooleanType active)
        {
            OrderByClause sort = new OrderByClause("CreateDate, Active");
	    WhereClause filter = new WhereClause();
	    filter.And("CreateDate", createDate.DBValue);
	    filter.And("Active", !active.IsValid ? active.DBValue : active.DBValue.Equals ("Y") ? 1 : 0);
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTERNET, TABLE, filter, sort, true);

	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("WebConfigData.FindActiveEntry found no rows.");
	    }
	    WebConfigData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
    }
}