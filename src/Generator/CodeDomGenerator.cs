using System;
using System.IO;

using IvanZ.CSParser;
using Mono.CSharp;
using System.CodeDom;


namespace Spring2.DataTierGenerator.Generator {

    /// <summary>
    /// Summary description for CodeDomGenerator.
    /// </summary>
    public class CodeDomGenerator {

	public CodeDomGenerator() {
	}

	void CreateTree() {
	    StringWriter sw =new StringWriter();
	    Console.SetOut(sw);
	    Console.SetError(sw);
           
	    FileStream f = null;
	    string fname = null;

	    CSharpParser p= new CSharpParser(fname, f, null);
			
	    int errno= p.parse();
	    if (errno>0 || Report.Warnings>0) {
		//ShowError(sw);
	    }

	    CodeCompileUnit unit= p.Builder.CurrCompileUnit;
	    if (unit!=null) {
		//		    treeView1.BeginUpdate();
		//		    treeView1.Nodes.Clear();
		//		    new TreeAnalyzer(new TreeCreator(treeView1),unit).DoAnalysis();
		//		    treeView1.EndUpdate();
	    }
	}

    }
}
