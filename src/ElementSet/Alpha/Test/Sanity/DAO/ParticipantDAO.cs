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
	    if (!propertyToSqlMap.Contains("IsValid")) {
		propertyToSqlMap.Add("IsValid",@"IsValid");
	    }
	    if (!propertyToSqlMap.Contains("RegistrationFee")) {
		propertyToSqlMap.Add("RegistrationFee",@"RegistrationFee");
	    }
	    if (!propertyToSqlMap.Contains("Tournament.TournamentId")) {
		propertyToSqlMap.Add("Tournament.TournamentId",@"Tournament_TournamentId");
	    }
	    if (!propertyToSqlMap.Contains("Tournament.Name")) {
		propertyToSqlMap.Add("Tournament.Name",@"Tournament_Name");
	    }
	    if (!propertyToSqlMap.Contains("Tournament.Description")) {
		propertyToSqlMap.Add("Tournament.Description",@"Tournament_Description");
	    }
	    if (!propertyToSqlMap.Contains("Tournament.NumberOfTeams")) {
		propertyToSqlMap.Add("Tournament.NumberOfTeams",@"Tournament_NumberOfTeams");
	    }
	    if (!propertyToSqlMap.Contains("Tournament.TeamSize")) {
		propertyToSqlMap.Add("Tournament.TeamSize",@"Tournament_TeamSize");
	    }
	    if (!propertyToSqlMap.Contains("Tournament.Format")) {
		propertyToSqlMap.Add("Tournament.Format",@"Tournament_Format");
	    }
	    if (!propertyToSqlMap.Contains("Tournament.RegistrationBeginDate")) {
		propertyToSqlMap.Add("Tournament.RegistrationBeginDate",@"Tournament_RegistrationBeginDate");
	    }
	    if (!propertyToSqlMap.Contains("Tournament.RegistrationEndDate")) {
		propertyToSqlMap.Add("Tournament.RegistrationEndDate",@"Tournament_RegistrationEndDate");
	    }
	    if (!propertyToSqlMap.Contains("Tournament.CancellationCutoffDate")) {
		propertyToSqlMap.Add("Tournament.CancellationCutoffDate",@"Tournament_CancellationCutoffDate");
	    }
	    if (!propertyToSqlMap.Contains("Tournament.RegistrationFee")) {
		propertyToSqlMap.Add("Tournament.RegistrationFee",@"Tournament_RegistrationFee");
	    }
	    if (!propertyToSqlMap.Contains("Tournament.RegistrationFeeDescription")) {
		propertyToSqlMap.Add("Tournament.RegistrationFeeDescription",@"Tournament_RegistrationFeeDescription");
	    }
	    if (!propertyToSqlMap.Contains("Tournament.DatesText")) {
		propertyToSqlMap.Add("Tournament.DatesText",@"Tournament_DatesText");
	    }
	    if (!propertyToSqlMap.Contains("Tournament.PrizesText")) {
		propertyToSqlMap.Add("Tournament.PrizesText",@"Tournament_PrizesText");
	    }
	    if (!propertyToSqlMap.Contains("Tournament.SponsorsText")) {
		propertyToSqlMap.Add("Tournament.SponsorsText",@"Tournament_SponsorsText");
	    }
	    if (!propertyToSqlMap.Contains("Tournament.LocationsText")) {
		propertyToSqlMap.Add("Tournament.LocationsText",@"Tournament_LocationsText");
	    }
	    if (!propertyToSqlMap.Contains("Tournament.Organizer.OrganizerId")) {
		propertyToSqlMap.Add("Tournament.Organizer.OrganizerId",@"Tournament_OrganizerId");
	    }
	    if (!propertyToSqlMap.Contains("Tournament.RegisteredParticipants")) {
		propertyToSqlMap.Add("Tournament.RegisteredParticipants",@"Tournament_RegisteredParticipants");
	    }
	    if (!propertyToSqlMap.Contains("Tournament.MaximumHandicap")) {
		propertyToSqlMap.Add("Tournament.MaximumHandicap",@"Tournament_MaximumHandicap");
	    }
	    if (!propertyToSqlMap.Contains("Tournament.Host")) {
		propertyToSqlMap.Add("Tournament.Host",@"Tournament_Host");
	    }
	    if (!propertyToSqlMap.Contains("Tournament.ShowPercentFull")) {
		propertyToSqlMap.Add("Tournament.ShowPercentFull",@"Tournament_ShowPercentFull");
	    }
	    if (!propertyToSqlMap.Contains("Tournament.ShowParticipants")) {
		propertyToSqlMap.Add("Tournament.ShowParticipants",@"Tournament_ShowParticipants");
	    }
	    if (!propertyToSqlMap.Contains("Team.TeamId")) {
		propertyToSqlMap.Add("Team.TeamId",@"Team_TeamId");
	    }
	    if (!propertyToSqlMap.Contains("Team.RegistrationKey")) {
		propertyToSqlMap.Add("Team.RegistrationKey",@"Team_RegistrationKey");
	    }
	    if (!propertyToSqlMap.Contains("Team.Status")) {
		propertyToSqlMap.Add("Team.Status",@"Team_Status");
	    }
	    if (!propertyToSqlMap.Contains("Team.TournamentId")) {
		propertyToSqlMap.Add("Team.TournamentId",@"Team_TournamentId");
	    }
	    if (!propertyToSqlMap.Contains("Golfer.GolferId")) {
		propertyToSqlMap.Add("Golfer.GolferId",@"Golfer_GolferId");
	    }
	    if (!propertyToSqlMap.Contains("Golfer.FirstName")) {
		propertyToSqlMap.Add("Golfer.FirstName",@"Golfer_FirstName");
	    }
	    if (!propertyToSqlMap.Contains("Golfer.MiddleInitial")) {
		propertyToSqlMap.Add("Golfer.MiddleInitial",@"Golfer_MiddleInitial");
	    }
	    if (!propertyToSqlMap.Contains("Golfer.LastName")) {
		propertyToSqlMap.Add("Golfer.LastName",@"Golfer_LastName");
	    }
	    if (!propertyToSqlMap.Contains("Golfer.Phone")) {
		propertyToSqlMap.Add("Golfer.Phone",@"Golfer_Phone");
	    }
	    if (!propertyToSqlMap.Contains("Golfer.Email")) {
		propertyToSqlMap.Add("Golfer.Email",@"Golfer_Email");
	    }
	    if (!propertyToSqlMap.Contains("Golfer.Address.Address1")) {
		propertyToSqlMap.Add("Golfer.Address.Address1",@"Golfer_Address1");
	    }
	    if (!propertyToSqlMap.Contains("Golfer.Address.Address2")) {
		propertyToSqlMap.Add("Golfer.Address.Address2",@"Golfer_Address2");
	    }
	    if (!propertyToSqlMap.Contains("Golfer.Address.City")) {
		propertyToSqlMap.Add("Golfer.Address.City",@"Golfer_City");
	    }
	    if (!propertyToSqlMap.Contains("Golfer.Address.State")) {
		propertyToSqlMap.Add("Golfer.Address.State",@"Golfer_State");
	    }
	    if (!propertyToSqlMap.Contains("Golfer.Address.Country")) {
		propertyToSqlMap.Add("Golfer.Address.Country",@"Golfer_Country");
	    }
	    if (!propertyToSqlMap.Contains("Golfer.Address.PostalCode")) {
		propertyToSqlMap.Add("Golfer.Address.PostalCode",@"Golfer_PostalCode");
	    }
	    if (!propertyToSqlMap.Contains("Golfer.DateOfBirth")) {
		propertyToSqlMap.Add("Golfer.DateOfBirth",@"Golfer_DateOfBirth");
	    }
	    if (!propertyToSqlMap.Contains("Golfer.Handicap")) {
		propertyToSqlMap.Add("Golfer.Handicap",@"Golfer_Handicap");
	    }
	    if (!propertyToSqlMap.Contains("Golfer.CourseNumber")) {
		propertyToSqlMap.Add("Golfer.CourseNumber",@"Golfer_CourseNumber");
	    }
	    if (!propertyToSqlMap.Contains("Golfer.PlayerNumber")) {
		propertyToSqlMap.Add("Golfer.PlayerNumber",@"Golfer_PlayerNumber");
	    }
	    if (!propertyToSqlMap.Contains("Golfer.Gender")) {
		propertyToSqlMap.Add("Golfer.Gender",@"Golfer_Gender");
	    }
	    if (!propertyToSqlMap.Contains("Golfer.GolferStatus")) {
		propertyToSqlMap.Add("Golfer.GolferStatus",@"Golfer_GolferStatus");
	    }
	    if (!propertyToSqlMap.Contains("Payment.PaymentId")) {
		propertyToSqlMap.Add("Payment.PaymentId",@"Payment_PaymentId");
	    }
	    if (!propertyToSqlMap.Contains("Payment.Tournament.TournamentId")) {
		propertyToSqlMap.Add("Payment.Tournament.TournamentId",@"Payment_TournamentId");
	    }
	    if (!propertyToSqlMap.Contains("Payment.AuthorizationNumber")) {
		propertyToSqlMap.Add("Payment.AuthorizationNumber",@"Payment_AuthorizationNumber");
	    }
	    if (!propertyToSqlMap.Contains("Payment.ReferenceNumber")) {
		propertyToSqlMap.Add("Payment.ReferenceNumber",@"Payment_ReferenceNumber");
	    }
	    if (!propertyToSqlMap.Contains("Payment.TransactionNumber")) {
		propertyToSqlMap.Add("Payment.TransactionNumber",@"Payment_TransactionNumber");
	    }
	    if (!propertyToSqlMap.Contains("Payment.Amount")) {
		propertyToSqlMap.Add("Payment.Amount",@"Payment_Amount");
	    }
	    if (!propertyToSqlMap.Contains("Payment.ProcessDate")) {
		propertyToSqlMap.Add("Payment.ProcessDate",@"Payment_ProcessDate");
	    }
	    if (!propertyToSqlMap.Contains("Payment.PaymentStatus")) {
		propertyToSqlMap.Add("Payment.PaymentStatus",@"Payment_PaymentStatus");
	    }
	    if (!propertyToSqlMap.Contains("Payment.Golfer.GolferId")) {
		propertyToSqlMap.Add("Payment.Golfer.GolferId",@"Payment_GolferId");
	    }
	    if (!propertyToSqlMap.Contains("Payment.CreditCard.Number")) {
		propertyToSqlMap.Add("Payment.CreditCard.Number",@"Payment_CreditCardNumber");
	    }
	    if (!propertyToSqlMap.Contains("Payment.CreditCard.ExpirationDate")) {
		propertyToSqlMap.Add("Payment.CreditCard.ExpirationDate",@"Payment_ExpirationDate");
	    }
	    if (!propertyToSqlMap.Contains("Payment.CreditCard.Name")) {
		propertyToSqlMap.Add("Payment.CreditCard.Name",@"Payment_CardholderName");
	    }
	    if (!propertyToSqlMap.Contains("Payment.CreditCard.Address.Address1")) {
		propertyToSqlMap.Add("Payment.CreditCard.Address.Address1",@"Payment_Address1");
	    }
	    if (!propertyToSqlMap.Contains("Payment.CreditCard.Address.Address2")) {
		propertyToSqlMap.Add("Payment.CreditCard.Address.Address2",@"Payment_Address2");
	    }
	    if (!propertyToSqlMap.Contains("Payment.CreditCard.Address.City")) {
		propertyToSqlMap.Add("Payment.CreditCard.Address.City",@"Payment_City");
	    }
	    if (!propertyToSqlMap.Contains("Payment.CreditCard.Address.State")) {
		propertyToSqlMap.Add("Payment.CreditCard.Address.State",@"Payment_State");
	    }
	    if (!propertyToSqlMap.Contains("Payment.CreditCard.Address.Country")) {
		propertyToSqlMap.Add("Payment.CreditCard.Address.Country",@"Payment_Country");
	    }
	    if (!propertyToSqlMap.Contains("Payment.CreditCard.Address.PostalCode")) {
		propertyToSqlMap.Add("Payment.CreditCard.Address.PostalCode",@"Payment_PostalCode");
	    }
	    if (!propertyToSqlMap.Contains("Payment.ConfirmationCode")) {
		propertyToSqlMap.Add("Payment.ConfirmationCode",@"Payment_ConfirmationCode");
	    }
	    if (!propertyToSqlMap.Contains("Payment.PaymentDate")) {
		propertyToSqlMap.Add("Payment.PaymentDate",@"Payment_PaymentDate");
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
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Tournament_TournamentId"))) {
		data.Tournament.TournamentId = IdType.UNSET;
	    } else {
		data.Tournament.TournamentId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("Tournament_TournamentId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Tournament_Name"))) {
		data.Tournament.Name = StringType.UNSET;
	    } else {
		data.Tournament.Name = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Tournament_Name")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Tournament_Description"))) {
		data.Tournament.Description = StringType.UNSET;
	    } else {
		data.Tournament.Description = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Tournament_Description")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Tournament_NumberOfTeams"))) {
		data.Tournament.NumberOfTeams = IntegerType.UNSET;
	    } else {
		data.Tournament.NumberOfTeams = new IntegerType(dataReader.GetInt32(dataReader.GetOrdinal("Tournament_NumberOfTeams")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Tournament_TeamSize"))) {
		data.Tournament.TeamSize = TeamSizeEnum.UNSET;
	    } else {
		data.Tournament.TeamSize = TeamSizeEnum.GetInstance(dataReader.GetString(dataReader.GetOrdinal("Tournament_TeamSize")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Tournament_Format"))) {
		data.Tournament.Format = TournamentFormatEnum.UNSET;
	    } else {
		data.Tournament.Format = TournamentFormatEnum.GetInstance(dataReader.GetString(dataReader.GetOrdinal("Tournament_Format")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Tournament_RegistrationBeginDate"))) {
		data.Tournament.RegistrationBeginDate = DateType.UNSET;
	    } else {
		data.Tournament.RegistrationBeginDate = new DateType(dataReader.GetDateTime(dataReader.GetOrdinal("Tournament_RegistrationBeginDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Tournament_RegistrationEndDate"))) {
		data.Tournament.RegistrationEndDate = DateType.UNSET;
	    } else {
		data.Tournament.RegistrationEndDate = new DateType(dataReader.GetDateTime(dataReader.GetOrdinal("Tournament_RegistrationEndDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Tournament_CancellationCutoffDate"))) {
		data.Tournament.CancellationCutoffDate = DateType.UNSET;
	    } else {
		data.Tournament.CancellationCutoffDate = new DateType(dataReader.GetDateTime(dataReader.GetOrdinal("Tournament_CancellationCutoffDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Tournament_RegistrationFee"))) {
		data.Tournament.RegistrationFee = CurrencyType.UNSET;
	    } else {
		data.Tournament.RegistrationFee = new CurrencyType(dataReader.GetDecimal(dataReader.GetOrdinal("Tournament_RegistrationFee")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Tournament_RegistrationFeeDescription"))) {
		data.Tournament.RegistrationFeeDescription = StringType.UNSET;
	    } else {
		data.Tournament.RegistrationFeeDescription = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Tournament_RegistrationFeeDescription")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Tournament_DatesText"))) {
		data.Tournament.DatesText = StringType.UNSET;
	    } else {
		data.Tournament.DatesText = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Tournament_DatesText")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Tournament_PrizesText"))) {
		data.Tournament.PrizesText = StringType.UNSET;
	    } else {
		data.Tournament.PrizesText = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Tournament_PrizesText")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Tournament_SponsorsText"))) {
		data.Tournament.SponsorsText = StringType.UNSET;
	    } else {
		data.Tournament.SponsorsText = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Tournament_SponsorsText")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Tournament_LocationsText"))) {
		data.Tournament.LocationsText = StringType.UNSET;
	    } else {
		data.Tournament.LocationsText = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Tournament_LocationsText")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Tournament_OrganizerId"))) {
		data.Tournament.Organizer.OrganizerId = IdType.UNSET;
	    } else {
		data.Tournament.Organizer.OrganizerId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("Tournament_OrganizerId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Tournament_RegisteredParticipants"))) {
		data.Tournament.RegisteredParticipants = IntegerType.UNSET;
	    } else {
		data.Tournament.RegisteredParticipants = new IntegerType(dataReader.GetInt32(dataReader.GetOrdinal("Tournament_RegisteredParticipants")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Tournament_MaximumHandicap"))) {
		data.Tournament.MaximumHandicap = IntegerType.UNSET;
	    } else {
		data.Tournament.MaximumHandicap = new IntegerType(dataReader.GetInt32(dataReader.GetOrdinal("Tournament_MaximumHandicap")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Tournament_Host"))) {
		data.Tournament.Host = StringType.UNSET;
	    } else {
		data.Tournament.Host = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Tournament_Host")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Tournament_ShowPercentFull"))) {
		data.Tournament.ShowPercentFull = BooleanType.UNSET;
	    } else {
		data.Tournament.ShowPercentFull = BooleanType.GetInstance(dataReader.GetString(dataReader.GetOrdinal("Tournament_ShowPercentFull")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Tournament_ShowParticipants"))) {
		data.Tournament.ShowParticipants = BooleanType.UNSET;
	    } else {
		data.Tournament.ShowParticipants = BooleanType.GetInstance(dataReader.GetString(dataReader.GetOrdinal("Tournament_ShowParticipants")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Team_TeamId"))) {
		data.Team.TeamId = IdType.UNSET;
	    } else {
		data.Team.TeamId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("Team_TeamId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Team_RegistrationKey"))) {
		data.Team.RegistrationKey = StringType.UNSET;
	    } else {
		data.Team.RegistrationKey = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Team_RegistrationKey")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Team_Status"))) {
		data.Team.Status = TeamStatusEnum.UNSET;
	    } else {
		data.Team.Status = TeamStatusEnum.GetInstance(dataReader.GetString(dataReader.GetOrdinal("Team_Status")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Team_TournamentId"))) {
		data.Team.TournamentId = IdType.UNSET;
	    } else {
		data.Team.TournamentId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("Team_TournamentId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Golfer_GolferId"))) {
		data.Golfer.GolferId = IdType.UNSET;
	    } else {
		data.Golfer.GolferId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("Golfer_GolferId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Golfer_FirstName"))) {
		data.Golfer.FirstName = StringType.UNSET;
	    } else {
		data.Golfer.FirstName = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Golfer_FirstName")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Golfer_MiddleInitial"))) {
		data.Golfer.MiddleInitial = StringType.UNSET;
	    } else {
		data.Golfer.MiddleInitial = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Golfer_MiddleInitial")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Golfer_LastName"))) {
		data.Golfer.LastName = StringType.UNSET;
	    } else {
		data.Golfer.LastName = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Golfer_LastName")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Golfer_Phone"))) {
		data.Golfer.Phone = StringType.UNSET;
	    } else {
		data.Golfer.Phone = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Golfer_Phone")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Golfer_Email"))) {
		data.Golfer.Email = StringType.UNSET;
	    } else {
		data.Golfer.Email = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Golfer_Email")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Golfer_Address1"))) {
		data.Golfer.Address.Address1 = StringType.UNSET;
	    } else {
		data.Golfer.Address.Address1 = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Golfer_Address1")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Golfer_Address2"))) {
		data.Golfer.Address.Address2 = StringType.UNSET;
	    } else {
		data.Golfer.Address.Address2 = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Golfer_Address2")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Golfer_City"))) {
		data.Golfer.Address.City = StringType.UNSET;
	    } else {
		data.Golfer.Address.City = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Golfer_City")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Golfer_State"))) {
		data.Golfer.Address.State = USStateCodeEnum.UNSET;
	    } else {
		data.Golfer.Address.State = USStateCodeEnum.GetInstance(dataReader.GetString(dataReader.GetOrdinal("Golfer_State")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Golfer_Country"))) {
		data.Golfer.Address.Country = StringType.UNSET;
	    } else {
		data.Golfer.Address.Country = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Golfer_Country")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Golfer_PostalCode"))) {
		data.Golfer.Address.PostalCode = StringType.UNSET;
	    } else {
		data.Golfer.Address.PostalCode = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Golfer_PostalCode")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Golfer_DateOfBirth"))) {
		data.Golfer.DateOfBirth = DateType.UNSET;
	    } else {
		data.Golfer.DateOfBirth = new DateType(dataReader.GetDateTime(dataReader.GetOrdinal("Golfer_DateOfBirth")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Golfer_Handicap"))) {
		data.Golfer.Handicap = DecimalType.UNSET;
	    } else {
		data.Golfer.Handicap = new DecimalType(dataReader.GetDecimal(dataReader.GetOrdinal("Golfer_Handicap")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Golfer_CourseNumber"))) {
		data.Golfer.CourseNumber = StringType.UNSET;
	    } else {
		data.Golfer.CourseNumber = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Golfer_CourseNumber")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Golfer_PlayerNumber"))) {
		data.Golfer.PlayerNumber = StringType.UNSET;
	    } else {
		data.Golfer.PlayerNumber = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Golfer_PlayerNumber")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Golfer_Gender"))) {
		data.Golfer.Gender = GenderType.UNSET;
	    } else {
		data.Golfer.Gender = GenderType.GetInstance(dataReader.GetString(dataReader.GetOrdinal("Golfer_Gender")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Golfer_GolferStatus"))) {
		data.Golfer.GolferStatus = GolferStatusEnum.UNSET;
	    } else {
		data.Golfer.GolferStatus = GolferStatusEnum.GetInstance(dataReader.GetString(dataReader.GetOrdinal("Golfer_GolferStatus")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Payment_PaymentId"))) {
		data.Payment.PaymentId = IdType.UNSET;
	    } else {
		data.Payment.PaymentId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("Payment_PaymentId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Payment_TournamentId"))) {
		data.Payment.Tournament.TournamentId = IdType.UNSET;
	    } else {
		data.Payment.Tournament.TournamentId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("Payment_TournamentId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Payment_AuthorizationNumber"))) {
		data.Payment.AuthorizationNumber = StringType.UNSET;
	    } else {
		data.Payment.AuthorizationNumber = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Payment_AuthorizationNumber")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Payment_ReferenceNumber"))) {
		data.Payment.ReferenceNumber = StringType.UNSET;
	    } else {
		data.Payment.ReferenceNumber = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Payment_ReferenceNumber")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Payment_TransactionNumber"))) {
		data.Payment.TransactionNumber = StringType.UNSET;
	    } else {
		data.Payment.TransactionNumber = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Payment_TransactionNumber")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Payment_Amount"))) {
		data.Payment.Amount = CurrencyType.UNSET;
	    } else {
		data.Payment.Amount = new CurrencyType(dataReader.GetDecimal(dataReader.GetOrdinal("Payment_Amount")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Payment_ProcessDate"))) {
		data.Payment.ProcessDate = DateType.UNSET;
	    } else {
		data.Payment.ProcessDate = new DateType(dataReader.GetDateTime(dataReader.GetOrdinal("Payment_ProcessDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Payment_PaymentStatus"))) {
		data.Payment.PaymentStatus = PaymentStatusEnum.UNSET;
	    } else {
		data.Payment.PaymentStatus = PaymentStatusEnum.GetInstance(dataReader.GetString(dataReader.GetOrdinal("Payment_PaymentStatus")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Payment_GolferId"))) {
		data.Payment.Golfer.GolferId = IdType.UNSET;
	    } else {
		data.Payment.Golfer.GolferId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("Payment_GolferId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Payment_CreditCardNumber"))) {
		data.Payment.CreditCard.Number = StringType.UNSET;
	    } else {
		data.Payment.CreditCard.Number = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Payment_CreditCardNumber")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Payment_ExpirationDate"))) {
		data.Payment.CreditCard.ExpirationDate = DateType.UNSET;
	    } else {
		data.Payment.CreditCard.ExpirationDate = new DateType(Spring2.Core.Util.DateUtil.ToDateTimeFromCreditCardDate(dataReader.GetString(dataReader.GetOrdinal("Payment_ExpirationDate"))));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Payment_CardholderName"))) {
		data.Payment.CreditCard.Name = StringType.UNSET;
	    } else {
		data.Payment.CreditCard.Name = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Payment_CardholderName")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Payment_Address1"))) {
		data.Payment.CreditCard.Address.Address1 = StringType.UNSET;
	    } else {
		data.Payment.CreditCard.Address.Address1 = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Payment_Address1")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Payment_Address2"))) {
		data.Payment.CreditCard.Address.Address2 = StringType.UNSET;
	    } else {
		data.Payment.CreditCard.Address.Address2 = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Payment_Address2")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Payment_City"))) {
		data.Payment.CreditCard.Address.City = StringType.UNSET;
	    } else {
		data.Payment.CreditCard.Address.City = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Payment_City")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Payment_State"))) {
		data.Payment.CreditCard.Address.State = USStateCodeEnum.UNSET;
	    } else {
		data.Payment.CreditCard.Address.State = USStateCodeEnum.GetInstance(dataReader.GetString(dataReader.GetOrdinal("Payment_State")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Payment_Country"))) {
		data.Payment.CreditCard.Address.Country = StringType.UNSET;
	    } else {
		data.Payment.CreditCard.Address.Country = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Payment_Country")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Payment_PostalCode"))) {
		data.Payment.CreditCard.Address.PostalCode = StringType.UNSET;
	    } else {
		data.Payment.CreditCard.Address.PostalCode = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Payment_PostalCode")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Payment_ConfirmationCode"))) {
		data.Payment.ConfirmationCode = StringType.UNSET;
	    } else {
		data.Payment.ConfirmationCode = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Payment_ConfirmationCode")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Payment_PaymentDate"))) {
		data.Payment.PaymentDate = DateType.UNSET;
	    } else {
		data.Payment.PaymentDate = new DateType(dataReader.GetDateTime(dataReader.GetOrdinal("Payment_PaymentDate")));
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
	/// <param name="Payment_ConfirmationCode">A field value to be matched.</param>
	/// <returns>The list of ParticipantDAO objects found.</returns>
	public static IList FindByConfirmationCode(StringType payment_ConfirmationCode) {
	    OrderByClause sort = new OrderByClause("Payment_ConfirmationCode");
	    WhereClause filter = new WhereClause();
	    filter.And("Payment_ConfirmationCode", payment_ConfirmationCode.DBValue);

	    return GetList(filter, sort);
	}

	/// <summary>
	/// Returns a list of objects which match the values for the fields specified.
	/// </summary>
	/// <param name="TournamentId">A field value to be matched.</param>
	/// <returns>The list of ParticipantDAO objects found.</returns>
	public static IList FindByTournamentId(IdType tournament_TournamentId) {
	    OrderByClause sort = new OrderByClause("TournamentId");
	    WhereClause filter = new WhereClause("TournamentId = @Tournament_TournamentId");
	    String sql = "Select * from " + VIEW + filter.FormatSql() + sort.FormatSql();
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, sql, CommandType.Text);
	    cmd.Parameters.Add(new SqlParameter("@Tournament_TournamentId", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "Tournament.TournamentId", DataRowVersion.Proposed, tournament_TournamentId.DBValue));
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
