using System;

using Spring2.Core.Types;

namespace StampinUp.DataObject
{
    public class MustShowControlViewData : Spring2.Core.DataObject.DataObject
    {

	private StringType src = StringType.DEFAULT;
	private StringType description = StringType.DEFAULT;
	private StringType summary = StringType.DEFAULT;
	private StringType classSrc = StringType.DEFAULT;
	private BooleanType mustShow = BooleanType.DEFAULT;
	private DateType dateStart = DateType.DEFAULT;
	private DateType dateEnd = DateType.DEFAULT;
	private IntegerType controlsID = IntegerType.DEFAULT;

	public static readonly String SRC = "Src";
	public static readonly String DESCRIPTION = "Description";
	public static readonly String SUMMARY = "Summary";
	public static readonly String CLASSSRC = "ClassSrc";
	public static readonly String MUSTSHOW = "MustShow";
	public static readonly String DATESTART = "DateStart";
	public static readonly String DATEEND = "DateEnd";
	public static readonly String CONTROLSID = "ControlsID";

	public StringType Src
	{
	    get
	    {
		return this.src;
	    }
	    set
	    {
		this.src = value;
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

	public StringType Summary
	{
	    get
	    {
		return this.summary;
	    }
	    set
	    {
		this.summary = value;
	    }
	}

	public StringType ClassSrc
	{
	    get
	    {
		return this.classSrc;
	    }
	    set
	    {
		this.classSrc = value;
	    }
	}

	public BooleanType MustShow
	{
	    get
	    {
		return this.mustShow;
	    }
	    set
	    {
		this.mustShow = value;
	    }
	}

	public DateType DateStart
	{
	    get
	    {
		return this.dateStart;
	    }
	    set
	    {
		this.dateStart = value;
	    }
	}

	public DateType DateEnd
	{
	    get
	    {
		return this.dateEnd;
	    }
	    set
	    {
		this.dateEnd = value;
	    }
	}

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
    }
}
