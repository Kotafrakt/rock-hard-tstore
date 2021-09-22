CREATE PROCEDURE [dbo].[Transaction_Deposit]
	@AccountId			int,
	@Amount				decimal (14,3),
	@Currency			int
AS
BEGIN
	DECLARE
	@TransactionType_Deposit int = 1
	INSERT INTO [dbo].[Transaction] (AccountId, TransactionType, [Date], Amount, Currency)
	VALUES (@AccountId, @TransactionType_Deposit, getdate(), @Amount, @Currency)
	SELECT @@IDENTITY
END