if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spGolfer_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spGolfer_Update]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spGolfer_Update

	@GolferId	int,
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


UPDATE
	Golfer
SET
	FirstName = @FirstName,
	MiddleInitial = @MiddleInitial,
	LastName = @LastName,
	Phone = @Phone,
	Email = @Email,
	Address1 = @Address1,
	Address2 = @Address2,
	City = @City,
	State = @State,
	Country = @Country,
	PostalCode = @PostalCode,
	DateOfBirth = @DateOfBirth,
	Handicap = @Handicap,
	CourseNumber = @CourseNumber,
	PlayerNumber = @PlayerNumber,
	Gender = @Gender,
	GolferStatus = @GolferStatus
WHERE
GolferId = @GolferId


if @@ROWCOUNT <> 1
    BEGIN
        RAISERROR  ('spGolfer_Update:  update was expected to update a single row and updated %d rows', 16,1, @@ROWCOUNT)
        RETURN(-1)
    END
GO

