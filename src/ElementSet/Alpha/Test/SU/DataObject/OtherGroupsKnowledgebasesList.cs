using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// OtherGroupsKnowledgebasesData generic collection
    /// </summary>
    public class OtherGroupsKnowledgebasesList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly OtherGroupsKnowledgebasesList UNSET = new OtherGroupsKnowledgebasesList(true);
	[Generate]
	public static readonly OtherGroupsKnowledgebasesList DEFAULT = new OtherGroupsKnowledgebasesList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private OtherGroupsKnowledgebasesList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public OtherGroupsKnowledgebasesList()
	{
	}

	// Indexer implementation.
	[Generate]
	public OtherGroupsKnowledgebasesData this[int index]
	{
	    get
	    {
		return (OtherGroupsKnowledgebasesData) List[index];
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
	public void Add(OtherGroupsKnowledgebasesData value)
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
	public Boolean Contains(OtherGroupsKnowledgebasesData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(OtherGroupsKnowledgebasesData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, OtherGroupsKnowledgebasesData value)
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
	public void Remove(OtherGroupsKnowledgebasesData value)
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
		if (o is OtherGroupsKnowledgebasesData)
		{
		    Add((OtherGroupsKnowledgebasesData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to OtherGroupsKnowledgebasesData");
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
