if exists (select * from sysobjects where id = object_id(N'[vwGolfer]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [vwGolfer]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW vwGolfer

AS

SELECT
	Golfer.GolferId,
	Golfer.FirstName,
	Golfer.MiddleInitial,
	Golfer.LastName,
	Golfer.Phone,
	Golfer.Email,
	Golfer.Address1,
	Golfer.Address2,
	Golfer.City,
	Golfer.State,
	Golfer.Country,
	Golfer.PostalCode,
	Golfer.DateOfBirth,
	Golfer.Handicap,
	Golfer.CourseNumber,
	Golfer.PlayerNumber,
	Golfer.Gender,
	Golfer.GolferStatus
FROM
	Golfer
GO
