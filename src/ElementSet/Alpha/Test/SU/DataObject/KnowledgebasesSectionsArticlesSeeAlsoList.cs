using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// KnowledgebasesSectionsArticlesSeeAlsoData generic collection
    /// </summary>
    public class KnowledgebasesSectionsArticlesSeeAlsoList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly KnowledgebasesSectionsArticlesSeeAlsoList UNSET = new KnowledgebasesSectionsArticlesSeeAlsoList(true);
	[Generate]
	public static readonly KnowledgebasesSectionsArticlesSeeAlsoList DEFAULT = new KnowledgebasesSectionsArticlesSeeAlsoList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private KnowledgebasesSectionsArticlesSeeAlsoList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public KnowledgebasesSectionsArticlesSeeAlsoList()
	{
	}

	// Indexer implementation.
	[Generate]
	public KnowledgebasesSectionsArticlesSeeAlsoData this[int index]
	{
	    get
	    {
		return (KnowledgebasesSectionsArticlesSeeAlsoData) List[index];
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
	public void Add(KnowledgebasesSectionsArticlesSeeAlsoData value)
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
	public Boolean Contains(KnowledgebasesSectionsArticlesSeeAlsoData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(KnowledgebasesSectionsArticlesSeeAlsoData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, KnowledgebasesSectionsArticlesSeeAlsoData value)
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
	public void Remove(KnowledgebasesSectionsArticlesSeeAlsoData value)
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
		if (o is KnowledgebasesSectionsArticlesSeeAlsoData)
		{
		    Add((KnowledgebasesSectionsArticlesSeeAlsoData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to KnowledgebasesSectionsArticlesSeeAlsoData");
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
