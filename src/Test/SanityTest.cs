using System;
using System.Collections;
using System.IO;

using NUnit.Framework;

using Spring2.Core.Util;

using Spring2.DataTierGenerator.Parser;
using Spring2.DataTierGenerator.Generator;

namespace Spring2.DataTierGenerator.Test {

    /// <summary>
    /// Tests that should provide confidence that the DTG is generating source as expected
    /// </summary>
    [TestFixture]
    public class SanityTest {

	[Test]
	public void TestSanity() {
	    IParser parser = new XmlParser("..\\..\\src\\Test\\Sanity\\sanity.xml");
	    if (!parser.IsValid) {
		foreach(String s in parser.Log) {
		    Console.Out.WriteLine(s);
		}
	    } else {
		IGenerator gen = new GeneratorTaskManager(parser);
		if (Directory.Exists(@".\SanityTest")) {
		    Directory.Delete(@".\SanityTest", true);
		}
		gen.Generate();
		foreach(String s in gen.Log) {
		    Console.Out.WriteLine(s);
		}

		CompareResults("..\\..\\src\\Test\\Sanity");
	    }
	}


	private void CompareResults(String compareRoot) {
	    Boolean pass = true;

	    pass = pass && CompareFile(compareRoot, "DAO", "GolferDAO.cs");
	    pass = pass && CompareFile(compareRoot, "DAO", "OrganizerDAO.cs");
	    pass = pass && CompareFile(compareRoot, "DAO", "ParticipantDAO.cs");
	    pass = pass && CompareFile(compareRoot, "DAO", "PaymentDAO.cs");
	    pass = pass && CompareFile(compareRoot, "DAO", "TeamDAO.cs");
	    pass = pass && CompareFile(compareRoot, "DAO", "TournamentDAO.cs");
	    pass = pass && CompareFile(compareRoot, "DAO", "TournamentFeeDAO.cs");

	    pass = pass && CompareFile(compareRoot, "DataObject", "ALaCarteItemCollection.cs");
	    pass = pass && CompareFile(compareRoot, "DataObject", "ParticipantCollection.cs");
	    pass = pass && CompareFile(compareRoot, "DataObject", "TeamCollection.cs");
	    pass = pass && CompareFile(compareRoot, "DataObject", "TournamentFeeCollection.cs");

	    pass = pass && CompareFile(compareRoot, "DataObject", "AddressData.cs");
	    pass = pass && CompareFile(compareRoot, "DataObject", "ALaCarteItemData.cs");
	    pass = pass && CompareFile(compareRoot, "DataObject", "ContactData.cs");
	    pass = pass && CompareFile(compareRoot, "DataObject", "CreditCardData.cs");
	    pass = pass && CompareFile(compareRoot, "DataObject", "FlightData.cs");
	    pass = pass && CompareFile(compareRoot, "DataObject", "GolferData.cs");
	    pass = pass && CompareFile(compareRoot, "DataObject", "LocationData.cs");
	    pass = pass && CompareFile(compareRoot, "DataObject", "NotificationData.cs");
	    pass = pass && CompareFile(compareRoot, "DataObject", "OrganizationData.cs");
	    pass = pass && CompareFile(compareRoot, "DataObject", "OrganizerData.cs");
	    pass = pass && CompareFile(compareRoot, "DataObject", "PairingData.cs");
	    pass = pass && CompareFile(compareRoot, "DataObject", "ParticipantData.cs");
	    pass = pass && CompareFile(compareRoot, "DataObject", "PaymentData.cs");
	    pass = pass && CompareFile(compareRoot, "DataObject", "PrizeData.cs");
	    pass = pass && CompareFile(compareRoot, "DataObject", "ProfileData.cs");
	    pass = pass && CompareFile(compareRoot, "DataObject", "RoundData.cs");
	    pass = pass && CompareFile(compareRoot, "DataObject", "ScorecardData.cs");
	    pass = pass && CompareFile(compareRoot, "DataObject", "SponsorData.cs");
	    pass = pass && CompareFile(compareRoot, "DataObject", "TeamData.cs");
	    pass = pass && CompareFile(compareRoot, "DataObject", "TournamentData.cs");
	    pass = pass && CompareFile(compareRoot, "DataObject", "TournamentFeeData.cs");
	    pass = pass && CompareFile(compareRoot, "DataObject", "WaitListData.cs");

	    pass = pass && CompareFile(compareRoot, "Sql\\proc", "spGolfer_Delete.proc.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\proc", "spGolfer_Insert.proc.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\proc", "spGolfer_Update.proc.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\proc", "spOrganizer_Delete.proc.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\proc", "spOrganizer_Insert.proc.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\proc", "spOrganizer_Update.proc.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\proc", "spParticipant_Delete.proc.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\proc", "spParticipant_Insert.proc.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\proc", "spParticipant_Update.proc.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\proc", "spPayment_Delete.proc.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\proc", "spPayment_Insert.proc.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\proc", "spPayment_Update.proc.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\proc", "spTeam_Delete.proc.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\proc", "spTeam_Insert.proc.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\proc", "spTeam_Update.proc.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\proc", "spTournament_Delete.proc.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\proc", "spTournament_Insert.proc.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\proc", "spTournament_Update.proc.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\proc", "spTournamentFee_Delete.proc.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\proc", "spTournamentFee_Insert.proc.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\proc", "spTournamentFee_Update.proc.sql");

	    pass = pass && CompareFile(compareRoot, "Sql\\table", "Golfer.table.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\table", "Organizer.table.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\table", "Participant.table.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\table", "Payment.table.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\table", "Team.table.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\table", "Tournament.table.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\table", "TournamentFee.table.sql");

	    pass = pass && CompareFile(compareRoot, "Sql\\view", "vwGolfer.view.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\view", "vwOrganizer.view.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\view", "vwPayment.view.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\view", "vwTeam.view.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\view", "vwTournamentFee.view.sql");

	    pass = pass && CompareFile(compareRoot, "Sql\\data", "GolferStatusEnum.data.sql");
	    pass = pass && CompareFile(compareRoot, "Sql\\data", "FunctionEnum.data.sql");

	    pass = pass && CompareFile(compareRoot, "Test", "CreditCardTypeEnumTest.cs");
	    pass = pass && CompareFile(compareRoot, "Test", "ExpMonthEnumTest.cs");
	    pass = pass && CompareFile(compareRoot, "Test", "ExpYearEnumTest.cs");
	    pass = pass && CompareFile(compareRoot, "Test", "GolferStatusEnumTest.cs");
	    pass = pass && CompareFile(compareRoot, "Test", "PaymentStatusEnumTest.cs");
	    pass = pass && CompareFile(compareRoot, "Test", "TeamSizeEnumTest.cs");
	    pass = pass && CompareFile(compareRoot, "Test", "TeamStatusEnumTest.cs");
	    pass = pass && CompareFile(compareRoot, "Test", "TournamentFormatEnumTest.cs");
	    pass = pass && CompareFile(compareRoot, "Test", "USStateEnumTest.cs");

	    pass = pass && CompareFile(compareRoot, "Types", "CreditCardTypeEnum.cs");
	    pass = pass && CompareFile(compareRoot, "Types", "ExpMonthEnum.cs");
	    pass = pass && CompareFile(compareRoot, "Types", "ExpYearEnum.cs");
	    pass = pass && CompareFile(compareRoot, "Types", "GolferStatusEnum.cs");
	    pass = pass && CompareFile(compareRoot, "Types", "PaymentStatusEnum.cs");
	    pass = pass && CompareFile(compareRoot, "Types", "TeamSizeEnum.cs");
	    pass = pass && CompareFile(compareRoot, "Types", "TeamStatusEnum.cs");
	    pass = pass && CompareFile(compareRoot, "Types", "TournamentFormatEnum.cs");
	    pass = pass && CompareFile(compareRoot, "Types", "USStateEnum.cs");

	    if (!pass) {
		Assertion.Fail("more than 1 output file did not match it's compare file.  Sanity is just an illusion.");
	    }
	}


