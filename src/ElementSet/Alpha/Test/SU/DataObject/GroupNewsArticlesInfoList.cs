using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// GroupNewsArticlesInfoData generic collection
    /// </summary>
    public class GroupNewsArticlesInfoList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly GroupNewsArticlesInfoList UNSET = new GroupNewsArticlesInfoList(true);
	[Generate]
	public static readonly GroupNewsArticlesInfoList DEFAULT = new GroupNewsArticlesInfoList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private GroupNewsArticlesInfoList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public GroupNewsArticlesInfoList()
	{
	}

	// Indexer implementation.
	[Generate]
	public GroupNewsArticlesInfoData this[int index]
	{
	    get
	    {
		return (GroupNewsArticlesInfoData) List[index];
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
	public void Add(GroupNewsArticlesInfoData value)
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
	public Boolean Contains(GroupNewsArticlesInfoData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(GroupNewsArticlesInfoData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, GroupNewsArticlesInfoData value)
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
	public void Remove(GroupNewsArticlesInfoData value)
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
		if (o is GroupNewsArticlesInfoData)
		{
		    Add((GroupNewsArticlesInfoData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to GroupNewsArticlesInfoData");
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
