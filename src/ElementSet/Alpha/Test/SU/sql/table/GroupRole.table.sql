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

if not exists (select * from dbo.sysobjects where id = object_id(N'[GroupRole]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE GroupRole (
	GroupRoleId int IDENTITY(1,1) NOT NULL,
	GroupId int NOT NULL,
	RoleId int NOT NULL,
	PermitDeny int NOT NULL
)
GO

if not exists(select * from syscolumns where id=object_id('GroupRole') and name = 'GroupRoleId')
  BEGIN
	ALTER TABLE GroupRole ADD
	    GroupRoleId int IDENTITY(1,1) NOT NULL
  END
else
  BEGIN
	exec #spAlterColumn 'GroupRole', 'GroupRoleId', 'int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('GroupRole') and name = 'GroupId')
  BEGIN
	select 'GroupId will not be added becuase it has no default and is required'
	/* -- commented out because column does not have default value and is required
	ALTER TABLE GroupRole ADD
	    GroupId int NOT NULL
	*/
  END
else
  BEGIN
	exec #spAlterColumn 'GroupRole', 'GroupId', 'int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('GroupRole') and name = 'RoleId')
  BEGIN
	select 'RoleId will not be added becuase it has no default and is required'
	/* -- commented out because column does not have default value and is required
	ALTER TABLE GroupRole ADD
	    RoleId int NOT NULL
	*/
  END
else
  BEGIN
	exec #spAlterColumn 'GroupRole', 'RoleId', 'int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('GroupRole') and name = 'PermitDeny')
  BEGIN
	select 'PermitDeny will not be added becuase it has no default and is required'
	/* -- commented out because column does not have default value and is required
	ALTER TABLE GroupRole ADD
	    PermitDeny int NOT NULL
	*/
  END
else
  BEGIN
	exec #spAlterColumn 'GroupRole', 'PermitDeny', 'int', 1
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_GroupRole') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE GroupRole WITH NOCHECK ADD
	CONSTRAINT PK_GroupRole PRIMARY KEY NONCLUSTERED
	(
		GroupRoleId
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'FK_GroupRole_Role') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE GroupRole ADD
	CONSTRAINT FK_GroupRole_Role FOREIGN KEY
	(
		RoleId
	)
	REFERENCES Role
	(
		RoleId
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'FK_GroupRole_Group') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE GroupRole ADD
	CONSTRAINT FK_GroupRole_Group FOREIGN KEY
	(
		GroupId
	)
	REFERENCES [Group]
	(
		GroupId
	)
GO


grant all on GroupRole to [public]
GO
