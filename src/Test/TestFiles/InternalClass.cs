using System;
using System.Collections;
using System.Configuration;
using System.Data;
using Spring2.Core.DAO;
using Spring2.Core.Types;
using Spring2.Core.Mail.BusinessLogic;
using Spring2.Core.Mail.DataObject;
using Spring2.Core.Mail.Types;

namespace Spring2.Core.Mail.Dao {
    
    
    public class MailMessageDAO : Spring2.Core.DAO.SqlEntityDAO {
        
	public static readonly MailMessageDAO DAO = new MailMessageDAO();
        
	private static readonly String VIEW = "vwMailMessage";
        
	private static readonly String CONNECTION_STRING_KEY = "ConnectionString";
        
	private static readonly Int32 COMMAND_TIMEOUT = 15;
        
	private static ColumnOrdinals columnOrdinals = null;
        
	/// <summary>
	/// Initializes the static map of property names to sql expressions.
	/// </summary>
	static MailMessageDAO() {
	    AddPropertyMapping("MailMessageId", @"MailMessageId");
	    AddPropertyMapping("ScheduleTime", @"ScheduleTime");
	    AddPropertyMapping("ProcessedTime", @"ProcessedTime");
	    AddPropertyMapping("Priority", @"Priority");
	    AddPropertyMapping("From", @"From");
	    AddPropertyMapping("To", @"To");
	    AddPropertyMapping("Cc", @"Cc");
	    AddPropertyMapping("Bcc", @"Bcc");
	    AddPropertyMapping("Subject", @"Subject");
	    AddPropertyMapping("BodyFormat", @"BodyFormat");
	    AddPropertyMapping("Body", @"Body");
	    AddPropertyMapping("MailMessageStatus", @"MailMessageStatus");
	    AddPropertyMapping("ReleasedByUserId", @"ReleasedByUserId");
	    AddPropertyMapping("MailMessageType", @"MailMessageType");
	    AddPropertyMapping("NumberOfAttempts", @"NumberOfAttempts");
	    AddPropertyMapping("MessageQueueDate", @"MessageQueueDate");
	}
        
	private MailMessageDAO() {
            
	}
        
	protected override String ConnectionStringKey {
	    get {
		return CONNECTION_STRING_KEY;
	    }
	}
        
	/// <summary>
	/// Returns a list of all MailMessage rows.
	/// </summary>
	/// <returns>List of MailMessage objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MailMessageList GetList() {
	    return GetList(null, null);
	}
        
