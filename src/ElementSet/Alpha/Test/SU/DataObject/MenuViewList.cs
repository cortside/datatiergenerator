using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// MenuViewData generic collection
    /// </summary>
    public class MenuViewList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly MenuViewList UNSET = new MenuViewList(true);
	[Generate]
	public static readonly MenuViewList DEFAULT = new MenuViewList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private MenuViewList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public MenuViewList()
	{
	}

	// Indexer implementation.
	[Generate]
	public MenuViewData this[int index]
	{
	    get
	    {
		return (MenuViewData) List[index];
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
	public void Add(MenuViewData value)
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
	public Boolean Contains(MenuViewData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(MenuViewData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, MenuViewData value)
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
	public void Remove(MenuViewData value)
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
		if (o is MenuViewData)
		{
		    Add((MenuViewData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to MenuViewData");
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
