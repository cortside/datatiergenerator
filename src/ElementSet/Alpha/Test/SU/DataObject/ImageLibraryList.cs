using System;

using Spring2.DataTierGenerator.Attribute;

namespace StampinUp.DataObject
{

    /// <summary>
    /// ImageLibraryData generic collection
    /// </summary>
    public class ImageLibraryList : System.Collections.CollectionBase
    {

	[Generate]
	public static readonly ImageLibraryList UNSET = new ImageLibraryList(true);
	[Generate]
	public static readonly ImageLibraryList DEFAULT = new ImageLibraryList(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private ImageLibraryList (Boolean immutable)
	{
	    this.immutable = immutable;
	}

	[Generate]
	public ImageLibraryList()
	{
	}

	// Indexer implementation.
	[Generate]
	public ImageLibraryData this[int index]
	{
	    get
	    {
		return (ImageLibraryData) List[index];
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
	public void Add(ImageLibraryData value)
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
	public Boolean Contains(ImageLibraryData value)
	{
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(ImageLibraryData value)
	{
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, ImageLibraryData value)
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
	public void Remove(ImageLibraryData value)
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
		if (o is ImageLibraryData)
		{
		    Add((ImageLibraryData)o);
		}
		else
		{
		    throw new System.InvalidCastException("object in list could not be cast to ImageLibraryData");
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
