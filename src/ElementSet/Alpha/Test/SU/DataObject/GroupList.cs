using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// GroupData generic collection
    /// </summary>
    public class GroupList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly GroupList UNSET = new GroupList(true);
	[Generate]
	public static readonly GroupList DEFAULT = new GroupList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private GroupList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public GroupList()
	{
	}

	// Indexer implementation.
	[Generate]
	public GroupData this[int index]
	{
	    get
	    {
		return (GroupData) List[index];
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
	public void Add(GroupData value)
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
	public Boolean Contains(GroupData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(GroupData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, GroupData value)
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
	public void Remove(GroupData value)
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
		if (o is GroupData)
		{
		    Add((GroupData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to GroupData");
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
