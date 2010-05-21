// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Daniel Grunwald" email="daniel@danielgrunwald.de"/>
//     <version>$Revision: 1.1 $</version>
// </file>

using System;
using System.Collections.Generic;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Ast;
using ICSharpCode.NRefactory.PrettyPrinter;

namespace Spring2.DataTierGenerator.Generator.Writer {

    public enum SpecialLocation {
    	NodeStart,
	NodeEnd
    }

    /// <summary>
    /// This class inserts specials between INodes.
    /// </summary>
    public sealed class SpecialNodesUserDataInserter : IDisposable {
	IEnumerator<ISpecial> enumerator;
	SpecialOutputVisitor visitor;
	bool available; // true when more specials are available

	public SpecialNodesUserDataInserter(IEnumerable<ISpecial> specials, SpecialOutputVisitor visitor) {
	    if (specials == null) throw new ArgumentNullException("specials");
	    if (visitor == null) throw new ArgumentNullException("visitor");
	    enumerator = specials.GetEnumerator();
	    this.visitor = visitor;
	    available = enumerator.MoveNext();
	}

	void WriteCurrent(INode node, SpecialLocation location) {
	    SpecialsContainer container = node.UserData as SpecialsContainer;
	    if (container == null) {
	    	container = new SpecialsContainer();
	    	node.UserData = container;
	    }

	    if (location == SpecialLocation.NodeStart) {
	    	container.NodeStart.Add(enumerator.Current);
	    } else {
	    	container.NodeEnd.Add(enumerator.Current);
	    }
	    available = enumerator.MoveNext();
	}

	AttributedNode currentAttributedNode;

	/// <summary>
	/// Writes all specials up to the start position of the node.
	/// </summary>
	public void AcceptNodeStart(INode node) {
	    if (node is AttributedNode) {
		currentAttributedNode = node as AttributedNode;
		if (currentAttributedNode.Attributes.Count == 0) {
		    AcceptPoint(node.StartLocation, node, SpecialLocation.NodeStart);
		    currentAttributedNode = null;
		}
	    } else {
		AcceptPoint(node.StartLocation, node, SpecialLocation.NodeStart);
	    }
	}

	/// <summary>
	/// Writes all specials up to the end position of the node.
	/// </summary>
	public void AcceptNodeEnd(INode node) {
	    visitor.ForceWriteInPreviousLine = true;
	    AcceptPoint(node.EndLocation, node, SpecialLocation.NodeEnd);
	    visitor.ForceWriteInPreviousLine = false;
	    if (currentAttributedNode != null) {
		if (node == currentAttributedNode.Attributes[currentAttributedNode.Attributes.Count - 1]) {
		    AcceptPoint(currentAttributedNode.StartLocation, currentAttributedNode, SpecialLocation.NodeStart);
		    currentAttributedNode = null;
		}
	    }
	}

	/// <summary>
	/// Writes all specials up to the specified location.
	/// </summary>
	public void AcceptPoint(Location loc, INode node, SpecialLocation location) {
	    while (available && enumerator.Current.StartPosition <= loc) {
		WriteCurrent(node, location);
	    }
	}

	/// <summary>
	/// Outputs all missing specials to the writer.
	/// </summary>
	public void Finish() {
	    while (available) {
		// TODO: how to handle the specials at the end....
		// TODO: maybe UserData should be an object that has both before and after collections of specials???
		//WriteCurrent();
		System.Diagnostics.Debug.WriteLine("finish => " + enumerator.Current.ToString());
		available = false;
	    }
	}

	void IDisposable.Dispose() {
	    Finish();
	}

	/// <summary>
	/// Registers a new SpecialNodesInserter with the output visitor.
	/// Make sure to call Finish() (or Dispose()) on the returned SpecialNodesInserter
	/// when the output is finished.
	/// </summary>
	public static SpecialNodesUserDataInserter Install(IEnumerable<ISpecial> specials, IOutputAstVisitor outputVisitor) {
	    SpecialNodesUserDataInserter sni = new SpecialNodesUserDataInserter(specials, new SpecialOutputVisitor(outputVisitor.OutputFormatter));
	    outputVisitor.BeforeNodeVisit += sni.AcceptNodeStart;
	    outputVisitor.AfterNodeVisit += sni.AcceptNodeEnd;
	    return sni;
	}
    }
}
