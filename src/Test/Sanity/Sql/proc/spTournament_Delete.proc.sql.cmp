if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spTournament_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spTournament_Delete]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spTournament_Delete

@TournamentId	int

AS

if not exists(SELECT * FROM Tournament WHERE (	TournamentId = @TournamentId
))
    BEGIN
        RAISERROR  ('spTournament_Delete: record not found to delete', 16,1)
        RETURN(-1)
    END

DELETE
FROM
	Tournament
WHERE 
	TournamentId = @TournamentId
GO

