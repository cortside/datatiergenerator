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
    
    
    public class GroupEmployeeViewDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[GroupEmployeeView]";
        
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
        static GroupEmployeeViewDAO()
        {
            propertyToSqlMap.Add("FirstName",@"FirstName");
	    propertyToSqlMap.Add("LastName",@"LastName");
	    propertyToSqlMap.Add("DepartmentName",@"DepartmentName");
	    propertyToSqlMap.Add("GroupIsActive",@"GroupIsActive");
	    propertyToSqlMap.Add("OrgGroupsID",@"OrgGroupsID");
	    propertyToSqlMap.Add("LocationName",@"LocationName");
	    propertyToSqlMap.Add("EmployeeTitle",@"EmployeeTitle");
	    propertyToSqlMap.Add("EmployeeIsActive",@"EmployeeIsActive");
	    propertyToSqlMap.Add("OrgGroupsEmployeesID",@"OrgGroupsEmployeesID");
	    propertyToSqlMap.Add("OrgEmployeesID",@"OrgEmployeesID");
	    propertyToSqlMap.Add("Email",@"Email");
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
        /// Returns a list of all GroupEmployeeView rows.
        /// </summary>
        /// <returns>List of GroupEmployeeViewData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static GroupEmployeeViewList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of GroupEmployeeView rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of GroupEmployeeViewData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static GroupEmployeeViewList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of GroupEmployeeView rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of GroupEmployeeViewData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static GroupEmployeeViewList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of GroupEmployeeView rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of GroupEmployeeViewData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static GroupEmployeeViewList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, whereClause, orderByClause, true);
	    GroupEmployeeViewList list = new GroupEmployeeViewList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a GroupEmployeeView entity using it's primary key.
        /// </summary>
        /// <returns>A GroupEmployeeViewData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static GroupEmployeeViewData Load()
        {
            WhereClause w = new WhereClause();
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for GroupEmployeeView.");
	    }
	    GroupEmployeeViewData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static GroupEmployeeViewData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            GroupEmployeeViewData data = new GroupEmployeeViewData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("FirstName")))
	    {
		data.FirstName = StringType.UNSET;
	    }
	    else
	    {
		data.FirstName = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("FirstName")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("LastName")))
	    {
		data.LastName = StringType.UNSET;
	    }
	    else
	    {
		data.LastName = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("LastName")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("DepartmentName")))
	    {
		data.DepartmentName = StringType.UNSET;
	    }
	    else
	    {
		data.DepartmentName = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("DepartmentName")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("GroupIsActive")))
	    {
		data.GroupIsActive = BooleanType.UNSET;
	    }
	    else
	    {
		data.GroupIsActive = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("GroupIsActive")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrgGroupsID")))
	    {
		data.OrgGroupsID = IntegerType.UNSET;
	    }
	    else
	    {
		data.OrgGroupsID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("OrgGroupsID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("LocationName")))
	    {
		data.LocationName = StringType.UNSET;
	    }
	    else
	    {
		data.LocationName = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("LocationName")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("EmployeeTitle")))
	    {
		data.EmployeeTitle = StringType.UNSET;
	    }
	    else
	    {
		data.EmployeeTitle = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("EmployeeTitle")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("EmployeeIsActive")))
	    {
		data.EmployeeIsActive = BooleanType.UNSET;
	    }
	    else
	    {
		data.EmployeeIsActive = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("EmployeeIsActive")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrgGroupsEmployeesID")))
	    {
		data.OrgGroupsEmployeesID = IntegerType.UNSET;
	    }
	    else
	    {
		data.OrgGroupsEmployeesID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("OrgGroupsEmployeesID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrgEmployeesID")))
	    {
		data.OrgEmployeesID = IntegerType.UNSET;
	    }
	    else
	    {
		data.OrgEmployeesID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("OrgEmployeesID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Email")))
	    {
		data.Email = StringType.UNSET;
	    }
	    else
	    {
		data.Email = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Email")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the GroupEmployeeView table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Insert(GroupEmployeeViewData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "FirstName,"
	    + "LastName,"
	    + "DepartmentName,"
	    + "GroupIsActive,"
	    + "OrgGroupsID,"
	    + "LocationName,"
	    + "EmployeeTitle,"
	    + "EmployeeIsActive,"
	    + "OrgGroupsEmployeesID,"
	    + "OrgEmployeesID,"
	    + "Email,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@FirstName,"
	    + "@LastName,"
	    + "@DepartmentName,"
	    + "@GroupIsActive,"
	    + "@OrgGroupsID,"
	    + "@LocationName,"
	    + "@EmployeeTitle,"
	    + "@EmployeeIsActive,"
	    + "@OrgGroupsEmployeesID,"
	    + "@OrgEmployeesID,"
	    + "@Email,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ")";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "FirstName", DataRowVersion.Proposed, data.FirstName.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "LastName", DataRowVersion.Proposed, data.LastName.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DepartmentName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "DepartmentName", DataRowVersion.Proposed, data.DepartmentName.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@GroupIsActive", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "GroupIsActive", DataRowVersion.Proposed, !data.GroupIsActive.IsValid ? data.GroupIsActive.DBValue : data.GroupIsActive.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@OrgGroupsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgGroupsID", DataRowVersion.Proposed, data.OrgGroupsID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@LocationName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "LocationName", DataRowVersion.Proposed, data.LocationName.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@EmployeeTitle", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "EmployeeTitle", DataRowVersion.Proposed, data.EmployeeTitle.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@EmployeeIsActive", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "EmployeeIsActive", DataRowVersion.Proposed, !data.EmployeeIsActive.IsValid ? data.EmployeeIsActive.DBValue : data.EmployeeIsActive.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@OrgGroupsEmployeesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgGroupsEmployeesID", DataRowVersion.Proposed, data.OrgGroupsEmployeesID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrgEmployeesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgEmployeesID", DataRowVersion.Proposed, data.OrgEmployeesID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "Email", DataRowVersion.Proposed, data.Email.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Updates a record in the GroupEmployeeView table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(GroupEmployeeViewData data)
        {
            // Create and execute the command
	    GroupEmployeeViewData oldData = Load ();
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.FirstName.Equals(data.FirstName))
	    {
		sql = sql + "FirstName=@FirstName,";
	    }
	    if (!oldData.LastName.Equals(data.LastName))
	    {
		sql = sql + "LastName=@LastName,";
	    }
	    if (!oldData.DepartmentName.Equals(data.DepartmentName))
	    {
		sql = sql + "DepartmentName=@DepartmentName,";
	    }
	    if (!oldData.GroupIsActive.Equals(data.GroupIsActive))
	    {
		sql = sql + "GroupIsActive=@GroupIsActive,";
	    }
	    if (!oldData.OrgGroupsID.Equals(data.OrgGroupsID))
	    {
		sql = sql + "OrgGroupsID=@OrgGroupsID,";
	    }
	    if (!oldData.LocationName.Equals(data.LocationName))
	    {
		sql = sql + "LocationName=@LocationName,";
	    }
	    if (!oldData.EmployeeTitle.Equals(data.EmployeeTitle))
	    {
		sql = sql + "EmployeeTitle=@EmployeeTitle,";
	    }
	    if (!oldData.EmployeeIsActive.Equals(data.EmployeeIsActive))
	    {
		sql = sql + "EmployeeIsActive=@EmployeeIsActive,";
	    }
	    if (!oldData.OrgGroupsEmployeesID.Equals(data.OrgGroupsEmployeesID))
	    {
		sql = sql + "OrgGroupsEmployeesID=@OrgGroupsEmployeesID,";
	    }
	    if (!oldData.OrgEmployeesID.Equals(data.OrgEmployeesID))
	    {
		sql = sql + "OrgEmployeesID=@OrgEmployeesID,";
	    }
	    if (!oldData.Email.Equals(data.Email))
	    {
		sql = sql + "Email=@Email,";
	    }
	    WhereClause w = new WhereClause();
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.FirstName.Equals(data.FirstName))
	    {
		cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "FirstName", DataRowVersion.Proposed, data.FirstName.DBValue));

	    }
	    if (!oldData.LastName.Equals(data.LastName))
	    {
		cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "LastName", DataRowVersion.Proposed, data.LastName.DBValue));

	    }
	    if (!oldData.DepartmentName.Equals(data.DepartmentName))
	    {
		cmd.Parameters.Add(new SqlParameter("@DepartmentName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "DepartmentName", DataRowVersion.Proposed, data.DepartmentName.DBValue));

	    }
	    if (!oldData.GroupIsActive.Equals(data.GroupIsActive))
	    {
		cmd.Parameters.Add(new SqlParameter("@GroupIsActive", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "GroupIsActive", DataRowVersion.Proposed, !data.GroupIsActive.IsValid ? data.GroupIsActive.DBValue : data.GroupIsActive.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.OrgGroupsID.Equals(data.OrgGroupsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgGroupsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgGroupsID", DataRowVersion.Proposed, data.OrgGroupsID.DBValue));

	    }
	    if (!oldData.LocationName.Equals(data.LocationName))
	    {
		cmd.Parameters.Add(new SqlParameter("@LocationName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "LocationName", DataRowVersion.Proposed, data.LocationName.DBValue));

	    }
	    if (!oldData.EmployeeTitle.Equals(data.EmployeeTitle))
	    {
		cmd.Parameters.Add(new SqlParameter("@EmployeeTitle", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "EmployeeTitle", DataRowVersion.Proposed, data.EmployeeTitle.DBValue));

	    }
	    if (!oldData.EmployeeIsActive.Equals(data.EmployeeIsActive))
	    {
		cmd.Parameters.Add(new SqlParameter("@EmployeeIsActive", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "EmployeeIsActive", DataRowVersion.Proposed, !data.EmployeeIsActive.IsValid ? data.EmployeeIsActive.DBValue : data.EmployeeIsActive.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.OrgGroupsEmployeesID.Equals(data.OrgGroupsEmployeesID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgGroupsEmployeesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgGroupsEmployeesID", DataRowVersion.Proposed, data.OrgGroupsEmployeesID.DBValue));

	    }
	    if (!oldData.OrgEmployeesID.Equals(data.OrgEmployeesID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgEmployeesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgEmployeesID", DataRowVersion.Proposed, data.OrgEmployeesID.DBValue));

	    }
	    if (!oldData.Email.Equals(data.Email))
	    {
		cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "Email", DataRowVersion.Proposed, data.Email.DBValue));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the GroupEmployeeView table by a composite primary key.
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