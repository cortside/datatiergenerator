using System;

namespace Spring2.DataTierGenerator.Generator.Styler {

    /// <summary>
    /// StylerLine generic collection
    /// </summary>
    public class StylerLineList : System.Collections.CollectionBase {

	public static readonly StylerLineList UNSET = new StylerLineList(true);
	public static readonly StylerLineList DEFAULT = new StylerLineList(true);

	private Boolean immutable = false;

	private StylerLineList (Boolean immutable) {
	    this.immutable = immutable;
	}

	public StylerLineList() {
	}

	// Indexer implementation.
	public StylerLine this[int index] {
	    get { return (StylerLine) List[index]; }
	    set {
		if (!immutable) {
		    List[index] = value;
		} else {
		    throw new System.Data.ReadOnlyException();
		}
	    }
	}

	public void Add(StylerLine value) {
	    if (!immutable) {
		List.Add(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	public Boolean Contains(StylerLine value) {
	    return List.Contains(value);
	}

	public Int32 IndexOf(StylerLine value) {
	    return List.IndexOf(value);
	}

	public void Insert(Int32 index, StylerLine value) {
	    if (!immutable) {
		List.Insert(index, value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

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

	public void Remove(StylerLine value) {
	    if (!immutable) {
		List.Remove(value);
	    } else {
		throw new System.Data.ReadOnlyException();
	    }
	}

	public void AddRange(System.Collections.IList list) {
	    foreach(Object o in list) {
		if (o is StylerLine) {
		    Add((StylerLine)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to StylerLine");
		}
	    }
	}

    }
}
