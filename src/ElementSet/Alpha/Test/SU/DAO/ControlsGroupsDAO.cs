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
    
    
    public class ControlsGroupsDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[ControlsGroups]";
        
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
        static ControlsGroupsDAO()
        {
            propertyToSqlMap.Add("ControlsGroupsID",@"ControlsGroupsID");
	    propertyToSqlMap.Add("ControlsID",@"ControlsID");
	    propertyToSqlMap.Add("OrgGroupsID",@"OrgGroupsID");
	    propertyToSqlMap.Add("SortOrder",@"SortOrder");
	    propertyToSqlMap.Add("DisplayOnHomePage",@"DisplayOnHomePage");
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
        /// Returns a list of all ControlsGroups rows.
        /// </summary>
        /// <returns>List of ControlsGroupsData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static ControlsGroupsList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of ControlsGroups rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of ControlsGroupsData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static ControlsGroupsList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of ControlsGroups rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of ControlsGroupsData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static ControlsGroupsList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of ControlsGroups rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of ControlsGroupsData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static ControlsGroupsList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, whereClause, orderByClause, true);
	    ControlsGroupsList list = new ControlsGroupsList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a ControlsGroups entity using it's primary key.
        /// </summary>
        /// <param name="ControlsGroupsID">A key field.</param>
        /// <returns>A ControlsGroupsData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static ControlsGroupsData Load(IdType controlsGroupsID)
        {
            WhereClause w = new WhereClause();
	    w.And("ControlsGroupsID", controlsGroupsID.DBValue);
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for ControlsGroups.");
	    }
	    ControlsGroupsData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static ControlsGroupsData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            ControlsGroupsData data = new ControlsGroupsData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ControlsGroupsID")))
	    {
		data.ControlsGroupsID = IdType.UNSET;
	    }
	    else
	    {
		data.ControlsGroupsID = new IdType (dataReader.GetInt32(dataReader.GetOrdinal("ControlsGroupsID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ControlsID")))
	    {
		data.ControlsID = IntegerType.UNSET;
	    }
	    else
	    {
		data.ControlsID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("ControlsID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrgGroupsID")))
	    {
		data.OrgGroupsID = IntegerType.UNSET;
	    }
	    else
	    {
		data.OrgGroupsID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("OrgGroupsID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("SortOrder")))
	    {
		data.SortOrder = IntegerType.UNSET;
	    }
	    else
	    {
		data.SortOrder = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("SortOrder")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("DisplayOnHomePage")))
	    {
		data.DisplayOnHomePage = BooleanType.UNSET;
	    }
	    else
	    {
		data.DisplayOnHomePage = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("DisplayOnHomePage")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the ControlsGroups table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static IdType Insert(ControlsGroupsData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "ControlsID,"
	    + "OrgGroupsID,"
	    + "SortOrder,"
	    + "DisplayOnHomePage,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@ControlsID,"
	    + "@OrgGroupsID,"
	    + "@SortOrder,"
	    + "@DisplayOnHomePage,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ");select Scope_Identity() Id";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@ControlsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ControlsID", DataRowVersion.Proposed, data.ControlsID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrgGroupsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgGroupsID", DataRowVersion.Proposed, data.OrgGroupsID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@SortOrder", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "SortOrder", DataRowVersion.Proposed, data.SortOrder.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DisplayOnHomePage", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "DisplayOnHomePage", DataRowVersion.Proposed, !data.DisplayOnHomePage.IsValid ? data.DisplayOnHomePage.DBValue : data.DisplayOnHomePage.DBValue.Equals ("Y") ? 1 : 0));

	    // Execute the query
	    SqlDataReader returnValue = cmd.ExecuteReader();
	    returnValue.Read();
	    int returnId = (int)(returnValue.GetDecimal(0));
	    returnValue.Close();
	    // Set the output paramter value(s)
	    return new IdType (returnId);
        }
        
        /// <summary>
        /// Updates a record in the ControlsGroups table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(ControlsGroupsData data)
        {
            // Create and execute the command
	    ControlsGroupsData oldData = Load ( data.ControlsGroupsID);
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.ControlsID.Equals(data.ControlsID))
	    {
		sql = sql + "ControlsID=@ControlsID,";
	    }
	    if (!oldData.OrgGroupsID.Equals(data.OrgGroupsID))
	    {
		sql = sql + "OrgGroupsID=@OrgGroupsID,";
	    }
	    if (!oldData.SortOrder.Equals(data.SortOrder))
	    {
		sql = sql + "SortOrder=@SortOrder,";
	    }
	    if (!oldData.DisplayOnHomePage.Equals(data.DisplayOnHomePage))
	    {
		sql = sql + "DisplayOnHomePage=@DisplayOnHomePage,";
	    }
	    WhereClause w = new WhereClause();
	    w.And("ControlsGroupsID", data.ControlsGroupsID.DBValue);
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.ControlsGroupsID.Equals(data.ControlsGroupsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@ControlsGroupsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ControlsGroupsID", DataRowVersion.Proposed, data.ControlsGroupsID.DBValue));

	    }
	    if (!oldData.ControlsID.Equals(data.ControlsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@ControlsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ControlsID", DataRowVersion.Proposed, data.ControlsID.DBValue));

	    }
	    if (!oldData.OrgGroupsID.Equals(data.OrgGroupsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgGroupsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgGroupsID", DataRowVersion.Proposed, data.OrgGroupsID.DBValue));

	    }
	    if (!oldData.SortOrder.Equals(data.SortOrder))
	    {
		cmd.Parameters.Add(new SqlParameter("@SortOrder", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "SortOrder", DataRowVersion.Proposed, data.SortOrder.DBValue));

	    }
	    if (!oldData.DisplayOnHomePage.Equals(data.DisplayOnHomePage))
	    {
		cmd.Parameters.Add(new SqlParameter("@DisplayOnHomePage", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "DisplayOnHomePage", DataRowVersion.Proposed, !data.DisplayOnHomePage.IsValid ? data.DisplayOnHomePage.DBValue : data.DisplayOnHomePage.DBValue.Equals ("Y") ? 1 : 0));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the ControlsGroups table by ControlsGroupsID.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Delete(IdType controlsGroupsID)
        {
            // Create and execute the command
	    string sql = "Delete From " + TABLE;
	    WhereClause w = new WhereClause();
	    w.And("ControlsGroupsID", controlsGroupsID.DBValue);
	    sql += w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();
        }
    }
}