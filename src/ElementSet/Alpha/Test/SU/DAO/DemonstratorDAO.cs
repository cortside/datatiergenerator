using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Spring2.Core.DAO;
using Spring2.Core.Types;
using StampinUp.Core.Types;
using StampinUp.DataObject;
using StampinUp.Types;
using Spring2.DataTierGenerator.Attribute;
using StampinUp.Core.DAO;
using StampinUp.Core.Util;

namespace StampinUp.Dao
{
    
    
    public class DemonstratorDAO : StampinUp.Core.DAO.DAOBase
    {
        
        [Generate()]
        private static readonly String TABLE = "[DemoMast]";
        
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
        static DemonstratorDAO()
        {
            propertyToSqlMap.Add("DemoId",@"Demo_Id");
	    propertyToSqlMap.Add("SponsorId",@"Sponsor_Id");
	    propertyToSqlMap.Add("BusEntityName",@"BusEntityName");
	    propertyToSqlMap.Add("Tin",@"TIN");
	    propertyToSqlMap.Add("BusEntityTypeID",@"BusEntityTypeID");
	    propertyToSqlMap.Add("CountryOfOrigin",@"CountryOfOrigin");
	    propertyToSqlMap.Add("StateOfOrigin",@"StateOfOrigin");
	    propertyToSqlMap.Add("BusEntityEffectDate",@"BusEntityEffectDate");
	    propertyToSqlMap.Add("AppToOpDate",@"AppToOpDate");
	    propertyToSqlMap.Add("NoticeOfIntentDate",@"NoticeOfIntentDate");
	    propertyToSqlMap.Add("DuePerformDate",@"DuePerformDate");
	    propertyToSqlMap.Add("AuthorizationDate",@"AuthorizationDate");
	    propertyToSqlMap.Add("FormationDate",@"FormationDate");
	    propertyToSqlMap.Add("ShipToAddress1",@"Ship_To_Address1");
	    propertyToSqlMap.Add("ShipToAddress2",@"Ship_To_Address2");
	    propertyToSqlMap.Add("ShipToCity",@"Ship_To_City");
	    propertyToSqlMap.Add("ShipToState",@"Ship_To_State");
	    propertyToSqlMap.Add("ShipToCounty",@"Ship_To_County");
	    propertyToSqlMap.Add("ShipToZip",@"Ship_To_Zip");
	    propertyToSqlMap.Add("ShipToZip4",@"Ship_To_Zip4");
	    propertyToSqlMap.Add("DemoLvl",@"Demo_Lvl");
	    propertyToSqlMap.Add("Status",@"Status");
	    propertyToSqlMap.Add("StatusDate",@"Status_Date");
	    propertyToSqlMap.Add("LastEmpName",@"Last_Emp_Name");
	    propertyToSqlMap.Add("LastChangeDate",@"Last_Change_Date");
	    propertyToSqlMap.Add("Fax",@"Fax");
	    propertyToSqlMap.Add("StarterKitOrdered",@"Starter_Kit_Ordered");
	    propertyToSqlMap.Add("StarterKit",@"Starter_Kit");
	    propertyToSqlMap.Add("Startdate",@"Startdate");
	    propertyToSqlMap.Add("PromoFlag",@"Promo_Flag");
	    propertyToSqlMap.Add("IsOrderingDisabled",@"Nsf_Warning");
	    propertyToSqlMap.Add("Password",@"Password1");
	    propertyToSqlMap.Add("Geocode",@"Geocode");
	    propertyToSqlMap.Add("Inout",@"Inout");
	    propertyToSqlMap.Add("Localcode1",@"Localcode1");
	    propertyToSqlMap.Add("Localcode2",@"Localcode2");
	    propertyToSqlMap.Add("Localcode3",@"Localcode3");
	    propertyToSqlMap.Add("Localcode4",@"Localcode4");
	    propertyToSqlMap.Add("Localcode5",@"Localcode5");
	    propertyToSqlMap.Add("SponsorUp2",@"Sponsor_Up2");
	    propertyToSqlMap.Add("SponsorUp3",@"Sponsor_Up3");
	    propertyToSqlMap.Add("Email",@"Email");
	    propertyToSqlMap.Add("SponsorUp4",@"Sponsor_Up4");
	    propertyToSqlMap.Add("SponsorUp5",@"Sponsor_Up5");
	    propertyToSqlMap.Add("OrigStartDate",@"OrigStartDate");
	    propertyToSqlMap.Add("OrigDemoId",@"OrigDemoId");
	    propertyToSqlMap.Add("Referral",@"Referral");
	    propertyToSqlMap.Add("LastReferralDate",@"LastReferralDate");
	    propertyToSqlMap.Add("Latitude",@"Latitude");
	    propertyToSqlMap.Add("Longitude",@"Longitude");
	    propertyToSqlMap.Add("AccountVerified",@"AccountVerified");
	    propertyToSqlMap.Add("AllElectronic",@"AllElectronic");
	    propertyToSqlMap.Add("AccountVerifiedDate",@"AccountVerifiedDate");
	    propertyToSqlMap.Add("APOStatusDate",@"APOStatusDate");
	    propertyToSqlMap.Add("IsPasswordDisabled",@"Pass_Lock");
	    propertyToSqlMap.Add("IsPasswordTemporary",@"GeneratedPassword");
	    propertyToSqlMap.Add("APOFlag",@"APOFlag");
	    propertyToSqlMap.Add("IsSmallSupplier",@"SmallSupplierFlag");
	    propertyToSqlMap.Add("ReceivedFreeShip",@"ReceivedFreeShip");
	    propertyToSqlMap.Add("WantPrintedReports",@"WantPrintedReports");
	    propertyToSqlMap.Add("QuestionId",@"QuestionId");
	    propertyToSqlMap.Add("Answer",@"Answer");
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
        /// Returns a list of all Demonstrator rows.
        /// </summary>
        /// <returns>List of DemonstratorData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static DemonstratorList GetList()
        {
            return GetList(null, null);
        }
        
        /// <summary>
        /// Returns a filtered list of Demonstrator rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <returns>List of DemonstratorData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static DemonstratorList GetList(IWhere whereClause)
        {
            return GetList(whereClause, null);
        }
        
        /// <summary>
        /// Returns an ordered list of Demonstrator rows.  All rows in the database are returned
        /// </summary>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of DemonstratorData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found.</exception>
        [Generate()]
        public static DemonstratorList GetList(IOrderBy orderByClause)
        {
            return GetList(null, orderByClause);
        }
        
        /// <summary>
        /// Returns an ordered and filtered list of Demonstrator rows.
        /// </summary>
        /// <param name="whereClause">Filtering criteria.</param>
        /// <param name="orderByClause">Ordering criteria.</param>
        /// <returns>List of DemonstratorData objects.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no rows are found matching the where criteria.</exception>
        [Generate()]
        public static DemonstratorList GetList(IWhere whereClause, IOrderBy orderByClause)
        {
            SqlDataReader dataReader = GetListReader(DatabaseEnum.ORDERDB, TABLE, whereClause, orderByClause, true);
	    DemonstratorList list = new DemonstratorList();

	    while (dataReader.Read())
	    {
		list.Add(GetDataObjectFromReader(dataReader));
	    }
	    dataReader.Close();
	    return list;
        }
        
        /// <summary>
        /// Finds a Demonstrator entity using it's primary key.
        /// </summary>
        /// <param name="Demo_Id">A key field.</param>
        /// <returns>A DemonstratorData object.</returns>
        /// <exception cref="Spring2.Core.DAO.FinderException">Thrown when no entity exists with the specified primary key..</exception>
        [Generate()]
        public static DemonstratorData Load(DemoIdType demoId)
        {
            WhereClause w = new WhereClause();
	    w.And("Demo_Id", demoId.DBValue);
	    SqlDataReader dataReader = GetListReader(DatabaseEnum.ORDERDB, TABLE, w, null, true);
	    if (!dataReader.Read())
	    {
		dataReader.Close();
		throw new FinderException("Load found no rows for Demonstrator.");
	    }
	    DemonstratorData data = GetDataObjectFromReader(dataReader);
	    dataReader.Close();
	    return data;
        }
        
        /// <summary>
        /// Builds a data object from the current row in a data reader..
        /// </summary>
        /// <param name="dataReader">Container for database row.</param>
        /// <returns>Data object built from current row.</returns>
        [Generate()]
        private static DemonstratorData GetDataObjectFromReader(SqlDataReader dataReader)
        {
            DemonstratorData data = new DemonstratorData();
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Demo_Id")))
	    {
		data.DemoId = DemoIdType.UNSET;
	    }
	    else
	    {
		data.DemoId = DemoIdType.Parse (dataReader.GetString(dataReader.GetOrdinal("Demo_Id")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Sponsor_Id")))
	    {
		data.SponsorId = DemoIdType.UNSET;
	    }
	    else
	    {
		data.SponsorId = DemoIdType.Parse (dataReader.GetString(dataReader.GetOrdinal("Sponsor_Id")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("BusEntityName")))
	    {
		data.BusEntityName = StringType.UNSET;
	    }
	    else
	    {
		data.BusEntityName = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("BusEntityName")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("TIN")))
	    {
		data.Tin = StringType.UNSET;
	    }
	    else
	    {
		data.Tin = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("TIN")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("BusEntityTypeID")))
	    {
		data.BusEntityTypeID = IdType.UNSET;
	    }
	    else
	    {
		data.BusEntityTypeID = new IdType (dataReader.GetInt32(dataReader.GetOrdinal("BusEntityTypeID")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("CountryOfOrigin")))
	    {
		data.CountryOfOrigin = CountryEnum.UNSET;
	    }
	    else
	    {
		data.CountryOfOrigin = CountryEnum.GetInstance(dataReader.GetString(dataReader.GetOrdinal("CountryOfOrigin")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("StateOfOrigin")))
	    {
		data.StateOfOrigin = StringType.UNSET;
	    }
	    else
	    {
		data.StateOfOrigin = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("StateOfOrigin")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("BusEntityEffectDate")))
	    {
		data.BusEntityEffectDate = DateType.UNSET;
	    }
	    else
	    {
		data.BusEntityEffectDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("BusEntityEffectDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("AppToOpDate")))
	    {
		data.AppToOpDate = DateType.UNSET;
	    }
	    else
	    {
		data.AppToOpDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("AppToOpDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("NoticeOfIntentDate")))
	    {
		data.NoticeOfIntentDate = DateType.UNSET;
	    }
	    else
	    {
		data.NoticeOfIntentDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("NoticeOfIntentDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("DuePerformDate")))
	    {
		data.DuePerformDate = DateType.UNSET;
	    }
	    else
	    {
		data.DuePerformDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("DuePerformDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("AuthorizationDate")))
	    {
		data.AuthorizationDate = DateType.UNSET;
	    }
	    else
	    {
		data.AuthorizationDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("AuthorizationDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("FormationDate")))
	    {
		data.FormationDate = DateType.UNSET;
	    }
	    else
	    {
		data.FormationDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("FormationDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Ship_To_Address1")))
	    {
		data.ShipToAddress1 = StringType.UNSET;
	    }
	    else
	    {
		data.ShipToAddress1 = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Ship_To_Address1")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Ship_To_Address2")))
	    {
		data.ShipToAddress2 = StringType.UNSET;
	    }
	    else
	    {
		data.ShipToAddress2 = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Ship_To_Address2")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Ship_To_City")))
	    {
		data.ShipToCity = StringType.UNSET;
	    }
	    else
	    {
		data.ShipToCity = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Ship_To_City")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Ship_To_State")))
	    {
		data.ShipToState = StringType.UNSET;
	    }
	    else
	    {
		data.ShipToState = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Ship_To_State")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Ship_To_County")))
	    {
		data.ShipToCounty = StringType.UNSET;
	    }
	    else
	    {
		data.ShipToCounty = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Ship_To_County")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Ship_To_Zip")))
	    {
		data.ShipToZip = StringType.UNSET;
	    }
	    else
	    {
		data.ShipToZip = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Ship_To_Zip")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Ship_To_Zip4")))
	    {
		data.ShipToZip4 = StringType.UNSET;
	    }
	    else
	    {
		data.ShipToZip4 = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Ship_To_Zip4")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Demo_Lvl")))
	    {
		data.DemoLvl = StringType.UNSET;
	    }
	    else
	    {
		data.DemoLvl = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Demo_Lvl")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Status")))
	    {
		data.Status = StringType.UNSET;
	    }
	    else
	    {
		data.Status = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Status")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Status_Date")))
	    {
		data.StatusDate = DateType.UNSET;
	    }
	    else
	    {
		data.StatusDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("Status_Date")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Last_Emp_Name")))
	    {
		data.LastEmpName = StringType.UNSET;
	    }
	    else
	    {
		data.LastEmpName = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Last_Emp_Name")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Last_Change_Date")))
	    {
		data.LastChangeDate = DateType.UNSET;
	    }
	    else
	    {
		data.LastChangeDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("Last_Change_Date")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Fax")))
	    {
		data.Fax = StringType.UNSET;
	    }
	    else
	    {
		data.Fax = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Fax")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Starter_Kit_Ordered")))
	    {
		data.StarterKitOrdered = BooleanType.UNSET;
	    }
	    else
	    {
		data.StarterKitOrdered = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("Starter_Kit_Ordered")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Starter_Kit")))
	    {
		data.StarterKit = IntegerType.UNSET;
	    }
	    else
	    {
		data.StarterKit = new IntegerType (dataReader.GetInt32(dataReader.GetOrdinal("Starter_Kit")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Startdate")))
	    {
		data.Startdate = DateType.UNSET;
	    }
	    else
	    {
		data.Startdate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("Startdate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Promo_Flag")))
	    {
		data.PromoFlag = StringType.UNSET;
	    }
	    else
	    {
		data.PromoFlag = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Promo_Flag")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Nsf_Warning")))
	    {
		data.IsOrderingDisabled = BooleanType.UNSET;
	    }
	    else
	    {
		data.IsOrderingDisabled = BooleanType.GetInstance(dataReader.GetString(dataReader.GetOrdinal("Nsf_Warning")).Equals("N") ? "Y" : "N");
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Password1")))
	    {
		data.Password = StringType.UNSET;
	    }
	    else
	    {
		data.Password = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Password1")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Geocode")))
	    {
		data.Geocode = StringType.UNSET;
	    }
	    else
	    {
		data.Geocode = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Geocode")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Inout")))
	    {
		data.Inout = StringType.UNSET;
	    }
	    else
	    {
		data.Inout = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Inout")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Localcode1")))
	    {
		data.Localcode1 = StringType.UNSET;
	    }
	    else
	    {
		data.Localcode1 = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Localcode1")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Localcode2")))
	    {
		data.Localcode2 = StringType.UNSET;
	    }
	    else
	    {
		data.Localcode2 = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Localcode2")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Localcode3")))
	    {
		data.Localcode3 = StringType.UNSET;
	    }
	    else
	    {
		data.Localcode3 = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Localcode3")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Localcode4")))
	    {
		data.Localcode4 = StringType.UNSET;
	    }
	    else
	    {
		data.Localcode4 = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Localcode4")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Localcode5")))
	    {
		data.Localcode5 = StringType.UNSET;
	    }
	    else
	    {
		data.Localcode5 = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Localcode5")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Sponsor_Up2")))
	    {
		data.SponsorUp2 = DemoIdType.UNSET;
	    }
	    else
	    {
		data.SponsorUp2 = DemoIdType.Parse (dataReader.GetString(dataReader.GetOrdinal("Sponsor_Up2")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Sponsor_Up3")))
	    {
		data.SponsorUp3 = DemoIdType.UNSET;
	    }
	    else
	    {
		data.SponsorUp3 = DemoIdType.Parse (dataReader.GetString(dataReader.GetOrdinal("Sponsor_Up3")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Email")))
	    {
		data.Email = StringType.UNSET;
	    }
	    else
	    {
		data.Email = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Email")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Sponsor_Up4")))
	    {
		data.SponsorUp4 = DemoIdType.UNSET;
	    }
	    else
	    {
		data.SponsorUp4 = DemoIdType.Parse (dataReader.GetString(dataReader.GetOrdinal("Sponsor_Up4")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Sponsor_Up5")))
	    {
		data.SponsorUp5 = DemoIdType.UNSET;
	    }
	    else
	    {
		data.SponsorUp5 = DemoIdType.Parse (dataReader.GetString(dataReader.GetOrdinal("Sponsor_Up5")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrigStartDate")))
	    {
		data.OrigStartDate = DateType.UNSET;
	    }
	    else
	    {
		data.OrigStartDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("OrigStartDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("OrigDemoId")))
	    {
		data.OrigDemoId = DemoIdType.UNSET;
	    }
	    else
	    {
		data.OrigDemoId = DemoIdType.Parse (dataReader.GetString(dataReader.GetOrdinal("OrigDemoId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Referral")))
	    {
		data.Referral = BooleanType.UNSET;
	    }
	    else
	    {
		data.Referral = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("Referral")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("LastReferralDate")))
	    {
		data.LastReferralDate = DateType.UNSET;
	    }
	    else
	    {
		data.LastReferralDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("LastReferralDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Latitude")))
	    {
		data.Latitude = DecimalType.UNSET;
	    }
	    else
	    {
		data.Latitude = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("Latitude")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Longitude")))
	    {
		data.Longitude = DecimalType.UNSET;
	    }
	    else
	    {
		data.Longitude = new DecimalType (dataReader.GetDouble(dataReader.GetOrdinal("Longitude")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("AccountVerified")))
	    {
		data.AccountVerified = StringType.UNSET;
	    }
	    else
	    {
		data.AccountVerified = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("AccountVerified")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("AllElectronic")))
	    {
		data.AllElectronic = StringType.UNSET;
	    }
	    else
	    {
		data.AllElectronic = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("AllElectronic")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("AccountVerifiedDate")))
	    {
		data.AccountVerifiedDate = DateType.UNSET;
	    }
	    else
	    {
		data.AccountVerifiedDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("AccountVerifiedDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("APOStatusDate")))
	    {
		data.APOStatusDate = DateType.UNSET;
	    }
	    else
	    {
		data.APOStatusDate = new DateType (dataReader.GetDateTime(dataReader.GetOrdinal("APOStatusDate")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Pass_Lock")))
	    {
		data.IsPasswordDisabled = BooleanType.UNSET;
	    }
	    else
	    {
		data.IsPasswordDisabled = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("Pass_Lock")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("GeneratedPassword")))
	    {
		data.IsPasswordTemporary = BooleanType.UNSET;
	    }
	    else
	    {
		data.IsPasswordTemporary = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("GeneratedPassword")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("APOFlag")))
	    {
		data.APOFlag = BooleanType.UNSET;
	    }
	    else
	    {
		data.APOFlag = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("APOFlag")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("SmallSupplierFlag")))
	    {
		data.IsSmallSupplier = BooleanType.UNSET;
	    }
	    else
	    {
		data.IsSmallSupplier = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("SmallSupplierFlag")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("ReceivedFreeShip")))
	    {
		data.ReceivedFreeShip = BooleanType.UNSET;
	    }
	    else
	    {
		data.ReceivedFreeShip = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("ReceivedFreeShip")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("WantPrintedReports")))
	    {
		data.WantPrintedReports = BooleanType.UNSET;
	    }
	    else
	    {
		data.WantPrintedReports = BooleanType.GetInstance(dataReader.GetBoolean(dataReader.GetOrdinal("WantPrintedReports")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("QuestionId")))
	    {
		data.QuestionId = PasswordQuestionEnum.UNSET;
	    }
	    else
	    {
		data.QuestionId = PasswordQuestionEnum.GetInstance(dataReader.GetInt32(dataReader.GetOrdinal("QuestionId")));
	    }
	    if (dataReader.IsDBNull(dataReader.GetOrdinal("Answer")))
	    {
		data.Answer = StringType.UNSET;
	    }
	    else
	    {
		data.Answer = StringType.Parse (dataReader.GetString(dataReader.GetOrdinal("Answer")));
	    }

	    return data;
        }
        
        /// <summary>
        /// Inserts a record into the DemoMast table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static DemoIdType Insert(DemonstratorData data)
        {
            // Create and execute the command
	    string sql = "Insert Into " + TABLE + "("
	    + "Demo_Id,"
	    + "Sponsor_Id,"
	    + "BusEntityName,"
	    + "TIN,"
	    + "BusEntityTypeID,"
	    + "CountryOfOrigin,"
	    + "StateOfOrigin,"
	    + "BusEntityEffectDate,"
	    + "AppToOpDate,"
	    + "NoticeOfIntentDate,"
	    + "DuePerformDate,"
	    + "AuthorizationDate,"
	    + "FormationDate,"
	    + "Ship_To_Address1,"
	    + "Ship_To_Address2,"
	    + "Ship_To_City,"
	    + "Ship_To_State,"
	    + "Ship_To_County,"
	    + "Ship_To_Zip,"
	    + "Ship_To_Zip4,"
	    + "Demo_Lvl,"
	    + "Status,"
	    + "Status_Date,"
	    + "Last_Emp_Name,"
	    + "Last_Change_Date,"
	    + "Fax,"
	    + "Starter_Kit_Ordered,"
	    + "Starter_Kit,"
	    + "Startdate,"
	    + "Promo_Flag,"
	    + "Nsf_Warning,"
	    + "Password1,"
	    + "Geocode,"
	    + "Inout,"
	    + "Localcode1,"
	    + "Localcode2,"
	    + "Localcode3,"
	    + "Localcode4,"
	    + "Localcode5,"
	    + "Sponsor_Up2,"
	    + "Sponsor_Up3,"
	    + "Email,"
	    + "Sponsor_Up4,"
	    + "Sponsor_Up5,"
	    + "OrigStartDate,"
	    + "OrigDemoId,"
	    + "Referral,"
	    + "LastReferralDate,"
	    + "Latitude,"
	    + "Longitude,"
	    + "AccountVerified,"
	    + "AllElectronic,"
	    + "AccountVerifiedDate,"
	    + "APOStatusDate,"
	    + "Pass_Lock,"
	    + "GeneratedPassword,"
	    + "APOFlag,"
	    + "SmallSupplierFlag,"
	    + "ReceivedFreeShip,"
	    + "WantPrintedReports,"
	    + "QuestionId,"
	    + "Answer,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ") values("
	    + "@Demo_Id,"
	    + "@Sponsor_Id,"
	    + "@BusEntityName,"
	    + "@TIN,"
	    + "@BusEntityTypeID,"
	    + "@CountryOfOrigin,"
	    + "@StateOfOrigin,"
	    + "@BusEntityEffectDate,"
	    + "@AppToOpDate,"
	    + "@NoticeOfIntentDate,"
	    + "@DuePerformDate,"
	    + "@AuthorizationDate,"
	    + "@FormationDate,"
	    + "@Ship_To_Address1,"
	    + "@Ship_To_Address2,"
	    + "@Ship_To_City,"
	    + "@Ship_To_State,"
	    + "@Ship_To_County,"
	    + "@Ship_To_Zip,"
	    + "@Ship_To_Zip4,"
	    + "@Demo_Lvl,"
	    + "@Status,"
	    + "@Status_Date,"
	    + "@Last_Emp_Name,"
	    + "@Last_Change_Date,"
	    + "@Fax,"
	    + "@Starter_Kit_Ordered,"
	    + "@Starter_Kit,"
	    + "@Startdate,"
	    + "@Promo_Flag,"
	    + "@Nsf_Warning,"
	    + "@Password1,"
	    + "@Geocode,"
	    + "@Inout,"
	    + "@Localcode1,"
	    + "@Localcode2,"
	    + "@Localcode3,"
	    + "@Localcode4,"
	    + "@Localcode5,"
	    + "@Sponsor_Up2,"
	    + "@Sponsor_Up3,"
	    + "@Email,"
	    + "@Sponsor_Up4,"
	    + "@Sponsor_Up5,"
	    + "@OrigStartDate,"
	    + "@OrigDemoId,"
	    + "@Referral,"
	    + "@LastReferralDate,"
	    + "@Latitude,"
	    + "@Longitude,"
	    + "@AccountVerified,"
	    + "@AllElectronic,"
	    + "@AccountVerifiedDate,"
	    + "@APOStatusDate,"
	    + "@Pass_Lock,"
	    + "@GeneratedPassword,"
	    + "@APOFlag,"
	    + "@SmallSupplierFlag,"
	    + "@ReceivedFreeShip,"
	    + "@WantPrintedReports,"
	    + "@QuestionId,"
	    + "@Answer,"
	    ;
	    sql = sql.Substring(0, sql.Length - 1) + ")";
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.ORDERDB, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    cmd.Parameters.Add(new SqlParameter("@Demo_Id", SqlDbType.VarChar, 7, ParameterDirection.Input, false, 0, 0, "DemoId", DataRowVersion.Proposed, data.DemoId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Sponsor_Id", SqlDbType.VarChar, 7, ParameterDirection.Input, false, 0, 0, "SponsorId", DataRowVersion.Proposed, data.SponsorId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@BusEntityName", SqlDbType.VarChar, 150, ParameterDirection.Input, false, 0, 0, "BusEntityName", DataRowVersion.Proposed, data.BusEntityName.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@TIN", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "Tin", DataRowVersion.Proposed, data.Tin.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@BusEntityTypeID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "BusEntityTypeID", DataRowVersion.Proposed, data.BusEntityTypeID.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@CountryOfOrigin", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "CountryOfOrigin", DataRowVersion.Proposed, data.CountryOfOrigin.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@StateOfOrigin", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "StateOfOrigin", DataRowVersion.Proposed, data.StateOfOrigin.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@BusEntityEffectDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "BusEntityEffectDate", DataRowVersion.Proposed, data.BusEntityEffectDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@AppToOpDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "AppToOpDate", DataRowVersion.Proposed, data.AppToOpDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@NoticeOfIntentDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "NoticeOfIntentDate", DataRowVersion.Proposed, data.NoticeOfIntentDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@DuePerformDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DuePerformDate", DataRowVersion.Proposed, data.DuePerformDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@AuthorizationDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "AuthorizationDate", DataRowVersion.Proposed, data.AuthorizationDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@FormationDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "FormationDate", DataRowVersion.Proposed, data.FormationDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Ship_To_Address1", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "ShipToAddress1", DataRowVersion.Proposed, data.ShipToAddress1.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Ship_To_Address2", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "ShipToAddress2", DataRowVersion.Proposed, data.ShipToAddress2.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Ship_To_City", SqlDbType.VarChar, 25, ParameterDirection.Input, false, 0, 0, "ShipToCity", DataRowVersion.Proposed, data.ShipToCity.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Ship_To_State", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "ShipToState", DataRowVersion.Proposed, data.ShipToState.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Ship_To_County", SqlDbType.VarChar, 25, ParameterDirection.Input, false, 0, 0, "ShipToCounty", DataRowVersion.Proposed, data.ShipToCounty.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Ship_To_Zip", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "ShipToZip", DataRowVersion.Proposed, data.ShipToZip.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Ship_To_Zip4", SqlDbType.VarChar, 4, ParameterDirection.Input, false, 0, 0, "ShipToZip4", DataRowVersion.Proposed, data.ShipToZip4.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Demo_Lvl", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "DemoLvl", DataRowVersion.Proposed, data.DemoLvl.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "Status", DataRowVersion.Proposed, data.Status.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Status_Date", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "StatusDate", DataRowVersion.Proposed, data.StatusDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Last_Emp_Name", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "LastEmpName", DataRowVersion.Proposed, data.LastEmpName.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Last_Change_Date", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "LastChangeDate", DataRowVersion.Proposed, data.LastChangeDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Fax", SqlDbType.VarChar, 14, ParameterDirection.Input, false, 0, 0, "Fax", DataRowVersion.Proposed, data.Fax.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Starter_Kit_Ordered", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "StarterKitOrdered", DataRowVersion.Proposed, !data.StarterKitOrdered.IsValid ? data.StarterKitOrdered.DBValue : data.StarterKitOrdered.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@Starter_Kit", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "StarterKit", DataRowVersion.Proposed, data.StarterKit.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Startdate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "Startdate", DataRowVersion.Proposed, data.Startdate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Promo_Flag", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "PromoFlag", DataRowVersion.Proposed, data.PromoFlag.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Nsf_Warning", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "IsOrderingDisabled", DataRowVersion.Proposed, !data.IsOrderingDisabled.IsValid ? data.IsOrderingDisabled.DBValue : data.IsOrderingDisabled.DBValue.Equals ("N") ? " " : "N"));
	    cmd.Parameters.Add(new SqlParameter("@Password1", SqlDbType.VarChar, 40, ParameterDirection.Input, false, 0, 0, "Password", DataRowVersion.Proposed, data.Password.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Geocode", SqlDbType.Char, 10, ParameterDirection.Input, false, 0, 0, "Geocode", DataRowVersion.Proposed, data.Geocode.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Inout", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "Inout", DataRowVersion.Proposed, data.Inout.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Localcode1", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "Localcode1", DataRowVersion.Proposed, data.Localcode1.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Localcode2", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "Localcode2", DataRowVersion.Proposed, data.Localcode2.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Localcode3", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "Localcode3", DataRowVersion.Proposed, data.Localcode3.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Localcode4", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "Localcode4", DataRowVersion.Proposed, data.Localcode4.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Localcode5", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "Localcode5", DataRowVersion.Proposed, data.Localcode5.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Sponsor_Up2", SqlDbType.VarChar, 7, ParameterDirection.Input, false, 0, 0, "SponsorUp2", DataRowVersion.Proposed, data.SponsorUp2.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Sponsor_Up3", SqlDbType.VarChar, 7, ParameterDirection.Input, false, 0, 0, "SponsorUp3", DataRowVersion.Proposed, data.SponsorUp3.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Email", DataRowVersion.Proposed, data.Email.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Sponsor_Up4", SqlDbType.VarChar, 7, ParameterDirection.Input, false, 0, 0, "SponsorUp4", DataRowVersion.Proposed, data.SponsorUp4.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Sponsor_Up5", SqlDbType.VarChar, 7, ParameterDirection.Input, false, 0, 0, "SponsorUp5", DataRowVersion.Proposed, data.SponsorUp5.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrigStartDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "OrigStartDate", DataRowVersion.Proposed, data.OrigStartDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@OrigDemoId", SqlDbType.Char, 7, ParameterDirection.Input, false, 0, 0, "OrigDemoId", DataRowVersion.Proposed, data.OrigDemoId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Referral", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "Referral", DataRowVersion.Proposed, !data.Referral.IsValid ? data.Referral.DBValue : data.Referral.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@LastReferralDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "LastReferralDate", DataRowVersion.Proposed, data.LastReferralDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Latitude", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "Latitude", DataRowVersion.Proposed, data.Latitude.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Longitude", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "Longitude", DataRowVersion.Proposed, data.Longitude.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@AccountVerified", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "AccountVerified", DataRowVersion.Proposed, data.AccountVerified.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@AllElectronic", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "AllElectronic", DataRowVersion.Proposed, data.AllElectronic.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@AccountVerifiedDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "AccountVerifiedDate", DataRowVersion.Proposed, data.AccountVerifiedDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@APOStatusDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "APOStatusDate", DataRowVersion.Proposed, data.APOStatusDate.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Pass_Lock", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsPasswordDisabled", DataRowVersion.Proposed, !data.IsPasswordDisabled.IsValid ? data.IsPasswordDisabled.DBValue : data.IsPasswordDisabled.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@GeneratedPassword", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsPasswordTemporary", DataRowVersion.Proposed, !data.IsPasswordTemporary.IsValid ? data.IsPasswordTemporary.DBValue : data.IsPasswordTemporary.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@APOFlag", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "APOFlag", DataRowVersion.Proposed, !data.APOFlag.IsValid ? data.APOFlag.DBValue : data.APOFlag.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@SmallSupplierFlag", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsSmallSupplier", DataRowVersion.Proposed, !data.IsSmallSupplier.IsValid ? data.IsSmallSupplier.DBValue : data.IsSmallSupplier.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@ReceivedFreeShip", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "ReceivedFreeShip", DataRowVersion.Proposed, !data.ReceivedFreeShip.IsValid ? data.ReceivedFreeShip.DBValue : data.ReceivedFreeShip.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@WantPrintedReports", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "WantPrintedReports", DataRowVersion.Proposed, !data.WantPrintedReports.IsValid ? data.WantPrintedReports.DBValue : data.WantPrintedReports.DBValue.Equals ("Y") ? 1 : 0));
	    cmd.Parameters.Add(new SqlParameter("@QuestionId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "QuestionId", DataRowVersion.Proposed, data.QuestionId.DBValue));
	    cmd.Parameters.Add(new SqlParameter("@Answer", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Answer", DataRowVersion.Proposed, data.Answer.DBValue));

	    // Execute the query
	    cmd.ExecuteNonQuery();
	    // Return field designated
	    return data.DemoId;
        }
        
        /// <summary>
        /// Updates a record in the DemoMast table.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Update(DemonstratorData data)
        {
            // Create and execute the command
	    DemonstratorData oldData = Load ( data.DemoId);
	    string sql = "Update " + TABLE + " set ";
	    if (!oldData.DemoId.Equals(data.DemoId))
	    {
		sql = sql + "Demo_Id=@Demo_Id,";
	    }
	    if (!oldData.SponsorId.Equals(data.SponsorId))
	    {
		sql = sql + "Sponsor_Id=@Sponsor_Id,";
	    }
	    if (!oldData.BusEntityName.Equals(data.BusEntityName))
	    {
		sql = sql + "BusEntityName=@BusEntityName,";
	    }
	    if (!oldData.Tin.Equals(data.Tin))
	    {
		sql = sql + "TIN=@TIN,";
	    }
	    if (!oldData.BusEntityTypeID.Equals(data.BusEntityTypeID))
	    {
		sql = sql + "BusEntityTypeID=@BusEntityTypeID,";
	    }
	    if (!oldData.CountryOfOrigin.Equals(data.CountryOfOrigin))
	    {
		sql = sql + "CountryOfOrigin=@CountryOfOrigin,";
	    }
	    if (!oldData.StateOfOrigin.Equals(data.StateOfOrigin))
	    {
		sql = sql + "StateOfOrigin=@StateOfOrigin,";
	    }
	    if (!oldData.BusEntityEffectDate.Equals(data.BusEntityEffectDate))
	    {
		sql = sql + "BusEntityEffectDate=@BusEntityEffectDate,";
	    }
	    if (!oldData.AppToOpDate.Equals(data.AppToOpDate))
	    {
		sql = sql + "AppToOpDate=@AppToOpDate,";
	    }
	    if (!oldData.NoticeOfIntentDate.Equals(data.NoticeOfIntentDate))
	    {
		sql = sql + "NoticeOfIntentDate=@NoticeOfIntentDate,";
	    }
	    if (!oldData.DuePerformDate.Equals(data.DuePerformDate))
	    {
		sql = sql + "DuePerformDate=@DuePerformDate,";
	    }
	    if (!oldData.AuthorizationDate.Equals(data.AuthorizationDate))
	    {
		sql = sql + "AuthorizationDate=@AuthorizationDate,";
	    }
	    if (!oldData.FormationDate.Equals(data.FormationDate))
	    {
		sql = sql + "FormationDate=@FormationDate,";
	    }
	    if (!oldData.ShipToAddress1.Equals(data.ShipToAddress1))
	    {
		sql = sql + "Ship_To_Address1=@Ship_To_Address1,";
	    }
	    if (!oldData.ShipToAddress2.Equals(data.ShipToAddress2))
	    {
		sql = sql + "Ship_To_Address2=@Ship_To_Address2,";
	    }
	    if (!oldData.ShipToCity.Equals(data.ShipToCity))
	    {
		sql = sql + "Ship_To_City=@Ship_To_City,";
	    }
	    if (!oldData.ShipToState.Equals(data.ShipToState))
	    {
		sql = sql + "Ship_To_State=@Ship_To_State,";
	    }
	    if (!oldData.ShipToCounty.Equals(data.ShipToCounty))
	    {
		sql = sql + "Ship_To_County=@Ship_To_County,";
	    }
	    if (!oldData.ShipToZip.Equals(data.ShipToZip))
	    {
		sql = sql + "Ship_To_Zip=@Ship_To_Zip,";
	    }
	    if (!oldData.ShipToZip4.Equals(data.ShipToZip4))
	    {
		sql = sql + "Ship_To_Zip4=@Ship_To_Zip4,";
	    }
	    if (!oldData.DemoLvl.Equals(data.DemoLvl))
	    {
		sql = sql + "Demo_Lvl=@Demo_Lvl,";
	    }
	    if (!oldData.Status.Equals(data.Status))
	    {
		sql = sql + "Status=@Status,";
	    }
	    if (!oldData.StatusDate.Equals(data.StatusDate))
	    {
		sql = sql + "Status_Date=@Status_Date,";
	    }
	    if (!oldData.LastEmpName.Equals(data.LastEmpName))
	    {
		sql = sql + "Last_Emp_Name=@Last_Emp_Name,";
	    }
	    if (!oldData.LastChangeDate.Equals(data.LastChangeDate))
	    {
		sql = sql + "Last_Change_Date=@Last_Change_Date,";
	    }
	    if (!oldData.Fax.Equals(data.Fax))
	    {
		sql = sql + "Fax=@Fax,";
	    }
	    if (!oldData.StarterKitOrdered.Equals(data.StarterKitOrdered))
	    {
		sql = sql + "Starter_Kit_Ordered=@Starter_Kit_Ordered,";
	    }
	    if (!oldData.StarterKit.Equals(data.StarterKit))
	    {
		sql = sql + "Starter_Kit=@Starter_Kit,";
	    }
	    if (!oldData.Startdate.Equals(data.Startdate))
	    {
		sql = sql + "Startdate=@Startdate,";
	    }
	    if (!oldData.PromoFlag.Equals(data.PromoFlag))
	    {
		sql = sql + "Promo_Flag=@Promo_Flag,";
	    }
	    if (!oldData.IsOrderingDisabled.Equals(data.IsOrderingDisabled))
	    {
		sql = sql + "Nsf_Warning=@Nsf_Warning,";
	    }
	    if (!oldData.Password.Equals(data.Password))
	    {
		sql = sql + "Password1=@Password1,";
	    }
	    if (!oldData.Geocode.Equals(data.Geocode))
	    {
		sql = sql + "Geocode=@Geocode,";
	    }
	    if (!oldData.Inout.Equals(data.Inout))
	    {
		sql = sql + "Inout=@Inout,";
	    }
	    if (!oldData.Localcode1.Equals(data.Localcode1))
	    {
		sql = sql + "Localcode1=@Localcode1,";
	    }
	    if (!oldData.Localcode2.Equals(data.Localcode2))
	    {
		sql = sql + "Localcode2=@Localcode2,";
	    }
	    if (!oldData.Localcode3.Equals(data.Localcode3))
	    {
		sql = sql + "Localcode3=@Localcode3,";
	    }
	    if (!oldData.Localcode4.Equals(data.Localcode4))
	    {
		sql = sql + "Localcode4=@Localcode4,";
	    }
	    if (!oldData.Localcode5.Equals(data.Localcode5))
	    {
		sql = sql + "Localcode5=@Localcode5,";
	    }
	    if (!oldData.SponsorUp2.Equals(data.SponsorUp2))
	    {
		sql = sql + "Sponsor_Up2=@Sponsor_Up2,";
	    }
	    if (!oldData.SponsorUp3.Equals(data.SponsorUp3))
	    {
		sql = sql + "Sponsor_Up3=@Sponsor_Up3,";
	    }
	    if (!oldData.Email.Equals(data.Email))
	    {
		sql = sql + "Email=@Email,";
	    }
	    if (!oldData.SponsorUp4.Equals(data.SponsorUp4))
	    {
		sql = sql + "Sponsor_Up4=@Sponsor_Up4,";
	    }
	    if (!oldData.SponsorUp5.Equals(data.SponsorUp5))
	    {
		sql = sql + "Sponsor_Up5=@Sponsor_Up5,";
	    }
	    if (!oldData.OrigStartDate.Equals(data.OrigStartDate))
	    {
		sql = sql + "OrigStartDate=@OrigStartDate,";
	    }
	    if (!oldData.OrigDemoId.Equals(data.OrigDemoId))
	    {
		sql = sql + "OrigDemoId=@OrigDemoId,";
	    }
	    if (!oldData.Referral.Equals(data.Referral))
	    {
		sql = sql + "Referral=@Referral,";
	    }
	    if (!oldData.LastReferralDate.Equals(data.LastReferralDate))
	    {
		sql = sql + "LastReferralDate=@LastReferralDate,";
	    }
	    if (!oldData.Latitude.Equals(data.Latitude))
	    {
		sql = sql + "Latitude=@Latitude,";
	    }
	    if (!oldData.Longitude.Equals(data.Longitude))
	    {
		sql = sql + "Longitude=@Longitude,";
	    }
	    if (!oldData.AccountVerified.Equals(data.AccountVerified))
	    {
		sql = sql + "AccountVerified=@AccountVerified,";
	    }
	    if (!oldData.AllElectronic.Equals(data.AllElectronic))
	    {
		sql = sql + "AllElectronic=@AllElectronic,";
	    }
	    if (!oldData.AccountVerifiedDate.Equals(data.AccountVerifiedDate))
	    {
		sql = sql + "AccountVerifiedDate=@AccountVerifiedDate,";
	    }
	    if (!oldData.APOStatusDate.Equals(data.APOStatusDate))
	    {
		sql = sql + "APOStatusDate=@APOStatusDate,";
	    }
	    if (!oldData.IsPasswordDisabled.Equals(data.IsPasswordDisabled))
	    {
		sql = sql + "Pass_Lock=@Pass_Lock,";
	    }
	    if (!oldData.IsPasswordTemporary.Equals(data.IsPasswordTemporary))
	    {
		sql = sql + "GeneratedPassword=@GeneratedPassword,";
	    }
	    if (!oldData.APOFlag.Equals(data.APOFlag))
	    {
		sql = sql + "APOFlag=@APOFlag,";
	    }
	    if (!oldData.IsSmallSupplier.Equals(data.IsSmallSupplier))
	    {
		sql = sql + "SmallSupplierFlag=@SmallSupplierFlag,";
	    }
	    if (!oldData.ReceivedFreeShip.Equals(data.ReceivedFreeShip))
	    {
		sql = sql + "ReceivedFreeShip=@ReceivedFreeShip,";
	    }
	    if (!oldData.WantPrintedReports.Equals(data.WantPrintedReports))
	    {
		sql = sql + "WantPrintedReports=@WantPrintedReports,";
	    }
	    if (!oldData.QuestionId.Equals(data.QuestionId))
	    {
		sql = sql + "QuestionId=@QuestionId,";
	    }
	    if (!oldData.Answer.Equals(data.Answer))
	    {
		sql = sql + "Answer=@Answer,";
	    }
	    WhereClause w = new WhereClause();
	    w.And("Demo_Id", data.DemoId.DBValue);
	    sql = sql.Substring(0,sql.Length - 1) + w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.ORDERDB, sql, CommandType.Text, COMMAND_TIMEOUT);
	    //Create the parameters and append them to the command object
	    if (!oldData.DemoId.Equals(data.DemoId))
	    {
		cmd.Parameters.Add(new SqlParameter("@Demo_Id", SqlDbType.VarChar, 7, ParameterDirection.Input, false, 0, 0, "DemoId", DataRowVersion.Proposed, data.DemoId.DBValue));

	    }
	    if (!oldData.SponsorId.Equals(data.SponsorId))
	    {
		Logger.Instance.Update ("DemoMast.Sponsor_Id", "Sponsor_Id", oldData.SponsorId, data.SponsorId);
	    }
	    if (!oldData.SponsorId.Equals(data.SponsorId))
	    {
		cmd.Parameters.Add(new SqlParameter("@Sponsor_Id", SqlDbType.VarChar, 7, ParameterDirection.Input, false, 0, 0, "SponsorId", DataRowVersion.Proposed, data.SponsorId.DBValue));

	    }
	    if (!oldData.BusEntityName.Equals(data.BusEntityName))
	    {
		Logger.Instance.Update ("DemoMast.BusEntityName", "BusEntityName", oldData.BusEntityName, data.BusEntityName);
	    }
	    if (!oldData.BusEntityName.Equals(data.BusEntityName))
	    {
		cmd.Parameters.Add(new SqlParameter("@BusEntityName", SqlDbType.VarChar, 150, ParameterDirection.Input, false, 0, 0, "BusEntityName", DataRowVersion.Proposed, data.BusEntityName.DBValue));

	    }
	    if (!oldData.Tin.Equals(data.Tin))
	    {
		Logger.Instance.Update ("DemoMast.TIN", "TIN", oldData.Tin, data.Tin);
	    }
	    if (!oldData.Tin.Equals(data.Tin))
	    {
		cmd.Parameters.Add(new SqlParameter("@TIN", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "Tin", DataRowVersion.Proposed, data.Tin.DBValue));

	    }
	    if (!oldData.BusEntityTypeID.Equals(data.BusEntityTypeID))
	    {
		Logger.Instance.Update ("DemoMast.BusEntityTypeID", "BusEntityTypeID", oldData.BusEntityTypeID, data.BusEntityTypeID);
	    }
	    if (!oldData.BusEntityTypeID.Equals(data.BusEntityTypeID))
	    {
		cmd.Parameters.Add(new SqlParameter("@BusEntityTypeID", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "BusEntityTypeID", DataRowVersion.Proposed, data.BusEntityTypeID.DBValue));

	    }
	    if (!oldData.CountryOfOrigin.Equals(data.CountryOfOrigin))
	    {
		Logger.Instance.Update ("DemoMast.CountryOfOrigin", "CountryOfOrigin", oldData.CountryOfOrigin, data.CountryOfOrigin);
	    }
	    if (!oldData.CountryOfOrigin.Equals(data.CountryOfOrigin))
	    {
		cmd.Parameters.Add(new SqlParameter("@CountryOfOrigin", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "CountryOfOrigin", DataRowVersion.Proposed, data.CountryOfOrigin.DBValue));

	    }
	    if (!oldData.StateOfOrigin.Equals(data.StateOfOrigin))
	    {
		Logger.Instance.Update ("DemoMast.StateOfOrigin", "StateOfOrigin", oldData.StateOfOrigin, data.StateOfOrigin);
	    }
	    if (!oldData.StateOfOrigin.Equals(data.StateOfOrigin))
	    {
		cmd.Parameters.Add(new SqlParameter("@StateOfOrigin", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "StateOfOrigin", DataRowVersion.Proposed, data.StateOfOrigin.DBValue));

	    }
	    if (!oldData.BusEntityEffectDate.Equals(data.BusEntityEffectDate))
	    {
		Logger.Instance.Update ("DemoMast.BusEntityEffectDate", "BusEntityEffectDate", oldData.BusEntityEffectDate, data.BusEntityEffectDate);
	    }
	    if (!oldData.BusEntityEffectDate.Equals(data.BusEntityEffectDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@BusEntityEffectDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "BusEntityEffectDate", DataRowVersion.Proposed, data.BusEntityEffectDate.DBValue));

	    }
	    if (!oldData.AppToOpDate.Equals(data.AppToOpDate))
	    {
		Logger.Instance.Update ("DemoMast.AppToOpDate", "AppToOpDate", oldData.AppToOpDate, data.AppToOpDate);
	    }
	    if (!oldData.AppToOpDate.Equals(data.AppToOpDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@AppToOpDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "AppToOpDate", DataRowVersion.Proposed, data.AppToOpDate.DBValue));

	    }
	    if (!oldData.NoticeOfIntentDate.Equals(data.NoticeOfIntentDate))
	    {
		Logger.Instance.Update ("DemoMast.NoticeOfIntentDate", "NoticeOfIntentDate", oldData.NoticeOfIntentDate, data.NoticeOfIntentDate);
	    }
	    if (!oldData.NoticeOfIntentDate.Equals(data.NoticeOfIntentDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@NoticeOfIntentDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "NoticeOfIntentDate", DataRowVersion.Proposed, data.NoticeOfIntentDate.DBValue));

	    }
	    if (!oldData.DuePerformDate.Equals(data.DuePerformDate))
	    {
		Logger.Instance.Update ("DemoMast.DuePerformDate", "DuePerformDate", oldData.DuePerformDate, data.DuePerformDate);
	    }
	    if (!oldData.DuePerformDate.Equals(data.DuePerformDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@DuePerformDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "DuePerformDate", DataRowVersion.Proposed, data.DuePerformDate.DBValue));

	    }
	    if (!oldData.AuthorizationDate.Equals(data.AuthorizationDate))
	    {
		Logger.Instance.Update ("DemoMast.AuthorizationDate", "AuthorizationDate", oldData.AuthorizationDate, data.AuthorizationDate);
	    }
	    if (!oldData.AuthorizationDate.Equals(data.AuthorizationDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@AuthorizationDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "AuthorizationDate", DataRowVersion.Proposed, data.AuthorizationDate.DBValue));

	    }
	    if (!oldData.FormationDate.Equals(data.FormationDate))
	    {
		Logger.Instance.Update ("DemoMast.FormationDate", "FormationDate", oldData.FormationDate, data.FormationDate);
	    }
	    if (!oldData.FormationDate.Equals(data.FormationDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@FormationDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "FormationDate", DataRowVersion.Proposed, data.FormationDate.DBValue));

	    }
	    if (!oldData.ShipToAddress1.Equals(data.ShipToAddress1))
	    {
		Logger.Instance.Update ("DemoMast.Ship_To_Address1", "Ship_To_Address1", oldData.ShipToAddress1, data.ShipToAddress1);
	    }
	    if (!oldData.ShipToAddress1.Equals(data.ShipToAddress1))
	    {
		cmd.Parameters.Add(new SqlParameter("@Ship_To_Address1", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "ShipToAddress1", DataRowVersion.Proposed, data.ShipToAddress1.DBValue));

	    }
	    if (!oldData.ShipToAddress2.Equals(data.ShipToAddress2))
	    {
		Logger.Instance.Update ("DemoMast.Ship_To_Address2", "Ship_To_Address2", oldData.ShipToAddress2, data.ShipToAddress2);
	    }
	    if (!oldData.ShipToAddress2.Equals(data.ShipToAddress2))
	    {
		cmd.Parameters.Add(new SqlParameter("@Ship_To_Address2", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "ShipToAddress2", DataRowVersion.Proposed, data.ShipToAddress2.DBValue));

	    }
	    if (!oldData.ShipToCity.Equals(data.ShipToCity))
	    {
		Logger.Instance.Update ("DemoMast.Ship_To_City", "Ship_To_City", oldData.ShipToCity, data.ShipToCity);
	    }
	    if (!oldData.ShipToCity.Equals(data.ShipToCity))
	    {
		cmd.Parameters.Add(new SqlParameter("@Ship_To_City", SqlDbType.VarChar, 25, ParameterDirection.Input, false, 0, 0, "ShipToCity", DataRowVersion.Proposed, data.ShipToCity.DBValue));

	    }
	    if (!oldData.ShipToState.Equals(data.ShipToState))
	    {
		Logger.Instance.Update ("DemoMast.Ship_To_State", "Ship_To_State", oldData.ShipToState, data.ShipToState);
	    }
	    if (!oldData.ShipToState.Equals(data.ShipToState))
	    {
		cmd.Parameters.Add(new SqlParameter("@Ship_To_State", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "ShipToState", DataRowVersion.Proposed, data.ShipToState.DBValue));

	    }
	    if (!oldData.ShipToCounty.Equals(data.ShipToCounty))
	    {
		Logger.Instance.Update ("DemoMast.Ship_To_County", "Ship_To_County", oldData.ShipToCounty, data.ShipToCounty);
	    }
	    if (!oldData.ShipToCounty.Equals(data.ShipToCounty))
	    {
		cmd.Parameters.Add(new SqlParameter("@Ship_To_County", SqlDbType.VarChar, 25, ParameterDirection.Input, false, 0, 0, "ShipToCounty", DataRowVersion.Proposed, data.ShipToCounty.DBValue));

	    }
	    if (!oldData.ShipToZip.Equals(data.ShipToZip))
	    {
		Logger.Instance.Update ("DemoMast.Ship_To_Zip", "Ship_To_Zip", oldData.ShipToZip, data.ShipToZip);
	    }
	    if (!oldData.ShipToZip.Equals(data.ShipToZip))
	    {
		cmd.Parameters.Add(new SqlParameter("@Ship_To_Zip", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "ShipToZip", DataRowVersion.Proposed, data.ShipToZip.DBValue));

	    }
	    if (!oldData.ShipToZip4.Equals(data.ShipToZip4))
	    {
		Logger.Instance.Update ("DemoMast.Ship_To_Zip4", "Ship_To_Zip4", oldData.ShipToZip4, data.ShipToZip4);
	    }
	    if (!oldData.ShipToZip4.Equals(data.ShipToZip4))
	    {
		cmd.Parameters.Add(new SqlParameter("@Ship_To_Zip4", SqlDbType.VarChar, 4, ParameterDirection.Input, false, 0, 0, "ShipToZip4", DataRowVersion.Proposed, data.ShipToZip4.DBValue));

	    }
	    if (!oldData.DemoLvl.Equals(data.DemoLvl))
	    {
		Logger.Instance.Update ("DemoMast.Demo_Lvl", "Demo_Lvl", oldData.DemoLvl, data.DemoLvl);
	    }
	    if (!oldData.DemoLvl.Equals(data.DemoLvl))
	    {
		cmd.Parameters.Add(new SqlParameter("@Demo_Lvl", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "DemoLvl", DataRowVersion.Proposed, data.DemoLvl.DBValue));

	    }
	    if (!oldData.Status.Equals(data.Status))
	    {
		Logger.Instance.Update ("DemoMast.Status", "Status", oldData.Status, data.Status);
	    }
	    if (!oldData.Status.Equals(data.Status))
	    {
		cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "Status", DataRowVersion.Proposed, data.Status.DBValue));

	    }
	    if (!oldData.StatusDate.Equals(data.StatusDate))
	    {
		Logger.Instance.Update ("DemoMast.Status_Date", "Status_Date", oldData.StatusDate, data.StatusDate);
	    }
	    if (!oldData.StatusDate.Equals(data.StatusDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@Status_Date", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "StatusDate", DataRowVersion.Proposed, data.StatusDate.DBValue));

	    }
	    if (!oldData.LastEmpName.Equals(data.LastEmpName))
	    {
		Logger.Instance.Update ("DemoMast.Last_Emp_Name", "Last_Emp_Name", oldData.LastEmpName, data.LastEmpName);
	    }
	    if (!oldData.LastEmpName.Equals(data.LastEmpName))
	    {
		cmd.Parameters.Add(new SqlParameter("@Last_Emp_Name", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 0, 0, "LastEmpName", DataRowVersion.Proposed, data.LastEmpName.DBValue));

	    }
	    if (!oldData.LastChangeDate.Equals(data.LastChangeDate))
	    {
		Logger.Instance.Update ("DemoMast.Last_Change_Date", "Last_Change_Date", oldData.LastChangeDate, data.LastChangeDate);
	    }
	    if (!oldData.LastChangeDate.Equals(data.LastChangeDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@Last_Change_Date", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "LastChangeDate", DataRowVersion.Proposed, data.LastChangeDate.DBValue));

	    }
	    if (!oldData.Fax.Equals(data.Fax))
	    {
		Logger.Instance.Update ("DemoMast.Fax", "Fax", oldData.Fax, data.Fax);
	    }
	    if (!oldData.Fax.Equals(data.Fax))
	    {
		cmd.Parameters.Add(new SqlParameter("@Fax", SqlDbType.VarChar, 14, ParameterDirection.Input, false, 0, 0, "Fax", DataRowVersion.Proposed, data.Fax.DBValue));

	    }
	    if (!oldData.StarterKitOrdered.Equals(data.StarterKitOrdered))
	    {
		Logger.Instance.Update ("DemoMast.Starter_Kit_Ordered", "Starter_Kit_Ordered", oldData.StarterKitOrdered, data.StarterKitOrdered);
	    }
	    if (!oldData.StarterKitOrdered.Equals(data.StarterKitOrdered))
	    {
		cmd.Parameters.Add(new SqlParameter("@Starter_Kit_Ordered", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "StarterKitOrdered", DataRowVersion.Proposed, !data.StarterKitOrdered.IsValid ? data.StarterKitOrdered.DBValue : data.StarterKitOrdered.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.StarterKit.Equals(data.StarterKit))
	    {
		Logger.Instance.Update ("DemoMast.Starter_Kit", "Starter_Kit", oldData.StarterKit, data.StarterKit);
	    }
	    if (!oldData.StarterKit.Equals(data.StarterKit))
	    {
		cmd.Parameters.Add(new SqlParameter("@Starter_Kit", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "StarterKit", DataRowVersion.Proposed, data.StarterKit.DBValue));

	    }
	    if (!oldData.Startdate.Equals(data.Startdate))
	    {
		Logger.Instance.Update ("DemoMast.Startdate", "Startdate", oldData.Startdate, data.Startdate);
	    }
	    if (!oldData.Startdate.Equals(data.Startdate))
	    {
		cmd.Parameters.Add(new SqlParameter("@Startdate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "Startdate", DataRowVersion.Proposed, data.Startdate.DBValue));

	    }
	    if (!oldData.PromoFlag.Equals(data.PromoFlag))
	    {
		Logger.Instance.Update ("DemoMast.Promo_Flag", "Promo_Flag", oldData.PromoFlag, data.PromoFlag);
	    }
	    if (!oldData.PromoFlag.Equals(data.PromoFlag))
	    {
		cmd.Parameters.Add(new SqlParameter("@Promo_Flag", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "PromoFlag", DataRowVersion.Proposed, data.PromoFlag.DBValue));

	    }
	    if (!oldData.IsOrderingDisabled.Equals(data.IsOrderingDisabled))
	    {
		Logger.Instance.Update ("DemoMast.Nsf_Warning", "Nsf_Warning", oldData.IsOrderingDisabled, data.IsOrderingDisabled);
	    }
	    if (!oldData.IsOrderingDisabled.Equals(data.IsOrderingDisabled))
	    {
		cmd.Parameters.Add(new SqlParameter("@Nsf_Warning", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "IsOrderingDisabled", DataRowVersion.Proposed, !data.IsOrderingDisabled.IsValid ? data.IsOrderingDisabled.DBValue : data.IsOrderingDisabled.DBValue.Equals ("N") ? " " : "N"));

	    }
	    if (!oldData.Password.Equals(data.Password))
	    {
		Logger.Instance.Update ("DemoMast.Password1", "Password1", oldData.Password, data.Password);
	    }
	    if (!oldData.Password.Equals(data.Password))
	    {
		cmd.Parameters.Add(new SqlParameter("@Password1", SqlDbType.VarChar, 40, ParameterDirection.Input, false, 0, 0, "Password", DataRowVersion.Proposed, data.Password.DBValue));

	    }
	    if (!oldData.Geocode.Equals(data.Geocode))
	    {
		Logger.Instance.Update ("DemoMast.Geocode", "Geocode", oldData.Geocode, data.Geocode);
	    }
	    if (!oldData.Geocode.Equals(data.Geocode))
	    {
		cmd.Parameters.Add(new SqlParameter("@Geocode", SqlDbType.Char, 10, ParameterDirection.Input, false, 0, 0, "Geocode", DataRowVersion.Proposed, data.Geocode.DBValue));

	    }
	    if (!oldData.Inout.Equals(data.Inout))
	    {
		Logger.Instance.Update ("DemoMast.Inout", "Inout", oldData.Inout, data.Inout);
	    }
	    if (!oldData.Inout.Equals(data.Inout))
	    {
		cmd.Parameters.Add(new SqlParameter("@Inout", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "Inout", DataRowVersion.Proposed, data.Inout.DBValue));

	    }
	    if (!oldData.Localcode1.Equals(data.Localcode1))
	    {
		Logger.Instance.Update ("DemoMast.Localcode1", "Localcode1", oldData.Localcode1, data.Localcode1);
	    }
	    if (!oldData.Localcode1.Equals(data.Localcode1))
	    {
		cmd.Parameters.Add(new SqlParameter("@Localcode1", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "Localcode1", DataRowVersion.Proposed, data.Localcode1.DBValue));

	    }
	    if (!oldData.Localcode2.Equals(data.Localcode2))
	    {
		Logger.Instance.Update ("DemoMast.Localcode2", "Localcode2", oldData.Localcode2, data.Localcode2);
	    }
	    if (!oldData.Localcode2.Equals(data.Localcode2))
	    {
		cmd.Parameters.Add(new SqlParameter("@Localcode2", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "Localcode2", DataRowVersion.Proposed, data.Localcode2.DBValue));

	    }
	    if (!oldData.Localcode3.Equals(data.Localcode3))
	    {
		Logger.Instance.Update ("DemoMast.Localcode3", "Localcode3", oldData.Localcode3, data.Localcode3);
	    }
	    if (!oldData.Localcode3.Equals(data.Localcode3))
	    {
		cmd.Parameters.Add(new SqlParameter("@Localcode3", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "Localcode3", DataRowVersion.Proposed, data.Localcode3.DBValue));

	    }
	    if (!oldData.Localcode4.Equals(data.Localcode4))
	    {
		Logger.Instance.Update ("DemoMast.Localcode4", "Localcode4", oldData.Localcode4, data.Localcode4);
	    }
	    if (!oldData.Localcode4.Equals(data.Localcode4))
	    {
		cmd.Parameters.Add(new SqlParameter("@Localcode4", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "Localcode4", DataRowVersion.Proposed, data.Localcode4.DBValue));

	    }
	    if (!oldData.Localcode5.Equals(data.Localcode5))
	    {
		Logger.Instance.Update ("DemoMast.Localcode5", "Localcode5", oldData.Localcode5, data.Localcode5);
	    }
	    if (!oldData.Localcode5.Equals(data.Localcode5))
	    {
		cmd.Parameters.Add(new SqlParameter("@Localcode5", SqlDbType.Char, 2, ParameterDirection.Input, false, 0, 0, "Localcode5", DataRowVersion.Proposed, data.Localcode5.DBValue));

	    }
	    if (!oldData.SponsorUp2.Equals(data.SponsorUp2))
	    {
		Logger.Instance.Update ("DemoMast.Sponsor_Up2", "Sponsor_Up2", oldData.SponsorUp2, data.SponsorUp2);
	    }
	    if (!oldData.SponsorUp2.Equals(data.SponsorUp2))
	    {
		cmd.Parameters.Add(new SqlParameter("@Sponsor_Up2", SqlDbType.VarChar, 7, ParameterDirection.Input, false, 0, 0, "SponsorUp2", DataRowVersion.Proposed, data.SponsorUp2.DBValue));

	    }
	    if (!oldData.SponsorUp3.Equals(data.SponsorUp3))
	    {
		Logger.Instance.Update ("DemoMast.Sponsor_Up3", "Sponsor_Up3", oldData.SponsorUp3, data.SponsorUp3);
	    }
	    if (!oldData.SponsorUp3.Equals(data.SponsorUp3))
	    {
		cmd.Parameters.Add(new SqlParameter("@Sponsor_Up3", SqlDbType.VarChar, 7, ParameterDirection.Input, false, 0, 0, "SponsorUp3", DataRowVersion.Proposed, data.SponsorUp3.DBValue));

	    }
	    if (!oldData.Email.Equals(data.Email))
	    {
		Logger.Instance.Update ("DemoMast.Email", "Email", oldData.Email, data.Email);
	    }
	    if (!oldData.Email.Equals(data.Email))
	    {
		cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Email", DataRowVersion.Proposed, data.Email.DBValue));

	    }
	    if (!oldData.SponsorUp4.Equals(data.SponsorUp4))
	    {
		Logger.Instance.Update ("DemoMast.Sponsor_Up4", "Sponsor_Up4", oldData.SponsorUp4, data.SponsorUp4);
	    }
	    if (!oldData.SponsorUp4.Equals(data.SponsorUp4))
	    {
		cmd.Parameters.Add(new SqlParameter("@Sponsor_Up4", SqlDbType.VarChar, 7, ParameterDirection.Input, false, 0, 0, "SponsorUp4", DataRowVersion.Proposed, data.SponsorUp4.DBValue));

	    }
	    if (!oldData.SponsorUp5.Equals(data.SponsorUp5))
	    {
		Logger.Instance.Update ("DemoMast.Sponsor_Up5", "Sponsor_Up5", oldData.SponsorUp5, data.SponsorUp5);
	    }
	    if (!oldData.SponsorUp5.Equals(data.SponsorUp5))
	    {
		cmd.Parameters.Add(new SqlParameter("@Sponsor_Up5", SqlDbType.VarChar, 7, ParameterDirection.Input, false, 0, 0, "SponsorUp5", DataRowVersion.Proposed, data.SponsorUp5.DBValue));

	    }
	    if (!oldData.OrigStartDate.Equals(data.OrigStartDate))
	    {
		Logger.Instance.Update ("DemoMast.OrigStartDate", "OrigStartDate", oldData.OrigStartDate, data.OrigStartDate);
	    }
	    if (!oldData.OrigStartDate.Equals(data.OrigStartDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrigStartDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "OrigStartDate", DataRowVersion.Proposed, data.OrigStartDate.DBValue));

	    }
	    if (!oldData.OrigDemoId.Equals(data.OrigDemoId))
	    {
		Logger.Instance.Update ("DemoMast.OrigDemoId", "OrigDemoId", oldData.OrigDemoId, data.OrigDemoId);
	    }
	    if (!oldData.OrigDemoId.Equals(data.OrigDemoId))
	    {
		cmd.Parameters.Add(new SqlParameter("@OrigDemoId", SqlDbType.Char, 7, ParameterDirection.Input, false, 0, 0, "OrigDemoId", DataRowVersion.Proposed, data.OrigDemoId.DBValue));

	    }
	    if (!oldData.Referral.Equals(data.Referral))
	    {
		Logger.Instance.Update ("DemoMast.Referral", "Referral", oldData.Referral, data.Referral);
	    }
	    if (!oldData.Referral.Equals(data.Referral))
	    {
		cmd.Parameters.Add(new SqlParameter("@Referral", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "Referral", DataRowVersion.Proposed, !data.Referral.IsValid ? data.Referral.DBValue : data.Referral.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.LastReferralDate.Equals(data.LastReferralDate))
	    {
		Logger.Instance.Update ("DemoMast.LastReferralDate", "LastReferralDate", oldData.LastReferralDate, data.LastReferralDate);
	    }
	    if (!oldData.LastReferralDate.Equals(data.LastReferralDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@LastReferralDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "LastReferralDate", DataRowVersion.Proposed, data.LastReferralDate.DBValue));

	    }
	    if (!oldData.Latitude.Equals(data.Latitude))
	    {
		Logger.Instance.Update ("DemoMast.Latitude", "Latitude", oldData.Latitude, data.Latitude);
	    }
	    if (!oldData.Latitude.Equals(data.Latitude))
	    {
		cmd.Parameters.Add(new SqlParameter("@Latitude", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "Latitude", DataRowVersion.Proposed, data.Latitude.DBValue));

	    }
	    if (!oldData.Longitude.Equals(data.Longitude))
	    {
		Logger.Instance.Update ("DemoMast.Longitude", "Longitude", oldData.Longitude, data.Longitude);
	    }
	    if (!oldData.Longitude.Equals(data.Longitude))
	    {
		cmd.Parameters.Add(new SqlParameter("@Longitude", SqlDbType.Float, 0, ParameterDirection.Input, false, 0, 0, "Longitude", DataRowVersion.Proposed, data.Longitude.DBValue));

	    }
	    if (!oldData.AccountVerified.Equals(data.AccountVerified))
	    {
		Logger.Instance.Update ("DemoMast.AccountVerified", "AccountVerified", oldData.AccountVerified, data.AccountVerified);
	    }
	    if (!oldData.AccountVerified.Equals(data.AccountVerified))
	    {
		cmd.Parameters.Add(new SqlParameter("@AccountVerified", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "AccountVerified", DataRowVersion.Proposed, data.AccountVerified.DBValue));

	    }
	    if (!oldData.AllElectronic.Equals(data.AllElectronic))
	    {
		Logger.Instance.Update ("DemoMast.AllElectronic", "AllElectronic", oldData.AllElectronic, data.AllElectronic);
	    }
	    if (!oldData.AllElectronic.Equals(data.AllElectronic))
	    {
		cmd.Parameters.Add(new SqlParameter("@AllElectronic", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "AllElectronic", DataRowVersion.Proposed, data.AllElectronic.DBValue));

	    }
	    if (!oldData.AccountVerifiedDate.Equals(data.AccountVerifiedDate))
	    {
		Logger.Instance.Update ("DemoMast.AccountVerifiedDate", "AccountVerifiedDate", oldData.AccountVerifiedDate, data.AccountVerifiedDate);
	    }
	    if (!oldData.AccountVerifiedDate.Equals(data.AccountVerifiedDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@AccountVerifiedDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "AccountVerifiedDate", DataRowVersion.Proposed, data.AccountVerifiedDate.DBValue));

	    }
	    if (!oldData.APOStatusDate.Equals(data.APOStatusDate))
	    {
		Logger.Instance.Update ("DemoMast.APOStatusDate", "APOStatusDate", oldData.APOStatusDate, data.APOStatusDate);
	    }
	    if (!oldData.APOStatusDate.Equals(data.APOStatusDate))
	    {
		cmd.Parameters.Add(new SqlParameter("@APOStatusDate", SqlDbType.DateTime, 0, ParameterDirection.Input, false, 0, 0, "APOStatusDate", DataRowVersion.Proposed, data.APOStatusDate.DBValue));

	    }
	    if (!oldData.IsPasswordDisabled.Equals(data.IsPasswordDisabled))
	    {
		Logger.Instance.Update ("DemoMast.Pass_Lock", "Pass_Lock", oldData.IsPasswordDisabled, data.IsPasswordDisabled);
	    }
	    if (!oldData.IsPasswordDisabled.Equals(data.IsPasswordDisabled))
	    {
		cmd.Parameters.Add(new SqlParameter("@Pass_Lock", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsPasswordDisabled", DataRowVersion.Proposed, !data.IsPasswordDisabled.IsValid ? data.IsPasswordDisabled.DBValue : data.IsPasswordDisabled.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.IsPasswordTemporary.Equals(data.IsPasswordTemporary))
	    {
		Logger.Instance.Update ("DemoMast.GeneratedPassword", "GeneratedPassword", oldData.IsPasswordTemporary, data.IsPasswordTemporary);
	    }
	    if (!oldData.IsPasswordTemporary.Equals(data.IsPasswordTemporary))
	    {
		cmd.Parameters.Add(new SqlParameter("@GeneratedPassword", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsPasswordTemporary", DataRowVersion.Proposed, !data.IsPasswordTemporary.IsValid ? data.IsPasswordTemporary.DBValue : data.IsPasswordTemporary.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.APOFlag.Equals(data.APOFlag))
	    {
		Logger.Instance.Update ("DemoMast.APOFlag", "APOFlag", oldData.APOFlag, data.APOFlag);
	    }
	    if (!oldData.APOFlag.Equals(data.APOFlag))
	    {
		cmd.Parameters.Add(new SqlParameter("@APOFlag", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "APOFlag", DataRowVersion.Proposed, !data.APOFlag.IsValid ? data.APOFlag.DBValue : data.APOFlag.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.IsSmallSupplier.Equals(data.IsSmallSupplier))
	    {
		Logger.Instance.Update ("DemoMast.SmallSupplierFlag", "SmallSupplierFlag", oldData.IsSmallSupplier, data.IsSmallSupplier);
	    }
	    if (!oldData.IsSmallSupplier.Equals(data.IsSmallSupplier))
	    {
		cmd.Parameters.Add(new SqlParameter("@SmallSupplierFlag", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "IsSmallSupplier", DataRowVersion.Proposed, !data.IsSmallSupplier.IsValid ? data.IsSmallSupplier.DBValue : data.IsSmallSupplier.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.ReceivedFreeShip.Equals(data.ReceivedFreeShip))
	    {
		Logger.Instance.Update ("DemoMast.ReceivedFreeShip", "ReceivedFreeShip", oldData.ReceivedFreeShip, data.ReceivedFreeShip);
	    }
	    if (!oldData.ReceivedFreeShip.Equals(data.ReceivedFreeShip))
	    {
		cmd.Parameters.Add(new SqlParameter("@ReceivedFreeShip", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "ReceivedFreeShip", DataRowVersion.Proposed, !data.ReceivedFreeShip.IsValid ? data.ReceivedFreeShip.DBValue : data.ReceivedFreeShip.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.WantPrintedReports.Equals(data.WantPrintedReports))
	    {
		Logger.Instance.Update ("DemoMast.WantPrintedReports", "WantPrintedReports", oldData.WantPrintedReports, data.WantPrintedReports);
	    }
	    if (!oldData.WantPrintedReports.Equals(data.WantPrintedReports))
	    {
		cmd.Parameters.Add(new SqlParameter("@WantPrintedReports", SqlDbType.Bit, 0, ParameterDirection.Input, false, 0, 0, "WantPrintedReports", DataRowVersion.Proposed, !data.WantPrintedReports.IsValid ? data.WantPrintedReports.DBValue : data.WantPrintedReports.DBValue.Equals ("Y") ? 1 : 0));

	    }
	    if (!oldData.QuestionId.Equals(data.QuestionId))
	    {
		Logger.Instance.Update ("DemoMast.QuestionId", "QuestionId", oldData.QuestionId, data.QuestionId);
	    }
	    if (!oldData.QuestionId.Equals(data.QuestionId))
	    {
		cmd.Parameters.Add(new SqlParameter("@QuestionId", SqlDbType.Int, 0, ParameterDirection.Input, false, 10, 0, "QuestionId", DataRowVersion.Proposed, data.QuestionId.DBValue));

	    }
	    if (!oldData.Answer.Equals(data.Answer))
	    {
		Logger.Instance.Update ("DemoMast.Answer", "Answer", oldData.Answer, data.Answer);
	    }
	    if (!oldData.Answer.Equals(data.Answer))
	    {
		cmd.Parameters.Add(new SqlParameter("@Answer", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Answer", DataRowVersion.Proposed, data.Answer.DBValue));

	    }

	    // Execute the query
	    if (cmd.Parameters.Count > 0)
	    {
		cmd.ExecuteNonQuery();
	    }
        }
        
        /// <summary>
        /// Deletes a record from the DemoMast table by a composite primary key.
        /// </summary>
        /// <param name=""></param>
        [Generate()]
        public static void Delete(DemoIdType demoId)
        {
            // Create and execute the command
	    string sql = "Delete From " + TABLE;
	    WhereClause w = new WhereClause();
	    w.And("Demo_Id", demoId.DBValue);
	    sql += w.FormatSql();
	    SqlCommand cmd = GetSqlCommand(DatabaseEnum.ORDERDB, sql, CommandType.Text, COMMAND_TIMEOUT);

	    // Execute the query and return the result
	    cmd.ExecuteNonQuery();
        }
    }
}