using System;
using System.Data;
using System.IO;
using System.Collections;
using System.Text;

namespace Spring2.DataTierGenerator.Core {
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

	    IndentableStringWriter writer = new IndentableStringWriter();

	    // Create the header for the class.
	    GetUsingNamespaces(writer, entity.Fields, true);

	    writer.WriteLine();
	    writer.WriteLine("namespace " + options.GetDAONameSpace(entity.Name) + " {");
	    writer.Write(1, "public class " + options.GetDAOClassName(entity.Name));

	    if (options.DataObjectBaseClass.Length>0) {
		writer.Write(" : " + options.DaoBaseClass);
	    }
	    writer.WriteLine(" {");
	    writer.WriteLine();

	    writer.WriteLine(2, "private static readonly String VIEW = \"" + entity.SqlEntity.View + "\";");
	    writer.WriteLine(2, "private static readonly String CONNECTION_STRING_KEY = \"" + entity.SqlEntity.Key + "\";");
	    writer.WriteLine();

	    CreateDAOListMethods(writer);

	    // Append the access methods.
	    CreateInsertMethod(writer);
	    if (entity.SqlEntity.HasUpdatableColumns()) {
		writer.WriteLine();
		CreateUpdateMethod(writer);
	    }
	    writer.WriteLine();
	    CreateDeleteMethods(writer);

	    if (entity.SqlEntity.GenerateSelectStoredProcScript) {
		writer.WriteLine();
		CreateSelectMethods(writer);
	    }

	    if (entity.Finders.Count>0) {
		writer.WriteLine();
		writer.Write(CreateFinderMethods());
	    }
		
	    // Close out the class and namespace.
	    writer.WriteLine(1, "}");
	    writer.WriteLine("}");

