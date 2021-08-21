CREATE PROCEDURE dbo.Transaction_SelectByAccountId
	@AccountId int
AS
BEGIN
	SELECT
		Id,		
		AccountId,
		Amount,
		Currency,
		TransactionType,
		[Date]
	FROM [dbo].[Transaction]
		WHERE AccountId = @AccountId and (TransactionType = 1 or TransactionType = 2)
END