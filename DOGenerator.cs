using System;
using System.Data;
using System.IO;
using System.Collections;
using System.Text;

namespace Spring2.DataTierGenerator {
    /// <summary>
    /// Generates stored procedures and associated data access code for the specified database.
    /// </summary>
    public class DOGenerator {
        private StreamWriter writer;
        private Configuration options;
        private String table;
        private ArrayList fields;

        /// <summary>
        /// Contructor for the Generator class.
        /// </summary>
        /// <param name="strConnectionString">Connecion string to a SQL Server database.</param>
        public DOGenerator(Configuration options, StreamWriter writer, String table, ArrayList fields) {
            this.options = options;
            this.writer = writer;
            this.table = table;
            this.fields = fields;
        }

        public void CreateDataObjectClass() {
            StreamWriter	objStreamWriter;
            StringBuilder	sb;
            string			strFileName;
			
            sb = new StringBuilder(4096);

            // Create the header for the class
            sb.Append("using System;\n");
            sb.Append("\n");
            sb.Append("namespace " + options.GetDONameSpace(table) + " {\n");
            sb.Append("\tpublic class " + options.GetDOClassName(table) + " {\n\n");

            Int32 intIndex;
            Field objField;
			
            // declaration of private member variables
            for (intIndex = 0; intIndex < fields.Count; intIndex++) {
                objField = (Field)fields[intIndex];
                //if (objField.IsIdentity == false && objField.IsRowGuidCol == false) {
                sb.Append("\t\tprivate ").Append(objField.ParameterType).Append(" ").Append(objField.GetFieldFormat()).Append(";\n");
                //}
                objField = null;
            }
            sb.Append("\n");

            // accessor methods
            for (intIndex = 0; intIndex < fields.Count; intIndex++) {
                objField = (Field)fields[intIndex];
                //if (objField.IsIdentity == false && objField.IsRowGuidCol == false) {
                sb.Append("\t\tpublic ").Append(objField.ParameterType).Append(" ").Append(objField.GetMethodFormat()).Append(" {\n");
                sb.Append("\t\t\tget { return this.").Append(objField.GetFieldFormat()).Append("; }\n");
                sb.Append("\t\t\tset { this.").Append(objField.GetFieldFormat()).Append(" = value; }\n");
                sb.Append("\t\t}\n\n");
                //}
                objField = null;
            }

		
            // Close out the class and namespace
            sb.Append("\t}\n");
            sb.Append("}\n");

            // Create the output stream
            strFileName = options.DoClassDirectory + "\\" + options.GetDOClassName(table) + ".cs";
            if (File.Exists(strFileName))
                File.Delete(strFileName);
            objStreamWriter = new StreamWriter(strFileName);
            objStreamWriter.Write(sb.ToString());
            objStreamWriter.Close();
            objStreamWriter = null;
            sb = null;
        }

    }
}
