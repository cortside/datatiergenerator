using System;
using System.Xml;

namespace Spring2.DataTierGenerator.Element {
 
    /// <summary>
    /// Summary description for CheckConstraintElement.
    /// </summary>
    public class CheckConstraintElement : ConstraintElement {

	private String checkClause = String.Empty;

	public CheckConstraintElement(XmlNode constraintNode, SqlEntityElement sqlEntity) : base(constraintNode, sqlEntity) {
	    checkClause = GetAttributeValue(constraintNode, CHECK_CLAUSE, checkClause);
	}
    }
}
