using System;
using System.Data;
using System.IO;
using System.Collections;
using System.Text;
using System.Windows.Forms;

namespace Spring2.DataTierGenerator {
    /// <summary>
    /// Generates stored procedures and associated data access code for the specified database.
    /// </summary>
    public class SQLGenerator : GeneratorBase {

        /// <summary>
        /// Contructor for the Generator class.
        /// </summary>
        /// <param name="strConnectionString">Connecion string to a SQL Server database.</param>
        public SQLGenerator(Configuration options, StreamWriter writer, Entity entity, ArrayList fields) : base(options, writer, entity, fields) {
        }
		
        /// <summary>
        /// Creates an insert stored procedure SQL script for the specified table
        /// </summary>
        /// <param name="table">Name of the table the stored procedure should be generated from.</param>
        /// <param name="fields">ArrayList object containing one or more Field objects as defined in the table.</param>
        public void CreateInsertStoredProcedure() {
            Field		objField;
            int				i;
            StringBuilder	sb;
            String			strProcName;
			
            // Create the SQL for the stored procedure
            sb = new StringBuilder(1024);

            strProcName = options.GetProcName(entity.Name, "Insert");

            sb.Append("CREATE PROCEDURE " + strProcName + "\n");

            // Create the parameter list
            Boolean first = true;
            for (i = 0; i < fields.Count; i++) {
                objField = (Field)fields[i];
                if (!objField.IsIdentity && !objField.IsViewColumn) {
                    if (!first) {
                        sb.Append(",\n");
                    } else {
                        first=false;
                    }
                    sb.Append("\t").Append(objField.CreateParameterString(false));
                }
                objField = null;
            }
            sb.Append("\n\nAS\n\n");
			
            for (i = 0; i < fields.Count; i++) {
                objField = (Field)fields[i];
                if (!objField.IsViewColumn) {
                    if (objField.IsRowGuidCol) {
                        sb.Append("SET @" + objField.ColumnName.Replace(" ", "_") + " = @@NEWID()\n\n");
                        break;
                    }
                }
            }
			
            sb.Append("INSERT INTO [" + entity.Name + "] (\n");
			
            // Create the parameter list
            first = true;
            for (i = 0; i < fields.Count; i++) {
                objField = (Field)fields[i];
                if (!objField.IsViewColumn) {
                    // Is the current field an identity column?
                    if (objField.IsIdentity == false) {
                        if (!first) {
                            sb.Append(",\n");
                        } else {
                            first=false;
                        }
                        sb.Append("\t[" + objField.ColumnName + "]");
                    }
                }
                objField = null;
            }
            sb.Append(")\nVALUES (\n");

            // Create the parameter list
            first=true;
            for (i = 0; i < fields.Count; i++) {
                objField = (Field)fields[i];
                if (!objField.IsViewColumn) {
                    // Is the current field an identity column?
                    if (objField.IsIdentity == false) {
                        if (!first) {
                            sb.Append(",\n");
                        } else {
                            first=false;
                        }
                        sb.Append("\t@" + objField.ColumnName.Replace(" ", "_"));
                    }
                }
                objField = null;
            }
            sb.Append(")\n");

            sb.Append("\n\nif @@rowcount <> 1 or @@error!=0\n");
            sb.Append("    BEGIN\n");
            sb.Append("        RAISERROR  20000 '").Append(strProcName).Append(": Unable to insert new row into ").Append(entity.Name).Append(" table '\n");
            //sb.Append("        ROLLBACK TRAN\n");
            sb.Append("        RETURN(-1)\n");
            sb.Append("    END\n");


            // Should we include a line for returning the identity?
/*            for (i = 0; i < fields.Count; i++) {
                objField = (Field)fields[i];
                if (!objField.IsViewColumn) {
                    // Is the current field an identity column?
                    if (objField.IsIdentity)
                        sb.Append("\nSET @" + objField.ColumnName.Replace(" ", "_") + " = @@IDENTITY\n");
                }
                objField = null;
            }
*/
			sb.Append("\nreturn @@IDENTITY\n");

            // Write out the stored procedure
            WriteProcToFile(strProcName, sb.ToString() + "\nGO\n\n");
            sb = null;
        }

		
        /// <summary>
        /// Creates an update stored procedure SQL script for the specified table
        /// </summary>
        /// <param name="table">Name of the table the stored procedure should be generated from.</param>
        /// <param name="fields">ArrayList object containing one or more Field objects as defined in the table.</param>
        public void CreateUpdateStoredProcedure() {
            Field objField;
            Field objOldField;
            Field objNewField;
            int i;
            StringBuilder sb = new StringBuilder(1024);
            int intWhereClauseCount;

            sb.Append("CREATE PROCEDURE " + options.GetProcName(entity.Name, "Update") + "\n\n");

            // Create the parameter list
			Boolean first = true;
				for (i = 0; i < fields.Count; i++) {
					objField = (Field)fields[i];
					if (!objField.IsViewColumn) {
//						if (!first) {
//							sb.Append(",\n");
//						} else {
//							first=false;
//						}
						//MessageBox.Show("isPrimaryKey=" + objField.IsPrimaryKey.ToString() + "\nisIdentity=" + objField.IsIdentity.ToString() + "\n", objField.ColumnName);

						//if (!objField.IsPrimaryKey && !objField.IsIdentity && !objField.IsRowGuidCol) {
							if (!first) {
								sb.Append(",\n");
							} else {
								first=false;
							}

							sb.Append(objField.CreateParameterString(false));
						//}
						/*							objOldField = objField.Copy();
													objOldField.ColumnName = "Old" + objOldField.ColumnName;
					
													objNewField = objField.Copy();
													objNewField.ColumnName = "New" + objNewField.ColumnName;
					
													sb.Append(objOldField.CreateParameterString(false));
													sb.Append(",\n");
													sb.Append(objNewField.CreateParameterString(false));
												} else {
													sb.Append(objField.CreateParameterString(false));
												} */
					}
					objField = null;
				}
				sb.Append("\n");

            sb.Append("\nAS\n");
            sb.Append("\nUPDATE\n\t[" + entity.Name + "]\n");
            sb.Append("SET\n");
			
            // Create the set statement
            first = true;
            for (i = 0; i < fields.Count; i++) {
                objField = (Field)fields[i];
                if (!objField.IsViewColumn) {
                    if (objField.IsIdentity == false && objField.IsRowGuidCol == false) {
//                        if (!first) {
//                            sb.Append(",\n");
//                        } else {
//                            first=false;
//                        }
                        if (objField.IsPrimaryKey) {
                            //sb.Append("\t[" + objField.ColumnName + "] = @New" + objField.ColumnName.Replace(" ", "_"));
                        } else {
							if (!first) {
								sb.Append(",\n");
							} else {
								first=false;
							}

                            sb.Append("\t[" + objField.ColumnName + "] = @" + objField.ColumnName.Replace(" ", "_"));
                        }
                    }
                }
                objField = null;
            }
            sb.Append("\n");
            sb.Append("WHERE\n");
			
            // Create the where clause
            intWhereClauseCount = 0;
			for (i = 0; i < fields.Count; i++) {
				objField = (Field)fields[i];
				if (!objField.IsViewColumn) {
					if (objField.IsIdentity || objField.IsRowGuidCol || objField.IsPrimaryKey) {
						intWhereClauseCount++;
				
						if (i == (fields.Count  - 1))
							sb.Append("\t");
						else if (i < (fields.Count - 1) && i > 0 && intWhereClauseCount > 1)
							sb.Append("\tAND ");
						else
							sb.Append("\t");
					
//						if (objField.IsPrimaryKey && !objField.IsIdentity && objField.IsRowGuidCol)
//							sb.Append("[" + objField.ColumnName + "] = @Old" + objField.ColumnName.Replace(" ", "_") + "\n");
//						else
							sb.Append("[" + objField.ColumnName + "] = @" + objField.ColumnName.Replace(" ", "_") + "\n");
					}
				}
				objField = null;
			}

            sb.Append("\n\nif @@ROWCOUNT <> 1\n");
            sb.Append("    BEGIN\n");
            //sb.Append("        RAISERROR  ('").Append(options.GetProcName(entity.Name, "Update")).Append(": ").Append(Field.GetIdentityColumn(fields).ColumnName).Append(" %d not found to update', 16,1, @").Append(Field.GetIdentityColumn(fields).ColumnName).Append(")\n");
			sb.Append("        RAISERROR  ('").Append(options.GetProcName(entity.Name, "Update")).Append(":  update was expected to update a single row and updated %d rows', 16,1, @@ROWCOUNT)\n");
            sb.Append("        RETURN(-1)\n");
            sb.Append("    END\n");


            // Write out the stored procedure
            WriteProcToFile(options.GetProcName(entity.Name, "Update"), sb.ToString() + "\nGO\n\n");
            sb = null;
        }


