if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spTestSqlEntities_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spTestSqlEntities_Delete]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spTestSqlEntities_Delete

@TestSqlEntitiesId	int

AS

if not exists(SELECT * FROM TestSqlEntities WHERE (	TestSqlEntitiesId = @TestSqlEntitiesId
))
    BEGIN
        RAISERROR  ('spTestSqlEntities_Delete: record not found to delete', 16,1)
        RETURN(-1)
    END

DELETE
FROM
	TestSqlEntities
WHERE 
	TestSqlEntitiesId = @TestSqlEntitiesId
GO

