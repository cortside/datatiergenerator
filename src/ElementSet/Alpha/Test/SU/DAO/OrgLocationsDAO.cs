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
    
    
    public class OrgLocationsDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[OrgLocations]";
        
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
        static OrgLocationsDAO()
        {
            propertyToSqlMap.Add("OrgLocationsID",@"OrgLocationsID");
	    propertyToSqlMap.Add("Description",@"Description");
	    propertyToSqlMap.Add("Address1",@"Address1");
	    propertyToSqlMap.Add("Address2",@"Address2");
	    propertyToSqlMap.Add("City",@"City");
	    propertyToSqlMap.Add("State",@"State");
	    propertyToSqlMap.Add("Country",@"Country");
	    propertyToSqlMap.Add("PostCode",@"PostCode");
	    propertyToSqlMap.Add("Prefix",@"Prefix");
	    propertyToSqlMap.Add("Image",@"Image");
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
        /// Returns a list of all OrgLocations rows.
        /// </summary>
        /// <returns>List of OrgLocationsData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static OrgLocationsList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of OrgLocations rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of OrgLocationsData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static OrgLocationsList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of OrgLocations rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of OrgLocationsData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static OrgLocationsList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of OrgLocations rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of OrgLocationsData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static OrgLocationsList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, whereClause, orderByClause, true);
	    OrgLocationsList list = new OrgLocationsList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a OrgLocations entity using it's primary key.
        /// </summary>
        /// <param name="OrgLocationsID">A key field.</param>
        /// <returns>A OrgLocationsData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static OrgLocationsData Load(IdType orgLocationsID)
        {
            WhereClause w = new WhereClause();
	    w.And("OrgLocationsID", orgLocationsID.DBValue);
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for OrgLocations.");
	    }
	    OrgLocationsData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static OrgLocationsData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            OrgLocationsData data = new OrgLocationsData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrgLocationsID")))
	    {
		data.OrgLocationsID = IdType.UNSET;
	    }
	    else
	    {
		data.OrgLocationsID = new IdType (dataReader.GetInt32(dataReader.GetOrdinal("OrgLocationsID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Description")))
	    {
		data.Description = StringType.UNSET;
	    }
	    else
	    {
		data.Description = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Description")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Address1")))
	    {
		data.Address1 = StringType.UNSET;
	    }
	    else
	    {
		data.Address1 = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Address1")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Address2")))
	    {
		data.Address2 = StringType.UNSET;
	    }
	    else
	    {
		data.Address2 = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Address2")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("City")))
	    {
		data.City = StringType.UNSET;
	    }
	    else
	    {
		data.City = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("City")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("State")))
	    {
		data.State = StringType.UNSET;
	    }
	    else
	    {
		data.State = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("State")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Country")))
	    {
		data.Country = StringType.UNSET;
	    }
	    else
	    {
		data.Country = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Country")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("PostCode")))
	    {
		data.PostCode = StringType.UNSET;
	    }
	    else
	    {
		data.PostCode = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("PostCode")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Prefix")))
	    {
		data.Prefix = StringType.UNSET;
	    }
	    else
	    {
		data.Prefix = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Prefix")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Image")))
	    {
		data.Image = StringType.UNSET;
	    }
	    else
	    {
		data.Image = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Image")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the OrgLocations table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static IdType Insert(OrgLocationsData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "Description,"
	    + "Address1,"
	    + "Address2,"
	    + "City,"
	    + "State,"
	    + "Country,"
	    + "PostCode,"
	    + "Prefix,"
	    + "Image,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@Description,"
	    + "@Address1,"
	    + "@Address2,"
	    + "@City,"
	    + "@State,"
	    + "@Country,"
	    + "@PostCode,"
	    + "@Prefix,"
	    + "@Image,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ");select Scope_Identity() Id";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Address1", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Address1", DataRowVersion.Proposed, data.Address1.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Address2", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Address2", DataRowVersion.Proposed, data.Address2.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@City", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "City", DataRowVersion.Proposed, data.City.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@State", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "State", DataRowVersion.Proposed, data.State.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Country", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Country", DataRowVersion.Proposed, data.Country.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@PostCode", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "PostCode", DataRowVersion.Proposed, data.PostCode.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Prefix", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Prefix", DataRowVersion.Proposed, data.Prefix.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Image", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Image", DataRowVersion.Proposed, data.Image.DBValue));

	    // Execute the query
	    SqlDataReader returnValue = cmd.ExecuteReader();
	    returnValue.Read();
	    int returnId = (int)(returnValue.GetDecimal(0));
	    returnValue.Close();
	    // Set the output paramter value(s)
	    return new IdType (returnId);
        }
        
        /// <summary>
        /// Updates a record in the OrgLocations table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(OrgLocationsData data)
        {
            // Create and execute the command
	    OrgLocationsData oldData = Load ( data.OrgLocationsID);
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.Description.Equals(data.Description))
	    {
		sql = sql + "Description=@Description,";
	    }
	    if (!oldData.Address1.Equals(data.Address1))
	    {
		sql = sql + "Address1=@Address1,";
	    }
	    if (!oldData.Address2.Equals(data.Address2))
	    {
		sql = sql + "Address2=@Address2,";
	    }
	    if (!oldData.City.Equals(data.City))
	    {
		sql = sql + "City=@City,";
	    }
	    if (!oldData.State.Equals(data.State))
	    {
		sql = sql + "State=@State,";
	    }
	    if (!oldData.Country.Equals(data.Country))
	    {
		sql = sql + "Country=@Country,";
	    }
	    if (!oldData.PostCode.Equals(data.PostCode))
	    {
		sql = sql + "PostCode=@PostCode,";
	    }
	    if (!oldData.Prefix.Equals(data.Prefix))
	    {
		sql = sql + "Prefix=@Prefix,";
	    }
	    if (!oldData.Image.Equals(data.Image))
	    {
		sql = sql + "Image=@Image,";
	    }
	    WhereClause w = new WhereClause();
	    w.And("OrgLocationsID", data.OrgLocationsID.DBValue);
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.OrgLocationsID.Equals(data.OrgLocationsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgLocationsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgLocationsID", DataRowVersion.Proposed, data.OrgLocationsID.DBValue));

	    }
	    if (!oldData.Description.Equals(data.Description))
	    {
		cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));

	    }
	    if (!oldData.Address1.Equals(data.Address1))
	    {
		cmd.Parameters.Add(new SqlParameter("@Address1", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Address1", DataRowVersion.Proposed, data.Address1.DBValue));

	    }
	    if (!oldData.Address2.Equals(data.Address2))
	    {
		cmd.Parameters.Add(new SqlParameter("@Address2", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Address2", DataRowVersion.Proposed, data.Address2.DBValue));

	    }
	    if (!oldData.City.Equals(data.City))
	    {
		cmd.Parameters.Add(new SqlParameter("@City", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "City", DataRowVersion.Proposed, data.City.DBValue));

	    }
	    if (!oldData.State.Equals(data.State))
	    {
		cmd.Parameters.Add(new SqlParameter("@State", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "State", DataRowVersion.Proposed, data.State.DBValue));

	    }
	    if (!oldData.Country.Equals(data.Country))
	    {
		cmd.Parameters.Add(new SqlParameter("@Country", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Country", DataRowVersion.Proposed, data.Country.DBValue));

	    }
	    if (!oldData.PostCode.Equals(data.PostCode))
	    {
		cmd.Parameters.Add(new SqlParameter("@PostCode", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "PostCode", DataRowVersion.Proposed, data.PostCode.DBValue));

	    }
	    if (!oldData.Prefix.Equals(data.Prefix))
	    {
		cmd.Parameters.Add(new SqlParameter("@Prefix", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Prefix", DataRowVersion.Proposed, data.Prefix.DBValue));

	    }
	    if (!oldData.Image.Equals(data.Image))
	    {
		cmd.Parameters.Add(new SqlParameter("@Image", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Image", DataRowVersion.Proposed, data.Image.DBValue));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the OrgLocations table by OrgLocationsID.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Delete(IdType orgLocationsID)
        {
            // Create and execute the command
	    string sql = "Delete From " + TABLE;
	    WhereClause w = new WhereClause();
	    w.And("OrgLocationsID", orgLocationsID.DBValue);
	    sql += w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();
        }
    }
}