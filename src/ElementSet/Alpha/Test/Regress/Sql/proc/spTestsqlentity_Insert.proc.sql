if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spTestsqlentity_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spTestsqlentity_Insert]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spTestsqlentity_Insert
	@sqlstringcolumn	varchar(50),
	@sqlintcolumn	int,
	@EmailFormat	char(1),
	@addr1	varchar(50),
	@addr2	varchar(50),
	@city	varchar(50),
	@state	varchar(50),
	@zip	varchar(50)

AS

INSERT INTO Testsqlentity (
	sqlstringcolumn,
	sqlintcolumn,
	EmailFormat,
	addr1,
	addr2,
	city,
	state,
	zip)
VALUES (
	@sqlstringcolumn,
	@sqlintcolumn,
	@EmailFormat,
	@addr1,
	@addr2,
	@city,
	@state,
	@zip)


if @@rowcount <> 1 or @@error!=0
    BEGIN
        RAISERROR  20000 'spTestsqlentity_Insert: Unable to insert new row into Testsqlentity table '
        RETURN(-1)
    END

return @@IDENTITY
GO

grant execute on [spTestsqlentity_Insert] to [public]
GO

