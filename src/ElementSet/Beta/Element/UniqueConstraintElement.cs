using System;
using System.Xml;

namespace Spring2.DataTierGenerator.Element {

    /// <summary>
    /// Summary description for UniqueConstraintElement.
    /// </summary>
    public class UniqueConstraintElement : ConstraintElement {

	protected Boolean clustered = false;

	public UniqueConstraintElement(XmlNode constraintNode, SqlEntityElement sqlEntity) : base(constraintNode, sqlEntity) {
	    clustered = Boolean.Parse(GetAttributeValue(constraintNode, CLUSTERED, clustered.ToString()));
	}
    }
}
