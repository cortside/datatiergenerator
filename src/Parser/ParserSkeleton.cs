using System;

using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;

using Spring2.Core.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Element;

namespace Spring2.DataTierGenerator.Parser {
    /// <summary>
    /// Summary description for XMLParser.
    /// </summary>
    public abstract class ParserSkeleton : IParser {

	protected Configuration options = new Configuration();
	protected ArrayList entities = new ArrayList();
	protected ArrayList enumtypes = new ArrayList();
	protected ArrayList collections = new ArrayList();
	protected ArrayList databases = new ArrayList();
	protected Hashtable types = new Hashtable();
	protected Hashtable sqltypes = new Hashtable();

	protected Boolean isValid = false;
	protected IList errors = new ArrayList();

	public Configuration Configuration {
	    get { return options; }
	}

	public Boolean IsValid {
	    get { return isValid; }
	}

	public IList Errors {
	    get { return errors; }
	}

	public String ErrorDescription {
	    get {
		String s = String.Empty;
		foreach(Object o in errors) {
		    s += o.ToString() + Environment.NewLine;
		}
		return s;
	    }
	}

	public IList Databases {
	    get { return (ArrayList)databases.Clone(); }
	}

	public IList Entities {
	    get { return (ArrayList)entities.Clone(); }
	}

	public IList Enums {
	    get { return (ArrayList)enumtypes.Clone(); }
	}

	public IList Collections {
	    get { return (ArrayList)collections.Clone(); }
	}

	public ICollection Types {
	    get { return types.Values; }
	}

	public ICollection SqlTypes {
	    get { return sqltypes.Values; }
	}

    }
}
