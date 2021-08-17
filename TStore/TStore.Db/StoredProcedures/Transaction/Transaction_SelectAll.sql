CREATE PROCEDURE dbo.Transaction_SelectAll
AS
BEGIN
	SELECT
		Id,
		AccountId,
		TransactionType,
		[Date],
		Amount
	FROM [dbo].[Transaction]
END