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
    
    
    public class LocaleDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[Locale]";
        
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
        static LocaleDAO()
        {
            propertyToSqlMap.Add("LocaleCode",@"LocaleCode");
	    propertyToSqlMap.Add("LocaleDescription",@"LocaleDescription");
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
        /// Returns a list of all Locale rows.
        /// </summary>
        /// <returns>List of LocaleData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static LocaleList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of Locale rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of LocaleData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static LocaleList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of Locale rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of LocaleData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static LocaleList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of Locale rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of LocaleData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static LocaleList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.ORDERDB, TABLE, whereClause, orderByClause, true);
	    LocaleList list = new LocaleList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a Locale entity using it's primary key.
        /// </summary>
        /// <param name="LocaleCode">A key field.</param>
        /// <returns>A LocaleData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static LocaleData Load(IdType localeCode)
        {
            WhereClause w = new WhereClause();
	    w.And("LocaleCode", localeCode.DBValue);
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.ORDERDB, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for Locale.");
	    }
	    LocaleData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static LocaleData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            LocaleData data = new LocaleData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("LocaleCode")))
	    {
		data.LocaleCode = IdType.UNSET;
	    }
	    else
	    {
		data.LocaleCode = new IdType (dataReader.GetInt32(dataReader.GetOrdinal("LocaleCode")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("LocaleDescription")))
	    {
		data.LocaleDescription = StringType.UNSET;
	    }
	    else
	    {
		data.LocaleDescription = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("LocaleDescription")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the Locale table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Insert(LocaleData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "LocaleCode,"
	    + "LocaleDescription,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@LocaleCode,"
	    + "@LocaleDescription,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ")";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.ORDERDB, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@LocaleCode", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "LocaleCode", DataRowVersion.Proposed, data.LocaleCode.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@LocaleDescription", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "LocaleDescription", DataRowVersion.Proposed, data.LocaleDescription.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Updates a record in the Locale table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(LocaleData data)
        {
            // Create and execute the command
	    LocaleData oldData = Load ( data.LocaleCode);
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.LocaleCode.Equals(data.LocaleCode))
	    {
		sql = sql + "LocaleCode=@LocaleCode,";
	    }
	    if (!oldData.LocaleDescription.Equals(data.LocaleDescription))
	    {
		sql = sql + "LocaleDescription=@LocaleDescription,";
	    }
	    WhereClause w = new WhereClause();
	    w.And("LocaleCode", data.LocaleCode.DBValue);
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.ORDERDB, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.LocaleCode.Equals(data.LocaleCode))
	    {
		cmd.Parameters.Add(new SqlParameter("@LocaleCode", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "LocaleCode", DataRowVersion.Proposed, data.LocaleCode.DBValue));

	    }
	    if (!oldData.LocaleDescription.Equals(data.LocaleDescription))
	    {
		cmd.Parameters.Add(new SqlParameter("@LocaleDescription", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "LocaleDescription", DataRowVersion.Proposed, data.LocaleDescription.DBValue));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the Locale table by a composite primary key.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Delete(IdType localeCode)
        {
            // Create and execute the command
	    string sql = "Delete From " + TABLE;
	    WhereClause w = new WhereClause();
	    w.And("LocaleCode", localeCode.DBValue);
	    sql += w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.ORDERDB, sql, CommandType.Text, COMMAND_TIMEOUT);

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();
        }
    }
}