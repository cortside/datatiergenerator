using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// OrgPhoneNumbersData generic collection
    /// </summary>
    public class OrgPhoneNumbersList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly OrgPhoneNumbersList UNSET = new OrgPhoneNumbersList(true);
	[Generate]
	public static readonly OrgPhoneNumbersList DEFAULT = new OrgPhoneNumbersList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private OrgPhoneNumbersList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public OrgPhoneNumbersList()
	{
	}

	// Indexer implementation.
	[Generate]
	public OrgPhoneNumbersData this[int index]
	{
	    get
	    {
		return (OrgPhoneNumbersData) List[index];
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
	public void Add(OrgPhoneNumbersData value)
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
	public Boolean Contains(OrgPhoneNumbersData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(OrgPhoneNumbersData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, OrgPhoneNumbersData value)
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
	public void Remove(OrgPhoneNumbersData value)
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
		if (o is OrgPhoneNumbersData)
		{
		    Add((OrgPhoneNumbersData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to OrgPhoneNumbersData");
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
