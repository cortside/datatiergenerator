using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// NewsArticlesLocationsData generic collection
    /// </summary>
    public class NewsArticlesLocationsList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly NewsArticlesLocationsList UNSET = new NewsArticlesLocationsList(true);
	[Generate]
	public static readonly NewsArticlesLocationsList DEFAULT = new NewsArticlesLocationsList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private NewsArticlesLocationsList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public NewsArticlesLocationsList()
	{
	}

	// Indexer implementation.
	[Generate]
	public NewsArticlesLocationsData this[int index]
	{
	    get
	    {
		return (NewsArticlesLocationsData) List[index];
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
	public void Add(NewsArticlesLocationsData value)
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
	public Boolean Contains(NewsArticlesLocationsData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(NewsArticlesLocationsData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, NewsArticlesLocationsData value)
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
	public void Remove(NewsArticlesLocationsData value)
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
		if (o is NewsArticlesLocationsData)
		{
		    Add((NewsArticlesLocationsData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to NewsArticlesLocationsData");
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
