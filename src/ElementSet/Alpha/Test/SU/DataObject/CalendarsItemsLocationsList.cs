using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// CalendarsItemsLocationsData generic collection
    /// </summary>
    public class CalendarsItemsLocationsList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly CalendarsItemsLocationsList UNSET = new CalendarsItemsLocationsList(true);
	[Generate]
	public static readonly CalendarsItemsLocationsList DEFAULT = new CalendarsItemsLocationsList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private CalendarsItemsLocationsList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public CalendarsItemsLocationsList()
	{
	}

	// Indexer implementation.
	[Generate]
	public CalendarsItemsLocationsData this[int index]
	{
	    get
	    {
		return (CalendarsItemsLocationsData) List[index];
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
	public void Add(CalendarsItemsLocationsData value)
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
	public Boolean Contains(CalendarsItemsLocationsData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(CalendarsItemsLocationsData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, CalendarsItemsLocationsData value)
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
	public void Remove(CalendarsItemsLocationsData value)
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
		if (o is CalendarsItemsLocationsData)
		{
		    Add((CalendarsItemsLocationsData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to CalendarsItemsLocationsData");
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
