if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spTestsqlentity_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spTestsqlentity_Update]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spTestsqlentity_Update

	@sqlstringcolumn	varchar(50),
	@sqlintcolumn	int,
	@EmailFormat	char(1),
	@addr1	varchar(50),
	@addr2	varchar(50),
	@city	varchar(50),
	@state	varchar(50),
	@zip	varchar(50)

AS


UPDATE
	Testsqlentity
SET
	EmailFormat = @EmailFormat,
	addr1 = @addr1,
	addr2 = @addr2,
	city = @city,
	state = @state,
	zip = @zip
WHERE
sqlstringcolumn = @sqlstringcolumn
	 AND sqlintcolumn = @sqlintcolumn


if @@ROWCOUNT <> 1
    BEGIN
        RAISERROR  ('spTestsqlentity_Update:  update was expected to update a single row and updated %d rows', 16,1, @@ROWCOUNT)
        RETURN(-1)
    END
GO

grant execute on [spTestsqlentity_Update] to [public]
GO

