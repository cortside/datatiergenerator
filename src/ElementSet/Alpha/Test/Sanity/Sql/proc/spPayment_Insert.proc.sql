if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spPayment_Insert]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spPayment_Insert]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE spPayment_Insert
	@TournamentId	int,
	@GolferId	int,
	@AuthorizationNumber	varchar(50),
	@ReferenceNumber	varchar(50),
	@TransactionNumber	varchar(50),
	@Amount	money,
	@ProcessDate	datetime,
	@PaymentStatus	varchar(10),
	@CreditCardNumber	varchar(25),
	@ExpirationDate	varchar(4),
	@CardholderName	varchar(50),
	@Address1	varchar(50),
	@Address2	varchar(50),
	@City	varchar(50),
	@State	char(2),
	@Country	varchar(50),
	@PostalCode	varchar(10),
	@ConfirmationCode	varchar(50),
	@PaymentDate	datetime

AS

INSERT INTO Payment (
	TournamentId,
	GolferId,
	AuthorizationNumber,
	ReferenceNumber,
	TransactionNumber,
	Amount,
	ProcessDate,
	PaymentStatus,
	CreditCardNumber,
	ExpirationDate,
	CardholderName,
	Address1,
	Address2,
	City,
	State,
	Country,
	PostalCode,
	ConfirmationCode,
	PaymentDate)
VALUES (
	@TournamentId,
	@GolferId,
	@AuthorizationNumber,
	@ReferenceNumber,
	@TransactionNumber,
	@Amount,
	@ProcessDate,
	@PaymentStatus,
	@CreditCardNumber,
	@ExpirationDate,
	@CardholderName,
	@Address1,
	@Address2,
	@City,
	@State,
	@Country,
	@PostalCode,
	@ConfirmationCode,
	@PaymentDate)


if @@rowcount <> 1 or @@error!=0
    BEGIN
        RAISERROR  20000 'spPayment_Insert: Unable to insert new row into Payment table '
        RETURN(-1)
    END

return @@IDENTITY
GO

