using System;
using System.Data;
using System.IO;
using System.Collections;
using System.Text;

namespace Spring2.DataTierGenerator {
    /// <summary>
    /// Generates stored procedures and associated data access code for the specified database.
    /// </summary>
    public class DOGenerator : GeneratorBase {

		/// <summary>
        /// Contructor for the Generator class.
        /// </summary>
        /// <param name="strConnectionString">Connecion string to a SQL Server database.</param>
        public DOGenerator(Configuration options, StreamWriter writer, Entity entity, ArrayList fields) : base(options, writer, entity, fields) {
        }

        public void CreateDataObjectClass() {
            StringBuilder sb = new StringBuilder(4096);
			
            // Create the header for the class
            sb.Append("using System;\n");

			if (options.UseDataTypes) {
				Hashtable namespaces = new Hashtable();
				foreach (Field field in fields) {
					if (!field.DataTypeNamespace.Equals(String.Empty) && !namespaces.Contains(field.DataTypeNamespace)) {
						namespaces.Add(field.DataTypeNamespace, field.DataTypeNamespace);
					}
				}
				foreach (Object o in namespaces.Keys) {
					sb.Append("using ").Append(o.ToString()).Append(";\n");
				}
			}

            sb.Append("\n");
            sb.Append("namespace " + options.GetDONameSpace(entity.Name) + " {\n");
            sb.Append("\tpublic class " + options.GetDOClassName(entity.Name) + " : Spring2.Core.DataObject.DataObject {\n\n");

            // declaration of private member variables
            for (Int32 intIndex = 0; intIndex < fields.Count; intIndex++) {
                Field objField = (Field)fields[intIndex];
                //if (objField.IsIdentity == false && objField.IsRowGuidCol == false) {
                sb.Append("\t\tprivate ");
                if (options.UseDataTypes && objField.DataType.Length>0) {
                    sb.Append(objField.DataTypeClass);
                    sb.Append(" ").Append(objField.GetFieldFormat()).Append(" = ").Append(objField.DataTypeClass).Append(".DEFAULT;\n");
                } else if (objField.Type.Length>1) {
                    sb.Append(objField.Type);
                    sb.Append(" ").Append(objField.GetFieldFormat()).Append(" = new ").Append(objField.ConcreteType).Append("();\n");
				} else  {
					sb.Append(objField.ParameterType);
					sb.Append(" ").Append(objField.GetFieldFormat()).Append(";\n");
				}
                //}
                objField = null;
            }
            sb.Append("\n");

            // accessor methods
            for (Int32 intIndex = 0; intIndex < fields.Count; intIndex++) {
                Field objField = (Field)fields[intIndex];
                //if (objField.IsIdentity == false && objField.IsRowGuidCol == false) {
                sb.Append("\t\tpublic ");
				if (options.UseDataTypes) {
					sb.Append(objField.DataTypeClass);
				} else {
					sb.Append(objField.ParameterType);
				}
				sb.Append(" ").Append(objField.GetMethodFormat()).Append(" {\n");
                sb.Append("\t\t\tget { return this.").Append(objField.GetFieldFormat()).Append("; }\n");
                sb.Append("\t\t\tset { this.").Append(objField.GetFieldFormat()).Append(" = value; }\n");
                sb.Append("\t\t}\n\n");
                //}
                objField = null;
            }

		
            // Close out the class and namespace
            sb.Append("\t}\n");
            sb.Append("}\n");

			WriteToFile(options.RootDirectory + options.DoClassDirectory + "\\" + options.GetDOClassName(entity.Name) + ".cs", sb.ToString());
        }

    }
}
