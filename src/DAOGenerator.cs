using System;
using System.Data;
using System.IO;
using System.Collections;
using System.Text;

namespace Spring2.DataTierGenerator {
    /// <summary>
    /// Generates stored procedures and associated data access code for the specified database.
    /// </summary>
    public class DAOGenerator : GeneratorBase {
	private Entity entity;
	/// <summary>
	/// Contructor for the Generator class.
	/// </summary>
	/// <param name="strConnectionString">Connecion string to a SQL Server database.</param>
	public DAOGenerator(Configuration options, Entity entity) : base(options) {
	    this.entity = entity;
	}
		
	/// <summary>
	/// Creates a C# data access class for all of the table's stored procedures.
	/// </summary>
	/// <param name="table">Name of the table the class should be generated for.</param>
	/// <param name="fields">ArrayList object containing one or more Field objects as defined in the table.</param>
	public void CreateDataAccessClass() {

	    StringBuilder sb = new StringBuilder(4096);

	    // Create the header for the class.
	    sb.Append(GetUsingNamespaces(entity.Fields, true));
	    sb.Append(Environment.NewLine);
	    sb.Append("namespace " + options.GetDAONameSpace(entity.Name) + " {\n");
	    sb.Append("    public class " + options.GetDAOClassName(entity.Name));

	    if (options.DataObjectBaseClass.Length>0) {
		sb.Append(" : ").Append(options.DaoBaseClass);
	    }
	    sb.Append(" {\n\n");

	    sb.Append("\n        private static readonly String VIEW = \"").Append(entity.SqlView).Append("\";\n\n");

	    CreateDAOListMethods(sb);

	    // Append the access methods.
	    CreateInsertMethod(sb);
	    if (entity.HasUpdatableFields()) {
		sb.Append("\n\n");
		CreateUpdateMethod(sb);
	    }
	    sb.Append("\n\n");
	    CreateDeleteMethods(sb);

	    if (options.GenerateSelectStoredProcs) {
		sb.Append("\n\n");
		CreateSelectMethods(sb);
	    }

	    if (entity.Finders.Count>0) {
		sb.Append("\n\n");
		sb.Append(CreateFinderMethods());
	    }
		
	    // Close out the class and namespace.
	    sb.Append("    }\n");
	    sb.Append("}\n");

	    FileInfo file = new FileInfo(options.RootDirectory + options.DaoClassDirectory + "\\" + options.GetDAOClassName(entity.Name) + ".cs");
	    WriteToFile(file, sb.ToString(), false);
	}

		
	/// <summary>
	/// Creates a string that represents the insert functionality of the data access class.
	/// </summary>
	/// <param name="table">Name of the table the data access class is for.</param>
	/// <param name="fields">ArrayList object containing one or more Field objects as defined in the table.</param>
	/// <param name="sb">StreamBuilder object that the resulting string should be appended to.</param>
	private void CreateInsertMethod(StringBuilder sb) {
			
	    // Append the method header.
	    sb.Append("        /// <summary>\n");
	    sb.Append("        /// Inserts a record into the " + entity.SqlObject + " table.\n");
	    sb.Append("        /// </summary>\n");
	    sb.Append("        /// <param name=\"\"></param>\n");
			
	    // Determine the return type of the insert function.
	    sb.Append("        public static ");
	    Field idField = entity.GetIdentityColumn();

	    // this is a hack becuase the method above returns a new Field instead of null - fix the method above and remove this code
	    if (idField != null && idField.Name.Equals(String.Empty)) {
		idField = null;
	    }
	    if (idField != null) {
		sb.Append(idField.Type.Name);
	    } else {
		sb.Append("void");
	    }

	    sb.Append(" Insert(");

	    // Append the method call parameters - data object.
	    sb.Append(entity.Name).Append("Data data").Append(") {\n");
			
	    // Append the variable declarations.
	    sb.Append("            SqlCommand cmd;\n");
	    sb.Append("\n");

	    sb.Append(GetCreateCommandSection(options.GetProcName(entity.SqlObject, "Insert")));
			
	    if (idField != null) {
		sb.Append("                SqlParameter rv = cmd.Parameters.Add(\"RETURN_VALUE\", SqlDbType.Int);\n");
		sb.Append("                rv.Direction = ParameterDirection.ReturnValue;\n");
	    }

	    // Append the parameter appends  ;)
	    sb.Append("                //Create the parameters and append them to the command object\n");
	    foreach (Field field in entity.Fields) {
		if (!field.IsViewColumn && field.SqlName.Length>0) {
		    if (field.IsIdentity || field.IsRowGuidCol) {
			//sb.Append("                " + field.CreateSqlParameter(true, true));
		    } else {
			sb.Append("                " + field.CreateSqlParameter(false, true));
		    }
		}
	    }
	    sb.Append("\n");

	    // Append the execute statement
	    sb.Append("                // Execute the query\n");
	    sb.Append("                cmd.ExecuteNonQuery();\n");
			
	    // Append the parameter value extraction
	    if (idField != null) {
		sb.Append("\n                // Set the output paramter value(s)\n");
		sb.Append("                return ");
		String readerMethod = "cmd.Parameters[\"RETURN_VALUE\"].Value";
		if (idField.Type.ConvertFromSqlTypeFormat.Length >0) {
		    sb.Append(String.Format(idField.Type.ConvertFromSqlTypeFormat, "", "", readerMethod, "", ""));
		} else {
		    sb.Append(readerMethod);
		}
		sb.Append(";\n");
	    }
			
	    // Append the method footer
	    sb.Append("        }\n");
	}


