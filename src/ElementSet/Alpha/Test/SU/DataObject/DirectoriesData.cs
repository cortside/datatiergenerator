using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class DirectoriesData : Spring2.Core.DataObject.DataObject
    {

	private IdType id = IdType.DEFAULT;
	private StringType name = StringType.DEFAULT;
	private StringType path = StringType.DEFAULT;
	private BooleanType isPublic = BooleanType.DEFAULT;
	private IntegerType orgGroupID = IntegerType.DEFAULT;

	public static readonly String ID = "Id";
	public static readonly String NAME = "Name";
	public static readonly String PATH = "Path";
	public static readonly String ISPUBLIC = "IsPublic";
	public static readonly String ORGGROUPID = "OrgGroupID";

	public IdType Id
	{
	    get
	    {
		return this.id;
	    }
	    set
	    {
		this.id = value;
	    }
	}

	public StringType Name
	{
	    get
	    {
		return this.name;
	    }
	    set
	    {
		this.name = value;
	    }
	}

	public StringType Path
	{
	    get
	    {
		return this.path;
	    }
	    set
	    {
		this.path = value;
	    }
	}

	public BooleanType IsPublic
	{
	    get
	    {
		return this.isPublic;
	    }
	    set
	    {
		this.isPublic = value;
	    }
	}

	public IntegerType OrgGroupID
	{
	    get
	    {
		return this.orgGroupID;
	    }
	    set
	    {
		this.orgGroupID = value;
	    }
	}
    }
}
