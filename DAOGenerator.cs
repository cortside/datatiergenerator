using System;
using System.Data;
using System.IO;
using System.Collections;
using System.Text;

namespace Spring2.DataTierGenerator {
    /// <summary>
    /// Generates stored procedures and associated data access code for the specified database.
    /// </summary>
    public class DAOGenerator {
        private StreamWriter writer;
        private Configuration options;
        private String table;
        private ArrayList fields;

        /// <summary>
        /// Contructor for the Generator class.
        /// </summary>
        /// <param name="strConnectionString">Connecion string to a SQL Server database.</param>
        public DAOGenerator(Configuration options, StreamWriter writer, String table, ArrayList fields) {
            this.options = options;
            this.writer = writer;
            this.table = table;
            this.fields = fields;
        }
		

        /// <summary>
        /// Creates a C# data access class for all of the table's stored procedures.
        /// </summary>
        /// <param name="table">Name of the table the class should be generated for.</param>
        /// <param name="fields">ArrayList object containing one or more Field objects as defined in the table.</param>
        public void CreateDataAccessClass() {
            StreamWriter	objStreamWriter;
            StringBuilder	sb;
            string			strFileName;
			
            sb = new StringBuilder(4096);

            // Create the header for the class
            sb.Append("using System;\n");
            sb.Append("using System.Data;\n");
            sb.Append("using System.Data.SqlClient;\n");
            sb.Append("using System.Configuration;\n");
            sb.Append("using System.Collections;\n");
			sb.Append("using Spring2.Core.DAO;\n");
            sb.Append("using ").Append(options.GetDONameSpace(null)).Append(";\n");

            sb.Append("\n");
            sb.Append("namespace " + options.GetDAONameSpace(table) + " {\n");
            sb.Append("\tpublic class " + options.GetDAOClassName(table) + " : Spring2.Core.DAO.EntityDAO {\n");

            //sb.Append("\n\t\tprivate readonly String TABLE=\"").Append(table).Append("\";\n\n");
            sb.Append("\n\t\tprivate readonly String VIEW=\"vw").Append(table).Append("\";\n\n");

            CreateDAOListMethods(sb);

            // Append the access methods
            CreateInsertMethod(sb);
            sb.Append("\n\n");
            CreateUpdateMethod(sb);
            sb.Append("\n\n");
            CreateDeleteMethods(sb);

			if (options.GenerateSelectStoredProcs) {
				sb.Append("\n\n");
				CreateSelectMethods(sb);
			}
		
            // Close out the class and namespace
            sb.Append("\t}\n");
            sb.Append("}\n");

            // Create the output stream
            strFileName = options.RootDirectory + options.DaoClassDirectory + "\\" + options.GetDAOClassName(table) + ".cs";
            if (File.Exists(strFileName))
                File.Delete(strFileName);
            objStreamWriter = new StreamWriter(strFileName);
            objStreamWriter.Write(sb.ToString());
            objStreamWriter.Close();
            objStreamWriter = null;
            sb = null;
        }

		
        /// <summary>
        /// Creates a string that represents the insert functionality of the data access class.
        /// </summary>
        /// <param name="table">Name of the table the data access class is for.</param>
        /// <param name="fields">ArrayList object containing one or more Field objects as defined in the table.</param>
        /// <param name="sb">StreamBuilder object that the resulting string should be appended to.</param>
        private void CreateInsertMethod(StringBuilder sb) {
            Field	objField;
            int			intIndex;
            bool		blnReturnVoid;
			
            // Append the method header
            sb.Append("\t\t/// <summary>\n");
            sb.Append("\t\t/// Inserts a record into the " + table + " table.\n");
            sb.Append("\t\t/// </summary>\n");
            sb.Append("\t\t/// <param name=\"\"></param>\n");
			
            // Determine the return type of the insert function
            blnReturnVoid = true;
            for (intIndex = 0; intIndex < fields.Count; intIndex++) {
                objField = (Field)fields[intIndex];
                if (objField.IsIdentity) {
                    sb.Append("\t\tpublic Int32 Insert(");
                    blnReturnVoid = false;
                    break;
                } else if (objField.IsRowGuidCol) {
                    sb.Append("\t\tpublic Guid Insert(");
                    blnReturnVoid = false;
                    break;
                }
            }
			
            if (blnReturnVoid)
                sb.Append("\t\tpublic void Insert(");
			
            // Append the method call parameters
            /*			for (intIndex = 0; intIndex < fields.Count; intIndex++) {
                            objField = (Field)fields[intIndex];
                            if (objField.IsIdentity == false && objField.IsRowGuidCol == false) {
                                strMethodParameter = CreateMethodParameter(objField);
                                sb.Append(strMethodParameter);
                                sb.Append(", ");
                            }
                            objField = null;
                        }
            */			

            // Append the method call parameters - data object
            sb.Append(table).Append("Data data, ");

            // Append the connection string parameter
            sb.Append("SqlConnection connection");
			
            // Append the method header
            sb.Append(") {\n");
			
            // Append the variable declarations
            //sb.Append("\t\t\tSqlConnection	objConnection;\n");
            sb.Append("\t\t\tSqlCommand		objCommand;\n");
            sb.Append("\n");

            // Append the try block
//            sb.Append("\t\t\ttry {\n");

			sb.Append(GetCreateCommandSection(options.GetProcName(table, "Insert")));
			
            sb.Append("\t\t\t\tSqlParameter rv = objCommand.Parameters.Add(\"RETURN_VALUE\", SqlDbType.Int);\n");
            sb.Append("\t\t\t\trv.Direction = ParameterDirection.ReturnValue;\n");

            // Append the parameter appends  ;)
            sb.Append("\t\t\t\t//Create the parameters and append them to the command object\n");
            for (intIndex = 0; intIndex < fields.Count; intIndex++) {
                objField = (Field)fields[intIndex];
                if (!objField.IsViewColumn) {
					if (objField.IsIdentity || objField.IsRowGuidCol) {
						//sb.Append("\t\t\t\t" + objField.CreateSqlParameter(true, true));
					} else {
						sb.Append("\t\t\t\t" + objField.CreateSqlParameter(false, true));
					}
                }
                objField = null;
            }
            sb.Append("\n");

            // Append the execute statement
            sb.Append("\t\t\t\t// Execute the query\n");
            sb.Append("\t\t\t\tobjCommand.ExecuteNonQuery();\n");
			
            // Append the parameter value extraction
            for (intIndex = 0; intIndex < fields.Count; intIndex++) {
                objField = (Field)fields[intIndex];
                if (objField.IsIdentity || objField.IsRowGuidCol) {
                    sb.Append("\n\t\t\t\t// Set the output paramter value(s)\n");
                    if (objField.IsIdentity)
                        //sb.Append("\t\t\t\treturn Int32.Parse(objCommand.Parameters[\"@" + objField.ColumnName.Replace(" ", "_") + "\"].Value.ToString());\n");
						sb.Append("\t\t\t\treturn Int32.Parse(objCommand.Parameters[\"RETURN_VALUE\"].Value.ToString());\n");
                    else
                        sb.Append("\t\t\t\treturn Guid.NewGuid(objCommand.Parameters[\"@" + objField.ColumnName.Replace(" ", "_") + "\"].Value.ToString());\n");
                }
                objField = null;
            }
			
            // Append the catch block
//            sb.Append("\t\t\t} catch (Exception objException) {\n");
//            sb.Append("\t\t\t\tthrow (new Exception(\"" + table.Replace(" ", "_") + ".Insert\\n\\n\" + objException.Message));\n");
//            sb.Append("\t\t\t}\n");
			
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
            Field	objField;
            Field	objNewField;
            Field	objOldField;
            //string		strMethodParameter;
            int			intIndex;
			
            // Append the method header
            sb.Append("\t\t/// <summary>\n");
            sb.Append("\t\t/// Updates a record in the " + table + " table.\n");
            sb.Append("\t\t/// </summary>\n");
            sb.Append("\t\t/// <param name=\"\"></param>\n");
            sb.Append("\t\tpublic void Update(");
			
            // Append the method call parameters
            /*			for (intIndex = 0; intIndex < fields.Count; intIndex++) {
                            objField = (Field)fields[intIndex];
                            if (objField.IsPrimaryKey && objField.IsIdentity == false && objField.IsRowGuidCol == false) {
                                objOldField = objField.Copy();
                                objOldField.ColumnName = "Old" + objOldField.ColumnName;
                                strMethodParameter = CreateMethodParameter(objOldField);
                                sb.Append(strMethodParameter);
                                sb.Append(", ");
					
                                objNewField = objField.Copy();
                                objNewField.ColumnName = "New" + objNewField.ColumnName;
                                strMethodParameter = CreateMethodParameter(objNewField);
                                sb.Append(strMethodParameter);
                                sb.Append(", ");
                            } else {
                                strMethodParameter = CreateMethodParameter(objField);
                                sb.Append(strMethodParameter);
                                sb.Append(", ");
                            }
                            objField = null;
                        }
            */
            // Append the method call parameters - data object
            sb.Append(table).Append("Data data, ");

			
            // Append the connection string parameter
            sb.Append("SqlConnection connection");
			
            // Append the method header
            sb.Append(") {\n");
			
            // Append the variable declarations
            //sb.Append("\t\t\tSqlConnection	objConnection;\n");
            sb.Append("\t\t\tSqlCommand		objCommand;\n");
            sb.Append("\n");

            // Append the try block
//            sb.Append("\t\t\ttry {\n");

			sb.Append(GetCreateCommandSection(options.GetProcName(table, "Update")));
			
            // Append the parameter appends  ;)
            sb.Append("\t\t\t\t//Create the parameters and append them to the command object\n");
            for (intIndex = 0; intIndex < fields.Count; intIndex++) {
                objField = (Field)fields[intIndex];
                if (!objField.IsViewColumn) {
//                    if (objField.IsPrimaryKey && objField.IsIdentity == false && objField.IsRowGuidCol == false) {
//                        objOldField = objField.Copy();
//                        objOldField.ColumnName = "Old" + objOldField.ColumnName;
//                        sb.Append("\t\t\t\t" + objOldField.CreateSqlParameter(false, true));
//					
//                        objNewField = objField.Copy();
//                        objNewField.ColumnName = "New" + objNewField.ColumnName;
//                        sb.Append("\t\t\t\t" + objNewField.CreateSqlParameter(false, true));
//                    } else {
                        sb.Append("\t\t\t\t" + objField.CreateSqlParameter(false, true));
//                    }
                }
                objField = null;
            }
            sb.Append("\n");
			
            // Append the execute statement
            sb.Append("\t\t\t\t// Execute the query\n");
            sb.Append("\t\t\t\tobjCommand.ExecuteNonQuery();\n");
			
            // Append the catch block
//            sb.Append("\t\t\t} catch (Exception objException) {\n");
//            sb.Append("\t\t\t\tthrow (new Exception(\"" + table.Replace(" ", "_") + ".Update\\n\\n\" + objException.Message));\n");
//            sb.Append("\t\t\t}\n");
			
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
            Field	objField;
            int			intIndex;
            string		strColumnName;
            string		strPrimaryKeyList;
            ArrayList	arrKeyList;
			
            // Create the array list of key fields
            strPrimaryKeyList = "";
            arrKeyList = new ArrayList();
            for (intIndex = 0; intIndex < fields.Count; intIndex++) {
                objField = (Field)fields[intIndex];
                if (objField.IsPrimaryKey) {
                    arrKeyList.Add(objField);
                    strPrimaryKeyList += objField.ColumnName.Replace(" ", "_") + "_";
                }
                objField = null;
            }
			
            // Trim off the last underscore
            if (strPrimaryKeyList.Length > 0)
                strPrimaryKeyList = strPrimaryKeyList.Substring(0, strPrimaryKeyList.Length - 1);

            /*********************************************************************************************************/
            // Create the remaining select functions based on identity columns or uniqueidentifiers
            for (intIndex = 0; intIndex < fields.Count; intIndex++) {
                objField = (Field)fields[intIndex];
			
                if (objField.IsIdentity || (options.GenerateProcsForForeignKey && (objField.IsRowGuidCol || objField.IsPrimaryKey || objField.IsForeignKey))) {
                    strColumnName = objField.ColumnName.Substring(0, 1).ToUpper() + objField.ColumnName.Substring(1);
					
					String methodName = "Delete" + strColumnName.Replace(" ", "_");
					// if this option is on, only generate the PK 
					if (options.GenerateOnlyPrimaryDeleteStoredProc) {
						arrKeyList.Clear();
						methodName = "Delete";
					}

                    // Append the method header
                    sb.Append("\t\t/// <summary>\n");
                    sb.Append("\t\t/// Deletes a record from the " + table + " table by " + objField.ColumnName + ".\n");
                    sb.Append("\t\t/// </summary>\n");
                    sb.Append("\t\t/// <param name=\"\"></param>\n");
                    //sb.Append("\t\tpublic void DeleteBy" + strColumnName.Replace(" ", "_") + "(" + objField.CreateMethodParameter() + ", SqlConnection connection) {\n");
					sb.Append("\t\tpublic void " + methodName + "(" + objField.CreateMethodParameter() + ", SqlConnection connection) {\n");
					
                    // Append the variable declarations
//                    sb.Append("\t\t\tSqlConnection	objConnection;\n");
                    sb.Append("\t\t\tSqlCommand		objCommand;\n");
                    sb.Append("\n");

                    // Append the try block
//                    sb.Append("\t\t\ttry {\n");

					sb.Append(GetCreateCommandSection(options.GetProcName(table, methodName)));

                    // Append the parameters
                    sb.Append("\t\t\t\t// Create and append the parameters\n");
                    sb.Append("\t\t\t\t" + objField.CreateSqlParameter(false, false));
                    sb.Append("\n");

                    // Append the execute statement
                    sb.Append("\t\t\t\t// Execute the query and return the result\n");
                    sb.Append("\t\t\t\tobjCommand.ExecuteNonQuery();\n");
					
                    // Append the catch block
//                    sb.Append("\t\t\t} catch (Exception objException) {\n");
//                    sb.Append("\t\t\t\tthrow (new Exception(\"" + table.Replace(" ", "_") + "." + methodName + "\\n\\n\" + objException.Message));\n");
//                    sb.Append("\t\t\t}\n");
					
                    // Append the method footer
                    if (arrKeyList.Count > 0)
                        sb.Append("\t\t}\n\n\n");
                    else
                        sb.Append("\t\t}\n\n");
				
                    objField = null;
                }
            }

            /*********************************************************************************************************/
            // Create the select functions based on a composite primary key
            if (arrKeyList.Count > 1) {
                // Append the method header
                sb.Append("\t\t/// <summary>\n");
                sb.Append("\t\t/// Deletes a record from the " + table + " table by a composite primary key.\n");
                sb.Append("\t\t/// </summary>\n");
                sb.Append("\t\t/// <param name=\"\"></param>\n");

				String methodName = "";
				if (options.GenerateOnlyPrimaryDeleteStoredProc) {
					methodName = "Delete";
				} else {
					methodName = "DeleteBy" + strPrimaryKeyList;
				}
				
                sb.Append("\t\tpublic void " + methodName + "(");
                for (intIndex = 0; intIndex < arrKeyList.Count; intIndex++) {
                    objField = (Field)arrKeyList[intIndex];
                    sb.Append(objField.CreateMethodParameter() + ", ");
                }
                sb.Append("SqlConnection connection) {\n");
				
                // Append the variable declarations
//                sb.Append("\t\t\tSqlConnection	objConnection;\n");
                sb.Append("\t\t\tSqlCommand		objCommand;\n");
                sb.Append("\n");

                // Append the try block
//                sb.Append("\t\t\ttry {\n");

				sb.Append(GetCreateCommandSection(options.GetProcName(table, methodName)));

                // Append the parameters
                sb.Append("\t\t\t\t// Create and append the parameters\n");
                for (intIndex = 0; intIndex < arrKeyList.Count; intIndex++) {
                    objField = (Field)arrKeyList[intIndex];
                    sb.Append("\t\t\t\t" + objField.CreateSqlParameter(false, false));
                    objField = null;
                }
                sb.Append("\n");

                // Append the execute statement
                sb.Append("\t\t\t\t// Execute the query and return the result\n");
                sb.Append("\t\t\t\tobjCommand.ExecuteNonQuery();\n");
				
                // Append the catch block
//                sb.Append("\t\t\t} catch (Exception objException) {\n");
//                sb.Append("\t\t\t\tthrow (new Exception(\"" + table.Replace(" ", "_") + "." + methodName + "\\n\\n\" + objException.Message));\n");
//                sb.Append("\t\t\t}\n");
				
                // Append the method footer
                sb.Append("\t\t}\n\n\n");
			
                objField = null;
            }
        }


