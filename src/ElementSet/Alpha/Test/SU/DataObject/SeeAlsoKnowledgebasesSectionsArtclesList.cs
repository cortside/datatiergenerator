using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// SeeAlsoKnowledgebasesSectionsArtclesData generic collection
    /// </summary>
    public class SeeAlsoKnowledgebasesSectionsArtclesList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly SeeAlsoKnowledgebasesSectionsArtclesList UNSET = new SeeAlsoKnowledgebasesSectionsArtclesList(true);
	[Generate]
	public static readonly SeeAlsoKnowledgebasesSectionsArtclesList DEFAULT = new SeeAlsoKnowledgebasesSectionsArtclesList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private SeeAlsoKnowledgebasesSectionsArtclesList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public SeeAlsoKnowledgebasesSectionsArtclesList()
	{
	}

	// Indexer implementation.
	[Generate]
	public SeeAlsoKnowledgebasesSectionsArtclesData this[int index]
	{
	    get
	    {
		return (SeeAlsoKnowledgebasesSectionsArtclesData) List[index];
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
	public void Add(SeeAlsoKnowledgebasesSectionsArtclesData value)
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
	public Boolean Contains(SeeAlsoKnowledgebasesSectionsArtclesData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(SeeAlsoKnowledgebasesSectionsArtclesData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, SeeAlsoKnowledgebasesSectionsArtclesData value)
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
	public void Remove(SeeAlsoKnowledgebasesSectionsArtclesData value)
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
		if (o is SeeAlsoKnowledgebasesSectionsArtclesData)
		{
		    Add((SeeAlsoKnowledgebasesSectionsArtclesData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to SeeAlsoKnowledgebasesSectionsArtclesData");
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
