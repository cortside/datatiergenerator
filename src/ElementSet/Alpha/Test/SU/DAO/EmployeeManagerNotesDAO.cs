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
    
    
    public class EmployeeManagerNotesDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[EmployeeManagerNotes]";
        
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
        static EmployeeManagerNotesDAO()
        {
            propertyToSqlMap.Add("Id",@"Id");
	    propertyToSqlMap.Add("ChangeEmployeeName",@"ChangeEmployeeName");
	    propertyToSqlMap.Add("IsAutomated",@"IsAutomated");
	    propertyToSqlMap.Add("NoteDate",@"NoteDate");
	    propertyToSqlMap.Add("Notes",@"Notes");
	    propertyToSqlMap.Add("Type",@"Type");
	    propertyToSqlMap.Add("EmployeeID",@"EmployeeID");
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
        /// Returns a list of all EmployeeManagerNotes rows.
        /// </summary>
        /// <returns>List of EmployeeManagerNotesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static EmployeeManagerNotesList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of EmployeeManagerNotes rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of EmployeeManagerNotesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static EmployeeManagerNotesList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of EmployeeManagerNotes rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of EmployeeManagerNotesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static EmployeeManagerNotesList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of EmployeeManagerNotes rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of EmployeeManagerNotesData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static EmployeeManagerNotesList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, whereClause, orderByClause, true);
	    EmployeeManagerNotesList list = new EmployeeManagerNotesList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a EmployeeManagerNotes entity using it's primary key.
        /// </summary>
        /// <param name="Id">A key field.</param>
        /// <returns>A EmployeeManagerNotesData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static EmployeeManagerNotesData Load(IdType id)
        {
            WhereClause w = new WhereClause();
	    w.And("Id", id.DBValue);
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.INTRANET, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for EmployeeManagerNotes.");
	    }
	    EmployeeManagerNotesData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static EmployeeManagerNotesData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            EmployeeManagerNotesData data = new EmployeeManagerNotesData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Id")))
	    {
		data.Id = IdType.UNSET;
	    }
	    else
	    {
		data.Id = new IdType (dataReader.GetInt32(dataReader.GetOrdinal("Id")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ChangeEmployeeName")))
	    {
		data.ChangeEmployeeName = StringType.UNSET;
	    }
	    else
	    {
		data.ChangeEmployeeName = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("ChangeEmployeeName")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("IsAutomated")))
	    {
		data.IsAutomated = BooleanType.UNSET;
	    }
	    else
	    {
		data.IsAutomated = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("IsAutomated")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("NoteDate")))
	    {
		data.NoteDate = DateType.UNSET;
	    }
	    else
	    {
		data.NoteDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("NoteDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Notes")))
	    {
		data.Notes = StringType.UNSET;
	    }
	    else
	    {
		data.Notes = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Notes")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Type")))
	    {
		data.Type = IntegerType.UNSET;
	    }
	    else
	    {
		data.Type = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("Type")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("EmployeeID")))
	    {
		data.EmployeeID = IntegerType.UNSET;
	    }
	    else
	    {
		data.EmployeeID = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("EmployeeID")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the EmployeeManagerNotes table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static IdType Insert(EmployeeManagerNotesData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "ChangeEmployeeName,"
	    + "IsAutomated,"
	    + "NoteDate,"
	    + "Notes,"
	    + "Type,"
	    + "EmployeeID,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@ChangeEmployeeName,"
	    + "@IsAutomated,"
	    + "@NoteDate,"
	    + "@Notes,"
	    + "@Type,"
	    + "@EmployeeID,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ");select Scope_Identity() Id";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.INTRANET, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@ChangeEmployeeName", SqlDbType.VarChar, 150, ParameterDirection.Input, false, 0, 0, "ChangeEmployeeName", DataRowVersion.Proposed, data.ChangeEmployeeName.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@IsAutomated", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsAutomated", DataRowVersion.Proposed, !data.IsAutomated.IsValid ? data.IsAutomated.DBValue : data.IsAutomated.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@NoteDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "NoteDate", DataRowVersion.Proposed, data.NoteDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Notes", SqlDbType.VarChar, 300, ParameterDirection.Input, false, 0, 0, "Notes", DataRowVersion.Proposed, data.Notes.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Type", DataRowVersion.Proposed, data.Type.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "EmployeeID", DataRowVersion.Proposed, data.EmployeeID.DBValue));

	    // Execute the query
	    SqlDataReader returnValue = cmd.ExecuteReader();
	    returnValue.Read();
	    int returnId = (int)(returnValue.GetDecimal(0));
	    returnValue.Close();
	    // Set the output paramter value(s)
	    return new IdType (returnId);
        }
        
        /// <summary>
        /// Updates a record in the EmployeeManagerNotes table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(EmployeeManagerNotesData data)
        {
            // Create and execute the command
	    EmployeeManagerNotesData oldData = Load ( data.Id);
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.ChangeEmployeeName.Equals(data.ChangeEmployeeName))
	    {
		sql = sql + "ChangeEmployeeName=@ChangeEmployeeName,";
	    }
	    if (!oldData.IsAutomated.Equals(data.IsAutomated))
	    {
		sql = sql + "IsAutomated=@IsAutomated,";
	    }
	    if (!oldData.NoteDate.Equals(data.NoteDate))
	    {
		sql = sql + "NoteDate=@NoteDate,";
	    }
	    if (!oldData.Notes.Equals(data.Notes))
	    {
		sql = sql + "Notes=@Notes,";
	    }
	    if (!oldData.Type.Equals(data.Type))
	    {
		sql = sql + "Type=@Type,";
	    }
	    if (!oldData.EmployeeID.Equals(data.EmployeeID))
	    {
		sql = sql + "EmployeeID=@EmployeeID,";
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
	    if (!oldData.ChangeEmployeeName.Equals(data.ChangeEmployeeName))
	    {
		cmd.Parameters.Add(new SqlParameter("@ChangeEmployeeName", SqlDbType.VarChar, 150, ParameterDirection.Input, false, 0, 0, "ChangeEmployeeName", DataRowVersion.Proposed, data.ChangeEmployeeName.DBValue));

	    }
	    if (!oldData.IsAutomated.Equals(data.IsAutomated))
	    {
		cmd.Parameters.Add(new SqlParameter("@IsAutomated", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsAutomated", DataRowVersion.Proposed, !data.IsAutomated.IsValid ? data.IsAutomated.DBValue : data.IsAutomated.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.NoteDate.Equals(data.NoteDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@NoteDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "NoteDate", DataRowVersion.Proposed, data.NoteDate.DBValue));

	    }
	    if (!oldData.Notes.Equals(data.Notes))
	    {
		cmd.Parameters.Add(new SqlParameter("@Notes", SqlDbType.VarChar, 300, ParameterDirection.Input, false, 0, 0, "Notes", DataRowVersion.Proposed, data.Notes.DBValue));

	    }
	    if (!oldData.Type.Equals(data.Type))
	    {
		cmd.Parameters.Add(new SqlParameter("@Type", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Type", DataRowVersion.Proposed, data.Type.DBValue));

	    }
	    if (!oldData.EmployeeID.Equals(data.EmployeeID))
	    {
		cmd.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "EmployeeID", DataRowVersion.Proposed, data.EmployeeID.DBValue));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the EmployeeManagerNotes table by Id.
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