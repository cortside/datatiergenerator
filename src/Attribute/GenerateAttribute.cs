using System;
using System.Reflection;

namespace Spring2.DataTierGenerator.Attribute {

    /// <summary>
    /// Indicates that a method or property is generated and contents can be replaced
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Constructor | AttributeTargets.Field, Inherited=false, AllowMultiple=false)]
    public class GenerateAttribute : System.Attribute {

	public GenerateAttribute() {            
	}

    }
}
