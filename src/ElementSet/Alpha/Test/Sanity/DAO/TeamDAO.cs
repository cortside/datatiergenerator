using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.Types;

using Golf.Tournament.DataObject;
using Golf.Tournament.Types;


namespace Golf.Tournament.DAO {
    public class TeamDAO : Spring2.Core.DAO.EntityDAO {

	private static readonly String VIEW = "vwTeam";
	private static readonly String CONNECTION_STRING_KEY = "ConnectionString";
	private static readonly Int32 COMMAND_TIMEOUT = 15;

	/// <summary>
	/// Hash table mapping entity property names to sql code.
	/// </summary>
	private static Hashtable propertyToSqlMap = new Hashtable();

	/// <summary>
	/// Initializes the static map of property names to sql expressions.
	/// </summary>
	static TeamDAO() {
	    if (!propertyToSqlMap.Contains("TeamId")) {
		propertyToSqlMap.Add("TeamId",@"TeamId");
	    }
	    if (!propertyToSqlMap.Contains("RegistrationKey")) {
		propertyToSqlMap.Add("RegistrationKey",@"RegistrationKey");
	    }
	    if (!propertyToSqlMap.Contains("Status")) {
		propertyToSqlMap.Add("Status",@"Status");
	    }
	    if (!propertyToSqlMap.Contains("TournamentId")) {
		propertyToSqlMap.Add("TournamentId",@"TournamentId");
	    }
	}

	/// <summary>
	/// Creates a where clause object by mapping the given where clause text.  The text may reference
	/// entity properties which will be mapped to sql code by enclosing the property names in braces.
	/// </summary>
	/// <param name="whereText">Text to be mapped</param>
	/// <returns>WhereClause object.</returns>
	/// <exception cref="ApplicationException">When property name found in braces is not found in the entity.</exception>
	public static IWhere Where(String whereText) {
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
	public static IWhere Where(String propertyName, String value) {
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
	public static IWhere Where(String propertyName, Int32 value) {
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
	public static IWhere Where(String propertyName, DateTime value)	{
	    return new WhereClause(GetPropertyMapping(propertyToSqlMap, propertyName), value);
	}

	/// <summary>
	/// Returns a list of all Team rows.
	/// </summary>
	/// <returns>List of TeamData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList() {
	    return GetList(null, null);
	}

	/// <summary>
	/// Returns a filtered list of Team rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of TeamData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public static IList GetList(IWhere whereClause) {
	    return GetList(whereClause, null);
	}

	/// <summary>
	/// Returns an ordered list of Team rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of TeamData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(IOrderBy orderByClause) {
	    return GetList(null, orderByClause);
	}

	/// <summary>
	/// Returns an ordered and filtered list of Team rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of TeamData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public static IList GetList(IWhere whereClause, IOrderBy orderByClause) {
	    SqlDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, whereClause, orderByClause);

