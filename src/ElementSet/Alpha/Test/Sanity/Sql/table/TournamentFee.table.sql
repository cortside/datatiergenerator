SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

if exists (select * from tempdb..sysobjects where name like '#spAlterColumn%' and xtype='P')
drop procedure #spAlterColumn
GO

CREATE PROCEDURE #spAlterColumn
    @table varchar(100),
    @column varchar(100),
    @type varchar(50),
    @required bit
AS
if exists (select * from syscolumns where name=@column and id=object_id(@table))
begin
	declare @nullstring varchar(8)
	set @nullstring = case when @required=0 then 'null' else 'not null' end
	exec('alter table [' + @table + '] alter column [' + @column + '] ' + @type + ' ' + @nullstring)
end
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'[TournamentFee]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE TournamentFee (
	TournamentFeeId int IDENTITY(1,1) NOT NULL,
	TournamentId int NOT NULL,
	[Key] varchar(50) NOT NULL,
	Fee money NOT NULL
)
GO

if not exists(select * from syscolumns where id=object_id('TournamentFee') and name = 'TournamentFeeId')
  BEGIN
	ALTER TABLE TournamentFee ADD
	    TournamentFeeId int IDENTITY(1,1) NOT NULL
  END
else
  BEGIN
	exec #spAlterColumn 'TournamentFee', 'TournamentFeeId', 'int', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('TournamentFee') and name = 'TournamentId')
  BEGIN
	select 'TournamentId will not be added becuase it has no default and is required'
	/* -- commented out because column does not have default value and is required
	ALTER TABLE TournamentFee ADD
	    TournamentId int NOT NULL
	*/
  END
else
  BEGIN
	exec #spAlterColumn 'TournamentFee', 'TournamentId', 'int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('TournamentFee') and name = 'Key')
  BEGIN
	select 'Key will not be added becuase it has no default and is required'
	/* -- commented out because column does not have default value and is required
	ALTER TABLE TournamentFee ADD
	    [Key] varchar(50) NOT NULL
	*/
  END
else
  BEGIN
	exec #spAlterColumn 'TournamentFee', 'Key', 'varchar(50)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('TournamentFee') and name = 'Fee')
  BEGIN
	select 'Fee will not be added becuase it has no default and is required'
	/* -- commented out because column does not have default value and is required
	ALTER TABLE TournamentFee ADD
	    Fee money NOT NULL
	*/
  END
else
  BEGIN
	exec #spAlterColumn 'TournamentFee', 'Fee', 'money', 1
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_TournamentFee') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE TournamentFee WITH NOCHECK ADD
	CONSTRAINT PK_TournamentFee PRIMARY KEY NONCLUSTERED
	(
		TournamentFeeId
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'FK_TournamentFee_Tournament') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE TournamentFee ADD
	CONSTRAINT FK_TournamentFee_Tournament FOREIGN KEY
	(
		TournamentId
	)
	REFERENCES Tournament
	(
		TournamentId
	)
GO

