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

if not exists (select * from dbo.sysobjects where id = object_id(N'[Role]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE Role (
	RoleId int NOT NULL,
	RoleName varchar(100) NOT NULL
)
GO

if not exists(select * from syscolumns where id=object_id('Role') and name = 'RoleId')
  BEGIN
	select 'RoleId will not be added becuase it has no default and is required'
	/* -- commented out because column does not have default value and is required
	ALTER TABLE Role ADD
	    RoleId int NOT NULL
	*/
  END
else
  BEGIN
	exec #spAlterColumn 'Role', 'RoleId', 'int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('Role') and name = 'RoleName')
  BEGIN
	select 'RoleName will not be added becuase it has no default and is required'
	/* -- commented out because column does not have default value and is required
	ALTER TABLE Role ADD
	    RoleName varchar(100) NOT NULL
	*/
  END
else
  BEGIN
	exec #spAlterColumn 'Role', 'RoleName', 'varchar(100)', 1
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_Role') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE Role WITH NOCHECK ADD
	CONSTRAINT PK_Role PRIMARY KEY NONCLUSTERED
	(
		RoleId
	)
GO


grant all on Role to [public]
GO
