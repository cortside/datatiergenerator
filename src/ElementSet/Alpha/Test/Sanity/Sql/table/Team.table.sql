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

if not exists (select * from dbo.sysobjects where id = object_id(N'[Team]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE Team (
	TeamId int IDENTITY(1,1) NOT NULL,
	RegistrationKey varchar(6) NULL,
	Status char(1) NULL,
	TournamentId int NULL
)
GO

if not exists(select * from syscolumns where id=object_id('Team') and name = 'TeamId')
  BEGIN
	ALTER TABLE Team ADD
	    TeamId int IDENTITY(1,1) NOT NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Team', 'TeamId', 'int', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Team') and name = 'RegistrationKey')
  BEGIN
	ALTER TABLE Team ADD
	    RegistrationKey varchar(6) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Team', 'RegistrationKey', 'varchar(6)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Team') and name = 'Status')
  BEGIN
	ALTER TABLE Team ADD
	    Status char(1) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Team', 'Status', 'char(1)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Team') and name = 'TournamentId')
  BEGIN
	ALTER TABLE Team ADD
	    TournamentId int NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Team', 'TournamentId', 'int', 0
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_Team') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE Team WITH NOCHECK ADD
	CONSTRAINT PK_Team PRIMARY KEY NONCLUSTERED
	(
		TeamId
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'FK_Team_Tournament') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE Team ADD
	CONSTRAINT FK_Team_Tournament FOREIGN KEY
	(
		TournamentId
	)
	REFERENCES Tournament
	(
		TournamentId
	)
GO

