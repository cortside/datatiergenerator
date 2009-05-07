using System;
using System.Collections;

namespace Spring2.DataTierGenerator.Element {

    /// <summary>
    /// Element to hold a list of data that can be used in a Task
    /// </summary>
    public class ListElement : ElementSkeleton {

	private IList list = new ArrayList();

	public IList List {
	    get { return this.list; }
	    set { this.list = value; }
	}

    }
}
