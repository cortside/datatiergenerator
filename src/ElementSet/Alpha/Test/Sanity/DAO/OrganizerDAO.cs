using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.Types;

using Golf.Tournament.DataObject;


namespace Golf.Tournament.DAO {
    public class OrganizerDAO : Spring2.Core.DAO.EntityDAO {

	private static readonly String VIEW = "vwOrganizer";
	private static readonly String CONNECTION_STRING_KEY = "ConnectionString";
	private static readonly Int32 COMMAND_TIMEOUT = 15;

	/// <summary>
	/// Hash table mapping entity property names to sql code.
	/// </summary>
	private static Hashtable propertyToSqlMap = new Hashtable();

	/// <summary>
	/// Initializes the static map of property names to sql expressions.
	/// </summary>
	static OrganizerDAO() {
	    if (!propertyToSqlMap.Contains("OrganizerId")) {
		propertyToSqlMap.Add("OrganizerId",@"OrganizerId");
	    }
	    if (!propertyToSqlMap.Contains("Name")) {
		propertyToSqlMap.Add("Name",@"Name");
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
	    if (!propertyToSqlMap.Contains("OrganizerContact.Name")) {
		propertyToSqlMap.Add("OrganizerContact.Name",@"OrganizerContactName");
	    }
	    if (!propertyToSqlMap.Contains("OrganizerContact.Phone")) {
		propertyToSqlMap.Add("OrganizerContact.Phone",@"OrganizerContactPhone");
	    }
	    if (!propertyToSqlMap.Contains("OrganizerContact.Email")) {
		propertyToSqlMap.Add("OrganizerContact.Email",@"OrganizerContactEmail");
	    }
	    if (!propertyToSqlMap.Contains("TechnicalContact.Name")) {
		propertyToSqlMap.Add("TechnicalContact.Name",@"TechnicalContactName");
	    }
	    if (!propertyToSqlMap.Contains("TechnicalContact.Phone")) {
		propertyToSqlMap.Add("TechnicalContact.Phone",@"TechnicalContactPhone");
	    }
	    if (!propertyToSqlMap.Contains("TechnicalContact.Email")) {
		propertyToSqlMap.Add("TechnicalContact.Email",@"TechnicalContactEmail");
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
	/// Returns a list of all Organizer rows.
	/// </summary>
	/// <returns>List of OrganizerData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList() {
	    return GetList(null, null);
	}

	/// <summary>
	/// Returns a filtered list of Organizer rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of OrganizerData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public static IList GetList(IWhere whereClause) {
	    return GetList(whereClause, null);
	}

	/// <summary>
	/// Returns an ordered list of Organizer rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of OrganizerData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(IOrderBy orderByClause) {
	    return GetList(null, orderByClause);
	}

	/// <summary>
	/// Returns an ordered and filtered list of Organizer rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of OrganizerData objects.</returns>
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
	/// Returns a list of all Organizer rows.
	/// </summary>
	/// <returns>List of OrganizerData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(Int32 maxRows) {
	    return GetList(null, null, maxRows);
	}

