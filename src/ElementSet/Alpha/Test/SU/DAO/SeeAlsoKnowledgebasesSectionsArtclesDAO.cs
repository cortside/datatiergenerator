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
    
    
    public class SeeAlsoKnowledgebasesSectionsArtclesDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[SeeAlsoKnowledgebasesSectionsArtcles]";
        
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
        static SeeAlsoKnowledgebasesSectionsArtclesDAO()
        {
            propertyToSqlMap.Add("KnowledgebasesSectionsArticlesID",@"KnowledgebasesSectionsArticlesID");
	    propertyToSqlMap.Add("Title",@"Title");
	    propertyToSqlMap.Add("Id",@"Id");
	    propertyToSqlMap.Add("SeeAlsoID",@"SeeAlsoID");
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
        /// Returns a list of all SeeAlsoKnowledgebasesSectionsArtcles rows.
        /// </summary>
        /// <returns>List of SeeAlsoKnowledgebasesSectionsArtclesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static SeeAlsoKnowledgebasesSectionsArtclesList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of SeeAlsoKnowledgebasesSectionsArtcles rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of SeeAlsoKnowledgebasesSectionsArtclesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static SeeAlsoKnowledgebasesSectionsArtclesList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of SeeAlsoKnowledgebasesSectionsArtcles rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of SeeAlsoKnowledgebasesSectionsArtclesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static SeeAlsoKnowledgebasesSectionsArtclesList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of SeeAlsoKnowledgebasesSectionsArtcles rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of SeeAlsoKnowledgebasesSectionsArtclesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static SeeAlsoKnowledgebasesSectionsArtclesList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, whereClause, orderByClause, true);
	    SeeAlsoKnowledgebasesSectionsArtclesList list = new SeeAlsoKnowledgebasesSectionsArtclesList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a SeeAlsoKnowledgebasesSectionsArtcles entity using it's primary key.
        /// </summary>
        /// <returns>A SeeAlsoKnowledgebasesSectionsArtclesData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static SeeAlsoKnowledgebasesSectionsArtclesData Load()
        {
            WhereClause w = new WhereClause();
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for SeeAlsoKnowledgebasesSectionsArtcles.");
	    }
	    SeeAlsoKnowledgebasesSectionsArtclesData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static SeeAlsoKnowledgebasesSectionsArtclesData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            SeeAlsoKnowledgebasesSectionsArtclesData data = new SeeAlsoKnowledgebasesSectionsArtclesData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("KnowledgebasesSectionsArticlesID")))
	    {
		data.KnowledgebasesSectionsArticlesID = IntegerType.UNSET;
	    }
	    else
	    {
		data.KnowledgebasesSectionsArticlesID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("KnowledgebasesSectionsArticlesID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Title")))
	    {
		data.Title = StringType.UNSET;
	    }
	    else
	    {
		data.Title = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Title")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Id")))
	    {
		data.Id = IntegerType.UNSET;
	    }
	    else
	    {
		data.Id = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("Id")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("SeeAlsoID")))
	    {
		data.SeeAlsoID = IntegerType.UNSET;
	    }
	    else
	    {
		data.SeeAlsoID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("SeeAlsoID")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the SeeAlsoKnowledgebasesSectionsArtcles table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Insert(SeeAlsoKnowledgebasesSectionsArtclesData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "KnowledgebasesSectionsArticlesID,"
	    + "Title,"
	    + "Id,"
	    + "SeeAlsoID,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@KnowledgebasesSectionsArticlesID,"
	    + "@Title,"
	    + "@Id,"
	    + "@SeeAlsoID,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ")";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@KnowledgebasesSectionsArticlesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "KnowledgebasesSectionsArticlesID", DataRowVersion.Proposed, data.KnowledgebasesSectionsArticlesID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Title", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 0, 0, "Title", DataRowVersion.Proposed, data.Title.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Id", DataRowVersion.Proposed, data.Id.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@SeeAlsoID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "SeeAlsoID", DataRowVersion.Proposed, data.SeeAlsoID.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Updates a record in the SeeAlsoKnowledgebasesSectionsArtcles table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(SeeAlsoKnowledgebasesSectionsArtclesData data)
        {
            // Create and execute the command
	    SeeAlsoKnowledgebasesSectionsArtclesData oldData = Load ();
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.KnowledgebasesSectionsArticlesID.Equals(data.KnowledgebasesSectionsArticlesID))
	    {
		sql = sql + "KnowledgebasesSectionsArticlesID=@KnowledgebasesSectionsArticlesID,";
	    }
	    if (!oldData.Title.Equals(data.Title))
	    {
		sql = sql + "Title=@Title,";
	    }
	    if (!oldData.Id.Equals(data.Id))
	    {
		sql = sql + "Id=@Id,";
	    }
	    if (!oldData.SeeAlsoID.Equals(data.SeeAlsoID))
	    {
		sql = sql + "SeeAlsoID=@SeeAlsoID,";
	    }
	    WhereClause w = new WhereClause();
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.KnowledgebasesSectionsArticlesID.Equals(data.KnowledgebasesSectionsArticlesID))
	    {
		cmd.Parameters.Add(new SqlParameter("@KnowledgebasesSectionsArticlesID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "KnowledgebasesSectionsArticlesID", DataRowVersion.Proposed, data.KnowledgebasesSectionsArticlesID.DBValue));

	    }
	    if (!oldData.Title.Equals(data.Title))
	    {
		cmd.Parameters.Add(new SqlParameter("@Title", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 0, 0, "Title", DataRowVersion.Proposed, data.Title.DBValue));

	    }
	    if (!oldData.Id.Equals(data.Id))
	    {
		cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Id", DataRowVersion.Proposed, data.Id.DBValue));

	    }
	    if (!oldData.SeeAlsoID.Equals(data.SeeAlsoID))
	    {
		cmd.Parameters.Add(new SqlParameter("@SeeAlsoID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "SeeAlsoID", DataRowVersion.Proposed, data.SeeAlsoID.DBValue));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the SeeAlsoKnowledgebasesSectionsArtcles table by a composite primary key.
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