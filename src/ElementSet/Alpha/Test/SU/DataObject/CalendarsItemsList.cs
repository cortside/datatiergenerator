using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// CalendarsItemsData generic collection
    /// </summary>
    public class CalendarsItemsList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly CalendarsItemsList UNSET = new CalendarsItemsList(true);
	[Generate]
	public static readonly CalendarsItemsList DEFAULT = new CalendarsItemsList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private CalendarsItemsList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public CalendarsItemsList()
	{
	}

	// Indexer implementation.
	[Generate]
	public CalendarsItemsData this[int index]
	{
	    get
	    {
		return (CalendarsItemsData) List[index];
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
	public void Add(CalendarsItemsData value)
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
	public Boolean Contains(CalendarsItemsData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(CalendarsItemsData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, CalendarsItemsData value)
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
	public void Remove(CalendarsItemsData value)
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
		if (o is CalendarsItemsData)
		{
		    Add((CalendarsItemsData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to CalendarsItemsData");
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
