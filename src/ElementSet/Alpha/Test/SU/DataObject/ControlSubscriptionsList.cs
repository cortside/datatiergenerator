using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// ControlSubscriptionsData generic collection
    /// </summary>
    public class ControlSubscriptionsList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly ControlSubscriptionsList UNSET = new ControlSubscriptionsList(true);
	[Generate]
	public static readonly ControlSubscriptionsList DEFAULT = new ControlSubscriptionsList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private ControlSubscriptionsList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public ControlSubscriptionsList()
	{
	}

	// Indexer implementation.
	[Generate]
	public ControlSubscriptionsData this[int index]
	{
	    get
	    {
		return (ControlSubscriptionsData) List[index];
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
	public void Add(ControlSubscriptionsData value)
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
	public Boolean Contains(ControlSubscriptionsData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(ControlSubscriptionsData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, ControlSubscriptionsData value)
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
	public void Remove(ControlSubscriptionsData value)
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
		if (o is ControlSubscriptionsData)
		{
		    Add((ControlSubscriptionsData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to ControlSubscriptionsData");
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
