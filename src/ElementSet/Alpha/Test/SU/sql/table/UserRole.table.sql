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

if not exists (select * from dbo.sysobjects where id = object_id(N'[UserRole]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE UserRole (
	UserRoleId int IDENTITY(1,1) NOT NULL,
	UserId int NOT NULL,
	RoleId int NOT NULL,
	PermitDeny int NOT NULL
)
GO

if not exists(select * from syscolumns where id=object_id('UserRole') and name = 'UserRoleId')
  BEGIN
	ALTER TABLE UserRole ADD
	    UserRoleId int IDENTITY(1,1) NOT NULL
  END
else
  BEGIN
	exec #spAlterColumn 'UserRole', 'UserRoleId', 'int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('UserRole') and name = 'UserId')
  BEGIN
	select 'UserId will not be added becuase it has no default and is required'
	/* -- commented out because column does not have default value and is required
	ALTER TABLE UserRole ADD
	    UserId int NOT NULL
	*/
  END
else
  BEGIN
	exec #spAlterColumn 'UserRole', 'UserId', 'int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('UserRole') and name = 'RoleId')
  BEGIN
	select 'RoleId will not be added becuase it has no default and is required'
	/* -- commented out because column does not have default value and is required
	ALTER TABLE UserRole ADD
	    RoleId int NOT NULL
	*/
  END
else
  BEGIN
	exec #spAlterColumn 'UserRole', 'RoleId', 'int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('UserRole') and name = 'PermitDeny')
  BEGIN
	select 'PermitDeny will not be added becuase it has no default and is required'
	/* -- commented out because column does not have default value and is required
	ALTER TABLE UserRole ADD
	    PermitDeny int NOT NULL
	*/
  END
else
  BEGIN
	exec #spAlterColumn 'UserRole', 'PermitDeny', 'int', 1
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_UserRole') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE UserRole WITH NOCHECK ADD
	CONSTRAINT PK_UserRole PRIMARY KEY NONCLUSTERED
	(
		UserRoleId
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'FK_UserRole_Role') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE UserRole ADD
	CONSTRAINT FK_UserRole_Role FOREIGN KEY
	(
		RoleId
	)
	REFERENCES Role
	(
		RoleId
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'FK_UserRole_User') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE UserRole ADD
	CONSTRAINT FK_UserRole_User FOREIGN KEY
	(
		UserId
	)
	REFERENCES [User]
	(
		UserId
	)
GO


grant all on UserRole to [public]
GO
