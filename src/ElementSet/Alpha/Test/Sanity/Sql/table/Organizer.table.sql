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

if not exists (select * from dbo.sysobjects where id = object_id(N'[Organizer]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE Organizer (
	OrganizerId int IDENTITY(1,1) NOT NULL,
	Name varchar(50) NOT NULL,
	Address1 varchar(50) NULL,
	Address2 varchar(50) NULL,
	City varchar(50) NULL,
	State char(2) NULL,
	Country varchar(50) NULL,
	PostalCode varchar(10) NULL,
	OrganizerContactName varchar(50) NULL,
	OrganizerContactPhone varchar(50) NULL,
	OrganizerContactEmail varchar(100) NULL,
	TechnicalContactName varchar(50) NULL,
	TechnicalContactPhone varchar(50) NULL,
	TechnicalContactEmail varchar(100) NULL
)
GO

if not exists(select * from syscolumns where id=object_id('Organizer') and name = 'OrganizerId')
  BEGIN
	ALTER TABLE Organizer ADD
	    OrganizerId int IDENTITY(1,1) NOT NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Organizer', 'OrganizerId', 'int', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Organizer') and name = 'Name')
  BEGIN
	select 'Name will not be added becuase it has no default and is required'
	/* -- commented out because column does not have default value and is required
	ALTER TABLE Organizer ADD
	    Name varchar(50) NOT NULL
	*/
  END
else
  BEGIN
	exec #spAlterColumn 'Organizer', 'Name', 'varchar(50)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('Organizer') and name = 'Address1')
  BEGIN
	ALTER TABLE Organizer ADD
	    Address1 varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Organizer', 'Address1', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Organizer') and name = 'Address2')
  BEGIN
	ALTER TABLE Organizer ADD
	    Address2 varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Organizer', 'Address2', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Organizer') and name = 'City')
  BEGIN
	ALTER TABLE Organizer ADD
	    City varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Organizer', 'City', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Organizer') and name = 'State')
  BEGIN
	ALTER TABLE Organizer ADD
	    State char(2) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Organizer', 'State', 'char(2)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Organizer') and name = 'Country')
  BEGIN
	ALTER TABLE Organizer ADD
	    Country varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Organizer', 'Country', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Organizer') and name = 'PostalCode')
  BEGIN
	ALTER TABLE Organizer ADD
	    PostalCode varchar(10) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Organizer', 'PostalCode', 'varchar(10)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Organizer') and name = 'OrganizerContactName')
  BEGIN
	ALTER TABLE Organizer ADD
	    OrganizerContactName varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Organizer', 'OrganizerContactName', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Organizer') and name = 'OrganizerContactPhone')
  BEGIN
	ALTER TABLE Organizer ADD
	    OrganizerContactPhone varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Organizer', 'OrganizerContactPhone', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Organizer') and name = 'OrganizerContactEmail')
  BEGIN
	ALTER TABLE Organizer ADD
	    OrganizerContactEmail varchar(100) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Organizer', 'OrganizerContactEmail', 'varchar(100)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Organizer') and name = 'TechnicalContactName')
  BEGIN
	ALTER TABLE Organizer ADD
	    TechnicalContactName varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Organizer', 'TechnicalContactName', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Organizer') and name = 'TechnicalContactPhone')
  BEGIN
	ALTER TABLE Organizer ADD
	    TechnicalContactPhone varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Organizer', 'TechnicalContactPhone', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Organizer') and name = 'TechnicalContactEmail')
  BEGIN
	ALTER TABLE Organizer ADD
	    TechnicalContactEmail varchar(100) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Organizer', 'TechnicalContactEmail', 'varchar(100)', 0
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_Organizer') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE Organizer WITH NOCHECK ADD
	CONSTRAINT PK_Organizer PRIMARY KEY NONCLUSTERED
	(
		OrganizerId
	)
GO

