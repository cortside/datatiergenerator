using System;

namespace Spring2.DataTierGenerator {
    public class SqlTypeData : Spring2.Core.DataObject.DataObject {

	private String name = String.Empty;
	private String type = String.Empty;
	private Int32 length = 0;
	private Int32 scale = 0;
	private Int32 precision = 0;
	private String readerMethodFormat = String.Empty;

	public String Name {
	    get { return this.name; }
	    set { this.name = value; }
	}

	public String Type {
	    get { return this.type; }
	    set { this.type = value; }
	}

	public Int32 Length {
	    get { return this.length; }
	    set { this.length = value; }
	}

	public Int32 Scale {
	    get { return this.scale; }
	    set { this.scale = value; }
	}

	public Int32 Precision {
	    get { return this.precision; }
	    set { this.precision = value; }
	}

	public String ReaderMethodFormat {
	    get { return this.readerMethodFormat; }
	    set { this.readerMethodFormat = value; }
	}

    }
}
