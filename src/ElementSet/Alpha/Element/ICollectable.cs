using System;
using System.Collections;

using Spring2.DataTierGenerator.Generator;

namespace Spring2.DataTierGenerator.Element
{
    /// <summary>
    /// Interface to be implemented to allow an object to be used in a generated collection.
    /// </summary>
    public interface ICollectable : IElement
    {
	ArrayList Comparers 
	{
	    get ;
	}
    }
}
