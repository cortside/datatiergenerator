using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// UserGroupData generic collection
    /// </summary>
    public class UserGroupList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly UserGroupList UNSET = new UserGroupList(true);
	[Generate]
	public static readonly UserGroupList DEFAULT = new UserGroupList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private UserGroupList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public UserGroupList()
	{
	}

	// Indexer implementation.
	[Generate]
	public UserGroupData this[int index]
	{
	    get
	    {
		return (UserGroupData) List[index];
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
	public void Add(UserGroupData value)
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
	public Boolean Contains(UserGroupData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(UserGroupData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, UserGroupData value)
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
	public void Remove(UserGroupData value)
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
		if (o is UserGroupData)
		{
		    Add((UserGroupData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to UserGroupData");
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
