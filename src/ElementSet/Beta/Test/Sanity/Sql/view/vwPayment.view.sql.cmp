if exists (select * from sysobjects where id = object_id(N'[vwPayment]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [vwPayment]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW vwPayment

AS

SELECT
	Payment.PaymentId,
	Payment.TournamentId,
	Payment.GolferId,
	Payment.AuthorizationNumber,
	Payment.ReferenceNumber,
	Payment.TransactionNumber,
	Payment.Amount,
	Payment.ProcessDate,
	Payment.PaymentStatus,
	Payment.CreditCardNumber,
	Payment.ExpirationDate,
	Payment.CardholderName,
	Payment.Address1,
	Payment.Address2,
	Payment.City,
	Payment.State,
	Payment.Country,
	Payment.PostalCode,
	Payment.ConfirmationCode,
	Payment.PaymentDate
FROM
	Payment
GO
