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

if not exists (select * from dbo.sysobjects where id = object_id(N'[Participant]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE Participant (
	ParticipantId int IDENTITY(1,1) NOT NULL,
	TeamId int NULL,
	TournamentId int NULL,
	GolferId int NULL,
	PaymentId int NULL,
	IsValid char(1) NULL,
	RegistrationFee money NULL
)
GO

if not exists(select * from syscolumns where id=object_id('Participant') and name = 'ParticipantId')
  BEGIN
	ALTER TABLE Participant ADD
	    ParticipantId int IDENTITY(1,1) NOT NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Participant', 'ParticipantId', 'int', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Participant') and name = 'TeamId')
  BEGIN
	ALTER TABLE Participant ADD
	    TeamId int NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Participant', 'TeamId', 'int', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Participant') and name = 'TournamentId')
  BEGIN
	ALTER TABLE Participant ADD
	    TournamentId int NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Participant', 'TournamentId', 'int', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Participant') and name = 'GolferId')
  BEGIN
	ALTER TABLE Participant ADD
	    GolferId int NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Participant', 'GolferId', 'int', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Participant') and name = 'PaymentId')
  BEGIN
	ALTER TABLE Participant ADD
	    PaymentId int NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Participant', 'PaymentId', 'int', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Participant') and name = 'IsValid')
  BEGIN
	ALTER TABLE Participant ADD
	    IsValid char(1) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Participant', 'IsValid', 'char(1)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Participant') and name = 'RegistrationFee')
  BEGIN
	ALTER TABLE Participant ADD
	    RegistrationFee money NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Participant', 'RegistrationFee', 'money', 0
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_Participant') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE Participant WITH NOCHECK ADD
	CONSTRAINT PK_Participant PRIMARY KEY NONCLUSTERED
	(
		ParticipantId
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'FK_Participant_Tournament') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE Participant ADD
	CONSTRAINT FK_Participant_Tournament FOREIGN KEY
	(
		TournamentId
	)
	REFERENCES Tournament
	(
		TournamentId
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'FK_Participant_Team') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE Participant ADD
	CONSTRAINT FK_Participant_Team FOREIGN KEY
	(
		TeamId
	)
	REFERENCES Team
	(
		TeamId
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'FK_Participant_Golfer') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE Participant ADD
	CONSTRAINT FK_Participant_Golfer FOREIGN KEY
	(
		GolferId
	)
	REFERENCES Golfer
	(
		GolferId
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'FK_Participant_Payment') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE Participant ADD
	CONSTRAINT FK_Participant_Payment FOREIGN KEY
	(
		PaymentId
	)
	REFERENCES Payment
	(
		PaymentId
	)
GO

