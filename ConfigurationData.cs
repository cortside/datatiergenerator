using System;

namespace Spring2.DataTierGenerator {
	public class ConfigurationData : Spring2.Core.DataObject.DataObject {

		protected String server = String.Empty;
		protected String database = String.Empty;
		protected String user = String.Empty;
		protected String password = String.Empty;
		protected String rootDirectory = String.Empty;
		protected String sqlScriptDirectory = "Sql";
		protected String daoClassDirectory = "Dao";
		protected String doClassDirectory = "DataObject";
		protected String storedProcNameFormat = String.Empty;
		protected String rootNameSpace = String.Empty;
		protected String xmlConfigFilename = String.Empty;
		protected Boolean singleFile = false;
		protected Boolean generateSqlViewScripts = false;
		protected Boolean generateSqlTableScripts = false;
		protected Boolean useViews = true;
		protected Boolean generateDataObjectClasses = true;
		protected Boolean generateDaoClasses = true;
		protected Boolean scriptDropStatement = true;
		protected Boolean generateProcsForForeignKey = false;
		protected Boolean generateSelectStoredProcs = false;
		protected Boolean generateOnlyPrimaryDeleteStoredProc = true;
		protected Boolean allowUpdateOfPrimaryKey = false;
		protected Boolean autoDiscoverEntities = true;
		protected Boolean autoDiscoverProperties = true;
		protected Boolean autoDiscoverAttributes = true;

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

		public String StoredProcNameFormat {
			get { return this.storedProcNameFormat; }
			set { this.storedProcNameFormat = value; }
		}

		public String RootNameSpace {
			get { return this.rootNameSpace; }
			set { this.rootNameSpace = value; }
		}

		public String XmlConfigFilename {
			get { return this.xmlConfigFilename; }
			set { this.xmlConfigFilename = value; }
		}

		public Boolean SingleFile {
			get { return this.singleFile; }
			set { this.singleFile = value; }
		}

		public Boolean GenerateSqlViewScripts {
			get { return this.generateSqlViewScripts; }
			set { this.generateSqlViewScripts = value; }
		}

		public Boolean GenerateSqlTableScripts {
			get { return this.generateSqlTableScripts; }
			set { this.generateSqlTableScripts = value; }
		}

		public Boolean UseViews {
			get { return this.useViews; }
			set { this.useViews = value; }
		}

		public Boolean GenerateDataObjectClasses {
			get { return this.generateDataObjectClasses; }
			set { this.generateDataObjectClasses = value; }
		}

		public Boolean GenerateDaoClasses {
			get { return this.generateDaoClasses; }
			set { this.generateDaoClasses = value; }
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

	}
}
