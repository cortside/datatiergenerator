using System;
using System.Collections;

using Spring2.DataTierGenerator.Generator;

namespace Spring2.DataTierGenerator.Element
{
    /// <summary>
    /// Interface to be implemented to allow an object to be used in generations
    /// requiring properties (DAO, DataObject, ...)
    /// </summary>
    public interface IPropertyContainer : IElement
    {
	PropertyElement FindFieldByName(String name);

	ArrayList Fields { get; }
    }
}
