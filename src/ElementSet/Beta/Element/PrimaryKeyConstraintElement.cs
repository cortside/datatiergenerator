using System;
using System.Xml;

namespace Spring2.DataTierGenerator.Element {

    /// <summary>
    /// Summary description for PrimaryKeyConstraintElement.
    /// </summary>
    public class PrimaryKeyConstraintElement : UniqueConstraintElement {
	public PrimaryKeyConstraintElement(XmlNode node, SqlEntityElement sqlEntity) : base(node, sqlEntity) {}
    }
}
