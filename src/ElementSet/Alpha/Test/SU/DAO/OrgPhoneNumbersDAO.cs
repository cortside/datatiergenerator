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
    
    
    public class OrgPhoneNumbersDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[OrgPhoneNumbers]";
        
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
        static OrgPhoneNumbersDAO()
        {
            propertyToSqlMap.Add("OrgPhoneNumbersID",@"OrgPhoneNumbersID");
	    propertyToSqlMap.Add("OrgDepartmentsID",@"OrgDepartmentsID");
	    propertyToSqlMap.Add("OrgLocationsID",@"OrgLocationsID");
	    propertyToSqlMap.Add("OrgEmployeesID",@"OrgEmployeesID");
	    propertyToSqlMap.Add("Description",@"Description");
	    propertyToSqlMap.Add("PhoneNumber",@"PhoneNumber");
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
        /// Returns a list of all OrgPhoneNumbers rows.
        /// </summary>
        /// <returns>List of OrgPhoneNumbersData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static OrgPhoneNumbersList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of OrgPhoneNumbers rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of OrgPhoneNumbersData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static OrgPhoneNumbersList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of OrgPhoneNumbers rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of OrgPhoneNumbersData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static OrgPhoneNumbersList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of OrgPhoneNumbers rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of OrgPhoneNumbersData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static OrgPhoneNumbersList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, whereClause, orderByClause, true);
	    OrgPhoneNumbersList list = new OrgPhoneNumbersList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a OrgPhoneNumbers entity using it's primary key.
        /// </summary>
        /// <param name="OrgPhoneNumbersID">A key field.</param>
        /// <returns>A OrgPhoneNumbersData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static OrgPhoneNumbersData Load(IdType orgPhoneNumbersID)
        {
            WhereClause w = new WhereClause();
	    w.And("OrgPhoneNumbersID", orgPhoneNumbersID.DBValue);
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for OrgPhoneNumbers.");
	    }
	    OrgPhoneNumbersData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static OrgPhoneNumbersData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            OrgPhoneNumbersData data = new OrgPhoneNumbersData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrgPhoneNumbersID")))
	    {
		data.OrgPhoneNumbersID = IdType.UNSET;
	    }
	    else
	    {
		data.OrgPhoneNumbersID = new IdType (dataReader.GetInt32(dataReader.GetOrdinal("OrgPhoneNumbersID")));
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
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrgEmployeesID")))
	    {
		data.OrgEmployeesID = IntegerType.UNSET;
	    }
	    else
	    {
		data.OrgEmployeesID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("OrgEmployeesID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Description")))
	    {
		data.Description = StringType.UNSET;
	    }
	    else
	    {
		data.Description = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Description")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("PhoneNumber")))
	    {
		data.PhoneNumber = StringType.UNSET;
	    }
	    else
	    {
		data.PhoneNumber = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("PhoneNumber")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the OrgPhoneNumbers table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static IdType Insert(OrgPhoneNumbersData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "OrgDepartmentsID,"
	    + "OrgLocationsID,"
	    + "OrgEmployeesID,"
	    + "Description,"
	    + "PhoneNumber,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@OrgDepartmentsID,"
	    + "@OrgLocationsID,"
	    + "@OrgEmployeesID,"
	    + "@Description,"
	    + "@PhoneNumber,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ");select Scope_Identity() Id";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@OrgDepartmentsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgDepartmentsID", DataRowVersion.Proposed, data.OrgDepartmentsID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrgLocationsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgLocationsID", DataRowVersion.Proposed, data.OrgLocationsID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrgEmployeesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgEmployeesID", DataRowVersion.Proposed, data.OrgEmployeesID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@PhoneNumber", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "PhoneNumber", DataRowVersion.Proposed, data.PhoneNumber.DBValue));

	    // Execute the query
	    SqlDataReader returnValue = cmd.ExecuteReader();
	    returnValue.Read();
	    int returnId = (int)(returnValue.GetDecimal(0));
	    returnValue.Close();
	    // Set the output paramter value(s)
	    return new IdType (returnId);
        }
        
        /// <summary>
        /// Updates a record in the OrgPhoneNumbers table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(OrgPhoneNumbersData data)
        {
            // Create and execute the command
	    OrgPhoneNumbersData oldData = Load ( data.OrgPhoneNumbersID);
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.OrgDepartmentsID.Equals(data.OrgDepartmentsID))
	    {
		sql = sql + "OrgDepartmentsID=@OrgDepartmentsID,";
	    }
	    if (!oldData.OrgLocationsID.Equals(data.OrgLocationsID))
	    {
		sql = sql + "OrgLocationsID=@OrgLocationsID,";
	    }
	    if (!oldData.OrgEmployeesID.Equals(data.OrgEmployeesID))
	    {
		sql = sql + "OrgEmployeesID=@OrgEmployeesID,";
	    }
	    if (!oldData.Description.Equals(data.Description))
	    {
		sql = sql + "Description=@Description,";
	    }
	    if (!oldData.PhoneNumber.Equals(data.PhoneNumber))
	    {
		sql = sql + "PhoneNumber=@PhoneNumber,";
	    }
	    WhereClause w = new WhereClause();
	    w.And("OrgPhoneNumbersID", data.OrgPhoneNumbersID.DBValue);
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.OrgPhoneNumbersID.Equals(data.OrgPhoneNumbersID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgPhoneNumbersID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgPhoneNumbersID", DataRowVersion.Proposed, data.OrgPhoneNumbersID.DBValue));

	    }
	    if (!oldData.OrgDepartmentsID.Equals(data.OrgDepartmentsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgDepartmentsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgDepartmentsID", DataRowVersion.Proposed, data.OrgDepartmentsID.DBValue));

	    }
	    if (!oldData.OrgLocationsID.Equals(data.OrgLocationsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgLocationsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgLocationsID", DataRowVersion.Proposed, data.OrgLocationsID.DBValue));

	    }
	    if (!oldData.OrgEmployeesID.Equals(data.OrgEmployeesID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgEmployeesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgEmployeesID", DataRowVersion.Proposed, data.OrgEmployeesID.DBValue));

	    }
	    if (!oldData.Description.Equals(data.Description))
	    {
		cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));

	    }
	    if (!oldData.PhoneNumber.Equals(data.PhoneNumber))
	    {
		cmd.Parameters.Add(new SqlParameter("@PhoneNumber", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "PhoneNumber", DataRowVersion.Proposed, data.PhoneNumber.DBValue));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the OrgPhoneNumbers table by OrgPhoneNumbersID.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Delete(IdType orgPhoneNumbersID)
        {
            // Create and execute the command
	    string sql = "Delete From " + TABLE;
	    WhereClause w = new WhereClause();
	    w.And("OrgPhoneNumbersID", orgPhoneNumbersID.DBValue);
	    sql += w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();
        }
    }
}