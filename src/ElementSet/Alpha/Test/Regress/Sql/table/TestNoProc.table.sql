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

if not exists (select * from dbo.sysobjects where id = object_id(N'[TestNoProc]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE TestNoProc (
	sqlstringcolumn varchar(50) NOT NULL,
	sqlintcolumn int NOT NULL,
	EmailFormat char(1) NULL,
	addr1 varchar(50) NULL,
	addr2 varchar(50) NULL,
	city varchar(50) NULL,
	state varchar(50) NULL,
	zip varchar(50) NULL
)
GO

if not exists(select * from syscolumns where id=object_id('TestNoProc') and name = 'sqlstringcolumn')
  BEGIN
	select 'sqlstringcolumn will not be added becuase it has no default and is required'
	/* -- commented out because column does not have default value and is required
	ALTER TABLE TestNoProc ADD
	    sqlstringcolumn varchar(50) NOT NULL
	*/
  END
else
  BEGIN
	exec #spAlterColumn 'TestNoProc', 'sqlstringcolumn', 'varchar(50)', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('TestNoProc') and name = 'sqlintcolumn')
  BEGIN
	select 'sqlintcolumn will not be added becuase it has no default and is required'
	/* -- commented out because column does not have default value and is required
	ALTER TABLE TestNoProc ADD
	    sqlintcolumn int NOT NULL
	*/
  END
else
  BEGIN
	exec #spAlterColumn 'TestNoProc', 'sqlintcolumn', 'int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('TestNoProc') and name = 'EmailFormat')
  BEGIN
	ALTER TABLE TestNoProc ADD
	    EmailFormat char(1) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'TestNoProc', 'EmailFormat', 'char(1)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('TestNoProc') and name = 'addr1')
  BEGIN
	ALTER TABLE TestNoProc ADD
	    addr1 varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'TestNoProc', 'addr1', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('TestNoProc') and name = 'addr2')
  BEGIN
	ALTER TABLE TestNoProc ADD
	    addr2 varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'TestNoProc', 'addr2', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('TestNoProc') and name = 'city')
  BEGIN
	ALTER TABLE TestNoProc ADD
	    city varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'TestNoProc', 'city', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('TestNoProc') and name = 'state')
  BEGIN
	ALTER TABLE TestNoProc ADD
	    state varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'TestNoProc', 'state', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('TestNoProc') and name = 'zip')
  BEGIN
	ALTER TABLE TestNoProc ADD
	    zip varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'TestNoProc', 'zip', 'varchar(50)', 0
  END
GO


grant all on TestNoProc to [public]
GO
