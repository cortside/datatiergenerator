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

if not exists (select * from dbo.sysobjects where id = object_id(N'[Tournament]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE Tournament (
	TournamentId int IDENTITY(1,1) NOT NULL,
	Name varchar(50) NOT NULL,
	Description varchar(500) NULL,
	NumberOfTeams int NULL,
	TeamSize char(1) NULL,
	Format char(1) NULL,
	RegistrationBeginDate datetime NULL,
	RegistrationEndDate datetime NULL,
	RegistrationFee money NULL,
	OrganizerId int NULL,
	CancellationCutoffDate datetime NULL,
	RegistrationFeeDescription varchar(250) NULL,
	DatesText varchar(50) NULL,
	PrizesText varchar(1000) NULL,
	SponsorsText varchar(1000) NULL,
	LocationsText varchar(250) NULL,
	MaximumHandicap int NULL,
	Host varchar(30) NULL,
	ShowPercentFull char(1) NULL,
	ShowParticipants char(1) NULL
)
GO

if not exists(select * from syscolumns where id=object_id('Tournament') and name = 'TournamentId')
  BEGIN
	ALTER TABLE Tournament ADD
	    TournamentId int IDENTITY(1,1) NOT NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Tournament', 'TournamentId', 'int', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Tournament') and name = 'Name')
  BEGIN
	select 'Name will not be added becuase it has no default and is required'
	/* -- commented out because column does not have default value and is required
	ALTER TABLE Tournament ADD
	    Name varchar(50) NOT NULL
	*/
  END
else
  BEGIN
	exec #spAlterColumn 'Tournament', 'Name', 'varchar(50)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('Tournament') and name = 'Description')
  BEGIN
	ALTER TABLE Tournament ADD
	    Description varchar(500) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Tournament', 'Description', 'varchar(500)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Tournament') and name = 'NumberOfTeams')
  BEGIN
	ALTER TABLE Tournament ADD
	    NumberOfTeams int NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Tournament', 'NumberOfTeams', 'int', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Tournament') and name = 'TeamSize')
  BEGIN
	ALTER TABLE Tournament ADD
	    TeamSize char(1) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Tournament', 'TeamSize', 'char(1)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Tournament') and name = 'Format')
  BEGIN
	ALTER TABLE Tournament ADD
	    Format char(1) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Tournament', 'Format', 'char(1)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Tournament') and name = 'RegistrationBeginDate')
  BEGIN
	ALTER TABLE Tournament ADD
	    RegistrationBeginDate datetime NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Tournament', 'RegistrationBeginDate', 'datetime', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Tournament') and name = 'RegistrationEndDate')
  BEGIN
	ALTER TABLE Tournament ADD
	    RegistrationEndDate datetime NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Tournament', 'RegistrationEndDate', 'datetime', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Tournament') and name = 'RegistrationFee')
  BEGIN
	ALTER TABLE Tournament ADD
	    RegistrationFee money NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Tournament', 'RegistrationFee', 'money', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Tournament') and name = 'OrganizerId')
  BEGIN
	ALTER TABLE Tournament ADD
	    OrganizerId int NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Tournament', 'OrganizerId', 'int', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Tournament') and name = 'CancellationCutoffDate')
  BEGIN
	ALTER TABLE Tournament ADD
	    CancellationCutoffDate datetime NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Tournament', 'CancellationCutoffDate', 'datetime', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Tournament') and name = 'RegistrationFeeDescription')
  BEGIN
	ALTER TABLE Tournament ADD
	    RegistrationFeeDescription varchar(250) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Tournament', 'RegistrationFeeDescription', 'varchar(250)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Tournament') and name = 'DatesText')
  BEGIN
	ALTER TABLE Tournament ADD
	    DatesText varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Tournament', 'DatesText', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Tournament') and name = 'PrizesText')
  BEGIN
	ALTER TABLE Tournament ADD
	    PrizesText varchar(1000) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Tournament', 'PrizesText', 'varchar(1000)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Tournament') and name = 'SponsorsText')
  BEGIN
	ALTER TABLE Tournament ADD
	    SponsorsText varchar(1000) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Tournament', 'SponsorsText', 'varchar(1000)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Tournament') and name = 'LocationsText')
  BEGIN
	ALTER TABLE Tournament ADD
	    LocationsText varchar(250) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Tournament', 'LocationsText', 'varchar(250)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Tournament') and name = 'MaximumHandicap')
  BEGIN
	ALTER TABLE Tournament ADD
	    MaximumHandicap int NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Tournament', 'MaximumHandicap', 'int', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Tournament') and name = 'Host')
  BEGIN
	ALTER TABLE Tournament ADD
	    Host varchar(30) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Tournament', 'Host', 'varchar(30)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Tournament') and name = 'ShowPercentFull')
  BEGIN
	ALTER TABLE Tournament ADD
	    ShowPercentFull char(1) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Tournament', 'ShowPercentFull', 'char(1)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Tournament') and name = 'ShowParticipants')
  BEGIN
	ALTER TABLE Tournament ADD
	    ShowParticipants char(1) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Tournament', 'ShowParticipants', 'char(1)', 0
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_Tournament') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE Tournament WITH NOCHECK ADD
	CONSTRAINT PK_Tournament PRIMARY KEY NONCLUSTERED
	(
		TournamentId
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'FK_Tournament_Organizer') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE Tournament ADD
	CONSTRAINT FK_Tournament_Organizer FOREIGN KEY
	(
		OrganizerId
	)
	REFERENCES Organizer
	(
		OrganizerId
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'UN_Tournament_Host') and OBJECTPROPERTY(id, N'IsUniqueCnst') = 1)
ALTER TABLE Tournament ADD
	CONSTRAINT UN_Tournament_Host UNIQUE
	(
		Host
	)
GO

