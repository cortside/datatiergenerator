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

if not exists (select * from dbo.sysobjects where id = object_id(N'[User]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE [User] (
	UserId int IDENTITY(1,1) NOT NULL,
	UserLogin varchar(100) NOT NULL,
	UserLocale int NOT NULL
)
GO

if not exists(select * from syscolumns where id=object_id('[User]') and name = 'UserId')
  BEGIN
	ALTER TABLE [User] ADD
	    UserId int IDENTITY(1,1) NOT NULL
  END
else
  BEGIN
	exec #spAlterColumn '[User]', 'UserId', 'int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('[User]') and name = 'UserLogin')
  BEGIN
	select 'UserLogin will not be added becuase it has no default and is required'
	/* -- commented out because column does not have default value and is required
	ALTER TABLE [User] ADD
	    UserLogin varchar(100) NOT NULL
	*/
  END
else
  BEGIN
	exec #spAlterColumn '[User]', 'UserLogin', 'varchar(100)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('[User]') and name = 'UserLocale')
  BEGIN
	select 'UserLocale will not be added becuase it has no default and is required'
	/* -- commented out because column does not have default value and is required
	ALTER TABLE [User] ADD
	    UserLocale int NOT NULL
	*/
  END
else
  BEGIN
	exec #spAlterColumn '[User]', 'UserLocale', 'int', 1
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_User') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE [User] WITH NOCHECK ADD
	CONSTRAINT PK_User PRIMARY KEY NONCLUSTERED
	(
		UserId
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'FK_User_Locale') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [User] ADD
	CONSTRAINT FK_User_Locale FOREIGN KEY
	(
		UserLocale
	)
	REFERENCES Locale
	(
		LocaleCode
	)
GO


grant all on [User] to [public]
GO
