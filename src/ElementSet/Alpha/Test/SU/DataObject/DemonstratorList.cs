using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// DemonstratorData generic collection
    /// </summary>
    public class DemonstratorList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly DemonstratorList UNSET = new DemonstratorList(true);
	[Generate]
	public static readonly DemonstratorList DEFAULT = new DemonstratorList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private DemonstratorList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public DemonstratorList()
	{
	}

	// Indexer implementation.
	[Generate]
	public DemonstratorData this[int index]
	{
	    get
	    {
		return (DemonstratorData) List[index];
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
	public void Add(DemonstratorData value)
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
	public Boolean Contains(DemonstratorData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(DemonstratorData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, DemonstratorData value)
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
	public void Remove(DemonstratorData value)
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
		if (o is DemonstratorData)
		{
		    Add((DemonstratorData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to DemonstratorData");
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
