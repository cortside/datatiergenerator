using System;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Spring2.DataTierGenerator {
    public class Configuration {
		private String server;
        private String database;
		private String user;
		private String password;
		private String rootDirectory;
        private String sqlScriptDirectory;
        private String daoClassDirectory;
        private String doClassDirectory;
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
		private String xmlConfigFilename;
		private Boolean useDataTypes;

		private Boolean autoDiscoverEntities;
		private Boolean autoDiscoverProperties;
		private Boolean autoDiscoverAttributes;

        public Configuration() {
			InitDefaults();
        }

		public Configuration(XmlNode root) {
			InitDefaults();
			if (root.HasChildNodes) {
				for (Int32 i=0; i<root.ChildNodes.Count; i++) {
					XmlNode node = root.ChildNodes[i];
					switch(node.Attributes["name"].Value.ToLower()) {
						case "projectnamespace":
							this.projectNameSpace = node.Attributes["value"].Value;
							break;
						case "server":
							this.server = node.Attributes["value"].Value;
							break;
						case "database":
							this.database = node.Attributes["value"].Value;
							break;
						case "user":
							this.user = node.Attributes["value"].Value;
							break;
						case "password":
							this.password = node.Attributes["value"].Value;
							break;
						case "rootdirectory":
							this.rootDirectory = node.Attributes["value"].Value;
							break;
						case "sqlscriptdirectory":
							this.sqlScriptDirectory = node.Attributes["value"].Value;
							break;
						case "daoclassdirectory":
							this.daoClassDirectory = node.Attributes["value"].Value;
							break;
						case "doclassdirectory":
							this.doClassDirectory = node.Attributes["value"].Value;
							break;
						case "singlefile":
							this.singleFile = Boolean.Parse(node.Attributes["value"].Value);
							break;
						case "createviews":
							this.createViews = Boolean.Parse(node.Attributes["value"].Value);
							break;
						case "createdataobjects":
							this.createDataObjects = Boolean.Parse(node.Attributes["value"].Value);
							break;
						case "scriptdropstatement":
							this.scriptDropStatement = Boolean.Parse(node.Attributes["value"].Value);
							break;
						case "storedprocnameformat":
							this.storedProcNameFormat = node.Attributes["value"].Value;
							break;
						case "generateprocsforforeignkey":
							this.generateProcsForForeignKey = Boolean.Parse(node.Attributes["value"].Value);
							break;
						case "generateselectstoredprocs":
							this.generateSelectStoredProcs= Boolean.Parse(node.Attributes["value"].Value);
							break;
						case "generateonlyprimarydeletestoredproc":
							this.generateOnlyPrimaryDeleteStoredProc= Boolean.Parse(node.Attributes["value"].Value);
							break;
						case "allowupdateofprimarykey":
							this.allowUpdateOfPrimaryKey = Boolean.Parse(node.Attributes["value"].Value);
							break;
						case "useviews":
							this.allowUpdateOfPrimaryKey = Boolean.Parse(node.Attributes["value"].Value);
							break;
						case "usedatatypes":
							this.useDataTypes = Boolean.Parse(node.Attributes["value"].Value);
							break;
						case "autodiscoverentities":
							this.autoDiscoverEntities = Boolean.Parse(node.Attributes["value"].Value);
							break;
						case "autodiscoverproperties":
							this.autoDiscoverProperties = Boolean.Parse(node.Attributes["value"].Value);
							break;
						case "autodiscoverattributes":
							this.autoDiscoverAttributes = Boolean.Parse(node.Attributes["value"].Value);
							break;
						default:
							Console.Out.WriteLine("Unrecognized configuration option: " + node.Attributes["name"].Value + " = " + node.Attributes["value"].Value);
							break;
					}
				}
			}

		}

		private void InitDefaults() {
			this.sqlScriptDirectory = "sql";
			this.daoClassDirectory = "DAO";
			this.doClassDirectory = "DataObject";
			this.generateProcsForForeignKey = false;
			this.generateSelectStoredProcs = false;
			this.generateOnlyPrimaryDeleteStoredProc = true;
			this.rootDirectory = "c:\\data\\work\\seamlessweb\\manhattan\\src\\";
			this.allowUpdateOfPrimaryKey = false;
			this.useDataTypes = true;
			autoDiscoverEntities = true;
			autoDiscoverProperties = true;
			autoDiscoverAttributes = true;
		}

// properties

		public String XmlConfigFilename {
			get { return this.xmlConfigFilename; }
			set { this.xmlConfigFilename = value; }
		}

		public String Server {
			get { return this.server; }
			set { this.server = value; }
		}

        public String Database {
            get { return this.database; }
            set { this.database = value; }
        }

		public String User {
			get { return this.user; }
			set { this.user = value; }
		}

		public String Password {
			get { return this.password; }
			set { this.password = value; }
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
			get { 			
				StringBuilder objStringBuilder = new StringBuilder(255);
				objStringBuilder.Append("Data Source = " + server + ";");
				objStringBuilder.Append("Initial Catalog = " + database + ";");
				objStringBuilder.Append("User ID = " + user + ";");
				objStringBuilder.Append("Password = " + password + ";");
				return objStringBuilder.ToString();
			}
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

		public Boolean UseDataTypes {
			get { return this.useDataTypes; }
			set { this.useDataTypes = value; }
		}

		public Boolean AutoDiscoverEntities {
			get { return this.autoDiscoverEntities; }
			set { this.autoDiscoverEntities = value; }
		}

		public Boolean AutoDiscoverProperties {
			get { return this.autoDiscoverProperties; }
			set { this.autoDiscoverProperties = value; }
		}

		public Boolean AutoDiscoverAttributes {
			get { return this.autoDiscoverAttributes; }
			set { this.autoDiscoverAttributes = value; }
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


		public override String ToString() {
			StringBuilder sb = new StringBuilder();
			Type t = this.GetType();
			foreach (PropertyInfo p in t.GetProperties()) {
				sb.Append(p.Name + ": ");
				sb.Append(p.GetValue(this, null));
				sb.Append(Environment.NewLine);
			}

			return sb.ToString();
		}

    }
}
