if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spPayment_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spPayment_Delete]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spPayment_Delete

@PaymentId	int

AS

if not exists(SELECT * FROM Payment WHERE (	PaymentId = @PaymentId
))
    BEGIN
        RAISERROR  ('spPayment_Delete: record not found to delete', 16,1)
        RETURN(-1)
    END

DELETE
FROM
	Payment
WHERE 
	PaymentId = @PaymentId
GO

