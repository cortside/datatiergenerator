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
    
    
    public class MustShowControlViewDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[MustShowControlView]";
        
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
        static MustShowControlViewDAO()
        {
            propertyToSqlMap.Add("Src",@"Src");
	    propertyToSqlMap.Add("Description",@"Description");
	    propertyToSqlMap.Add("Summary",@"Summary");
	    propertyToSqlMap.Add("ClassSrc",@"ClassSrc");
	    propertyToSqlMap.Add("MustShow",@"MustShow");
	    propertyToSqlMap.Add("DateStart",@"DateStart");
	    propertyToSqlMap.Add("DateEnd",@"DateEnd");
	    propertyToSqlMap.Add("ControlsID",@"ControlsID");
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
        /// Returns a list of all MustShowControlView rows.
        /// </summary>
        /// <returns>List of MustShowControlViewData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static MustShowControlViewList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of MustShowControlView rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of MustShowControlViewData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static MustShowControlViewList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of MustShowControlView rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of MustShowControlViewData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static MustShowControlViewList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of MustShowControlView rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of MustShowControlViewData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static MustShowControlViewList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, whereClause, orderByClause, true);
	    MustShowControlViewList list = new MustShowControlViewList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a MustShowControlView entity using it's primary key.
        /// </summary>
        /// <returns>A MustShowControlViewData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static MustShowControlViewData Load()
        {
            WhereClause w = new WhereClause();
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for MustShowControlView.");
	    }
	    MustShowControlViewData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static MustShowControlViewData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            MustShowControlViewData data = new MustShowControlViewData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Src")))
	    {
		data.Src = StringType.UNSET;
	    }
	    else
	    {
		data.Src = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Src")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Description")))
	    {
		data.Description = StringType.UNSET;
	    }
	    else
	    {
		data.Description = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Description")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Summary")))
	    {
		data.Summary = StringType.UNSET;
	    }
	    else
	    {
		data.Summary = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Summary")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ClassSrc")))
	    {
		data.ClassSrc = StringType.UNSET;
	    }
	    else
	    {
		data.ClassSrc = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("ClassSrc")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("MustShow")))
	    {
		data.MustShow = BooleanType.UNSET;
	    }
	    else
	    {
		data.MustShow = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("MustShow")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("DateStart")))
	    {
		data.DateStart = DateType.UNSET;
	    }
	    else
	    {
		data.DateStart = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("DateStart")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("DateEnd")))
	    {
		data.DateEnd = DateType.UNSET;
	    }
	    else
	    {
		data.DateEnd = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("DateEnd")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ControlsID")))
	    {
		data.ControlsID = IntegerType.UNSET;
	    }
	    else
	    {
		data.ControlsID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("ControlsID")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the MustShowControlView table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Insert(MustShowControlViewData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "Src,"
	    + "Description,"
	    + "Summary,"
	    + "ClassSrc,"
	    + "MustShow,"
	    + "DateStart,"
	    + "DateEnd,"
	    + "ControlsID,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@Src,"
	    + "@Description,"
	    + "@Summary,"
	    + "@ClassSrc,"
	    + "@MustShow,"
	    + "@DateStart,"
	    + "@DateEnd,"
	    + "@ControlsID,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ")";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@Src", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Src", DataRowVersion.Proposed, data.Src.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Summary", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Summary", DataRowVersion.Proposed, data.Summary.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ClassSrc", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "ClassSrc", DataRowVersion.Proposed, data.ClassSrc.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@MustShow", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "MustShow", DataRowVersion.Proposed, !data.MustShow.IsValid ? data.MustShow.DBValue : data.MustShow.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@DateStart", SqlDbType.SmallDateTime, 0, ParameterDirection.Input, false, 0, 0, "DateStart", DataRowVersion.Proposed, data.DateStart.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.SmallDateTime, 0, ParameterDirection.Input, false, 0, 0, "DateEnd", DataRowVersion.Proposed, data.DateEnd.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ControlsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ControlsID", DataRowVersion.Proposed, data.ControlsID.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Updates a record in the MustShowControlView table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(MustShowControlViewData data)
        {
            // Create and execute the command
	    MustShowControlViewData oldData = Load ();
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.Src.Equals(data.Src))
	    {
		sql = sql + "Src=@Src,";
	    }
	    if (!oldData.Description.Equals(data.Description))
	    {
		sql = sql + "Description=@Description,";
	    }
	    if (!oldData.Summary.Equals(data.Summary))
	    {
		sql = sql + "Summary=@Summary,";
	    }
	    if (!oldData.ClassSrc.Equals(data.ClassSrc))
	    {
		sql = sql + "ClassSrc=@ClassSrc,";
	    }
	    if (!oldData.MustShow.Equals(data.MustShow))
	    {
		sql = sql + "MustShow=@MustShow,";
	    }
	    if (!oldData.DateStart.Equals(data.DateStart))
	    {
		sql = sql + "DateStart=@DateStart,";
	    }
	    if (!oldData.DateEnd.Equals(data.DateEnd))
	    {
		sql = sql + "DateEnd=@DateEnd,";
	    }
	    if (!oldData.ControlsID.Equals(data.ControlsID))
	    {
		sql = sql + "ControlsID=@ControlsID,";
	    }
	    WhereClause w = new WhereClause();
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.Src.Equals(data.Src))
	    {
		cmd.Parameters.Add(new SqlParameter("@Src", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Src", DataRowVersion.Proposed, data.Src.DBValue));

	    }
	    if (!oldData.Description.Equals(data.Description))
	    {
		cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));

	    }
	    if (!oldData.Summary.Equals(data.Summary))
	    {
		cmd.Parameters.Add(new SqlParameter("@Summary", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Summary", DataRowVersion.Proposed, data.Summary.DBValue));

	    }
	    if (!oldData.ClassSrc.Equals(data.ClassSrc))
	    {
		cmd.Parameters.Add(new SqlParameter("@ClassSrc", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "ClassSrc", DataRowVersion.Proposed, data.ClassSrc.DBValue));

	    }
	    if (!oldData.MustShow.Equals(data.MustShow))
	    {
		cmd.Parameters.Add(new SqlParameter("@MustShow", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "MustShow", DataRowVersion.Proposed, !data.MustShow.IsValid ? data.MustShow.DBValue : data.MustShow.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.DateStart.Equals(data.DateStart))
	    {
		cmd.Parameters.Add(new SqlParameter("@DateStart", SqlDbType.SmallDateTime, 0, ParameterDirection.Input, false, 0, 0, "DateStart", DataRowVersion.Proposed, data.DateStart.DBValue));

	    }
	    if (!oldData.DateEnd.Equals(data.DateEnd))
	    {
		cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.SmallDateTime, 0, ParameterDirection.Input, false, 0, 0, "DateEnd", DataRowVersion.Proposed, data.DateEnd.DBValue));

	    }
	    if (!oldData.ControlsID.Equals(data.ControlsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@ControlsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ControlsID", DataRowVersion.Proposed, data.ControlsID.DBValue));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the MustShowControlView table by a composite primary key.
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