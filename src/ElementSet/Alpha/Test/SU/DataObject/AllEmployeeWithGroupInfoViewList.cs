using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// AllEmployeeWithGroupInfoViewData generic collection
    /// </summary>
    public class AllEmployeeWithGroupInfoViewList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly AllEmployeeWithGroupInfoViewList UNSET = new AllEmployeeWithGroupInfoViewList(true);
	[Generate]
	public static readonly AllEmployeeWithGroupInfoViewList DEFAULT = new AllEmployeeWithGroupInfoViewList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private AllEmployeeWithGroupInfoViewList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public AllEmployeeWithGroupInfoViewList()
	{
	}

	// Indexer implementation.
	[Generate]
	public AllEmployeeWithGroupInfoViewData this[int index]
	{
	    get
	    {
		return (AllEmployeeWithGroupInfoViewData) List[index];
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
	public void Add(AllEmployeeWithGroupInfoViewData value)
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
	public Boolean Contains(AllEmployeeWithGroupInfoViewData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(AllEmployeeWithGroupInfoViewData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, AllEmployeeWithGroupInfoViewData value)
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
	public void Remove(AllEmployeeWithGroupInfoViewData value)
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
		if (o is AllEmployeeWithGroupInfoViewData)
		{
		    Add((AllEmployeeWithGroupInfoViewData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to AllEmployeeWithGroupInfoViewData");
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
