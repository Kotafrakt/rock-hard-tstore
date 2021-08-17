CREATE PROCEDURE dbo.Transaction_Insert
	@AccountId int,
	@TransactionType int,
	@Date datetime2 (7),
	@Amount decimal (14,3)
AS
BEGIN
	INSERT INTO [dbo].[Transaction] (AccountId, TransactionType, [Date], Amount)
	VALUES (@AccountId, @TransactionType, @Date, @Amount)
	SELECT @@IDENTITY
END
