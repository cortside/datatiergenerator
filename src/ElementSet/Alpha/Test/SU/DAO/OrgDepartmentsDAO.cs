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
    
    
    public class OrgDepartmentsDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[OrgDepartments]";
        
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
        static OrgDepartmentsDAO()
        {
            propertyToSqlMap.Add("OrgDepartmentsID",@"OrgDepartmentsID");
	    propertyToSqlMap.Add("Description",@"Description");
	    propertyToSqlMap.Add("Active",@"Active");
	    propertyToSqlMap.Add("OrgDivisionsID",@"OrgDivisionsID");
	    propertyToSqlMap.Add("GLAccount",@"GLAccount");
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
        /// Returns a list of all OrgDepartments rows.
        /// </summary>
        /// <returns>List of OrgDepartmentsData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static OrgDepartmentsList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of OrgDepartments rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of OrgDepartmentsData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static OrgDepartmentsList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of OrgDepartments rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of OrgDepartmentsData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static OrgDepartmentsList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of OrgDepartments rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of OrgDepartmentsData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static OrgDepartmentsList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, whereClause, orderByClause, true);
	    OrgDepartmentsList list = new OrgDepartmentsList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a OrgDepartments entity using it's primary key.
        /// </summary>
        /// <param name="OrgDepartmentsID">A key field.</param>
        /// <returns>A OrgDepartmentsData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static OrgDepartmentsData Load(IdType orgDepartmentsID)
        {
            WhereClause w = new WhereClause();
	    w.And("OrgDepartmentsID", orgDepartmentsID.DBValue);
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for OrgDepartments.");
	    }
	    OrgDepartmentsData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static OrgDepartmentsData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            OrgDepartmentsData data = new OrgDepartmentsData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrgDepartmentsID")))
	    {
		data.OrgDepartmentsID = IdType.UNSET;
	    }
	    else
	    {
		data.OrgDepartmentsID = new IdType (dataReader.GetInt32(dataReader.GetOrdinal("OrgDepartmentsID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Description")))
	    {
		data.Description = StringType.UNSET;
	    }
	    else
	    {
		data.Description = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Description")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Active")))
	    {
		data.Active = BooleanType.UNSET;
	    }
	    else
	    {
		data.Active = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("Active")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrgDivisionsID")))
	    {
		data.OrgDivisionsID = IntegerType.UNSET;
	    }
	    else
	    {
		data.OrgDivisionsID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("OrgDivisionsID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("GLAccount")))
	    {
		data.GLAccount = StringType.UNSET;
	    }
	    else
	    {
		data.GLAccount = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("GLAccount")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the OrgDepartments table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static IdType Insert(OrgDepartmentsData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "Description,"
	    + "Active,"
	    + "OrgDivisionsID,"
	    + "GLAccount,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@Description,"
	    + "@Active,"
	    + "@OrgDivisionsID,"
	    + "@GLAccount,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ");select Scope_Identity() Id";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Active", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "Active", DataRowVersion.Proposed, !data.Active.IsValid ? data.Active.DBValue : data.Active.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@OrgDivisionsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgDivisionsID", DataRowVersion.Proposed, data.OrgDivisionsID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@GLAccount", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "GLAccount", DataRowVersion.Proposed, data.GLAccount.DBValue));

	    // Execute the query
	    SqlDataReader returnValue = cmd.ExecuteReader();
	    returnValue.Read();
	    int returnId = (int)(returnValue.GetDecimal(0));
	    returnValue.Close();
	    // Set the output paramter value(s)
	    return new IdType (returnId);
        }
        
        /// <summary>
        /// Updates a record in the OrgDepartments table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(OrgDepartmentsData data)
        {
            // Create and execute the command
	    OrgDepartmentsData oldData = Load ( data.OrgDepartmentsID);
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.Description.Equals(data.Description))
	    {
		sql = sql + "Description=@Description,";
	    }
	    if (!oldData.Active.Equals(data.Active))
	    {
		sql = sql + "Active=@Active,";
	    }
	    if (!oldData.OrgDivisionsID.Equals(data.OrgDivisionsID))
	    {
		sql = sql + "OrgDivisionsID=@OrgDivisionsID,";
	    }
	    if (!oldData.GLAccount.Equals(data.GLAccount))
	    {
		sql = sql + "GLAccount=@GLAccount,";
	    }
	    WhereClause w = new WhereClause();
	    w.And("OrgDepartmentsID", data.OrgDepartmentsID.DBValue);
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.OrgDepartmentsID.Equals(data.OrgDepartmentsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgDepartmentsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgDepartmentsID", DataRowVersion.Proposed, data.OrgDepartmentsID.DBValue));

	    }
	    if (!oldData.Description.Equals(data.Description))
	    {
		cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));

	    }
	    if (!oldData.Active.Equals(data.Active))
	    {
		cmd.Parameters.Add(new SqlParameter("@Active", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "Active", DataRowVersion.Proposed, !data.Active.IsValid ? data.Active.DBValue : data.Active.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.OrgDivisionsID.Equals(data.OrgDivisionsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgDivisionsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgDivisionsID", DataRowVersion.Proposed, data.OrgDivisionsID.DBValue));

	    }
	    if (!oldData.GLAccount.Equals(data.GLAccount))
	    {
		cmd.Parameters.Add(new SqlParameter("@GLAccount", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "GLAccount", DataRowVersion.Proposed, data.GLAccount.DBValue));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the OrgDepartments table by OrgDepartmentsID.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Delete(IdType orgDepartmentsID)
        {
            // Create and execute the command
	    string sql = "Delete From " + TABLE;
	    WhereClause w = new WhereClause();
	    w.And("OrgDepartmentsID", orgDepartmentsID.DBValue);
	    sql += w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();
        }
    }
}