if exists (select * from sysobjects where id = object_id(N'[vwParticipant]') and OBJECTPROPERTY(id, N'IsView') = 1)
drop view [vwParticipant]
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW vwParticipant

AS

SELECT
	Participant.ParticipantId,
	Participant.TeamId,
	Participant.TournamentId,
	Participant.GolferId,
	Participant.PaymentId,
	Participant.IsValid,
	Participant.RegistrationFee,
	Tournament.TournamentId Tournament_TournamentId,
	Tournament.Name Tournament_Name,
	Tournament.Description Tournament_Description,
	Tournament.NumberOfTeams Tournament_NumberOfTeams,
	Tournament.TeamSize Tournament_TeamSize,
	Tournament.Format Tournament_Format,
	Tournament.RegistrationBeginDate Tournament_RegistrationBeginDate,
	Tournament.RegistrationEndDate Tournament_RegistrationEndDate,
	Tournament.RegistrationFee Tournament_RegistrationFee,
	Tournament.OrganizerId Tournament_OrganizerId,
	Tournament.CancellationCutoffDate Tournament_CancellationCutoffDate,
	Tournament.RegistrationFeeDescription Tournament_RegistrationFeeDescription,
	Tournament.DatesText Tournament_DatesText,
	Tournament.PrizesText Tournament_PrizesText,
	Tournament.SponsorsText Tournament_SponsorsText,
	Tournament.LocationsText Tournament_LocationsText,
	Tournament.MaximumHandicap Tournament_MaximumHandicap,
	Tournament.RegisteredParticipants Tournament_RegisteredParticipants,
	Tournament.Host Tournament_Host,
	Tournament.ShowPercentFull Tournament_ShowPercentFull,
	Tournament.ShowParticipants Tournament_ShowParticipants,
	Payment.PaymentId Payment_PaymentId,
	Payment.TournamentId Payment_TournamentId,
	Payment.GolferId Payment_GolferId,
	Payment.AuthorizationNumber Payment_AuthorizationNumber,
	Payment.ReferenceNumber Payment_ReferenceNumber,
	Payment.TransactionNumber Payment_TransactionNumber,
	Payment.Amount Payment_Amount,
	Payment.ProcessDate Payment_ProcessDate,
	Payment.PaymentStatus Payment_PaymentStatus,
	Payment.CreditCardNumber Payment_CreditCardNumber,
	Payment.ExpirationDate Payment_ExpirationDate,
	Payment.CardholderName Payment_CardholderName,
	Payment.Address1 Payment_Address1,
	Payment.Address2 Payment_Address2,
	Payment.City Payment_City,
	Payment.State Payment_State,
	Payment.Country Payment_Country,
	Payment.PostalCode Payment_PostalCode,
	Payment.ConfirmationCode Payment_ConfirmationCode,
	Payment.PaymentDate Payment_PaymentDate,
	Golfer.GolferId Golfer_GolferId,
	Golfer.FirstName Golfer_FirstName,
	Golfer.MiddleInitial Golfer_MiddleInitial,
	Golfer.LastName Golfer_LastName,
	Golfer.Phone Golfer_Phone,
	Golfer.Email Golfer_Email,
	Golfer.Address1 Golfer_Address1,
	Golfer.Address2 Golfer_Address2,
	Golfer.City Golfer_City,
	Golfer.State Golfer_State,
	Golfer.Country Golfer_Country,
	Golfer.PostalCode Golfer_PostalCode,
	Golfer.DateOfBirth Golfer_DateOfBirth,
	Golfer.Handicap Golfer_Handicap,
	Golfer.CourseNumber Golfer_CourseNumber,
	Golfer.PlayerNumber Golfer_PlayerNumber,
	Golfer.Gender Golfer_Gender,
	Golfer.GolferStatus Golfer_GolferStatus,
	Team.TeamId Team_TeamId,
	Team.RegistrationKey Team_RegistrationKey,
	Team.Status Team_Status,
	Team.TournamentId Team_TournamentId
FROM
	Participant
LEFT JOIN vwTournament Tournament on Participant.TournamentId = Tournament.TournamentId
LEFT JOIN vwPayment Payment on Participant.PaymentId = Payment.PaymentId
LEFT JOIN vwGolfer Golfer on Participant.GolferId = Golfer.GolferId
LEFT JOIN vwTeam Team on Participant.TeamId = Team.TeamId
GO
