using System;

using Spring2.DataTierGenerator.Attribute;

namespace Spring2.DataTierGenerator.DataObject
{

    /// <summary>
    /// TestSqlEntityData generic collection
    /// </summary>
    public class TestSqlEntityList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly TestSqlEntityList UNSET = new TestSqlEntityList(true);
	[Generate]
	public static readonly TestSqlEntityList DEFAULT = new TestSqlEntityList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private TestSqlEntityList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public TestSqlEntityList()
	{
	}

	// Indexer implementation.
	[Generate]
	public TestSqlEntityData this[int index]
	{
	    get
	    {
		return (TestSqlEntityData) List[index];
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
	public void Add(TestSqlEntityData value)
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
	public Boolean Contains(TestSqlEntityData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(TestSqlEntityData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, TestSqlEntityData value)
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
	public void Remove(TestSqlEntityData value)
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
		if (o is TestSqlEntityData)
		{
		    Add((TestSqlEntityData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to TestSqlEntityData");
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
