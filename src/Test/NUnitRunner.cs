using System;

namespace Spring2.DataTierGenerator.Test {
    public class NUnitRunner {
	public static void Main(String[] args) { 
	    NUnit.TextUI.TestRunner.Run(AllTests.Suite);
	    Console.Out.WriteLine();
	    Console.Out.WriteLine("press any key to close");
	    Console.Read();
	} 

    }
}
