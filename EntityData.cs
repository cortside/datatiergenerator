using System;
using System.Collections;

namespace Spring2.DataTierGenerator {
    public class EntityData : Spring2.Core.DataObject.DataObject {

	protected String name = String.Empty;
	protected String sqlObject = String.Empty;
	protected Hashtable types = new Hashtable();
	protected Hashtable sqltypes = new Hashtable();

	public String Name {
	    get { return this.name; }
	    set { this.name = value; }
	}

	public String SqlObject {
	    get { return this.sqlObject; }
	    set { this.sqlObject = value; }
	}

	public Hashtable Types {
	    get { return this.types; }
	    set { this.types = value; }
	}
	public Hashtable SqlTypes {
	    get { return this.sqltypes; }
	    set { this.sqltypes = value; }
	}

    }
}