	    IList list = new ArrayList();
	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}

	/// <summary>
	/// Returns a list of all Team rows.
	/// </summary>
	/// <returns>List of TeamData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(Int32 maxRows) {
	    return GetList(null, null, maxRows);
	}

	/// <summary>
	/// Returns a filtered list of Team rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of TeamData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public static IList GetList(IWhere whereClause, Int32 maxRows) {
	    return GetList(whereClause, null, maxRows);
	}

	/// <summary>
	/// Returns an ordered list of Team rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of TeamData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(IOrderBy orderByClause, Int32 maxRows) {
	    return GetList(null, orderByClause, maxRows);
	}

	/// <summary>
	/// Returns an ordered and filtered list of Team rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of TeamData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public static IList GetList(IWhere whereClause, IOrderBy orderByClause, Int32 maxRows) {
	    SqlDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, whereClause, orderByClause, maxRows);

	    ArrayList list = new ArrayList();
	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}

	/// <summary>
	/// Finds a Team entity using it's primary key.
	/// </summary>
	/// <param name="TeamId">A key field.</param>
	/// <returns>A TeamData object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	public static TeamData Load(IdType teamId) {
	    WhereClause w = new WhereClause();
	    w.And("TeamId", teamId.DBValue);
	    SqlDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, w, null);

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Load found no rows for Team.");
	    }
	    TeamData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	private static TeamData GetDataObjectFromReader(SqlDataReader dataReader) {
	    TeamData data = new TeamData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("TeamId"))) {
		data.TeamId = IdType.UNSET;
	    } else {
		data.TeamId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("TeamId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("RegistrationKey"))) {
		data.RegistrationKey = StringType.UNSET;
	    } else {
		data.RegistrationKey = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("RegistrationKey")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Status"))) {
		data.Status = TeamStatusEnum.UNSET;
	    } else {
		data.Status = TeamStatusEnum.GetInstance(dataReader.GetString(dataReader.GetOrdinal("Status")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("TournamentId"))) {
		data.TournamentId = IdType.UNSET;
	    } else {
		data.TournamentId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("TournamentId")));
	    }

	    return data;
	}

	/// <summary>
	/// Inserts a record into the Team table.
	/// </summary>
	/// <param name=""></param>
	public static IdType Insert(TeamData data) {
	    return Insert(data, null);
	}

	/// <summary>
	/// Inserts a record into the Team table.
	/// </summary>
	/// <param name=""></param>
	public static IdType Insert(TeamData data, SqlTransaction transaction) {
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spTeam_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    SqlParameter rv = cmd.Parameters.Add("RETURN_VALUE", SqlDbType.Int);
	    rv.Direction = ParameterDirection.ReturnValue;
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@RegistrationKey", SqlDbType.VarChar, 6, ParameterDirection.Input, false, 0, 0, "RegistrationKey", DataRowVersion.Proposed, data.RegistrationKey.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "Status", DataRowVersion.Proposed, data.Status.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@TournamentId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "TournamentId", DataRowVersion.Proposed, data.TournamentId.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }

	    // Set the output paramter value(s)
	    return new IdType((Int32)(cmd.Parameters["RETURN_VALUE"].Value));
	}

	/// <summary>
	/// Updates a record in the Team table.
	/// </summary>
	/// <param name=""></param>
	public static void Update(TeamData data) {
	    Update(data, null);
	}

	/// <summary>
	/// Updates a record in the Team table.
	/// </summary>
	/// <param name=""></param>
	public static void Update(TeamData data, SqlTransaction transaction) {
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spTeam_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@TeamId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "TeamId", DataRowVersion.Proposed, data.TeamId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@RegistrationKey", SqlDbType.VarChar, 6, ParameterDirection.Input, false, 0, 0, "RegistrationKey", DataRowVersion.Proposed, data.RegistrationKey.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "Status", DataRowVersion.Proposed, data.Status.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@TournamentId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "TournamentId", DataRowVersion.Proposed, data.TournamentId.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }
	}

	/// <summary>
	/// Deletes a record from the Team table by TeamId.
	/// </summary>
	/// <param name=""></param>
	public static void Delete(IdType teamId) {
	    Delete(teamId, null);
	}

	/// <summary>
	/// Deletes a record from the Team table by TeamId.
	/// </summary>
	/// <param name=""></param>
	public static void Delete(IdType teamId, SqlTransaction transaction) {
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spTeam_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    // Create and append the parameters
	    cmd.Parameters.Add(new SqlParameter("@TeamId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "TeamId", DataRowVersion.Proposed, teamId.DBValue));

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }
	}

	/// <summary>
	/// Returns a list of objects which match the values for the fields specified.
	/// </summary>
	/// <param name="TournamentId">A field value to be matched.</param>
	/// <returns>The list of TeamDAO objects found.</returns>
	public static IList FindByTournamentId(IdType tournamentId) {
	    OrderByClause sort = new OrderByClause("TournamentId");
	    WhereClause filter = new WhereClause();
	    filter.And("TournamentId", tournamentId.DBValue);

	    return GetList(filter, sort);
	}

	/// <summary>
	/// Get a new transaction for a connection that can be created from this classes connection string
	/// </summary>
	public static DaoTransaction GetNewTransaction(String transactionName) {
	    SqlConnection conn = GetSqlConnection(GetConnectionString(CONNECTION_STRING_KEY));
	    DaoTransaction transaction = new DaoTransaction(conn.BeginTransaction(transactionName));

	    return transaction;
	}

	/// <summary>
	/// Get a new transaction for a connection that can be created from this classes connection string
	/// </summary>
	public static DaoTransaction GetNewTransaction() {
	    SqlConnection conn = GetSqlConnection(GetConnectionString(CONNECTION_STRING_KEY));
	    DaoTransaction transaction = new DaoTransaction(conn.BeginTransaction());

	    return transaction;
	}

    }
}