        /// <summary>
        /// Creates delete stored procedures for the specified table
        /// </summary>
        /// <param name="table">Name of the table the stored procedure should be generated from.</param>
        /// <param name="fields">ArrayList object containing one or more Field objects as defined in the table.</param>
        public void CreateDeleteStoredProcedures() {
            ArrayList		arrKeyList;
            Field		objField;
            int				i;
            string			strColumnName;
            StringBuilder	sb;
            string			strPrimaryKeyList;

            // Create the array list of key fields
            strPrimaryKeyList = "";
            arrKeyList = new ArrayList();
            for (i = 0; i < fields.Count; i++) {
                objField = (Field)fields[i];
                if (objField.IsPrimaryKey) {
                    arrKeyList.Add(objField);
                    strPrimaryKeyList += objField.ColumnName.Replace(" ", "_") + "_";
                }
                objField = null;
            }
			
            // Trim off the last underscore
            if (strPrimaryKeyList.Length > 0)
                strPrimaryKeyList = strPrimaryKeyList.Substring(0, strPrimaryKeyList.Length - 1);

            /************************************************************************************/
            // Create the stored procedures, with parameters for each identity, RowGuidCol, primary key, or foreign key column
            for (i = 0; i < fields.Count; i++) {
                objField = (Field)fields[i];

                // Is is an identity or uniqueidentifier column?
                if (objField.IsIdentity || (options.GenerateProcsForForeignKey && (objField.IsRowGuidCol || objField.IsPrimaryKey || objField.IsForeignKey))) {

					// if this option is on, only generate the PK 
					if (options.GenerateOnlyPrimaryDeleteStoredProc) {
						arrKeyList.Clear();
					}

                    // Format the column name to make sure the first character is upper case
                    strColumnName = objField.ColumnName;
                    strColumnName = strColumnName.Substring(0, 1).ToUpper() + strColumnName.Substring(1);
				
                    // Create the SQL for the stored procedure
                    String			strProcName;
			
                    // Create the SQL for the stored procedure
                    sb = new StringBuilder(1024);
                    
                    if (objField.IsIdentity) {
                        strProcName = options.GetProcName(entity.Name, "Delete");
                    } else {
                        strProcName = options.GetProcName(entity.Name, "DeleteBy" + strColumnName.Replace(" ", "_"));
                    }

                    sb.Append("CREATE PROCEDURE " + strProcName + "\n\n");

                    sb.Append("@" + objField.ColumnName.Replace(" ", "_") + " " + objField.SqlType);
                    sb.Append("\n\nAS\n\n");

                    sb.Append("if not exists(SELECT ").Append(objField.ColumnName).Append(" FROM ").Append(entity.Name).Append(" WHERE (").Append(objField.ColumnName).Append(" = @").Append(objField.ColumnName).Append("))\n");
                    sb.Append("    BEGIN\n");
                    sb.Append("        RAISERROR  ('").Append(strProcName).Append(": ").Append(objField.ColumnName).Append(" %d not found to delete', 16,1, @").Append(objField.ColumnName).Append(")\n");
                    sb.Append("        RETURN(-1)\n");
                    sb.Append("    END\n\n");


                    sb.Append("DELETE\n");
                    sb.Append("FROM\n\t[" + entity.Name + "]\n");
                    sb.Append("WHERE \n");
                    sb.Append("\t[" + objField.ColumnName + "] = @" + objField.ColumnName.Replace(" ", "_") + "\n");

                    // Write out the stored procedure
                    WriteProcToFile(strProcName, sb.ToString() + "\nGO\n\n");
                    sb = null;
                }
            }
				
            /************************************************************************************/
            // Create the stored procedure for a composite primary key
            if (arrKeyList.Count > 1) {
                // Create the SQL for the stored procedure
                String			strProcName;
			
                // Create the SQL for the stored procedure
                sb = new StringBuilder(1024);

				if (options.GenerateOnlyPrimaryDeleteStoredProc) {
					strProcName = options.GetProcName(entity.Name, "Delete");
				} else {
					strProcName = options.GetProcName(entity.Name, "DeleteBy" + strPrimaryKeyList);
				}

                sb.Append("CREATE PROCEDURE " + strProcName + "\n\n");				
				
                //// Is this a self-referencing key?
                //sb.Append("CREATE PROCEDURE proc" + entity.Name.Replace(" ", "_") + "DeleteBy" + strPrimaryKeyList + "\n\n");

                // Create the parameter list
                for (i = 0; i < arrKeyList.Count; i++) {
                    objField = (Field)arrKeyList[i];
                    sb.Append("@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.SqlType);
                    objField = null;
					
                    if (i == arrKeyList.Count-1)
                        sb.Append("\n");
                    else
                        sb.Append(",\n");
                }
				
                sb.Append("\n\nAS\n\n");

				// Create the where clause
				StringBuilder where = new StringBuilder();
				for (i = 0; i < arrKeyList.Count; i++) {
					objField = (Field)arrKeyList[i];
					where.Append("\t[" + objField.ColumnName + "] = @" + objField.ColumnName.Replace(" ", "_"));
					
					if (i != arrKeyList.Count-1)
						where.Append(" and \n");
					else
						where.Append("\n");
				}


				sb.Append("if not exists(SELECT ").Append("*").Append(" FROM ").Append(entity.Name).Append(" WHERE (").Append(where.ToString()).Append("))\n");
				sb.Append("    BEGIN\n");
				sb.Append("        RAISERROR  ('").Append(strProcName).Append(": ").Append("record").Append(" not found to delete', 16,1)\n");
				sb.Append("        RETURN(-1)\n");
				sb.Append("    END\n\n");


                sb.Append("DELETE\n");
                sb.Append("FROM\n\t[" + entity.Name + "]\n");
                sb.Append("WHERE \n");

/*                // Create the where clause
                for (i = 0; i < arrKeyList.Count; i++) {
                    objField = (Field)fields[i];
                    sb.Append("\t[" + objField.ColumnName + "] = @" + objField.ColumnName.Replace(" ", "_"));
					
                    if (i != fields.Count)
                        sb.Append(",\n");
                    else
                        sb.Append("\n");
                } */
				sb.Append(where.ToString());
				
                // Write out the stored procedure
                WriteProcToFile(strProcName, sb.ToString() + "\nGO\n\n");
                sb = null;
            }
        }


        /// <summary>
        /// Creates a select stored procedure SQL script for the specified table by the table's identity or uniqueidentifier column,
        ///	and select stored procedures for all foreign keys columns in the table.
        /// </summary>
        /// <param name="table">Name of the table the stored procedure should be generated from.</param>
        /// <param name="fields">ArrayList object containing one or more Field objects as defined in the table.</param>
        public void CreateSelectStoredProcedures() {
            ArrayList		arrKeyList;
            Field		objField;
            string			strColumnName;
            int				i;
            StringBuilder	sb;
            string			strPrimaryKeyList;

            /************************************************************************************/
            // Create the full list stored procedure
            String			strProcName;
			
            // Create the SQL for the stored procedure
            sb = new StringBuilder(1024);

            strProcName = options.GetProcName(entity.Name, "Select");

            sb.Append("CREATE PROCEDURE " + strProcName + "\n\n");
            sb.Append("\n\nAS\n\n");
            sb.Append("SELECT\n\t*\n");
            sb.Append("FROM\n\t[");
            if (options.UseViews)
                sb.Append("vw");
            sb.Append(entity.Name);
            sb.Append("]\n");
			
            // Write out the stored procedure
            WriteProcToFile(strProcName, sb.ToString() + "\nGO\n\n");
            sb = null;

            // Create the array list of key fields
            strPrimaryKeyList = "";
            arrKeyList = new ArrayList();
            for (i = 0; i < fields.Count; i++) {
                objField = (Field)fields[i];
                if (objField.IsPrimaryKey) {
                    arrKeyList.Add(objField);
                    strPrimaryKeyList += objField.ColumnName.Replace(" ", "_") + "_";
                }
                objField = null;
            }
			
            // Trim off the last underscore
            if (strPrimaryKeyList.Length > 0)
                strPrimaryKeyList = strPrimaryKeyList.Substring(0, strPrimaryKeyList.Length - 1);

            /************************************************************************************/
            // Create the stored procedures, with parameters for each identity, RowGuidCol, primary key, or foreign key column
            for (i = 0; i < fields.Count; i++) {
                objField = (Field)fields[i];

                // Is is an identity or uniqueidentifier column?
                if (objField.IsIdentity || (options.GenerateProcsForForeignKey && (objField.IsRowGuidCol || objField.IsPrimaryKey || objField.IsForeignKey))) {

                    // Format the column name to make sure the first character is upper case
                    strColumnName = objField.ColumnName;
                    strColumnName = strColumnName.Substring(0, 1).ToUpper() + strColumnName.Substring(1);
				
                    // Create the SQL for the stored procedure
                    sb = new StringBuilder(1024);

                    strProcName = options.GetProcName(entity.Name, "SelectBy" + strColumnName.Replace(" ", "_"));
                    sb.Append("CREATE PROCEDURE " + strProcName + "\n\n");

                    sb.Append("@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.SqlType);
                    sb.Append("\n\nAS\n\n");
                    sb.Append("SELECT\n\t*\n");
                    sb.Append("FROM\n\t[");
                    if (options.UseViews)
                        sb.Append("vw");
                    sb.Append(entity.Name);
                    sb.Append("]\n");

                    sb.Append("WHERE \n");
                    sb.Append("\t[" + objField.ColumnName + "] = @" + objField.ColumnName.Replace(" ", "_") + "\n");
					
                    // Write out the stored procedure
                    WriteProcToFile(strProcName, sb.ToString() + "\nGO\n\n");
                    sb = null;
                }
            }
				
            /************************************************************************************/
            // Create the stored procedure for a composite primary key
            if (arrKeyList.Count > 1) {
                // Create the SQL for the stored procedure
                sb = new StringBuilder(1024);

                strProcName = options.GetProcName(entity.Name, "SelectBy" + strPrimaryKeyList);
                sb.Append("CREATE PROCEDURE " + strProcName + "\n\n");
				
                //// Is this a self-referencing key?
                //sb.Append("CREATE PROCEDURE proc" + entity.Name.Replace(" ", "_") + "SelectBy" + strPrimaryKeyList + "\n\n");

                // Create the parameter list
                for (i = 0; i < arrKeyList.Count; i++) {
                    objField = (Field)arrKeyList[i];
                    sb.Append("@" + objField.ColumnName.Replace(" ", "_") + "\t" + objField.SqlType);
                    objField = null;
					
                    if (i == arrKeyList.Count)
                        sb.Append("\n");
                    else
                        sb.Append(",\n");
                }
				
                sb.Append("\n\nAS\n\n");
                sb.Append("SELECT\n\t*\n");
                sb.Append("FROM\n\t[" + entity.Name + "]\n");
                sb.Append("WHERE \n");

                // Create the where clause
                for (i = 0; i < arrKeyList.Count; i++) {
                    objField = (Field)fields[i];
                    sb.Append("\t[" + objField.ColumnName + "] = @" + objField.ColumnName.Replace(" ", "_"));
					
                    if (i != fields.Count)
                        sb.Append(",\n");
                    else
                        sb.Append("\n");
                }
				
                // Write out the stored procedure
                WriteProcToFile(strProcName, sb.ToString() + "\nGO\n\n");
                sb = null;
            }
        }

        public void CreateView() {
            Field		objField;
            int				i;
            StringBuilder	sb;
            String			strProcName;
			
            // Create the SQL for the stored procedure
            sb = new StringBuilder(1024);

            strProcName = "vw" + entity.Name;

            sb.Append("if exists (select * from sysobjects where id = object_id(N'[" + strProcName + "]') and OBJECTPROPERTY(id, N'IsView') = 1)\n");
            sb.Append("drop view [" + strProcName + "]\n");
            sb.Append("GO\n");
            sb.Append("\n");
            sb.Append("create view " + strProcName + "\n");
            sb.Append("\n");
            sb.Append("AS\n");
            sb.Append("\n");
            sb.Append("SELECT ");
			
            // Create the parameter list
            for (i = 0; i < fields.Count; i++) {
                objField = (Field)fields[i];
                if (!objField.IsViewColumn) {
                    if (i>0) {
                        sb.Append(",\n");
                    }
                    sb.Append("\t[" + objField.ColumnName + "]");
                }
                objField = null;
            }
            sb.Append("\n");			
            sb.Append("FROM\n\t[");
            sb.Append(entity.Name);
            sb.Append("]\n");

            // Write out the stored procedure
			String fileName = options.RootDirectory + options.SqlScriptDirectory + "\\view\\" + strProcName + ".view.sql";
            WriteToFile(fileName, sb.ToString() + "\nGO\n\n");
            sb = null;
        }

        /// <summary>
        /// Internal helper method to write stored procedure to file based on options.SingleFile
        /// </summary>
        /// <param name="strStoredProcName">Name of stored procedure (used to name file if not outputting as single file)</param>
        /// <param name="strStoredProcText">Text to create stored procedure.</param>
        private void WriteProcToFile(String strStoredProcName, String strStoredProcText) {
            StringBuilder sb = new StringBuilder();
            
            if (options.ScriptDropStatement) {
                sb.Append("if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[" + strStoredProcName + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)\n");
                sb.Append("drop procedure [dbo].[" + strStoredProcName + "]\n");
                sb.Append("GO\n\n");
            }

            sb.Append(strStoredProcText);

			String fileName = options.RootDirectory + options.SqlScriptDirectory + "\\proc\\" + strStoredProcName + ".proc.sql";
            WriteToFile(fileName, sb.ToString());
        }

    }
}

