if exists (select * from sysobjects where id = object_id(N'[vwTeam]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [vwTeam]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW vwTeam

AS

SELECT
	Team.TeamId,
	Team.RegistrationKey,
	Team.Status,
	Team.TournamentId
FROM
	Team
GO
