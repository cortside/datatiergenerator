using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.Types;

using Golf.Tournament.DataObject;


namespace Golf.Tournament.DAO {
    public class ParticipantDAO : Spring2.Core.DAO.EntityDAO {

	private static readonly String VIEW = "vwParticipant";
	private static readonly String CONNECTION_STRING_KEY = "ConnectionString";
	private static readonly Int32 COMMAND_TIMEOUT = 15;

	/// <summary>
	/// Hash table mapping entity property names to sql code.
	/// </summary>
	private static Hashtable propertyToSqlMap = new Hashtable();

	/// <summary>
	/// Initializes the static map of property names to sql expressions.
	/// </summary>
	static ParticipantDAO() {
	    if (!propertyToSqlMap.Contains("ParticipantId")) {
		propertyToSqlMap.Add("ParticipantId",@"ParticipantId");
	    }
	    if (!propertyToSqlMap.Contains("Golfer.GolferId")) {
		propertyToSqlMap.Add("Golfer.GolferId",@"GolferId");
	    }
	    if (!propertyToSqlMap.Contains("Tournament.TournamentId")) {
		propertyToSqlMap.Add("Tournament.TournamentId",@"TournamentId");
	    }
	    if (!propertyToSqlMap.Contains("Team.TeamId")) {
		propertyToSqlMap.Add("Team.TeamId",@"TeamId");
	    }
	    if (!propertyToSqlMap.Contains("Payment.PaymentId")) {
		propertyToSqlMap.Add("Payment.PaymentId",@"PaymentId");
	    }
	    if (!propertyToSqlMap.Contains("Payment.ConfirmationCode")) {
		propertyToSqlMap.Add("Payment.ConfirmationCode",@"ConfirmationCode");
	    }
	    if (!propertyToSqlMap.Contains("Payment.PaymentDate")) {
		propertyToSqlMap.Add("Payment.PaymentDate",@"PaymentDate");
	    }
	    if (!propertyToSqlMap.Contains("IsValid")) {
		propertyToSqlMap.Add("IsValid",@"IsValid");
	    }
	    if (!propertyToSqlMap.Contains("RegistrationFee")) {
		propertyToSqlMap.Add("RegistrationFee",@"RegistrationFee");
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
	/// Returns a list of all Participant rows.
	/// </summary>
	/// <returns>List of ParticipantData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList() {
	    return GetList(null, null);
	}

	/// <summary>
	/// Returns a filtered list of Participant rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of ParticipantData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public static IList GetList(IWhere whereClause) {
	    return GetList(whereClause, null);
	}

	/// <summary>
	/// Returns an ordered list of Participant rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of ParticipantData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(IOrderBy orderByClause) {
	    return GetList(null, orderByClause);
	}

