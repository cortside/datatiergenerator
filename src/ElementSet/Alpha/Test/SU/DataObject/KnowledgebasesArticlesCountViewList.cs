using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// KnowledgebasesArticlesCountViewData generic collection
    /// </summary>
    public class KnowledgebasesArticlesCountViewList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly KnowledgebasesArticlesCountViewList UNSET = new KnowledgebasesArticlesCountViewList(true);
	[Generate]
	public static readonly KnowledgebasesArticlesCountViewList DEFAULT = new KnowledgebasesArticlesCountViewList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private KnowledgebasesArticlesCountViewList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public KnowledgebasesArticlesCountViewList()
	{
	}

	// Indexer implementation.
	[Generate]
	public KnowledgebasesArticlesCountViewData this[int index]
	{
	    get
	    {
		return (KnowledgebasesArticlesCountViewData) List[index];
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
	public void Add(KnowledgebasesArticlesCountViewData value)
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
	public Boolean Contains(KnowledgebasesArticlesCountViewData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(KnowledgebasesArticlesCountViewData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, KnowledgebasesArticlesCountViewData value)
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
	public void Remove(KnowledgebasesArticlesCountViewData value)
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
		if (o is KnowledgebasesArticlesCountViewData)
		{
		    Add((KnowledgebasesArticlesCountViewData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to KnowledgebasesArticlesCountViewData");
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
