using System;

namespace Spring2.DataTierGenerator {
    public class TypeData : Spring2.Core.DataObject.DataObject {

	private String name = String.Empty;
	private String concreteType = String.Empty;
	private String package = String.Empty;
	private String convertToSqlTypeFormat = "{0}.{1}";
	private String convertFromSqlTypeFormat = "{2}";
	private String newInstanceFormat = String.Empty;

	public String Name {
	    get { return this.name; }
	    set { this.name = value; }
	}

	public String ConcreteType {
	    get { return this.concreteType; }
	    set { this.concreteType = value; }
	}
	    
	public String Package {
	    get { return this.package; }
	    set { this.package = value; }
	}

	public String ConvertToSqlTypeFormat {
	    get { return this.convertToSqlTypeFormat; }
	    set { this.convertToSqlTypeFormat = value; }
	}

	public String ConvertFromSqlTypeFormat {
	    get { return this.convertFromSqlTypeFormat; }
	    set { this.convertFromSqlTypeFormat = value; }
	}

	public String NewInstanceFormat {
	    get { return this.newInstanceFormat; }
	    set { this.newInstanceFormat = value; }
	}

    }
}
