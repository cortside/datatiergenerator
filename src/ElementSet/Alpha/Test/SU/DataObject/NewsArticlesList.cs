using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// NewsArticlesData generic collection
    /// </summary>
    public class NewsArticlesList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly NewsArticlesList UNSET = new NewsArticlesList(true);
	[Generate]
	public static readonly NewsArticlesList DEFAULT = new NewsArticlesList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private NewsArticlesList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public NewsArticlesList()
	{
	}

	// Indexer implementation.
	[Generate]
	public NewsArticlesData this[int index]
	{
	    get
	    {
		return (NewsArticlesData) List[index];
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
	public void Add(NewsArticlesData value)
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
	public Boolean Contains(NewsArticlesData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(NewsArticlesData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, NewsArticlesData value)
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
	public void Remove(NewsArticlesData value)
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
		if (o is NewsArticlesData)
		{
		    Add((NewsArticlesData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to NewsArticlesData");
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
