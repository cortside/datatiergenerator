using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// UserData generic collection
    /// </summary>
    public class UserList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly UserList UNSET = new UserList(true);
	[Generate]
	public static readonly UserList DEFAULT = new UserList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private UserList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public UserList()
	{
	}

	// Indexer implementation.
	[Generate]
	public UserData this[int index]
	{
	    get
	    {
		return (UserData) List[index];
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
	public void Add(UserData value)
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
	public Boolean Contains(UserData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(UserData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, UserData value)
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
	public void Remove(UserData value)
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
		if (o is UserData)
		{
		    Add((UserData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to UserData");
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
