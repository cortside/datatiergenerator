using System;
using System.Collections.Generic;
using ICSharpCode.NRefactory.Ast;

namespace Spring2.DataTierGenerator.Generator.Writer {

    public class NamespaceUnit {

	private NamespaceDeclaration node;
	private List<TypeUnit> types = new List<TypeUnit>();

	public NamespaceUnit() {
	    node = null;
	}

	public NamespaceUnit(NamespaceDeclaration ns) {
	    this.node = ns;
	}

	public NamespaceDeclaration Node {
	    get {
		return node;
	    }
	}

	public List<TypeUnit> Types {
	    get {
		return types;
	    }
	}

	public Boolean HasType(TypeUnit n) {
	    foreach (TypeUnit type in Types) {
		if (type.Node.Name == n.Node.Name) {
		    return true;
		}
	    }

	    return false;
	}

	public TypeUnit GetType(String name) {
	    foreach (TypeUnit type in Types) {
		if (type.Node.Name.Equals(name)) {
		    return type;
		}
	    }
	    return null;
	}

	public void AddType(TypeUnit type) {
	    types.Add(type);
	}

	public void Merge(NamespaceUnit mergeNamespace) {
	    foreach (TypeUnit mergeType in Types) {
		if (!HasType(mergeType)) {
		    AddType(mergeType);
		} else {
		    TypeUnit type = GetType(mergeType.Node.Name);
		    type.Merge(mergeType);
		}
	    }
	}

	public void RemoveGeneratedMembers() {
	    foreach (TypeUnit type in Types) {
		type.RemoveGeneratedMembers();
	    }
	}
    }
}
