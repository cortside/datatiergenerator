using System;

namespace Spring2.DataTierGenerator {
    public class Configuration {
        private String database;
		private String rootDirectory;
        private String sqlScriptDirectory;
        private String daoClassDirectory;
        private String doClassDirectory;
        private String connectionString;
        private Boolean singleFile;
        private Boolean createViews;
        private Boolean useViews;
        private Boolean createDataObjects;
        private Boolean scriptDropStatement;
        private String storedProcNameFormat;
        private String projectNameSpace;
        private Boolean generateProcsForForeignKey;
        private Boolean generateSelectStoredProcs;
		private Boolean generateOnlyPrimaryDeleteStoredProc;
		private Boolean allowUpdateOfPrimaryKey;

        public Configuration() {
            this.sqlScriptDirectory = "sql";
            this.daoClassDirectory = "DAO";
            this.doClassDirectory = "DataObject";
            this.generateProcsForForeignKey = false;
            this.generateSelectStoredProcs = false;
			this.generateOnlyPrimaryDeleteStoredProc = true;
			this.rootDirectory = "c:\\data\\work\\seamlessweb\\manhattan\\src\\";
			this.allowUpdateOfPrimaryKey = false;
        }

// properties
        public String Database {
            get { return this.database; }
            set { this.database = value; }
        }

		public String RootDirectory {
			get { return this.rootDirectory; }
			set { this.rootDirectory = value; }
		}

        public String SqlScriptDirectory {
            get { return this.sqlScriptDirectory; }
            set { this.sqlScriptDirectory = value; }
        }
        public String DaoClassDirectory {
            get { return this.daoClassDirectory; }
            set { this.daoClassDirectory = value; }
        }
        public String DoClassDirectory {
            get { return this.doClassDirectory; }
            set { this.doClassDirectory = value; }
        }

        public String ConnectionString {
            get { return this.connectionString; }
            set { this.connectionString = value; }
        }

        public String StoredProcNameFormat {
            get { return this.storedProcNameFormat; }
            set { this.storedProcNameFormat = value; }
        }

        public String ProjectNameSpace {
            get { return this.projectNameSpace; }
            set { this.projectNameSpace = value; }
        }

        public Boolean SingleFile {
            get { return this.singleFile; }
            set { this.singleFile = value; }
        }

        public Boolean CreateViews {
            get { return this.createViews; }
            set { this.createViews = value; }
        }

        public Boolean UseViews {
            get { return this.useViews; }
            set { this.useViews = value; }
        }

        public Boolean CreateDataObjects {
            get { return this.createDataObjects; }
            set { this.createDataObjects = value; }
        }

        public Boolean ScriptDropStatement {
            get { return this.scriptDropStatement; }
            set { this.scriptDropStatement = value; }
        }

        public Boolean GenerateProcsForForeignKey {
            get { return this.generateProcsForForeignKey; }
            set { this.generateProcsForForeignKey = value; }
        }

        public Boolean GenerateSelectStoredProcs {
            get { return this.generateSelectStoredProcs; }
            set { this.generateSelectStoredProcs = value; }
        }

		public Boolean GenerateOnlyPrimaryDeleteStoredProc {
			get { return this.generateOnlyPrimaryDeleteStoredProc; }
			set { this.generateOnlyPrimaryDeleteStoredProc = value; }
		}

		public Boolean AllowUpdateOfPrimaryKey {
			get { return this.allowUpdateOfPrimaryKey; }
			set { this.allowUpdateOfPrimaryKey = value; }
		}

// methods
        public String GetProcName(String table, String type) {
            String s;

            s = "proc" + table.Replace(" ", "_") + type;
            s = "sp" + table.Replace(" ", "_") + "_" + type;

            return s;
        }

        public String GetDAONameSpace(String table) {
            String s;

            s = this.Database + ".DataAccess." + table.Replace(" ", "_");
            s = this.ProjectNameSpace  + ".DAO";

            return s;
        }

        public String GetDONameSpace(String table) {
            String s;

            if (this.Database != null && table != null)
                s = this.Database + ".DataAccess." + table.Replace(" ", "_");
            s = this.ProjectNameSpace  + ".DataObject";

            return s;
        }

        public String GetDAOClassName(String table) {
            String s;

            s = "cls" + table.Replace(" ", "_");
            s = table.Replace(" ", "_") + "DAO";

            return s;
        }

        public String GetDOClassName(String table) {
            String s;

            s = "cls" + table.Replace(" ", "_");
            s = table.Replace(" ", "_") + "Data";

            return s;
        }

    }
}
