using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// KnowledgebasesData generic collection
    /// </summary>
    public class KnowledgebasesList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly KnowledgebasesList UNSET = new KnowledgebasesList(true);
	[Generate]
	public static readonly KnowledgebasesList DEFAULT = new KnowledgebasesList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private KnowledgebasesList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public KnowledgebasesList()
	{
	}

	// Indexer implementation.
	[Generate]
	public KnowledgebasesData this[int index]
	{
	    get
	    {
		return (KnowledgebasesData) List[index];
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
	public void Add(KnowledgebasesData value)
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
	public Boolean Contains(KnowledgebasesData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(KnowledgebasesData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, KnowledgebasesData value)
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
	public void Remove(KnowledgebasesData value)
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
		if (o is KnowledgebasesData)
		{
		    Add((KnowledgebasesData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to KnowledgebasesData");
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