	/// <summary>
	/// Creates a string that represents the update functionality of the data access class.
	/// </summary>
	/// <param name="table">Name of the table the data access class is for.</param>
	/// <param name="fields">ArrayList object containing one or more Field objects as defined in the table.</param>
	/// <param name="sb">StreamBuilder object that the resulting string should be appended to.</param>
	private void CreateUpdateMethod(StringBuilder sb) {
			
	    // Append the method header
	    sb.Append("        /// <summary>\n");
	    sb.Append("        /// Updates a record in the " + entity.SqlObject + " table.\n");
	    sb.Append("        /// </summary>\n");
	    sb.Append("        /// <param name=\"\"></param>\n");
	    sb.Append("        public static void Update(");
			
	    // Append the method call parameters - data object
	    sb.Append(entity.Name).Append("Data data");

	    // Append the method header
	    sb.Append(") {\n");
			
	    // Append the variable declarations
	    sb.Append("            SqlCommand cmd;\n");
	    sb.Append("\n");

	    sb.Append(GetCreateCommandSection(options.GetProcName(entity.SqlObject, "Update")));
			
	    // Append the parameter appends  ;)
	    sb.Append("                //Create the parameters and append them to the command object\n");
	    for (int i = 0; i < entity.Fields.Count; i++) {
		Field field = (Field)entity.Fields[i];
		if (!field.IsViewColumn && field.SqlName.Length>0) {
		    sb.Append("                " + field.CreateSqlParameter(false, true));
		}
	    }
	    sb.Append("\n");
			
	    // Append the execute statement
	    sb.Append("                // Execute the query\n");
	    sb.Append("                cmd.ExecuteNonQuery();\n");
						
	    // Append the method footer
	    sb.Append("        }\n");
	}


