using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// OrderMstData generic collection
    /// </summary>
    public class OrderMstList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly OrderMstList UNSET = new OrderMstList(true);
	[Generate]
	public static readonly OrderMstList DEFAULT = new OrderMstList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private OrderMstList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public OrderMstList()
	{
	}

	// Indexer implementation.
	[Generate]
	public OrderMstData this[int index]
	{
	    get
	    {
		return (OrderMstData) List[index];
	    }
	    set
	    {
		if (!immutable)
		{
		    List[index] = value;
		}
		else
		{
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(OrderMstData value)
	{
	    if (!immutable)
	    {
		List.Add(value);
	    }
	    else
	    {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(OrderMstData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(OrderMstData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, OrderMstData value)
	{
	    if (!immutable)
	    {
		List.Insert(index, value);
	    }
	    else
	    {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void Remove(int index)
	{
	    if (!immutable)
	    {
		if (index > Count - 1 || index < 0)
		{
		    throw new IndexOutOfRangeException();
		}
		else
		{
		    List.RemoveAt(index);
		}
	    }
	    else
	    {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void Remove(OrderMstData value)
	{
	    if (!immutable)
	    {
		List.Remove(value);
	    }
	    else
	    {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(System.Collections.IList list)
	{
	    foreach(Object o in list)
	    {
		if (o is OrderMstData)
		{
		    Add((OrderMstData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to OrderMstData");
		}
	    }
	}

	[Generate]
	public Boolean IsDefault
	{
	    get
	    {
		return Object.ReferenceEquals(this, DEFAULT);
	    }
	}

	[Generate]
	public Boolean IsUnset
	{
	    get
	    {
		return Object.ReferenceEquals(this, UNSET);
	    }
	}

	[Generate]
	public Boolean IsValid
	{
	    get
	    {
		return !(IsDefault || IsUnset);
	    }
	}


    }
}
