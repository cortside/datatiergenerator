using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// RoleData generic collection
    /// </summary>
    public class RoleList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly RoleList UNSET = new RoleList(true);
	[Generate]
	public static readonly RoleList DEFAULT = new RoleList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private RoleList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public RoleList()
	{
	}

	// Indexer implementation.
	[Generate]
	public RoleData this[int index]
	{
	    get
	    {
		return (RoleData) List[index];
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
	public void Add(RoleData value)
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
	public Boolean Contains(RoleData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(RoleData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, RoleData value)
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
	public void Remove(RoleData value)
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
		if (o is RoleData)
		{
		    Add((RoleData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to RoleData");
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
