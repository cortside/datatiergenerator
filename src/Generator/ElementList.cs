using System;
using System.Collections;

namespace Spring2.DataTierGenerator.Generator {

    /// <summary>
    /// IElement generic collection
    /// </summary>
    public class ElementList : System.Collections.CollectionBase {
	
	// Indexer implementation.
	public IElement this[int index] {
	    get { return (IElement) List[index]; }
	    set { 
		List[index] = value;
	    }
	}

	public void Add(IElement value) {
	    List.Add(value);
	}

	public Boolean Contains(IElement value) {
	    return List.Contains(value);
	}
	
	public Int32 IndexOf(IElement value) {
	    return List.IndexOf(value);
	}
	
	public void Insert(Int32 index, IElement value) {
	    List.Insert(index, value);
	}

	public void Remove(int index) {
	    if (index > Count - 1 || index < 0) {
		throw new IndexOutOfRangeException();
	    } else {
		List.RemoveAt(index); 
	    }
	}

	public void Remove(IElement value) {
	    List.Remove(value); 
	}

	public void AddRange(IList list) {
	    foreach(Object o in list) {
		if (o is IElement) {
		    Add((IElement)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to IElement");
		}
	    }
	}

    }
}