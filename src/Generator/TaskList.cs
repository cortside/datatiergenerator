using System;
using System.Collections;

namespace Spring2.DataTierGenerator.Generator {

    /// <summary>
    /// ITask generic collection
    /// </summary>
    public class TaskList : System.Collections.CollectionBase {
	
	// Indexer implementation.
	public ITask this[int index] {
	    get { return (ITask) List[index]; }
	    set { 
		List[index] = value;
	    }
	}

	public void Add(ITask value) {
	    List.Add(value);
	}

	public Boolean Contains(ITask value) {
	    return List.Contains(value);
	}
	
	public Int32 IndexOf(ITask value) {
	    return List.IndexOf(value);
	}
	
	public void Insert(Int32 index, ITask value) {
	    List.Insert(index, value);
	}

	public void Remove(int index) {
	    if (index > Count - 1 || index < 0) {
		throw new IndexOutOfRangeException();
	    } else {
		List.RemoveAt(index); 
	    }
	}

	public void Remove(ITask value) {
	    List.Remove(value); 
	}

	public void AddRange(IList list) {
	    foreach(Object o in list) {
		if (o is ITask) {
		    Add((ITask)o);
		} else {
		    throw new System.InvalidCastException("object in list could not be cast to ITask");
		}
	    }
	}

    }
}