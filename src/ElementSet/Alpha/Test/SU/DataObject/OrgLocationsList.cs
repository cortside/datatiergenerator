using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// OrgLocationsData generic collection
    /// </summary>
    public class OrgLocationsList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly OrgLocationsList UNSET = new OrgLocationsList(true);
	[Generate]
	public static readonly OrgLocationsList DEFAULT = new OrgLocationsList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private OrgLocationsList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public OrgLocationsList()
	{
	}

	// Indexer implementation.
	[Generate]
	public OrgLocationsData this[int index]
	{
	    get
	    {
		return (OrgLocationsData) List[index];
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
	public void Add(OrgLocationsData value)
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
	public Boolean Contains(OrgLocationsData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(OrgLocationsData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, OrgLocationsData value)
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
	public void Remove(OrgLocationsData value)
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
		if (o is OrgLocationsData)
		{
		    Add((OrgLocationsData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to OrgLocationsData");
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
