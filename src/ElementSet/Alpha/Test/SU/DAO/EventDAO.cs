using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Spring2.Core.DAO;
using Spring2.Core.Types;
using StampinUp.DataObject;
using StampinUp.Types;
using Spring2.DataTierGenerator.Attribute;
using StampinUp.Core.DAO;
using StampinUp.Core.Types;
using StampinUp.Core.Util;

namespace StampinUp.Dao
{
    
    
    public class EventDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[Event]";
        
        [Generate()]
        private static readonly Int32 COMMAND_TIMEOUT = 30;
        
        /// <summary>
        /// Hash table mapping entity property names to sql code.
        /// </summary>
        [Generate()]
        private static Hashtable propertyToSqlMap = new Hashtable();
        
        /// <summary>
        /// Initializes the static map of property names to sql expressions.
        /// </summary>
        static EventDAO()
        {
            propertyToSqlMap.Add("EventId",@"Event_Id");
	    propertyToSqlMap.Add("Description",@"Description");
	    propertyToSqlMap.Add("InternalAccess",@"Internal");
	    propertyToSqlMap.Add("ExternalAccess",@"External");
	    propertyToSqlMap.Add("CloseDate",@"CloseDate");
	    propertyToSqlMap.Add("StartDate",@"StartDate");
	    propertyToSqlMap.Add("DemoItemId",@"DemoItemId");
	    propertyToSqlMap.Add("GuestItemId",@"GuestItemId");
	    propertyToSqlMap.Add("MaxRegistrations",@"MaxRegistrations");
	    propertyToSqlMap.Add("Warning",@"Warning");
	    propertyToSqlMap.Add("MinDemoLevel",@"MinDemoLevel");
	    propertyToSqlMap.Add("SpouseOnly",@"SpouseOnly");
	    propertyToSqlMap.Add("MaxGuests",@"MaxGuests");
	    propertyToSqlMap.Add("AskTerms",@"AskTerms");
	    propertyToSqlMap.Add("AskColor",@"AskColor");
	    propertyToSqlMap.Add("DemoFirstNameOnly",@"DemoFirstNameOnly");
	    propertyToSqlMap.Add("AskDemoMobility",@"AskDemoMobility");
	    propertyToSqlMap.Add("AskDemoHearing",@"AskDemoHearing");
	    propertyToSqlMap.Add("JointDemoOnly",@"JointDemoOnly");
	    propertyToSqlMap.Add("ShowFinalSplash",@"ShowFinalSplash");
	    propertyToSqlMap.Add("NoGuestMeal",@"NoGuestMeal");
	    propertyToSqlMap.Add("DependentEvent",@"DependentEvent");
	    propertyToSqlMap.Add("AllowCheckPayment",@"AllowCheckPayment");
	    propertyToSqlMap.Add("NoGuestPeriod",@"NoGuestPeriod");
	    propertyToSqlMap.Add("CountryCode",@"CountryCode");
	    propertyToSqlMap.Add("EventType",@"EventType");
	    propertyToSqlMap.Add("EventDate",@"EventDate");
	    propertyToSqlMap.Add("GLPostedDate",@"GLPostedDate");
	    propertyToSqlMap.Add("CancelFeeItemId",@"CancelFeeItemId");
	    propertyToSqlMap.Add("GuestCancelFeeItemId",@"GuestCancelFeeItemId");
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
        /// Returns a list of all Event rows.
        /// </summary>
        /// <returns>List of EventData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static EventList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of Event rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of EventData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static EventList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of Event rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of EventData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static EventList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of Event rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of EventData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static EventList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.ORDERDB, TABLE, whereClause, orderByClause, true);
	    EventList list = new EventList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a Event entity using it's primary key.
        /// </summary>
        /// <param name="Event_Id">A key field.</param>
        /// <returns>A EventData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static EventData Load(IdType eventId)
        {
            WhereClause w = new WhereClause();
	    w.And("Event_Id", eventId.DBValue);
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.ORDERDB, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for Event.");
	    }
	    EventData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static EventData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            EventData data = new EventData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Event_Id")))
	    {
		data.EventId = IdType.UNSET;
	    }
	    else
	    {
		data.EventId = new IdType (dataReader.GetInt32(dataReader.GetOrdinal("Event_Id")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Description")))
	    {
		data.Description = StringType.UNSET;
	    }
	    else
	    {
		data.Description = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Description")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Internal")))
	    {
		data.InternalAccess = IntegerType.UNSET;
	    }
	    else
	    {
		data.InternalAccess = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("Internal")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("External")))
	    {
		data.ExternalAccess = IntegerType.UNSET;
	    }
	    else
	    {
		data.ExternalAccess = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("External")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("CloseDate")))
	    {
		data.CloseDate = DateType.UNSET;
	    }
	    else
	    {
		data.CloseDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("CloseDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("StartDate")))
	    {
		data.StartDate = DateType.UNSET;
	    }
	    else
	    {
		data.StartDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("StartDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("DemoItemId")))
	    {
		data.DemoItemId = StringType.UNSET;
	    }
	    else
	    {
		data.DemoItemId = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("DemoItemId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("GuestItemId")))
	    {
		data.GuestItemId = StringType.UNSET;
	    }
	    else
	    {
		data.GuestItemId = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("GuestItemId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("MaxRegistrations")))
	    {
		data.MaxRegistrations = IntegerType.UNSET;
	    }
	    else
	    {
		data.MaxRegistrations = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("MaxRegistrations")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Warning")))
	    {
		data.Warning = StringType.UNSET;
	    }
	    else
	    {
		data.Warning = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Warning")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("MinDemoLevel")))
	    {
		data.MinDemoLevel = IntegerType.UNSET;
	    }
	    else
	    {
		data.MinDemoLevel = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("MinDemoLevel")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("SpouseOnly")))
	    {
		data.SpouseOnly = IntegerType.UNSET;
	    }
	    else
	    {
		data.SpouseOnly = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("SpouseOnly")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("MaxGuests")))
	    {
		data.MaxGuests = IntegerType.UNSET;
	    }
	    else
	    {
		data.MaxGuests = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("MaxGuests")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("AskTerms")))
	    {
		data.AskTerms = StringType.UNSET;
	    }
	    else
	    {
		data.AskTerms = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("AskTerms")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("AskColor")))
	    {
		data.AskColor = StringType.UNSET;
	    }
	    else
	    {
		data.AskColor = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("AskColor")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("DemoFirstNameOnly")))
	    {
		data.DemoFirstNameOnly = StringType.UNSET;
	    }
	    else
	    {
		data.DemoFirstNameOnly = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("DemoFirstNameOnly")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("AskDemoMobility")))
	    {
		data.AskDemoMobility = StringType.UNSET;
	    }
	    else
	    {
		data.AskDemoMobility = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("AskDemoMobility")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("AskDemoHearing")))
	    {
		data.AskDemoHearing = StringType.UNSET;
	    }
	    else
	    {
		data.AskDemoHearing = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("AskDemoHearing")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("JointDemoOnly")))
	    {
		data.JointDemoOnly = StringType.UNSET;
	    }
	    else
	    {
		data.JointDemoOnly = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("JointDemoOnly")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ShowFinalSplash")))
	    {
		data.ShowFinalSplash = StringType.UNSET;
	    }
	    else
	    {
		data.ShowFinalSplash = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("ShowFinalSplash")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("NoGuestMeal")))
	    {
		data.NoGuestMeal = StringType.UNSET;
	    }
	    else
	    {
		data.NoGuestMeal = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("NoGuestMeal")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("DependentEvent")))
	    {
		data.DependentEvent = IdType.UNSET;
	    }
	    else
	    {
		data.DependentEvent = new IdType (dataReader.GetInt32(dataReader.GetOrdinal("DependentEvent")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("AllowCheckPayment")))
	    {
		data.AllowCheckPayment = StringType.UNSET;
	    }
	    else
	    {
		data.AllowCheckPayment = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("AllowCheckPayment")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("NoGuestPeriod")))
	    {
		data.NoGuestPeriod = IntegerType.UNSET;
	    }
	    else
	    {
		data.NoGuestPeriod = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("NoGuestPeriod")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("CountryCode")))
	    {
		data.CountryCode = CountryEnum.UNSET;
	    }
	    else
	    {
		data.CountryCode = CountryEnum.GetInstance(dataReader.GetString(dataReader.GetOrdinal("CountryCode")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("EventType")))
	    {
		data.EventType = StringType.UNSET;
	    }
	    else
	    {
		data.EventType = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("EventType")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("EventDate")))
	    {
		data.EventDate = DateType.UNSET;
	    }
	    else
	    {
		data.EventDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("EventDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("GLPostedDate")))
	    {
		data.GLPostedDate = DateType.UNSET;
	    }
	    else
	    {
		data.GLPostedDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("GLPostedDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("CancelFeeItemId")))
	    {
		data.CancelFeeItemId = StringType.UNSET;
	    }
	    else
	    {
		data.CancelFeeItemId = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("CancelFeeItemId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("GuestCancelFeeItemId")))
	    {
		data.GuestCancelFeeItemId = StringType.UNSET;
	    }
	    else
	    {
		data.GuestCancelFeeItemId = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("GuestCancelFeeItemId")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the Event table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Insert(EventData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "Event_Id,"
	    + "Description,"
	    + "Internal,"
	    + "External,"
	    + "CloseDate,"
	    + "StartDate,"
	    + "DemoItemId,"
	    + "GuestItemId,"
	    + "MaxRegistrations,"
	    + "Warning,"
	    + "MinDemoLevel,"
	    + "SpouseOnly,"
	    + "MaxGuests,"
	    + "AskTerms,"
	    + "AskColor,"
	    + "DemoFirstNameOnly,"
	    + "AskDemoMobility,"
	    + "AskDemoHearing,"
	    + "JointDemoOnly,"
	    + "ShowFinalSplash,"
	    + "NoGuestMeal,"
	    + "DependentEvent,"
	    + "AllowCheckPayment,"
	    + "NoGuestPeriod,"
	    + "CountryCode,"
	    + "EventType,"
	    + "EventDate,"
	    + "GLPostedDate,"
	    + "CancelFeeItemId,"
	    + "GuestCancelFeeItemId,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@Event_Id,"
	    + "@Description,"
	    + "@Internal,"
	    + "@External,"
	    + "@CloseDate,"
	    + "@StartDate,"
	    + "@DemoItemId,"
	    + "@GuestItemId,"
	    + "@MaxRegistrations,"
	    + "@Warning,"
	    + "@MinDemoLevel,"
	    + "@SpouseOnly,"
	    + "@MaxGuests,"
	    + "@AskTerms,"
	    + "@AskColor,"
	    + "@DemoFirstNameOnly,"
	    + "@AskDemoMobility,"
	    + "@AskDemoHearing,"
	    + "@JointDemoOnly,"
	    + "@ShowFinalSplash,"
	    + "@NoGuestMeal,"
	    + "@DependentEvent,"
	    + "@AllowCheckPayment,"
	    + "@NoGuestPeriod,"
	    + "@CountryCode,"
	    + "@EventType,"
	    + "@EventDate,"
	    + "@GLPostedDate,"
	    + "@CancelFeeItemId,"
	    + "@GuestCancelFeeItemId,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ")";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.ORDERDB, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@Event_Id", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "EventId", DataRowVersion.Proposed, data.EventId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.Char, 80, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Internal", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "InternalAccess", DataRowVersion.Proposed, data.InternalAccess.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@External", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ExternalAccess", DataRowVersion.Proposed, data.ExternalAccess.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@CloseDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "CloseDate", DataRowVersion.Proposed, data.CloseDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "StartDate", DataRowVersion.Proposed, data.StartDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DemoItemId", SqlDbType.Char, 20, ParameterDirection.Input, false, 0, 0, "DemoItemId", DataRowVersion.Proposed, data.DemoItemId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@GuestItemId", SqlDbType.Char, 20, ParameterDirection.Input, false, 0, 0, "GuestItemId", DataRowVersion.Proposed, data.GuestItemId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@MaxRegistrations", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "MaxRegistrations", DataRowVersion.Proposed, data.MaxRegistrations.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Warning", SqlDbType.VarChar, 5000, ParameterDirection.Input, false, 0, 0, "Warning", DataRowVersion.Proposed, data.Warning.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@MinDemoLevel", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "MinDemoLevel", DataRowVersion.Proposed, data.MinDemoLevel.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@SpouseOnly", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "SpouseOnly", DataRowVersion.Proposed, data.SpouseOnly.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@MaxGuests", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "MaxGuests", DataRowVersion.Proposed, data.MaxGuests.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@AskTerms", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "AskTerms", DataRowVersion.Proposed, data.AskTerms.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@AskColor", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "AskColor", DataRowVersion.Proposed, data.AskColor.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DemoFirstNameOnly", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "DemoFirstNameOnly", DataRowVersion.Proposed, data.DemoFirstNameOnly.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@AskDemoMobility", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "AskDemoMobility", DataRowVersion.Proposed, data.AskDemoMobility.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@AskDemoHearing", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "AskDemoHearing", DataRowVersion.Proposed, data.AskDemoHearing.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@JointDemoOnly", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "JointDemoOnly", DataRowVersion.Proposed, data.JointDemoOnly.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@ShowFinalSplash", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "ShowFinalSplash", DataRowVersion.Proposed, data.ShowFinalSplash.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@NoGuestMeal", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "NoGuestMeal", DataRowVersion.Proposed, data.NoGuestMeal.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DependentEvent", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "DependentEvent", DataRowVersion.Proposed, data.DependentEvent.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@AllowCheckPayment", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "AllowCheckPayment", DataRowVersion.Proposed, data.AllowCheckPayment.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@NoGuestPeriod", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "NoGuestPeriod", DataRowVersion.Proposed, data.NoGuestPeriod.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@CountryCode", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "CountryCode", DataRowVersion.Proposed, data.CountryCode.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@EventType", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "EventType", DataRowVersion.Proposed, data.EventType.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@EventDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "EventDate", DataRowVersion.Proposed, data.EventDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@GLPostedDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "GLPostedDate", DataRowVersion.Proposed, data.GLPostedDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@CancelFeeItemId", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "CancelFeeItemId", DataRowVersion.Proposed, data.CancelFeeItemId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@GuestCancelFeeItemId", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "GuestCancelFeeItemId", DataRowVersion.Proposed, data.GuestCancelFeeItemId.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();
        }
        
        /// <summary>
        /// Updates a record in the Event table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(EventData data)
        {
            // Create and execute the command
	    EventData oldData = Load ( data.EventId);
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.EventId.Equals(data.EventId))
	    {
		sql = sql + "Event_Id=@Event_Id,";
	    }
	    if (!oldData.Description.Equals(data.Description))
	    {
		sql = sql + "Description=@Description,";
	    }
	    if (!oldData.InternalAccess.Equals(data.InternalAccess))
	    {
		sql = sql + "Internal=@Internal,";
	    }
	    if (!oldData.ExternalAccess.Equals(data.ExternalAccess))
	    {
		sql = sql + "External=@External,";
	    }
	    if (!oldData.CloseDate.Equals(data.CloseDate))
	    {
		sql = sql + "CloseDate=@CloseDate,";
	    }
	    if (!oldData.StartDate.Equals(data.StartDate))
	    {
		sql = sql + "StartDate=@StartDate,";
	    }
	    if (!oldData.DemoItemId.Equals(data.DemoItemId))
	    {
		sql = sql + "DemoItemId=@DemoItemId,";
	    }
	    if (!oldData.GuestItemId.Equals(data.GuestItemId))
	    {
		sql = sql + "GuestItemId=@GuestItemId,";
	    }
	    if (!oldData.MaxRegistrations.Equals(data.MaxRegistrations))
	    {
		sql = sql + "MaxRegistrations=@MaxRegistrations,";
	    }
	    if (!oldData.Warning.Equals(data.Warning))
	    {
		sql = sql + "Warning=@Warning,";
	    }
	    if (!oldData.MinDemoLevel.Equals(data.MinDemoLevel))
	    {
		sql = sql + "MinDemoLevel=@MinDemoLevel,";
	    }
	    if (!oldData.SpouseOnly.Equals(data.SpouseOnly))
	    {
		sql = sql + "SpouseOnly=@SpouseOnly,";
	    }
	    if (!oldData.MaxGuests.Equals(data.MaxGuests))
	    {
		sql = sql + "MaxGuests=@MaxGuests,";
	    }
	    if (!oldData.AskTerms.Equals(data.AskTerms))
	    {
		sql = sql + "AskTerms=@AskTerms,";
	    }
	    if (!oldData.AskColor.Equals(data.AskColor))
	    {
		sql = sql + "AskColor=@AskColor,";
	    }
	    if (!oldData.DemoFirstNameOnly.Equals(data.DemoFirstNameOnly))
	    {
		sql = sql + "DemoFirstNameOnly=@DemoFirstNameOnly,";
	    }
	    if (!oldData.AskDemoMobility.Equals(data.AskDemoMobility))
	    {
		sql = sql + "AskDemoMobility=@AskDemoMobility,";
	    }
	    if (!oldData.AskDemoHearing.Equals(data.AskDemoHearing))
	    {
		sql = sql + "AskDemoHearing=@AskDemoHearing,";
	    }
	    if (!oldData.JointDemoOnly.Equals(data.JointDemoOnly))
	    {
		sql = sql + "JointDemoOnly=@JointDemoOnly,";
	    }
	    if (!oldData.ShowFinalSplash.Equals(data.ShowFinalSplash))
	    {
		sql = sql + "ShowFinalSplash=@ShowFinalSplash,";
	    }
	    if (!oldData.NoGuestMeal.Equals(data.NoGuestMeal))
	    {
		sql = sql + "NoGuestMeal=@NoGuestMeal,";
	    }
	    if (!oldData.DependentEvent.Equals(data.DependentEvent))
	    {
		sql = sql + "DependentEvent=@DependentEvent,";
	    }
	    if (!oldData.AllowCheckPayment.Equals(data.AllowCheckPayment))
	    {
		sql = sql + "AllowCheckPayment=@AllowCheckPayment,";
	    }
	    if (!oldData.NoGuestPeriod.Equals(data.NoGuestPeriod))
	    {
		sql = sql + "NoGuestPeriod=@NoGuestPeriod,";
	    }
	    if (!oldData.CountryCode.Equals(data.CountryCode))
	    {
		sql = sql + "CountryCode=@CountryCode,";
	    }
	    if (!oldData.EventType.Equals(data.EventType))
	    {
		sql = sql + "EventType=@EventType,";
	    }
	    if (!oldData.EventDate.Equals(data.EventDate))
	    {
		sql = sql + "EventDate=@EventDate,";
	    }
	    if (!oldData.GLPostedDate.Equals(data.GLPostedDate))
	    {
		sql = sql + "GLPostedDate=@GLPostedDate,";
	    }
	    if (!oldData.CancelFeeItemId.Equals(data.CancelFeeItemId))
	    {
		sql = sql + "CancelFeeItemId=@CancelFeeItemId,";
	    }
	    if (!oldData.GuestCancelFeeItemId.Equals(data.GuestCancelFeeItemId))
	    {
		sql = sql + "GuestCancelFeeItemId=@GuestCancelFeeItemId,";
	    }
	    WhereClause w = new WhereClause();
	    w.And("Event_Id", data.EventId.DBValue);
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.ORDERDB, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.EventId.Equals(data.EventId))
	    {
		cmd.Parameters.Add(new SqlParameter("@Event_Id", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "EventId", DataRowVersion.Proposed, data.EventId.DBValue));

	    }
	    if (!oldData.Description.Equals(data.Description))
	    {
		cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.Char, 80, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Proposed, data.Description.DBValue));

	    }
	    if (!oldData.InternalAccess.Equals(data.InternalAccess))
	    {
		cmd.Parameters.Add(new SqlParameter("@Internal", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "InternalAccess", DataRowVersion.Proposed, data.InternalAccess.DBValue));

	    }
	    if (!oldData.ExternalAccess.Equals(data.ExternalAccess))
	    {
		cmd.Parameters.Add(new SqlParameter("@External", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "ExternalAccess", DataRowVersion.Proposed, data.ExternalAccess.DBValue));

	    }
	    if (!oldData.CloseDate.Equals(data.CloseDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@CloseDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "CloseDate", DataRowVersion.Proposed, data.CloseDate.DBValue));

	    }
	    if (!oldData.StartDate.Equals(data.StartDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "StartDate", DataRowVersion.Proposed, data.StartDate.DBValue));

	    }
	    if (!oldData.DemoItemId.Equals(data.DemoItemId))
	    {
		cmd.Parameters.Add(new SqlParameter("@DemoItemId", SqlDbType.Char, 20, ParameterDirection.Input, false, 0, 0, "DemoItemId", DataRowVersion.Proposed, data.DemoItemId.DBValue));

	    }
	    if (!oldData.GuestItemId.Equals(data.GuestItemId))
	    {
		cmd.Parameters.Add(new SqlParameter("@GuestItemId", SqlDbType.Char, 20, ParameterDirection.Input, false, 0, 0, "GuestItemId", DataRowVersion.Proposed, data.GuestItemId.DBValue));

	    }
	    if (!oldData.MaxRegistrations.Equals(data.MaxRegistrations))
	    {
		cmd.Parameters.Add(new SqlParameter("@MaxRegistrations", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "MaxRegistrations", DataRowVersion.Proposed, data.MaxRegistrations.DBValue));

	    }
	    if (!oldData.Warning.Equals(data.Warning))
	    {
		cmd.Parameters.Add(new SqlParameter("@Warning", SqlDbType.VarChar, 5000, ParameterDirection.Input, false, 0, 0, "Warning", DataRowVersion.Proposed, data.Warning.DBValue));

	    }
	    if (!oldData.MinDemoLevel.Equals(data.MinDemoLevel))
	    {
		cmd.Parameters.Add(new SqlParameter("@MinDemoLevel", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "MinDemoLevel", DataRowVersion.Proposed, data.MinDemoLevel.DBValue));

	    }
	    if (!oldData.SpouseOnly.Equals(data.SpouseOnly))
	    {
		cmd.Parameters.Add(new SqlParameter("@SpouseOnly", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "SpouseOnly", DataRowVersion.Proposed, data.SpouseOnly.DBValue));

	    }
	    if (!oldData.MaxGuests.Equals(data.MaxGuests))
	    {
		cmd.Parameters.Add(new SqlParameter("@MaxGuests", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "MaxGuests", DataRowVersion.Proposed, data.MaxGuests.DBValue));

	    }
	    if (!oldData.AskTerms.Equals(data.AskTerms))
	    {
		cmd.Parameters.Add(new SqlParameter("@AskTerms", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "AskTerms", DataRowVersion.Proposed, data.AskTerms.DBValue));

	    }
	    if (!oldData.AskColor.Equals(data.AskColor))
	    {
		cmd.Parameters.Add(new SqlParameter("@AskColor", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "AskColor", DataRowVersion.Proposed, data.AskColor.DBValue));

	    }
	    if (!oldData.DemoFirstNameOnly.Equals(data.DemoFirstNameOnly))
	    {
		cmd.Parameters.Add(new SqlParameter("@DemoFirstNameOnly", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "DemoFirstNameOnly", DataRowVersion.Proposed, data.DemoFirstNameOnly.DBValue));

	    }
	    if (!oldData.AskDemoMobility.Equals(data.AskDemoMobility))
	    {
		cmd.Parameters.Add(new SqlParameter("@AskDemoMobility", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "AskDemoMobility", DataRowVersion.Proposed, data.AskDemoMobility.DBValue));

	    }
	    if (!oldData.AskDemoHearing.Equals(data.AskDemoHearing))
	    {
		cmd.Parameters.Add(new SqlParameter("@AskDemoHearing", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "AskDemoHearing", DataRowVersion.Proposed, data.AskDemoHearing.DBValue));

	    }
	    if (!oldData.JointDemoOnly.Equals(data.JointDemoOnly))
	    {
		cmd.Parameters.Add(new SqlParameter("@JointDemoOnly", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "JointDemoOnly", DataRowVersion.Proposed, data.JointDemoOnly.DBValue));

	    }
	    if (!oldData.ShowFinalSplash.Equals(data.ShowFinalSplash))
	    {
		cmd.Parameters.Add(new SqlParameter("@ShowFinalSplash", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "ShowFinalSplash", DataRowVersion.Proposed, data.ShowFinalSplash.DBValue));

	    }
	    if (!oldData.NoGuestMeal.Equals(data.NoGuestMeal))
	    {
		cmd.Parameters.Add(new SqlParameter("@NoGuestMeal", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "NoGuestMeal", DataRowVersion.Proposed, data.NoGuestMeal.DBValue));

	    }
	    if (!oldData.DependentEvent.Equals(data.DependentEvent))
	    {
		cmd.Parameters.Add(new SqlParameter("@DependentEvent", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "DependentEvent", DataRowVersion.Proposed, data.DependentEvent.DBValue));

	    }
	    if (!oldData.AllowCheckPayment.Equals(data.AllowCheckPayment))
	    {
		cmd.Parameters.Add(new SqlParameter("@AllowCheckPayment", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "AllowCheckPayment", DataRowVersion.Proposed, data.AllowCheckPayment.DBValue));

	    }
	    if (!oldData.NoGuestPeriod.Equals(data.NoGuestPeriod))
	    {
		cmd.Parameters.Add(new SqlParameter("@NoGuestPeriod", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "NoGuestPeriod", DataRowVersion.Proposed, data.NoGuestPeriod.DBValue));

	    }
	    if (!oldData.CountryCode.Equals(data.CountryCode))
	    {
		cmd.Parameters.Add(new SqlParameter("@CountryCode", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "CountryCode", DataRowVersion.Proposed, data.CountryCode.DBValue));

	    }
	    if (!oldData.EventType.Equals(data.EventType))
	    {
		cmd.Parameters.Add(new SqlParameter("@EventType", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "EventType", DataRowVersion.Proposed, data.EventType.DBValue));

	    }
	    if (!oldData.EventDate.Equals(data.EventDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@EventDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "EventDate", DataRowVersion.Proposed, data.EventDate.DBValue));

	    }
	    if (!oldData.GLPostedDate.Equals(data.GLPostedDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@GLPostedDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "GLPostedDate", DataRowVersion.Proposed, data.GLPostedDate.DBValue));

	    }
	    if (!oldData.CancelFeeItemId.Equals(data.CancelFeeItemId))
	    {
		cmd.Parameters.Add(new SqlParameter("@CancelFeeItemId", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "CancelFeeItemId", DataRowVersion.Proposed, data.CancelFeeItemId.DBValue));

	    }
	    if (!oldData.GuestCancelFeeItemId.Equals(data.GuestCancelFeeItemId))
	    {
		cmd.Parameters.Add(new SqlParameter("@GuestCancelFeeItemId", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "GuestCancelFeeItemId", DataRowVersion.Proposed, data.GuestCancelFeeItemId.DBValue));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the Event table by a composite primary key.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Delete(IdType eventId)
        {
            // Create and execute the command
	    string sql = "Delete From " + TABLE;
	    WhereClause w = new WhereClause();
	    w.And("Event_Id", eventId.DBValue);
	    sql += w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.ORDERDB, sql, CommandType.Text, COMMAND_TIMEOUT);

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();
        }
    }
}