if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spTournamentFee_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spTournamentFee_Delete]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spTournamentFee_Delete

@TournamentFeeId	int

AS

if not exists(SELECT * FROM TournamentFee WHERE (	TournamentFeeId = @TournamentFeeId
))
    BEGIN
        RAISERROR  ('spTournamentFee_Delete: record not found to delete', 16,1)
        RETURN(-1)
    END

DELETE
FROM
	TournamentFee
WHERE 
	TournamentFeeId = @TournamentFeeId
GO

