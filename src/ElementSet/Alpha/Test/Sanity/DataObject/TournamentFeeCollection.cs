using Spring2.DataTierGenerator.Attribute;

using System;

using Spring2.Core.Types;


namespace Golf.Tournament.DataObject {

    /// <summary>
    /// TournamentFeeData generic collection
    /// </summary>
    public class TournamentFeeCollection : System.Collections.CollectionBase {

	[Generate]
	public static readonly TournamentFeeCollection UNSET = new TournamentFeeCollection(true);
	[Generate]
	public static readonly TournamentFeeCollection DEFAULT = new TournamentFeeCollection(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private TournamentFeeCollection (Boolean immutable) {
	    this.immutable = immutable;
	}

	[Generate]
	public TournamentFeeCollection() {
	}

	// Indexer implementation.
	[Generate]
	public TournamentFeeData this[int index] {
	    get { return (TournamentFeeData) List[index]; }
	    set {
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(TournamentFeeData value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(TournamentFeeData value) {
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(TournamentFeeData value) {
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, TournamentFeeData value) {
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
	public void Remove(TournamentFeeData value) {
	    if (!immutable) {
		List.Remove(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is TournamentFeeData) {
		    Add((TournamentFeeData)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to TournamentFeeData");
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
	/// See if the list contains an instance by identity
	/// </summary>
	[Generate]
	public Boolean Contains(IdType tournamentFeeId) {
	    foreach(TournamentFeeData o in List) {
		if (o.TournamentFeeId.Equals(tournamentFeeId)) {
		    return true;
		}
	    }
	    return false;
	}

	/// <summary>
	/// returns the instance by identity or null if it not found
	/// </summary>
	[Generate]
	public TournamentFeeData this[IdType tournamentFeeId] {
	    get {
		foreach(TournamentFeeData o in List) {
		    if (o.TournamentFeeId.Equals(tournamentFeeId)) {
			return o;
		    }
		}

		// not found
		return null;
	    }
	}

	/// <summary>
	/// Returns a new list that contains all of the elements that are in both lists
	/// </summary>
	[Generate]
	public TournamentFeeCollection RetainAll(TournamentFeeCollection list) {
	    TournamentFeeCollection result = new TournamentFeeCollection();

	    foreach(TournamentFeeData data in List) {
		if (list.Contains(data.TournamentFeeId)) {
		    result.Add(data);
		}
	    }

	    return result;
	}

	/// <summary>
	/// return a new list that contains only the elements not contained in the argument list
	/// </summary>
	[Generate]
	public TournamentFeeCollection RemoveAll(TournamentFeeCollection list) {
	    TournamentFeeCollection result = new TournamentFeeCollection();

	    foreach(TournamentFeeData data in List) {
		if (!list.Contains(data.TournamentFeeId)) {
		    result.Add(data);
		}
	    }

	    return result;
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
	public class TournamentFeeIdSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		TournamentFeeData o1 = (TournamentFeeData)a;
		TournamentFeeData o2 = (TournamentFeeData)b;

		if (o1 == null || o2 == null || !o1.TournamentFeeId.IsValid || !o2.TournamentFeeId.IsValid) {
		    return 0;
		}
		return o1.TournamentFeeId.CompareTo(o2.TournamentFeeId);
	    }
	}

	[Generate]
	public class TournamentIdSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		TournamentFeeData o1 = (TournamentFeeData)a;
		TournamentFeeData o2 = (TournamentFeeData)b;

		if (o1 == null || o2 == null || !o1.TournamentId.IsValid || !o2.TournamentId.IsValid) {
		    return 0;
		}
		return o1.TournamentId.CompareTo(o2.TournamentId);
	    }
	}

	[Generate]
	public class KeySorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		TournamentFeeData o1 = (TournamentFeeData)a;
		TournamentFeeData o2 = (TournamentFeeData)b;

		if (o1 == null || o2 == null || !o1.Key.IsValid || !o2.Key.IsValid) {
		    return 0;
		}
		return o1.Key.CompareTo(o2.Key);
	    }
	}

	[Generate]
	public class FeeSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		TournamentFeeData o1 = (TournamentFeeData)a;
		TournamentFeeData o2 = (TournamentFeeData)b;

		if (o1 == null || o2 == null || !o1.Fee.IsValid || !o2.Fee.IsValid) {
		    return 0;
		}
		return o1.Fee.CompareTo(o2.Fee);
	    }
	}

    }
}
