using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class ControlsClassesMapViewData : Spring2.Core.DataObject.DataObject
    {

	private IntegerType controlsID = IntegerType.DEFAULT;
	private StringType description = StringType.DEFAULT;
	private IntegerType controlsClassesID = IntegerType.DEFAULT;

	public static readonly String CONTROLSID = "ControlsID";
	public static readonly String DESCRIPTION = "Description";
	public static readonly String CONTROLSCLASSESID = "ControlsClassesID";

	public IntegerType ControlsID
	{
	    get
	    {
		return this.controlsID;
	    }
	    set
	    {
		this.controlsID = value;
	    }
	}

	public StringType Description
	{
	    get
	    {
		return this.description;
	    }
	    set
	    {
		this.description = value;
	    }
	}

	public IntegerType ControlsClassesID
	{
	    get
	    {
		return this.controlsClassesID;
	    }
	    set
	    {
		this.controlsClassesID = value;
	    }
	}
    }
}