	/// <summary>
	/// Creates a string that represents the delete functionality of the data access class.
	/// </summary>
	/// <param name="table">Name of the table the data access class is for.</param>
	/// <param name="fields">ArrayList object containing one or more Field objects as defined in the table.</param>
	/// <param name="sb">StreamBuilder object that the resulting string should be appended to.</param>
	private void CreateDeleteMethods(StringBuilder sb) {
			
	    // Create the array list of key fields
	    String primaryKeyList = String.Empty;
	    ArrayList keyList = new ArrayList();

	    for (int i = 0; i < entity.Fields.Count; i++) {
		Field field = (Field)entity.Fields[i];
		if (field.IsPrimaryKey) {
		    keyList.Add(field);
		    primaryKeyList += field.Name.Replace(" ", "_") + "_";
		}
	    }
			
	    // Trim off the last underscore
	    if (primaryKeyList.Length > 0)
		primaryKeyList = primaryKeyList.Substring(0, primaryKeyList.Length - 1);

	    /*********************************************************************************************************/
	    // Create the remaining select functions based on identity columns or uniqueidentifiers
	    for (int i = 0; i < entity.Fields.Count; i++) {
		Field field = (Field)entity.Fields[i];
			
		if (field.IsIdentity || (options.GenerateProcsForForeignKey && (field.IsRowGuidCol || field.IsPrimaryKey || field.IsForeignKey))) {
		    String columnName = field.Name.Substring(0, 1).ToUpper() + field.Name.Substring(1);
					
		    String methodName = "Delete" + columnName.Replace(" ", "_");
		    // if this option is on, only generate the PK 
		    if (options.GenerateOnlyPrimaryDeleteStoredProc) {
			keyList.Clear();
			methodName = "Delete";
		    }

		    // Append the method header
		    sb.Append("        /// <summary>\n");
		    sb.Append("        /// Deletes a record from the " + entity.SqlObject + " table by " + field.Name + ".\n");
		    sb.Append("        /// </summary>\n");
		    sb.Append("        /// <param name=\"\"></param>\n");
		    sb.Append("        public static void " + methodName + "(" + field.CreateMethodParameter() + ") {\n");
					
		    sb.Append("            SqlCommand cmd;\n");
		    sb.Append("\n");

		    sb.Append(GetCreateCommandSection(options.GetProcName(entity.SqlObject, methodName)));

		    // Append the parameters
		    sb.Append("                // Create and append the parameters\n");
		    sb.Append("                " + field.CreateSqlParameter(false, false));
		    sb.Append("\n");

		    // Append the execute statement
		    sb.Append("                // Execute the query and return the result\n");
		    sb.Append("                cmd.ExecuteNonQuery();\n");
					
		    // Append the method footer
		    if (keyList.Count > 0) {
			sb.Append("        }\n\n\n");
		    } else {
			sb.Append("        }\n\n");
		    }
		}
	    }

	    /*********************************************************************************************************/
	    // Create the select functions based on a composite primary key
	    if (keyList.Count > 1) {
		// Append the method header
		sb.Append("        /// <summary>\n");
		sb.Append("        /// Deletes a record from the " + entity.SqlObject + " table by a composite primary key.\n");
		sb.Append("        /// </summary>\n");
		sb.Append("        /// <param name=\"\"></param>\n");

		String methodName = "";
		if (options.GenerateOnlyPrimaryDeleteStoredProc) {
		    methodName = "Delete";
		} else {
		    methodName = "DeleteBy" + primaryKeyList;
		}
				
		sb.Append("        public static void " + methodName + "(");

		String parms = String.Empty;
		foreach (Field field in keyList) {
		    if (parms.Length>0) {
			parms += ", ";
		    }
		    parms += field.CreateMethodParameter();
		}
		sb.Append(parms).Append(") {\n");
				
		sb.Append("            SqlCommand cmd;\n");
		sb.Append("\n");

		sb.Append(GetCreateCommandSection(options.GetProcName(entity.SqlObject, methodName)));

		// Append the parameters
		sb.Append("                // Create and append the parameters\n");
		for (int i = 0; i < keyList.Count; i++) {
		    Field field = (Field)keyList[i];
		    sb.Append("                " + field.CreateSqlParameter(false, false));
		}
		sb.Append("\n");

		// Append the execute statement
		sb.Append("                // Execute the query and return the result\n");
		sb.Append("                cmd.ExecuteNonQuery();\n");
				
		// Append the method footer
		sb.Append("        }\n\n\n");
	    }
	}


