CREATE PROCEDURE dbo.Transaction_SelectByAccountId
@AccountId int
AS
BEGIN
	SELECT
		Id,		
		TransactionType,
		AccountId,
		[Date],
		Amount
	FROM [dbo].[Transaction]
		WHERE AccountId=@AccountId
END
