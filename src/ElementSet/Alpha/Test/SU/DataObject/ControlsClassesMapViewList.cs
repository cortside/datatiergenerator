using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// ControlsClassesMapViewData generic collection
    /// </summary>
    public class ControlsClassesMapViewList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly ControlsClassesMapViewList UNSET = new ControlsClassesMapViewList(true);
	[Generate]
	public static readonly ControlsClassesMapViewList DEFAULT = new ControlsClassesMapViewList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private ControlsClassesMapViewList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public ControlsClassesMapViewList()
	{
	}

	// Indexer implementation.
	[Generate]
	public ControlsClassesMapViewData this[int index]
	{
	    get
	    {
		return (ControlsClassesMapViewData) List[index];
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
	public void Add(ControlsClassesMapViewData value)
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
	public Boolean Contains(ControlsClassesMapViewData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(ControlsClassesMapViewData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, ControlsClassesMapViewData value)
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
	public void Remove(ControlsClassesMapViewData value)
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
		if (o is ControlsClassesMapViewData)
		{
		    Add((ControlsClassesMapViewData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to ControlsClassesMapViewData");
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
