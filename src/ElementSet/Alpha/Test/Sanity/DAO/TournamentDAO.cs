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
    public class TournamentDAO : Spring2.Core.DAO.EntityDAO {

	private static readonly String VIEW = "vwTournament";
	private static readonly String CONNECTION_STRING_KEY = "ConnectionString";
	private static readonly Int32 COMMAND_TIMEOUT = 15;

	/// <summary>
	/// Hash table mapping entity property names to sql code.
	/// </summary>
	private static Hashtable propertyToSqlMap = new Hashtable();

	/// <summary>
	/// Initializes the static map of property names to sql expressions.
	/// </summary>
	static TournamentDAO() {
	    if (!propertyToSqlMap.Contains("TournamentId")) {
		propertyToSqlMap.Add("TournamentId",@"TournamentId");
	    }
	    if (!propertyToSqlMap.Contains("Name")) {
		propertyToSqlMap.Add("Name",@"Name");
	    }
	    if (!propertyToSqlMap.Contains("Description")) {
		propertyToSqlMap.Add("Description",@"Description");
	    }
	    if (!propertyToSqlMap.Contains("NumberOfTeams")) {
		propertyToSqlMap.Add("NumberOfTeams",@"NumberOfTeams");
	    }
	    if (!propertyToSqlMap.Contains("TeamSize")) {
		propertyToSqlMap.Add("TeamSize",@"TeamSize");
	    }
	    if (!propertyToSqlMap.Contains("Format")) {
		propertyToSqlMap.Add("Format",@"Format");
	    }
	    if (!propertyToSqlMap.Contains("RegistrationBeginDate")) {
		propertyToSqlMap.Add("RegistrationBeginDate",@"RegistrationBeginDate");
	    }
	    if (!propertyToSqlMap.Contains("RegistrationEndDate")) {
		propertyToSqlMap.Add("RegistrationEndDate",@"RegistrationEndDate");
	    }
	    if (!propertyToSqlMap.Contains("CancellationCutoffDate")) {
		propertyToSqlMap.Add("CancellationCutoffDate",@"CancellationCutoffDate");
	    }
	    if (!propertyToSqlMap.Contains("RegistrationFee")) {
		propertyToSqlMap.Add("RegistrationFee",@"RegistrationFee");
	    }
	    if (!propertyToSqlMap.Contains("RegistrationFeeDescription")) {
		propertyToSqlMap.Add("RegistrationFeeDescription",@"RegistrationFeeDescription");
	    }
	    if (!propertyToSqlMap.Contains("DatesText")) {
		propertyToSqlMap.Add("DatesText",@"DatesText");
	    }
	    if (!propertyToSqlMap.Contains("PrizesText")) {
		propertyToSqlMap.Add("PrizesText",@"PrizesText");
	    }
	    if (!propertyToSqlMap.Contains("SponsorsText")) {
		propertyToSqlMap.Add("SponsorsText",@"SponsorsText");
	    }
	    if (!propertyToSqlMap.Contains("LocationsText")) {
		propertyToSqlMap.Add("LocationsText",@"LocationsText");
	    }
	    if (!propertyToSqlMap.Contains("Organizer.OrganizerId")) {
		propertyToSqlMap.Add("Organizer.OrganizerId",@"OrganizerId");
	    }
	    if (!propertyToSqlMap.Contains("RegisteredParticipants")) {
		propertyToSqlMap.Add("RegisteredParticipants",@"RegisteredParticipants");
	    }
	    if (!propertyToSqlMap.Contains("MaximumHandicap")) {
		propertyToSqlMap.Add("MaximumHandicap",@"MaximumHandicap");
	    }
	    if (!propertyToSqlMap.Contains("Host")) {
		propertyToSqlMap.Add("Host",@"Host");
	    }
	    if (!propertyToSqlMap.Contains("ShowPercentFull")) {
		propertyToSqlMap.Add("ShowPercentFull",@"ShowPercentFull");
	    }
	    if (!propertyToSqlMap.Contains("ShowParticipants")) {
		propertyToSqlMap.Add("ShowParticipants",@"ShowParticipants");
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
	/// Returns a list of all Tournament rows.
	/// </summary>
	/// <returns>List of TournamentData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList() {
	    return GetList(null, null);
	}

	/// <summary>
	/// Returns a filtered list of Tournament rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of TournamentData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public static IList GetList(IWhere whereClause) {
	    return GetList(whereClause, null);
	}

	/// <summary>
	/// Returns an ordered list of Tournament rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of TournamentData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(IOrderBy orderByClause) {
	    return GetList(null, orderByClause);
	}

	/// <summary>
	/// Returns an ordered and filtered list of Tournament rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of TournamentData objects.</returns>
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
	/// Returns a list of all Tournament rows.
	/// </summary>
	/// <returns>List of TournamentData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(Int32 maxRows) {
	    return GetList(null, null, maxRows);
	}

	/// <summary>
	/// Returns a filtered list of Tournament rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of TournamentData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public static IList GetList(IWhere whereClause, Int32 maxRows) {
	    return GetList(whereClause, null, maxRows);
	}

	/// <summary>
	/// Returns an ordered list of Tournament rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of TournamentData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(IOrderBy orderByClause, Int32 maxRows) {
	    return GetList(null, orderByClause, maxRows);
	}

	/// <summary>
	/// Returns an ordered and filtered list of Tournament rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of TournamentData objects.</returns>
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
	/// Finds a Tournament entity using it's primary key.
	/// </summary>
	/// <param name="TournamentId">A key field.</param>
	/// <returns>A TournamentData object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	public static TournamentData Load(IdType tournamentId) {
	    WhereClause w = new WhereClause();
	    w.And("TournamentId", tournamentId.DBValue);
	    SqlDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, w, null);

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Load found no rows for Tournament.");
	    }
	    TournamentData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	private static TournamentData GetDataObjectFromReader(SqlDataReader dataReader) {
	    TournamentData data = new TournamentData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("TournamentId"))) {
		data.TournamentId = IdType.UNSET;
	    } else {
		data.TournamentId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("TournamentId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Name"))) {
		data.Name = StringType.UNSET;
	    } else {
		data.Name = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Name")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Description"))) {
		data.Description = StringType.UNSET;
	    } else {
		data.Description = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Description")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("NumberOfTeams"))) {
		data.NumberOfTeams = IntegerType.UNSET;
	    } else {
		data.NumberOfTeams = new IntegerType(dataReader.GetInt32(dataReader.GetOrdinal("NumberOfTeams")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("TeamSize"))) {
		data.TeamSize = TeamSizeEnum.UNSET;
	    } else {
		data.TeamSize = TeamSizeEnum.GetInstance(dataReader.GetString(dataReader.GetOrdinal("TeamSize")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Format"))) {
		data.Format = TournamentFormatEnum.UNSET;
	    } else {
		data.Format = TournamentFormatEnum.GetInstance(dataReader.GetString(dataReader.GetOrdinal("Format")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("RegistrationBeginDate"))) {
		data.RegistrationBeginDate = DateType.UNSET;
	    } else {
		data.RegistrationBeginDate = new DateType(dataReader.GetDateTime(dataReader.GetOrdinal("RegistrationBeginDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("RegistrationEndDate"))) {
		data.RegistrationEndDate = DateType.UNSET;
	    } else {
		data.RegistrationEndDate = new DateType(dataReader.GetDateTime(dataReader.GetOrdinal("RegistrationEndDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("CancellationCutoffDate"))) {
		data.CancellationCutoffDate = DateType.UNSET;
	    } else {
		data.CancellationCutoffDate = new DateType(dataReader.GetDateTime(dataReader.GetOrdinal("CancellationCutoffDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("RegistrationFee"))) {
		data.RegistrationFee = CurrencyType.UNSET;
	    } else {
		data.RegistrationFee = new CurrencyType(dataReader.GetDecimal(dataReader.GetOrdinal("RegistrationFee")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("RegistrationFeeDescription"))) {
		data.RegistrationFeeDescription = StringType.UNSET;
	    } else {
		data.RegistrationFeeDescription = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("RegistrationFeeDescription")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("DatesText"))) {
		data.DatesText = StringType.UNSET;
	    } else {
		data.DatesText = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("DatesText")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("PrizesText"))) {
		data.PrizesText = StringType.UNSET;
	    } else {
		data.PrizesText = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("PrizesText")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("SponsorsText"))) {
		data.SponsorsText = StringType.UNSET;
	    } else {
		data.SponsorsText = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("SponsorsText")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("LocationsText"))) {
		data.LocationsText = StringType.UNSET;
	    } else {
		data.LocationsText = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("LocationsText")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrganizerId"))) {
		data.Organizer.OrganizerId = IdType.UNSET;
	    } else {
		data.Organizer.OrganizerId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("OrganizerId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("RegisteredParticipants"))) {
		data.RegisteredParticipants = IntegerType.UNSET;
	    } else {
		data.RegisteredParticipants = new IntegerType(dataReader.GetInt32(dataReader.GetOrdinal("RegisteredParticipants")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("MaximumHandicap"))) {
		data.MaximumHandicap = IntegerType.UNSET;
	    } else {
		data.MaximumHandicap = new IntegerType(dataReader.GetInt32(dataReader.GetOrdinal("MaximumHandicap")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Host"))) {
		data.Host = StringType.UNSET;
	    } else {
		data.Host = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Host")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ShowPercentFull"))) {
		data.ShowPercentFull = BooleanType.UNSET;
	    } else {
		data.ShowPercentFull = BooleanType.GetInstance(dataReader.GetString(dataReader.GetOrdinal("ShowPercentFull")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ShowParticipants"))) {
		data.ShowParticipants = BooleanType.UNSET;
	    } else {
		data.ShowParticipants = BooleanType.GetInstance(dataReader.GetString(dataReader.GetOrdinal("ShowParticipants")));
	    }

	    return data;
	}

	/// <summary>
	/// Inserts a record into the Tournament table.
	/// </summary>
	/// <param name=""></param>
	public static IdType Insert(TournamentData data) {
	    return Insert(data, null);
	}

	/// <summary>
	/// Inserts a record into the Tournament table.
	/// </summary>
	/// <param name=""></param>
	public static IdType Insert(TournamentData data, SqlTransaction transaction) {
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spTournament_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    SqlParameter rv = cmd.Parameters.Add("RETURN_VALUE", SqlDbType.Int);
	    rv.Direction = ParameterDirection.ReturnValue;
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Proposed, data.Name.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@NumberOfTeams", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "NumberOfTeams", DataRowVersion.Proposed, data.NumberOfTeams.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@TeamSize", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "TeamSize", DataRowVersion.Proposed, data.TeamSize.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Format", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "Format", DataRowVersion.Proposed, data.Format.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@RegistrationBeginDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "RegistrationBeginDate", DataRowVersion.Proposed, data.RegistrationBeginDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@RegistrationEndDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "RegistrationEndDate", DataRowVersion.Proposed, data.RegistrationEndDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@CancellationCutoffDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "CancellationCutoffDate", DataRowVersion.Proposed, data.CancellationCutoffDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@RegistrationFee", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "RegistrationFee", DataRowVersion.Proposed, data.RegistrationFee.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@RegistrationFeeDescription", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 0, 0, "RegistrationFeeDescription", DataRowVersion.Proposed, data.RegistrationFeeDescription.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DatesText", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "DatesText", DataRowVersion.Proposed, data.DatesText.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@PrizesText", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "PrizesText", DataRowVersion.Proposed, data.PrizesText.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@SponsorsText", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "SponsorsText", DataRowVersion.Proposed, data.SponsorsText.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@LocationsText", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 0, 0, "LocationsText", DataRowVersion.Proposed, data.LocationsText.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrganizerId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Organizer.OrganizerId", DataRowVersion.Proposed, data.Organizer.OrganizerId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@MaximumHandicap", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "MaximumHandicap", DataRowVersion.Proposed, data.MaximumHandicap.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Host", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "Host", DataRowVersion.Proposed, data.Host.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ShowPercentFull", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "ShowPercentFull", DataRowVersion.Proposed, data.ShowPercentFull.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ShowParticipants", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "ShowParticipants", DataRowVersion.Proposed, data.ShowParticipants.DBValue));

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
	/// Updates a record in the Tournament table.
	/// </summary>
	/// <param name=""></param>
	public static void Update(TournamentData data) {
	    Update(data, null);
	}

	/// <summary>
	/// Updates a record in the Tournament table.
	/// </summary>
	/// <param name=""></param>
	public static void Update(TournamentData data, SqlTransaction transaction) {
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spTournament_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@TournamentId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "TournamentId", DataRowVersion.Proposed, data.TournamentId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Proposed, data.Name.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@NumberOfTeams", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "NumberOfTeams", DataRowVersion.Proposed, data.NumberOfTeams.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@TeamSize", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "TeamSize", DataRowVersion.Proposed, data.TeamSize.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Format", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "Format", DataRowVersion.Proposed, data.Format.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@RegistrationBeginDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "RegistrationBeginDate", DataRowVersion.Proposed, data.RegistrationBeginDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@RegistrationEndDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "RegistrationEndDate", DataRowVersion.Proposed, data.RegistrationEndDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@CancellationCutoffDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "CancellationCutoffDate", DataRowVersion.Proposed, data.CancellationCutoffDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@RegistrationFee", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "RegistrationFee", DataRowVersion.Proposed, data.RegistrationFee.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@RegistrationFeeDescription", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 0, 0, "RegistrationFeeDescription", DataRowVersion.Proposed, data.RegistrationFeeDescription.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DatesText", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "DatesText", DataRowVersion.Proposed, data.DatesText.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@PrizesText", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "PrizesText", DataRowVersion.Proposed, data.PrizesText.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@SponsorsText", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "SponsorsText", DataRowVersion.Proposed, data.SponsorsText.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@LocationsText", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 0, 0, "LocationsText", DataRowVersion.Proposed, data.LocationsText.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrganizerId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Organizer.OrganizerId", DataRowVersion.Proposed, data.Organizer.OrganizerId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@MaximumHandicap", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "MaximumHandicap", DataRowVersion.Proposed, data.MaximumHandicap.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Host", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "Host", DataRowVersion.Proposed, data.Host.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ShowPercentFull", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "ShowPercentFull", DataRowVersion.Proposed, data.ShowPercentFull.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ShowParticipants", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "ShowParticipants", DataRowVersion.Proposed, data.ShowParticipants.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }
	}

	/// <summary>
	/// Deletes a record from the Tournament table by TournamentId.
	/// </summary>
	/// <param name=""></param>
	public static void Delete(IdType tournamentId) {
	    Delete(tournamentId, null);
	}

	/// <summary>
	/// Deletes a record from the Tournament table by TournamentId.
	/// </summary>
	/// <param name=""></param>
	public static void Delete(IdType tournamentId, SqlTransaction transaction) {
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spTournament_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    // Create and append the parameters
	    cmd.Parameters.Add(new SqlParameter("@TournamentId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "TournamentId", DataRowVersion.Proposed, tournamentId.DBValue));

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }
	}

	/// <summary>
	/// Returns an object which matches the values for the fields specified.
	/// </summary>
	/// <param name="Host">A field value to be matched.</param>
	/// <returns>The object found.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static TournamentData FindByHost(StringType host) {
	    OrderByClause sort = new OrderByClause("Host");
	    WhereClause filter = new WhereClause();
	    filter.And("Host", host.DBValue);
	    SqlDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, sort);

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("TournamentData.FindByHost found no rows.");
	    }
	    TournamentData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
	}

	/// <summary>
	/// Returns an object which matches the values for the fields specified.
	/// </summary>
	/// <param name="TournamentId">A field value to be matched.</param>
	/// <returns>The object found.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static TournamentData FindByTournamentId(IdType tournamentId, Int32 limit) {
	    OrderByClause sort = new OrderByClause("TournamentId");
	    WhereClause filter = new WhereClause();
	    filter.And("TournamentId", tournamentId.DBValue);
	    SqlDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, sort, limit);

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("TournamentData.FindByTournamentId found no rows.");
	    }
	    TournamentData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
	}

	/// <summary>
	/// Returns a list of objects which match the values for the fields specified.
	/// </summary>
	/// <param name="Name">A field value to be matched.</param>
	/// <returns>The list of TournamentDAO objects found.</returns>
	public static IList FindByName(StringType name, Int32 limit) {
	    OrderByClause sort = new OrderByClause("Name");
	    WhereClause filter = new WhereClause("Name like @Name + '%'");
	    String sql = "Select top " + limit.ToString() + " * from " + VIEW + filter.FormatSql() + sort.FormatSql();
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, sql, CommandType.Text);
	    cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Proposed, name.DBValue));
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
