CREATE PROCEDURE dbo.Transaction_Transfer
	@SenderAccountId		int,
	@RecipientAccountId		int,
	@SenderAmount			decimal (14,3),
	@RecipientAmount		decimal (14,3),
	@SenderCurrency			int,
	@RecipientCurrency		int
AS
BEGIN
	DECLARE
	@CurrentDate datetime2 = getdate()
	INSERT INTO [dbo].[Transaction] (AccountId, Amount, Currency, TransactionType, [Date])
	VALUES (@SenderAccountId, @SenderAmount, @SenderCurrency, 3, @CurrentDate)
	DECLARE @SenderTransactionId int = @@IDENTITY
	INSERT INTO [dbo].[Transaction] (AccountId, Amount, Currency, TransactionType, [Date])
	VALUES (@RecipientAccountId, @RecipientAmount, @RecipientCurrency, 3, @CurrentDate)
	DECLARE @RecipientTransactionId int = @@IDENTITY
	SELECT @SenderAccountId, @RecipientAccountId
END