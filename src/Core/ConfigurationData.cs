using System;

namespace Spring2.DataTierGenerator.Core {
    public class ConfigurationData : Spring2.Core.DataObject.DataObject {

	protected String rootDirectory = String.Empty;
	protected String daoClassDirectory = "Dao";
	protected String doClassDirectory = "DataObject";
	protected String collectionClassDirectory = "DataObject";
	protected String typesClassDirectory = "Types";
	protected String rootNameSpace = String.Empty;
	protected String xmlConfigFilename = String.Empty;
	protected Boolean generateDataObjectClasses = true;
	protected Boolean generateDaoClasses = true;
	protected String dataObjectBaseClass = "Spring2.Core.DataObject.DataObject";
	protected String daoBaseClass = "Spring2.Core.DAO.EntityDAO";
	protected String enumBaseClass = "Spring2.Core.Types.EnumDataType";

	/// <summary>		
	/// Name of database server.		
	/// </summary>		

	public String RootDirectory {
	    get { return this.rootDirectory; }
	    set { this.rootDirectory = value; }
	}

	public String TypesClassDirectory {
	    get { return this.typesClassDirectory; }
	    set { this.typesClassDirectory = value; }
	}

	public String DaoClassDirectory {
	    get { return this.daoClassDirectory; }
	    set { this.daoClassDirectory = value; }
	}

	public String DoClassDirectory {
	    get { return this.doClassDirectory; }
	    set { this.doClassDirectory = value; }
	}

	public String CollectionClassDirectory {
	    get { return this.collectionClassDirectory; }
	    set { this.collectionClassDirectory = value; }
	}

	public String RootNameSpace {
	    get { return this.rootNameSpace; }
	    set { this.rootNameSpace = value; }
	}

	public String XmlConfigFilename {
	    get { return this.xmlConfigFilename; }
	    set { this.xmlConfigFilename = value; }
	}

	public Boolean GenerateDataObjectClasses {
	    get { return this.generateDataObjectClasses; }
	    set { this.generateDataObjectClasses = value; }
	}

	public Boolean GenerateDaoClasses {
	    get { return this.generateDaoClasses; }
	    set { this.generateDaoClasses = value; }
	}

	public String DataObjectBaseClass {
	    get { return this.dataObjectBaseClass; }
	    set { this.dataObjectBaseClass = value; }
	}

	public String DaoBaseClass {
	    get { return this.daoBaseClass; }
	    set { this.daoBaseClass = value; }
	}

	public String EnumBaseClass {
	    get { return this.enumBaseClass; }
	    set { this.enumBaseClass = value; }
	}

    }
}
