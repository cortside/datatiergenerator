using System;

using Spring2.Core.Types;

using Golf.Tournament.DataObject;
using Golf.Tournament.Types;

namespace Golf.Tournament.DataObject {
    public class TeamData : Spring2.Core.DataObject.DataObject {

	private IdType teamId = IdType.DEFAULT;
	private StringType registrationKey = StringType.DEFAULT;
	private TeamStatusEnum status = TeamStatusEnum.DEFAULT;
	private IdType tournamentId = IdType.DEFAULT;
	private ParticipantCollection participants = ParticipantCollection.DEFAULT;

	public static readonly String TEAMID = "TeamId";
	public static readonly String REGISTRATIONKEY = "RegistrationKey";
	public static readonly String STATUS = "Status";
	public static readonly String TOURNAMENTID = "TournamentId";
	public static readonly String PARTICIPANTS = "Participants";

	public IdType TeamId {
	    get { return this.teamId; }
	    set { this.teamId = value; }
	}

	public StringType RegistrationKey {
	    get { return this.registrationKey; }
	    set { this.registrationKey = value; }
	}

	public TeamStatusEnum Status {
	    get { return this.status; }
	    set { this.status = value; }
	}

	public IdType TournamentId {
	    get { return this.tournamentId; }
	    set { this.tournamentId = value; }
	}

	/// <summary>
	/// List of ParticipantData objects.
	/// </summary>
	public ParticipantCollection Participants {
	    get { return this.participants; }
	    set { this.participants = value; }
	}
    }
}
