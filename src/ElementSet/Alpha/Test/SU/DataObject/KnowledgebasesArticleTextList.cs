using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// KnowledgebasesArticleTextData generic collection
    /// </summary>
    public class KnowledgebasesArticleTextList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly KnowledgebasesArticleTextList UNSET = new KnowledgebasesArticleTextList(true);
	[Generate]
	public static readonly KnowledgebasesArticleTextList DEFAULT = new KnowledgebasesArticleTextList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private KnowledgebasesArticleTextList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public KnowledgebasesArticleTextList()
	{
	}

	// Indexer implementation.
	[Generate]
	public KnowledgebasesArticleTextData this[int index]
	{
	    get
	    {
		return (KnowledgebasesArticleTextData) List[index];
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
	public void Add(KnowledgebasesArticleTextData value)
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
	public Boolean Contains(KnowledgebasesArticleTextData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(KnowledgebasesArticleTextData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, KnowledgebasesArticleTextData value)
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
	public void Remove(KnowledgebasesArticleTextData value)
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
		if (o is KnowledgebasesArticleTextData)
		{
		    Add((KnowledgebasesArticleTextData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to KnowledgebasesArticleTextData");
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
