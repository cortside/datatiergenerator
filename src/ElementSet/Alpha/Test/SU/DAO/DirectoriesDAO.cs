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
    
    
    public class DirectoriesDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[Directories]";
        
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
        static DirectoriesDAO()
        {
            propertyToSqlMap.Add("Id",@"Id");
	    propertyToSqlMap.Add("Name",@"Name");
	    propertyToSqlMap.Add("Path",@"Path");
	    propertyToSqlMap.Add("IsPublic",@"IsPublic");
	    propertyToSqlMap.Add("OrgGroupID",@"OrgGroupID");
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
        /// Returns a list of all Directories rows.
        /// </summary>
        /// <returns>List of DirectoriesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static DirectoriesList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of Directories rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of DirectoriesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static DirectoriesList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of Directories rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of DirectoriesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static DirectoriesList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of Directories rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of DirectoriesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static DirectoriesList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, whereClause, orderByClause, true);
	    DirectoriesList list = new DirectoriesList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a Directories entity using it's primary key.
        /// </summary>
        /// <param name="Id">A key field.</param>
        /// <returns>A DirectoriesData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static DirectoriesData Load(IdType id)
        {
            WhereClause w = new WhereClause();
	    w.And("Id", id.DBValue);
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for Directories.");
	    }
	    DirectoriesData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static DirectoriesData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            DirectoriesData data = new DirectoriesData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Id")))
	    {
		data.Id = IdType.UNSET;
	    }
	    else
	    {
		data.Id = new IdType (dataReader.GetInt32(dataReader.GetOrdinal("Id")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Name")))
	    {
		data.Name = StringType.UNSET;
	    }
	    else
	    {
		data.Name = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Name")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Path")))
	    {
		data.Path = StringType.UNSET;
	    }
	    else
	    {
		data.Path = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Path")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("IsPublic")))
	    {
		data.IsPublic = BooleanType.UNSET;
	    }
	    else
	    {
		data.IsPublic = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("IsPublic")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrgGroupID")))
	    {
		data.OrgGroupID = IntegerType.UNSET;
	    }
	    else
	    {
		data.OrgGroupID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("OrgGroupID")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the Directories table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static IdType Insert(DirectoriesData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "Name,"
	    + "Path,"
	    + "IsPublic,"
	    + "OrgGroupID,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@Name,"
	    + "@Path,"
	    + "@IsPublic,"
	    + "@OrgGroupID,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ");select Scope_Identity() Id";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Proposed, data.Name.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Path", SqlDbType.VarChar, 150, ParameterDirection.Input, false, 0, 0, "Path", DataRowVersion.Proposed, data.Path.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@IsPublic", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsPublic", DataRowVersion.Proposed, !data.IsPublic.IsValid ? data.IsPublic.DBValue : data.IsPublic.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@OrgGroupID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgGroupID", DataRowVersion.Proposed, data.OrgGroupID.DBValue));

	    // Execute the query
	    SqlDataReader returnValue = cmd.ExecuteReader();
	    returnValue.Read();
	    int returnId = (int)(returnValue.GetDecimal(0));
	    returnValue.Close();
	    // Set the output paramter value(s)
	    return new IdType (returnId);
        }
        
        /// <summary>
        /// Updates a record in the Directories table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(DirectoriesData data)
        {
            // Create and execute the command
	    DirectoriesData oldData = Load ( data.Id);
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.Name.Equals(data.Name))
	    {
		sql = sql + "Name=@Name,";
	    }
	    if (!oldData.Path.Equals(data.Path))
	    {
		sql = sql + "Path=@Path,";
	    }
	    if (!oldData.IsPublic.Equals(data.IsPublic))
	    {
		sql = sql + "IsPublic=@IsPublic,";
	    }
	    if (!oldData.OrgGroupID.Equals(data.OrgGroupID))
	    {
		sql = sql + "OrgGroupID=@OrgGroupID,";
	    }
	    WhereClause w = new WhereClause();
	    w.And("Id", data.Id.DBValue);
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.Id.Equals(data.Id))
	    {
		cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Id", DataRowVersion.Proposed, data.Id.DBValue));

	    }
	    if (!oldData.Name.Equals(data.Name))
	    {
		cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Proposed, data.Name.DBValue));

	    }
	    if (!oldData.Path.Equals(data.Path))
	    {
		cmd.Parameters.Add(new SqlParameter("@Path", SqlDbType.VarChar, 150, ParameterDirection.Input, false, 0, 0, "Path", DataRowVersion.Proposed, data.Path.DBValue));

	    }
	    if (!oldData.IsPublic.Equals(data.IsPublic))
	    {
		cmd.Parameters.Add(new SqlParameter("@IsPublic", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsPublic", DataRowVersion.Proposed, !data.IsPublic.IsValid ? data.IsPublic.DBValue : data.IsPublic.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.OrgGroupID.Equals(data.OrgGroupID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgGroupID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgGroupID", DataRowVersion.Proposed, data.OrgGroupID.DBValue));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the Directories table by Id.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Delete(IdType id)
        {
            // Create and execute the command
	    string sql = "Delete From " + TABLE;
	    WhereClause w = new WhereClause();
	    w.And("Id", id.DBValue);
	    sql += w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();
        }
    }
}