using Spring2.DataTierGenerator.Attribute;

using System;

using Spring2.Core.Types;


namespace Golf.Tournament.DataObject {

    /// <summary>
    /// ALaCarteItemData generic collection
    /// </summary>
    public class ALaCarteItemCollection : System.Collections.CollectionBase {

	[Generate]
	public static readonly ALaCarteItemCollection UNSET = new ALaCarteItemCollection(true);
	[Generate]
	public static readonly ALaCarteItemCollection DEFAULT = new ALaCarteItemCollection(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private ALaCarteItemCollection (Boolean immutable) {
	    this.immutable = immutable;
	}

	[Generate]
	public ALaCarteItemCollection() {
	}

	// Indexer implementation.
	[Generate]
	public ALaCarteItemData this[int index] {
	    get { return (ALaCarteItemData) List[index]; }
	    set {
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(ALaCarteItemData value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(ALaCarteItemData value) {
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(ALaCarteItemData value) {
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, ALaCarteItemData value) {
	    if (!immutable) {
		List.Insert(index, value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void Remove(int index) {
	    if (!immutable) {
		if (index > Count - 1 || index < 0) {
		    throw new IndexOutOfRangeException();
		} else {
		    List.RemoveAt(index);
		}
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void Remove(ALaCarteItemData value) {
	    if (!immutable) {
		List.Remove(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is ALaCarteItemData) {
		    Add((ALaCarteItemData)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to ALaCarteItemData");
		}
	    }
	}

	[Generate]
	public Boolean IsDefault {
	    get { return Object.ReferenceEquals(this, DEFAULT); }
	}

	[Generate]
	public Boolean IsUnset {
	    get { return Object.ReferenceEquals(this, UNSET); }
	}

	[Generate]
	public Boolean IsValid {
	    get {
		return !(IsDefault || IsUnset);
	    }
	}


	/// <summary>
	/// Sort a list by a column
	/// </summary>
	[Generate]
	public void Sort(System.Collections.IComparer comparer) {
	    this.InnerList.Sort(comparer);
	}

	/// <summary>
	/// Sort the list given the name of a comparer class.
	/// </summary>
	[Generate]
	public void Sort(String comparerName) {
	    Type type = GetType().GetNestedType(comparerName);
	    if (type == null) {
		throw new System.ArgumentException(String.Format("Comparer {0} not found in class {1}.", comparerName, GetType().Name));
	    }

	    System.Collections.IComparer comparer = Activator.CreateInstance(type) as System.Collections.IComparer;
	    if (comparer == null) {
		throw new System.ArgumentException("compareName must be the name of class that implements IComparer.");
	    }

	    this.InnerList.Sort(comparer);
	}

	[Generate]
	public class DescriptionSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		ALaCarteItemData o1 = (ALaCarteItemData)a;
		ALaCarteItemData o2 = (ALaCarteItemData)b;

		if (o1 == null || o2 == null || !o1.Description.IsValid || !o2.Description.IsValid) {
		    return 0;
		}
		return o1.Description.CompareTo(o2.Description);
	    }
	}

	[Generate]
	public class PriceSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		ALaCarteItemData o1 = (ALaCarteItemData)a;
		ALaCarteItemData o2 = (ALaCarteItemData)b;

		if (o1 == null || o2 == null || !o1.Price.IsValid || !o2.Price.IsValid) {
		    return 0;
		}
		return o1.Price.CompareTo(o2.Price);
	    }
	}

    }
}
