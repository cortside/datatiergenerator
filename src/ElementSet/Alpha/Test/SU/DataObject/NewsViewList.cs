using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// NewsViewData generic collection
    /// </summary>
    public class NewsViewList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly NewsViewList UNSET = new NewsViewList(true);
	[Generate]
	public static readonly NewsViewList DEFAULT = new NewsViewList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private NewsViewList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public NewsViewList()
	{
	}

	// Indexer implementation.
	[Generate]
	public NewsViewData this[int index]
	{
	    get
	    {
		return (NewsViewData) List[index];
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
	public void Add(NewsViewData value)
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
	public Boolean Contains(NewsViewData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(NewsViewData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, NewsViewData value)
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
	public void Remove(NewsViewData value)
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
		if (o is NewsViewData)
		{
		    Add((NewsViewData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to NewsViewData");
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
