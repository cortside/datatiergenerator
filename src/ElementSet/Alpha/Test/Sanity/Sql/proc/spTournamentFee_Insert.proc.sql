if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spTournamentFee_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spTournamentFee_Insert]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spTournamentFee_Insert
	@TournamentId	int,
	@Key	varchar(50),
	@Fee	money

AS

INSERT INTO TournamentFee (
	TournamentId,
	[Key],
	Fee)
VALUES (
	@TournamentId,
	@Key,
	@Fee)


if @@rowcount <> 1 or @@error!=0
    BEGIN
        RAISERROR  20000 'spTournamentFee_Insert: Unable to insert new row into TournamentFee table '
        RETURN(-1)
    END

return @@IDENTITY
GO

