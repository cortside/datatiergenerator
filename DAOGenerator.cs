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

	/// <summary>
	/// Contructor for the Generator class.
	/// </summary>
	/// <param name="strConnectionString">Connecion string to a SQL Server database.</param>
	public DAOGenerator(Configuration options, Entity entity) : base(options, entity) {
	}
		

	/// <summary>
	/// Creates a C# data access class for all of the table's stored procedures.
	/// </summary>
	/// <param name="table">Name of the table the class should be generated for.</param>
	/// <param name="fields">ArrayList object containing one or more Field objects as defined in the table.</param>
	public void CreateDataAccessClass() {

	    StringBuilder sb = new StringBuilder(4096);

	    // Create the header for the class.
	    sb.Append(GetUsingNamespaces(true));
	    sb.Append("\n");
	    sb.Append("namespace " + options.GetDAONameSpace(entity.Name) + " {\n");
	    sb.Append("\tpublic class " + options.GetDAOClassName(entity.Name));
	    if (options.DataObjectBaseClass.Length>0) {
		sb.Append(" : ").Append(options.DaoBaseClass);
	    }
	    sb.Append(" {\n\n");

	    sb.Append("\n\t\tprivate static readonly String VIEW = \"vw").Append(entity.Name).Append("\";\n\n");

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
		
	    // Close out the class and namespace.
	    sb.Append("\t}\n");
	    sb.Append("}\n");

	    // Create the output stream
	    String fileName = options.RootDirectory + options.DaoClassDirectory + "\\" + options.GetDAOClassName(entity.Name) + ".cs";
	    if (File.Exists(fileName)) {
		File.Delete(fileName);
	    }

	    StreamWriter writer = new StreamWriter(fileName);
	    writer.Write(sb.ToString());
	    writer.Close();
	}

		
	/// <summary>
	/// Creates a string that represents the insert functionality of the data access class.
	/// </summary>
	/// <param name="table">Name of the table the data access class is for.</param>
	/// <param name="fields">ArrayList object containing one or more Field objects as defined in the table.</param>
	/// <param name="sb">StreamBuilder object that the resulting string should be appended to.</param>
	private void CreateInsertMethod(StringBuilder sb) {
			
	    // Append the method header.
	    sb.Append("\t\t/// <summary>\n");
	    sb.Append("\t\t/// Inserts a record into the " + entity.Name + " table.\n");
	    sb.Append("\t\t/// </summary>\n");
	    sb.Append("\t\t/// <param name=\"\"></param>\n");
			
	    // Determine the return type of the insert function.
	    sb.Append("\t\tpublic static ");
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
	    sb.Append("\t\t\tSqlCommand cmd;\n");
	    sb.Append("\n");

	    sb.Append(GetCreateCommandSection(options.GetProcName(entity.Name, "Insert")));
			
	    if (idField != null) {
		sb.Append("\t\t\t\tSqlParameter rv = cmd.Parameters.Add(\"RETURN_VALUE\", SqlDbType.Int);\n");
		sb.Append("\t\t\t\trv.Direction = ParameterDirection.ReturnValue;\n");
	    }

	    // Append the parameter appends  ;)
	    sb.Append("\t\t\t\t//Create the parameters and append them to the command object\n");
	    foreach (Field field in entity.Fields) {
		if (!field.IsViewColumn && field.SqlName.Length>0) {
		    if (field.IsIdentity || field.IsRowGuidCol) {
			//sb.Append("\t\t\t\t" + field.CreateSqlParameter(true, true));
		    } else {
			sb.Append("\t\t\t\t" + field.CreateSqlParameter(false, true));
		    }
		}
	    }
	    sb.Append("\n");

	    // Append the execute statement
	    sb.Append("\t\t\t\t// Execute the query\n");
	    sb.Append("\t\t\t\tcmd.ExecuteNonQuery();\n");
			
	    // Append the parameter value extraction
	    if (idField != null) {
		sb.Append("\n\t\t\t\t// Set the output paramter value(s)\n");
		sb.Append("\t\t\t\treturn ");
		String readerMethod = "cmd.Parameters[\"RETURN_VALUE\"].Value";
		if (idField.Type.ConvertFromSqlTypeFormat.Length >0) {
		    sb.Append(String.Format(idField.Type.ConvertFromSqlTypeFormat, "", "", readerMethod, "", ""));
		} else {
		    sb.Append(readerMethod);
		}
		sb.Append(";\n");
	    }
			
	    // Append the method footer
	    sb.Append("\t\t}\n");
	}


	/// <summary>
	/// Creates a string that represents the update functionality of the data access class.
	/// </summary>
	/// <param name="table">Name of the table the data access class is for.</param>
	/// <param name="fields">ArrayList object containing one or more Field objects as defined in the table.</param>
	/// <param name="sb">StreamBuilder object that the resulting string should be appended to.</param>
	private void CreateUpdateMethod(StringBuilder sb) {
			
	    // Append the method header
	    sb.Append("\t\t/// <summary>\n");
	    sb.Append("\t\t/// Updates a record in the " + entity.Name + " table.\n");
	    sb.Append("\t\t/// </summary>\n");
	    sb.Append("\t\t/// <param name=\"\"></param>\n");
	    sb.Append("\t\tpublic static void Update(");
			
	    // Append the method call parameters - data object
	    sb.Append(entity.Name).Append("Data data");

	    // Append the method header
	    sb.Append(") {\n");
			
	    // Append the variable declarations
	    sb.Append("\t\t\tSqlCommand cmd;\n");
	    sb.Append("\n");

	    sb.Append(GetCreateCommandSection(options.GetProcName(entity.Name, "Update")));
			
	    // Append the parameter appends  ;)
	    sb.Append("\t\t\t\t//Create the parameters and append them to the command object\n");
	    for (int i = 0; i < entity.Fields.Count; i++) {
		Field field = (Field)entity.Fields[i];
		if (!field.IsViewColumn && field.SqlName.Length>0) {
		    sb.Append("\t\t\t\t" + field.CreateSqlParameter(false, true));
		}
	    }
	    sb.Append("\n");
			
	    // Append the execute statement
	    sb.Append("\t\t\t\t// Execute the query\n");
	    sb.Append("\t\t\t\tcmd.ExecuteNonQuery();\n");
						
	    // Append the method footer
	    sb.Append("\t\t}\n");
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
		    sb.Append("\t\t/// <summary>\n");
		    sb.Append("\t\t/// Deletes a record from the " + entity.Name + " table by " + field.Name + ".\n");
		    sb.Append("\t\t/// </summary>\n");
		    sb.Append("\t\t/// <param name=\"\"></param>\n");
		    //sb.Append("\t\tpublic void DeleteBy" + strColumnName.Replace(" ", "_") + "(" + field.CreateMethodParameter() + ", SqlConnection connection) {\n");
		    sb.Append("\t\tpublic static void " + methodName + "(" + field.CreateMethodParameter() + ") {\n");
					
		    // Append the variable declarations
		    //                    sb.Append("\t\t\tSqlConnection	objConnection;\n");
		    sb.Append("\t\t\tSqlCommand cmd;\n");
		    sb.Append("\n");

		    // Append the try block
		    //                    sb.Append("\t\t\ttry {\n");

		    sb.Append(GetCreateCommandSection(options.GetProcName(entity.Name, methodName)));

		    // Append the parameters
		    sb.Append("\t\t\t\t// Create and append the parameters\n");
		    sb.Append("\t\t\t\t" + field.CreateSqlParameter(false, false));
		    sb.Append("\n");

		    // Append the execute statement
		    sb.Append("\t\t\t\t// Execute the query and return the result\n");
		    sb.Append("\t\t\t\tcmd.ExecuteNonQuery();\n");
					
		    // Append the catch block
		    //                    sb.Append("\t\t\t} catch (Exception objException) {\n");
		    //                    sb.Append("\t\t\t\tthrow (new Exception(\"" + table.Replace(" ", "_") + "." + methodName + "\\n\\n\" + objException.Message));\n");
		    //                    sb.Append("\t\t\t}\n");
					
		    // Append the method footer
		    if (keyList.Count > 0) {
			sb.Append("\t\t}\n\n\n");
		    } else {
			sb.Append("\t\t}\n\n");
		    }
		}
	    }

	    /*********************************************************************************************************/
	    // Create the select functions based on a composite primary key
	    if (keyList.Count > 1) {
		// Append the method header
		sb.Append("\t\t/// <summary>\n");
		sb.Append("\t\t/// Deletes a record from the " + entity.Name + " table by a composite primary key.\n");
		sb.Append("\t\t/// </summary>\n");
		sb.Append("\t\t/// <param name=\"\"></param>\n");

		String methodName = "";
		if (options.GenerateOnlyPrimaryDeleteStoredProc) {
		    methodName = "Delete";
		} else {
		    methodName = "DeleteBy" + primaryKeyList;
		}
				
		sb.Append("\t\tpublic static void " + methodName + "(");
		for (int i = 0; i < keyList.Count; i++) {
		    Field field = (Field)keyList[i];
		    sb.Append(field.CreateMethodParameter() + ", ");
		}
		sb.Append("SqlConnection connection) {\n");
				
		// Append the variable declarations
		//                sb.Append("\t\t\tSqlConnection	objConnection;\n");
		sb.Append("\t\t\tSqlCommand cmd;\n");
		sb.Append("\n");

		// Append the try block
		//                sb.Append("\t\t\ttry {\n");

		sb.Append(GetCreateCommandSection(options.GetProcName(entity.Name, methodName)));

		// Append the parameters
		sb.Append("\t\t\t\t// Create and append the parameters\n");
		for (int i = 0; i < keyList.Count; i++) {
		    Field field = (Field)keyList[i];
		    sb.Append("\t\t\t\t" + field.CreateSqlParameter(false, false));
		}
		sb.Append("\n");

		// Append the execute statement
		sb.Append("\t\t\t\t// Execute the query and return the result\n");
		sb.Append("\t\t\t\tcmd.ExecuteNonQuery();\n");
				
		// Append the catch block
		//                sb.Append("\t\t\t} catch (Exception objException) {\n");
		//                sb.Append("\t\t\t\tthrow (new Exception(\"" + entity.Name.Replace(" ", "_") + "." + methodName + "\\n\\n\" + objException.Message));\n");
		//                sb.Append("\t\t\t}\n");
				
		// Append the method footer
		sb.Append("\t\t}\n\n\n");
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
	    sb.Append("\t\t/// <summary>\n");
	    sb.Append("\t\t/// Selects a record from the " + entity.Name + " table.\n");
	    sb.Append("\t\t/// </summary>\n");
	    sb.Append("\t\t/// <param name=\"\"></param>\n");
	    sb.Append("\t\tpublic static SqlDataReader Select() {\n");
			
	    // Append the variable declarations
	    //            sb.Append("\t\t\tSqlConnection	objConnection;\n");
	    sb.Append("\t\t\tSqlCommand cmd;\n");
	    sb.Append("\n");

	    // Append the try block
	    sb.Append("\t\t\ttry {\n");

	    sb.Append(GetCreateCommandSection(options.GetProcName(entity.Name, "Select")));

	    // Append the execute statement
	    sb.Append("\t\t\t\t// Execute the query and return the result\n");
	    sb.Append("\t\t\t\treturn cmd.ExecuteReader(CommandBehavior.CloseConnection);\n");
			
	    // Append the catch block
	    sb.Append("\t\t\t} catch (Exception objException) {\n");
	    sb.Append("\t\t\t\tthrow (new Exception(\"" + entity.Name.Replace(" ", "_") + ".Select\\n\\n\" + objException.Message));\n");
	    sb.Append("\t\t\t}\n");
			
	    // Append the method footer
	    sb.Append("\t\t}\n\n\n");

	    /*********************************************************************************************************/
	    // Create the remaining select functions based on identity columns or uniqueidentifiers
	    for (intIndex = 0; intIndex < entity.Fields.Count; intIndex++) {
		field = (Field)entity.Fields[intIndex];
			
		if (field.IsIdentity || field.IsRowGuidCol || field.IsPrimaryKey || field.IsForeignKey) {
		    strColumnName = field.Name.Substring(0, 1).ToUpper() + field.Name.Substring(1);
				
		    // Append the method header
		    sb.Append("\t\t/// <summary>\n");
		    sb.Append("\t\t/// Selects a record from the " + entity.Name + " table by " + field.Name + ".\n");
		    sb.Append("\t\t/// </summary>\n");
		    sb.Append("\t\t/// <param name=\"\"></param>\n");
		    sb.Append("\t\tpublic static SqlDataReader SelectBy" + strColumnName.Replace(" ", "_") + "(" + field.CreateMethodParameter() + ") {\n");
					
		    // Append the variable declarations
		    //                    sb.Append("\t\t\tSqlConnection	objConnection;\n");
		    sb.Append("\t\t\tSqlCommand cmd;\n");
		    sb.Append("\n");

		    // Append the try block
		    sb.Append("\t\t\ttry {\n");

		    sb.Append(GetCreateCommandSection(options.GetProcName(entity.Name, options.GetProcName(entity.Name, "SelectBy" + strColumnName.Replace(" ", "_")))));

		    // Append the parameters
		    sb.Append("\t\t\t\t// Create and append the parameters\n");
		    sb.Append("\t\t\t\t" + field.CreateSqlParameter(false, false));
		    sb.Append("\n");

		    // Append the execute statement
		    sb.Append("\t\t\t\t// Execute the query and return the result\n");
		    sb.Append("\t\t\t\treturn cmd.ExecuteReader(CommandBehavior.CloseConnection);\n");
					
		    // Append the catch block
		    sb.Append("\t\t\t} catch (Exception objException) {\n");
		    sb.Append("\t\t\t\tthrow (new Exception(\"" + entity.Name.Replace(" ", "_") + ".SelectBy" + strColumnName.Replace(" ", "_") + "\\n\\n\" + objException.Message));\n");
		    sb.Append("\t\t\t}\n");
					
		    // Append the method footer
		    if (arrKeyList.Count > 0)
			sb.Append("\t\t}\n\n\n");
		    else
			sb.Append("\t\t}\n\n");
				
		    field = null;
		}
	    }

	    /*********************************************************************************************************/
	    // Create the select functions based on a composite primary key
	    if (arrKeyList.Count > 1) {
		// Append the method header
		sb.Append("\t\t/// <summary>\n");
		sb.Append("\t\t/// Selects a record from the " + entity.Name + " table by a composite primary key.\n");
		sb.Append("\t\t/// </summary>\n");
		sb.Append("\t\t/// <param name=\"\"></param>\n");
				
		sb.Append("\t\tpublic static SqlDataReader SelectBy" + strPrimaryKeyList + "(");
		for (intIndex = 0; intIndex < arrKeyList.Count; intIndex++) {
		    field = (Field)arrKeyList[intIndex];
		    sb.Append(field.CreateMethodParameter() + ", ");
		}
		sb.Append("SqlConnection connection) {\n");
				
		// Append the variable declarations
		//                sb.Append("\t\t\tSqlConnection	objConnection;\n");
		sb.Append("\t\t\tSqlCommand cmd;\n");
		sb.Append("\n");

		// Append the try block
		sb.Append("\t\t\ttry {\n");

		sb.Append(GetCreateCommandSection(options.GetProcName(entity.Name, options.GetProcName(entity.Name, "SelectBy" + strPrimaryKeyList))));

		// Append the parameters
		sb.Append("\t\t\t\t// Create and append the parameters\n");
		for (intIndex = 0; intIndex < arrKeyList.Count; intIndex++) {
		    field = (Field)arrKeyList[intIndex];
		    sb.Append("\t\t\t\t" + field.CreateSqlParameter(false, false));
		    field = null;
		}
		sb.Append("\n");

		// Append the execute statement
		sb.Append("\t\t\t\t// Execute the query and return the result\n");
		sb.Append("\t\t\t\treturn cmd.ExecuteReader(CommandBehavior.CloseConnection);\n");
				
		// Append the catch block
		sb.Append("\t\t\t} catch (Exception objException) {\n");
		sb.Append("\t\t\t\tthrow (new Exception(\"" + entity.Name.Replace(" ", "_") + ".SelectBy" + strPrimaryKeyList + "\\n\\n\" + objException.Message));\n");
		sb.Append("\t\t\t}\n");
				
		// Append the method footer
		sb.Append("\t\t}\n\n\n");
			
		field = null;
	    }
	}
		
	private void CreateDAOListMethods(StringBuilder sb) {
			
	    // GetList methods.
	    sb.Append("\t\tpublic static IList GetList() { \n");
	    sb.Append("\t\t\treturn GetList(null, null);\n");
	    sb.Append("\t\t}\n");
	    sb.Append("\n");

	    sb.Append("\t\tpublic static IList GetList(IWhere whereClause) { \n");
	    sb.Append("\t\t\treturn GetList(whereClause, null);\n");
	    sb.Append("\t\t}\n");
	    sb.Append("\n");

	    sb.Append("\t\tpublic static IList GetList(IWhere whereClause, IOrderBy orderByClause) { \n");
	    sb.Append("\t\t	SqlDataReader dataReader = GetListReader(VIEW, whereClause, orderByClause); \n");
	    sb.Append("\t\t	 \n");
	    sb.Append("\t\t	ArrayList list = new ArrayList(); \n");
	    sb.Append("\t\t	while (dataReader.Read()) { \n");
	    sb.Append("\t\t		list.Add(GetDataObjectFromReader(dataReader)); \n");
	    sb.Append("\t\t	} \n");
	    sb.Append("\t\t	dataReader.Close(); \n");
	    sb.Append("\t\t	return list; \n");
	    sb.Append("\t\t} \n");
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
	    sb.Append("\t\tpublic static ").Append(options.GetDOClassName(entity.Name)).Append(" Load(").Append(parms).Append(") {\n");
	    sb.Append("\t\t	WhereClause w = new WhereClause();\n");
	    foreach (Field field in keys) {
		sb.Append("\t\t	w.And(\"").Append(field.SqlName).Append("\", ").Append(String.Format(field.Type.ConvertToSqlTypeFormat, "", field.GetMethodFormat(), "", "", field.GetMethodFormat())).Append(");\n");
	    }
	    sb.Append("\t\t	SqlDataReader dataReader = GetListReader(VIEW, w, null);\n");
	    sb.Append("\t\t    \n");
	    sb.Append("\t\t	dataReader.Read();\n");
	    sb.Append("\t\t	return GetDataObjectFromReader(dataReader);\n");
	    sb.Append("\t\t}\n");
	    sb.Append("\n");			

	    // GetDataObjectFromReader
	    sb.Append("\t\tprivate static ").Append(options.GetDOClassName(entity.Name)).Append(" GetDataObjectFromReader(SqlDataReader dataReader) {\n");
	    sb.Append("\t\t	").Append(options.GetDOClassName(entity.Name)).Append(" data = new ").Append(options.GetDOClassName(entity.Name)).Append("();\n");

	    foreach (Field field in entity.Fields) {
		if (field.SqlType.Name.Length>0) {
		    sb.Append("\t\tif (dataReader.IsDBNull(dataReader.GetOrdinal(\"").Append(field.SqlName).Append("\"))) { \n");
		    sb.Append("\t\t\tdata.").Append(field.GetMethodFormat()).Append(" = ").Append(field.Type.NullInstanceFormat).Append(";\n"); 
		    sb.Append("\t\t} else {\n");

		    sb.Append("\t\t\tdata.").Append(field.GetMethodFormat()).Append(" = "); 
		    String readerMethod = String.Format(field.SqlType.ReaderMethodFormat, "dataReader", field.SqlName);
		    if (field.Type.ConvertFromSqlTypeFormat.Length >0) {
			sb.Append(String.Format(field.Type.ConvertFromSqlTypeFormat, "data", field.GetMethodFormat(), readerMethod, "dataReader", field.SqlName));
		    } else {
			sb.Append(readerMethod);
		    }
		    sb.Append(";\n");
		    sb.Append("\t\t}\n");

		}
	    }
	    sb.Append("\t\t\n");
	    sb.Append("\t\t	return data;\n");
	    sb.Append("\t\t}\n");
	    sb.Append("\n");			
	}


	private String GetCreateCommandSection(String procName) {
	    StringBuilder sb = new StringBuilder();
	    // Append the connection object creation
	    //                    sb.Append("\t\t\t\t// Create and open the database connection\n");
	    //                    sb.Append("\t\t\t\tobjConnection = new SqlConnection(ConfigurationSettings.AppSettings[\"ConnectionString\"]);\n");
	    //                    sb.Append("\t\t\t\tobjConnection.Open();\n");
	    //                    sb.Append("\n");
					
	    // Append the command object creation
	    sb.Append("\t\t\t\t// Create and execute the command\n");
	    //                    sb.Append("\t\t\t\tcmd = new SqlCommand();\n");
	    //					sb.Append("\t\t\t\tcmd.Connection = objConnection;\n");
	    sb.Append("\t\t\t\tcmd = GetSqlCommand(\"" + procName + "\", CommandType.StoredProcedure);\n");

	    //sb.Append("\t\t\t\tcmd.CommandText = \"" + options.GetProcName(entity.Name, "DeleteBy" + strColumnName.Replace(" ", "_")) + "\";\n");
	    //sb.Append("\t\t\t\tcmd.CommandText = \"" + procName + "\";\n");
	    //sb.Append("\t\t\t\tcmd.CommandType = CommandType.StoredProcedure;\n");
	    sb.Append("\n");
	    return sb.ToString();
	}
    }
}

