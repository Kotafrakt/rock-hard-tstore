CREATE PROCEDURE [dbo].[Transaction_Withdraw]
	@AccountId			int,
	@Amount				decimal (14,3),
	@Currency			int,
	@Date				datetime2
AS
BEGIN
	IF @Date = (
		select 
		top (1) t.Date from dbo.[Transaction] t
		where AccountId=@AccountId
		order by [Date] desc)
	BEGIN
		DECLARE
		@TransactionType_Withdraw int = 2

		INSERT INTO [dbo].[Transaction] (AccountId, TransactionType, [Date], Amount, Currency)
		VALUES (@AccountId, @TransactionType_Withdraw, getdate(), @Amount, @Currency)
	END
SELECT @@IDENTITY
END