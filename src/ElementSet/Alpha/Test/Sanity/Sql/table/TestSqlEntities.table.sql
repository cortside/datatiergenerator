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

if not exists (select * from dbo.sysobjects where id = object_id(N'[TestSqlEntities]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE TestSqlEntities (
	TestSqlEntitiesId int IDENTITY(1,1) NOT NULL,
	float float(0) NULL,
	datetime datetime NULL,
	bit bit NULL,
	smallint smallint NULL,
	tinyint tinyint NULL,
	smallmoney smallmoney NULL,
	money money NULL,
	text text NULL,
	smalldatetime smalldatetime NULL,
	char char(10) NULL,
	varchar varchar(10) NULL,
	int int NULL,
	numeric numeric(10, 3) NULL,
	bigint bigint NULL,
	decimal decimal(10, 3) NULL,
	nchar nchar(10) NULL,
	ntext ntext NULL,
	nvarchar nvarchar(10) NULL,
	real real NULL,
	uniqueidentifier uniqueidentifier NULL
)
GO

if not exists(select * from syscolumns where id=object_id('TestSqlEntities') and name = 'TestSqlEntitiesId')
  BEGIN
	ALTER TABLE TestSqlEntities ADD
	    TestSqlEntitiesId int IDENTITY(1,1) NOT NULL
  END
else
  BEGIN
	exec #spAlterColumn 'TestSqlEntities', 'TestSqlEntitiesId', 'int', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('TestSqlEntities') and name = 'float')
  BEGIN
	ALTER TABLE TestSqlEntities ADD
	    float float(0) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'TestSqlEntities', 'float', 'float(0)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('TestSqlEntities') and name = 'datetime')
  BEGIN
	ALTER TABLE TestSqlEntities ADD
	    datetime datetime NULL
  END
else
  BEGIN
	exec #spAlterColumn 'TestSqlEntities', 'datetime', 'datetime', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('TestSqlEntities') and name = 'bit')
  BEGIN
	ALTER TABLE TestSqlEntities ADD
	    bit bit NULL
  END
else
  BEGIN
	exec #spAlterColumn 'TestSqlEntities', 'bit', 'bit', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('TestSqlEntities') and name = 'smallint')
  BEGIN
	ALTER TABLE TestSqlEntities ADD
	    smallint smallint NULL
  END
else
  BEGIN
	exec #spAlterColumn 'TestSqlEntities', 'smallint', 'smallint', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('TestSqlEntities') and name = 'tinyint')
  BEGIN
	ALTER TABLE TestSqlEntities ADD
	    tinyint tinyint NULL
  END
else
  BEGIN
	exec #spAlterColumn 'TestSqlEntities', 'tinyint', 'tinyint', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('TestSqlEntities') and name = 'smallmoney')
  BEGIN
	ALTER TABLE TestSqlEntities ADD
	    smallmoney smallmoney NULL
  END
else
  BEGIN
	exec #spAlterColumn 'TestSqlEntities', 'smallmoney', 'smallmoney', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('TestSqlEntities') and name = 'money')
  BEGIN
	ALTER TABLE TestSqlEntities ADD
	    money money NULL
  END
else
  BEGIN
	exec #spAlterColumn 'TestSqlEntities', 'money', 'money', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('TestSqlEntities') and name = 'text')
  BEGIN
	ALTER TABLE TestSqlEntities ADD
	    text text NULL
  END
GO

if not exists(select * from syscolumns where id=object_id('TestSqlEntities') and name = 'smalldatetime')
  BEGIN
	ALTER TABLE TestSqlEntities ADD
	    smalldatetime smalldatetime NULL
  END
else
  BEGIN
	exec #spAlterColumn 'TestSqlEntities', 'smalldatetime', 'smalldatetime', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('TestSqlEntities') and name = 'char')
  BEGIN
	ALTER TABLE TestSqlEntities ADD
	    char char(10) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'TestSqlEntities', 'char', 'char(10)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('TestSqlEntities') and name = 'varchar')
  BEGIN
	ALTER TABLE TestSqlEntities ADD
	    varchar varchar(10) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'TestSqlEntities', 'varchar', 'varchar(10)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('TestSqlEntities') and name = 'int')
  BEGIN
	ALTER TABLE TestSqlEntities ADD
	    int int NULL
  END
else
  BEGIN
	exec #spAlterColumn 'TestSqlEntities', 'int', 'int', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('TestSqlEntities') and name = 'numeric')
  BEGIN
	ALTER TABLE TestSqlEntities ADD
	    numeric numeric(10, 3) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'TestSqlEntities', 'numeric', 'numeric(10, 3)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('TestSqlEntities') and name = 'bigint')
  BEGIN
	ALTER TABLE TestSqlEntities ADD
	    bigint bigint NULL
  END
else
  BEGIN
	exec #spAlterColumn 'TestSqlEntities', 'bigint', 'bigint', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('TestSqlEntities') and name = 'decimal')
  BEGIN
	ALTER TABLE TestSqlEntities ADD
	    decimal decimal(10, 3) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'TestSqlEntities', 'decimal', 'decimal(10, 3)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('TestSqlEntities') and name = 'nchar')
  BEGIN
	ALTER TABLE TestSqlEntities ADD
	    nchar nchar(10) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'TestSqlEntities', 'nchar', 'nchar(10)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('TestSqlEntities') and name = 'ntext')
  BEGIN
	ALTER TABLE TestSqlEntities ADD
	    ntext ntext NULL
  END
else
  BEGIN
	exec #spAlterColumn 'TestSqlEntities', 'ntext', 'ntext', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('TestSqlEntities') and name = 'nvarchar')
  BEGIN
	ALTER TABLE TestSqlEntities ADD
	    nvarchar nvarchar(10) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'TestSqlEntities', 'nvarchar', 'nvarchar(10)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('TestSqlEntities') and name = 'real')
  BEGIN
	ALTER TABLE TestSqlEntities ADD
	    real real NULL
  END
else
  BEGIN
	exec #spAlterColumn 'TestSqlEntities', 'real', 'real', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('TestSqlEntities') and name = 'uniqueidentifier')
  BEGIN
	ALTER TABLE TestSqlEntities ADD
	    uniqueidentifier uniqueidentifier NULL
  END
else
  BEGIN
	exec #spAlterColumn 'TestSqlEntities', 'uniqueidentifier', 'uniqueidentifier', 0
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_TestSqlEntities') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE TestSqlEntities WITH NOCHECK ADD
	CONSTRAINT PK_TestSqlEntities PRIMARY KEY NONCLUSTERED
	(
		TestSqlEntitiesId
	)
GO

