CREATE PROCEDURE dbo.Transaction_Transfer
	@AccountId		int,
	@RecipientAccountId		int,
	@Amount			decimal (14,3),
	@RecipientAmount		decimal (14,3),
	@Currency			int,
	@RecipientCurrency		int
AS
BEGIN
	DECLARE
	@CurrentDate datetime2 = getdate(),
	@TransactionType_Transfer int = 3

	INSERT INTO [dbo].[Transaction] (AccountId, Amount, Currency, TransactionType, [Date])
	VALUES (@AccountId, -@Amount, @Currency, @TransactionType_Transfer, @CurrentDate)
	DECLARE @SenderTransactionId bigint = @@IDENTITY
	
	INSERT INTO [dbo].[Transaction] (AccountId, Amount, Currency, TransactionType, [Date])
	VALUES (@RecipientAccountId, @RecipientAmount, @RecipientCurrency, @TransactionType_Transfer, @CurrentDate)
	DECLARE @RecipientTransactionId bigint = @@IDENTITY

	SELECT @SenderTransactionId, @RecipientTransactionId
END