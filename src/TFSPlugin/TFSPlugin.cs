using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using Spring2.DataTierGenerator.PluginInterface;

namespace TFSPlugin {
    public class TFSPlugin : IPreAndPostProcessFile {

        private Workspace workspace = null;

        public TFSPlugin() {
            String tfsName = ConfigurationManager.AppSettings["TfsRespoitoryServer"].ToString();//"24.39.144.139";
            TeamFoundationServer tfs = new TeamFoundationServer(tfsName);
            VersionControlServer versionControl = (VersionControlServer)tfs.GetService(typeof(VersionControlServer));

            versionControl.NonFatalError += TFSPlugin.OnNonFatalError;
            versionControl.Getting += TFSPlugin.OnGetting;
            versionControl.BeforeCheckinPendingChange += TFSPlugin.OnBeforeCheckinPendingChange;
            versionControl.NewPendingChange += TFSPlugin.OnNewPendingChange;

	    String workPath = ConfigurationManager.AppSettings["TfsRespoitoryPath"].ToString();//@"C:\Data\Work\Seamlessweb\Manhattan";
            workspace = versionControl.GetWorkspace(workPath);
        }

        public void PreWriteExisting(String file) {
            CheckOutFile(file);
        }

        public void PreWriteNew(String file) {
        }

        public void PostWriteNew(String file) {
            AddFile(file);
        }

        public void PostWriteExisting(String file) {
            AddFile(file);
        }


        private void CheckOutFile(String filePath) {
            try {
                workspace.PendEdit(filePath);
            } finally {
            }
        }

        private void AddFile(String filePath) {
            try {
                workspace.PendAdd(filePath);
            } catch (Exception ex) {
                String x = ex.Message;
            }
        }

        internal static void OnNonFatalError(Object sender, ExceptionEventArgs e) {
            if (e.Exception != null) {
                Console.Error.WriteLine("Non-fatal exception: " + e.Exception.Message);
            } else {
                Console.Error.WriteLine("Non-fatal failure: " + e.Failure.Message);
            }
        }

        internal static void OnGetting(Object sender, GettingEventArgs e) {
            Console.WriteLine("Getting: " + e.TargetLocalItem + ", status: " + e.Status);
        }

        internal static void OnBeforeCheckinPendingChange(Object sender, ProcessingChangeEventArgs e) {
            Console.WriteLine("Checking in " + e.PendingChange.LocalItem);
        }

        internal static void OnNewPendingChange(Object sender, PendingChangeEventArgs e) {
            Console.WriteLine("Pending " + PendingChange.GetLocalizedStringForChangeType(e.PendingChange.ChangeType) + " on " + e.PendingChange.LocalItem);
        }

    }
}
