using System;
using System.Collections;

namespace StampinUp.Types
{

    public class LocaleEnum : Spring2.Core.Types.EnumDataType
    {

	private static readonly ArrayList OPTIONS = new ArrayList();

	public static readonly new LocaleEnum DEFAULT = new LocaleEnum();
	public static readonly new LocaleEnum UNSET = new LocaleEnum();

	/// <summary>
	/// Afrikaans
	/// </summary>
	public static readonly LocaleEnum AF = new LocaleEnum("1", "af");
	/// <summary>
	/// Afrikaans - South Africa
	/// </summary>
	public static readonly LocaleEnum AFZA = new LocaleEnum("2", "af-ZA");
	/// <summary>
	/// Albanian
	/// </summary>
	public static readonly LocaleEnum SQ = new LocaleEnum("3", "sq");
	/// <summary>
	/// Albanian - Albania
	/// </summary>
	public static readonly LocaleEnum SQAL = new LocaleEnum("4", "sq-AL");
	/// <summary>
	/// Arabic
	/// </summary>
	public static readonly LocaleEnum AR = new LocaleEnum("5", "ar");
	/// <summary>
	/// Arabic - Algeria
	/// </summary>
	public static readonly LocaleEnum ARDZ = new LocaleEnum("6", "ar-DZ");
	/// <summary>
	/// Arabic - Bahrain
	/// </summary>
	public static readonly LocaleEnum ARBH = new LocaleEnum("7", "ar-BH");
	/// <summary>
	/// Arabic - Egypt
	/// </summary>
	public static readonly LocaleEnum AREG = new LocaleEnum("8", "ar-EG");
	/// <summary>
	/// Arabic - Iraq
	/// </summary>
	public static readonly LocaleEnum ARIQ = new LocaleEnum("9", "ar-IQ");
	/// <summary>
	/// Arabic - Jordan
	/// </summary>
	public static readonly LocaleEnum ARJO = new LocaleEnum("10", "ar-JO");
	/// <summary>
	/// Arabic - Kuwait
	/// </summary>
	public static readonly LocaleEnum ARKW = new LocaleEnum("11", "ar-KW");
	/// <summary>
	/// Arabic - Lebanon
	/// </summary>
	public static readonly LocaleEnum ARLB = new LocaleEnum("12", "ar-LB");
	/// <summary>
	/// Arabic - Libya
	/// </summary>
	public static readonly LocaleEnum ARLY = new LocaleEnum("13", "ar-LY");
	/// <summary>
	/// Arabic - Morocco
	/// </summary>
	public static readonly LocaleEnum ARMA = new LocaleEnum("14", "ar-MA");
	/// <summary>
	/// Arabic - Oman
	/// </summary>
	public static readonly LocaleEnum AROM = new LocaleEnum("15", "ar-OM");
	/// <summary>
	/// Arabic - Qatar
	/// </summary>
	public static readonly LocaleEnum ARQA = new LocaleEnum("16", "ar-QA");
	/// <summary>
	/// Arabic - Saudi Arabia
	/// </summary>
	public static readonly LocaleEnum ARSA = new LocaleEnum("17", "ar-SA");
	/// <summary>
	/// Arabic - Syria
	/// </summary>
	public static readonly LocaleEnum ARSY = new LocaleEnum("18", "ar-SY");
	/// <summary>
	/// Arabic - Tunisia
	/// </summary>
	public static readonly LocaleEnum ARTN = new LocaleEnum("19", "ar-TN");
	/// <summary>
	/// Arabic - United Arab Emirates
	/// </summary>
	public static readonly LocaleEnum ARAE = new LocaleEnum("20", "ar-AE");
	/// <summary>
	/// Arabic - Yemen
	/// </summary>
	public static readonly LocaleEnum ARYE = new LocaleEnum("21", "ar-YE");
	/// <summary>
	/// Armenian
	/// </summary>
	public static readonly LocaleEnum HY = new LocaleEnum("22", "hy");
	/// <summary>
	/// Armenian - Armenia
	/// </summary>
	public static readonly LocaleEnum HYAM = new LocaleEnum("23", "hy-AM");
	/// <summary>
	/// Azeri
	/// </summary>
	public static readonly LocaleEnum AZ = new LocaleEnum("24", "az");
	/// <summary>
	/// Azeri (Cyrillic) - Azerbaijan
	/// </summary>
	public static readonly LocaleEnum AZAZCYRL = new LocaleEnum("25", "az-AZ-Cyrl");
	/// <summary>
	/// Azeri (Latin) - Azerbaijan
	/// </summary>
	public static readonly LocaleEnum AZAZLATN = new LocaleEnum("26", "az-AZ-Latn");
	/// <summary>
	/// Basque
	/// </summary>
	public static readonly LocaleEnum EU = new LocaleEnum("27", "eu");
	/// <summary>
	/// Basque - Basque
	/// </summary>
	public static readonly LocaleEnum EUES = new LocaleEnum("28", "eu-ES");
	/// <summary>
	/// Belarusian
	/// </summary>
	public static readonly LocaleEnum BE = new LocaleEnum("29", "be");
	/// <summary>
	/// Belarusian - Belarus
	/// </summary>
	public static readonly LocaleEnum BEBY = new LocaleEnum("30", "be-BY");
	/// <summary>
	/// Bulgarian
	/// </summary>
	public static readonly LocaleEnum BG = new LocaleEnum("31", "bg");
	/// <summary>
	/// Bulgarian - Bulgaria
	/// </summary>
	public static readonly LocaleEnum BGBG = new LocaleEnum("32", "bg-BG");
	/// <summary>
	/// Catalan
	/// </summary>
	public static readonly LocaleEnum CA = new LocaleEnum("33", "ca");
	/// <summary>
	/// Catalan - Catalan
	/// </summary>
	public static readonly LocaleEnum CAES = new LocaleEnum("34", "ca-ES");
	/// <summary>
	/// Chinese - Hong Kong SAR
	/// </summary>
	public static readonly LocaleEnum ZHHK = new LocaleEnum("35", "zh-HK");
	/// <summary>
	/// Chinese - Macau SAR
	/// </summary>
	public static readonly LocaleEnum ZHMO = new LocaleEnum("36", "zh-MO");
	/// <summary>
	/// Chinese - China
	/// </summary>
	public static readonly LocaleEnum ZHCN = new LocaleEnum("37", "zh-CN");
	/// <summary>
	/// Chinese (Simplified)
	/// </summary>
	public static readonly LocaleEnum ZHCHS = new LocaleEnum("38", "zh-CHS");
	/// <summary>
	/// Chinese - Singapore
	/// </summary>
	public static readonly LocaleEnum ZHSG = new LocaleEnum("39", "zh-SG");
	/// <summary>
	/// Chinese - Taiwan
	/// </summary>
	public static readonly LocaleEnum ZHTW = new LocaleEnum("40", "zh-TW");
	/// <summary>
	/// Chinese (Traditional)
	/// </summary>
	public static readonly LocaleEnum ZHCHT = new LocaleEnum("41", "zh-CHT");
	/// <summary>
	/// Croatian
	/// </summary>
	public static readonly LocaleEnum HR = new LocaleEnum("42", "hr");
	/// <summary>
	/// Croatian - Croatia
	/// </summary>
	public static readonly LocaleEnum HRHR = new LocaleEnum("43", "hr-HR");
	/// <summary>
	/// Czech
	/// </summary>
	public static readonly LocaleEnum CS = new LocaleEnum("44", "cs");
	/// <summary>
	/// Czech - Czech Republic
	/// </summary>
	public static readonly LocaleEnum CSCZ = new LocaleEnum("45", "cs-CZ");
	/// <summary>
	/// Danish
	/// </summary>
	public static readonly LocaleEnum DA = new LocaleEnum("46", "da");
	/// <summary>
	/// Danish - Denmark
	/// </summary>
	public static readonly LocaleEnum DADK = new LocaleEnum("47", "da-DK");
	/// <summary>
	/// Dhivehi
	/// </summary>
	public static readonly LocaleEnum DIV = new LocaleEnum("48", "div");
	/// <summary>
	/// Dhivehi - Maldives
	/// </summary>
	public static readonly LocaleEnum DIVMV = new LocaleEnum("49", "div-MV");
	/// <summary>
	/// Dutch
	/// </summary>
	public static readonly LocaleEnum NL = new LocaleEnum("50", "nl");
	/// <summary>
	/// Dutch - Belgium
	/// </summary>
	public static readonly LocaleEnum NLBE = new LocaleEnum("51", "nl-BE");
	/// <summary>
	/// Dutch - The Netherlands
	/// </summary>
	public static readonly LocaleEnum NLNL = new LocaleEnum("52", "nl-NL");
	/// <summary>
	/// English
	/// </summary>
	public static readonly LocaleEnum EN = new LocaleEnum("53", "en");
	/// <summary>
	/// English - Australia
	/// </summary>
	public static readonly LocaleEnum ENAU = new LocaleEnum("54", "en-AU");
	/// <summary>
	/// English - Belize
	/// </summary>
	public static readonly LocaleEnum ENBZ = new LocaleEnum("55", "en-BZ");
	/// <summary>
	/// English - Canada
	/// </summary>
	public static readonly LocaleEnum ENCA = new LocaleEnum("56", "en-CA");
	/// <summary>
	/// English - Caribbean
	/// </summary>
	public static readonly LocaleEnum ENCB = new LocaleEnum("57", "en-CB");
	/// <summary>
	/// English - Ireland
	/// </summary>
	public static readonly LocaleEnum ENIE = new LocaleEnum("58", "en-IE");
	/// <summary>
	/// English - Jamaica
	/// </summary>
	public static readonly LocaleEnum ENJM = new LocaleEnum("59", "en-JM");
	/// <summary>
	/// English - New Zealand
	/// </summary>
	public static readonly LocaleEnum ENNZ = new LocaleEnum("60", "en-NZ");
	/// <summary>
	/// English - Philippines
	/// </summary>
	public static readonly LocaleEnum ENPH = new LocaleEnum("61", "en-PH");
	/// <summary>
	/// English - South Africa
	/// </summary>
	public static readonly LocaleEnum ENZA = new LocaleEnum("62", "en-ZA");
	/// <summary>
	/// English - Trinidad and Tobago
	/// </summary>
	public static readonly LocaleEnum ENTT = new LocaleEnum("63", "en-TT");
	/// <summary>
	/// English - United Kingdom
	/// </summary>
	public static readonly LocaleEnum ENGB = new LocaleEnum("64", "en-GB");
	/// <summary>
	/// English - United States
	/// </summary>
	public static readonly LocaleEnum ENUS = new LocaleEnum("65", "en-US");
	/// <summary>
	/// English - Zimbabwe
	/// </summary>
	public static readonly LocaleEnum ENZW = new LocaleEnum("66", "en-ZW");
	/// <summary>
	/// Estonian
	/// </summary>
	public static readonly LocaleEnum ET = new LocaleEnum("67", "et");
	/// <summary>
	/// Estonian - Estonia
	/// </summary>
	public static readonly LocaleEnum ETEE = new LocaleEnum("68", "et-EE");
	/// <summary>
	/// Faroese
	/// </summary>
	public static readonly LocaleEnum FO = new LocaleEnum("69", "fo");
	/// <summary>
	/// Faroese - Faroe Islands
	/// </summary>
	public static readonly LocaleEnum FOFO = new LocaleEnum("70", "fo-FO");
	/// <summary>
	/// Farsi
	/// </summary>
	public static readonly LocaleEnum FA = new LocaleEnum("71", "fa");
	/// <summary>
	/// Farsi - Iran
	/// </summary>
	public static readonly LocaleEnum FAIR = new LocaleEnum("72", "fa-IR");
	/// <summary>
	/// Finnish
	/// </summary>
	public static readonly LocaleEnum FI = new LocaleEnum("73", "fi");
	/// <summary>
	/// Finnish - Finland
	/// </summary>
	public static readonly LocaleEnum FIFI = new LocaleEnum("74", "fi-FI");
	/// <summary>
	/// French
	/// </summary>
	public static readonly LocaleEnum FR = new LocaleEnum("75", "fr");
	/// <summary>
	/// French - Belgium
	/// </summary>
	public static readonly LocaleEnum FRBE = new LocaleEnum("76", "fr-BE");
	/// <summary>
	/// French - Canada
	/// </summary>
	public static readonly LocaleEnum FRCA = new LocaleEnum("77", "fr-CA");
	/// <summary>
	/// French - France
	/// </summary>
	public static readonly LocaleEnum FRFR = new LocaleEnum("78", "fr-FR");
	/// <summary>
	/// French - Luxembourg
	/// </summary>
	public static readonly LocaleEnum FRLU = new LocaleEnum("79", "fr-LU");
	/// <summary>
	/// French - Monaco
	/// </summary>
	public static readonly LocaleEnum FRMC = new LocaleEnum("80", "fr-MC");
	/// <summary>
	/// French - Switzerland
	/// </summary>
	public static readonly LocaleEnum FRCH = new LocaleEnum("81", "fr-CH");
	/// <summary>
	/// Galician
	/// </summary>
	public static readonly LocaleEnum GL = new LocaleEnum("82", "gl");
	/// <summary>
	/// Galician - Galician
	/// </summary>
	public static readonly LocaleEnum GLES = new LocaleEnum("83", "gl-ES");
	/// <summary>
	/// Georgian
	/// </summary>
	public static readonly LocaleEnum KA = new LocaleEnum("84", "ka");
	/// <summary>
	/// Georgian - Georgia
	/// </summary>
	public static readonly LocaleEnum KAGE = new LocaleEnum("85", "ka-GE");
	/// <summary>
	/// German
	/// </summary>
	public static readonly LocaleEnum DE = new LocaleEnum("86", "de");
	/// <summary>
	/// German - Austria
	/// </summary>
	public static readonly LocaleEnum DEAT = new LocaleEnum("87", "de-AT");
	/// <summary>
	/// German - Germany
	/// </summary>
	public static readonly LocaleEnum DEDE = new LocaleEnum("88", "de-DE");
	/// <summary>
	/// German - Liechtenstein
	/// </summary>
	public static readonly LocaleEnum DELI = new LocaleEnum("89", "de-LI");
	/// <summary>
	/// German - Luxembourg
	/// </summary>
	public static readonly LocaleEnum DELU = new LocaleEnum("90", "de-LU");
	/// <summary>
	/// German - Switzerland
	/// </summary>
	public static readonly LocaleEnum DECH = new LocaleEnum("91", "de-CH");
	/// <summary>
	/// Greek
	/// </summary>
	public static readonly LocaleEnum EL = new LocaleEnum("92", "el");
	/// <summary>
	/// Greek - Greece
	/// </summary>
	public static readonly LocaleEnum ELGR = new LocaleEnum("93", "el-GR");
	/// <summary>
	/// Gujarati
	/// </summary>
	public static readonly LocaleEnum GU = new LocaleEnum("94", "gu");
	/// <summary>
	/// Gujarati - India
	/// </summary>
	public static readonly LocaleEnum GUIN = new LocaleEnum("95", "gu-IN");
	/// <summary>
	/// Hebrew
	/// </summary>
	public static readonly LocaleEnum HE = new LocaleEnum("96", "he");
	/// <summary>
	/// Hebrew - Israel
	/// </summary>
	public static readonly LocaleEnum HEIL = new LocaleEnum("97", "he-IL");
	/// <summary>
	/// Hindi
	/// </summary>
	public static readonly LocaleEnum HI = new LocaleEnum("98", "hi");
	/// <summary>
	/// Hindi - India
	/// </summary>
	public static readonly LocaleEnum HIIN = new LocaleEnum("99", "hi-IN");
	/// <summary>
	/// Hungarian
	/// </summary>
	public static readonly LocaleEnum HU = new LocaleEnum("100", "hu");
	/// <summary>
	/// Hungarian - Hungary
	/// </summary>
	public static readonly LocaleEnum HUHU = new LocaleEnum("101", "hu-HU");
	/// <summary>
	/// Icelandic
	/// </summary>
	public static readonly LocaleEnum IS = new LocaleEnum("102", "is");
	/// <summary>
	/// Icelandic - Iceland
	/// </summary>
	public static readonly LocaleEnum ISIS = new LocaleEnum("103", "is-IS");
	/// <summary>
	/// Indonesian
	/// </summary>
	public static readonly LocaleEnum ID = new LocaleEnum("104", "id");
	/// <summary>
	/// Indonesian - Indonesia
	/// </summary>
	public static readonly LocaleEnum IDID = new LocaleEnum("105", "id-ID");
	/// <summary>
	/// Italian
	/// </summary>
	public static readonly LocaleEnum IT = new LocaleEnum("106", "it");
	/// <summary>
	/// Italian - Italy
	/// </summary>
	public static readonly LocaleEnum ITIT = new LocaleEnum("107", "it-IT");
	/// <summary>
	/// Italian - Switzerland
	/// </summary>
	public static readonly LocaleEnum ITCH = new LocaleEnum("108", "it-CH");
	/// <summary>
	/// Japanese
	/// </summary>
	public static readonly LocaleEnum JA = new LocaleEnum("109", "ja");
	/// <summary>
	/// Japanese - Japan
	/// </summary>
	public static readonly LocaleEnum JAJP = new LocaleEnum("110", "ja-JP");
	/// <summary>
	/// Kannada
	/// </summary>
	public static readonly LocaleEnum KN = new LocaleEnum("111", "kn");
	/// <summary>
	/// Kannada - India
	/// </summary>
	public static readonly LocaleEnum KNIN = new LocaleEnum("112", "kn-IN");
	/// <summary>
	/// Kazakh
	/// </summary>
	public static readonly LocaleEnum KK = new LocaleEnum("113", "kk");
	/// <summary>
	/// Kazakh - Kazakhstan
	/// </summary>
	public static readonly LocaleEnum KKKZ = new LocaleEnum("114", "kk-KZ");
	/// <summary>
	/// Konkani
	/// </summary>
	public static readonly LocaleEnum KOK = new LocaleEnum("115", "kok");
	/// <summary>
	/// Konkani - India
	/// </summary>
	public static readonly LocaleEnum KOKIN = new LocaleEnum("116", "kok-IN");
	/// <summary>
	/// Korean
	/// </summary>
	public static readonly LocaleEnum KO = new LocaleEnum("117", "ko");
	/// <summary>
	/// Korean - Korea
	/// </summary>
	public static readonly LocaleEnum KOKR = new LocaleEnum("118", "ko-KR");
	/// <summary>
	/// Kyrgyz
	/// </summary>
	public static readonly LocaleEnum KY = new LocaleEnum("119", "ky");
	/// <summary>
	/// Kyrgyz - Kazakhstan
	/// </summary>
	public static readonly LocaleEnum KYKZ = new LocaleEnum("120", "ky-KZ");
	/// <summary>
	/// Latvian
	/// </summary>
	public static readonly LocaleEnum LV = new LocaleEnum("121", "lv");
	/// <summary>
	/// Latvian - Latvia
	/// </summary>
	public static readonly LocaleEnum LVLV = new LocaleEnum("122", "lv-LV");
	/// <summary>
	/// Lithuanian
	/// </summary>
	public static readonly LocaleEnum LT = new LocaleEnum("123", "lt");
	/// <summary>
	/// Lithuanian - Lithuania
	/// </summary>
	public static readonly LocaleEnum LTLT = new LocaleEnum("124", "lt-LT");
	/// <summary>
	/// Macedonian
	/// </summary>
	public static readonly LocaleEnum MK = new LocaleEnum("125", "mk");
	/// <summary>
	/// Macedonian - FYROM
	/// </summary>
	public static readonly LocaleEnum MKMK = new LocaleEnum("126", "mk-MK");
	/// <summary>
	/// Malay
	/// </summary>
	public static readonly LocaleEnum MS = new LocaleEnum("127", "ms");
	/// <summary>
	/// Malay - Brunei
	/// </summary>
	public static readonly LocaleEnum MSBN = new LocaleEnum("128", "ms-BN");
	/// <summary>
	/// Malay - Malaysia
	/// </summary>
	public static readonly LocaleEnum MSMY = new LocaleEnum("129", "ms-MY");
	/// <summary>
	/// Marathi
	/// </summary>
	public static readonly LocaleEnum MR = new LocaleEnum("130", "mr");
	/// <summary>
	/// Marathi - India
	/// </summary>
	public static readonly LocaleEnum MRIN = new LocaleEnum("131", "mr-IN");
	/// <summary>
	/// Mongolian
	/// </summary>
	public static readonly LocaleEnum MN = new LocaleEnum("132", "mn");
	/// <summary>
	/// Mongolian - Mongolia
	/// </summary>
	public static readonly LocaleEnum MNMN = new LocaleEnum("133", "mn-MN");
	/// <summary>
	/// Norwegian
	/// </summary>
	public static readonly LocaleEnum NO = new LocaleEnum("134", "no");
	/// <summary>
	/// Norwegian (Bokmsl) - Norway
	/// </summary>
	public static readonly LocaleEnum NBNO = new LocaleEnum("135", "nb-NO");
	/// <summary>
	/// Norwegian (Nynorsk) - Norway
	/// </summary>
	public static readonly LocaleEnum NNNO = new LocaleEnum("136", "nn-NO");
	/// <summary>
	/// Polish
	/// </summary>
	public static readonly LocaleEnum PL = new LocaleEnum("137", "pl");
	/// <summary>
	/// Polish - Poland
	/// </summary>
	public static readonly LocaleEnum PLPL = new LocaleEnum("138", "pl-PL");
	/// <summary>
	/// Portuguese
	/// </summary>
	public static readonly LocaleEnum PT = new LocaleEnum("139", "pt");
	/// <summary>
	/// Portuguese - Brazil
	/// </summary>
	public static readonly LocaleEnum PTBR = new LocaleEnum("140", "pt-BR");
	/// <summary>
	/// Portuguese - Portugal
	/// </summary>
	public static readonly LocaleEnum PTPT = new LocaleEnum("141", "pt-PT");
	/// <summary>
	/// Punjabi
	/// </summary>
	public static readonly LocaleEnum PA = new LocaleEnum("142", "pa");
	/// <summary>
	/// Punjabi - India
	/// </summary>
	public static readonly LocaleEnum PAIN = new LocaleEnum("143", "pa-IN");
	/// <summary>
	/// Romanian
	/// </summary>
	public static readonly LocaleEnum RO = new LocaleEnum("144", "ro");
	/// <summary>
	/// Romanian - Romania
	/// </summary>
	public static readonly LocaleEnum RORO = new LocaleEnum("145", "ro-RO");
	/// <summary>
	/// Russian
	/// </summary>
	public static readonly LocaleEnum RU = new LocaleEnum("146", "ru");
	/// <summary>
	/// Russian - Russia
	/// </summary>
	public static readonly LocaleEnum RURU = new LocaleEnum("147", "ru-RU");
	/// <summary>
	/// Sanskrit
	/// </summary>
	public static readonly LocaleEnum SA = new LocaleEnum("148", "sa");
	/// <summary>
	/// Sanskrit - India
	/// </summary>
	public static readonly LocaleEnum SAIN = new LocaleEnum("149", "sa-IN");
	/// <summary>
	/// Serbian (Cyrillic) - Serbia
	/// </summary>
	public static readonly LocaleEnum SRSPCYRL = new LocaleEnum("150", "sr-SP-Cyrl");
	/// <summary>
	/// Serbian (Latin) - Serbia
	/// </summary>
	public static readonly LocaleEnum SRSPLATN = new LocaleEnum("151", "sr-SP-Latn");
	/// <summary>
	/// Slovak
	/// </summary>
	public static readonly LocaleEnum SK = new LocaleEnum("152", "sk");
	/// <summary>
	/// Slovak - Slovakia
	/// </summary>
	public static readonly LocaleEnum SKSK = new LocaleEnum("153", "sk-SK");
	/// <summary>
	/// Slovenian
	/// </summary>
	public static readonly LocaleEnum SL = new LocaleEnum("154", "sl");
	/// <summary>
	/// Slovenian - Slovenia
	/// </summary>
	public static readonly LocaleEnum SLSI = new LocaleEnum("155", "sl-SI");
	/// <summary>
	/// Spanish
	/// </summary>
	public static readonly LocaleEnum ES = new LocaleEnum("156", "es");
	/// <summary>
	/// Spanish - Argentina
	/// </summary>
	public static readonly LocaleEnum ESAR = new LocaleEnum("157", "es-AR");
	/// <summary>
	/// Spanish - Bolivia
	/// </summary>
	public static readonly LocaleEnum ESBO = new LocaleEnum("158", "es-BO");
	/// <summary>
	/// Spanish - Chile
	/// </summary>
	public static readonly LocaleEnum ESCL = new LocaleEnum("159", "es-CL");
	/// <summary>
	/// Spanish - Colombia
	/// </summary>
	public static readonly LocaleEnum ESCO = new LocaleEnum("160", "es-CO");
	/// <summary>
	/// Spanish - Costa Rica
	/// </summary>
	public static readonly LocaleEnum ESCR = new LocaleEnum("161", "es-CR");
	/// <summary>
	/// Spanish - Dominican Republic
	/// </summary>
	public static readonly LocaleEnum ESDO = new LocaleEnum("162", "es-DO");
	/// <summary>
	/// Spanish - Ecuador
	/// </summary>
	public static readonly LocaleEnum ESEC = new LocaleEnum("163", "es-EC");
	/// <summary>
	/// Spanish - El Salvador
	/// </summary>
	public static readonly LocaleEnum ESSV = new LocaleEnum("164", "es-SV");
	/// <summary>
	/// Spanish - Guatemala
	/// </summary>
	public static readonly LocaleEnum ESGT = new LocaleEnum("165", "es-GT");
	/// <summary>
	/// Spanish - Honduras
	/// </summary>
	public static readonly LocaleEnum ESHN = new LocaleEnum("166", "es-HN");
	/// <summary>
	/// Spanish - Mexico
	/// </summary>
	public static readonly LocaleEnum ESMX = new LocaleEnum("167", "es-MX");
	/// <summary>
	/// Spanish - Nicaragua
	/// </summary>
	public static readonly LocaleEnum ESNI = new LocaleEnum("168", "es-NI");
	/// <summary>
	/// Spanish - Panama
	/// </summary>
	public static readonly LocaleEnum ESPA = new LocaleEnum("169", "es-PA");
	/// <summary>
	/// Spanish - Paraguay
	/// </summary>
	public static readonly LocaleEnum ESPY = new LocaleEnum("170", "es-PY");
	/// <summary>
	/// Spanish - Peru
	/// </summary>
	public static readonly LocaleEnum ESPE = new LocaleEnum("171", "es-PE");
	/// <summary>
	/// Spanish - Puerto Rico
	/// </summary>
	public static readonly LocaleEnum ESPR = new LocaleEnum("172", "es-PR");
	/// <summary>
	/// Spanish - Spain
	/// </summary>
	public static readonly LocaleEnum ESES = new LocaleEnum("173", "es-ES");
	/// <summary>
	/// Spanish - Uruguay
	/// </summary>
	public static readonly LocaleEnum ESUY = new LocaleEnum("174", "es-UY");
	/// <summary>
	/// Spanish - Venezuela
	/// </summary>
	public static readonly LocaleEnum ESVE = new LocaleEnum("175", "es-VE");
	/// <summary>
	/// Swahili
	/// </summary>
	public static readonly LocaleEnum SW = new LocaleEnum("176", "sw");
	/// <summary>
	/// Swahili - Kenya
	/// </summary>
	public static readonly LocaleEnum SWKE = new LocaleEnum("177", "sw-KE");
	/// <summary>
	/// Swedish
	/// </summary>
	public static readonly LocaleEnum SV = new LocaleEnum("178", "sv");
	/// <summary>
	/// Swedish - Finland
	/// </summary>
	public static readonly LocaleEnum SVFI = new LocaleEnum("179", "sv-FI");
	/// <summary>
	/// Swedish - Sweden
	/// </summary>
	public static readonly LocaleEnum SVSE = new LocaleEnum("180", "sv-SE");
	/// <summary>
	/// Syriac
	/// </summary>
	public static readonly LocaleEnum SYR = new LocaleEnum("181", "syr");
	/// <summary>
	/// Syriac - Syria
	/// </summary>
	public static readonly LocaleEnum SYRSY = new LocaleEnum("182", "syr-SY");
	/// <summary>
	/// Tamil
	/// </summary>
	public static readonly LocaleEnum TA = new LocaleEnum("183", "ta");
	/// <summary>
	/// Tamil - India
	/// </summary>
	public static readonly LocaleEnum TAIN = new LocaleEnum("184", "ta-IN");
	/// <summary>
	/// Tatar
	/// </summary>
	public static readonly LocaleEnum TT = new LocaleEnum("185", "tt");
	/// <summary>
	/// Tatar - Russia
	/// </summary>
	public static readonly LocaleEnum TTRU = new LocaleEnum("186", "tt-RU");
	/// <summary>
	/// Telugu
	/// </summary>
	public static readonly LocaleEnum TE = new LocaleEnum("187", "te");
	/// <summary>
	/// Telugu - India
	/// </summary>
	public static readonly LocaleEnum TEIN = new LocaleEnum("188", "te-IN");
	/// <summary>
	/// Thai
	/// </summary>
	public static readonly LocaleEnum TH = new LocaleEnum("189", "th");
	/// <summary>
	/// Thai - Thailand
	/// </summary>
	public static readonly LocaleEnum THTH = new LocaleEnum("190", "th-TH");
	/// <summary>
	/// Turkish
	/// </summary>
	public static readonly LocaleEnum TR = new LocaleEnum("191", "tr");
	/// <summary>
	/// Turkish - Turkey
	/// </summary>
	public static readonly LocaleEnum TRTR = new LocaleEnum("192", "tr-TR");
	/// <summary>
	/// Ukrainian
	/// </summary>
	public static readonly LocaleEnum UK = new LocaleEnum("193", "uk");
	/// <summary>
	/// Ukrainian - Ukraine
	/// </summary>
	public static readonly LocaleEnum UKUA = new LocaleEnum("194", "uk-UA");
	/// <summary>
	/// Urdu
	/// </summary>
	public static readonly LocaleEnum UR = new LocaleEnum("195", "ur");
	/// <summary>
	/// Urdu - Pakistan
	/// </summary>
	public static readonly LocaleEnum URPK = new LocaleEnum("196", "ur-PK");
	/// <summary>
	/// Uzbek
	/// </summary>
	public static readonly LocaleEnum UZ = new LocaleEnum("197", "uz");
	/// <summary>
	/// Uzbek (Cyrillic) - Uzbekistan
	/// </summary>
	public static readonly LocaleEnum UZUZCYRL = new LocaleEnum("198", "uz-UZ-Cyrl");
	/// <summary>
	/// Uzbek (Latin) - Uzbekistan
	/// </summary>
	public static readonly LocaleEnum UZUZLATN = new LocaleEnum("199", "uz-UZ-Latn");
	/// <summary>
	/// Vietnamese
	/// </summary>
	public static readonly LocaleEnum VI = new LocaleEnum("200", "vi");
	/// <summary>
	/// Vietnamese - Vietnam
	/// </summary>
	public static readonly LocaleEnum VIVN = new LocaleEnum("201", "vi-VN");

