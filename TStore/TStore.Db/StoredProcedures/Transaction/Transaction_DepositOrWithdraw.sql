CREATE PROCEDURE dbo.Transaction_DepositOrWithdraw
	@AccountId			int,
	@TransactionType	int,
	@Amount				decimal (14,3),
	@Currency			int
AS
BEGIN
	BEGIN
			INSERT INTO [dbo].[Transaction] (AccountId, Amount, Currency, TransactionType, [Date])
			VALUES (@AccountId, @Amount, @Currency, @TransactionType, getdate())
	END
	SELECT @@IDENTITY
END