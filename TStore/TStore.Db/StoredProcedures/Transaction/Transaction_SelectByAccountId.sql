CREATE PROCEDURE dbo.Transaction_SelectByAccountId
	@AccountId int
AS
BEGIN
	SELECT
		t.Id,		
		t.AccountId,
		t.Amount,
		t.Currency,
		t.TransactionType,
		t.[Date]
	FROM [dbo].[Transaction] t
	WHERE t.AccountId = @AccountId
UNION ALL
	SELECT
		tr.Id,
		tr.AccountId,		
		tr.Amount,
		tr.Currency,
		tr.TransactionType,
		tr.[Date]
	FROM [dbo].[Transaction] t
	LEFT JOIN [dbo].[Transaction] tr
	ON t.Date = tr.Date
	WHERE t.AccountId = @AccountId and tr.AccountId <> t.AccountId and t.TransactionType = 3
	ORDER BY t.Id
END