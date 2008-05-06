// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Daniel Grunwald" email="daniel@danielgrunwald.de"/>
//     <version>$Revision: 1.2 $</version>
// </file>

using System;
using System.Collections;
using System.Collections.Generic;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.Ast;
using ICSharpCode.NRefactory.PrettyPrinter;

namespace Spring2.DataTierGenerator.Generator.Writer {

    public class SpecialsContainer {
    	private IList<ISpecial> nodeStart = new List<ISpecial>();
	private IList<ISpecial> nodeEnd = new List<ISpecial>();

    	public IList<ISpecial> NodeStart {
    	    get { return nodeStart; }
    	}

	public IList<ISpecial> NodeEnd {
	    get { return nodeEnd; }
	}
    }

    public class SpecialOutputVisitor : ISpecialVisitor {
	readonly IOutputFormatter formatter;
	private SpecialsContainer startAndEndContainer;

	public SpecialOutputVisitor(IOutputFormatter formatter) {
	    this.formatter = formatter;
	    startAndEndContainer = new SpecialsContainer();
	}

	public bool ForceWriteInPreviousLine;

	public object Visit(ISpecial special, object data) {
	    if (!specialHasBeenHandled(special)) {
		Console.WriteLine("Warning: SpecialOutputVisitor.Visit(ISpecial) called with " + special);
		considerSpecialAsHandled(special);
	    }
	    return data;
	}

	public object Visit(BlankLine special, object data) {
	    if (!specialHasBeenHandled(special)) {
		formatter.PrintBlankLine(ForceWriteInPreviousLine);
		considerSpecialAsHandled(special);
	    }
	    return data;
	}

	public object Visit(Comment special, object data) {
	    if (!specialHasBeenHandled(special)) {
		formatter.PrintComment(special, ForceWriteInPreviousLine);
		considerSpecialAsHandled(special);
	    }
	    return data;
	}

	public object Visit(PreprocessingDirective special, object data) {
	    if (!specialHasBeenHandled(special)) {
		formatter.PrintPreprocessingDirective(special, ForceWriteInPreviousLine);
		considerSpecialAsHandled(special);
	    }
	    return data;
	}

	private bool specialHasBeenHandled(ISpecial special) {
	    int posOfStart = startAndEndContainer.NodeStart.IndexOf(special);
	    int posOfEnd = startAndEndContainer.NodeEnd.IndexOf(special);
	    bool alreadyHandled = (posOfStart == posOfEnd) && (posOfStart != -1);
	    return alreadyHandled;
	}

	private void considerSpecialAsHandled(ISpecial special) {
	    startAndEndContainer.NodeStart.Add(special);
	    startAndEndContainer.NodeEnd.Add(special);
	}
    }

    /// <summary>
    /// This class inserts specials between INodes.
    /// </summary>
    public sealed class SpecialNodesByMapInserter : IDisposable {
	//IEnumerator<ISpecial> enumerator;
	private SpecialOutputVisitor visitor;
	//bool available; // true when more specials are available
    	private Hashtable specialsMap;

	public SpecialNodesByMapInserter(Hashtable specialsMap, SpecialOutputVisitor visitor) {
	    if (specialsMap == null) throw new ArgumentNullException("specialsMap");
	    if (visitor == null) throw new ArgumentNullException("visitor");
	    //enumerator = specialsMap.GetEnumerator();
	    this.visitor = visitor;
	    //available = enumerator.MoveNext();
	    this.specialsMap = specialsMap;
	}

	//void WriteCurrent() {
	//    enumerator.Current.AcceptVisitor(visitor, null);
	//    available = enumerator.MoveNext();
	//}

	//AttributedNode currentAttributedNode;

	/// <summary>
	/// Writes all specials up to the start position of the node.
	/// </summary>
	public void AcceptNodeStart(INode node) {
	    SpecialsContainer specials = node.UserData as SpecialsContainer;
	    if (specials != null) {
		foreach(ISpecial special in specials.NodeStart) {
		    special.AcceptVisitor(visitor, null);
		}
	    }
	}

	/// <summary>
	/// Writes all specials up to the end position of the node.
	/// </summary>
	public void AcceptNodeEnd(INode node) {
	    visitor.ForceWriteInPreviousLine = true;
	    SpecialsContainer specials = node.UserData as SpecialsContainer;
	    if (specials != null) {
		foreach (ISpecial special in specials.NodeEnd) {
		    special.AcceptVisitor(visitor, null);
		}
	    }
	    visitor.ForceWriteInPreviousLine = false;
	}

	///// <summary>
	///// Writes all specials up to the specified location.
	///// </summary>
	//public void AcceptPoint(Location loc) {
	//    while (available && enumerator.Current.StartPosition <= loc) {
	//        WriteCurrent();
	//    }
	//}

	/// <summary>
	/// Outputs all missing specials to the writer.
	/// </summary>
	public void Finish() {
	    //while (available) {
	    //    WriteCurrent();
	    //}
	}

	void IDisposable.Dispose() {
	    Finish();
	}

	/// <summary>
	/// Registers a new SpecialNodesInserter with the output visitor.
	/// Make sure to call Finish() (or Dispose()) on the returned SpecialNodesInserter
	/// when the output is finished.
	/// </summary>
	public static SpecialNodesByMapInserter Install(Hashtable specialsMap, IOutputAstVisitor outputVisitor) {
	    SpecialNodesByMapInserter sni = new SpecialNodesByMapInserter(specialsMap, new SpecialOutputVisitor(outputVisitor.OutputFormatter));
	    outputVisitor.BeforeNodeVisit += sni.AcceptNodeStart;
	    outputVisitor.AfterNodeVisit += sni.AcceptNodeEnd;
	    return sni;
	}
    }

}
