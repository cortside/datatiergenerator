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
    public class PaymentDAO : Spring2.Core.DAO.EntityDAO {

	private static readonly String VIEW = "vwPayment";
	private static readonly String CONNECTION_STRING_KEY = "ConnectionString";
	private static readonly Int32 COMMAND_TIMEOUT = 15;

	/// <summary>
	/// Hash table mapping entity property names to sql code.
	/// </summary>
	private static Hashtable propertyToSqlMap = new Hashtable();

	/// <summary>
	/// Initializes the static map of property names to sql expressions.
	/// </summary>
	static PaymentDAO() {
	    if (!propertyToSqlMap.Contains("PaymentId")) {
		propertyToSqlMap.Add("PaymentId",@"PaymentId");
	    }
	    if (!propertyToSqlMap.Contains("Tournament.TournamentId")) {
		propertyToSqlMap.Add("Tournament.TournamentId",@"TournamentId");
	    }
	    if (!propertyToSqlMap.Contains("AuthorizationNumber")) {
		propertyToSqlMap.Add("AuthorizationNumber",@"AuthorizationNumber");
	    }
	    if (!propertyToSqlMap.Contains("ReferenceNumber")) {
		propertyToSqlMap.Add("ReferenceNumber",@"ReferenceNumber");
	    }
	    if (!propertyToSqlMap.Contains("TransactionNumber")) {
		propertyToSqlMap.Add("TransactionNumber",@"TransactionNumber");
	    }
	    if (!propertyToSqlMap.Contains("Amount")) {
		propertyToSqlMap.Add("Amount",@"Amount");
	    }
	    if (!propertyToSqlMap.Contains("ProcessDate")) {
		propertyToSqlMap.Add("ProcessDate",@"ProcessDate");
	    }
	    if (!propertyToSqlMap.Contains("PaymentStatus")) {
		propertyToSqlMap.Add("PaymentStatus",@"PaymentStatus");
	    }
	    if (!propertyToSqlMap.Contains("Golfer.GolferId")) {
		propertyToSqlMap.Add("Golfer.GolferId",@"GolferId");
	    }
	    if (!propertyToSqlMap.Contains("CreditCard.Number")) {
		propertyToSqlMap.Add("CreditCard.Number",@"CreditCardNumber");
	    }
	    if (!propertyToSqlMap.Contains("CreditCard.ExpirationDate")) {
		propertyToSqlMap.Add("CreditCard.ExpirationDate",@"ExpirationDate");
	    }
	    if (!propertyToSqlMap.Contains("CreditCard.Name")) {
		propertyToSqlMap.Add("CreditCard.Name",@"CardholderName");
	    }
	    if (!propertyToSqlMap.Contains("CreditCard.Address.Address1")) {
		propertyToSqlMap.Add("CreditCard.Address.Address1",@"Address1");
	    }
	    if (!propertyToSqlMap.Contains("CreditCard.Address.Address2")) {
		propertyToSqlMap.Add("CreditCard.Address.Address2",@"Address2");
	    }
	    if (!propertyToSqlMap.Contains("CreditCard.Address.City")) {
		propertyToSqlMap.Add("CreditCard.Address.City",@"City");
	    }
	    if (!propertyToSqlMap.Contains("CreditCard.Address.State")) {
		propertyToSqlMap.Add("CreditCard.Address.State",@"State");
	    }
	    if (!propertyToSqlMap.Contains("CreditCard.Address.Country")) {
		propertyToSqlMap.Add("CreditCard.Address.Country",@"Country");
	    }
	    if (!propertyToSqlMap.Contains("CreditCard.Address.PostalCode")) {
		propertyToSqlMap.Add("CreditCard.Address.PostalCode",@"PostalCode");
	    }
	    if (!propertyToSqlMap.Contains("ConfirmationCode")) {
		propertyToSqlMap.Add("ConfirmationCode",@"ConfirmationCode");
	    }
	    if (!propertyToSqlMap.Contains("PaymentDate")) {
		propertyToSqlMap.Add("PaymentDate",@"PaymentDate");
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
	/// Returns a list of all Payment rows.
	/// </summary>
	/// <returns>List of PaymentData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList() {
	    return GetList(null, null);
	}

	/// <summary>
	/// Returns a filtered list of Payment rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of PaymentData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public static IList GetList(IWhere whereClause) {
	    return GetList(whereClause, null);
	}

	/// <summary>
	/// Returns an ordered list of Payment rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of PaymentData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(IOrderBy orderByClause) {
	    return GetList(null, orderByClause);
	}

	/// <summary>
	/// Returns an ordered and filtered list of Payment rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of PaymentData objects.</returns>
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
	/// Returns a list of all Payment rows.
	/// </summary>
	/// <returns>List of PaymentData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(Int32 maxRows) {
	    return GetList(null, null, maxRows);
	}

	/// <summary>
	/// Returns a filtered list of Payment rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of PaymentData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public static IList GetList(IWhere whereClause, Int32 maxRows) {
	    return GetList(whereClause, null, maxRows);
	}

	/// <summary>
	/// Returns an ordered list of Payment rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of PaymentData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(IOrderBy orderByClause, Int32 maxRows) {
	    return GetList(null, orderByClause, maxRows);
	}

	/// <summary>
	/// Returns an ordered and filtered list of Payment rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of PaymentData objects.</returns>
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
	/// Finds a Payment entity using it's primary key.
	/// </summary>
	/// <param name="PaymentId">A key field.</param>
	/// <returns>A PaymentData object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	public static PaymentData Load(IdType paymentId) {
	    WhereClause w = new WhereClause();
	    w.And("PaymentId", paymentId.DBValue);
	    SqlDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, w, null);

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Load found no rows for Payment.");
	    }
	    PaymentData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	private static PaymentData GetDataObjectFromReader(SqlDataReader dataReader) {
	    PaymentData data = new PaymentData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("PaymentId"))) {
		data.PaymentId = IdType.UNSET;
	    } else {
		data.PaymentId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("PaymentId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("TournamentId"))) {
		data.Tournament.TournamentId = IdType.UNSET;
	    } else {
		data.Tournament.TournamentId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("TournamentId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("AuthorizationNumber"))) {
		data.AuthorizationNumber = StringType.UNSET;
	    } else {
		data.AuthorizationNumber = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("AuthorizationNumber")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ReferenceNumber"))) {
		data.ReferenceNumber = StringType.UNSET;
	    } else {
		data.ReferenceNumber = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("ReferenceNumber")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("TransactionNumber"))) {
		data.TransactionNumber = StringType.UNSET;
	    } else {
		data.TransactionNumber = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("TransactionNumber")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Amount"))) {
		data.Amount = CurrencyType.UNSET;
	    } else {
		data.Amount = new CurrencyType(dataReader.GetDecimal(dataReader.GetOrdinal("Amount")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ProcessDate"))) {
		data.ProcessDate = DateType.UNSET;
	    } else {
		data.ProcessDate = new DateType(dataReader.GetDateTime(dataReader.GetOrdinal("ProcessDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("PaymentStatus"))) {
		data.PaymentStatus = PaymentStatusEnum.UNSET;
	    } else {
		data.PaymentStatus = PaymentStatusEnum.GetInstance(dataReader.GetString(dataReader.GetOrdinal("PaymentStatus")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("GolferId"))) {
		data.Golfer.GolferId = IdType.UNSET;
	    } else {
		data.Golfer.GolferId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("GolferId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("CreditCardNumber"))) {
		data.CreditCard.Number = StringType.UNSET;
	    } else {
		data.CreditCard.Number = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("CreditCardNumber")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ExpirationDate"))) {
		data.CreditCard.ExpirationDate = DateType.UNSET;
	    } else {
		data.CreditCard.ExpirationDate = new DateType(Spring2.Core.Util.DateUtil.ToDateTimeFromCreditCardDate(dataReader.GetString(dataReader.GetOrdinal("ExpirationDate"))));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("CardholderName"))) {
		data.CreditCard.Name = StringType.UNSET;
	    } else {
		data.CreditCard.Name = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("CardholderName")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Address1"))) {
		data.CreditCard.Address.Address1 = StringType.UNSET;
	    } else {
		data.CreditCard.Address.Address1 = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Address1")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Address2"))) {
		data.CreditCard.Address.Address2 = StringType.UNSET;
	    } else {
		data.CreditCard.Address.Address2 = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Address2")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("City"))) {
		data.CreditCard.Address.City = StringType.UNSET;
	    } else {
		data.CreditCard.Address.City = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("City")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("State"))) {
		data.CreditCard.Address.State = USStateCodeEnum.UNSET;
	    } else {
		data.CreditCard.Address.State = USStateCodeEnum.GetInstance(dataReader.GetString(dataReader.GetOrdinal("State")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Country"))) {
		data.CreditCard.Address.Country = StringType.UNSET;
	    } else {
		data.CreditCard.Address.Country = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Country")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("PostalCode"))) {
		data.CreditCard.Address.PostalCode = StringType.UNSET;
	    } else {
		data.CreditCard.Address.PostalCode = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("PostalCode")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ConfirmationCode"))) {
		data.ConfirmationCode = StringType.UNSET;
	    } else {
		data.ConfirmationCode = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("ConfirmationCode")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("PaymentDate"))) {
		data.PaymentDate = DateType.UNSET;
	    } else {
		data.PaymentDate = new DateType(dataReader.GetDateTime(dataReader.GetOrdinal("PaymentDate")));
	    }

	    return data;
	}

	/// <summary>
	/// Inserts a record into the Payment table.
	/// </summary>
	/// <param name=""></param>
	public static IdType Insert(PaymentData data) {
	    return Insert(data, null);
	}

	/// <summary>
	/// Inserts a record into the Payment table.
	/// </summary>
	/// <param name=""></param>
	public static IdType Insert(PaymentData data, SqlTransaction transaction) {
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spPayment_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    SqlParameter rv = cmd.Parameters.Add("RETURN_VALUE", SqlDbType.Int);
	    rv.Direction = ParameterDirection.ReturnValue;
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@TournamentId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Tournament.TournamentId", DataRowVersion.Proposed, data.Tournament.TournamentId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@AuthorizationNumber", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "AuthorizationNumber", DataRowVersion.Proposed, data.AuthorizationNumber.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ReferenceNumber", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "ReferenceNumber", DataRowVersion.Proposed, data.ReferenceNumber.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@TransactionNumber", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "TransactionNumber", DataRowVersion.Proposed, data.TransactionNumber.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "Amount", DataRowVersion.Proposed, data.Amount.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ProcessDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "ProcessDate", DataRowVersion.Proposed, data.ProcessDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@PaymentStatus", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "PaymentStatus", DataRowVersion.Proposed, data.PaymentStatus.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@GolferId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Golfer.GolferId", DataRowVersion.Proposed, data.Golfer.GolferId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@CreditCardNumber", SqlDbType.VarChar, 25, ParameterDirection.Input, false, 0, 0, "CreditCard.Number", DataRowVersion.Proposed, data.CreditCard.Number.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ExpirationDate", SqlDbType.VarChar, 4, ParameterDirection.Input, false, 0, 0, "CreditCard.ExpirationDate", DataRowVersion.Proposed, data.CreditCard.ExpirationDate.ToString("MMyy")));
	    cmd.Parameters.Add(new SqlParameter("@CardholderName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "CreditCard.Name", DataRowVersion.Proposed, data.CreditCard.Name.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Address1", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "CreditCard.Address.Address1", DataRowVersion.Proposed, data.CreditCard.Address.Address1.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Address2", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "CreditCard.Address.Address2", DataRowVersion.Proposed, data.CreditCard.Address.Address2.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@City", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "CreditCard.Address.City", DataRowVersion.Proposed, data.CreditCard.Address.City.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@State", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "CreditCard.Address.State", DataRowVersion.Proposed, data.CreditCard.Address.State.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Country", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "CreditCard.Address.Country", DataRowVersion.Proposed, data.CreditCard.Address.Country.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@PostalCode", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "CreditCard.Address.PostalCode", DataRowVersion.Proposed, data.CreditCard.Address.PostalCode.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ConfirmationCode", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "ConfirmationCode", DataRowVersion.Proposed, data.ConfirmationCode.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@PaymentDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "PaymentDate", DataRowVersion.Proposed, data.PaymentDate.DBValue));

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
	/// Updates a record in the Payment table.
	/// </summary>
	/// <param name=""></param>
	public static void Update(PaymentData data) {
	    Update(data, null);
	}

	/// <summary>
	/// Updates a record in the Payment table.
	/// </summary>
	/// <param name=""></param>
	public static void Update(PaymentData data, SqlTransaction transaction) {
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spPayment_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@PaymentId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "PaymentId", DataRowVersion.Proposed, data.PaymentId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@TournamentId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Tournament.TournamentId", DataRowVersion.Proposed, data.Tournament.TournamentId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@AuthorizationNumber", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "AuthorizationNumber", DataRowVersion.Proposed, data.AuthorizationNumber.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ReferenceNumber", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "ReferenceNumber", DataRowVersion.Proposed, data.ReferenceNumber.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@TransactionNumber", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "TransactionNumber", DataRowVersion.Proposed, data.TransactionNumber.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "Amount", DataRowVersion.Proposed, data.Amount.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ProcessDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "ProcessDate", DataRowVersion.Proposed, data.ProcessDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@PaymentStatus", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "PaymentStatus", DataRowVersion.Proposed, data.PaymentStatus.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@GolferId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Golfer.GolferId", DataRowVersion.Proposed, data.Golfer.GolferId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@CreditCardNumber", SqlDbType.VarChar, 25, ParameterDirection.Input, false, 0, 0, "CreditCard.Number", DataRowVersion.Proposed, data.CreditCard.Number.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ExpirationDate", SqlDbType.VarChar, 4, ParameterDirection.Input, false, 0, 0, "CreditCard.ExpirationDate", DataRowVersion.Proposed, data.CreditCard.ExpirationDate.ToString("MMyy")));
	    cmd.Parameters.Add(new SqlParameter("@CardholderName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "CreditCard.Name", DataRowVersion.Proposed, data.CreditCard.Name.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Address1", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "CreditCard.Address.Address1", DataRowVersion.Proposed, data.CreditCard.Address.Address1.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Address2", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "CreditCard.Address.Address2", DataRowVersion.Proposed, data.CreditCard.Address.Address2.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@City", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "CreditCard.Address.City", DataRowVersion.Proposed, data.CreditCard.Address.City.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@State", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "CreditCard.Address.State", DataRowVersion.Proposed, data.CreditCard.Address.State.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Country", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "CreditCard.Address.Country", DataRowVersion.Proposed, data.CreditCard.Address.Country.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@PostalCode", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "CreditCard.Address.PostalCode", DataRowVersion.Proposed, data.CreditCard.Address.PostalCode.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ConfirmationCode", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "ConfirmationCode", DataRowVersion.Proposed, data.ConfirmationCode.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@PaymentDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "PaymentDate", DataRowVersion.Proposed, data.PaymentDate.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }
	}

	/// <summary>
	/// Deletes a record from the Payment table by PaymentId.
	/// </summary>
	/// <param name=""></param>
	public static void Delete(IdType paymentId) {
	    Delete(paymentId, null);
	}

	/// <summary>
	/// Deletes a record from the Payment table by PaymentId.
	/// </summary>
	/// <param name=""></param>
	public static void Delete(IdType paymentId, SqlTransaction transaction) {
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spPayment_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    // Create and append the parameters
	    cmd.Parameters.Add(new SqlParameter("@PaymentId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "PaymentId", DataRowVersion.Proposed, paymentId.DBValue));

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }
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