        /// <summary>
        /// Creates a string that represents the select functionality of the data access class.
        /// </summary>
        /// <param name="table">Name of the table the data access class is for.</param>
        /// <param name="fields">ArrayList object containing one or more Field objects as defined in the table.</param>
        /// <param name="sb">StreamBuilder object that the resulting string should be appended to.</param>
        private void CreateSelectMethods(StringBuilder sb) {
            Field	objField;
            int			intIndex;
            string		strColumnName;
            string		strPrimaryKeyList;
            ArrayList	arrKeyList;
			
            // Create the array list of key fields
            strPrimaryKeyList = "";
            arrKeyList = new ArrayList();
            for (intIndex = 0; intIndex < fields.Count; intIndex++) {
                objField = (Field)fields[intIndex];
                if (objField.IsPrimaryKey) {
                    arrKeyList.Add(objField);
                    strPrimaryKeyList += objField.ColumnName.Replace(" ", "_") + "_";
                }
                objField = null;
            }
			
            // Trim off the last underscore
            if (strPrimaryKeyList.Length > 0)
                strPrimaryKeyList = strPrimaryKeyList.Substring(0, strPrimaryKeyList.Length - 1);

            /*********************************************************************************************************/
            // Create the initial "select all" function
			
            // Append the method header
            sb.Append("\t\t/// <summary>\n");
            sb.Append("\t\t/// Selects a record from the " + table + " table.\n");
            sb.Append("\t\t/// </summary>\n");
            sb.Append("\t\t/// <param name=\"\"></param>\n");
            sb.Append("\t\tpublic SqlDataReader Select(SqlConnection connection) {\n");
			
            // Append the variable declarations
//            sb.Append("\t\t\tSqlConnection	objConnection;\n");
            sb.Append("\t\t\tSqlCommand		objCommand;\n");
            sb.Append("\n");

            // Append the try block
            sb.Append("\t\t\ttry {\n");

			sb.Append(GetCreateCommandSection(options.GetProcName(table, "Select")));

            // Append the execute statement
            sb.Append("\t\t\t\t// Execute the query and return the result\n");
            sb.Append("\t\t\t\treturn objCommand.ExecuteReader(CommandBehavior.CloseConnection);\n");
			
            // Append the catch block
            sb.Append("\t\t\t} catch (Exception objException) {\n");
            sb.Append("\t\t\t\tthrow (new Exception(\"" + table.Replace(" ", "_") + ".Select\\n\\n\" + objException.Message));\n");
            sb.Append("\t\t\t}\n");
			
            // Append the method footer
            sb.Append("\t\t}\n\n\n");

            /*********************************************************************************************************/
            // Create the remaining select functions based on identity columns or uniqueidentifiers
            for (intIndex = 0; intIndex < fields.Count; intIndex++) {
                objField = (Field)fields[intIndex];
			
                if (objField.IsIdentity || objField.IsRowGuidCol || objField.IsPrimaryKey || objField.IsForeignKey) {
                    strColumnName = objField.ColumnName.Substring(0, 1).ToUpper() + objField.ColumnName.Substring(1);
				
                    // Append the method header
                    sb.Append("\t\t/// <summary>\n");
                    sb.Append("\t\t/// Selects a record from the " + table + " table by " + objField.ColumnName + ".\n");
                    sb.Append("\t\t/// </summary>\n");
                    sb.Append("\t\t/// <param name=\"\"></param>\n");
                    sb.Append("\t\tpublic SqlDataReader SelectBy" + strColumnName.Replace(" ", "_") + "(" + objField.CreateMethodParameter() + ", SqlConnection connection) {\n");
					
                    // Append the variable declarations
//                    sb.Append("\t\t\tSqlConnection	objConnection;\n");
                    sb.Append("\t\t\tSqlCommand		objCommand;\n");
                    sb.Append("\n");

                    // Append the try block
                    sb.Append("\t\t\ttry {\n");

					sb.Append(GetCreateCommandSection(options.GetProcName(table, options.GetProcName(table, "SelectBy" + strColumnName.Replace(" ", "_")))));

                    // Append the parameters
                    sb.Append("\t\t\t\t// Create and append the parameters\n");
                    sb.Append("\t\t\t\t" + objField.CreateSqlParameter(false, false));
                    sb.Append("\n");

                    // Append the execute statement
                    sb.Append("\t\t\t\t// Execute the query and return the result\n");
                    sb.Append("\t\t\t\treturn objCommand.ExecuteReader(CommandBehavior.CloseConnection);\n");
					
                    // Append the catch block
                    sb.Append("\t\t\t} catch (Exception objException) {\n");
                    sb.Append("\t\t\t\tthrow (new Exception(\"" + table.Replace(" ", "_") + ".SelectBy" + strColumnName.Replace(" ", "_") + "\\n\\n\" + objException.Message));\n");
                    sb.Append("\t\t\t}\n");
					
                    // Append the method footer
                    if (arrKeyList.Count > 0)
                        sb.Append("\t\t}\n\n\n");
                    else
                        sb.Append("\t\t}\n\n");
				
                    objField = null;
                }
            }

            /*********************************************************************************************************/
            // Create the select functions based on a composite primary key
            if (arrKeyList.Count > 1) {
                // Append the method header
                sb.Append("\t\t/// <summary>\n");
                sb.Append("\t\t/// Selects a record from the " + table + " table by a composite primary key.\n");
                sb.Append("\t\t/// </summary>\n");
                sb.Append("\t\t/// <param name=\"\"></param>\n");
				
                sb.Append("\t\tpublic SqlDataReader SelectBy" + strPrimaryKeyList + "(");
                for (intIndex = 0; intIndex < arrKeyList.Count; intIndex++) {
                    objField = (Field)arrKeyList[intIndex];
                    sb.Append(objField.CreateMethodParameter() + ", ");
                }
                sb.Append("SqlConnection connection) {\n");
				
                // Append the variable declarations
//                sb.Append("\t\t\tSqlConnection	objConnection;\n");
                sb.Append("\t\t\tSqlCommand		objCommand;\n");
                sb.Append("\n");

                // Append the try block
                sb.Append("\t\t\ttry {\n");

				sb.Append(GetCreateCommandSection(options.GetProcName(table, options.GetProcName(table, "SelectBy" + strPrimaryKeyList))));

                // Append the parameters
                sb.Append("\t\t\t\t// Create and append the parameters\n");
                for (intIndex = 0; intIndex < arrKeyList.Count; intIndex++) {
                    objField = (Field)arrKeyList[intIndex];
                    sb.Append("\t\t\t\t" + objField.CreateSqlParameter(false, false));
                    objField = null;
                }
                sb.Append("\n");

                // Append the execute statement
                sb.Append("\t\t\t\t// Execute the query and return the result\n");
                sb.Append("\t\t\t\treturn objCommand.ExecuteReader(CommandBehavior.CloseConnection);\n");
				
                // Append the catch block
                sb.Append("\t\t\t} catch (Exception objException) {\n");
                sb.Append("\t\t\t\tthrow (new Exception(\"" + table.Replace(" ", "_") + ".SelectBy" + strPrimaryKeyList + "\\n\\n\" + objException.Message));\n");
                sb.Append("\t\t\t}\n");
				
                // Append the method footer
                sb.Append("\t\t}\n\n\n");
			
                objField = null;
            }
        }
		
