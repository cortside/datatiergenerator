using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.Types;
using Spring2.DataTierGenerator.DataObject;


namespace Spring2.DataTierGenerator.Dao
{
    public class Testsqlentity2DAO : Spring2.Core.DAO.EntityDAO
    {

	private static readonly String VIEW = "vwTestsqlentity2";
	private static readonly String CONNECTION_STRING_KEY = "con1";
	private static readonly Int32 COMMAND_TIMEOUT = 33;

	/// <summary>
	/// Hash table mapping entity property names to sql code.
	/// </summary>
	private static Hashtable propertyToSqlMap = new Hashtable();

	/// <summary>
	/// Initializes the static map of property names to sql expressions.
	/// </summary>
	static Testsqlentity2DAO()
	{
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
	/// Returns a list of all Testsqlentity2 rows.
	/// </summary>
	/// <returns>List of Testsqlentity2Data objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList()
	{
	    return GetList(null, null);
	}

	/// <summary>
	/// Returns a filtered list of Testsqlentity2 rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of Testsqlentity2Data objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public static IList GetList(IWhere whereClause)
	{
	    return GetList(whereClause, null);
	}

	/// <summary>
	/// Returns an ordered list of Testsqlentity2 rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of Testsqlentity2Data objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(IOrderBy orderByClause)
	{
	    return GetList(null, orderByClause);
	}

	/// <summary>
	/// Returns an ordered and filtered list of Testsqlentity2 rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of Testsqlentity2Data objects.</returns>
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
	/// Returns a list of all Testsqlentity2 rows.
	/// </summary>
	/// <returns>List of Testsqlentity2Data objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(Int32 maxRows)
	{
	    return GetList(null, null, maxRows);
	}

	/// <summary>
	/// Returns a filtered list of Testsqlentity2 rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of Testsqlentity2Data objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public static IList GetList(IWhere whereClause, Int32 maxRows)
	{
	    return GetList(whereClause, null, maxRows);
	}

	/// <summary>
	/// Returns an ordered list of Testsqlentity2 rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of Testsqlentity2Data objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(IOrderBy orderByClause, Int32 maxRows)
	{
	    return GetList(null, orderByClause, maxRows);
	}

	/// <summary>
	/// Returns an ordered and filtered list of Testsqlentity2 rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of Testsqlentity2Data objects.</returns>
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
	/// Finds a Testsqlentity2 entity using it's primary key.
	/// </summary>
	/// <returns>A Testsqlentity2Data object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	public static Testsqlentity2Data Load()
	{
	    WhereClause w = new WhereClause();
	    SqlDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, w, null);

	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for Testsqlentity2.");
	    }
	    Testsqlentity2Data data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	private static Testsqlentity2Data GetDataObjectFromReader(SqlDataReader dataReader)
	{
	    Testsqlentity2Data data = new Testsqlentity2Data();

	    return data;
	}

	/// <summary>
	/// Inserts a record into the Testsqlentity2 table.
	/// </summary>
	/// <param name=""></param>
	public static void Insert(Testsqlentity2Data data)
	{
	    Insert(data, null);
	}

	/// <summary>
	/// Inserts a record into the Testsqlentity2 table.
	/// </summary>
	/// <param name=""></param>
	public static void Insert(Testsqlentity2Data data, SqlTransaction transaction)
	{
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spTestsqlentity2_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null)
	    {
		cmd.Connection.Close();
	    }

	}

	/// <summary>
	/// Updates a record in the Testsqlentity2 table.
	/// </summary>
	/// <param name=""></param>
	public static void Update(Testsqlentity2Data data)
	{
	    Update(data, null);
	}

	/// <summary>
	/// Updates a record in the Testsqlentity2 table.
	/// </summary>
	/// <param name=""></param>
	public static void Update(Testsqlentity2Data data, SqlTransaction transaction)
	{
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spTestsqlentity2_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null)
	    {
		cmd.Connection.Close();
	    }
	}

	/// <summary>
	/// Deletes a record from the Testsqlentity2 table by a composite primary key.
	/// </summary>
	/// <param name=""></param>
	public static void Delete()
	{
	    Delete(, null);
	}

	/// <summary>
	/// Deletes a record from the Testsqlentity2 table by a composite primary key.
	/// </summary>
	/// <param name=""></param>
	public static void Delete(, SqlTransaction transaction)
	{
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spTestsqlentity2_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    // Create and append the parameters
	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null)
	    {
		cmd.Connection.Close();
	    }
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
