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
    
    
    public class OrgGroupsDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[OrgGroups]";
        
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
        static OrgGroupsDAO()
        {
            propertyToSqlMap.Add("OrgGroupsID",@"OrgGroupsID");
	    propertyToSqlMap.Add("Description",@"Description");
	    propertyToSqlMap.Add("IsActive",@"IsActive");
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
        /// Returns a list of all OrgGroups rows.
        /// </summary>
        /// <returns>List of OrgGroupsData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static OrgGroupsList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of OrgGroups rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of OrgGroupsData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static OrgGroupsList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of OrgGroups rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of OrgGroupsData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static OrgGroupsList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of OrgGroups rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of OrgGroupsData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static OrgGroupsList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, whereClause, orderByClause, true);
	    OrgGroupsList list = new OrgGroupsList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a OrgGroups entity using it's primary key.
        /// </summary>
        /// <param name="OrgGroupsID">A key field.</param>
        /// <returns>A OrgGroupsData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static OrgGroupsData Load(IdType orgGroupsID)
        {
            WhereClause w = new WhereClause();
	    w.And("OrgGroupsID", orgGroupsID.DBValue);
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for OrgGroups.");
	    }
	    OrgGroupsData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static OrgGroupsData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            OrgGroupsData data = new OrgGroupsData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrgGroupsID")))
	    {
		data.OrgGroupsID = IdType.UNSET;
	    }
	    else
	    {
		data.OrgGroupsID = new IdType (dataReader.GetInt32(dataReader.GetOrdinal("OrgGroupsID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Description")))
	    {
		data.Description = StringType.UNSET;
	    }
	    else
	    {
		data.Description = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Description")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("IsActive")))
	    {
		data.IsActive = BooleanType.UNSET;
	    }
	    else
	    {
		data.IsActive = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("IsActive")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the OrgGroups table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static IdType Insert(OrgGroupsData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "Description,"
	    + "IsActive,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@Description,"
	    + "@IsActive,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ");select Scope_Identity() Id";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsActive", DataRowVersion.Proposed, !data.IsActive.IsValid ? data.IsActive.DBValue : data.IsActive.DBValue.Equals ("Y") ? 1 : 0));

	    // Execute the query
	    SqlDataReader returnValue = cmd.ExecuteReader();
	    returnValue.Read();
	    int returnId = (int)(returnValue.GetDecimal(0));
	    returnValue.Close();
	    // Set the output paramter value(s)
	    return new IdType (returnId);
        }
        
        /// <summary>
        /// Updates a record in the OrgGroups table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(OrgGroupsData data)
        {
            // Create and execute the command
	    OrgGroupsData oldData = Load ( data.OrgGroupsID);
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.Description.Equals(data.Description))
	    {
		sql = sql + "Description=@Description,";
	    }
	    if (!oldData.IsActive.Equals(data.IsActive))
	    {
		sql = sql + "IsActive=@IsActive,";
	    }
	    WhereClause w = new WhereClause();
	    w.And("OrgGroupsID", data.OrgGroupsID.DBValue);
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.OrgGroupsID.Equals(data.OrgGroupsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgGroupsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgGroupsID", DataRowVersion.Proposed, data.OrgGroupsID.DBValue));

	    }
	    if (!oldData.Description.Equals(data.Description))
	    {
		cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));

	    }
	    if (!oldData.IsActive.Equals(data.IsActive))
	    {
		cmd.Parameters.Add(new SqlParameter("@IsActive", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsActive", DataRowVersion.Proposed, !data.IsActive.IsValid ? data.IsActive.DBValue : data.IsActive.DBValue.Equals ("Y") ? 1 : 0));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the OrgGroups table by OrgGroupsID.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Delete(IdType orgGroupsID)
        {
            // Create and execute the command
	    string sql = "Delete From " + TABLE;
	    WhereClause w = new WhereClause();
	    w.And("OrgGroupsID", orgGroupsID.DBValue);
	    sql += w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();
        }
    }
}