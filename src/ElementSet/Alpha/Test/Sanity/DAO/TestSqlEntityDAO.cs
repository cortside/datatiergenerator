using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.Types;

using Golf.Tournament.DataObject;


namespace Golf.Tournament.DAO {
    public class TestSqlEntityDAO : Spring2.Core.DAO.EntityDAO {

	private static readonly String VIEW = "vwTestSqlEntities";
	private static readonly String CONNECTION_STRING_KEY = "ConnectionString";
	private static readonly Int32 COMMAND_TIMEOUT = 15;

	/// <summary>
	/// Hash table mapping entity property names to sql code.
	/// </summary>
	private static Hashtable propertyToSqlMap = new Hashtable();

	/// <summary>
	/// Initializes the static map of property names to sql expressions.
	/// </summary>
	static TestSqlEntityDAO() {
	    if (!propertyToSqlMap.Contains("FloatProperty")) {
		propertyToSqlMap.Add("FloatProperty",@"float");
	    }
	    if (!propertyToSqlMap.Contains("DateTimeProperty")) {
		propertyToSqlMap.Add("DateTimeProperty",@"datetime");
	    }
	    if (!propertyToSqlMap.Contains("BigIntProperty")) {
		propertyToSqlMap.Add("BigIntProperty",@"bigint");
	    }
	    if (!propertyToSqlMap.Contains("BitProperty")) {
		propertyToSqlMap.Add("BitProperty",@"bit");
	    }
	    if (!propertyToSqlMap.Contains("SmallIntProperty")) {
		propertyToSqlMap.Add("SmallIntProperty",@"smallint");
	    }
	    if (!propertyToSqlMap.Contains("TinyIntProperty")) {
		propertyToSqlMap.Add("TinyIntProperty",@"tinyint");
	    }
	    if (!propertyToSqlMap.Contains("SmallMoneyProperty")) {
		propertyToSqlMap.Add("SmallMoneyProperty",@"smallmoney");
	    }
	    if (!propertyToSqlMap.Contains("MoneyProperty")) {
		propertyToSqlMap.Add("MoneyProperty",@"money");
	    }
	    if (!propertyToSqlMap.Contains("TextProperty")) {
		propertyToSqlMap.Add("TextProperty",@"text");
	    }
	    if (!propertyToSqlMap.Contains("SmallDateTimeProperty")) {
		propertyToSqlMap.Add("SmallDateTimeProperty",@"smalldatetime");
	    }
	    if (!propertyToSqlMap.Contains("CharProperty")) {
		propertyToSqlMap.Add("CharProperty",@"char");
	    }
	    if (!propertyToSqlMap.Contains("VarCharProperty")) {
		propertyToSqlMap.Add("VarCharProperty",@"varchar");
	    }
	    if (!propertyToSqlMap.Contains("IntProperty")) {
		propertyToSqlMap.Add("IntProperty",@"int");
	    }
	    if (!propertyToSqlMap.Contains("NumericProperty")) {
		propertyToSqlMap.Add("NumericProperty",@"numeric");
	    }
	    if (!propertyToSqlMap.Contains("DecimalProperty")) {
		propertyToSqlMap.Add("DecimalProperty",@"decimal");
	    }
	    if (!propertyToSqlMap.Contains("NCharProperty")) {
		propertyToSqlMap.Add("NCharProperty",@"nchar");
	    }
	    if (!propertyToSqlMap.Contains("NTextProperty")) {
		propertyToSqlMap.Add("NTextProperty",@"ntext");
	    }
	    if (!propertyToSqlMap.Contains("NVarCharProperty")) {
		propertyToSqlMap.Add("NVarCharProperty",@"nvarchar");
	    }
	    if (!propertyToSqlMap.Contains("RealProperty")) {
		propertyToSqlMap.Add("RealProperty",@"real");
	    }
	    if (!propertyToSqlMap.Contains("UniqueIdentifierProperty")) {
		propertyToSqlMap.Add("UniqueIdentifierProperty",@"uniqueidentifier");
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
	/// Returns a list of all TestSqlEntity rows.
	/// </summary>
	/// <returns>List of TestSqlEntityData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList() {
	    return GetList(null, null);
	}

	/// <summary>
	/// Returns a filtered list of TestSqlEntity rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of TestSqlEntityData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public static IList GetList(IWhere whereClause) {
	    return GetList(whereClause, null);
	}

