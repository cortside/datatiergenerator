using System;
using System.Data;
using System.IO;
using System.Collections;
using System.Text;
using System.Windows.Forms;

namespace Spring2.DataTierGenerator {
	public class GeneratorBase {

		protected StreamWriter writer;
		protected Configuration options;
		protected Entity entity;
		protected ArrayList fields;

		protected GeneratorBase() {
		}

		public GeneratorBase(Configuration options, StreamWriter writer, Entity entity, ArrayList fields) {
			this.options = options;
			this.writer = writer;
			this.entity = entity;
			this.fields = fields;
		}

		/// <summary>
		/// Internal helper method to write stored procedure to file based on options.SingleFile
		/// </summary>
		/// <param name="strStoredProcName">Name of stored procedure (used to name file if not outputting as single file)</param>
		/// <param name="strStoredProcText">Text to create stored procedure.</param>
		protected void WriteToFile(String fileName, String text) {
			StringBuilder sb = new StringBuilder();

			if (!options.SingleFile) {
				if (File.Exists(fileName))
					File.Delete(fileName);
				writer = new StreamWriter(fileName);
			} else {
				sb.Append("/*\n");
				sb.Append("******************************************************************************\n");
				sb.Append("******************************************************************************\n");
				sb.Append("*/\n");
			}

			sb.Append(text);
			writer.Write(sb.ToString());

			if (!options.SingleFile) {
				writer.Close();
				writer = null;
			}
		}

	}

}