	/// <summary>
	/// Returns an ordered and filtered list of Participant rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of ParticipantData objects.</returns>
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
	/// Returns a list of all Participant rows.
	/// </summary>
	/// <returns>List of ParticipantData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(Int32 maxRows) {
	    return GetList(null, null, maxRows);
	}

	/// <summary>
	/// Returns a filtered list of Participant rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of ParticipantData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public static IList GetList(IWhere whereClause, Int32 maxRows) {
	    return GetList(whereClause, null, maxRows);
	}

	/// <summary>
	/// Returns an ordered list of Participant rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of ParticipantData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(IOrderBy orderByClause, Int32 maxRows) {
	    return GetList(null, orderByClause, maxRows);
	}

	/// <summary>
	/// Returns an ordered and filtered list of Participant rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of ParticipantData objects.</returns>
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
	/// Finds a Participant entity using it's primary key.
	/// </summary>
	/// <param name="ParticipantId">A key field.</param>
	/// <returns>A ParticipantData object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	public static ParticipantData Load(IdType participantId) {
	    WhereClause w = new WhereClause();
	    w.And("ParticipantId", participantId.DBValue);
	    SqlDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, w, null);

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Load found no rows for Participant.");
	    }
	    ParticipantData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	private static ParticipantData GetDataObjectFromReader(SqlDataReader dataReader) {
	    ParticipantData data = new ParticipantData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ParticipantId"))) {
		data.ParticipantId = IdType.UNSET;
	    } else {
		data.ParticipantId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("ParticipantId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("GolferId"))) {
		data.Golfer.GolferId = IdType.UNSET;
	    } else {
		data.Golfer.GolferId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("GolferId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("TournamentId"))) {
		data.Tournament.TournamentId = IdType.UNSET;
	    } else {
		data.Tournament.TournamentId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("TournamentId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("TeamId"))) {
		data.Team.TeamId = IdType.UNSET;
	    } else {
		data.Team.TeamId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("TeamId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("PaymentId"))) {
		data.Payment.PaymentId = IdType.UNSET;
	    } else {
		data.Payment.PaymentId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("PaymentId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ConfirmationCode"))) {
		data.Payment.ConfirmationCode = StringType.UNSET;
	    } else {
		data.Payment.ConfirmationCode = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("ConfirmationCode")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("PaymentDate"))) {
		data.Payment.PaymentDate = DateType.UNSET;
	    } else {
		data.Payment.PaymentDate = new DateType(dataReader.GetDateTime(dataReader.GetOrdinal("PaymentDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("IsValid"))) {
		data.IsValid = BooleanType.UNSET;
	    } else {
		data.IsValid = BooleanType.GetInstance(dataReader.GetString(dataReader.GetOrdinal("IsValid")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("RegistrationFee"))) {
		data.RegistrationFee = CurrencyType.UNSET;
	    } else {
		data.RegistrationFee = new CurrencyType(dataReader.GetDecimal(dataReader.GetOrdinal("RegistrationFee")));
	    }

	    return data;
	}

	/// <summary>
	/// Inserts a record into the Participant table.
	/// </summary>
	/// <param name=""></param>
	public static IdType Insert(ParticipantData data) {
	    return Insert(data, null);
	}

	/// <summary>
	/// Inserts a record into the Participant table.
	/// </summary>
	/// <param name=""></param>
	public static IdType Insert(ParticipantData data, SqlTransaction transaction) {
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spParticipant_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    SqlParameter rv = cmd.Parameters.Add("RETURN_VALUE", SqlDbType.Int);
	    rv.Direction = ParameterDirection.ReturnValue;
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@GolferId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Golfer.GolferId", DataRowVersion.Proposed, data.Golfer.GolferId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@TournamentId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Tournament.TournamentId", DataRowVersion.Proposed, data.Tournament.TournamentId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@TeamId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Team.TeamId", DataRowVersion.Proposed, data.Team.TeamId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@PaymentId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Payment.PaymentId", DataRowVersion.Proposed, data.Payment.PaymentId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@IsValid", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "IsValid", DataRowVersion.Proposed, data.IsValid.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@RegistrationFee", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "RegistrationFee", DataRowVersion.Proposed, data.RegistrationFee.DBValue));

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
	/// Updates a record in the Participant table.
	/// </summary>
	/// <param name=""></param>
	public static void Update(ParticipantData data) {
	    Update(data, null);
	}

	/// <summary>
	/// Updates a record in the Participant table.
	/// </summary>
	/// <param name=""></param>
	public static void Update(ParticipantData data, SqlTransaction transaction) {
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spParticipant_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@ParticipantId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ParticipantId", DataRowVersion.Proposed, data.ParticipantId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@GolferId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Golfer.GolferId", DataRowVersion.Proposed, data.Golfer.GolferId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@TournamentId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Tournament.TournamentId", DataRowVersion.Proposed, data.Tournament.TournamentId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@TeamId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Team.TeamId", DataRowVersion.Proposed, data.Team.TeamId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@PaymentId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Payment.PaymentId", DataRowVersion.Proposed, data.Payment.PaymentId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@IsValid", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "IsValid", DataRowVersion.Proposed, data.IsValid.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@RegistrationFee", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "RegistrationFee", DataRowVersion.Proposed, data.RegistrationFee.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }
	}

	/// <summary>
	/// Deletes a record from the Participant table by ParticipantId.
	/// </summary>
	/// <param name=""></param>
	public static void Delete(IdType participantId) {
	    Delete(participantId, null);
	}

	/// <summary>
	/// Deletes a record from the Participant table by ParticipantId.
	/// </summary>
	/// <param name=""></param>
	public static void Delete(IdType participantId, SqlTransaction transaction) {
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spParticipant_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    // Create and append the parameters
	    cmd.Parameters.Add(new SqlParameter("@ParticipantId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ParticipantId", DataRowVersion.Proposed, participantId.DBValue));

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
	/// <returns>The list of ParticipantDAO objects found.</returns>
	public static IList FindByTournamentId(IdType tournament_TournamentId) {
	    OrderByClause sort = new OrderByClause("TournamentId");
	    WhereClause filter = new WhereClause();
	    filter.And("TournamentId", tournament_TournamentId.DBValue);

	    return GetList(filter, sort);
	}

	/// <summary>
	/// Returns a list of objects which match the values for the fields specified.
	/// </summary>
	/// <param name="TournamentId">A field value to be matched.</param>
	/// <param name="TeamId">A field value to be matched.</param>
	/// <returns>The list of ParticipantDAO objects found.</returns>
	public static IList FindByTeamId(IdType tournament_TournamentId, IdType team_TeamId) {
	    OrderByClause sort = new OrderByClause("TournamentId, TeamId");
	    WhereClause filter = new WhereClause();
	    filter.And("TournamentId", tournament_TournamentId.DBValue);
	    filter.And("TeamId", team_TeamId.DBValue);

	    return GetList(filter, sort);
	}

	/// <summary>
	/// Returns a list of objects which match the values for the fields specified.
	/// </summary>
	/// <param name="ConfirmationCode">A field value to be matched.</param>
	/// <returns>The list of ParticipantDAO objects found.</returns>
	public static IList FindByConfirmationCode(StringType payment_ConfirmationCode) {
	    OrderByClause sort = new OrderByClause("ConfirmationCode");
	    WhereClause filter = new WhereClause();
	    filter.And("ConfirmationCode", payment_ConfirmationCode.DBValue);

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
