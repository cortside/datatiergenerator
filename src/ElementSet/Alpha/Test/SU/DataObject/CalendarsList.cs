using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// CalendarsData generic collection
    /// </summary>
    public class CalendarsList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly CalendarsList UNSET = new CalendarsList(true);
	[Generate]
	public static readonly CalendarsList DEFAULT = new CalendarsList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private CalendarsList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public CalendarsList()
	{
	}

	// Indexer implementation.
	[Generate]
	public CalendarsData this[int index]
	{
	    get
	    {
		return (CalendarsData) List[index];
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
	public void Add(CalendarsData value)
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
	public Boolean Contains(CalendarsData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(CalendarsData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, CalendarsData value)
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
	public void Remove(CalendarsData value)
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
		if (o is CalendarsData)
		{
		    Add((CalendarsData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to CalendarsData");
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