	/// <summary>
	/// Returns a filtered list of MailMessage rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <returns>List of MailMessage objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public MailMessageList GetList(SqlFilter filter) {
	    return GetList(filter, null);
	}
        
	/// <summary>
	/// Returns an ordered list of MailMessage rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of MailMessage objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MailMessageList GetList(IOrderBy orderByClause) {
	    return GetList(null, orderByClause);
	}
        
	/// <summary>
	/// Returns an ordered and filtered list of MailMessage rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <returns>List of MailMessage objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public MailMessageList GetList(SqlFilter filter, IOrderBy orderByClause) {
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause);

	    MailMessageList list = new MailMessageList();
	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}
        
	/// <summary>
	/// Returns a list of all MailMessage rows.
	/// </summary>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MailMessage objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MailMessageList GetList(Int32 maxRows) {
	    return GetList(null, null, maxRows);
	}
        
	/// <summary>
	/// Returns a filtered list of MailMessage rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MailMessage objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public MailMessageList GetList(SqlFilter filter, Int32 maxRows) {
	    return GetList(filter, null, maxRows);
	}
        
	/// <summary>
	/// Returns an ordered list of MailMessage rows.  All rows in the database are returned
	/// </summary>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MailMessage objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
	public MailMessageList GetList(IOrderBy orderByClause, Int32 maxRows) {
	    return GetList(null, orderByClause, maxRows);
	}
        
	/// <summary>
	/// Returns an ordered and filtered list of MailMessage rows.
	/// </summary>
	/// <param name="filter">Filtering criteria.</param>
	/// <param name="orderByClause">Ordering criteria.</param>
	/// <param name="maxRows">Uses TOP to limit results to specified number of rows</param>
	/// <returns>List of MailMessage objects.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
	public MailMessageList GetList(SqlFilter filter, IOrderBy orderByClause, Int32 maxRows) {
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, orderByClause, maxRows);

	    MailMessageList list = new MailMessageList();
	    while (dataReader.Read()) {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
	}
        
	/// <summary>
	/// Finds a MailMessage entity using it's primary key.
	/// </summary>
	/// <param name="MailMessageId">A key field.</param>
	/// <returns>A MailMessage object.</returns>
	/// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists witht he specified primary key..</exception>
	public MailMessage Load(IdType mailMessageId) {
	    SqlFilter filter = new SqlFilter();
	    filter.And(new SqlEqualityPredicate("MailMessageId", EqualityOperatorEnum.Equal, mailMessageId.IsValid ? mailMessageId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);	
	    return GetDataObject(dataReader);
	}
        
	/// <summary>
	/// Repopulates an existing business entity instance
	/// </summary>
	public void Reload(MailMessage instance) {
	    SqlFilter filter = new SqlFilter();
	    filter.And(new SqlEqualityPredicate("MailMessageId", EqualityOperatorEnum.Equal, instance.MailMessageId.IsValid ? instance.MailMessageId.ToInt32() as Object : DBNull.Value));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);	

	    if (!dataReader.Read()) {
		dataReader.Close();
		throw new FinderException("Reload found no rows for MailMessage.");
	    }
	    GetDataObjectFromReader(instance, dataReader);
	    dataReader.Close();
	}
        
	/// <summary>
	/// Read through the reader and return a data object list
	/// </summary>
	private MailMessageList GetList(IDataReader reader) {
	    MailMessageList list = new MailMessageList();
	    while (reader.Read()) {
		list.Add(GetDataObjectFromReader(reader));
	    }
	    reader.Close();
	    return list;
	}
        
	/// <summary>
	/// Read from reader and return a single data object
	/// </summary>
	private MailMessage GetDataObject(IDataReader reader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(reader);
	    }
	    return GetDataObject(reader, columnOrdinals);
	}
        
	/// <summary>
	/// Read from reader and return a single data object
	/// </summary>
	private MailMessage GetDataObject(IDataReader reader, ColumnOrdinals ordinals) {
	    if (!reader.Read()) {
		reader.Close();
		throw new FinderException("Reader contained no rows.");
	    }
	    MailMessage data = GetDataObjectFromReader(reader, ordinals);
	    reader.Close();
	    return data;
	}
        
	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static MailMessage GetDataObjectFromReader(MailMessage data, IDataReader dataReader, ColumnOrdinals ordinals) {
	    return GetDataObjectFromReader(data, dataReader, String.Empty, ordinals);
	}
        
	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static MailMessage GetDataObjectFromReader(MailMessage data, IDataReader dataReader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(dataReader);
	    }
	    return GetDataObjectFromReader(data, dataReader, String.Empty, columnOrdinals);
	}
        
	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static MailMessage GetDataObjectFromReader(IDataReader dataReader, ColumnOrdinals ordinals) {
	    MailMessage data = new MailMessage(false);
	    return GetDataObjectFromReader(data, dataReader, String.Empty, ordinals);
	}
        
	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static MailMessage GetDataObjectFromReader(IDataReader dataReader) {
	    if (columnOrdinals == null) {
		columnOrdinals = new ColumnOrdinals(dataReader);
	    }
	    MailMessage data = new MailMessage(false);
	    return GetDataObjectFromReader(data, dataReader, String.Empty, columnOrdinals);
	}
        
	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static MailMessage GetDataObjectFromReader(IDataReader dataReader, String prefix, ColumnOrdinals ordinals) {
	    MailMessage data = new MailMessage(false);
	    return GetDataObjectFromReader(data, dataReader, prefix, columnOrdinals);
	}
        
	/// <summary>
	/// Builds a data object from the current row in a data reader..
	/// </summary>
	/// <param name="dataReader">Container for database row.</param>
	/// <returns>Data object built from current row.</returns>
	internal static MailMessage GetDataObjectFromReader(MailMessage data, IDataReader dataReader, String prefix, ColumnOrdinals ordinals) {
	    if (dataReader.IsDBNull(ordinals.MailMessageId)) {
		data.MailMessageId = IdType.UNSET;
	    } else {
		data.MailMessageId = new IdType(dataReader.GetInt32(ordinals.MailMessageId));
	    }
	    if (dataReader.IsDBNull(ordinals.ScheduleTime)) {
		data.ScheduleTime = DateTimeType.UNSET;
	    } else {
		data.ScheduleTime = new DateTimeType(dataReader.GetDateTime(ordinals.ScheduleTime));
	    }
	    if (dataReader.IsDBNull(ordinals.ProcessedTime)) {
		data.ProcessedTime = DateTimeType.UNSET;
	    } else {
		data.ProcessedTime = new DateTimeType(dataReader.GetDateTime(ordinals.ProcessedTime));
	    }
	    if (dataReader.IsDBNull(ordinals.Priority)) {
		data.Priority = MailPriorityEnum.UNSET;
	    } else {
		data.Priority = MailPriorityEnum.GetInstance(dataReader.GetString(ordinals.Priority));
	    }
	    if (dataReader.IsDBNull(ordinals.From)) {
		data.From = StringType.UNSET;
	    } else {
		data.From = StringType.Parse(dataReader.GetString(ordinals.From));
	    }
	    if (dataReader.IsDBNull(ordinals.To)) {
		data.To = StringType.UNSET;
	    } else {
		data.To = StringType.Parse(dataReader.GetString(ordinals.To));
	    }
	    if (dataReader.IsDBNull(ordinals.Cc)) {
		data.Cc = StringType.UNSET;
	    } else {
		data.Cc = StringType.Parse(dataReader.GetString(ordinals.Cc));
	    }
	    if (dataReader.IsDBNull(ordinals.Bcc)) {
		data.Bcc = StringType.UNSET;
	    } else {
		data.Bcc = StringType.Parse(dataReader.GetString(ordinals.Bcc));
	    }
	    if (dataReader.IsDBNull(ordinals.Subject)) {
		data.Subject = StringType.UNSET;
	    } else {
		data.Subject = StringType.Parse(dataReader.GetString(ordinals.Subject));
	    }
	    if (dataReader.IsDBNull(ordinals.BodyFormat)) {
		data.BodyFormat = MailBodyFormatEnum.UNSET;
	    } else {
		data.BodyFormat = MailBodyFormatEnum.GetInstance(dataReader.GetString(ordinals.BodyFormat));
	    }
	    if (dataReader.IsDBNull(ordinals.Body)) {
		data.Body = StringType.UNSET;
	    } else {
		data.Body = StringType.Parse(dataReader.GetString(ordinals.Body));
	    }
	    if (dataReader.IsDBNull(ordinals.MailMessageStatus)) {
		data.MailMessageStatus = MailMessageStatusEnum.UNSET;
	    } else {
		data.MailMessageStatus = MailMessageStatusEnum.GetInstance(dataReader.GetString(ordinals.MailMessageStatus));
	    }
	    if (dataReader.IsDBNull(ordinals.ReleasedByUserId)) {
		data.ReleasedByUserId = IdType.UNSET;
	    } else {
		data.ReleasedByUserId = new IdType(dataReader.GetInt32(ordinals.ReleasedByUserId));
	    }
	    if (dataReader.IsDBNull(ordinals.MailMessageType)) {
		data.MailMessageType = StringType.UNSET;
	    } else {
		data.MailMessageType = StringType.Parse(dataReader.GetString(ordinals.MailMessageType));
	    }
	    if (dataReader.IsDBNull(ordinals.NumberOfAttempts)) {
		data.NumberOfAttempts = IntegerType.UNSET;
	    } else {
		data.NumberOfAttempts = new IntegerType(dataReader.GetInt32(ordinals.NumberOfAttempts));
	    }
	    if (dataReader.IsDBNull(ordinals.MessageQueueDate)) {
		data.MessageQueueDate = DateTimeType.UNSET;
	    } else {
		data.MessageQueueDate = new DateTimeType(dataReader.GetDateTime(ordinals.MessageQueueDate));
	    }
	    return data;
	}
        
	/// <summary>
	/// Inserts a record into the MailMessage table.
	/// </summary>
	/// <param name="data"></param>
	public IdType Insert(MailMessage data) {
	    return Insert(data, null);
	}
        
	/// <summary>
	/// Inserts a record into the MailMessage table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
	public IdType Insert(MailMessage data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spMailMessage_Insert", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    IDbDataParameter idParam = CreateDataParameter("RETURN_VALUE", DbType.Int32, ParameterDirection.ReturnValue);
	    cmd.Parameters.Add(idParam);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter("@ScheduleTime", DbType.DateTime, ParameterDirection.Input, data.ScheduleTime.IsValid ? data.ScheduleTime.ToDateTime() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@ProcessedTime", DbType.DateTime, ParameterDirection.Input, data.ProcessedTime.IsValid ? data.ProcessedTime.ToDateTime() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Priority", DbType.AnsiString, ParameterDirection.Input, data.Priority.DBValue));
	    cmd.Parameters.Add(CreateDataParameter("@From", DbType.AnsiString, ParameterDirection.Input, data.From.IsValid ? data.From.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@To", DbType.AnsiString, ParameterDirection.Input, data.To.IsValid ? data.To.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Cc", DbType.AnsiString, ParameterDirection.Input, data.Cc.IsValid ? data.Cc.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Bcc", DbType.AnsiString, ParameterDirection.Input, data.Bcc.IsValid ? data.Bcc.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Subject", DbType.AnsiString, ParameterDirection.Input, data.Subject.IsValid ? data.Subject.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@BodyFormat", DbType.AnsiString, ParameterDirection.Input, data.BodyFormat.DBValue));
	    cmd.Parameters.Add(CreateDataParameter("@Body", DbType.AnsiString, ParameterDirection.Input, data.Body.IsValid ? data.Body.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@MailMessageStatus", DbType.AnsiString, ParameterDirection.Input, data.MailMessageStatus.DBValue));
	    cmd.Parameters.Add(CreateDataParameter("@ReleasedByUserId", DbType.Int32, ParameterDirection.Input, data.ReleasedByUserId.IsValid ? data.ReleasedByUserId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@MailMessageType", DbType.AnsiString, ParameterDirection.Input, data.MailMessageType.IsValid ? data.MailMessageType.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@NumberOfAttempts", DbType.Int32, ParameterDirection.Input, data.NumberOfAttempts.IsValid ? data.NumberOfAttempts.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@MessageQueueDate", DbType.DateTime, ParameterDirection.Input, data.MessageQueueDate.IsValid ? data.MessageQueueDate.ToDateTime() as Object : DBNull.Value));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }

	    // Set the output paramter value(s)
	    return new IdType((Int32)idParam.Value);
	}
        
	/// <summary>
	/// Updates a record in the MailMessage table.
	/// </summary>
	/// <param name="data"></param>
	public void Update(MailMessage data) {
	    Update(data, null);
	}
        
	/// <summary>
	/// Updates a record in the MailMessage table.
	/// </summary>
	/// <param name="data"></param>
	/// <param name="transaction"></param>
	public void Update(MailMessage data, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spMailMessage_Update", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(CreateDataParameter("@MailMessageId", DbType.Int32, ParameterDirection.Input, data.MailMessageId.IsValid ? data.MailMessageId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@ScheduleTime", DbType.DateTime, ParameterDirection.Input, data.ScheduleTime.IsValid ? data.ScheduleTime.ToDateTime() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@ProcessedTime", DbType.DateTime, ParameterDirection.Input, data.ProcessedTime.IsValid ? data.ProcessedTime.ToDateTime() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Priority", DbType.AnsiString, ParameterDirection.Input, data.Priority.DBValue));
	    cmd.Parameters.Add(CreateDataParameter("@From", DbType.AnsiString, ParameterDirection.Input, data.From.IsValid ? data.From.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@To", DbType.AnsiString, ParameterDirection.Input, data.To.IsValid ? data.To.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Cc", DbType.AnsiString, ParameterDirection.Input, data.Cc.IsValid ? data.Cc.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Bcc", DbType.AnsiString, ParameterDirection.Input, data.Bcc.IsValid ? data.Bcc.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@Subject", DbType.AnsiString, ParameterDirection.Input, data.Subject.IsValid ? data.Subject.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@BodyFormat", DbType.AnsiString, ParameterDirection.Input, data.BodyFormat.DBValue));
	    cmd.Parameters.Add(CreateDataParameter("@Body", DbType.AnsiString, ParameterDirection.Input, data.Body.IsValid ? data.Body.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@MailMessageStatus", DbType.AnsiString, ParameterDirection.Input, data.MailMessageStatus.DBValue));
	    cmd.Parameters.Add(CreateDataParameter("@ReleasedByUserId", DbType.Int32, ParameterDirection.Input, data.ReleasedByUserId.IsValid ? data.ReleasedByUserId.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@MailMessageType", DbType.AnsiString, ParameterDirection.Input, data.MailMessageType.IsValid ? data.MailMessageType.ToString() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@NumberOfAttempts", DbType.Int32, ParameterDirection.Input, data.NumberOfAttempts.IsValid ? data.NumberOfAttempts.ToInt32() as Object : DBNull.Value));
	    cmd.Parameters.Add(CreateDataParameter("@MessageQueueDate", DbType.DateTime, ParameterDirection.Input, data.MessageQueueDate.IsValid ? data.MessageQueueDate.ToDateTime() as Object : DBNull.Value));

	    // Execute the query
	    cmd.ExecuteNonQuery();

	    // do not close the connection if it is part of a transaction
	    if (transaction == null) {
		cmd.Connection.Close();
	    }
	}
        
	/// <summary>
	/// Deletes a record from the MailMessage table by MailMessageId.
	/// </summary>
	/// <param name="MailMessageId">A key field.</param>
	public void Delete(IdType mailMessageId) {
	    Delete(mailMessageId, null);
	}
        
	/// <summary>
	/// Deletes a record from the MailMessage table by MailMessageId.
	/// </summary>
	/// <param name="MailMessageId">A key field.</param>
	/// <param name="transaction"></param>
	public void Delete(IdType mailMessageId, IDbTransaction transaction) {
	    // Create and execute the command
	    IDbCommand cmd = GetDbCommand(CONNECTION_STRING_KEY, "spMailMessage_Delete", CommandType.StoredProcedure, COMMAND_TIMEOUT, transaction);

	    // Create and append the parameters
	    cmd.Parameters.Add(CreateDataParameter("@MailMessageId", DbType.Int32, ParameterDirection.Input, mailMessageId.IsValid ? mailMessageId.ToInt32() as Object : DBNull.Value));
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
	/// <param name="MailMessageStatus">A field value to be matched.</param>
	/// <returns>The list of MailMessageDAO objects found.</returns>
	public MailMessageList FindByStatus(MailMessageStatusEnum mailMessageStatus) {
	    OrderByClause sort = new OrderByClause("MailMessageStatus");
	    SqlFilter filter = new SqlFilter();
	    filter.And(new SqlEqualityPredicate("MailMessageStatus", EqualityOperatorEnum.Equal, mailMessageStatus.DBValue));
	    IDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, null);	

	    return GetList(dataReader);
	}
        
	public sealed class ColumnOrdinals {
            
	    public Int32 MailMessageId;
            
	    public Int32 ScheduleTime;
            
	    public Int32 ProcessedTime;
            
	    public Int32 Priority;
            
	    public Int32 From;
            
	    public Int32 To;
            
	    public Int32 Cc;
            
	    public Int32 Bcc;
            
	    public Int32 Subject;
            
	    public Int32 BodyFormat;
            
	    public Int32 Body;
            
	    public Int32 MailMessageStatus;
            
	    public Int32 ReleasedByUserId;
            
	    public Int32 MailMessageType;
            
	    public Int32 NumberOfAttempts;
            
	    public Int32 MessageQueueDate;
            
	    internal ColumnOrdinals(IDataReader reader) {
		MailMessageId = reader.GetOrdinal("MailMessageId");
		ScheduleTime = reader.GetOrdinal("ScheduleTime");
		ProcessedTime = reader.GetOrdinal("ProcessedTime");
		Priority = reader.GetOrdinal("Priority");
		From = reader.GetOrdinal("From");
		To = reader.GetOrdinal("To");
		Cc = reader.GetOrdinal("Cc");
		Bcc = reader.GetOrdinal("Bcc");
		Subject = reader.GetOrdinal("Subject");
		BodyFormat = reader.GetOrdinal("BodyFormat");
		Body = reader.GetOrdinal("Body");
		MailMessageStatus = reader.GetOrdinal("MailMessageStatus");
		ReleasedByUserId = reader.GetOrdinal("ReleasedByUserId");
		MailMessageType = reader.GetOrdinal("MailMessageType");
		NumberOfAttempts = reader.GetOrdinal("NumberOfAttempts");
		MessageQueueDate = reader.GetOrdinal("MessageQueueDate");
	    }
            
	    internal ColumnOrdinals(IDataReader reader, String prefix) {
		MailMessageId = reader.GetOrdinal(prefix + "MailMessageId");
		ScheduleTime = reader.GetOrdinal(prefix + "ScheduleTime");
		ProcessedTime = reader.GetOrdinal(prefix + "ProcessedTime");
		Priority = reader.GetOrdinal(prefix + "Priority");
		From = reader.GetOrdinal(prefix + "From");
		To = reader.GetOrdinal(prefix + "To");
		Cc = reader.GetOrdinal(prefix + "Cc");
		Bcc = reader.GetOrdinal(prefix + "Bcc");
		Subject = reader.GetOrdinal(prefix + "Subject");
		BodyFormat = reader.GetOrdinal(prefix + "BodyFormat");
		Body = reader.GetOrdinal(prefix + "Body");
		MailMessageStatus = reader.GetOrdinal(prefix + "MailMessageStatus");
		ReleasedByUserId = reader.GetOrdinal(prefix + "ReleasedByUserId");
		MailMessageType = reader.GetOrdinal(prefix + "MailMessageType");
		NumberOfAttempts = reader.GetOrdinal(prefix + "NumberOfAttempts");
		MessageQueueDate = reader.GetOrdinal(prefix + "MessageQueueDate");
	    }
	}
    }
}
