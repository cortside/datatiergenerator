if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spParticipant_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spParticipant_Update]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spParticipant_Update

	@ParticipantId	int,
	@TeamId	int,
	@TournamentId	int,
	@GolferId	int,
	@PaymentId	int,
	@IsValid	char(1),
	@RegistrationFee	money

AS


UPDATE
	Participant
SET
	TeamId = @TeamId,
	TournamentId = @TournamentId,
	GolferId = @GolferId,
	PaymentId = @PaymentId,
	IsValid = @IsValid,
	RegistrationFee = @RegistrationFee
WHERE
ParticipantId = @ParticipantId


if @@ROWCOUNT <> 1
    BEGIN
        RAISERROR  ('spParticipant_Update:  update was expected to update a single row and updated %d rows', 16,1, @@ROWCOUNT)
        RETURN(-1)
    END
GO

