if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spParticipant_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spParticipant_Delete]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spParticipant_Delete

@ParticipantId	int

AS

if not exists(SELECT * FROM Participant WHERE (	ParticipantId = @ParticipantId
))
    BEGIN
        RAISERROR  ('spParticipant_Delete: record not found to delete', 16,1)
        RETURN(-1)
    END

DELETE
FROM
	Participant
WHERE 
	ParticipantId = @ParticipantId
GO

