using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// SessionInformationData generic collection
    /// </summary>
    public class SessionInformationList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly SessionInformationList UNSET = new SessionInformationList(true);
	[Generate]
	public static readonly SessionInformationList DEFAULT = new SessionInformationList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private SessionInformationList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public SessionInformationList()
	{
	}

	// Indexer implementation.
	[Generate]
	public SessionInformationData this[int index]
	{
	    get
	    {
		return (SessionInformationData) List[index];
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
	public void Add(SessionInformationData value)
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
	public Boolean Contains(SessionInformationData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(SessionInformationData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, SessionInformationData value)
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
	public void Remove(SessionInformationData value)
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
		if (o is SessionInformationData)
		{
		    Add((SessionInformationData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to SessionInformationData");
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