	    FileInfo file = new FileInfo(options.RootDirectory + options.DaoClassDirectory + "\\" + options.GetDAOClassName(entity.Name) + ".cs");
	    WriteToFile(file, writer.ToString(), false);
	}

		
	/// <summary>
	/// Creates a string that represents the insert functionality of the data access class.
	/// </summary>
	/// <param name="table">Name of the table the data access class is for.</param>
	/// <param name="fields">ArrayList object containing one or more Field objects as defined in the table.</param>
	/// <param name="sb">StreamBuilder object that the resulting string should be appended to.</param>
	private void CreateInsertMethod(IndentableStringWriter writer) {
			
	    // Append the method header.
	    writer.WriteLine(2, "/// <summary>");
	    writer.WriteLine(2, "/// Inserts a record into the " + entity.SqlEntity.Name + " table.");
	    writer.WriteLine(2, "/// </summary>");
	    writer.WriteLine(2, "/// <param name=\"\"></param>");
			
	    // Determine the return type of the insert function.
	    writer.Write(2, "public static ");
	    Field idField = entity.GetIdentityField();
	    if (idField != null) {
		writer.Write(idField.Type.Name);
	    } else {
		writer.Write("void");
	    }

	    writer.Write(" Insert(");

	    // Append the method call parameters - data object.
	    writer.WriteLine(entity.Name + "Data data" + ") {");
			
	    // Append the variable declarations.
	    writer.WriteLine(3, "SqlCommand cmd;");
	    writer.WriteLine();

	    GetCreateCommandSection(writer, options.GetProcName(entity.SqlEntity.Name, "Insert"));
			
	    if (idField != null) {
		writer.WriteLine(3, "SqlParameter rv = cmd.Parameters.Add(\"RETURN_VALUE\", SqlDbType.Int);");
		writer.WriteLine(3, "rv.Direction = ParameterDirection.ReturnValue;");
	    }

	    // Append the parameter appends  ;)
	    writer.WriteLine(3, "//Create the parameters and append them to the command object");
	    foreach (Field field in entity.Fields) {
		if (!field.Column.ViewColumn && field.Column.Name.Length>0) {
		    if (field.Column.Identity || field.Column.RowGuidCol) {
			//writer.Write(3, field.CreateSqlParameter(true, true));
		    } else {
			writer.Write(3, field.CreateSqlParameter(false, true));
		    }
		}
	    }
	    writer.WriteLine();

	    // Append the execute statement
	    writer.WriteLine(3, "// Execute the query");
	    writer.WriteLine(3, "cmd.ExecuteNonQuery();");
			
	    // Append the parameter value extraction
	    if (idField != null) {
		writer.WriteLine(3, "// Set the output paramter value(s)");
		writer.Write(3, "return ");
		String readerMethod = "cmd.Parameters[\"RETURN_VALUE\"].Value";
		if (idField.Type.ConvertFromSqlTypeFormat.Length >0) {
		    writer.Write(String.Format(idField.Type.ConvertFromSqlTypeFormat, "", "", readerMethod, "", ""));
		} else {
		    writer.Write(readerMethod);
		}
		writer.WriteLine(";");
	    }
			
	    // Append the method footer
	    writer.WriteLine(2, "}");
	}


	/// <summary>
	/// Creates a string that represents the update functionality of the data access class.
	/// </summary>
	/// <param name="table">Name of the table the data access class is for.</param>
	/// <param name="fields">ArrayList object containing one or more Field objects as defined in the table.</param>
	/// <param name="sb">StreamBuilder object that the resulting string should be appended to.</param>
	private void CreateUpdateMethod(IndentableStringWriter writer) {
			
	    // Append the method header
	    writer.WriteLine(2, "/// <summary>");
	    writer.WriteLine(2, "/// Updates a record in the " + entity.SqlEntity.Name + " table.");
	    writer.WriteLine(2, "/// </summary>");
	    writer.WriteLine(2, "/// <param name=\"\"></param>");
	    writer.Write(2, "public static void Update(");
			
	    // Append the method call parameters - data object
	    writer.Write(entity.Name + "Data data");

	    // Append the method header
	    writer.WriteLine(") {");
			
	    // Append the variable declarations
	    writer.WriteLine(3, "SqlCommand cmd;");
	    writer.WriteLine();

	    GetCreateCommandSection(writer, options.GetProcName(entity.SqlEntity.Name, "Update"));
			
	    // Append the parameter appends  ;)
	    writer.WriteLine(3, "//Create the parameters and append them to the command object");
	    for (int i = 0; i < entity.Fields.Count; i++) {
		Field field = (Field)entity.Fields[i];
		if (!field.Column.ViewColumn && field.Column.Name.Length>0) {
		    writer.Write(3, field.CreateSqlParameter(false, true));
		}
	    }
	    writer.WriteLine();
			
	    // Append the execute statement
	    writer.WriteLine(3, "// Execute the query");
	    writer.WriteLine(3, "cmd.ExecuteNonQuery();");
						
	    // Append the method footer
	    writer.WriteLine(2, "}");
	}


	/// <summary>
	/// Creates a string that represents the delete functionality of the data access class.
	/// </summary>
	/// <param name="table">Name of the table the data access class is for.</param>
	/// <param name="fields">ArrayList object containing one or more Field objects as defined in the table.</param>
	/// <param name="sb">StreamBuilder object that the resulting string should be appended to.</param>
	private void CreateDeleteMethods(IndentableStringWriter writer) {
			
	    // Create the array list of key fields
	    String primaryKeyList = String.Empty;
	    ArrayList keyList = new ArrayList();

	    for (int i = 0; i < entity.Fields.Count; i++) {
		Field field = (Field)entity.Fields[i];
		if (entity.SqlEntity.IsPrimaryKeyColumn(field.Column.Name)) {
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
			
		if (field.Column.Identity || (entity.SqlEntity.GenerateProcsForForeignKey && (field.Column.RowGuidCol || entity.SqlEntity.IsPrimaryKeyColumn(field.Column.Name) || entity.SqlEntity.IsPrimaryKeyColumn(field.Column.Name)))) {
		    String columnName = field.Name.Substring(0, 1).ToUpper() + field.Name.Substring(1);
					
		    String methodName = "Delete" + columnName.Replace(" ", "_");
		    // if this option is on, only generate the PK 
		    if (entity.SqlEntity.GenerateOnlyPrimaryDeleteStoredProc) {
			keyList.Clear();
			methodName = "Delete";
		    }

		    // Append the method header
		    writer.WriteLine(2, "/// <summary>");
		    writer.WriteLine(2, "/// Deletes a record from the " + entity.SqlEntity.Name + " table by " + field.Name + ".");
		    writer.WriteLine(2, "/// </summary>");
		    writer.WriteLine(2, "/// <param name=\"\"></param>");
		    writer.WriteLine(2, "public static void " + methodName + "(" + field.CreateMethodParameter() + ") {");
					
		    writer.WriteLine(3, "SqlCommand cmd;");
		    writer.WriteLine();

		    GetCreateCommandSection(writer, options.GetProcName(entity.SqlEntity.Name, methodName));

		    // Append the parameters
		    writer.WriteLine(3, "// Create and append the parameters");
		    writer.Write(3, field.CreateSqlParameter(false, false));
		    writer.WriteLine();

		    // Append the execute statement
		    writer.WriteLine(3, "// Execute the query and return the result");
		    writer.WriteLine(3, "cmd.ExecuteNonQuery();");
					
		    // Append the method footer
		    if (keyList.Count > 0) {
			writer.WriteLine(2, "}");
			writer.WriteLine();
		    } else {
			writer.WriteLine(2, "}");
			writer.WriteLine();
		    }
		}
	    }

	    /*********************************************************************************************************/
	    // Create the select functions based on a composite primary key
	    if (keyList.Count > 1) {
		// Append the method header
		writer.WriteLine(2, "/// <summary>");
		writer.WriteLine(2, "/// Deletes a record from the " + entity.SqlEntity.Name + " table by a composite primary key.");
		writer.WriteLine(2, "/// </summary>");
		writer.WriteLine(2, "/// <param name=\"\"></param>");

		String methodName = String.Empty;
		if (entity.SqlEntity.GenerateOnlyPrimaryDeleteStoredProc) {
		    methodName = "Delete";
		} else {
		    methodName = "DeleteBy" + primaryKeyList;
		}
				
		writer.Write(2, "public static void " + methodName + "(");

		String parms = String.Empty;
		foreach (Field field in keyList) {
		    if (parms.Length>0) {
			parms += ", ";
		    }
		    parms += field.CreateMethodParameter();
		}
		writer.WriteLine(parms + ") {");
				
		writer.WriteLine(3, "SqlCommand cmd;");
		writer.WriteLine();

		GetCreateCommandSection(writer, options.GetProcName(entity.SqlEntity.Name, methodName));

		// Append the parameters
		writer.WriteLine(4, "// Create and append the parameters");
		for (int i = 0; i < keyList.Count; i++) {
		    Field field = (Field)keyList[i];
		    writer.Write(4, field.CreateSqlParameter(false, false));
		}
		writer.WriteLine();

		// Append the execute statement
		writer.WriteLine(4, "// Execute the query and return the result");
		writer.WriteLine(4, "cmd.ExecuteNonQuery();");
				
		// Append the method footer
		writer.WriteLine(2, "}");
		writer.WriteLine();
	    }
	}


	/// <summary>
	/// Creates a string that represents the select functionality of the data access class.
	/// </summary>
	/// <param name="table">Name of the table the data access class is for.</param>
	/// <param name="fields">ArrayList object containing one or more Field objects as defined in the table.</param>
	/// <param name="sb">StreamBuilder object that the resulting string should be appended to.</param>
	private void CreateSelectMethods(IndentableStringWriter writer) {
	    Field	field;
	    int			intIndex;
	    string		strColumnName;
	    string		strPrimaryKeyList;
	    ArrayList	arrKeyList;
			
	    // Create the array list of key fields
	    strPrimaryKeyList = String.Empty;
	    arrKeyList = new ArrayList();
	    for (intIndex = 0; intIndex < entity.Fields.Count; intIndex++) {
		field = (Field)entity.Fields[intIndex];
		if (entity.SqlEntity.IsPrimaryKeyColumn(field.Column.Name)) {
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
	    writer.WriteLine(2, "/// <summary>");
	    writer.WriteLine(2, "/// Selects a record from the " + entity.SqlEntity.Name + " table.");
	    writer.WriteLine(2, "/// </summary>");
	    writer.WriteLine(2, "/// <param name=\"\"></param>");
	    writer.WriteLine(2, "public static SqlDataReader Select() {");
			
	    // Append the variable declarations
	    //writer.WriteLine(3, "SqlConnection objConnection;");
	    writer.WriteLine(3, "SqlCommand cmd;");
	    writer.WriteLine();

	    // Append the try block
	    writer.WriteLine(3, "try {");

	    GetCreateCommandSection(writer, options.GetProcName(entity.SqlEntity.Name, "Select"));

	    // Append the execute statement
	    writer.WriteLine(4, "// Execute the query and return the result");
	    writer.WriteLine(4, "return cmd.ExecuteReader(CommandBehavior.CloseConnection);");
			
	    // Append the catch block
	    writer.WriteLine(3, "} catch (Exception objException) {");
	    writer.WriteLine(4, "throw (new Exception(\"" + entity.Name.Replace(" ", "_") + ".Select\\n\\n\" + objException.Message));");
	    writer.WriteLine(3, "}");
			
	    // Append the method footer
	    writer.WriteLine(2, "}");
	    writer.WriteLine();

	    /*********************************************************************************************************/
	    // Create the remaining select functions based on identity columns or uniqueidentifiers
	    for (intIndex = 0; intIndex < entity.Fields.Count; intIndex++) {
		field = (Field)entity.Fields[intIndex];
			
		if (field.Column.Identity || field.Column.RowGuidCol || entity.SqlEntity.IsPrimaryKeyColumn(field.Column.Name) || entity.SqlEntity.IsForeignKeyColumn(field.Column.Name)) {
		    strColumnName = field.Name.Substring(0, 1).ToUpper() + field.Name.Substring(1);
				
		    // Append the method header
		    writer.WriteLine(2, "/// <summary>");
		    writer.WriteLine(2, "/// Selects a record from the " + entity.Name + " table by " + field.Name + ".");
		    writer.WriteLine(2, "/// </summary>");
		    writer.WriteLine(2, "/// <param name=\"\"></param>");
		    writer.WriteLine(2, "public static SqlDataReader SelectBy" + strColumnName.Replace(" ", "_") + "(" + field.CreateMethodParameter() + ") {");
					
		    // Append the variable declarations
		    //writer.WriteLine(3, "SqlConnection objConnection;");
		    writer.WriteLine(3, "SqlCommand cmd;");
		    writer.WriteLine();

		    // Append the try block
		    writer.WriteLine(3, "try {");

		    GetCreateCommandSection(writer, options.GetProcName(entity.Name, options.GetProcName(entity.Name, "SelectBy" + strColumnName.Replace(" ", "_"))));

		    // Append the parameters
		    writer.WriteLine(4, "// Create and append the parameters");
		    writer.Write(4, field.CreateSqlParameter(false, false));
		    writer.WriteLine();

		    // Append the execute statement
		    writer.WriteLine(4, "// Execute the query and return the result");
		    writer.WriteLine(4, "return cmd.ExecuteReader(CommandBehavior.CloseConnection);");
					
		    // Append the catch block
		    writer.WriteLine(3, "} catch (Exception objException) {");
		    writer.WriteLine(4, "throw (new Exception(\"" + entity.Name.Replace(" ", "_") + ".SelectBy" + strColumnName.Replace(" ", "_") + "\\n\\n\" + objException.Message));");
		    writer.WriteLine(3, "}");
					
		    // Append the method footer
		    if (arrKeyList.Count > 0) {
			writer.WriteLine(2, "}");
			writer.WriteLine();
			writer.WriteLine();
		    } else {
			writer.WriteLine(2, "}");
			writer.WriteLine();
		    }
				
		    field = null;
		}
	    }

	    /*********************************************************************************************************/
	    // Create the select functions based on a composite primary key
	    if (arrKeyList.Count > 1) {
		// Append the method header
		writer.WriteLine(2, "/// <summary>");
		writer.WriteLine(2, "/// Selects a record from the " + entity.Name + " table by a composite primary key.");
		writer.WriteLine(2, "/// </summary>");
		writer.WriteLine(2, "/// <param name=\"\"></param>");
				
		writer.Write(2, "public static SqlDataReader SelectBy" + strPrimaryKeyList + "(");
		for (intIndex = 0; intIndex < arrKeyList.Count; intIndex++) {
		    field = (Field)arrKeyList[intIndex];
		    writer.Write(field.CreateMethodParameter() + ", ");
		}
		writer.WriteLine("SqlConnection connection) {");
				
		// Append the variable declarations
		//writer.WriteLine(3, "SqlConnection objConnection;");
		writer.WriteLine(3, "SqlCommand cmd;");
		writer.WriteLine();

		// Append the try block
		writer.WriteLine(3, "try {");

		GetCreateCommandSection(writer, options.GetProcName(entity.Name, options.GetProcName(entity.Name, "SelectBy" + strPrimaryKeyList)));

		// Append the parameters
		writer.WriteLine(4, "// Create and append the parameters");
		for (intIndex = 0; intIndex < arrKeyList.Count; intIndex++) {
		    field = (Field)arrKeyList[intIndex];
		    writer.Write(4, field.CreateSqlParameter(false, false));
		    field = null;
		}
		writer.WriteLine();

		// Append the execute statement
		writer.WriteLine(4, "// Execute the query and return the result");
		writer.WriteLine(4, "return cmd.ExecuteReader(CommandBehavior.CloseConnection);");
				
		// Append the catch block
		writer.WriteLine(3, "} catch (Exception objException) {");
		writer.WriteLine(4, "throw (new Exception(\"" + entity.Name.Replace(" ", "_") + ".SelectBy" + strPrimaryKeyList + "\\n\\n\" + objException.Message));");
		writer.WriteLine(3, "}");
				
		// Append the method footer
		writer.WriteLine(2, "}");
		writer.WriteLine();
		writer.WriteLine();
			
		field = null;
	    }
	}
		
	private void CreateDAOListMethods(IndentableStringWriter writer) {
			
	    // GetList - no parms
	    writer.WriteLine(2, "public static IList GetList() { ");
	    writer.WriteLine(3, "return GetList(null, null);");
	    writer.WriteLine(2, "}");
	    writer.WriteLine();

	    // GetList - where
	    writer.WriteLine(2, "public static IList GetList(IWhere whereClause) { ");
	    writer.WriteLine(3, "return GetList(whereClause, null);");
	    writer.WriteLine(2, "}");
	    writer.WriteLine();

	    // GetList - order by
	    writer.WriteLine(2, "public static IList GetList(IOrderBy orderByClause) { ");
	    writer.WriteLine(3, "return GetList(null, orderByClause);");
	    writer.WriteLine(2, "}");
	    writer.WriteLine();

	    // GetList - both
	    writer.WriteLine(2, "public static IList GetList(IWhere whereClause, IOrderBy orderByClause) { ");
	    writer.WriteLine(3, "SqlDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, whereClause, orderByClause); ");
	    writer.WriteLine();
	    writer.WriteLine(3, "ArrayList list = new ArrayList(); ");
	    writer.WriteLine(3, "while (dataReader.Read()) { ");
	    writer.WriteLine(4, "list.Add(GetDataObjectFromReader(dataReader)); ");
	    writer.WriteLine(3, "}");
	    writer.WriteLine(3, "dataReader.Close();");
	    writer.WriteLine(3, "return list; ");
	    writer.WriteLine(2, "}");
	    writer.WriteLine();

	    // Load
	    IList keys = entity.GetPrimaryKeyFields();
	    String parms = String.Empty;
	    foreach (Field field in keys) {
		if (!parms.Equals(String.Empty)) {
		    parms += ", ";
		}
		parms += field.CreateMethodParameter();
	    }
	    writer.WriteLine(2, "public static " + options.GetDOClassName(entity.Name) + " Load(" + parms + ") {");
	    writer.WriteLine(3, "WhereClause w = new WhereClause();");
	    foreach (Field field in keys) {
		writer.WriteLine(3, "w.And(\"" + field.Column.Name + "\", " + String.Format(field.Type.ConvertToSqlTypeFormat, "", field.GetFieldFormat(), "", "", field.GetFieldFormat()) + ");");
	    }
	    writer.WriteLine(3, "SqlDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, w, null);");
	    writer.WriteLine();
	    writer.WriteLine(3, "dataReader.Read();");
	    writer.WriteLine(3, options.GetDOClassName(entity.Name) + " data = GetDataObjectFromReader(dataReader);");
	    writer.WriteLine(3, "dataReader.Close();");
	    writer.WriteLine(3, "return data;");
	    writer.WriteLine(2, "}");
	    writer.WriteLine();			

	    // GetDataObjectFromReader
	    writer.WriteLine(2, "private static " + options.GetDOClassName(entity.Name) + " GetDataObjectFromReader(SqlDataReader dataReader) {");
	    writer.WriteLine(3, options.GetDOClassName(entity.Name) + " data = new " + options.GetDOClassName(entity.Name) + "();");

	    foreach (Field field in entity.Fields) {
		if (field.Column.SqlType.Name.Length>0) {
		    writer.WriteLine(3, "if (dataReader.IsDBNull(dataReader.GetOrdinal(\"" + field.Column.Name + "\"))) { ");
		    writer.WriteLine(4, "data." + field.GetMethodFormat() + " = " + field.Type.NullInstanceFormat + ";"); 
		    writer.WriteLine(3, "} else {");

		    writer.Write(4, "data." + field.GetMethodFormat() + " = "); 
		    String readerMethod = String.Format(field.Column.SqlType.ReaderMethodFormat, "dataReader", field.Column.Name);
		    if (field.Type.ConvertFromSqlTypeFormat.Length >0) {
			writer.Write(String.Format(field.Type.ConvertFromSqlTypeFormat, "data", field.GetMethodFormat(), readerMethod, "dataReader", field.Column.Name));
		    } else {
			writer.Write(readerMethod);
		    }
		    writer.WriteLine(";");
		    writer.WriteLine(3, "}");
		}
	    }
	    writer.WriteLine();
	    writer.WriteLine(3, "return data;");
	    writer.WriteLine(2, "}");
	    writer.WriteLine();			
	}


	private String GetCreateCommandSection(IndentableStringWriter writer, String procName) {
	    writer.WriteLine(3, "// Create and execute the command");
	    writer.WriteLine(3, "cmd = GetSqlCommand(CONNECTION_STRING_KEY, \"" + procName + "\", CommandType.StoredProcedure);");
	    writer.WriteLine();
	    return writer.ToString();
	}


	private String CreateFinderMethods() {
	    IndentableStringWriter writer = new IndentableStringWriter();

	    foreach (Finder finder in entity.Finders) {

		String parms = String.Empty;
		foreach (Field field in finder.Fields) {
		    if (!parms.Equals(String.Empty)) {
			parms += ", ";
		    }
		    parms += field.CreateMethodParameter();
		}
		writer.Write(2, "public static ");
		if (finder.Unique) {
		    writer.Write(options.GetDOClassName(entity.Name));
		} else {
		    writer.Write("IList");
		}
		writer.WriteLine(" " + finder.Name + "(" + parms + ") {");
		writer.WriteLine(2, "	WhereClause filter = new WhereClause();");
		writer.WriteLine(2, "	OrderByClause sort = new OrderByClause(\"" + finder.Sort + "\");");
		foreach (Field field in finder.Fields) {
		    writer.WriteLine(2, "	filter.And(\"" + field.Column.Name + "\", " + String.Format(field.Type.ConvertToSqlTypeFormat, "", field.GetFieldFormat(), "", "", field.GetFieldFormat()) + ");");
		}

		if (!finder.Unique) {
		    writer.WriteLine();
		    writer.WriteLine(4, "return GetList(filter, sort);");
    		} else {
		    writer.WriteLine(2, "	SqlDataReader dataReader = GetListReader(CONNECTION_STRING_KEY, VIEW, filter, sort);");
		    writer.WriteLine();
		    writer.WriteLine(2, "	dataReader.Read();");
		    writer.WriteLine(4, options.GetDOClassName(entity.Name) + " data = GetDataObjectFromReader(dataReader);");
		    writer.WriteLine(4, "dataReader.Close();");
		    writer.WriteLine(2, "	return data;");
		}
		writer.WriteLine(2, "}");
		writer.WriteLine();			

	    }


	    return writer.ToString();
	}
    }
}

