using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// OrderDtlData generic collection
    /// </summary>
    public class OrderDtlList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly OrderDtlList UNSET = new OrderDtlList(true);
	[Generate]
	public static readonly OrderDtlList DEFAULT = new OrderDtlList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private OrderDtlList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public OrderDtlList()
	{
	}

	// Indexer implementation.
	[Generate]
	public OrderDtlData this[int index]
	{
	    get
	    {
		return (OrderDtlData) List[index];
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
	public void Add(OrderDtlData value)
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
	public Boolean Contains(OrderDtlData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(OrderDtlData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, OrderDtlData value)
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
	public void Remove(OrderDtlData value)
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
		if (o is OrderDtlData)
		{
		    Add((OrderDtlData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to OrderDtlData");
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
