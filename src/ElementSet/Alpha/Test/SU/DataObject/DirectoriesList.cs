using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// DirectoriesData generic collection
    /// </summary>
    public class DirectoriesList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly DirectoriesList UNSET = new DirectoriesList(true);
	[Generate]
	public static readonly DirectoriesList DEFAULT = new DirectoriesList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private DirectoriesList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public DirectoriesList()
	{
	}

	// Indexer implementation.
	[Generate]
	public DirectoriesData this[int index]
	{
	    get
	    {
		return (DirectoriesData) List[index];
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
	public void Add(DirectoriesData value)
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
	public Boolean Contains(DirectoriesData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(DirectoriesData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, DirectoriesData value)
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
	public void Remove(DirectoriesData value)
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
		if (o is DirectoriesData)
		{
		    Add((DirectoriesData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to DirectoriesData");
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
