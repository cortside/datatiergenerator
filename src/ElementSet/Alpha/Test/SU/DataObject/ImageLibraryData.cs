using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class ImageLibraryData : Spring2.Core.DataObject.DataObject
    {

	private IdType id = IdType.DEFAULT;
	private StringType fileName = StringType.DEFAULT;
	private IntegerType orgGroupID = IntegerType.DEFAULT;

	public static readonly String ID = "Id";
	public static readonly String FILENAME = "FileName";
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

	public StringType FileName
	{
	    get
	    {
		return this.fileName;
	    }
	    set
	    {
		this.fileName = value;
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
