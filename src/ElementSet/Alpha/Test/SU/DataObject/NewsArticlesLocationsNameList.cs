using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// NewsArticlesLocationsNameData generic collection
    /// </summary>
    public class NewsArticlesLocationsNameList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly NewsArticlesLocationsNameList UNSET = new NewsArticlesLocationsNameList(true);
	[Generate]
	public static readonly NewsArticlesLocationsNameList DEFAULT = new NewsArticlesLocationsNameList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private NewsArticlesLocationsNameList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public NewsArticlesLocationsNameList()
	{
	}

	// Indexer implementation.
	[Generate]
	public NewsArticlesLocationsNameData this[int index]
	{
	    get
	    {
		return (NewsArticlesLocationsNameData) List[index];
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
	public void Add(NewsArticlesLocationsNameData value)
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
	public Boolean Contains(NewsArticlesLocationsNameData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(NewsArticlesLocationsNameData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, NewsArticlesLocationsNameData value)
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
	public void Remove(NewsArticlesLocationsNameData value)
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
		if (o is NewsArticlesLocationsNameData)
		{
		    Add((NewsArticlesLocationsNameData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to NewsArticlesLocationsNameData");
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
