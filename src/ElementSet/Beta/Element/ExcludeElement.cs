using System;
using System.Collections;
using System.Text;
using System.Xml;

using Spring2.DataTierGenerator;
using Spring2.DataTierGenerator.Parser;

namespace Spring2.DataTierGenerator.Element {

    public class ExcludeElement : ElementSkeleton {

	public static readonly String EXCLUDE = "EXCLUDE";

	public ExcludeElement() {}

	public ExcludeElement(XmlNode excludeNode) : base(excludeNode) {
	    if (!EXCLUDE.Equals(excludeNode.Name)) {
		throw new ArgumentException("The XmlNode argument is not an exclude node.");
	    }
	}

	public override void Validate(IParser parser) {
	    // No validation required.
	}
    }
}