	private Boolean CompareFile(String compareRoot, String directory, String filename) {
	    if (!isMatch("SanityTest\\" + directory, compareRoot + "\\" +directory, filename, "", "cmp")) {
		Console.Out.WriteLine(directory + "\\" + filename + "...FAIL");
		return false;
	    }
	    Console.Out.WriteLine(directory + "\\" + filename + "...PASS");
	    return true;
	}


	protected internal virtual bool isMatch(System.String resultsDir, System.String compareDir, System.String baseFileName, System.String resultExt, System.String compareExt) {
	    Boolean SHOW_RESULTS = true;

	    String basename = resultsDir + "\\" + baseFileName;
	    System.String result = StringUtil.fileContentsToString(resultsDir + "\\" + baseFileName);
	    System.String compare = StringUtil.fileContentsToString(compareDir + "\\" + baseFileName + "." + compareExt);
			
	    Boolean equals = result.Equals(compare);
	    if (!equals && SHOW_RESULTS) {
		Console.Out.WriteLine(basename + " :: ");
		String[] cmp = compare.Split(Environment.NewLine.ToCharArray());
		String[] res = result.Split(Environment.NewLine.ToCharArray());

		IEnumerator cmpi = cmp.GetEnumerator();
		IEnumerator resi = res.GetEnumerator();
		Int32 line =0;
		while (cmpi.MoveNext() && resi.MoveNext()) {
		    line++;
		    String s1 = resi.Current.ToString().Replace("\t", "        ").Trim();
		    String s2 = cmpi.Current.ToString().Replace("\t", "        ").Trim();
		    if (!s1.Equals(s2)) {
			Console.Out.WriteLine(line.ToString() + " : " + cmpi.Current.ToString());
			Console.Out.WriteLine(line.ToString() + " : " + resi.Current.ToString());
		    }
		}
	    }
	    return equals;
	}


    }
}
