if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spTestsqlentity2_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spTestsqlentity2_Delete]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spTestsqlentity2_Delete



AS

if not exists(SELECT * FROM Testsqlentity2 WHERE ())
    BEGIN
        RAISERROR  ('spTestsqlentity2_Delete: record not found to delete', 16,1)
        RETURN(-1)
    END

DELETE
FROM
	Testsqlentity2
WHERE 
GO

grant execute on [spTestsqlentity2_Delete] to [public]
GO