	/// <summary>
	/// Returns an ordered list of TestSqlEntity rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of TestSqlEntityData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(IOrderBy orderByClause) {
	    return GetList(null, orderByClause);
	}

	/// <summary>
	/// Returns an ordered and filtered list of TestSqlEntity rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of TestSqlEntityData objects.</returns>
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
	/// Returns a list of all TestSqlEntity rows.
	/// </summary>
	/// <returns>List of TestSqlEntityData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(Int32 maxRows) {
	    return GetList(null, null, maxRows);
	}

	/// <summary>
	/// Returns a filtered list of TestSqlEntity rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <returns>List of TestSqlEntityData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public static IList GetList(IWhere whereClause, Int32 maxRows) {
	    return GetList(whereClause, null, maxRows);
	}

	/// <summary>
	/// Returns an ordered list of TestSqlEntity rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of TestSqlEntityData objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public static IList GetList(IOrderBy orderByClause, Int32 maxRows) {
	    return GetList(null, orderByClause, maxRows);
	}

	/// <summary>
	/// Returns an ordered and filtered list of TestSqlEntity rows.
	/// </summary>
	/// <param name="whereClause">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of TestSqlEntityData objects.</returns>
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
	/// Finds a TestSqlEntity entity using it's primary key.
	/// </summary>
	/// <returns>A TestSqlEntityData object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	public static TestSqlEntityData Load() {
	    WhereClause w = new WhereClause();
	    SqlDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, w, null);

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Load found no rows for TestSqlEntity.");
	    }
	    TestSqlEntityData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
	}

	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	private static TestSqlEntityData GetDataObjectFromReader(SqlDataReader dataReader) {
	    TestSqlEntityData data = new TestSqlEntityData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("float"))) {
		data.FloatProperty = DecimalType.UNSET;
	    } else {
		data.FloatProperty = new DecimalType(dataReader.GetDouble(dataReader.GetOrdinal("float")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("datetime"))) {
		data.DateTimeProperty = DateType.UNSET;
	    } else {
		data.DateTimeProperty = new DateType(dataReader.GetDateTime(dataReader.GetOrdinal("datetime")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("bigint"))) {
		data.BigIntProperty = 0;
	    } else {
		data.BigIntProperty = dataReader.GetInt64(dataReader.GetOrdinal("bigint"));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("bit"))) {
		data.BitProperty = BooleanType.UNSET;
	    } else {
		data.BitProperty = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("bit")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("smallint"))) {
		data.SmallIntProperty = IntegerType.UNSET;
	    } else {
		data.SmallIntProperty = new IntegerType(dataReader.GetInt16(dataReader.GetOrdinal("smallint")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("tinyint"))) {
		data.TinyIntProperty = IntegerType.UNSET;
	    } else {
		data.TinyIntProperty = new IntegerType(dataReader.GetByte(dataReader.GetOrdinal("tinyint")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("smallmoney"))) {
		data.SmallMoneyProperty = CurrencyType.UNSET;
	    } else {
		data.SmallMoneyProperty = new CurrencyType(dataReader.GetDecimal(dataReader.GetOrdinal("smallmoney")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("money"))) {
		data.MoneyProperty = CurrencyType.UNSET;
	    } else {
		data.MoneyProperty = new CurrencyType(dataReader.GetDecimal(dataReader.GetOrdinal("money")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("text"))) {
		data.TextProperty = StringType.UNSET;
	    } else {
		data.TextProperty = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("text")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("smalldatetime"))) {
		data.SmallDateTimeProperty = DateType.UNSET;
	    } else {
		data.SmallDateTimeProperty = new DateType(dataReader.GetDateTime(dataReader.GetOrdinal("smalldatetime")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("char"))) {
		data.CharProperty = StringType.UNSET;
	    } else {
		data.CharProperty = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("char")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("varchar"))) {
		data.VarCharProperty = StringType.UNSET;
	    } else {
		data.VarCharProperty = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("varchar")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("int"))) {
		data.IntProperty = IntegerType.UNSET;
	    } else {
		data.IntProperty = new IntegerType(dataReader.GetInt32(dataReader.GetOrdinal("int")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("numeric"))) {
		data.NumericProperty = DecimalType.UNSET;
	    } else {
		data.NumericProperty = new DecimalType(dataReader.GetDecimal(dataReader.GetOrdinal("numeric")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("decimal"))) {
		data.DecimalProperty = DecimalType.UNSET;
	    } else {
		data.DecimalProperty = new DecimalType(dataReader.GetDecimal(dataReader.GetOrdinal("decimal")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("nchar"))) {
		data.NCharProperty = StringType.UNSET;
	    } else {
		data.NCharProperty = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("nchar")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ntext"))) {
		data.NTextProperty = StringType.UNSET;
	    } else {
		data.NTextProperty = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("ntext")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("nvarchar"))) {
		data.NVarCharProperty = StringType.UNSET;
	    } else {
		data.NVarCharProperty = StringType.Parse(dataReader.GetString(dataReader.GetOrdinal("nvarchar")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("real"))) {
		data.RealProperty = DecimalType.UNSET;
	    } else {
		data.RealProperty = new DecimalType(dataReader.GetFloat(dataReader.GetOrdinal("real")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("uniqueidentifier"))) {
		data.UniqueIdentifierProperty = new Guid();
	    } else {
		data.UniqueIdentifierProperty = dataReader.GetGuid(dataReader.GetOrdinal("uniqueidentifier"));
	    }

	    return data;
	}

	/// <summary>
	/// Inserts a record into the TestSqlEntities table.
	/// </summary>
	/// <param name=""></param>
	public static void Insert(TestSqlEntityData data) {
	    Insert(data, null);
	}

	/// <summary>
	/// Inserts a record into the TestSqlEntities table.
	/// </summary>
	/// <param name=""></param>
	public static void Insert(TestSqlEntityData data, SqlTransaction transaction) {
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spTestSqlEntities_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@float", SqlDbType.Float, 10, ParameterDirection.Input, false, 0, 0, "FloatProperty", DataRowVersion.Proposed, data.FloatProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@datetime", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DateTimeProperty", DataRowVersion.Proposed, data.DateTimeProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@bigint", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "BigIntProperty", DataRowVersion.Proposed, data.BigIntProperty));
	    cmd.Parameters.Add(new SqlParameter("@bit", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "BitProperty", DataRowVersion.Proposed, data.BitProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@smallint", SqlDbType.SmallInt, 0, ParameterDirection.Input, false, 0, 0, "SmallIntProperty", DataRowVersion.Proposed, data.SmallIntProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@tinyint", SqlDbType.TinyInt, 0, ParameterDirection.Input, false, 0, 0, "TinyIntProperty", DataRowVersion.Proposed, data.TinyIntProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@smallmoney", SqlDbType.SmallMoney, 0, ParameterDirection.Input, false, 0, 0, "SmallMoneyProperty", DataRowVersion.Proposed, data.SmallMoneyProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@money", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "MoneyProperty", DataRowVersion.Proposed, data.MoneyProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@text", SqlDbType.Text, 0, ParameterDirection.Input, false, 0, 0, "TextProperty", DataRowVersion.Proposed, data.TextProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@smalldatetime", SqlDbType.SmallDateTime, 0, ParameterDirection.Input, false, 0, 0, "SmallDateTimeProperty", DataRowVersion.Proposed, data.SmallDateTimeProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@char", SqlDbType.Char, 10, ParameterDirection.Input, false, 0, 0, "CharProperty", DataRowVersion.Proposed, data.CharProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@varchar", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "VarCharProperty", DataRowVersion.Proposed, data.VarCharProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@int", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "IntProperty", DataRowVersion.Proposed, data.IntProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@numeric", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 10, 3, "NumericProperty", DataRowVersion.Proposed, data.NumericProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@decimal", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 10, 3, "DecimalProperty", DataRowVersion.Proposed, data.DecimalProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@nchar", SqlDbType.NChar, 10, ParameterDirection.Input, false, 0, 0, "NCharProperty", DataRowVersion.Proposed, data.NCharProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ntext", SqlDbType.NText, 0, ParameterDirection.Input, false, 0, 0, "NTextProperty", DataRowVersion.Proposed, data.NTextProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@nvarchar", SqlDbType.NVarChar, 10, ParameterDirection.Input, false, 0, 0, "NVarCharProperty", DataRowVersion.Proposed, data.NVarCharProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@real", SqlDbType.Real, 0, ParameterDirection.Input, false, 0, 0, "RealProperty", DataRowVersion.Proposed, data.RealProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@uniqueidentifier", SqlDbType.UniqueIdentifier, 0, ParameterDirection.Input, false, 0, 0, "UniqueIdentifierProperty", DataRowVersion.Proposed, data.UniqueIdentifierProperty));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }

	}

	/// <summary>
	/// Updates a record in the TestSqlEntities table.
	/// </summary>
	/// <param name=""></param>
	public static void Update(TestSqlEntityData data) {
	    Update(data, null);
	}

	/// <summary>
	/// Updates a record in the TestSqlEntities table.
	/// </summary>
	/// <param name=""></param>
	public static void Update(TestSqlEntityData data, SqlTransaction transaction) {
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spTestSqlEntities_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@float", SqlDbType.Float, 10, ParameterDirection.Input, false, 0, 0, "FloatProperty", DataRowVersion.Proposed, data.FloatProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@datetime", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DateTimeProperty", DataRowVersion.Proposed, data.DateTimeProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@bigint", SqlDbType.BigInt, 0, ParameterDirection.Input, false, 0, 0, "BigIntProperty", DataRowVersion.Proposed, data.BigIntProperty));
	    cmd.Parameters.Add(new SqlParameter("@bit", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "BitProperty", DataRowVersion.Proposed, data.BitProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@smallint", SqlDbType.SmallInt, 0, ParameterDirection.Input, false, 0, 0, "SmallIntProperty", DataRowVersion.Proposed, data.SmallIntProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@tinyint", SqlDbType.TinyInt, 0, ParameterDirection.Input, false, 0, 0, "TinyIntProperty", DataRowVersion.Proposed, data.TinyIntProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@smallmoney", SqlDbType.SmallMoney, 0, ParameterDirection.Input, false, 0, 0, "SmallMoneyProperty", DataRowVersion.Proposed, data.SmallMoneyProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@money", SqlDbType.Money, 0, ParameterDirection.Input, false, 0, 0, "MoneyProperty", DataRowVersion.Proposed, data.MoneyProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@text", SqlDbType.Text, 0, ParameterDirection.Input, false, 0, 0, "TextProperty", DataRowVersion.Proposed, data.TextProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@smalldatetime", SqlDbType.SmallDateTime, 0, ParameterDirection.Input, false, 0, 0, "SmallDateTimeProperty", DataRowVersion.Proposed, data.SmallDateTimeProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@char", SqlDbType.Char, 10, ParameterDirection.Input, false, 0, 0, "CharProperty", DataRowVersion.Proposed, data.CharProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@varchar", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "VarCharProperty", DataRowVersion.Proposed, data.VarCharProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@int", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "IntProperty", DataRowVersion.Proposed, data.IntProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@numeric", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 10, 3, "NumericProperty", DataRowVersion.Proposed, data.NumericProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@decimal", SqlDbType.Decimal, 0, ParameterDirection.Input, false, 10, 3, "DecimalProperty", DataRowVersion.Proposed, data.DecimalProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@nchar", SqlDbType.NChar, 10, ParameterDirection.Input, false, 0, 0, "NCharProperty", DataRowVersion.Proposed, data.NCharProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ntext", SqlDbType.NText, 0, ParameterDirection.Input, false, 0, 0, "NTextProperty", DataRowVersion.Proposed, data.NTextProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@nvarchar", SqlDbType.NVarChar, 10, ParameterDirection.Input, false, 0, 0, "NVarCharProperty", DataRowVersion.Proposed, data.NVarCharProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@real", SqlDbType.Real, 0, ParameterDirection.Input, false, 0, 0, "RealProperty", DataRowVersion.Proposed, data.RealProperty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@uniqueidentifier", SqlDbType.UniqueIdentifier, 0, ParameterDirection.Input, false, 0, 0, "UniqueIdentifierProperty", DataRowVersion.Proposed, data.UniqueIdentifierProperty));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }
	}

	/// <summary>
	/// Deletes a record from the TestSqlEntities table by a composite primary key.
	/// </summary>
	/// <param name=""></param>
	public static void Delete() {
	    Delete(, null);
	}

	/// <summary>
	/// Deletes a record from the TestSqlEntities table by a composite primary key.
	/// </summary>
	/// <param name=""></param>
	public static void Delete(, SqlTransaction transaction) {
	    // Create and execute the command
	    SqlCommand cmd = GetSqlCommand(CONNECTION_STRING_KEY, "spTestSqlEntities_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    // Create and append the parameters
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
