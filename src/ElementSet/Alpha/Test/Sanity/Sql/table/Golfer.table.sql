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

if not exists (select * from dbo.sysobjects where id = object_id(N'[Golfer]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE Golfer (
	GolferId int IDENTITY(1,1) NOT NULL,
	FirstName varchar(50) NULL,
	MiddleInitial char(1) NULL,
	LastName varchar(50) NULL,
	Phone varchar(50) NULL,
	Email varchar(50) NULL,
	Address1 varchar(50) NULL,
	Address2 varchar(50) NULL,
	City varchar(50) NULL,
	State char(2) NULL,
	Country varchar(50) NULL,
	PostalCode varchar(10) NULL,
	DateOfBirth datetime NULL,
	Handicap decimal(3, 1) NULL,
	CourseNumber varchar(10) NULL,
	PlayerNumber varchar(10) NULL,
	Gender varchar(1) NULL,
	GolferStatus char(1) NULL
)
GO

if not exists(select * from syscolumns where id=object_id('Golfer') and name = 'GolferId')
  BEGIN
	ALTER TABLE Golfer ADD
	    GolferId int IDENTITY(1,1) NOT NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Golfer', 'GolferId', 'int', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Golfer') and name = 'FirstName')
  BEGIN
	ALTER TABLE Golfer ADD
	    FirstName varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Golfer', 'FirstName', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Golfer') and name = 'MiddleInitial')
  BEGIN
	ALTER TABLE Golfer ADD
	    MiddleInitial char(1) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Golfer', 'MiddleInitial', 'char(1)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Golfer') and name = 'LastName')
  BEGIN
	ALTER TABLE Golfer ADD
	    LastName varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Golfer', 'LastName', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Golfer') and name = 'Phone')
  BEGIN
	ALTER TABLE Golfer ADD
	    Phone varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Golfer', 'Phone', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Golfer') and name = 'Email')
  BEGIN
	ALTER TABLE Golfer ADD
	    Email varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Golfer', 'Email', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Golfer') and name = 'Address1')
  BEGIN
	ALTER TABLE Golfer ADD
	    Address1 varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Golfer', 'Address1', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Golfer') and name = 'Address2')
  BEGIN
	ALTER TABLE Golfer ADD
	    Address2 varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Golfer', 'Address2', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Golfer') and name = 'City')
  BEGIN
	ALTER TABLE Golfer ADD
	    City varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Golfer', 'City', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Golfer') and name = 'State')
  BEGIN
	ALTER TABLE Golfer ADD
	    State char(2) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Golfer', 'State', 'char(2)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Golfer') and name = 'Country')
  BEGIN
	ALTER TABLE Golfer ADD
	    Country varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Golfer', 'Country', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Golfer') and name = 'PostalCode')
  BEGIN
	ALTER TABLE Golfer ADD
	    PostalCode varchar(10) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Golfer', 'PostalCode', 'varchar(10)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Golfer') and name = 'DateOfBirth')
  BEGIN
	ALTER TABLE Golfer ADD
	    DateOfBirth datetime NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Golfer', 'DateOfBirth', 'datetime', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Golfer') and name = 'Handicap')
  BEGIN
	ALTER TABLE Golfer ADD
	    Handicap decimal(3, 1) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Golfer', 'Handicap', 'decimal(3, 1)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Golfer') and name = 'CourseNumber')
  BEGIN
	ALTER TABLE Golfer ADD
	    CourseNumber varchar(10) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Golfer', 'CourseNumber', 'varchar(10)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Golfer') and name = 'PlayerNumber')
  BEGIN
	ALTER TABLE Golfer ADD
	    PlayerNumber varchar(10) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Golfer', 'PlayerNumber', 'varchar(10)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Golfer') and name = 'Gender')
  BEGIN
	ALTER TABLE Golfer ADD
	    Gender varchar(1) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Golfer', 'Gender', 'varchar(1)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Golfer') and name = 'GolferStatus')
  BEGIN
	ALTER TABLE Golfer ADD
	    GolferStatus char(1) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Golfer', 'GolferStatus', 'char(1)', 0
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_Golfer') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE Golfer WITH NOCHECK ADD
	CONSTRAINT PK_Golfer PRIMARY KEY NONCLUSTERED
	(
		GolferId
	)
GO

