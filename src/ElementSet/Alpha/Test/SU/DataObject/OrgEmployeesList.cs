using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// OrgEmployeesData generic collection
    /// </summary>
    public class OrgEmployeesList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly OrgEmployeesList UNSET = new OrgEmployeesList(true);
	[Generate]
	public static readonly OrgEmployeesList DEFAULT = new OrgEmployeesList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private OrgEmployeesList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public OrgEmployeesList()
	{
	}

	// Indexer implementation.
	[Generate]
	public OrgEmployeesData this[int index]
	{
	    get
	    {
		return (OrgEmployeesData) List[index];
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
	public void Add(OrgEmployeesData value)
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
	public Boolean Contains(OrgEmployeesData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(OrgEmployeesData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, OrgEmployeesData value)
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
	public void Remove(OrgEmployeesData value)
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
		if (o is OrgEmployeesData)
		{
		    Add((OrgEmployeesData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to OrgEmployeesData");
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
