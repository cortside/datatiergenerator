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
    
    
    public class OrgEmployeesDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[OrgEmployees]";
        
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
        static OrgEmployeesDAO()
        {
            propertyToSqlMap.Add("OrgEmployeesID",@"OrgEmployeesID");
	    propertyToSqlMap.Add("OrgDepartmentsID",@"OrgDepartmentsID");
	    propertyToSqlMap.Add("OrgLocationsID",@"OrgLocationsID");
	    propertyToSqlMap.Add("OrgWorkspacesID",@"OrgWorkspacesID");
	    propertyToSqlMap.Add("FirstName",@"FirstName");
	    propertyToSqlMap.Add("LastName",@"LastName");
	    propertyToSqlMap.Add("NTUserAccount",@"NTUserAccount");
	    propertyToSqlMap.Add("IsActive",@"IsActive");
	    propertyToSqlMap.Add("Email",@"Email");
	    propertyToSqlMap.Add("EmployeeTitle",@"EmployeeTitle");
	    propertyToSqlMap.Add("DateHired",@"DateHired");
	    propertyToSqlMap.Add("DateTerminated",@"DateTerminated");
	    propertyToSqlMap.Add("Manager",@"Manager");
	    propertyToSqlMap.Add("EmployeeNumber",@"EmployeeNumber");
	    propertyToSqlMap.Add("Style",@"Style");
	    propertyToSqlMap.Add("Map",@"Map");
	    propertyToSqlMap.Add("MapX",@"MapX");
	    propertyToSqlMap.Add("MapY",@"MapY");
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
        /// Returns a list of all OrgEmployees rows.
        /// </summary>
        /// <returns>List of OrgEmployeesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static OrgEmployeesList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of OrgEmployees rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of OrgEmployeesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static OrgEmployeesList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of OrgEmployees rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of OrgEmployeesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static OrgEmployeesList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of OrgEmployees rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of OrgEmployeesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static OrgEmployeesList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, whereClause, orderByClause, true);
	    OrgEmployeesList list = new OrgEmployeesList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a OrgEmployees entity using it's primary key.
        /// </summary>
        /// <param name="OrgEmployeesID">A key field.</param>
        /// <returns>A OrgEmployeesData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static OrgEmployeesData Load(IdType orgEmployeesID)
        {
            WhereClause w = new WhereClause();
	    w.And("OrgEmployeesID", orgEmployeesID.DBValue);
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for OrgEmployees.");
	    }
	    OrgEmployeesData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static OrgEmployeesData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            OrgEmployeesData data = new OrgEmployeesData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrgEmployeesID")))
	    {
		data.OrgEmployeesID = IdType.UNSET;
	    }
	    else
	    {
		data.OrgEmployeesID = new IdType (dataReader.GetInt32(dataReader.GetOrdinal("OrgEmployeesID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrgDepartmentsID")))
	    {
		data.OrgDepartmentsID = IntegerType.UNSET;
	    }
	    else
	    {
		data.OrgDepartmentsID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("OrgDepartmentsID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrgLocationsID")))
	    {
		data.OrgLocationsID = IntegerType.UNSET;
	    }
	    else
	    {
		data.OrgLocationsID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("OrgLocationsID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrgWorkspacesID")))
	    {
		data.OrgWorkspacesID = IntegerType.UNSET;
	    }
	    else
	    {
		data.OrgWorkspacesID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("OrgWorkspacesID")));
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
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("NTUserAccount")))
	    {
		data.NTUserAccount = StringType.UNSET;
	    }
	    else
	    {
		data.NTUserAccount = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("NTUserAccount")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("IsActive")))
	    {
		data.IsActive = BooleanType.UNSET;
	    }
	    else
	    {
		data.IsActive = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("IsActive")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Email")))
	    {
		data.Email = StringType.UNSET;
	    }
	    else
	    {
		data.Email = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Email")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("EmployeeTitle")))
	    {
		data.EmployeeTitle = StringType.UNSET;
	    }
	    else
	    {
		data.EmployeeTitle = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("EmployeeTitle")));
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
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Map")))
	    {
		data.Map = StringType.UNSET;
	    }
	    else
	    {
		data.Map = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Map")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("MapX")))
	    {
		data.MapX = DecimalType.UNSET;
	    }
	    else
	    {
		data.MapX = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("MapX")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("MapY")))
	    {
		data.MapY = DecimalType.UNSET;
	    }
	    else
	    {
		data.MapY = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("MapY")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the OrgEmployees table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static IdType Insert(OrgEmployeesData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "OrgDepartmentsID,"
	    + "OrgLocationsID,"
	    + "OrgWorkspacesID,"
	    + "FirstName,"
	    + "LastName,"
	    + "NTUserAccount,"
	    + "IsActive,"
	    + "Email,"
	    + "EmployeeTitle,"
	    + "DateHired,"
	    + "DateTerminated,"
	    + "Manager,"
	    + "EmployeeNumber,"
	    + "Style,"
	    + "Map,"
	    + "MapX,"
	    + "MapY,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@OrgDepartmentsID,"
	    + "@OrgLocationsID,"
	    + "@OrgWorkspacesID,"
	    + "@FirstName,"
	    + "@LastName,"
	    + "@NTUserAccount,"
	    + "@IsActive,"
	    + "@Email,"
	    + "@EmployeeTitle,"
	    + "@DateHired,"
	    + "@DateTerminated,"
	    + "@Manager,"
	    + "@EmployeeNumber,"
	    + "@Style,"
	    + "@Map,"
	    + "@MapX,"
	    + "@MapY,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ");select Scope_Identity() Id";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@OrgDepartmentsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgDepartmentsID", DataRowVersion.Proposed, data.OrgDepartmentsID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrgLocationsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgLocationsID", DataRowVersion.Proposed, data.OrgLocationsID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrgWorkspacesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgWorkspacesID", DataRowVersion.Proposed, data.OrgWorkspacesID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "FirstName", DataRowVersion.Proposed, data.FirstName.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "LastName", DataRowVersion.Proposed, data.LastName.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@NTUserAccount", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "NTUserAccount", DataRowVersion.Proposed, data.NTUserAccount.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsActive", DataRowVersion.Proposed, !data.IsActive.IsValid ? data.IsActive.DBValue : data.IsActive.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "Email", DataRowVersion.Proposed, data.Email.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@EmployeeTitle", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "EmployeeTitle", DataRowVersion.Proposed, data.EmployeeTitle.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DateHired", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DateHired", DataRowVersion.Proposed, data.DateHired.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DateTerminated", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DateTerminated", DataRowVersion.Proposed, data.DateTerminated.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Manager", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Manager", DataRowVersion.Proposed, data.Manager.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@EmployeeNumber", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "EmployeeNumber", DataRowVersion.Proposed, data.EmployeeNumber.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Style", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Style", DataRowVersion.Proposed, data.Style.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Map", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Map", DataRowVersion.Proposed, data.Map.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@MapX", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "MapX", DataRowVersion.Proposed, data.MapX.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@MapY", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "MapY", DataRowVersion.Proposed, data.MapY.DBValue));

	    // Execute the query
	    SqlDataReader returnValue = cmd.ExecuteReader();
	    returnValue.Read();
	    int returnId = (int)(returnValue.GetDecimal(0));
	    returnValue.Close();
	    // Set the output paramter value(s)
	    return new IdType (returnId);
        }
        
        /// <summary>
        /// Updates a record in the OrgEmployees table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(OrgEmployeesData data)
        {
            // Create and execute the command
	    OrgEmployeesData oldData = Load ( data.OrgEmployeesID);
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.OrgDepartmentsID.Equals(data.OrgDepartmentsID))
	    {
		sql = sql + "OrgDepartmentsID=@OrgDepartmentsID,";
	    }
	    if (!oldData.OrgLocationsID.Equals(data.OrgLocationsID))
	    {
		sql = sql + "OrgLocationsID=@OrgLocationsID,";
	    }
	    if (!oldData.OrgWorkspacesID.Equals(data.OrgWorkspacesID))
	    {
		sql = sql + "OrgWorkspacesID=@OrgWorkspacesID,";
	    }
	    if (!oldData.FirstName.Equals(data.FirstName))
	    {
		sql = sql + "FirstName=@FirstName,";
	    }
	    if (!oldData.LastName.Equals(data.LastName))
	    {
		sql = sql + "LastName=@LastName,";
	    }
	    if (!oldData.NTUserAccount.Equals(data.NTUserAccount))
	    {
		sql = sql + "NTUserAccount=@NTUserAccount,";
	    }
	    if (!oldData.IsActive.Equals(data.IsActive))
	    {
		sql = sql + "IsActive=@IsActive,";
	    }
	    if (!oldData.Email.Equals(data.Email))
	    {
		sql = sql + "Email=@Email,";
	    }
	    if (!oldData.EmployeeTitle.Equals(data.EmployeeTitle))
	    {
		sql = sql + "EmployeeTitle=@EmployeeTitle,";
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
	    if (!oldData.Map.Equals(data.Map))
	    {
		sql = sql + "Map=@Map,";
	    }
	    if (!oldData.MapX.Equals(data.MapX))
	    {
		sql = sql + "MapX=@MapX,";
	    }
	    if (!oldData.MapY.Equals(data.MapY))
	    {
		sql = sql + "MapY=@MapY,";
	    }
	    WhereClause w = new WhereClause();
	    w.And("OrgEmployeesID", data.OrgEmployeesID.DBValue);
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.OrgEmployeesID.Equals(data.OrgEmployeesID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgEmployeesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgEmployeesID", DataRowVersion.Proposed, data.OrgEmployeesID.DBValue));

	    }
	    if (!oldData.OrgDepartmentsID.Equals(data.OrgDepartmentsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgDepartmentsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgDepartmentsID", DataRowVersion.Proposed, data.OrgDepartmentsID.DBValue));

	    }
	    if (!oldData.OrgLocationsID.Equals(data.OrgLocationsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgLocationsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgLocationsID", DataRowVersion.Proposed, data.OrgLocationsID.DBValue));

	    }
	    if (!oldData.OrgWorkspacesID.Equals(data.OrgWorkspacesID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgWorkspacesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgWorkspacesID", DataRowVersion.Proposed, data.OrgWorkspacesID.DBValue));

	    }
	    if (!oldData.FirstName.Equals(data.FirstName))
	    {
		cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "FirstName", DataRowVersion.Proposed, data.FirstName.DBValue));

	    }
	    if (!oldData.LastName.Equals(data.LastName))
	    {
		cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "LastName", DataRowVersion.Proposed, data.LastName.DBValue));

	    }
	    if (!oldData.NTUserAccount.Equals(data.NTUserAccount))
	    {
		cmd.Parameters.Add(new SqlParameter("@NTUserAccount", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "NTUserAccount", DataRowVersion.Proposed, data.NTUserAccount.DBValue));

	    }
	    if (!oldData.IsActive.Equals(data.IsActive))
	    {
		cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsActive", DataRowVersion.Proposed, !data.IsActive.IsValid ? data.IsActive.DBValue : data.IsActive.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.Email.Equals(data.Email))
	    {
		cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "Email", DataRowVersion.Proposed, data.Email.DBValue));

	    }
	    if (!oldData.EmployeeTitle.Equals(data.EmployeeTitle))
	    {
		cmd.Parameters.Add(new SqlParameter("@EmployeeTitle", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "EmployeeTitle", DataRowVersion.Proposed, data.EmployeeTitle.DBValue));

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
	    if (!oldData.Map.Equals(data.Map))
	    {
		cmd.Parameters.Add(new SqlParameter("@Map", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Map", DataRowVersion.Proposed, data.Map.DBValue));

	    }
	    if (!oldData.MapX.Equals(data.MapX))
	    {
		cmd.Parameters.Add(new SqlParameter("@MapX", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "MapX", DataRowVersion.Proposed, data.MapX.DBValue));

	    }
	    if (!oldData.MapY.Equals(data.MapY))
	    {
		cmd.Parameters.Add(new SqlParameter("@MapY", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "MapY", DataRowVersion.Proposed, data.MapY.DBValue));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the OrgEmployees table by OrgEmployeesID.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Delete(IdType orgEmployeesID)
        {
            // Create and execute the command
	    string sql = "Delete From " + TABLE;
	    WhereClause w = new WhereClause();
	    w.And("OrgEmployeesID", orgEmployeesID.DBValue);
	    sql += w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Returns a list of objects which match the values for the fields specified.
        /// </summary>
        /// <param name="NTUserAccount">A field value to be matched.</param>
        /// <returns>The list of OrgEmployeesDAO objects found.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static OrgEmployeesList FindByUsername(StringType nTUserAccount)
        {
            OrderByClause sort = new OrderByClause("NTUserAccount");
	    WhereClause filter = new WhereClause();
	    filter.And("NTUserAccount", nTUserAccount.DBValue);

	    return GetList(filter, sort);
        }
    }
}