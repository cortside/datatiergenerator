if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spTeam_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spTeam_Delete]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spTeam_Delete

@TeamId	int

AS

if not exists(SELECT * FROM Team WHERE (	TeamId = @TeamId
))
    BEGIN
        RAISERROR  ('spTeam_Delete: record not found to delete', 16,1)
        RETURN(-1)
    END

DELETE
FROM
	Team
WHERE 
	TeamId = @TeamId
GO

