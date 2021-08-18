CREATE PROCEDURE dbo.Transaction_Insert
	@AccountId			int,
	@TransactionType	int,
	@Amount				decimal (14,3)
AS
BEGIN
	INSERT INTO [dbo].[Transaction] (AccountId, TransactionType, [Date], Amount)
	VALUES (@AccountId, @TransactionType, getdate(), @Amount)
	SELECT @@IDENTITY
END