using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// EmployeeControlSubscriptionsViewData generic collection
    /// </summary>
    public class EmployeeControlSubscriptionsViewList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly EmployeeControlSubscriptionsViewList UNSET = new EmployeeControlSubscriptionsViewList(true);
	[Generate]
	public static readonly EmployeeControlSubscriptionsViewList DEFAULT = new EmployeeControlSubscriptionsViewList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private EmployeeControlSubscriptionsViewList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public EmployeeControlSubscriptionsViewList()
	{
	}

	// Indexer implementation.
	[Generate]
	public EmployeeControlSubscriptionsViewData this[int index]
	{
	    get
	    {
		return (EmployeeControlSubscriptionsViewData) List[index];
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
	public void Add(EmployeeControlSubscriptionsViewData value)
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
	public Boolean Contains(EmployeeControlSubscriptionsViewData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(EmployeeControlSubscriptionsViewData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, EmployeeControlSubscriptionsViewData value)
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
	public void Remove(EmployeeControlSubscriptionsViewData value)
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
		if (o is EmployeeControlSubscriptionsViewData)
		{
		    Add((EmployeeControlSubscriptionsViewData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to EmployeeControlSubscriptionsViewData");
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
