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
    public class GolferDAO : Spring2.Core.DAO.EntityDAO {

	private static readonly String VIEW = "vwGolfer";
	private static readonly String CONNECTION_STRING_KEY = "ConnectionString";
	private static readonly Int32 COMMAND_TIMEOUT = 15;

	/// <summary>
	/// Hash table mapping entity property names to sql code.
	/// </summary>
	private static Hashtable propertyToSqlMap = new Hashtable();

	/// <summary>
	/// Initializes the static map of property names to sql expressions.
	/// </summary>
	static GolferDAO() {
	    if (!propertyToSqlMap.Contains("GolferId")) {
		propertyToSqlMap.Add("GolferId",@"GolferId");
	    }
	    if (!propertyToSqlMap.Contains("FirstName")) {
		propertyToSqlMap.Add("FirstName",@"FirstName");
	    }
	    if (!propertyToSqlMap.Contains("MiddleInitial")) {
		propertyToSqlMap.Add("MiddleInitial",@"MiddleInitial");
	    }
	    if (!propertyToSqlMap.Contains("LastName")) {
		propertyToSqlMap.Add("LastName",@"LastName");
	    }
	    if (!propertyToSqlMap.Contains("Phone")) {
		propertyToSqlMap.Add("Phone",@"Phone");
	    }
	    if (!propertyToSqlMap.Contains("Email")) {
		propertyToSqlMap.Add("Email",@"Email");
	    }
	    if (!propertyToSqlMap.Contains("Address.Address1")) {
		propertyToSqlMap.Add("Address.Address1",@"Address1");
	    }
	    if (!propertyToSqlMap.Contains("Address.Address2")) {
		propertyToSqlMap.Add("Address.Address2",@"Address2");
	    }
	    if (!propertyToSqlMap.Contains("Address.City")) {
		propertyToSqlMap.Add("Address.City",@"City");
	    }
	    if (!propertyToSqlMap.Contains("Address.State")) {
		propertyToSqlMap.Add("Address.State",@"State");
	    }
	    if (!propertyToSqlMap.Contains("Address.Country")) {
		propertyToSqlMap.Add("Address.Country",@"Country");
	    }
	    if (!propertyToSqlMap.Contains("Address.PostalCode")) {
		propertyToSqlMap.Add("Address.PostalCode",@"PostalCode");
	    }
	    if (!propertyToSqlMap.Contains("DateOfBirth")) {
		propertyToSqlMap.Add("DateOfBirth",@"DateOfBirth");
	    }
	    if (!propertyToSqlMap.Contains("Handicap")) {
		propertyToSqlMap.Add("Handicap",@"Handicap");
	    }
	    if (!propertyToSqlMap.Contains("CourseNumber")) {
		propertyToSqlMap.Add("CourseNumber",@"CourseNumber");
	    }
	    if (!propertyToSqlMap.Contains("PlayerNumber")) {
		propertyToSqlMap.Add("PlayerNumber",@"PlayerNumber");
	    }
	    if (!propertyToSqlMap.Contains("Gender")) {
		propertyToSqlMap.Add("Gender",@"Gender");
	    }
	    if (!propertyToSqlMap.Contains("GolferStatus")) {
		propertyToSqlMap.Add("GolferStatus",@"GolferStatus");
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
	/// Returns a list of all Golfer rows.
	/// </summary>
	/// <returns>List of GolferData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList() {
	    return GetList(null, null);
	}

	/// <summary>
	/// Returns a filtered list of Golfer rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of GolferData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public static IList GetList(IWhere whereClause) {
	    return GetList(whereClause, null);
	}

	/// <summary>
	/// Returns an ordered list of Golfer rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of GolferData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(IOrderBy orderByClause) {
	    return GetList(null, orderByClause);
	}

	/// <summary>
	/// Returns an ordered and filtered list of Golfer rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of GolferData objects.</returns>
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
	/// Returns a list of all Golfer rows.
	/// </summary>
	/// <returns>List of GolferData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(Int32 maxRows) {
	    return GetList(null, null, maxRows);
	}

	/// <summary>
	/// Returns a filtered list of Golfer rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of GolferData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public static IList GetList(IWhere whereClause, Int32 maxRows) {
	    return GetList(whereClause, null, maxRows);
	}

	/// <summary>
	/// Returns an ordered list of Golfer rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of GolferData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(IOrderBy orderByClause, Int32 maxRows) {
	    return GetList(null, orderByClause, maxRows);
	}

	/// <summary>
	/// Returns an ordered and filtered list of Golfer rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of GolferData objects.</returns>
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
	/// Finds a Golfer entity using it's primary key.
	/// </summary>
	/// <param name="GolferId">A key field.</param>
	/// <returns>A GolferData object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	public static GolferData Load(IdType golferId) {
	    WhereClause w = new WhereClause();
	    w.And("GolferId", golferId.DBValue);
	    SqlDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, w, null);

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Load found no rows for Golfer.");
	    }
	    GolferData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	private static GolferData GetDataObjectFromReader(SqlDataReader dataReader) {
	    GolferData data = new GolferData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("GolferId"))) {
		data.GolferId = IdType.UNSET;
	    } else {
		data.GolferId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("GolferId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("FirstName"))) {
		data.FirstName = StringType.UNSET;
	    } else {
		data.FirstName = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("FirstName")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("MiddleInitial"))) {
		data.MiddleInitial = StringType.UNSET;
	    } else {
		data.MiddleInitial = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("MiddleInitial")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("LastName"))) {
		data.LastName = StringType.UNSET;
	    } else {
		data.LastName = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("LastName")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Phone"))) {
		data.Phone = StringType.UNSET;
	    } else {
		data.Phone = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Phone")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Email"))) {
		data.Email = StringType.UNSET;
	    } else {
		data.Email = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Email")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Address1"))) {
		data.Address.Address1 = StringType.UNSET;
	    } else {
		data.Address.Address1 = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Address1")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Address2"))) {
		data.Address.Address2 = StringType.UNSET;
	    } else {
		data.Address.Address2 = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Address2")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("City"))) {
		data.Address.City = StringType.UNSET;
	    } else {
		data.Address.City = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("City")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("State"))) {
		data.Address.State = USStateCodeEnum.UNSET;
	    } else {
		data.Address.State = USStateCodeEnum.GetInstance(dataReader.GetString(dataReader.GetOrdinal("State")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Country"))) {
		data.Address.Country = StringType.UNSET;
	    } else {
		data.Address.Country = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Country")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("PostalCode"))) {
		data.Address.PostalCode = StringType.UNSET;
	    } else {
		data.Address.PostalCode = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("PostalCode")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("DateOfBirth"))) {
		data.DateOfBirth = DateType.UNSET;
	    } else {
		data.DateOfBirth = new DateType(dataReader.GetDateTime(dataReader.GetOrdinal("DateOfBirth")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Handicap"))) {
		data.Handicap = DecimalType.UNSET;
	    } else {
		data.Handicap = new DecimalType(dataReader.GetDecimal(dataReader.GetOrdinal("Handicap")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("CourseNumber"))) {
		data.CourseNumber = StringType.UNSET;
	    } else {
		data.CourseNumber = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("CourseNumber")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("PlayerNumber"))) {
		data.PlayerNumber = StringType.UNSET;
	    } else {
		data.PlayerNumber = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("PlayerNumber")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Gender"))) {
		data.Gender = GenderType.UNSET;
	    } else {
		data.Gender = GenderType.GetInstance(dataReader.GetString(dataReader.GetOrdinal("Gender")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("GolferStatus"))) {
		data.GolferStatus = GolferStatusEnum.UNSET;
	    } else {
		data.GolferStatus = GolferStatusEnum.GetInstance(dataReader.GetString(dataReader.GetOrdinal("GolferStatus")));
	    }

	    return data;
	}

	/// <summary>
	/// Inserts a record into the Golfer table.
	/// </summary>
	/// <param name=""></param>
	public static IdType Insert(GolferData data) {
	    return Insert(data, null);
	}

	/// <summary>
	/// Inserts a record into the Golfer table.
	/// </summary>
	/// <param name=""></param>
	public static IdType Insert(GolferData data, SqlTransaction transaction) {
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spGolfer_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    SqlParameter rv = cmd.Parameters.Add("RETURN_VALUE", SqlDbType.Int);
	    rv.Direction = ParameterDirection.ReturnValue;
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "FirstName", DataRowVersion.Proposed, data.FirstName.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@MiddleInitial", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "MiddleInitial", DataRowVersion.Proposed, data.MiddleInitial.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "LastName", DataRowVersion.Proposed, data.LastName.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Phone", DataRowVersion.Proposed, data.Phone.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Email", DataRowVersion.Proposed, data.Email.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Address1", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Address.Address1", DataRowVersion.Proposed, data.Address.Address1.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Address2", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Address.Address2", DataRowVersion.Proposed, data.Address.Address2.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@City", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Address.City", DataRowVersion.Proposed, data.Address.City.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@State", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "Address.State", DataRowVersion.Proposed, data.Address.State.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Country", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Address.Country", DataRowVersion.Proposed, data.Address.Country.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@PostalCode", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Address.PostalCode", DataRowVersion.Proposed, data.Address.PostalCode.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DateOfBirth", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DateOfBirth", DataRowVersion.Proposed, data.DateOfBirth.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Handicap", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 3, 1, "Handicap", DataRowVersion.Proposed, data.Handicap.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@CourseNumber", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "CourseNumber", DataRowVersion.Proposed, data.CourseNumber.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@PlayerNumber", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "PlayerNumber", DataRowVersion.Proposed, data.PlayerNumber.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Gender", SqlDbType.VarChar, 1, ParameterDirection.Input, false, 0, 0, "Gender", DataRowVersion.Proposed, data.Gender.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@GolferStatus", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "GolferStatus", DataRowVersion.Proposed, data.GolferStatus.DBValue));

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
	/// Updates a record in the Golfer table.
	/// </summary>
	/// <param name=""></param>
	public static void Update(GolferData data) {
	    Update(data, null);
	}

	/// <summary>
	/// Updates a record in the Golfer table.
	/// </summary>
	/// <param name=""></param>
	public static void Update(GolferData data, SqlTransaction transaction) {
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spGolfer_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@GolferId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "GolferId", DataRowVersion.Proposed, data.GolferId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "FirstName", DataRowVersion.Proposed, data.FirstName.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@MiddleInitial", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "MiddleInitial", DataRowVersion.Proposed, data.MiddleInitial.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "LastName", DataRowVersion.Proposed, data.LastName.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Phone", DataRowVersion.Proposed, data.Phone.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Email", DataRowVersion.Proposed, data.Email.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Address1", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Address.Address1", DataRowVersion.Proposed, data.Address.Address1.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Address2", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Address.Address2", DataRowVersion.Proposed, data.Address.Address2.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@City", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Address.City", DataRowVersion.Proposed, data.Address.City.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@State", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "Address.State", DataRowVersion.Proposed, data.Address.State.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Country", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Address.Country", DataRowVersion.Proposed, data.Address.Country.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@PostalCode", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Address.PostalCode", DataRowVersion.Proposed, data.Address.PostalCode.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DateOfBirth", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DateOfBirth", DataRowVersion.Proposed, data.DateOfBirth.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Handicap", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 3, 1, "Handicap", DataRowVersion.Proposed, data.Handicap.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@CourseNumber", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "CourseNumber", DataRowVersion.Proposed, data.CourseNumber.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@PlayerNumber", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "PlayerNumber", DataRowVersion.Proposed, data.PlayerNumber.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Gender", SqlDbType.VarChar, 1, ParameterDirection.Input, false, 0, 0, "Gender", DataRowVersion.Proposed, data.Gender.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@GolferStatus", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "GolferStatus", DataRowVersion.Proposed, data.GolferStatus.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }
	}

	/// <summary>
	/// Deletes a record from the Golfer table by GolferId.
	/// </summary>
	/// <param name=""></param>
	public static void Delete(IdType golferId) {
	    Delete(golferId, null);
	}

	/// <summary>
	/// Deletes a record from the Golfer table by GolferId.
	/// </summary>
	/// <param name=""></param>
	public static void Delete(IdType golferId, SqlTransaction transaction) {
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spGolfer_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    // Create and append the parameters
	    cmd.Parameters.Add(new SqlParameter("@GolferId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "GolferId", DataRowVersion.Proposed, golferId.DBValue));

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
