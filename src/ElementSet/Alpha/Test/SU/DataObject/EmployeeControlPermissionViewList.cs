using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// EmployeeControlPermissionViewData generic collection
    /// </summary>
    public class EmployeeControlPermissionViewList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly EmployeeControlPermissionViewList UNSET = new EmployeeControlPermissionViewList(true);
	[Generate]
	public static readonly EmployeeControlPermissionViewList DEFAULT = new EmployeeControlPermissionViewList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private EmployeeControlPermissionViewList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public EmployeeControlPermissionViewList()
	{
	}

	// Indexer implementation.
	[Generate]
	public EmployeeControlPermissionViewData this[int index]
	{
	    get
	    {
		return (EmployeeControlPermissionViewData) List[index];
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
	public void Add(EmployeeControlPermissionViewData value)
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
	public Boolean Contains(EmployeeControlPermissionViewData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(EmployeeControlPermissionViewData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, EmployeeControlPermissionViewData value)
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
	public void Remove(EmployeeControlPermissionViewData value)
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
		if (o is EmployeeControlPermissionViewData)
		{
		    Add((EmployeeControlPermissionViewData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to EmployeeControlPermissionViewData");
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
