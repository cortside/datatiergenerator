if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spTeam_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spTeam_Insert]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spTeam_Insert
	@RegistrationKey	varchar(6),
	@Status	char(1),
	@TournamentId	int

AS

INSERT INTO Team (
	RegistrationKey,
	Status,
	TournamentId)
VALUES (
	@RegistrationKey,
	@Status,
	@TournamentId)


if @@rowcount <> 1 or @@error!=0
    BEGIN
        RAISERROR  20000 'spTeam_Insert: Unable to insert new row into Team table '
        RETURN(-1)
    END

return @@IDENTITY
GO