	/// <summary>
	/// Creates a string that represents the select functionality of the data access class.
	/// </summary>
	/// <param name="table">Name of the table the data access class is for.</param>
	/// <param name="fields">ArrayList object containing one or more Field objects as defined in the table.</param>
	/// <param name="sb">StreamBuilder object that the resulting string should be appended to.</param>
	private void CreateSelectMethods(StringBuilder sb) {
	    Field	field;
	    int			intIndex;
	    string		strColumnName;
	    string		strPrimaryKeyList;
	    ArrayList	arrKeyList;
			
	    // Create the array list of key fields
	    strPrimaryKeyList = "";
	    arrKeyList = new ArrayList();
	    for (intIndex = 0; intIndex < entity.Fields.Count; intIndex++) {
		field = (Field)entity.Fields[intIndex];
		if (field.IsPrimaryKey) {
		    arrKeyList.Add(field);
		    strPrimaryKeyList += field.Name.Replace(" ", "_") + "_";
		}
		field = null;
	    }
			
	    // Trim off the last underscore
	    if (strPrimaryKeyList.Length > 0)
		strPrimaryKeyList = strPrimaryKeyList.Substring(0, strPrimaryKeyList.Length - 1);

	    /*********************************************************************************************************/
	    // Create the initial "select all" function
			
	    // Append the method header
	    sb.Append("        /// <summary>\n");
	    sb.Append("        /// Selects a record from the " + entity.SqlObject + " table.\n");
	    sb.Append("        /// </summary>\n");
	    sb.Append("        /// <param name=\"\"></param>\n");
	    sb.Append("        public static SqlDataReader Select() {\n");
			
	    // Append the variable declarations
	    //            sb.Append("            SqlConnection	objConnection;\n");
	    sb.Append("            SqlCommand cmd;\n");
	    sb.Append("\n");

	    // Append the try block
	    sb.Append("            try {\n");

	    sb.Append(GetCreateCommandSection(options.GetProcName(entity.SqlObject, "Select")));

	    // Append the execute statement
	    sb.Append("                // Execute the query and return the result\n");
	    sb.Append("                return cmd.ExecuteReader(CommandBehavior.CloseConnection);\n");
			
	    // Append the catch block
	    sb.Append("            } catch (Exception objException) {\n");
	    sb.Append("                throw (new Exception(\"" + entity.Name.Replace(" ", "_") + ".Select\\n\\n\" + objException.Message));\n");
	    sb.Append("            }\n");
			
	    // Append the method footer
	    sb.Append("        }\n\n\n");

	    /*********************************************************************************************************/
	    // Create the remaining select functions based on identity columns or uniqueidentifiers
	    for (intIndex = 0; intIndex < entity.Fields.Count; intIndex++) {
		field = (Field)entity.Fields[intIndex];
			
		if (field.IsIdentity || field.IsRowGuidCol || field.IsPrimaryKey || field.IsForeignKey) {
		    strColumnName = field.Name.Substring(0, 1).ToUpper() + field.Name.Substring(1);
				
		    // Append the method header
		    sb.Append("        /// <summary>\n");
		    sb.Append("        /// Selects a record from the " + entity.Name + " table by " + field.Name + ".\n");
		    sb.Append("        /// </summary>\n");
		    sb.Append("        /// <param name=\"\"></param>\n");
		    sb.Append("        public static SqlDataReader SelectBy" + strColumnName.Replace(" ", "_") + "(" + field.CreateMethodParameter() + ") {\n");
					
		    // Append the variable declarations
		    //                    sb.Append("            SqlConnection	objConnection;\n");
		    sb.Append("            SqlCommand cmd;\n");
		    sb.Append("\n");

		    // Append the try block
		    sb.Append("            try {\n");

		    sb.Append(GetCreateCommandSection(options.GetProcName(entity.Name, options.GetProcName(entity.Name, "SelectBy" + strColumnName.Replace(" ", "_")))));

		    // Append the parameters
		    sb.Append("                // Create and append the parameters\n");
		    sb.Append("                " + field.CreateSqlParameter(false, false));
		    sb.Append("\n");

		    // Append the execute statement
		    sb.Append("                // Execute the query and return the result\n");
		    sb.Append("                return cmd.ExecuteReader(CommandBehavior.CloseConnection);\n");
					
		    // Append the catch block
		    sb.Append("            } catch (Exception objException) {\n");
		    sb.Append("                throw (new Exception(\"" + entity.Name.Replace(" ", "_") + ".SelectBy" + strColumnName.Replace(" ", "_") + "\\n\\n\" + objException.Message));\n");
		    sb.Append("            }\n");
					
		    // Append the method footer
		    if (arrKeyList.Count > 0)
			sb.Append("        }\n\n\n");
		    else
			sb.Append("        }\n\n");
				
		    field = null;
		}
	    }

	    /*********************************************************************************************************/
	    // Create the select functions based on a composite primary key
	    if (arrKeyList.Count > 1) {
		// Append the method header
		sb.Append("        /// <summary>\n");
		sb.Append("        /// Selects a record from the " + entity.Name + " table by a composite primary key.\n");
		sb.Append("        /// </summary>\n");
		sb.Append("        /// <param name=\"\"></param>\n");
				
		sb.Append("        public static SqlDataReader SelectBy" + strPrimaryKeyList + "(");
		for (intIndex = 0; intIndex < arrKeyList.Count; intIndex++) {
		    field = (Field)arrKeyList[intIndex];
		    sb.Append(field.CreateMethodParameter() + ", ");
		}
		sb.Append("SqlConnection connection) {\n");
				
		// Append the variable declarations
		//                sb.Append("            SqlConnection	objConnection;\n");
		sb.Append("            SqlCommand cmd;\n");
		sb.Append("\n");

		// Append the try block
		sb.Append("            try {\n");

		sb.Append(GetCreateCommandSection(options.GetProcName(entity.Name, options.GetProcName(entity.Name, "SelectBy" + strPrimaryKeyList))));

		// Append the parameters
		sb.Append("                // Create and append the parameters\n");
		for (intIndex = 0; intIndex < arrKeyList.Count; intIndex++) {
		    field = (Field)arrKeyList[intIndex];
		    sb.Append("                " + field.CreateSqlParameter(false, false));
		    field = null;
		}
		sb.Append("\n");

		// Append the execute statement
		sb.Append("                // Execute the query and return the result\n");
		sb.Append("                return cmd.ExecuteReader(CommandBehavior.CloseConnection);\n");
				
		// Append the catch block
		sb.Append("            } catch (Exception objException) {\n");
		sb.Append("                throw (new Exception(\"" + entity.Name.Replace(" ", "_") + ".SelectBy" + strPrimaryKeyList + "\\n\\n\" + objException.Message));\n");
		sb.Append("            }\n");
				
		// Append the method footer
		sb.Append("        }\n\n\n");
			
		field = null;
	    }
	}
		
