using System;

using Spring2.DataTierGenerator.Attribute;

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using Spring2.Core.DAO;
using Spring2.Core.Types;

using StampinUp.DataObject;
using StampinUp.Types;


namespace StampinUp.DataObject
{

    /// <summary>
    /// UserRoleData generic collection
    /// </summary>
    public class UserRoleList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly UserRoleList UNSET = new UserRoleList(true);
	[Generate]
	public static readonly UserRoleList DEFAULT = new UserRoleList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private UserRoleList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public UserRoleList()
	{
	}

	// Indexer implementation.
	[Generate]
	public UserRoleData this[int index]
	{
	    get
	    {
		return (UserRoleData) List[index];
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
	public void Add(UserRoleData value)
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
	public Boolean Contains(UserRoleData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(UserRoleData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, UserRoleData value)
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
	public void Remove(UserRoleData value)
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
		if (o is UserRoleData)
		{
		    Add((UserRoleData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to UserRoleData");
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



	/// <summary>
	/// Sorts the list.
	/// </summary>
	/// <param name="comparer">comparer to use for sorting the list.</param>
	[Generate]
	public void Sort(IComparer comparer)
	{
	    InnerList.Sort(comparer);
	}

	/// <summary>
	/// Used to sort or search an ArrayList of UserRoleData objects on the RoleId properties
	/// </summary>
	[Generate]
	public class CompareByRoleId : IComparer
	{
	    /// <summary>
	    /// Compares two UserRoleData objects.
	    /// </summary>
	    /// <param name="o1">First object</param>
	    /// <param name="o2">Second Object</param>
	    /// <returns>0 if equal, -1 if first less than second and 1 if first greater than second.  Note that null is always less than non-null.</returns>
	    public int Compare(object o1, object o2)
	    {
		if (o1 == null && o2 == null)
		{
		    return 0;
		}

		if (o1 == null)
		{
		    return -1;
		}

		if (o2 == null)
		{
		    return 1;
		}

		UserRoleData g1 = (UserRoleData)o1;
		UserRoleData g2 = (UserRoleData)o2;

		if (g1.RoleId.IsValid && !g2.RoleId.IsValid)
		{
		    return 1;
		}
		else if (!g1.RoleId.IsValid && g2.RoleId.IsValid)
		{
		return -1;
	    }
	    else if (g1.RoleId.IsValid && g2.RoleId.IsValid && g1.RoleId.ToString().CompareTo(g2.RoleId.ToString()) != 0
	    {
	    return g1.RoleId.ToString().CompareTo(g2.RoleId.ToString());
	}

	// No inequalities found, so they must be equal.
	return 0;
    }
}
}
}
