using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.NRefactory.Ast;

namespace Spring2.DataTierGenerator.Generator.Writer {
    public class MemberUnit {

    	private MemberNode node;

	public MemberUnit(MemberNode member) {
	    node = member;
	}

    	public MemberNode Node {
    	    get {
    	    	return node;
    	    }
    	}

    	public Boolean IsGenerated {
    	    get {
		//foreach(AttributeNode attribute in node.Attributes) {
		//    if (attribute.Name.Equals("Generate")) {
		//        return true;
		//    }
		//}

    	    	return false;
    	    }
    	}



    }
}
