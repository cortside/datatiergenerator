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

if not exists (select * from dbo.sysobjects where id = object_id(N'[UserGroup]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE UserGroup (
	UserGroupId int IDENTITY(1,1) NOT NULL,
	GroupId int NOT NULL,
	UserId int NOT NULL
)
GO

if not exists(select * from syscolumns where id=object_id('UserGroup') and name = 'UserGroupId')
  BEGIN
	ALTER TABLE UserGroup ADD
	    UserGroupId int IDENTITY(1,1) NOT NULL
  END
else
  BEGIN
	exec #spAlterColumn 'UserGroup', 'UserGroupId', 'int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('UserGroup') and name = 'GroupId')
  BEGIN
	select 'GroupId will not be added becuase it has no default and is required'
	/* -- commented out because column does not have default value and is required
	ALTER TABLE UserGroup ADD
	    GroupId int NOT NULL
	*/
  END
else
  BEGIN
	exec #spAlterColumn 'UserGroup', 'GroupId', 'int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('UserGroup') and name = 'UserId')
  BEGIN
	select 'UserId will not be added becuase it has no default and is required'
	/* -- commented out because column does not have default value and is required
	ALTER TABLE UserGroup ADD
	    UserId int NOT NULL
	*/
  END
else
  BEGIN
	exec #spAlterColumn 'UserGroup', 'UserId', 'int', 1
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_UserGroup') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE UserGroup WITH NOCHECK ADD
	CONSTRAINT PK_UserGroup PRIMARY KEY NONCLUSTERED
	(
		UserGroupId
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'FK_UserGroup_User') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE UserGroup ADD
	CONSTRAINT FK_UserGroup_User FOREIGN KEY
	(
		UserId
	)
	REFERENCES [User]
	(
		UserId
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'FK_UserGroup_Group') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE UserGroup ADD
	CONSTRAINT FK_UserGroup_Group FOREIGN KEY
	(
		GroupId
	)
	REFERENCES [Group]
	(
		GroupId
	)
GO


grant all on UserGroup to [public]
GO