	/// <summary>
	/// Returns a filtered list of Organizer rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of OrganizerData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public static IList GetList(IWhere whereClause, Int32 maxRows) {
	    return GetList(whereClause, null, maxRows);
	}

	/// <summary>
	/// Returns an ordered list of Organizer rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of OrganizerData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(IOrderBy orderByClause, Int32 maxRows) {
	    return GetList(null, orderByClause, maxRows);
	}

	/// <summary>
	/// Returns an ordered and filtered list of Organizer rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of OrganizerData objects.</returns>
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
	/// Finds a Organizer entity using it's primary key.
	/// </summary>
	/// <param name="OrganizerId">A key field.</param>
	/// <returns>A OrganizerData object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	public static OrganizerData Load(IdType organizerId) {
	    WhereClause w = new WhereClause();
	    w.And("OrganizerId", organizerId.DBValue);
	    SqlDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, w, null);

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Load found no rows for Organizer.");
	    }
	    OrganizerData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	private static OrganizerData GetDataObjectFromReader(SqlDataReader dataReader) {
	    OrganizerData data = new OrganizerData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrganizerId"))) {
		data.OrganizerId = IdType.UNSET;
	    } else {
		data.OrganizerId = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("OrganizerId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Name"))) {
		data.Name = StringType.UNSET;
	    } else {
		data.Name = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("Name")));
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
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrganizerContactName"))) {
		data.OrganizerContact.Name = StringType.UNSET;
	    } else {
		data.OrganizerContact.Name = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("OrganizerContactName")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrganizerContactPhone"))) {
		data.OrganizerContact.Phone = StringType.UNSET;
	    } else {
		data.OrganizerContact.Phone = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("OrganizerContactPhone")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrganizerContactEmail"))) {
		data.OrganizerContact.Email = StringType.UNSET;
	    } else {
		data.OrganizerContact.Email = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("OrganizerContactEmail")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("TechnicalContactName"))) {
		data.TechnicalContact.Name = StringType.UNSET;
	    } else {
		data.TechnicalContact.Name = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("TechnicalContactName")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("TechnicalContactPhone"))) {
		data.TechnicalContact.Phone = StringType.UNSET;
	    } else {
		data.TechnicalContact.Phone = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("TechnicalContactPhone")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("TechnicalContactEmail"))) {
		data.TechnicalContact.Email = StringType.UNSET;
	    } else {
		data.TechnicalContact.Email = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("TechnicalContactEmail")));
	    }

	    return data;
	}

	/// <summary>
	/// Inserts a record into the Organizer table.
	/// </summary>
	/// <param name=""></param>
	public static IdType Insert(OrganizerData data) {
	    return Insert(data, null);
	}

	/// <summary>
	/// Inserts a record into the Organizer table.
	/// </summary>
	/// <param name=""></param>
	public static IdType Insert(OrganizerData data, SqlTransaction transaction) {
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spOrganizer_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    SqlParameter rv = cmd.Parameters.Add("RETURN_VALUE", SqlDbType.Int);
	    rv.Direction = ParameterDirection.ReturnValue;
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Proposed, data.Name.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Address1", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Address.Address1", DataRowVersion.Proposed, data.Address.Address1.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Address2", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Address.Address2", DataRowVersion.Proposed, data.Address.Address2.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@City", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Address.City", DataRowVersion.Proposed, data.Address.City.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@State", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "Address.State", DataRowVersion.Proposed, data.Address.State.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Country", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Address.Country", DataRowVersion.Proposed, data.Address.Country.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@PostalCode", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Address.PostalCode", DataRowVersion.Proposed, data.Address.PostalCode.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrganizerContactName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "OrganizerContact.Name", DataRowVersion.Proposed, data.OrganizerContact.Name.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrganizerContactPhone", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "OrganizerContact.Phone", DataRowVersion.Proposed, data.OrganizerContact.Phone.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrganizerContactEmail", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "OrganizerContact.Email", DataRowVersion.Proposed, data.OrganizerContact.Email.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@TechnicalContactName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "TechnicalContact.Name", DataRowVersion.Proposed, data.TechnicalContact.Name.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@TechnicalContactPhone", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "TechnicalContact.Phone", DataRowVersion.Proposed, data.TechnicalContact.Phone.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@TechnicalContactEmail", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "TechnicalContact.Email", DataRowVersion.Proposed, data.TechnicalContact.Email.DBValue));

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
	/// Updates a record in the Organizer table.
	/// </summary>
	/// <param name=""></param>
	public static void Update(OrganizerData data) {
	    Update(data, null);
	}

	/// <summary>
	/// Updates a record in the Organizer table.
	/// </summary>
	/// <param name=""></param>
	public static void Update(OrganizerData data, SqlTransaction transaction) {
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spOrganizer_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@OrganizerId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrganizerId", DataRowVersion.Proposed, data.OrganizerId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Proposed, data.Name.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Address1", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Address.Address1", DataRowVersion.Proposed, data.Address.Address1.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Address2", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Address.Address2", DataRowVersion.Proposed, data.Address.Address2.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@City", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Address.City", DataRowVersion.Proposed, data.Address.City.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@State", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "Address.State", DataRowVersion.Proposed, data.Address.State.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Country", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Address.Country", DataRowVersion.Proposed, data.Address.Country.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@PostalCode", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Address.PostalCode", DataRowVersion.Proposed, data.Address.PostalCode.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrganizerContactName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "OrganizerContact.Name", DataRowVersion.Proposed, data.OrganizerContact.Name.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrganizerContactPhone", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "OrganizerContact.Phone", DataRowVersion.Proposed, data.OrganizerContact.Phone.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrganizerContactEmail", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "OrganizerContact.Email", DataRowVersion.Proposed, data.OrganizerContact.Email.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@TechnicalContactName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "TechnicalContact.Name", DataRowVersion.Proposed, data.TechnicalContact.Name.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@TechnicalContactPhone", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "TechnicalContact.Phone", DataRowVersion.Proposed, data.TechnicalContact.Phone.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@TechnicalContactEmail", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "TechnicalContact.Email", DataRowVersion.Proposed, data.TechnicalContact.Email.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }
	}

	/// <summary>
	/// Deletes a record from the Organizer table by OrganizerId.
	/// </summary>
	/// <param name=""></param>
	public static void Delete(IdType organizerId) {
	    Delete(organizerId, null);
	}

	/// <summary>
	/// Deletes a record from the Organizer table by OrganizerId.
	/// </summary>
	/// <param name=""></param>
	public static void Delete(IdType organizerId, SqlTransaction transaction) {
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spOrganizer_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    // Create and append the parameters
	    cmd.Parameters.Add(new SqlParameter("@OrganizerId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "OrganizerId", DataRowVersion.Proposed, organizerId.DBValue));

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
