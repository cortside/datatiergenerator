using System;

using Spring2.Core.Types;

namespace Spring2.DataTierGenerator.DataObject
{
    public class Testsqlentity2Data : Spring2.Core.DataObject.DataObject
    {

	private IdType id = IdType.DEFAULT;

	public static readonly String ID = "Id";

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
    }
}
