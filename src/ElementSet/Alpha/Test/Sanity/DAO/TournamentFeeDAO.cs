using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.Types;

using Golf.Tournament.DataObject;


namespace Golf.Tournament.DAO {
    public class TournamentFeeDAO : Spring2.Core.DAO.EntityDAO {

	private static readonly String VIEW = "vwTournamentFee";
	private static readonly String CONNECTION_STRING_KEY = "ConnectionString";
	private static readonly Int32 COMMAND_TIMEOUT = 15;

	/// <summary>
	/// Hash table mapping entity property names to sql code.
	/// </summary>
	private static Hashtable propertyToSqlMap = new Hashtable();

	/// <summary>
	/// Initializes the static map of property names to sql expressions.
	/// </summary>
	static TournamentFeeDAO() {
	    if (!propertyToSqlMap.Contains("TournamentFeeId")) {
		propertyToSqlMap.Add("TournamentFeeId",@"TournamentFeeId");
	    }
	    if (!propertyToSqlMap.Contains("TournamentId")) {
		propertyToSqlMap.Add("TournamentId",@"TournamentId");
	    }
	    if (!propertyToSqlMap.Contains("Key")) {
		propertyToSqlMap.Add("Key",@"Key");
	    }
	    if (!propertyToSqlMap.Contains("Fee")) {
		propertyToSqlMap.Add("Fee",@"Fee");
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
	/// Returns a list of all TournamentFee rows.
	/// </summary>
	/// <returns>List of TournamentFeeData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList() {
	    return GetList(null, null);
	}

	/// <summary>
	/// Returns a filtered list of TournamentFee rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of TournamentFeeData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public static IList GetList(IWhere whereClause) {
	    return GetList(whereClause, null);
	}

	/// <summary>
	/// Returns an ordered list of TournamentFee rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of TournamentFeeData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(IOrderBy orderByClause) {
	    return GetList(null, orderByClause);
	}

	/// <summary>
	/// Returns an ordered and filtered list of TournamentFee rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of TournamentFeeData objects.</returns>
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
	/// Returns a list of all TournamentFee rows.
	/// </summary>
	/// <returns>List of TournamentFeeData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(Int32 maxRows) {
	    return GetList(null, null, maxRows);
	}

	/// <summary>
	/// Returns a filtered list of TournamentFee rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of TournamentFeeData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public static IList GetList(IWhere whereClause, Int32 maxRows) {
	    return GetList(whereClause, null, maxRows);
	}

	/// <summary>
	/// Returns an ordered list of TournamentFee rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of TournamentFeeData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(IOrderBy orderByClause, Int32 maxRows) {
	    return GetList(null, orderByClause, maxRows);
	}

	/// <summary>
	/// Returns an ordered and filtered list of TournamentFee rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of TournamentFeeData objects.</returns>
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
	/// Finds a TournamentFee entity using it's primary key.
	/// </summary>
	/// <param name="TournamentFeeId">A key field.</param>
	/// <returns>A TournamentFeeData object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	public static TournamentFeeData Load(IdType tournamentFeeId) {
	    WhereClause w = new WhereClause();
	    w.And("TournamentFeeId", tournamentFeeId.DBValue);
	    SqlDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, w, null);

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Load found no rows for TournamentFee.");
	    }
	    TournamentFeeData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	private static TournamentFeeData GetDataObjectFromReader(SqlDataReader dataReader) {
	    TournamentFeeData data = new TournamentFeeData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("TournamentFeeId"))) {
		data.TournamentFeeId = IdType.UNSET;
	    } else {
		data.TournamentFeeId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("TournamentFeeId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("TournamentId"))) {
		data.TournamentId = IdType.UNSET;
	    } else {
		data.TournamentId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("TournamentId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Key"))) {
		data.Key = StringType.UNSET;
	    } else {
		data.Key = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Key")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Fee"))) {
		data.Fee = CurrencyType.UNSET;
	    } else {
		data.Fee = new CurrencyType(dataReader.GetDecimal(dataReader.GetOrdinal("Fee")));
	    }

	    return data;
	}

	/// <summary>
	/// Inserts a record into the TournamentFee table.
	/// </summary>
	/// <param name=""></param>
	public static IdType Insert(TournamentFeeData data) {
	    return Insert(data, null);
	}

	/// <summary>
	/// Inserts a record into the TournamentFee table.
	/// </summary>
	/// <param name=""></param>
	public static IdType Insert(TournamentFeeData data, SqlTransaction transaction) {
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spTournamentFee_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    SqlParameter rv = cmd.Parameters.Add("RETURN_VALUE", SqlDbType.Int);
	    rv.Direction = ParameterDirection.ReturnValue;
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@TournamentId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "TournamentId", DataRowVersion.Proposed, data.TournamentId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Key", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Key", DataRowVersion.Proposed, data.Key.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Fee", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "Fee", DataRowVersion.Proposed, data.Fee.DBValue));

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
	/// Updates a record in the TournamentFee table.
	/// </summary>
	/// <param name=""></param>
	public static void Update(TournamentFeeData data) {
	    Update(data, null);
	}

	/// <summary>
	/// Updates a record in the TournamentFee table.
	/// </summary>
	/// <param name=""></param>
	public static void Update(TournamentFeeData data, SqlTransaction transaction) {
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spTournamentFee_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@TournamentFeeId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "TournamentFeeId", DataRowVersion.Proposed, data.TournamentFeeId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@TournamentId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "TournamentId", DataRowVersion.Proposed, data.TournamentId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Key", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Key", DataRowVersion.Proposed, data.Key.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Fee", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "Fee", DataRowVersion.Proposed, data.Fee.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }
	}

	/// <summary>
	/// Deletes a record from the TournamentFee table by TournamentFeeId.
	/// </summary>
	/// <param name=""></param>
	public static void Delete(IdType tournamentFeeId) {
	    Delete(tournamentFeeId, null);
	}

	/// <summary>
	/// Deletes a record from the TournamentFee table by TournamentFeeId.
	/// </summary>
	/// <param name=""></param>
	public static void Delete(IdType tournamentFeeId, SqlTransaction transaction) {
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spTournamentFee_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    // Create and append the parameters
	    cmd.Parameters.Add(new SqlParameter("@TournamentFeeId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "TournamentFeeId", DataRowVersion.Proposed, tournamentFeeId.DBValue));

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
	/// <returns>The list of TournamentFeeDAO objects found.</returns>
	public static IList FindByTournamentId(IdType tournamentId) {
	    OrderByClause sort = new OrderByClause("TournamentId");
	    WhereClause filter = new WhereClause();
	    filter.And("TournamentId", tournamentId.DBValue);

	    return GetList(filter, sort);
	}

	/// <summary>
	/// Returns a list of objects which match the values for the fields specified.
	/// </summary>
	/// <param name="TournamentId">A field value to be matched.</param>
	/// <param name="Key">A field value to be matched.</param>
	/// <returns>The list of TournamentFeeDAO objects found.</returns>
	public static IList FindByUserIdAndRolePattern(IdType tournamentId, StringType key) {
	    OrderByClause sort = new OrderByClause("TournamentId, Key");
	    WhereClause filter = new WhereClause("TournamentId = @TournamentId and Key like @Key");
	    String sql = "Select * from " + VIEW + filter.FormatSql() + sort.FormatSql();
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, sql, CommandType.Text);
	    cmd.Parameters.Add(new SqlParameter("@TournamentId", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "TournamentId", DataRowVersion.Proposed, tournamentId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Key", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Key", DataRowVersion.Proposed, key.DBValue));
	    SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
	    IList list = new ArrayList();

	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
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
