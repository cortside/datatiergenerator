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
    
    
    public class MenuViewDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[MenuView]";
        
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
        static MenuViewDAO()
        {
            propertyToSqlMap.Add("ParentMenuItemsID",@"ParentMenuItemsID");
	    propertyToSqlMap.Add("Description",@"Description");
	    propertyToSqlMap.Add("Url",@"url");
	    propertyToSqlMap.Add("SortOrder",@"SortOrder");
	    propertyToSqlMap.Add("Target",@"Target");
	    propertyToSqlMap.Add("DateStart",@"DateStart");
	    propertyToSqlMap.Add("DateEnd",@"DateEnd");
	    propertyToSqlMap.Add("NodeImgSrc",@"NodeImgSrc");
	    propertyToSqlMap.Add("MenuItemsID",@"MenuItemsID");
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
        /// Returns a list of all MenuView rows.
        /// </summary>
        /// <returns>List of MenuViewData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static MenuViewList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of MenuView rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of MenuViewData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static MenuViewList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of MenuView rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of MenuViewData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static MenuViewList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of MenuView rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of MenuViewData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static MenuViewList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, whereClause, orderByClause, true);
	    MenuViewList list = new MenuViewList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a MenuView entity using it's primary key.
        /// </summary>
        /// <returns>A MenuViewData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static MenuViewData Load()
        {
            WhereClause w = new WhereClause();
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for MenuView.");
	    }
	    MenuViewData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static MenuViewData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            MenuViewData data = new MenuViewData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ParentMenuItemsID")))
	    {
		data.ParentMenuItemsID = IntegerType.UNSET;
	    }
	    else
	    {
		data.ParentMenuItemsID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("ParentMenuItemsID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Description")))
	    {
		data.Description = StringType.UNSET;
	    }
	    else
	    {
		data.Description = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Description")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("url")))
	    {
		data.Url = StringType.UNSET;
	    }
	    else
	    {
		data.Url = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("url")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("SortOrder")))
	    {
		data.SortOrder = IntegerType.UNSET;
	    }
	    else
	    {
		data.SortOrder = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("SortOrder")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Target")))
	    {
		data.Target = StringType.UNSET;
	    }
	    else
	    {
		data.Target = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Target")));
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
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("NodeImgSrc")))
	    {
		data.NodeImgSrc = StringType.UNSET;
	    }
	    else
	    {
		data.NodeImgSrc = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("NodeImgSrc")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("MenuItemsID")))
	    {
		data.MenuItemsID = IntegerType.UNSET;
	    }
	    else
	    {
		data.MenuItemsID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("MenuItemsID")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the MenuView table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Insert(MenuViewData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "ParentMenuItemsID,"
	    + "Description,"
	    + "url,"
	    + "SortOrder,"
	    + "Target,"
	    + "DateStart,"
	    + "DateEnd,"
	    + "NodeImgSrc,"
	    + "MenuItemsID,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@ParentMenuItemsID,"
	    + "@Description,"
	    + "@url,"
	    + "@SortOrder,"
	    + "@Target,"
	    + "@DateStart,"
	    + "@DateEnd,"
	    + "@NodeImgSrc,"
	    + "@MenuItemsID,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ")";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@ParentMenuItemsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ParentMenuItemsID", DataRowVersion.Proposed, data.ParentMenuItemsID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@url", SqlDbType.VarChar, 300, ParameterDirection.Input, false, 0, 0, "url", DataRowVersion.Proposed, data.Url.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@SortOrder", SqlDbType.TinyInt, 0, ParameterDirection.Input, false, 0, 0, "SortOrder", DataRowVersion.Proposed, data.SortOrder.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Target", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "Target", DataRowVersion.Proposed, data.Target.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DateStart", SqlDbType.SmallDateTime, 0, ParameterDirection.Input, false, 0, 0, "DateStart", DataRowVersion.Proposed, data.DateStart.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.SmallDateTime, 0, ParameterDirection.Input, false, 0, 0, "DateEnd", DataRowVersion.Proposed, data.DateEnd.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@NodeImgSrc", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "NodeImgSrc", DataRowVersion.Proposed, data.NodeImgSrc.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@MenuItemsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "MenuItemsID", DataRowVersion.Proposed, data.MenuItemsID.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Updates a record in the MenuView table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(MenuViewData data)
        {
            // Create and execute the command
	    MenuViewData oldData = Load ();
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.ParentMenuItemsID.Equals(data.ParentMenuItemsID))
	    {
		sql = sql + "ParentMenuItemsID=@ParentMenuItemsID,";
	    }
	    if (!oldData.Description.Equals(data.Description))
	    {
		sql = sql + "Description=@Description,";
	    }
	    if (!oldData.Url.Equals(data.Url))
	    {
		sql = sql + "url=@url,";
	    }
	    if (!oldData.SortOrder.Equals(data.SortOrder))
	    {
		sql = sql + "SortOrder=@SortOrder,";
	    }
	    if (!oldData.Target.Equals(data.Target))
	    {
		sql = sql + "Target=@Target,";
	    }
	    if (!oldData.DateStart.Equals(data.DateStart))
	    {
		sql = sql + "DateStart=@DateStart,";
	    }
	    if (!oldData.DateEnd.Equals(data.DateEnd))
	    {
		sql = sql + "DateEnd=@DateEnd,";
	    }
	    if (!oldData.NodeImgSrc.Equals(data.NodeImgSrc))
	    {
		sql = sql + "NodeImgSrc=@NodeImgSrc,";
	    }
	    if (!oldData.MenuItemsID.Equals(data.MenuItemsID))
	    {
		sql = sql + "MenuItemsID=@MenuItemsID,";
	    }
	    WhereClause w = new WhereClause();
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.ParentMenuItemsID.Equals(data.ParentMenuItemsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@ParentMenuItemsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ParentMenuItemsID", DataRowVersion.Proposed, data.ParentMenuItemsID.DBValue));

	    }
	    if (!oldData.Description.Equals(data.Description))
	    {
		cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));

	    }
	    if (!oldData.Url.Equals(data.Url))
	    {
		cmd.Parameters.Add(new SqlParameter("@url", SqlDbType.VarChar, 300, ParameterDirection.Input, false, 0, 0, "url", DataRowVersion.Proposed, data.Url.DBValue));

	    }
	    if (!oldData.SortOrder.Equals(data.SortOrder))
	    {
		cmd.Parameters.Add(new SqlParameter("@SortOrder", SqlDbType.TinyInt, 0, ParameterDirection.Input, false, 0, 0, "SortOrder", DataRowVersion.Proposed, data.SortOrder.DBValue));

	    }
	    if (!oldData.Target.Equals(data.Target))
	    {
		cmd.Parameters.Add(new SqlParameter("@Target", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "Target", DataRowVersion.Proposed, data.Target.DBValue));

	    }
	    if (!oldData.DateStart.Equals(data.DateStart))
	    {
		cmd.Parameters.Add(new SqlParameter("@DateStart", SqlDbType.SmallDateTime, 0, ParameterDirection.Input, false, 0, 0, "DateStart", DataRowVersion.Proposed, data.DateStart.DBValue));

	    }
	    if (!oldData.DateEnd.Equals(data.DateEnd))
	    {
		cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.SmallDateTime, 0, ParameterDirection.Input, false, 0, 0, "DateEnd", DataRowVersion.Proposed, data.DateEnd.DBValue));

	    }
	    if (!oldData.NodeImgSrc.Equals(data.NodeImgSrc))
	    {
		cmd.Parameters.Add(new SqlParameter("@NodeImgSrc", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "NodeImgSrc", DataRowVersion.Proposed, data.NodeImgSrc.DBValue));

	    }
	    if (!oldData.MenuItemsID.Equals(data.MenuItemsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@MenuItemsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "MenuItemsID", DataRowVersion.Proposed, data.MenuItemsID.DBValue));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the MenuView table by a composite primary key.
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