using System;
using System.IO;

namespace Spring2.DataTierGenerator {
    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    public class ResourceManager {

	public String AssemblyName {
	    get { return this.GetType().Assembly.GetName().Name; }
	}

	public Stream ConfigSchema {
	    get { 
		System.IO.Stream s = this.GetType().Assembly.GetManifestResourceStream(this.GetType(), "config.xsd");
		return s;
	    }
	}

	/// <summary>
	/// Get a resource by name - will find resource emebeded in assembly or in file system.
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	public Stream GetResource(String name) {
	    return null;
	}

    }
}