	public static LocaleEnum GetInstance(Object value)
	{
	    if (value is String)
	    {
		foreach (LocaleEnum t in OPTIONS)
		{
		    if (t.Value.Equals(value))
		    {
			return t;
		    }
		}
	    }
	    if (value is Int32)
	    {
		foreach (LocaleEnum t in OPTIONS)
		{
		    try
		    {
			if (Int32.Parse(t.Code).Equals(value))
			{
			    return t;
			}
		    }
		    catch (Exception)
		    {
			// parse exception - continue
		    }
		}
	    }

	    return UNSET;
	}

	private LocaleEnum() {}

	private LocaleEnum(String code, String name)
	{
	    this.code = code;
	    this.name = name;
	    OPTIONS.Add(this);
	}

	public override Boolean IsDefault
	{
	    get
	    {
		return Object.ReferenceEquals(this, DEFAULT);
	    }
	}

	public override Boolean IsUnset
	{
	    get
	    {
		return Object.ReferenceEquals(this, UNSET);
	    }
	}

	public static IList Options
	{
	    get
	    {
		return OPTIONS;
	    }
	}

	/// <summary>
	/// Convert a LocaleEnum instance to an Int32;
	/// </summary>
	/// <returns>the Int32 representation for the enum instance.</returns>
	/// <exception cref="InvalidCastException">when converting DEFAULT or UNSET to an Int32.</exception>
	public Int32 ToInt32()
	{
	    if (IsValid)
	    {
		try
		{
		    return Int32.Parse(code);
		}
		catch (Exception)
		{
		    // parse error  - don't do anything - an acceptable exception will be thrown below
		}
	    }

	    // instance was !IsValid or there was a parser error
	    throw new InvalidCastException();
	}
    }
}
