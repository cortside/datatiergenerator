if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spOrganizer_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spOrganizer_Insert]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spOrganizer_Insert
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

INSERT INTO Organizer (
	Name,
	Address1,
	Address2,
	City,
	State,
	Country,
	PostalCode,
	OrganizerContactName,
	OrganizerContactPhone,
	OrganizerContactEmail,
	TechnicalContactName,
	TechnicalContactPhone,
	TechnicalContactEmail)
VALUES (
	@Name,
	@Address1,
	@Address2,
	@City,
	@State,
	@Country,
	@PostalCode,
	@OrganizerContactName,
	@OrganizerContactPhone,
	@OrganizerContactEmail,
	@TechnicalContactName,
	@TechnicalContactPhone,
	@TechnicalContactEmail)


if @@rowcount <> 1 or @@error!=0
    BEGIN
        RAISERROR  20000 'spOrganizer_Insert: Unable to insert new row into Organizer table '
        RETURN(-1)
    END

return @@IDENTITY
GO

