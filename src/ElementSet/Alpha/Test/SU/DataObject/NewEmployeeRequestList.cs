using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// NewEmployeeRequestData generic collection
    /// </summary>
    public class NewEmployeeRequestList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly NewEmployeeRequestList UNSET = new NewEmployeeRequestList(true);
	[Generate]
	public static readonly NewEmployeeRequestList DEFAULT = new NewEmployeeRequestList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private NewEmployeeRequestList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public NewEmployeeRequestList()
	{
	}

	// Indexer implementation.
	[Generate]
	public NewEmployeeRequestData this[int index]
	{
	    get
	    {
		return (NewEmployeeRequestData) List[index];
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
	public void Add(NewEmployeeRequestData value)
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
	public Boolean Contains(NewEmployeeRequestData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(NewEmployeeRequestData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, NewEmployeeRequestData value)
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
	public void Remove(NewEmployeeRequestData value)
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
		if (o is NewEmployeeRequestData)
		{
		    Add((NewEmployeeRequestData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to NewEmployeeRequestData");
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
