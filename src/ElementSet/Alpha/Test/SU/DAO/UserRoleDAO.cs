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
    
    
    public class UserRoleDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[UserRole]";
        
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
        static UserRoleDAO()
        {
            propertyToSqlMap.Add("UserRoleId",@"UserRoleId");
	    propertyToSqlMap.Add("UserId",@"UserId");
	    propertyToSqlMap.Add("RoleId",@"RoleId");
	    propertyToSqlMap.Add("PermitDeny",@"PermitDeny");
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
        /// Returns a list of all UserRole rows.
        /// </summary>
        /// <returns>List of UserRoleData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static UserRoleList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of UserRole rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of UserRoleData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static UserRoleList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of UserRole rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of UserRoleData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static UserRoleList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of UserRole rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of UserRoleData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static UserRoleList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.ORDERDB, TABLE, whereClause, orderByClause, true);
	    UserRoleList list = new UserRoleList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a UserRole entity using it's primary key.
        /// </summary>
        /// <param name="UserRoleId">A key field.</param>
        /// <returns>A UserRoleData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static UserRoleData Load(IdType userRoleId)
        {
            WhereClause w = new WhereClause();
	    w.And("UserRoleId", userRoleId.DBValue);
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.ORDERDB, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for UserRole.");
	    }
	    UserRoleData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static UserRoleData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            UserRoleData data = new UserRoleData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("UserRoleId")))
	    {
		data.UserRoleId = IdType.UNSET;
	    }
	    else
	    {
		data.UserRoleId = new IdType (dataReader.GetInt32(dataReader.GetOrdinal("UserRoleId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("UserId")))
	    {
		data.UserId = IdType.UNSET;
	    }
	    else
	    {
		data.UserId = new IdType (dataReader.GetInt32(dataReader.GetOrdinal("UserId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("RoleId")))
	    {
		data.RoleId = RoleEnum.UNSET;
	    }
	    else
	    {
		data.RoleId = RoleEnum.GetInstance(dataReader.GetInt32(dataReader.GetOrdinal("RoleId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("PermitDeny")))
	    {
		data.PermitDeny = PermitDenyEnum.UNSET;
	    }
	    else
	    {
		data.PermitDeny = PermitDenyEnum.GetInstance(dataReader.GetInt32(dataReader.GetOrdinal("PermitDeny")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the UserRole table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static IdType Insert(UserRoleData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "UserId,"
	    + "RoleId,"
	    + "PermitDeny,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@UserId,"
	    + "@RoleId,"
	    + "@PermitDeny,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ");select Scope_Identity() Id";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.ORDERDB, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "UserId", DataRowVersion.Proposed, data.UserId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@RoleId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "RoleId", DataRowVersion.Proposed, data.RoleId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@PermitDeny", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "PermitDeny", DataRowVersion.Proposed, data.PermitDeny.DBValue));

	    // Execute the query
	    SqlDataReader returnValue = cmd.ExecuteReader();
	    returnValue.Read();
	    int returnId = (int)(returnValue.GetDecimal(0));
	    returnValue.Close();
	    // Set the output paramter value(s)
	    return new IdType (returnId);
        }
        
        /// <summary>
        /// Updates a record in the UserRole table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(UserRoleData data)
        {
            // Create and execute the command
	    UserRoleData oldData = Load ( data.UserRoleId);
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.UserId.Equals(data.UserId))
	    {
		sql = sql + "UserId=@UserId,";
	    }
	    if (!oldData.RoleId.Equals(data.RoleId))
	    {
		sql = sql + "RoleId=@RoleId,";
	    }
	    if (!oldData.PermitDeny.Equals(data.PermitDeny))
	    {
		sql = sql + "PermitDeny=@PermitDeny,";
	    }
	    WhereClause w = new WhereClause();
	    w.And("UserRoleId", data.UserRoleId.DBValue);
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.ORDERDB, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.UserRoleId.Equals(data.UserRoleId))
	    {
		cmd.Parameters.Add(new SqlParameter("@UserRoleId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "UserRoleId", DataRowVersion.Proposed, data.UserRoleId.DBValue));

	    }
	    if (!oldData.UserId.Equals(data.UserId))
	    {
		cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "UserId", DataRowVersion.Proposed, data.UserId.DBValue));

	    }
	    if (!oldData.RoleId.Equals(data.RoleId))
	    {
		cmd.Parameters.Add(new SqlParameter("@RoleId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "RoleId", DataRowVersion.Proposed, data.RoleId.DBValue));

	    }
	    if (!oldData.PermitDeny.Equals(data.PermitDeny))
	    {
		cmd.Parameters.Add(new SqlParameter("@PermitDeny", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "PermitDeny", DataRowVersion.Proposed, data.PermitDeny.DBValue));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the UserRole table by UserRoleId.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Delete(IdType userRoleId)
        {
            // Create and execute the command
	    string sql = "Delete From " + TABLE;
	    WhereClause w = new WhereClause();
	    w.And("UserRoleId", userRoleId.DBValue);
	    sql += w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.ORDERDB, sql, CommandType.Text, COMMAND_TIMEOUT);

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Returns a list of objects which match the values for the fields specified.
        /// </summary>
        /// <param name="UserId">A field value to be matched.</param>
        /// <returns>The list of UserRoleDAO objects found.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static UserRoleList FindByUserId(IdType userId)
        {
            OrderByClause sort = new OrderByClause("UserId");
	    WhereClause filter = new WhereClause();
	    filter.And("UserId", userId.DBValue);

	    return GetList(filter, sort);
        }
    }
}