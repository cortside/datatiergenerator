using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// EmployeeControlsViewData generic collection
    /// </summary>
    public class EmployeeControlsViewList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly EmployeeControlsViewList UNSET = new EmployeeControlsViewList(true);
	[Generate]
	public static readonly EmployeeControlsViewList DEFAULT = new EmployeeControlsViewList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private EmployeeControlsViewList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public EmployeeControlsViewList()
	{
	}

	// Indexer implementation.
	[Generate]
	public EmployeeControlsViewData this[int index]
	{
	    get
	    {
		return (EmployeeControlsViewData) List[index];
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
	public void Add(EmployeeControlsViewData value)
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
	public Boolean Contains(EmployeeControlsViewData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(EmployeeControlsViewData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, EmployeeControlsViewData value)
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
	public void Remove(EmployeeControlsViewData value)
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
		if (o is EmployeeControlsViewData)
		{
		    Add((EmployeeControlsViewData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to EmployeeControlsViewData");
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
