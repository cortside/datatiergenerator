using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.Types;
using Spring2.DataTierGenerator.DataObject;
using Spring2.DataTierGenerator.Types;


namespace Spring2.DataTierGenerator.Dao
{
    public class TestsqlentityDAO : Spring2.Core.DAO.EntityDAO
    {

	private static readonly String VIEW = "vwTestsqlentity";
	private static readonly String CONNECTION_STRING_KEY = "con1";
	private static readonly Int32 COMMAND_TIMEOUT = 25;

	/// <summary>
	/// Hash table mapping entity property names to sql code.
	/// </summary>
	private static Hashtable propertyToSqlMap = new Hashtable();

	/// <summary>
	/// Initializes the static map of property names to sql expressions.
	/// </summary>
	static TestsqlentityDAO()
	{
	    if (!propertyToSqlMap.Contains("StringColumn"))
	    {
		propertyToSqlMap.Add("StringColumn",@"sqlstringcolumn");
	    }
	    if (!propertyToSqlMap.Contains("Int32Column"))
	    {
		propertyToSqlMap.Add("Int32Column",@"sqlintcolumn");
	    }
	    if (!propertyToSqlMap.Contains("EmailFormat"))
	    {
		propertyToSqlMap.Add("EmailFormat",@"EmailFormat");
	    }
	    if (!propertyToSqlMap.Contains("Address.Address1"))
	    {
		propertyToSqlMap.Add("Address.Address1",@"addr1");
	    }
	    if (!propertyToSqlMap.Contains("Address.Address2"))
	    {
		propertyToSqlMap.Add("Address.Address2",@"addr2");
	    }
	    if (!propertyToSqlMap.Contains("Address.City"))
	    {
		propertyToSqlMap.Add("Address.City",@"city");
	    }
	    if (!propertyToSqlMap.Contains("Address.State"))
	    {
		propertyToSqlMap.Add("Address.State",@"state");
	    }
	    if (!propertyToSqlMap.Contains("Address.PostalCode"))
	    {
		propertyToSqlMap.Add("Address.PostalCode",@"zip");
	    }
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
	/// Returns a list of all Testsqlentity rows.
	/// </summary>
	/// <returns>List of TestsqlentityData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList()
	{
	    return GetList(null, null);
	}

	/// <summary>
	/// Returns a filtered list of Testsqlentity rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of TestsqlentityData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public static IList GetList(IWhere whereClause)
	{
	    return GetList(whereClause, null);
	}

	/// <summary>
	/// Returns an ordered list of Testsqlentity rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of TestsqlentityData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(IOrderBy orderByClause)
	{
	    return GetList(null, orderByClause);
	}

	/// <summary>
	/// Returns an ordered and filtered list of Testsqlentity rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of TestsqlentityData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public static IList GetList(IWhere whereClause, IOrderBy orderByClause)
	{
	    SqlDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, whereClause, orderByClause);

	    IList list = new ArrayList();
	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}

	/// <summary>
	/// Returns a list of all Testsqlentity rows.
	/// </summary>
	/// <returns>List of TestsqlentityData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(Int32 maxRows)
	{
	    return GetList(null, null, maxRows);
	}

	/// <summary>
	/// Returns a filtered list of Testsqlentity rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of TestsqlentityData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public static IList GetList(IWhere whereClause, Int32 maxRows)
	{
	    return GetList(whereClause, null, maxRows);
	}

	/// <summary>
	/// Returns an ordered list of Testsqlentity rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of TestsqlentityData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(IOrderBy orderByClause, Int32 maxRows)
	{
	    return GetList(null, orderByClause, maxRows);
	}

	/// <summary>
	/// Returns an ordered and filtered list of Testsqlentity rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of TestsqlentityData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public static IList GetList(IWhere whereClause, IOrderBy orderByClause, Int32 maxRows)
	{
	    SqlDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, whereClause, orderByClause, maxRows);

	    ArrayList list = new ArrayList();
	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}

