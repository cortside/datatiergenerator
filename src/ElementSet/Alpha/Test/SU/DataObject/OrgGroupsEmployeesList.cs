using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// OrgGroupsEmployeesData generic collection
    /// </summary>
    public class OrgGroupsEmployeesList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly OrgGroupsEmployeesList UNSET = new OrgGroupsEmployeesList(true);
	[Generate]
	public static readonly OrgGroupsEmployeesList DEFAULT = new OrgGroupsEmployeesList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private OrgGroupsEmployeesList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public OrgGroupsEmployeesList()
	{
	}

	// Indexer implementation.
	[Generate]
	public OrgGroupsEmployeesData this[int index]
	{
	    get
	    {
		return (OrgGroupsEmployeesData) List[index];
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
	public void Add(OrgGroupsEmployeesData value)
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
	public Boolean Contains(OrgGroupsEmployeesData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(OrgGroupsEmployeesData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, OrgGroupsEmployeesData value)
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
	public void Remove(OrgGroupsEmployeesData value)
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
		if (o is OrgGroupsEmployeesData)
		{
		    Add((OrgGroupsEmployeesData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to OrgGroupsEmployeesData");
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
