if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spGolfer_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spGolfer_Insert]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spGolfer_Insert
	@FirstName	varchar(50),
	@MiddleInitial	char(1),
	@LastName	varchar(50),
	@Phone	varchar(50),
	@Email	varchar(50),
	@Address1	varchar(50),
	@Address2	varchar(50),
	@City	varchar(50),
	@State	char(2),
	@Country	varchar(50),
	@PostalCode	varchar(10),
	@DateOfBirth	datetime,
	@Handicap	decimal(3, 1),
	@CourseNumber	varchar(10),
	@PlayerNumber	varchar(10),
	@Gender	varchar(1),
	@GolferStatus	char(1)

AS

INSERT INTO Golfer (
	FirstName,
	MiddleInitial,
	LastName,
	Phone,
	Email,
	Address1,
	Address2,
	City,
	State,
	Country,
	PostalCode,
	DateOfBirth,
	Handicap,
	CourseNumber,
	PlayerNumber,
	Gender,
	GolferStatus)
VALUES (
	@FirstName,
	@MiddleInitial,
	@LastName,
	@Phone,
	@Email,
	@Address1,
	@Address2,
	@City,
	@State,
	@Country,
	@PostalCode,
	@DateOfBirth,
	@Handicap,
	@CourseNumber,
	@PlayerNumber,
	@Gender,
	@GolferStatus)


if @@rowcount <> 1 or @@error!=0
    BEGIN
        RAISERROR  20000 'spGolfer_Insert: Unable to insert new row into Golfer table '
        RETURN(-1)
    END

return @@IDENTITY
GO

