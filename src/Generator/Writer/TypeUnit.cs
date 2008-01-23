using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.NRefactory.Ast;

namespace Spring2.DataTierGenerator.Generator.Writer {
    public class TypeUnit {

    	private TypeDeclaration node;

	public TypeUnit(TypeDeclaration node) {
    	    this.node = node;
    	}

	public TypeDeclaration Node {
    	    get {
    	    	return node;
    	    }
    	}

    	public List<MemberUnit> Members {
    	    get {
		List<MemberUnit> members = new List<MemberUnit>();

    	    	return members;
    	    }
    	}

    	private void AddMembers(List<MemberUnit> members, IList list) {
    	    foreach(MemberNode member in list) {
    	    	members.Add(new MemberUnit(member));
    	    }
    	}

	public Boolean HasMember(MemberNode n) {
	    //foreach (MemberUnit member in Members) {
	    //    if (member.Node.Names[0].QualifiedIdentifier == n.Names[0].QualifiedIdentifier) {
	    //        return true;
	    //    }
	    //}

	    return false;
	}

	public MemberUnit GetMember(String name) {
	    //foreach (MemberUnit member in Members) {
	    //    if (member.Node.Names[0].QualifiedIdentifier == name) {
	    //        return member;
	    //    }
	    //}
	    return null;
	}

	public void AddMember(MemberNode member) {
	}

    	public void Merge(TypeUnit mergeType) {
	    foreach (MemberUnit mergeMember in mergeType.Members) {
		if (!HasMember(mergeMember.Node)) {
		    AddMember(mergeMember.Node);
		//} else {
		//    MemberUnit member = GetMember(mergeMember.Node.Names[0].QualifiedIdentifier);
		//    member.Merge(mergeMember);
		}
	    }
	}

    	public void RemoveGeneratedMembers() {
    	    foreach(MemberUnit member in Members) {
    	    	if (member.IsGenerated) {
    	    	    RemoveMember(member);
    	    	}
    	    }
    	}

	public void RemoveMember(MemberUnit member) {
	}
    }
}
