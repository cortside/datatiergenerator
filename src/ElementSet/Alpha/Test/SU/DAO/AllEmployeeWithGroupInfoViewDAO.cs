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
    
    
    public class AllEmployeeWithGroupInfoViewDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[AllEmployeeWithGroupInfoView]";
        
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
        static AllEmployeeWithGroupInfoViewDAO()
        {
            propertyToSqlMap.Add("OrgEmployeesID",@"OrgEmployeesID");
	    propertyToSqlMap.Add("FirstName",@"FirstName");
	    propertyToSqlMap.Add("LastName",@"LastName");
	    propertyToSqlMap.Add("EmployeeTitle",@"EmployeeTitle");
	    propertyToSqlMap.Add("DepartmentName",@"DepartmentName");
	    propertyToSqlMap.Add("LocationName",@"LocationName");
	    propertyToSqlMap.Add("PhoneNumber",@"PhoneNumber");
	    propertyToSqlMap.Add("PhoneDescription",@"PhoneDescription");
	    propertyToSqlMap.Add("Email",@"Email");
	    propertyToSqlMap.Add("DateHired",@"DateHired");
	    propertyToSqlMap.Add("DateTerminated",@"DateTerminated");
	    propertyToSqlMap.Add("Manager",@"Manager");
	    propertyToSqlMap.Add("EmployeeNumber",@"EmployeeNumber");
	    propertyToSqlMap.Add("Style",@"Style");
	    propertyToSqlMap.Add("IsActive",@"IsActive");
	    propertyToSqlMap.Add("NTUserAccount",@"NTUserAccount");
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
        /// Returns a list of all AllEmployeeWithGroupInfoView rows.
        /// </summary>
        /// <returns>List of AllEmployeeWithGroupInfoViewData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static AllEmployeeWithGroupInfoViewList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of AllEmployeeWithGroupInfoView rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of AllEmployeeWithGroupInfoViewData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static AllEmployeeWithGroupInfoViewList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of AllEmployeeWithGroupInfoView rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of AllEmployeeWithGroupInfoViewData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static AllEmployeeWithGroupInfoViewList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of AllEmployeeWithGroupInfoView rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of AllEmployeeWithGroupInfoViewData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static AllEmployeeWithGroupInfoViewList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, whereClause, orderByClause, true);
	    AllEmployeeWithGroupInfoViewList list = new AllEmployeeWithGroupInfoViewList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a AllEmployeeWithGroupInfoView entity using it's primary key.
        /// </summary>
        /// <returns>A AllEmployeeWithGroupInfoViewData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static AllEmployeeWithGroupInfoViewData Load()
        {
            WhereClause w = new WhereClause();
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for AllEmployeeWithGroupInfoView.");
	    }
	    AllEmployeeWithGroupInfoViewData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static AllEmployeeWithGroupInfoViewData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            AllEmployeeWithGroupInfoViewData data = new AllEmployeeWithGroupInfoViewData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrgEmployeesID")))
	    {
		data.OrgEmployeesID = IntegerType.UNSET;
	    }
	    else
	    {
		data.OrgEmployeesID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("OrgEmployeesID")));
	    }
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
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("EmployeeTitle")))
	    {
		data.EmployeeTitle = StringType.UNSET;
	    }
	    else
	    {
		data.EmployeeTitle = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("EmployeeTitle")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("DepartmentName")))
	    {
		data.DepartmentName = StringType.UNSET;
	    }
	    else
	    {
		data.DepartmentName = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("DepartmentName")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("LocationName")))
	    {
		data.LocationName = StringType.UNSET;
	    }
	    else
	    {
		data.LocationName = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("LocationName")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("PhoneNumber")))
	    {
		data.PhoneNumber = StringType.UNSET;
	    }
	    else
	    {
		data.PhoneNumber = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("PhoneNumber")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("PhoneDescription")))
	    {
		data.PhoneDescription = StringType.UNSET;
	    }
	    else
	    {
		data.PhoneDescription = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("PhoneDescription")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Email")))
	    {
		data.Email = StringType.UNSET;
	    }
	    else
	    {
		data.Email = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Email")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("DateHired")))
	    {
		data.DateHired = DateType.UNSET;
	    }
	    else
	    {
		data.DateHired = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("DateHired")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("DateTerminated")))
	    {
		data.DateTerminated = DateType.UNSET;
	    }
	    else
	    {
		data.DateTerminated = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("DateTerminated")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Manager")))
	    {
		data.Manager = IntegerType.UNSET;
	    }
	    else
	    {
		data.Manager = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("Manager")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("EmployeeNumber")))
	    {
		data.EmployeeNumber = StringType.UNSET;
	    }
	    else
	    {
		data.EmployeeNumber = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("EmployeeNumber")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Style")))
	    {
		data.Style = StringType.UNSET;
	    }
	    else
	    {
		data.Style = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Style")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("IsActive")))
	    {
		data.IsActive = BooleanType.UNSET;
	    }
	    else
	    {
		data.IsActive = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("IsActive")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("NTUserAccount")))
	    {
		data.NTUserAccount = StringType.UNSET;
	    }
	    else
	    {
		data.NTUserAccount = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("NTUserAccount")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the AllEmployeeWithGroupInfoView table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Insert(AllEmployeeWithGroupInfoViewData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "OrgEmployeesID,"
	    + "FirstName,"
	    + "LastName,"
	    + "EmployeeTitle,"
	    + "DepartmentName,"
	    + "LocationName,"
	    + "PhoneNumber,"
	    + "PhoneDescription,"
	    + "Email,"
	    + "DateHired,"
	    + "DateTerminated,"
	    + "Manager,"
	    + "EmployeeNumber,"
	    + "Style,"
	    + "IsActive,"
	    + "NTUserAccount,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@OrgEmployeesID,"
	    + "@FirstName,"
	    + "@LastName,"
	    + "@EmployeeTitle,"
	    + "@DepartmentName,"
	    + "@LocationName,"
	    + "@PhoneNumber,"
	    + "@PhoneDescription,"
	    + "@Email,"
	    + "@DateHired,"
	    + "@DateTerminated,"
	    + "@Manager,"
	    + "@EmployeeNumber,"
	    + "@Style,"
	    + "@IsActive,"
	    + "@NTUserAccount,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ")";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@OrgEmployeesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgEmployeesID", DataRowVersion.Proposed, data.OrgEmployeesID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "FirstName", DataRowVersion.Proposed, data.FirstName.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "LastName", DataRowVersion.Proposed, data.LastName.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@EmployeeTitle", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "EmployeeTitle", DataRowVersion.Proposed, data.EmployeeTitle.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DepartmentName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "DepartmentName", DataRowVersion.Proposed, data.DepartmentName.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@LocationName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "LocationName", DataRowVersion.Proposed, data.LocationName.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@PhoneNumber", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "PhoneNumber", DataRowVersion.Proposed, data.PhoneNumber.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@PhoneDescription", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "PhoneDescription", DataRowVersion.Proposed, data.PhoneDescription.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "Email", DataRowVersion.Proposed, data.Email.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DateHired", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DateHired", DataRowVersion.Proposed, data.DateHired.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DateTerminated", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DateTerminated", DataRowVersion.Proposed, data.DateTerminated.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Manager", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Manager", DataRowVersion.Proposed, data.Manager.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@EmployeeNumber", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "EmployeeNumber", DataRowVersion.Proposed, data.EmployeeNumber.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Style", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Style", DataRowVersion.Proposed, data.Style.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsActive", DataRowVersion.Proposed, !data.IsActive.IsValid ? data.IsActive.DBValue : data.IsActive.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@NTUserAccount", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "NTUserAccount", DataRowVersion.Proposed, data.NTUserAccount.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Updates a record in the AllEmployeeWithGroupInfoView table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(AllEmployeeWithGroupInfoViewData data)
        {
            // Create and execute the command
	    AllEmployeeWithGroupInfoViewData oldData = Load ();
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.OrgEmployeesID.Equals(data.OrgEmployeesID))
	    {
		sql = sql + "OrgEmployeesID=@OrgEmployeesID,";
	    }
	    if (!oldData.FirstName.Equals(data.FirstName))
	    {
		sql = sql + "FirstName=@FirstName,";
	    }
	    if (!oldData.LastName.Equals(data.LastName))
	    {
		sql = sql + "LastName=@LastName,";
	    }
	    if (!oldData.EmployeeTitle.Equals(data.EmployeeTitle))
	    {
		sql = sql + "EmployeeTitle=@EmployeeTitle,";
	    }
	    if (!oldData.DepartmentName.Equals(data.DepartmentName))
	    {
		sql = sql + "DepartmentName=@DepartmentName,";
	    }
	    if (!oldData.LocationName.Equals(data.LocationName))
	    {
		sql = sql + "LocationName=@LocationName,";
	    }
	    if (!oldData.PhoneNumber.Equals(data.PhoneNumber))
	    {
		sql = sql + "PhoneNumber=@PhoneNumber,";
	    }
	    if (!oldData.PhoneDescription.Equals(data.PhoneDescription))
	    {
		sql = sql + "PhoneDescription=@PhoneDescription,";
	    }
	    if (!oldData.Email.Equals(data.Email))
	    {
		sql = sql + "Email=@Email,";
	    }
	    if (!oldData.DateHired.Equals(data.DateHired))
	    {
		sql = sql + "DateHired=@DateHired,";
	    }
	    if (!oldData.DateTerminated.Equals(data.DateTerminated))
	    {
		sql = sql + "DateTerminated=@DateTerminated,";
	    }
	    if (!oldData.Manager.Equals(data.Manager))
	    {
		sql = sql + "Manager=@Manager,";
	    }
	    if (!oldData.EmployeeNumber.Equals(data.EmployeeNumber))
	    {
		sql = sql + "EmployeeNumber=@EmployeeNumber,";
	    }
	    if (!oldData.Style.Equals(data.Style))
	    {
		sql = sql + "Style=@Style,";
	    }
	    if (!oldData.IsActive.Equals(data.IsActive))
	    {
		sql = sql + "IsActive=@IsActive,";
	    }
	    if (!oldData.NTUserAccount.Equals(data.NTUserAccount))
	    {
		sql = sql + "NTUserAccount=@NTUserAccount,";
	    }
	    WhereClause w = new WhereClause();
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.OrgEmployeesID.Equals(data.OrgEmployeesID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgEmployeesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgEmployeesID", DataRowVersion.Proposed, data.OrgEmployeesID.DBValue));

	    }
	    if (!oldData.FirstName.Equals(data.FirstName))
	    {
		cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "FirstName", DataRowVersion.Proposed, data.FirstName.DBValue));

	    }
	    if (!oldData.LastName.Equals(data.LastName))
	    {
		cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "LastName", DataRowVersion.Proposed, data.LastName.DBValue));

	    }
	    if (!oldData.EmployeeTitle.Equals(data.EmployeeTitle))
	    {
		cmd.Parameters.Add(new SqlParameter("@EmployeeTitle", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "EmployeeTitle", DataRowVersion.Proposed, data.EmployeeTitle.DBValue));

	    }
	    if (!oldData.DepartmentName.Equals(data.DepartmentName))
	    {
		cmd.Parameters.Add(new SqlParameter("@DepartmentName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "DepartmentName", DataRowVersion.Proposed, data.DepartmentName.DBValue));

	    }
	    if (!oldData.LocationName.Equals(data.LocationName))
	    {
		cmd.Parameters.Add(new SqlParameter("@LocationName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "LocationName", DataRowVersion.Proposed, data.LocationName.DBValue));

	    }
	    if (!oldData.PhoneNumber.Equals(data.PhoneNumber))
	    {
		cmd.Parameters.Add(new SqlParameter("@PhoneNumber", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "PhoneNumber", DataRowVersion.Proposed, data.PhoneNumber.DBValue));

	    }
	    if (!oldData.PhoneDescription.Equals(data.PhoneDescription))
	    {
		cmd.Parameters.Add(new SqlParameter("@PhoneDescription", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "PhoneDescription", DataRowVersion.Proposed, data.PhoneDescription.DBValue));

	    }
	    if (!oldData.Email.Equals(data.Email))
	    {
		cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "Email", DataRowVersion.Proposed, data.Email.DBValue));

	    }
	    if (!oldData.DateHired.Equals(data.DateHired))
	    {
		cmd.Parameters.Add(new SqlParameter("@DateHired", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DateHired", DataRowVersion.Proposed, data.DateHired.DBValue));

	    }
	    if (!oldData.DateTerminated.Equals(data.DateTerminated))
	    {
		cmd.Parameters.Add(new SqlParameter("@DateTerminated", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DateTerminated", DataRowVersion.Proposed, data.DateTerminated.DBValue));

	    }
	    if (!oldData.Manager.Equals(data.Manager))
	    {
		cmd.Parameters.Add(new SqlParameter("@Manager", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Manager", DataRowVersion.Proposed, data.Manager.DBValue));

	    }
	    if (!oldData.EmployeeNumber.Equals(data.EmployeeNumber))
	    {
		cmd.Parameters.Add(new SqlParameter("@EmployeeNumber", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "EmployeeNumber", DataRowVersion.Proposed, data.EmployeeNumber.DBValue));

	    }
	    if (!oldData.Style.Equals(data.Style))
	    {
		cmd.Parameters.Add(new SqlParameter("@Style", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Style", DataRowVersion.Proposed, data.Style.DBValue));

	    }
	    if (!oldData.IsActive.Equals(data.IsActive))
	    {
		cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsActive", DataRowVersion.Proposed, !data.IsActive.IsValid ? data.IsActive.DBValue : data.IsActive.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.NTUserAccount.Equals(data.NTUserAccount))
	    {
		cmd.Parameters.Add(new SqlParameter("@NTUserAccount", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "NTUserAccount", DataRowVersion.Proposed, data.NTUserAccount.DBValue));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the AllEmployeeWithGroupInfoView table by a composite primary key.
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