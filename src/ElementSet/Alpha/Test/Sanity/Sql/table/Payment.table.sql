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

if not exists (select * from dbo.sysobjects where id = object_id(N'[Payment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
CREATE TABLE Payment (
	PaymentId int IDENTITY(1,1) NOT NULL,
	TournamentId int NULL,
	GolferId int NULL,
	AuthorizationNumber varchar(50) NULL,
	ReferenceNumber varchar(50) NULL,
	TransactionNumber varchar(50) NULL,
	Amount money NULL,
	ProcessDate datetime NULL,
	PaymentStatus varchar(10) NULL,
	CreditCardNumber varchar(25) NULL,
	ExpirationDate varchar(4) NULL,
	CardholderName varchar(50) NULL,
	Address1 varchar(50) NULL,
	Address2 varchar(50) NULL,
	City varchar(50) NULL,
	State char(2) NULL,
	Country varchar(50) NULL,
	PostalCode varchar(10) NULL,
	ConfirmationCode varchar(50) NULL,
	PaymentDate datetime NULL
)
GO

if not exists(select * from syscolumns where id=object_id('Payment') and name = 'PaymentId')
  BEGIN
	ALTER TABLE Payment ADD
	    PaymentId int IDENTITY(1,1) NOT NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Payment', 'PaymentId', 'int', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Payment') and name = 'TournamentId')
  BEGIN
	ALTER TABLE Payment ADD
	    TournamentId int NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Payment', 'TournamentId', 'int', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Payment') and name = 'GolferId')
  BEGIN
	ALTER TABLE Payment ADD
	    GolferId int NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Payment', 'GolferId', 'int', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Payment') and name = 'AuthorizationNumber')
  BEGIN
	ALTER TABLE Payment ADD
	    AuthorizationNumber varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Payment', 'AuthorizationNumber', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Payment') and name = 'ReferenceNumber')
  BEGIN
	ALTER TABLE Payment ADD
	    ReferenceNumber varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Payment', 'ReferenceNumber', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Payment') and name = 'TransactionNumber')
  BEGIN
	ALTER TABLE Payment ADD
	    TransactionNumber varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Payment', 'TransactionNumber', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Payment') and name = 'Amount')
  BEGIN
	ALTER TABLE Payment ADD
	    Amount money NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Payment', 'Amount', 'money', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Payment') and name = 'ProcessDate')
  BEGIN
	ALTER TABLE Payment ADD
	    ProcessDate datetime NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Payment', 'ProcessDate', 'datetime', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Payment') and name = 'PaymentStatus')
  BEGIN
	ALTER TABLE Payment ADD
	    PaymentStatus varchar(10) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Payment', 'PaymentStatus', 'varchar(10)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Payment') and name = 'CreditCardNumber')
  BEGIN
	ALTER TABLE Payment ADD
	    CreditCardNumber varchar(25) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Payment', 'CreditCardNumber', 'varchar(25)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Payment') and name = 'ExpirationDate')
  BEGIN
	ALTER TABLE Payment ADD
	    ExpirationDate varchar(4) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Payment', 'ExpirationDate', 'varchar(4)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Payment') and name = 'CardholderName')
  BEGIN
	ALTER TABLE Payment ADD
	    CardholderName varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Payment', 'CardholderName', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Payment') and name = 'Address1')
  BEGIN
	ALTER TABLE Payment ADD
	    Address1 varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Payment', 'Address1', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Payment') and name = 'Address2')
  BEGIN
	ALTER TABLE Payment ADD
	    Address2 varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Payment', 'Address2', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Payment') and name = 'City')
  BEGIN
	ALTER TABLE Payment ADD
	    City varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Payment', 'City', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Payment') and name = 'State')
  BEGIN
	ALTER TABLE Payment ADD
	    State char(2) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Payment', 'State', 'char(2)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Payment') and name = 'Country')
  BEGIN
	ALTER TABLE Payment ADD
	    Country varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Payment', 'Country', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Payment') and name = 'PostalCode')
  BEGIN
	ALTER TABLE Payment ADD
	    PostalCode varchar(10) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Payment', 'PostalCode', 'varchar(10)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Payment') and name = 'ConfirmationCode')
  BEGIN
	ALTER TABLE Payment ADD
	    ConfirmationCode varchar(50) NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Payment', 'ConfirmationCode', 'varchar(50)', 0
  END
GO

if not exists(select * from syscolumns where id=object_id('Payment') and name = 'PaymentDate')
  BEGIN
	ALTER TABLE Payment ADD
	    PaymentDate datetime NULL
  END
else
  BEGIN
	exec #spAlterColumn 'Payment', 'PaymentDate', 'datetime', 0
  END
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'PK_Payment') and OBJECTPROPERTY(id, N'IsPrimaryKey') = 1)
ALTER TABLE Payment WITH NOCHECK ADD
	CONSTRAINT PK_Payment PRIMARY KEY NONCLUSTERED
	(
		PaymentId
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'FK_Payment_Tournament') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE Payment ADD
	CONSTRAINT FK_Payment_Tournament FOREIGN KEY
	(
		TournamentId
	)
	REFERENCES Tournament
	(
		TournamentId
	)
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'FK_Payment_Golfer') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE Payment ADD
	CONSTRAINT FK_Payment_Golfer FOREIGN KEY
	(
		GolferId
	)
	REFERENCES Golfer
	(
		GolferId
	)
GO

if not exists (select * from sysindexes where name='IX_Payment_ConfirmationCode' and id=object_id(N'[Payment]'))
	CREATE UNIQUE INDEX IX_Payment_ConfirmationCode ON Payment
	(
        	ConfirmationCode
	)
GO