        private void CreateDAOListMethods(StringBuilder sb) {
            Field	objField;
            int			intIndex;
			
            // Append the method header
            sb.Append("\t\tprotected override String GetViewName() { \n");
            sb.Append("\t\t	return VIEW; \n");
            sb.Append("\t\t} \n");
            sb.Append("\t\t \n");
            sb.Append("\t\tpublic override ICollection GetList(IWhere whereClause, IOrderBy orderByClause, SqlConnection connection) { \n");
            sb.Append("\t\t	SqlDataReader dataReader = GetListReader(whereClause, orderByClause, connection); \n");
            sb.Append("\t\t	 \n");
            sb.Append("\t\t	ArrayList list = new ArrayList(); \n");
            sb.Append("\t\t	while (dataReader.Read()) { \n");
            sb.Append("\t\t		list.Add(GetDataObjectFromReader(dataReader)); \n");
            sb.Append("\t\t	} \n");
            sb.Append("\t\t	dataReader.Close(); \n");
            sb.Append("\t\t	return list; \n");
            sb.Append("\t\t} \n");

//            sb.Append("\t\tprivate SqlDataReader GetListReader() { \n");
//            sb.Append("\t\treturn GetListReader(\"\", \"\"); \n");
//            sb.Append("\t\t} \n");
//            sb.Append("\t\t \n");
//            sb.Append("\t\tprivate SqlDataReader GetListReader(String whereClause, String orderByClause) { \n");
//            sb.Append("\t\tSqlDataReader dataReader = null; \n");
//            sb.Append("\t\t \n");
//            sb.Append("\t\ttry { \n");
//            sb.Append("\t\t	Database data = new Database(); \n");
//            sb.Append("\t\t \n");
//            sb.Append("\t\t	String sql = \"select * from \" + VIEW;\n");
//            sb.Append("\t\t	if (whereClause.Trim().Length >0) { \n");
//            sb.Append("\t\t		sql = sql + \" where \" + whereClause; \n");
//            sb.Append("\t\t	} \n");
//            sb.Append("\t\t	if (orderByClause.Trim().Length >0) { \n");
//            sb.Append("\t\t		sql = sql + \" order by \" + orderByClause; \n");
//            sb.Append("\t\t	} \n");
//            sb.Append("\t\t \n");
//            sb.Append("\t\t	data.ExecuteSQLSelect (sql, out dataReader); \n");
//            sb.Append("\t\t} catch (Exception ex) { \n");
//            sb.Append("\t\t	Error.Log(ex.ToString()); \n");
//            sb.Append("\t\t} \n");
//            sb.Append("\t\t \n");
//            sb.Append("\t\treturn dataReader; \n");
//            sb.Append("\t\t}\n");
//            sb.Append("\n");
//
//            sb.Append("\t\t	public ICollection GetList() {\n");
//            sb.Append("\t\t		return GetList(\"\", \"\");\n");
//            sb.Append("\t\t	}\n");
//            sb.Append("\n");
//            sb.Append("\t\tpublic ICollection GetList(String whereClause) {\n");
//            sb.Append("\t\t	return GetList(whereClause, \"\");\n");
//            sb.Append("\t\t}\n");
//            sb.Append("\n");
//            sb.Append("\t\tpublic ICollection GetList(String whereClause, String orderByClause) {\n");
//            sb.Append("\t\t	SqlDataReader dataReader = GetListReader();\n");
//            sb.Append("\t\t    \n");
//            sb.Append("\t\t	ArrayList list = new ArrayList();\n");
//            sb.Append("\t\t	while (dataReader.Read()) {\n");
//            sb.Append("\t\t		list.Add(GetDataObjectFromReader(dataReader));\n");
//            sb.Append("\t\t	}\n");
//            sb.Append("\t\t	dataReader.Close();\n");
//            sb.Append("\t\t	return list;\n");
//            sb.Append("\t\t}\n");
//            sb.Append("\n");			

            sb.Append("\t\tpublic ").Append(options.GetDOClassName(table)).Append(" Load(Int32 id, SqlConnection connection) {\n");
			sb.Append("\t\t	SqlDataReader dataReader = GetListReader(new WhereClause(\"").Append(Field.GetIdentityColumn(fields)).Append("\", id), null, connection);\n");
            sb.Append("\t\t    \n");
            sb.Append("\t\t	dataReader.Read();\n");
            sb.Append("\t\t	return GetDataObjectFromReader(dataReader);\n");
            sb.Append("\t\t}\n");
            sb.Append("\n");			

            sb.Append("\t\tprivate ").Append(options.GetDOClassName(table)).Append(" GetDataObjectFromReader(SqlDataReader dataReader) {\n");
            sb.Append("\t\t	").Append(options.GetDOClassName(table)).Append(" data = new ").Append(options.GetDOClassName(table)).Append("();\n");

            for (intIndex = 0; intIndex < fields.Count; intIndex++) {
                objField = (Field)fields[intIndex];
				switch (objField.ReaderType.ToLower()) {
					case "bytes":
						sb.Append("\t\t\tdataReader.GetBytes(").Append(intIndex).Append(", 0, data.").Append(objField.ColumnName).Append(", 0, ").Append(objField.Length).Append(");\n");
						break;
					default: 
						sb.Append("\t\t\tdata.").Append(objField.ColumnName).Append(" = ").Append("dataReader.Get").Append(objField.ReaderType).Append("(").Append(intIndex).Append(");\n");
						break;
				}
                objField = null;
            }
            /*
            sb.Append("\t\t	if (dataReader.IsDBNull(7)) { \n");
            sb.Append("\t\t		data.LastUpdateUser = \"\";\n");
            sb.Append("\t\t	} else {\n");
            sb.Append("\t\t		data.LastUpdateUser = dataReader.GetString(7);\n");
            sb.Append("\t\t	}\n");
            */
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
			//                    sb.Append("\t\t\t\tobjCommand = new SqlCommand();\n");
			//					sb.Append("\t\t\t\tobjCommand.Connection = objConnection;\n");
			sb.Append("\t\t\t\tobjCommand = GetSqlCommand(connection, \"" + procName + "\", CommandType.StoredProcedure);\n");

			//sb.Append("\t\t\t\tobjCommand.CommandText = \"" + options.GetProcName(table, "DeleteBy" + strColumnName.Replace(" ", "_")) + "\";\n");
			//sb.Append("\t\t\t\tobjCommand.CommandText = \"" + procName + "\";\n");
			//sb.Append("\t\t\t\tobjCommand.CommandType = CommandType.StoredProcedure;\n");
			sb.Append("\n");
			return sb.ToString();
		}

    }
}

