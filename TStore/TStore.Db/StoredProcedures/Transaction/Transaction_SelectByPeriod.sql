CREATE PROCEDURE dbo.Transaction_SelectByPeriod
	@From				datetime2,
	@To					datetime2,
	@AccountId			int = NULL
AS
BEGIN
IF @AccountId is null
	SELECT
		t.Id,		
		t.AccountId,
		t.Amount,
		t.Currency,
		t.TransactionType,
		t.[Date]
	FROM [dbo].[Transaction] t
	WHERE t.[Date] BETWEEN @From and @To
ELSE
	SELECT
		t.Id,		
		t.AccountId,
		t.Amount,
		t.Currency,
		t.TransactionType,
		t.[Date]
	FROM [dbo].[Transaction] t
	WHERE t.[Date] BETWEEN @From and @To and t.AccountId = @AccountId
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
	WHERE t.[Date] BETWEEN @From and @To and t.AccountId = @AccountId and tr.AccountId <> t.AccountId and t.TransactionType = 3 
	ORDER BY t.Date
END