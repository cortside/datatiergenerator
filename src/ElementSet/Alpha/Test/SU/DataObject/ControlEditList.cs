using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// ControlEditData generic collection
    /// </summary>
    public class ControlEditList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly ControlEditList UNSET = new ControlEditList(true);
	[Generate]
	public static readonly ControlEditList DEFAULT = new ControlEditList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private ControlEditList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public ControlEditList()
	{
	}

	// Indexer implementation.
	[Generate]
	public ControlEditData this[int index]
	{
	    get
	    {
		return (ControlEditData) List[index];
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
	public void Add(ControlEditData value)
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
	public Boolean Contains(ControlEditData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(ControlEditData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, ControlEditData value)
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
	public void Remove(ControlEditData value)
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
		if (o is ControlEditData)
		{
		    Add((ControlEditData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to ControlEditData");
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
