using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// KnowledgebasesSectionsArticlesData generic collection
    /// </summary>
    public class KnowledgebasesSectionsArticlesList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly KnowledgebasesSectionsArticlesList UNSET = new KnowledgebasesSectionsArticlesList(true);
	[Generate]
	public static readonly KnowledgebasesSectionsArticlesList DEFAULT = new KnowledgebasesSectionsArticlesList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private KnowledgebasesSectionsArticlesList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public KnowledgebasesSectionsArticlesList()
	{
	}

	// Indexer implementation.
	[Generate]
	public KnowledgebasesSectionsArticlesData this[int index]
	{
	    get
	    {
		return (KnowledgebasesSectionsArticlesData) List[index];
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
	public void Add(KnowledgebasesSectionsArticlesData value)
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
	public Boolean Contains(KnowledgebasesSectionsArticlesData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(KnowledgebasesSectionsArticlesData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, KnowledgebasesSectionsArticlesData value)
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
	public void Remove(KnowledgebasesSectionsArticlesData value)
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
		if (o is KnowledgebasesSectionsArticlesData)
		{
		    Add((KnowledgebasesSectionsArticlesData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to KnowledgebasesSectionsArticlesData");
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
