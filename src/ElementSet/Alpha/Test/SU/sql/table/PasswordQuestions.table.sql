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

if not exists (select * from dbo.sysobjects where id = object_id(N'[PasswordQuestions]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE PasswordQuestions (
	QuestionId int NOT NULL,
	Question varchar(255) NOT NULL
)
GO

if not exists(select * from syscolumns where id=object_id('PasswordQuestions') and name = 'QuestionId')
  BEGIN
	select 'QuestionId will not be added becuase it has no default and is required'
	/* -- commented out because column does not have default value and is required
	ALTER TABLE PasswordQuestions ADD
	    QuestionId int NOT NULL
	*/
  END
else
  BEGIN
	exec #spAlterColumn 'PasswordQuestions', 'QuestionId', 'int', 1
  END
GO

if not exists(select * from syscolumns where id=object_id('PasswordQuestions') and name = 'Question')
  BEGIN
	select 'Question will not be added becuase it has no default and is required'
	/* -- commented out because column does not have default value and is required
	ALTER TABLE PasswordQuestions ADD
	    Question varchar(255) NOT NULL
	*/
  END
else
  BEGIN
	exec #spAlterColumn 'PasswordQuestions', 'Question', 'varchar(255)', 1
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_PasswordQuestions') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE PasswordQuestions WITH NOCHECK ADD
	CONSTRAINT PK_PasswordQuestions PRIMARY KEY NONCLUSTERED
	(
		QuestionId
	)
GO


grant all on PasswordQuestions to [public]
GO
