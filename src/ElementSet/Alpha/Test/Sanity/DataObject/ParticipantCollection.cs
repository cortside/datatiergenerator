using Spring2.DataTierGenerator.Attribute;

using System;
using System.Collections;

using Spring2.Core.Types;

using Golf.Tournament.DataObject;
using Golf.Tournament.Types;


namespace Golf.Tournament.DataObject {

    /// <summary>
    /// ParticipantData generic collection
    /// </summary>
    public class ParticipantCollection : System.Collections.CollectionBase {

	[Generate]
	public static readonly ParticipantCollection UNSET = new ParticipantCollection(true);
	[Generate]
	public static readonly ParticipantCollection DEFAULT = new ParticipantCollection(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private ParticipantCollection (Boolean immutable) {
	    this.immutable = immutable;
	}

	[Generate]
	public ParticipantCollection() {
	}

	// Indexer implementation.
	[Generate]
	public ParticipantData this[int index] {
	    get { return (ParticipantData) List[index]; }
	    set {
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(ParticipantData value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(ParticipantData value) {
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(ParticipantData value) {
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, ParticipantData value) {
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
	public void Remove(ParticipantData value) {
	    if (!immutable) {
		List.Remove(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is ParticipantData) {
		    Add((ParticipantData)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to ParticipantData");
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
	public Boolean Contains(IdType participantId) {
	    foreach(ParticipantData o in List) {
		if (o.ParticipantId.Equals(participantId)) {
		    return true;
		}
	    }
	    return false;
	}

	/// <summary>
	/// returns the instance by identity or null if it not found
	/// </summary>
	[Generate]
	public ParticipantData this[IdType participantId] {
	    get {
		foreach(ParticipantData o in List) {
		    if (o.ParticipantId.Equals(participantId)) {
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
	public ParticipantCollection RetainAll(ParticipantCollection list) {
	    ParticipantCollection result = new ParticipantCollection();

	    foreach(ParticipantData data in List) {
		if (list.Contains(data.ParticipantId)) {
		    result.Add(data);
		}
	    }

	    return result;
	}

	/// <summary>
	/// return a new list that contains only the elements not contained in the argument list
	/// </summary>
	[Generate]
	public ParticipantCollection RemoveAll(ParticipantCollection list) {
	    ParticipantCollection result = new ParticipantCollection();

	    foreach(ParticipantData data in List) {
		if (!list.Contains(data.ParticipantId)) {
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
	public class ParticipantIdSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		ParticipantData o1 = (ParticipantData)a;
		ParticipantData o2 = (ParticipantData)b;

		if (o1 == null || o2 == null || !o1.ParticipantId.IsValid || !o2.ParticipantId.IsValid) {
		    return 0;
		}
		return o1.ParticipantId.CompareTo(o2.ParticipantId);
	    }
	}

	[Generate]
	public class RegistrationFeeSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		ParticipantData o1 = (ParticipantData)a;
		ParticipantData o2 = (ParticipantData)b;

		if (o1 == null || o2 == null || !o1.RegistrationFee.IsValid || !o2.RegistrationFee.IsValid) {
		    return 0;
		}
		return o1.RegistrationFee.CompareTo(o2.RegistrationFee);
	    }
	}

    }
}