	/// <summary>
	/// Finds a Testsqlentity entity using it's primary key.
	/// </summary>
	/// <param name="sqlstringcolumn">A key field.</param>
	/// <param name="sqlintcolumn">A key field.</param>
	/// <returns>A TestsqlentityData object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	public static TestsqlentityData Load(StringType stringColumn, IdType int32Column)
	{
	    WhereClause w = new WhereClause();
	    w.And("sqlstringcolumn", stringColumn.DBValue);
	    w.And("sqlintcolumn", int32Column.DBValue);
	    SqlDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, w, null);

	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for Testsqlentity.");
	    }
	    TestsqlentityData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	private static TestsqlentityData GetDataObjectFromReader(SqlDataReader dataReader)
	{
	    TestsqlentityData data = new TestsqlentityData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("sqlstringcolumn")))
	    {
		data.StringColumn = StringType.UNSET;
	    }
	    else
	    {
		data.StringColumn = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("sqlstringcolumn")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("sqlintcolumn")))
	    {
		data.Int32Column = IdType.UNSET;
	    }
	    else
	    {
		data.Int32Column = new IdType(dataReader.GetInt32(dataReader.GetOrdinal("sqlintcolumn")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("EmailFormat")))
	    {
		data.EmailFormat = FormatType.UNSET;
	    }
	    else
	    {
		data.EmailFormat = FormatType.GetInstance(dataReader.GetString(dataReader.GetOrdinal("EmailFormat")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("addr1")))
	    {
		data.Address.Address1 = StringType.UNSET;
	    }
	    else
	    {
		data.Address.Address1 = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("addr1")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("addr2")))
	    {
		data.Address.Address2 = StringType.UNSET;
	    }
	    else
	    {
		data.Address.Address2 = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("addr2")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("city")))
	    {
		data.Address.City = StringType.UNSET;
	    }
	    else
	    {
		data.Address.City = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("city")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("state")))
	    {
		data.Address.State = StringType.UNSET;
	    }
	    else
	    {
		data.Address.State = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("state")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("zip")))
	    {
		data.Address.PostalCode = StringType.UNSET;
	    }
	    else
	    {
		data.Address.PostalCode = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("zip")));
	    }

	    return data;
	}

	/// <summary>
	/// Inserts a record into the Testsqlentity table.
	/// </summary>
	/// <param name=""></param>
	public static void Insert(TestsqlentityData data)
	{
	    Insert(data, null);
	}

	/// <summary>
	/// Inserts a record into the Testsqlentity table.
	/// </summary>
	/// <param name=""></param>
	public static void Insert(TestsqlentityData data, SqlTransaction transaction)
	{
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spTestsqlentity_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@sqlstringcolumn", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "StringColumn", DataRowVersion.Proposed, data.StringColumn.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@sqlintcolumn", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Int32Column", DataRowVersion.Proposed, data.Int32Column.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@EmailFormat", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "EmailFormat", DataRowVersion.Proposed, data.EmailFormat.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@addr1", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Address.Address1", DataRowVersion.Proposed, data.Address.Address1.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@addr2", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Address.Address2", DataRowVersion.Proposed, data.Address.Address2.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@city", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Address.City", DataRowVersion.Proposed, data.Address.City.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@state", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Address.State", DataRowVersion.Proposed, data.Address.State.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@zip", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Address.PostalCode", DataRowVersion.Proposed, data.Address.PostalCode.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null)
	    {
		cmd.Connection.Close();
	    }

	}

	/// <summary>
	/// Updates a record in the Testsqlentity table.
	/// </summary>
	/// <param name=""></param>
	public static void Update(TestsqlentityData data)
	{
	    Update(data, null);
	}

	/// <summary>
	/// Updates a record in the Testsqlentity table.
	/// </summary>
	/// <param name=""></param>
	public static void Update(TestsqlentityData data, SqlTransaction transaction)
	{
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spTestsqlentity_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@sqlstringcolumn", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "StringColumn", DataRowVersion.Proposed, data.StringColumn.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@sqlintcolumn", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "Int32Column", DataRowVersion.Proposed, data.Int32Column.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@EmailFormat", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "EmailFormat", DataRowVersion.Proposed, data.EmailFormat.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@addr1", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Address.Address1", DataRowVersion.Proposed, data.Address.Address1.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@addr2", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Address.Address2", DataRowVersion.Proposed, data.Address.Address2.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@city", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Address.City", DataRowVersion.Proposed, data.Address.City.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@state", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Address.State", DataRowVersion.Proposed, data.Address.State.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@zip", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Address.PostalCode", DataRowVersion.Proposed, data.Address.PostalCode.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null)
	    {
		cmd.Connection.Close();
	    }
	}

	/// <summary>
	/// Returns a list of objects which match the values for the fields specified.
	/// </summary>
	/// <param name="zip">A field value to be matched.</param>
	/// <returns>The list of TestsqlentityDAO objects found.</returns>
	public static IList FindByPostalCode(StringType address_PostalCode)
	{
	    OrderByClause sort = new OrderByClause("Int32Column, StringColumn");
	    WhereClause filter = new WhereClause();
	    filter.And("zip", address_PostalCode.DBValue);

	    return GetList(filter, sort);
	}

	/// <summary>
	/// Returns an object which matches the values for the fields specified.
	/// </summary>
	/// <param name="sqlstringcolumn">A field value to be matched.</param>
	/// <param name="sqlintcolumn">A field value to be matched.</param>
	/// <returns>The object found.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static TestsqlentityData FindByPK(StringType stringColumn, IdType int32Column)
	{
	    OrderByClause sort = new OrderByClause("sqlstringcolumn, sqlintcolumn");
	    WhereClause filter = new WhereClause();
	    filter.And("sqlstringcolumn", stringColumn.DBValue);
	    filter.And("sqlintcolumn", int32Column.DBValue);
	    SqlDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, sort);

	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("TestsqlentityData.FindByPK found no rows.");
	    }
	    TestsqlentityData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
	}

	/// <summary>
	/// Get a new transaction for a connection that can be created from this classes connection string
	/// </summary>
	public static DaoTransaction GetNewTransaction(String transactionName)
	{
	    SqlConnection conn = GetSqlConnection(GetConnectionString(CONNECTION_STRING_KEY));
	    DaoTransaction transaction = new DaoTransaction(conn.BeginTransaction(transactionName));

	    return transaction;
	}

	/// <summary>
	/// Get a new transaction for a connection that can be created from this classes connection string
	/// </summary>
	public static DaoTransaction GetNewTransaction()
	{
	    SqlConnection conn = GetSqlConnection(GetConnectionString(CONNECTION_STRING_KEY));
	    DaoTransaction transaction = new DaoTransaction(conn.BeginTransaction());

	    return transaction;
	}

    }
}
