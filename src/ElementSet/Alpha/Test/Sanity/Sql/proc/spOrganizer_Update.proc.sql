if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spOrganizer_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spOrganizer_Update]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spOrganizer_Update

	@OrganizerId	int,
	@Name	varchar(50),
	@Address1	varchar(50),
	@Address2	varchar(50),
	@City	varchar(50),
	@State	char(2),
	@Country	varchar(50),
	@PostalCode	varchar(10),
	@OrganizerContactName	varchar(50),
	@OrganizerContactPhone	varchar(50),
	@OrganizerContactEmail	varchar(100),
	@TechnicalContactName	varchar(50),
	@TechnicalContactPhone	varchar(50),
	@TechnicalContactEmail	varchar(100)

AS


UPDATE
	Organizer
SET
	Name = @Name,
	Address1 = @Address1,
	Address2 = @Address2,
	City = @City,
	State = @State,
	Country = @Country,
	PostalCode = @PostalCode,
	OrganizerContactName = @OrganizerContactName,
	OrganizerContactPhone = @OrganizerContactPhone,
	OrganizerContactEmail = @OrganizerContactEmail,
	TechnicalContactName = @TechnicalContactName,
	TechnicalContactPhone = @TechnicalContactPhone,
	TechnicalContactEmail = @TechnicalContactEmail
WHERE
OrganizerId = @OrganizerId


if @@ROWCOUNT <> 1
    BEGIN
        RAISERROR  ('spOrganizer_Update:  update was expected to update a single row and updated %d rows', 16,1, @@ROWCOUNT)
        RETURN(-1)
    END
GO

