using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// GroupKnowledgebasesSubscriptionsData generic collection
    /// </summary>
    public class GroupKnowledgebasesSubscriptionsList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly GroupKnowledgebasesSubscriptionsList UNSET = new GroupKnowledgebasesSubscriptionsList(true);
	[Generate]
	public static readonly GroupKnowledgebasesSubscriptionsList DEFAULT = new GroupKnowledgebasesSubscriptionsList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private GroupKnowledgebasesSubscriptionsList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public GroupKnowledgebasesSubscriptionsList()
	{
	}

	// Indexer implementation.
	[Generate]
	public GroupKnowledgebasesSubscriptionsData this[int index]
	{
	    get
	    {
		return (GroupKnowledgebasesSubscriptionsData) List[index];
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
	public void Add(GroupKnowledgebasesSubscriptionsData value)
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
	public Boolean Contains(GroupKnowledgebasesSubscriptionsData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(GroupKnowledgebasesSubscriptionsData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, GroupKnowledgebasesSubscriptionsData value)
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
	public void Remove(GroupKnowledgebasesSubscriptionsData value)
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
		if (o is GroupKnowledgebasesSubscriptionsData)
		{
		    Add((GroupKnowledgebasesSubscriptionsData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to GroupKnowledgebasesSubscriptionsData");
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
