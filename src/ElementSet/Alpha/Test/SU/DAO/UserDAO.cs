using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Spring2.Core.DAO;
using Spring2.Core.Types;
using StampinUp.DataObject;
using StampinUp.Types;
using Spring2.DataTierGenerator.Attribute;
using StampinUp.Core.DAO;
using StampinUp.Core.Types;
using StampinUp.Core.Util;

namespace StampinUp.Dao
{
    
    
    public class UserDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[User]";
        
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
        static UserDAO()
        {
            propertyToSqlMap.Add("UserId",@"UserId");
	    propertyToSqlMap.Add("UserLogin",@"UserLogin");
	    propertyToSqlMap.Add("UserLocale",@"UserLocale");
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
        /// Returns a list of all User rows.
        /// </summary>
        /// <returns>List of UserData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static UserList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of User rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of UserData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static UserList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of User rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of UserData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static UserList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of User rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of UserData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static UserList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.ORDERDB, TABLE, whereClause, orderByClause, true);
	    UserList list = new UserList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a User entity using it's primary key.
        /// </summary>
        /// <param name="UserId">A key field.</param>
        /// <returns>A UserData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static UserData Load(IdType userId)
        {
            WhereClause w = new WhereClause();
	    w.And("UserId", userId.DBValue);
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.ORDERDB, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for User.");
	    }
	    UserData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static UserData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            UserData data = new UserData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("UserId")))
	    {
		data.UserId = IdType.UNSET;
	    }
	    else
	    {
		data.UserId = new IdType (dataReader.GetInt32(dataReader.GetOrdinal("UserId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("UserLogin")))
	    {
		data.UserLogin = StringType.UNSET;
	    }
	    else
	    {
		data.UserLogin = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("UserLogin")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("UserLocale")))
	    {
		data.UserLocale = LocaleEnum.UNSET;
	    }
	    else
	    {
		data.UserLocale = LocaleEnum.GetInstance(dataReader.GetInt32(dataReader.GetOrdinal("UserLocale")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the User table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static IdType Insert(UserData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "UserLogin,"
	    + "UserLocale,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@UserLogin,"
	    + "@UserLocale,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ");select Scope_Identity() Id";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.ORDERDB, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@UserLogin", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "UserLogin", DataRowVersion.Proposed, data.UserLogin.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@UserLocale", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "UserLocale", DataRowVersion.Proposed, data.UserLocale.DBValue));

	    // Execute the query
	    SqlDataReader returnValue = cmd.ExecuteReader();
	    returnValue.Read();
	    int returnId = (int)(returnValue.GetDecimal(0));
	    returnValue.Close();
	    // Set the output paramter value(s)
	    return new IdType (returnId);
        }
        
        /// <summary>
        /// Updates a record in the User table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(UserData data)
        {
            // Create and execute the command
	    UserData oldData = Load ( data.UserId);
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.UserLogin.Equals(data.UserLogin))
	    {
		sql = sql + "UserLogin=@UserLogin,";
	    }
	    if (!oldData.UserLocale.Equals(data.UserLocale))
	    {
		sql = sql + "UserLocale=@UserLocale,";
	    }
	    WhereClause w = new WhereClause();
	    w.And("UserId", data.UserId.DBValue);
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.ORDERDB, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.UserId.Equals(data.UserId))
	    {
		cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "UserId", DataRowVersion.Proposed, data.UserId.DBValue));

	    }
	    if (!oldData.UserLogin.Equals(data.UserLogin))
	    {
		cmd.Parameters.Add(new SqlParameter("@UserLogin", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "UserLogin", DataRowVersion.Proposed, data.UserLogin.DBValue));

	    }
	    if (!oldData.UserLocale.Equals(data.UserLocale))
	    {
		cmd.Parameters.Add(new SqlParameter("@UserLocale", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "UserLocale", DataRowVersion.Proposed, data.UserLocale.DBValue));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the User table by UserId.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Delete(IdType userId)
        {
            // Create and execute the command
	    string sql = "Delete From " + TABLE;
	    WhereClause w = new WhereClause();
	    w.And("UserId", userId.DBValue);
	    sql += w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.ORDERDB, sql, CommandType.Text, COMMAND_TIMEOUT);

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Returns a list of objects which match the values for the fields specified.
        /// </summary>
        /// <param name="UserLogin">A field value to be matched.</param>
        /// <returns>The list of UserDAO objects found.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static UserList FindByLogin(StringType userLogin)
        {
            OrderByClause sort = new OrderByClause("UserLogin");
	    WhereClause filter = new WhereClause();
	    filter.And("UserLogin", userLogin.DBValue);

	    return GetList(filter, sort);
        }
    }
}