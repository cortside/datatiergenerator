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
    public class TestNoUpdateDAO : Spring2.Core.DAO.EntityDAO
    {

	private static readonly String VIEW = "vwTestNoUpdate";
	private static readonly String CONNECTION_STRING_KEY = "con1";

	/// <summary>
	/// Hash table mapping entity property names to sql code.
	/// </summary>
	private static Hashtable propertyToSqlMap = new Hashtable();

	/// <summary>
	/// Initializes the static map of property names to sql expressions.
	/// </summary>
	static TestNoUpdateDAO()
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
	/// Returns a list of all TestNoUpdate rows.
	/// </summary>
	/// <returns>List of TestNoUpdateData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList()
	{
	    return GetList(null, null);
	}

	/// <summary>
	/// Returns a filtered list of TestNoUpdate rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of TestNoUpdateData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public static IList GetList(IWhere whereClause)
	{
	    return GetList(whereClause, null);
	}

	/// <summary>
	/// Returns an ordered list of TestNoUpdate rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of TestNoUpdateData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(IOrderBy orderByClause)
	{
	    return GetList(null, orderByClause);
	}

	/// <summary>
	/// Returns an ordered and filtered list of TestNoUpdate rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of TestNoUpdateData objects.</returns>
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
	/// Returns a list of all TestNoUpdate rows.
	/// </summary>
	/// <returns>List of TestNoUpdateData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(Int32 maxRows)
	{
	    return GetList(null, null, maxRows);
	}

	/// <summary>
	/// Returns a filtered list of TestNoUpdate rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of TestNoUpdateData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public static IList GetList(IWhere whereClause, Int32 maxRows)
	{
	    return GetList(whereClause, null, maxRows);
	}

	/// <summary>
	/// Returns an ordered list of TestNoUpdate rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of TestNoUpdateData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(IOrderBy orderByClause, Int32 maxRows)
	{
	    return GetList(null, orderByClause, maxRows);
	}

	/// <summary>
	/// Returns an ordered and filtered list of TestNoUpdate rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of TestNoUpdateData objects.</returns>
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
	/// Finds a TestNoUpdate entity using it's primary key.
	/// </summary>
	/// <returns>A TestNoUpdateData object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	public static TestNoUpdateData Load()
	{
	    WhereClause w = new WhereClause();
	    SqlDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, w, null);

	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for TestNoUpdate.");
	    }
	    TestNoUpdateData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	private static TestNoUpdateData GetDataObjectFromReader(SqlDataReader dataReader)
	{
	    TestNoUpdateData data = new TestNoUpdateData();
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
	/// Returns a list of objects which match the values for the fields specified.
	/// </summary>
	/// <param name="zip">A field value to be matched.</param>
	/// <returns>The list of TestNoUpdateDAO objects found.</returns>
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
	public static TestNoUpdateData FindByPK(StringType stringColumn, IdType int32Column)
	{
	    OrderByClause sort = new OrderByClause("sqlstringcolumn, sqlintcolumn");
	    WhereClause filter = new WhereClause();
	    filter.And("sqlstringcolumn", stringColumn.DBValue);
	    filter.And("sqlintcolumn", int32Column.DBValue);
	    SqlDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, sort);

	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("TestNoUpdateData.FindByPK found no rows.");
	    }
	    TestNoUpdateData data = GetDataObjectFromReader(dataReader);
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
