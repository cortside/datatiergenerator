if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spTournament_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spTournament_Update]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spTournament_Update

	@TournamentId	int,
	@Name	varchar(50),
	@Description	varchar(500),
	@NumberOfTeams	int,
	@TeamSize	char(1),
	@Format	char(1),
	@RegistrationBeginDate	datetime,
	@RegistrationEndDate	datetime,
	@RegistrationFee	money,
	@OrganizerId	int,
	@CancellationCutoffDate	datetime,
	@RegistrationFeeDescription	varchar(250),
	@DatesText	varchar(50),
	@PrizesText	varchar(1000),
	@SponsorsText	varchar(1000),
	@LocationsText	varchar(250),
	@MaximumHandicap	int,
	@Host	varchar(30),
	@ShowPercentFull	char(1),
	@ShowParticipants	char(1)

AS


UPDATE
	Tournament
SET
	Name = @Name,
	Description = @Description,
	NumberOfTeams = @NumberOfTeams,
	TeamSize = @TeamSize,
	Format = @Format,
	RegistrationBeginDate = @RegistrationBeginDate,
	RegistrationEndDate = @RegistrationEndDate,
	RegistrationFee = @RegistrationFee,
	OrganizerId = @OrganizerId,
	CancellationCutoffDate = @CancellationCutoffDate,
	RegistrationFeeDescription = @RegistrationFeeDescription,
	DatesText = @DatesText,
	PrizesText = @PrizesText,
	SponsorsText = @SponsorsText,
	LocationsText = @LocationsText,
	MaximumHandicap = @MaximumHandicap,
	Host = @Host,
	ShowPercentFull = @ShowPercentFull,
	ShowParticipants = @ShowParticipants
WHERE
TournamentId = @TournamentId


if @@ROWCOUNT <> 1
    BEGIN
        RAISERROR  ('spTournament_Update:  update was expected to update a single row and updated %d rows', 16,1, @@ROWCOUNT)
        RETURN(-1)
    END
GO

