using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// ControlsGroupsData generic collection
    /// </summary>
    public class ControlsGroupsList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly ControlsGroupsList UNSET = new ControlsGroupsList(true);
	[Generate]
	public static readonly ControlsGroupsList DEFAULT = new ControlsGroupsList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private ControlsGroupsList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public ControlsGroupsList()
	{
	}

	// Indexer implementation.
	[Generate]
	public ControlsGroupsData this[int index]
	{
	    get
	    {
		return (ControlsGroupsData) List[index];
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
	public void Add(ControlsGroupsData value)
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
	public Boolean Contains(ControlsGroupsData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(ControlsGroupsData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, ControlsGroupsData value)
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
	public void Remove(ControlsGroupsData value)
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
		if (o is ControlsGroupsData)
		{
		    Add((ControlsGroupsData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to ControlsGroupsData");
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
