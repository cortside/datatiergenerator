using Spring2.DataTierGenerator.Attribute;

using System;

using Spring2.Core.Types;

using Golf.Tournament.DataObject;
using Golf.Tournament.Types;


namespace Golf.Tournament.DataObject {

    /// <summary>
    /// TeamData generic collection
    /// </summary>
    public class TeamCollection : System.Collections.CollectionBase {

	[Generate]
	public static readonly TeamCollection UNSET = new TeamCollection(true);
	[Generate]
	public static readonly TeamCollection DEFAULT = new TeamCollection(true);

	[Generate]
	private Boolean immutable = false;

	[Generate]
	private TeamCollection (Boolean immutable) {
	    this.immutable = immutable;
	}

	[Generate]
	public TeamCollection() {
	}

	// Indexer implementation.
	[Generate]
	public TeamData this[int index] {
	    get { return (TeamData) List[index]; }
	    set {
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	[Generate]
	public void Add(TeamData value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public Boolean Contains(TeamData value) {
	    return List.Contains(value);
	}

	[Generate]
	public Int32 IndexOf(TeamData value) {
	    return List.IndexOf(value);
	}

	[Generate]
	public void Insert(Int32 index, TeamData value) {
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
	public void Remove(TeamData value) {
	    if (!immutable) {
		List.Remove(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	[Generate]
	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is TeamData) {
		    Add((TeamData)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to TeamData");
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
	public Boolean Contains(IdType teamId) {
	    foreach(TeamData o in List) {
		if (o.TeamId.Equals(teamId)) {
		    return true;
		}
	    }
	    return false;
	}

	/// <summary>
	/// returns the instance by identity or null if it not found
	/// </summary>
	[Generate]
	public TeamData this[IdType teamId] {
	    get {
		foreach(TeamData o in List) {
		    if (o.TeamId.Equals(teamId)) {
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
	public TeamCollection RetainAll(TeamCollection list) {
	    TeamCollection result = new TeamCollection();

	    foreach(TeamData data in List) {
		if (list.Contains(data.TeamId)) {
		    result.Add(data);
		}
	    }

	    return result;
	}

	/// <summary>
	/// return a new list that contains only the elements not contained in the argument list
	/// </summary>
	[Generate]
	public TeamCollection RemoveAll(TeamCollection list) {
	    TeamCollection result = new TeamCollection();

	    foreach(TeamData data in List) {
		if (!list.Contains(data.TeamId)) {
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
	public class TeamIdSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		TeamData o1 = (TeamData)a;
		TeamData o2 = (TeamData)b;

		if (o1 == null || o2 == null || !o1.TeamId.IsValid || !o2.TeamId.IsValid) {
		    return 0;
		}
		return o1.TeamId.CompareTo(o2.TeamId);
	    }
	}

	[Generate]
	public class RegistrationKeySorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		TeamData o1 = (TeamData)a;
		TeamData o2 = (TeamData)b;

		if (o1 == null || o2 == null || !o1.RegistrationKey.IsValid || !o2.RegistrationKey.IsValid) {
		    return 0;
		}
		return o1.RegistrationKey.CompareTo(o2.RegistrationKey);
	    }
	}

	[Generate]
	public class TournamentIdSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		TeamData o1 = (TeamData)a;
		TeamData o2 = (TeamData)b;

		if (o1 == null || o2 == null || !o1.TournamentId.IsValid || !o2.TournamentId.IsValid) {
		    return 0;
		}
		return o1.TournamentId.CompareTo(o2.TournamentId);
	    }
	}

	[Generate]
	public class ParticipantsSorter : System.Collections.IComparer {
	    public Int32 Compare(Object a, Object b) {
		TeamData o1 = (TeamData)a;
		TeamData o2 = (TeamData)b;

		if (o1 == null || o2 == null || !o1.Participants.IsValid || !o2.Participants.IsValid) {
		    return 0;
		}
		return o1.Participants.CompareTo(o2.Participants);
	    }
	}

    }
}
