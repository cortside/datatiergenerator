if exists (select * from sysobjects where id = object_id(N'[vwTestSqlEntities]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [vwTestSqlEntities]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW vwTestSqlEntities

AS

SELECT
	TestSqlEntities.TestSqlEntitiesId,
	TestSqlEntities.float,
	TestSqlEntities.datetime,
	TestSqlEntities.bit,
	TestSqlEntities.smallint,
	TestSqlEntities.tinyint,
	TestSqlEntities.smallmoney,
	TestSqlEntities.money,
	TestSqlEntities.text,
	TestSqlEntities.smalldatetime,
	TestSqlEntities.char,
	TestSqlEntities.varchar,
	TestSqlEntities.int,
	TestSqlEntities.numeric,
	TestSqlEntities.bigint,
	TestSqlEntities.decimal,
	TestSqlEntities.nchar,
	TestSqlEntities.ntext,
	TestSqlEntities.nvarchar,
	TestSqlEntities.real,
	TestSqlEntities.uniqueidentifier
FROM
	TestSqlEntities
GO
