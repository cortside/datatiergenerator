if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spParticipant_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spParticipant_Insert]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spParticipant_Insert
	@TeamId	int,
	@TournamentId	int,
	@GolferId	int,
	@PaymentId	int,
	@IsValid	char(1),
	@RegistrationFee	money

AS

INSERT INTO Participant (
	TeamId,
	TournamentId,
	GolferId,
	PaymentId,
	IsValid,
	RegistrationFee)
VALUES (
	@TeamId,
	@TournamentId,
	@GolferId,
	@PaymentId,
	@IsValid,
	@RegistrationFee)


if @@rowcount <> 1 or @@error!=0
    BEGIN
        RAISERROR  20000 'spParticipant_Insert: Unable to insert new row into Participant table '
        RETURN(-1)
    END

return @@IDENTITY
GO

