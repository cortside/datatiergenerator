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
    
    
    public class ControlEditDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[ControlEdit]";
        
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
        static ControlEditDAO()
        {
            propertyToSqlMap.Add("IsBasic",@"IsBasic");
	    propertyToSqlMap.Add("Summary",@"Summary");
	    propertyToSqlMap.Add("Description",@"Description");
	    propertyToSqlMap.Add("Src",@"Src");
	    propertyToSqlMap.Add("IconImage",@"IconImage");
	    propertyToSqlMap.Add("DisplayOnHomePage",@"DisplayOnHomePage");
	    propertyToSqlMap.Add("SortOrder",@"SortOrder");
	    propertyToSqlMap.Add("ControlsID",@"ControlsID");
	    propertyToSqlMap.Add("ControlsGroupsID",@"ControlsGroupsID");
	    propertyToSqlMap.Add("OrgGroupID",@"OrgGroupID");
	    propertyToSqlMap.Add("ManagePage",@"ManagePage");
	    propertyToSqlMap.Add("EntireCompany",@"EntireCompany");
	    propertyToSqlMap.Add("MustShow",@"MustShow");
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
        /// Returns a list of all ControlEdit rows.
        /// </summary>
        /// <returns>List of ControlEditData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static ControlEditList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of ControlEdit rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of ControlEditData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static ControlEditList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of ControlEdit rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of ControlEditData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static ControlEditList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of ControlEdit rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of ControlEditData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static ControlEditList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, whereClause, orderByClause, true);
	    ControlEditList list = new ControlEditList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a ControlEdit entity using it's primary key.
        /// </summary>
        /// <returns>A ControlEditData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static ControlEditData Load()
        {
            WhereClause w = new WhereClause();
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for ControlEdit.");
	    }
	    ControlEditData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static ControlEditData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            ControlEditData data = new ControlEditData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("IsBasic")))
	    {
		data.IsBasic = BooleanType.UNSET;
	    }
	    else
	    {
		data.IsBasic = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("IsBasic")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Summary")))
	    {
		data.Summary = StringType.UNSET;
	    }
	    else
	    {
		data.Summary = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Summary")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Description")))
	    {
		data.Description = StringType.UNSET;
	    }
	    else
	    {
		data.Description = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Description")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Src")))
	    {
		data.Src = StringType.UNSET;
	    }
	    else
	    {
		data.Src = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Src")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("IconImage")))
	    {
		data.IconImage = StringType.UNSET;
	    }
	    else
	    {
		data.IconImage = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("IconImage")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("DisplayOnHomePage")))
	    {
		data.DisplayOnHomePage = BooleanType.UNSET;
	    }
	    else
	    {
		data.DisplayOnHomePage = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("DisplayOnHomePage")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("SortOrder")))
	    {
		data.SortOrder = IntegerType.UNSET;
	    }
	    else
	    {
		data.SortOrder = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("SortOrder")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ControlsID")))
	    {
		data.ControlsID = IntegerType.UNSET;
	    }
	    else
	    {
		data.ControlsID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("ControlsID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ControlsGroupsID")))
	    {
		data.ControlsGroupsID = IntegerType.UNSET;
	    }
	    else
	    {
		data.ControlsGroupsID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("ControlsGroupsID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrgGroupID")))
	    {
		data.OrgGroupID = IntegerType.UNSET;
	    }
	    else
	    {
		data.OrgGroupID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("OrgGroupID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ManagePage")))
	    {
		data.ManagePage = StringType.UNSET;
	    }
	    else
	    {
		data.ManagePage = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("ManagePage")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("EntireCompany")))
	    {
		data.EntireCompany = BooleanType.UNSET;
	    }
	    else
	    {
		data.EntireCompany = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("EntireCompany")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("MustShow")))
	    {
		data.MustShow = BooleanType.UNSET;
	    }
	    else
	    {
		data.MustShow = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("MustShow")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the ControlEdit table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Insert(ControlEditData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "IsBasic,"
	    + "Summary,"
	    + "Description,"
	    + "Src,"
	    + "IconImage,"
	    + "DisplayOnHomePage,"
	    + "SortOrder,"
	    + "ControlsID,"
	    + "ControlsGroupsID,"
	    + "OrgGroupID,"
	    + "ManagePage,"
	    + "EntireCompany,"
	    + "MustShow,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@IsBasic,"
	    + "@Summary,"
	    + "@Description,"
	    + "@Src,"
	    + "@IconImage,"
	    + "@DisplayOnHomePage,"
	    + "@SortOrder,"
	    + "@ControlsID,"
	    + "@ControlsGroupsID,"
	    + "@OrgGroupID,"
	    + "@ManagePage,"
	    + "@EntireCompany,"
	    + "@MustShow,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ")";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@IsBasic", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsBasic", DataRowVersion.Proposed, !data.IsBasic.IsValid ? data.IsBasic.DBValue : data.IsBasic.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@Summary", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Summary", DataRowVersion.Proposed, data.Summary.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Src", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Src", DataRowVersion.Proposed, data.Src.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@IconImage", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "IconImage", DataRowVersion.Proposed, data.IconImage.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DisplayOnHomePage", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "DisplayOnHomePage", DataRowVersion.Proposed, !data.DisplayOnHomePage.IsValid ? data.DisplayOnHomePage.DBValue : data.DisplayOnHomePage.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@SortOrder", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "SortOrder", DataRowVersion.Proposed, data.SortOrder.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ControlsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ControlsID", DataRowVersion.Proposed, data.ControlsID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ControlsGroupsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ControlsGroupsID", DataRowVersion.Proposed, data.ControlsGroupsID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrgGroupID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgGroupID", DataRowVersion.Proposed, data.OrgGroupID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ManagePage", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "ManagePage", DataRowVersion.Proposed, data.ManagePage.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@EntireCompany", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "EntireCompany", DataRowVersion.Proposed, !data.EntireCompany.IsValid ? data.EntireCompany.DBValue : data.EntireCompany.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@MustShow", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "MustShow", DataRowVersion.Proposed, !data.MustShow.IsValid ? data.MustShow.DBValue : data.MustShow.DBValue.Equals ("Y") ? 1 : 0));

	    // Execute the query
	    cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Updates a record in the ControlEdit table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(ControlEditData data)
        {
            // Create and execute the command
	    ControlEditData oldData = Load ();
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.IsBasic.Equals(data.IsBasic))
	    {
		sql = sql + "IsBasic=@IsBasic,";
	    }
	    if (!oldData.Summary.Equals(data.Summary))
	    {
		sql = sql + "Summary=@Summary,";
	    }
	    if (!oldData.Description.Equals(data.Description))
	    {
		sql = sql + "Description=@Description,";
	    }
	    if (!oldData.Src.Equals(data.Src))
	    {
		sql = sql + "Src=@Src,";
	    }
	    if (!oldData.IconImage.Equals(data.IconImage))
	    {
		sql = sql + "IconImage=@IconImage,";
	    }
	    if (!oldData.DisplayOnHomePage.Equals(data.DisplayOnHomePage))
	    {
		sql = sql + "DisplayOnHomePage=@DisplayOnHomePage,";
	    }
	    if (!oldData.SortOrder.Equals(data.SortOrder))
	    {
		sql = sql + "SortOrder=@SortOrder,";
	    }
	    if (!oldData.ControlsID.Equals(data.ControlsID))
	    {
		sql = sql + "ControlsID=@ControlsID,";
	    }
	    if (!oldData.ControlsGroupsID.Equals(data.ControlsGroupsID))
	    {
		sql = sql + "ControlsGroupsID=@ControlsGroupsID,";
	    }
	    if (!oldData.OrgGroupID.Equals(data.OrgGroupID))
	    {
		sql = sql + "OrgGroupID=@OrgGroupID,";
	    }
	    if (!oldData.ManagePage.Equals(data.ManagePage))
	    {
		sql = sql + "ManagePage=@ManagePage,";
	    }
	    if (!oldData.EntireCompany.Equals(data.EntireCompany))
	    {
		sql = sql + "EntireCompany=@EntireCompany,";
	    }
	    if (!oldData.MustShow.Equals(data.MustShow))
	    {
		sql = sql + "MustShow=@MustShow,";
	    }
	    WhereClause w = new WhereClause();
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.IsBasic.Equals(data.IsBasic))
	    {
		cmd.Parameters.Add(new SqlParameter("@IsBasic", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsBasic", DataRowVersion.Proposed, !data.IsBasic.IsValid ? data.IsBasic.DBValue : data.IsBasic.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.Summary.Equals(data.Summary))
	    {
		cmd.Parameters.Add(new SqlParameter("@Summary", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Summary", DataRowVersion.Proposed, data.Summary.DBValue));

	    }
	    if (!oldData.Description.Equals(data.Description))
	    {
		cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));

	    }
	    if (!oldData.Src.Equals(data.Src))
	    {
		cmd.Parameters.Add(new SqlParameter("@Src", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Src", DataRowVersion.Proposed, data.Src.DBValue));

	    }
	    if (!oldData.IconImage.Equals(data.IconImage))
	    {
		cmd.Parameters.Add(new SqlParameter("@IconImage", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "IconImage", DataRowVersion.Proposed, data.IconImage.DBValue));

	    }
	    if (!oldData.DisplayOnHomePage.Equals(data.DisplayOnHomePage))
	    {
		cmd.Parameters.Add(new SqlParameter("@DisplayOnHomePage", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "DisplayOnHomePage", DataRowVersion.Proposed, !data.DisplayOnHomePage.IsValid ? data.DisplayOnHomePage.DBValue : data.DisplayOnHomePage.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.SortOrder.Equals(data.SortOrder))
	    {
		cmd.Parameters.Add(new SqlParameter("@SortOrder", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "SortOrder", DataRowVersion.Proposed, data.SortOrder.DBValue));

	    }
	    if (!oldData.ControlsID.Equals(data.ControlsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@ControlsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ControlsID", DataRowVersion.Proposed, data.ControlsID.DBValue));

	    }
	    if (!oldData.ControlsGroupsID.Equals(data.ControlsGroupsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@ControlsGroupsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ControlsGroupsID", DataRowVersion.Proposed, data.ControlsGroupsID.DBValue));

	    }
	    if (!oldData.OrgGroupID.Equals(data.OrgGroupID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgGroupID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgGroupID", DataRowVersion.Proposed, data.OrgGroupID.DBValue));

	    }
	    if (!oldData.ManagePage.Equals(data.ManagePage))
	    {
		cmd.Parameters.Add(new SqlParameter("@ManagePage", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "ManagePage", DataRowVersion.Proposed, data.ManagePage.DBValue));

	    }
	    if (!oldData.EntireCompany.Equals(data.EntireCompany))
	    {
		cmd.Parameters.Add(new SqlParameter("@EntireCompany", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "EntireCompany", DataRowVersion.Proposed, !data.EntireCompany.IsValid ? data.EntireCompany.DBValue : data.EntireCompany.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.MustShow.Equals(data.MustShow))
	    {
		cmd.Parameters.Add(new SqlParameter("@MustShow", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "MustShow", DataRowVersion.Proposed, !data.MustShow.IsValid ? data.MustShow.DBValue : data.MustShow.DBValue.Equals ("Y") ? 1 : 0));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the ControlEdit table by a composite primary key.
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
        
        /// <summary>
        /// Returns a list of objects which match the values for the fields specified.
        /// </summary>
        /// <param name="IsBasic">A field value to be matched.</param>
        /// <param name="OrgGroupID">A field value to be matched.</param>
        /// <returns>The list of ControlEditDAO objects found.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static ControlEditList GetByBasicAndGroupID(BooleanType isBasic, IntegerType orgGroupID)
        {
            OrderByClause sort = new OrderByClause("IsBasic, OrgGroupID");
	    WhereClause filter = new WhereClause();
	    filter.And("IsBasic", !isBasic.IsValid ? isBasic.DBValue : isBasic.DBValue.Equals ("Y") ? 1 : 0);
	    filter.And("OrgGroupID", orgGroupID.DBValue);

	    return GetList(filter, sort);
        }
    }
}