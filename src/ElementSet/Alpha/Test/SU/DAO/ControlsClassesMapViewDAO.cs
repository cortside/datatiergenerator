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
    
    
    public class ControlsClassesMapViewDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[ControlsClassesMapView]";
        
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
        static ControlsClassesMapViewDAO()
        {
            propertyToSqlMap.Add("ControlsID",@"ControlsID");
	    propertyToSqlMap.Add("Description",@"Description");
	    propertyToSqlMap.Add("ControlsClassesID",@"ControlsClassesID");
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
        /// Returns a list of all ControlsClassesMapView rows.
        /// </summary>
        /// <returns>List of ControlsClassesMapViewData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static ControlsClassesMapViewList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of ControlsClassesMapView rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of ControlsClassesMapViewData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static ControlsClassesMapViewList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of ControlsClassesMapView rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of ControlsClassesMapViewData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static ControlsClassesMapViewList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of ControlsClassesMapView rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of ControlsClassesMapViewData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static ControlsClassesMapViewList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, whereClause, orderByClause, true);
	    ControlsClassesMapViewList list = new ControlsClassesMapViewList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a ControlsClassesMapView entity using it's primary key.
        /// </summary>
        /// <returns>A ControlsClassesMapViewData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static ControlsClassesMapViewData Load()
        {
            WhereClause w = new WhereClause();
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for ControlsClassesMapView.");
	    }
	    ControlsClassesMapViewData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static ControlsClassesMapViewData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            ControlsClassesMapViewData data = new ControlsClassesMapViewData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ControlsID")))
	    {
		data.ControlsID = IntegerType.UNSET;
	    }
	    else
	    {
		data.ControlsID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("ControlsID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Description")))
	    {
		data.Description = StringType.UNSET;
	    }
	    else
	    {
		data.Description = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Description")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ControlsClassesID")))
	    {
		data.ControlsClassesID = IntegerType.UNSET;
	    }
	    else
	    {
		data.ControlsClassesID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("ControlsClassesID")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the ControlsClassesMapView table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Insert(ControlsClassesMapViewData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "ControlsID,"
	    + "Description,"
	    + "ControlsClassesID,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@ControlsID,"
	    + "@Description,"
	    + "@ControlsClassesID,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ")";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@ControlsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ControlsID", DataRowVersion.Proposed, data.ControlsID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ControlsClassesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ControlsClassesID", DataRowVersion.Proposed, data.ControlsClassesID.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Updates a record in the ControlsClassesMapView table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(ControlsClassesMapViewData data)
        {
            // Create and execute the command
	    ControlsClassesMapViewData oldData = Load ();
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.ControlsID.Equals(data.ControlsID))
	    {
		sql = sql + "ControlsID=@ControlsID,";
	    }
	    if (!oldData.Description.Equals(data.Description))
	    {
		sql = sql + "Description=@Description,";
	    }
	    if (!oldData.ControlsClassesID.Equals(data.ControlsClassesID))
	    {
		sql = sql + "ControlsClassesID=@ControlsClassesID,";
	    }
	    WhereClause w = new WhereClause();
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.ControlsID.Equals(data.ControlsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@ControlsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ControlsID", DataRowVersion.Proposed, data.ControlsID.DBValue));

	    }
	    if (!oldData.Description.Equals(data.Description))
	    {
		cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));

	    }
	    if (!oldData.ControlsClassesID.Equals(data.ControlsClassesID))
	    {
		cmd.Parameters.Add(new SqlParameter("@ControlsClassesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ControlsClassesID", DataRowVersion.Proposed, data.ControlsClassesID.DBValue));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the ControlsClassesMapView table by a composite primary key.
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