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
		t.Id,
		(select tr.AccountId
			from [dbo].[Transaction] tr
			where tr.Date = t.Date
			and tr.AccountId <> t.AccountId),		
		-t.Amount,
		t.Currency,
		t.TransactionType,
		t.[Date]
	FROM [dbo].[Transaction] t
		WHERE t.AccountId = @AccountId and t.TransactionType = 3
		ORDER BY t.Date
END