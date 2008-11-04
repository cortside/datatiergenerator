using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Spring2.DataTierGenerator.Generator.Styler {
    public class CommandLineStyler : BaseStyler, IStyler {
        ProcessStartInfo startInfo = null;
        private string inFile = "";

        public CommandLineStyler(Hashtable options) {

            startInfo = new ProcessStartInfo();
            if (options.Contains("FileName")) {
                startInfo.FileName = options["FileName"].ToString();
            } else {
                throw new System.IO.FileNotFoundException("Filename to execute was not specified.");
            }
            if (options.Contains("Arguments")) {
                startInfo.Arguments = options["Arguments"].ToString();
            }
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
        }

        public string File {
            get { return inFile; }
            set { inFile = value; }
        }

        public string Style(string input) {
            Process p = Process.Start(startInfo);
            p.StandardInput.Write(input);
            p.StandardInput.Close();
            string output = p.StandardOutput.ReadToEnd();
            string error = p.StandardError.ReadToEnd();
            p.WaitForExit();
            p.Close();
            if (error.Trim().Length > 0) {
                WriteToLog(error);
            }
            return output;
        }
    }
}