	private void CreateDAOListMethods(StringBuilder sb) {
			
	    // GetList - no parms
	    sb.Append("        public static IList GetList() { \n");
	    sb.Append("            return GetList(null, null);\n");
	    sb.Append("        }\n");
	    sb.Append("\n");

	    // GetList - where
	    sb.Append("        public static IList GetList(IWhere whereClause) { \n");
	    sb.Append("            return GetList(whereClause, null);\n");
	    sb.Append("        }\n");
	    sb.Append("\n");

	    // GetList - order by
	    sb.Append("        public static IList GetList(IOrderBy orderByClause) { \n");
	    sb.Append("            return GetList(null, orderByClause);\n");
	    sb.Append("        }\n");
	    sb.Append("\n");

	    // GetList - both
	    sb.Append("        public static IList GetList(IWhere whereClause, IOrderBy orderByClause) { \n");
	    sb.Append("        	SqlDataReader dataReader = GetListReader(VIEW, whereClause, orderByClause); \n");
	    sb.Append("        	 \n");
	    sb.Append("        	ArrayList list = new ArrayList(); \n");
	    sb.Append("        	while (dataReader.Read()) { \n");
	    sb.Append("        		list.Add(GetDataObjectFromReader(dataReader)); \n");
	    sb.Append("        	} \n");
	    sb.Append("        	dataReader.Close(); \n");
	    sb.Append("        	return list; \n");
	    sb.Append("        } \n");
	    sb.Append("\n");

	    // Load
	    IList keys = entity.GetPrimaryKeyColumns();
	    String parms = "";
	    foreach (Field field in keys) {
		if (!parms.Equals(String.Empty)) {
		    parms += ", ";
		}
		parms += field.CreateMethodParameter();
	    }
	    sb.Append("        public static ").Append(options.GetDOClassName(entity.Name)).Append(" Load(").Append(parms).Append(") {\n");
	    sb.Append("        	WhereClause w = new WhereClause();\n");
	    foreach (Field field in keys) {
		sb.Append("        	w.And(\"").Append(field.SqlName).Append("\", ").Append(String.Format(field.Type.ConvertToSqlTypeFormat, "", field.GetFieldFormat(), "", "", field.GetFieldFormat())).Append(");\n");
	    }
	    sb.Append("        	SqlDataReader dataReader = GetListReader(VIEW, w, null);\n");
	    sb.Append("            \n");
	    sb.Append("        	dataReader.Read();\n");
	    sb.Append("                ").Append(options.GetDOClassName(entity.Name)).Append(" data = GetDataObjectFromReader(dataReader);\n");
	    sb.Append("                dataReader.Close();\n");
	    sb.Append("        	return data;\n");
	    sb.Append("        }\n");
	    sb.Append("\n");			

	    // GetDataObjectFromReader
	    sb.Append("        private static ").Append(options.GetDOClassName(entity.Name)).Append(" GetDataObjectFromReader(SqlDataReader dataReader) {\n");
	    sb.Append("        	").Append(options.GetDOClassName(entity.Name)).Append(" data = new ").Append(options.GetDOClassName(entity.Name)).Append("();\n");

	    foreach (Field field in entity.Fields) {
		if (field.SqlType.Name.Length>0) {
		    sb.Append("        if (dataReader.IsDBNull(dataReader.GetOrdinal(\"").Append(field.SqlName).Append("\"))) { \n");
		    sb.Append("            data.").Append(field.GetMethodFormat()).Append(" = ").Append(field.Type.NullInstanceFormat).Append(";\n"); 
		    sb.Append("        } else {\n");

		    sb.Append("            data.").Append(field.GetMethodFormat()).Append(" = "); 
		    String readerMethod = String.Format(field.SqlType.ReaderMethodFormat, "dataReader", field.SqlName);
		    if (field.Type.ConvertFromSqlTypeFormat.Length >0) {
			sb.Append(String.Format(field.Type.ConvertFromSqlTypeFormat, "data", field.GetMethodFormat(), readerMethod, "dataReader", field.SqlName));
		    } else {
			sb.Append(readerMethod);
		    }
		    sb.Append(";\n");
		    sb.Append("        }\n");

		}
	    }
	    sb.Append("        \n");
	    sb.Append("        	return data;\n");
	    sb.Append("        }\n");
	    sb.Append("\n");			
	}


