using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// EventData generic collection
    /// </summary>
    public class EventList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly EventList UNSET = new EventList(true);
	[Generate]
	public static readonly EventList DEFAULT = new EventList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private EventList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public EventList()
	{
	}

	// Indexer implementation.
	[Generate]
	public EventData this[int index]
	{
	    get
	    {
		return (EventData) List[index];
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
	public void Add(EventData value)
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
	public Boolean Contains(EventData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(EventData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, EventData value)
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
	public void Remove(EventData value)
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
		if (o is EventData)
		{
		    Add((EventData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to EventData");
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
