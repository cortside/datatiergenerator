if exists (select * from sysobjects where id = object_id(N'[vwTournamentFee]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [vwTournamentFee]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW vwTournamentFee

AS

SELECT
	TournamentFee.TournamentFeeId,
	TournamentFee.TournamentId,
	TournamentFee.[Key],
	TournamentFee.Fee
FROM
	TournamentFee
GO
