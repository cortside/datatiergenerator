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
    
    
    public class GroupRoleDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[GroupRole]";
        
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
        static GroupRoleDAO()
        {
            propertyToSqlMap.Add("GroupRoleId",@"GroupRoleId");
	    propertyToSqlMap.Add("GroupId",@"GroupId");
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
        /// Returns a list of all GroupRole rows.
        /// </summary>
        /// <returns>List of GroupRoleData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static GroupRoleList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of GroupRole rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of GroupRoleData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static GroupRoleList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of GroupRole rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of GroupRoleData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static GroupRoleList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of GroupRole rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of GroupRoleData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static GroupRoleList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.ORDERDB, TABLE, whereClause, orderByClause, true);
	    GroupRoleList list = new GroupRoleList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a GroupRole entity using it's primary key.
        /// </summary>
        /// <param name="GroupRoleId">A key field.</param>
        /// <returns>A GroupRoleData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static GroupRoleData Load(IdType groupRoleId)
        {
            WhereClause w = new WhereClause();
	    w.And("GroupRoleId", groupRoleId.DBValue);
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.ORDERDB, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for GroupRole.");
	    }
	    GroupRoleData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static GroupRoleData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            GroupRoleData data = new GroupRoleData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("GroupRoleId")))
	    {
		data.GroupRoleId = IdType.UNSET;
	    }
	    else
	    {
		data.GroupRoleId = new IdType (dataReader.GetInt32(dataReader.GetOrdinal("GroupRoleId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("GroupId")))
	    {
		data.GroupId = IdType.UNSET;
	    }
	    else
	    {
		data.GroupId = new IdType (dataReader.GetInt32(dataReader.GetOrdinal("GroupId")));
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
        /// Inserts a record into the GroupRole table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static IdType Insert(GroupRoleData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "GroupId,"
	    + "RoleId,"
	    + "PermitDeny,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@GroupId,"
	    + "@RoleId,"
	    + "@PermitDeny,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ");select Scope_Identity() Id";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.ORDERDB, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@GroupId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "GroupId", DataRowVersion.Proposed, data.GroupId.DBValue));
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
        /// Updates a record in the GroupRole table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(GroupRoleData data)
        {
            // Create and execute the command
	    GroupRoleData oldData = Load ( data.GroupRoleId);
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.GroupId.Equals(data.GroupId))
	    {
		sql = sql + "GroupId=@GroupId,";
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
	    w.And("GroupRoleId", data.GroupRoleId.DBValue);
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.ORDERDB, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.GroupRoleId.Equals(data.GroupRoleId))
	    {
		cmd.Parameters.Add(new SqlParameter("@GroupRoleId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "GroupRoleId", DataRowVersion.Proposed, data.GroupRoleId.DBValue));

	    }
	    if (!oldData.GroupId.Equals(data.GroupId))
	    {
		cmd.Parameters.Add(new SqlParameter("@GroupId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "GroupId", DataRowVersion.Proposed, data.GroupId.DBValue));

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
        /// Deletes a record from the GroupRole table by GroupRoleId.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Delete(IdType groupRoleId)
        {
            // Create and execute the command
	    string sql = "Delete From " + TABLE;
	    WhereClause w = new WhereClause();
	    w.And("GroupRoleId", groupRoleId.DBValue);
	    sql += w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.ORDERDB, sql, CommandType.Text, COMMAND_TIMEOUT);

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Returns a list of objects which match the values for the fields specified.
        /// </summary>
        /// <param name="GroupId">A field value to be matched.</param>
        /// <returns>The list of GroupRoleDAO objects found.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static GroupRoleList FindByGroupId(IdType groupId)
        {
            OrderByClause sort = new OrderByClause("GroupId");
	    WhereClause filter = new WhereClause();
	    filter.And("GroupId", groupId.DBValue);

	    return GetList(filter, sort);
        }
    }
}