if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spTestSqlEntities_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spTestSqlEntities_Update]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spTestSqlEntities_Update

	@TestSqlEntitiesId	int,
	@float	float(0),
	@datetime	datetime,
	@bit	bit,
	@smallint	smallint,
	@tinyint	tinyint,
	@smallmoney	smallmoney,
	@money	money,
	@text	text,
	@smalldatetime	smalldatetime,
	@char	char(10),
	@varchar	varchar(10),
	@int	int,
	@numeric	numeric(10, 3),
	@bigint	bigint,
	@decimal	decimal(10, 3),
	@nchar	nchar(10),
	@ntext	ntext,
	@nvarchar	nvarchar(10),
	@real	real,
	@uniqueidentifier	uniqueidentifier

AS


UPDATE
	TestSqlEntities
SET
	float = @float,
	datetime = @datetime,
	bit = @bit,
	smallint = @smallint,
	tinyint = @tinyint,
	smallmoney = @smallmoney,
	money = @money,
	text = @text,
	smalldatetime = @smalldatetime,
	char = @char,
	varchar = @varchar,
	int = @int,
	numeric = @numeric,
	bigint = @bigint,
	decimal = @decimal,
	nchar = @nchar,
	ntext = @ntext,
	nvarchar = @nvarchar,
	real = @real,
	uniqueidentifier = @uniqueidentifier
WHERE
TestSqlEntitiesId = @TestSqlEntitiesId


if @@ROWCOUNT <> 1
    BEGIN
        RAISERROR  ('spTestSqlEntities_Update:  update was expected to update a single row and updated %d rows', 16,1, @@ROWCOUNT)
        RETURN(-1)
    END
GO

