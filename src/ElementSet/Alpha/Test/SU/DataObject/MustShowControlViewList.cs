using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// MustShowControlViewData generic collection
    /// </summary>
    public class MustShowControlViewList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly MustShowControlViewList UNSET = new MustShowControlViewList(true);
	[Generate]
	public static readonly MustShowControlViewList DEFAULT = new MustShowControlViewList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private MustShowControlViewList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public MustShowControlViewList()
	{
	}

	// Indexer implementation.
	[Generate]
	public MustShowControlViewData this[int index]
	{
	    get
	    {
		return (MustShowControlViewData) List[index];
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
	public void Add(MustShowControlViewData value)
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
	public Boolean Contains(MustShowControlViewData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(MustShowControlViewData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, MustShowControlViewData value)
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
	public void Remove(MustShowControlViewData value)
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
		if (o is MustShowControlViewData)
		{
		    Add((MustShowControlViewData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to MustShowControlViewData");
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