	private String GetCreateCommandSection(String procName) {
	    StringBuilder sb = new StringBuilder();

	    sb.Append("                // Create and execute the command\n");
	    sb.Append("                cmd = GetSqlCommand(\"" + procName + "\", CommandType.StoredProcedure);\n");
	    sb.Append("\n");
	    return sb.ToString();
	}


	private String CreateFinderMethods() {
	    StringBuilder sb = new StringBuilder();

	    foreach (Finder finder in entity.Finders) {

		String parms = "";
		foreach (Field field in finder.Fields) {
		    if (!parms.Equals(String.Empty)) {
			parms += ", ";
		    }
		    parms += field.CreateMethodParameter();
		}
		sb.Append("        public static ");
		if (finder.Unique) {
		    sb.Append(options.GetDOClassName(entity.Name));
		} else {
		    sb.Append("IList");
		}
		sb.Append(" ").Append(finder.Name).Append("(").Append(parms).Append(") {\n");
		sb.Append("        	WhereClause filter = new WhereClause();\n");
		sb.Append("        	OrderByClause sort = new OrderByClause(\"").Append(finder.Sort).Append("\");\n");
		foreach (Field field in finder.Fields) {
		    sb.Append("        	filter.And(\"").Append(field.SqlName).Append("\", ").Append(String.Format(field.Type.ConvertToSqlTypeFormat, "", field.GetFieldFormat(), "", "", field.GetFieldFormat())).Append(");\n");
		}

		if (!finder.Unique) {
		    sb.Append("            \n");
		    sb.Append("                return GetList(filter, sort);\n");
    		} else {
		    sb.Append("        	SqlDataReader dataReader = GetListReader(VIEW, filter, sort);\n");
		    sb.Append("            \n");
		    sb.Append("        	dataReader.Read();\n");
		    sb.Append("                ").Append(options.GetDOClassName(entity.Name)).Append(" data = GetDataObjectFromReader(dataReader);\n");
		    sb.Append("                dataReader.Close();\n");
		    sb.Append("        	return data;\n");
		}
		sb.Append("        }\n");
		sb.Append("\n");			

	    }


	    return sb.ToString();
	}
    }
}

