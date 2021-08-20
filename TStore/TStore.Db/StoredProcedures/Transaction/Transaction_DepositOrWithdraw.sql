CREATE PROCEDURE dbo.Transaction_DepositOrWithdraw
	@AccountId			int,
	@TransactionType	int,
	@Amount				decimal (14,3),
	@Currency			int
AS
BEGIN
	BEGIN
	IF @TransactionType = 1
		BEGIN
			INSERT INTO [dbo].[Transaction] (AccountId, TransactionType, [Date], Amount, Currency)
			VALUES (@AccountId, @TransactionType, getdate(), @Amount, @Currency)
		END
	ELSE
		BEGIN
			INSERT INTO [dbo].[Transaction] (AccountId, TransactionType, [Date], Amount, Currency)
			VALUES (@AccountId, @TransactionType, getdate(), -@Amount, @Currency)
		END
	END
	SELECT @@IDENTITY
END