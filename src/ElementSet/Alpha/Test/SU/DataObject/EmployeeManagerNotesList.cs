using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// EmployeeManagerNotesData generic collection
    /// </summary>
    public class EmployeeManagerNotesList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly EmployeeManagerNotesList UNSET = new EmployeeManagerNotesList(true);
	[Generate]
	public static readonly EmployeeManagerNotesList DEFAULT = new EmployeeManagerNotesList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private EmployeeManagerNotesList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public EmployeeManagerNotesList()
	{
	}

	// Indexer implementation.
	[Generate]
	public EmployeeManagerNotesData this[int index]
	{
	    get
	    {
		return (EmployeeManagerNotesData) List[index];
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
	public void Add(EmployeeManagerNotesData value)
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
	public Boolean Contains(EmployeeManagerNotesData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(EmployeeManagerNotesData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, EmployeeManagerNotesData value)
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
	public void Remove(EmployeeManagerNotesData value)
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
		if (o is EmployeeManagerNotesData)
		{
		    Add((EmployeeManagerNotesData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to EmployeeManagerNotesData");
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
