if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spTeam_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spTeam_Update]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spTeam_Update

	@TeamId	int,
	@RegistrationKey	varchar(6),
	@Status	char(1),
	@TournamentId	int

AS


UPDATE
	Team
SET
	RegistrationKey = @RegistrationKey,
	Status = @Status,
	TournamentId = @TournamentId
WHERE
TeamId = @TeamId


if @@ROWCOUNT <> 1
    BEGIN
        RAISERROR  ('spTeam_Update:  update was expected to update a single row and updated %d rows', 16,1, @@ROWCOUNT)
        RETURN(-1)
    END
GO

