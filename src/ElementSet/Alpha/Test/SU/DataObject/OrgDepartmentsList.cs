using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// OrgDepartmentsData generic collection
    /// </summary>
    public class OrgDepartmentsList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly OrgDepartmentsList UNSET = new OrgDepartmentsList(true);
	[Generate]
	public static readonly OrgDepartmentsList DEFAULT = new OrgDepartmentsList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private OrgDepartmentsList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public OrgDepartmentsList()
	{
	}

	// Indexer implementation.
	[Generate]
	public OrgDepartmentsData this[int index]
	{
	    get
	    {
		return (OrgDepartmentsData) List[index];
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
	public void Add(OrgDepartmentsData value)
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
	public Boolean Contains(OrgDepartmentsData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(OrgDepartmentsData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, OrgDepartmentsData value)
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
	public void Remove(OrgDepartmentsData value)
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
		if (o is OrgDepartmentsData)
		{
		    Add((OrgDepartmentsData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to OrgDepartmentsData");
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
