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
    
    
    public class KnowledgebasesDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[Knowledgebases]";
        
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
        static KnowledgebasesDAO()
        {
            propertyToSqlMap.Add("KnowledgebasesID",@"KnowledgebasesID");
	    propertyToSqlMap.Add("Description",@"Description");
	    propertyToSqlMap.Add("DateStart",@"DateStart");
	    propertyToSqlMap.Add("DateEnd",@"DateEnd");
	    propertyToSqlMap.Add("IsPublic",@"IsPublic");
	    propertyToSqlMap.Add("OrgGroupsID",@"OrgGroupsID");
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
        /// Returns a list of all Knowledgebases rows.
        /// </summary>
        /// <returns>List of KnowledgebasesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static KnowledgebasesList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of Knowledgebases rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of KnowledgebasesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static KnowledgebasesList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of Knowledgebases rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of KnowledgebasesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static KnowledgebasesList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of Knowledgebases rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of KnowledgebasesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static KnowledgebasesList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, whereClause, orderByClause, true);
	    KnowledgebasesList list = new KnowledgebasesList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a Knowledgebases entity using it's primary key.
        /// </summary>
        /// <param name="KnowledgebasesID">A key field.</param>
        /// <returns>A KnowledgebasesData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static KnowledgebasesData Load(IdType knowledgebasesID)
        {
            WhereClause w = new WhereClause();
	    w.And("KnowledgebasesID", knowledgebasesID.DBValue);
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for Knowledgebases.");
	    }
	    KnowledgebasesData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static KnowledgebasesData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            KnowledgebasesData data = new KnowledgebasesData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("KnowledgebasesID")))
	    {
		data.KnowledgebasesID = IdType.UNSET;
	    }
	    else
	    {
		data.KnowledgebasesID = new IdType (dataReader.GetInt32(dataReader.GetOrdinal("KnowledgebasesID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Description")))
	    {
		data.Description = StringType.UNSET;
	    }
	    else
	    {
		data.Description = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Description")));
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
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("IsPublic")))
	    {
		data.IsPublic = BooleanType.UNSET;
	    }
	    else
	    {
		data.IsPublic = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("IsPublic")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrgGroupsID")))
	    {
		data.OrgGroupsID = IntegerType.UNSET;
	    }
	    else
	    {
		data.OrgGroupsID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("OrgGroupsID")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the Knowledgebases table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static IdType Insert(KnowledgebasesData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "Description,"
	    + "DateStart,"
	    + "DateEnd,"
	    + "IsPublic,"
	    + "OrgGroupsID,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@Description,"
	    + "@DateStart,"
	    + "@DateEnd,"
	    + "@IsPublic,"
	    + "@OrgGroupsID,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ");select Scope_Identity() Id";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 150, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DateStart", SqlDbType.SmallDateTime, 0, ParameterDirection.Input, false, 0, 0, "DateStart", DataRowVersion.Proposed, data.DateStart.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.SmallDateTime, 0, ParameterDirection.Input, false, 0, 0, "DateEnd", DataRowVersion.Proposed, data.DateEnd.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@IsPublic", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsPublic", DataRowVersion.Proposed, !data.IsPublic.IsValid ? data.IsPublic.DBValue : data.IsPublic.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@OrgGroupsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgGroupsID", DataRowVersion.Proposed, data.OrgGroupsID.DBValue));

	    // Execute the query
	    SqlDataReader returnValue = cmd.ExecuteReader();
	    returnValue.Read();
	    int returnId = (int)(returnValue.GetDecimal(0));
	    returnValue.Close();
	    // Set the output paramter value(s)
	    return new IdType (returnId);
        }
        
        /// <summary>
        /// Updates a record in the Knowledgebases table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(KnowledgebasesData data)
        {
            // Create and execute the command
	    KnowledgebasesData oldData = Load ( data.KnowledgebasesID);
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.Description.Equals(data.Description))
	    {
		sql = sql + "Description=@Description,";
	    }
	    if (!oldData.DateStart.Equals(data.DateStart))
	    {
		sql = sql + "DateStart=@DateStart,";
	    }
	    if (!oldData.DateEnd.Equals(data.DateEnd))
	    {
		sql = sql + "DateEnd=@DateEnd,";
	    }
	    if (!oldData.IsPublic.Equals(data.IsPublic))
	    {
		sql = sql + "IsPublic=@IsPublic,";
	    }
	    if (!oldData.OrgGroupsID.Equals(data.OrgGroupsID))
	    {
		sql = sql + "OrgGroupsID=@OrgGroupsID,";
	    }
	    WhereClause w = new WhereClause();
	    w.And("KnowledgebasesID", data.KnowledgebasesID.DBValue);
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.KnowledgebasesID.Equals(data.KnowledgebasesID))
	    {
		cmd.Parameters.Add(new SqlParameter("@KnowledgebasesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "KnowledgebasesID", DataRowVersion.Proposed, data.KnowledgebasesID.DBValue));

	    }
	    if (!oldData.Description.Equals(data.Description))
	    {
		cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 150, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));

	    }
	    if (!oldData.DateStart.Equals(data.DateStart))
	    {
		cmd.Parameters.Add(new SqlParameter("@DateStart", SqlDbType.SmallDateTime, 0, ParameterDirection.Input, false, 0, 0, "DateStart", DataRowVersion.Proposed, data.DateStart.DBValue));

	    }
	    if (!oldData.DateEnd.Equals(data.DateEnd))
	    {
		cmd.Parameters.Add(new SqlParameter("@DateEnd", SqlDbType.SmallDateTime, 0, ParameterDirection.Input, false, 0, 0, "DateEnd", DataRowVersion.Proposed, data.DateEnd.DBValue));

	    }
	    if (!oldData.IsPublic.Equals(data.IsPublic))
	    {
		cmd.Parameters.Add(new SqlParameter("@IsPublic", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsPublic", DataRowVersion.Proposed, !data.IsPublic.IsValid ? data.IsPublic.DBValue : data.IsPublic.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.OrgGroupsID.Equals(data.OrgGroupsID))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrgGroupsID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrgGroupsID", DataRowVersion.Proposed, data.OrgGroupsID.DBValue));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the Knowledgebases table by KnowledgebasesID.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Delete(IdType knowledgebasesID)
        {
            // Create and execute the command
	    string sql = "Delete From " + TABLE;
	    WhereClause w = new WhereClause();
	    w.And("KnowledgebasesID", knowledgebasesID.DBValue);
	    sql += w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();
        }
    }
}