using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// OrgGroupsData generic collection
    /// </summary>
    public class OrgGroupsList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly OrgGroupsList UNSET = new OrgGroupsList(true);
	[Generate]
	public static readonly OrgGroupsList DEFAULT = new OrgGroupsList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private OrgGroupsList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public OrgGroupsList()
	{
	}

	// Indexer implementation.
	[Generate]
	public OrgGroupsData this[int index]
	{
	    get
	    {
		return (OrgGroupsData) List[index];
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
	public void Add(OrgGroupsData value)
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
	public Boolean Contains(OrgGroupsData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(OrgGroupsData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, OrgGroupsData value)
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
	public void Remove(OrgGroupsData value)
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
		if (o is OrgGroupsData)
		{
		    Add((OrgGroupsData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to OrgGroupsData");
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
