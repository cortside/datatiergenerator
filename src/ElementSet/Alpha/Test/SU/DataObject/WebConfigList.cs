using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// WebConfigData generic collection
    /// </summary>
    public class WebConfigList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly WebConfigList UNSET = new WebConfigList(true);
	[Generate]
	public static readonly WebConfigList DEFAULT = new WebConfigList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private WebConfigList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public WebConfigList()
	{
	}

	// Indexer implementation.
	[Generate]
	public WebConfigData this[int index]
	{
	    get
	    {
		return (WebConfigData) List[index];
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
	public void Add(WebConfigData value)
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
	public Boolean Contains(WebConfigData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(WebConfigData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, WebConfigData value)
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
	public void Remove(WebConfigData value)
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
		if (o is WebConfigData)
		{
		    Add((WebConfigData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to WebConfigData");
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
