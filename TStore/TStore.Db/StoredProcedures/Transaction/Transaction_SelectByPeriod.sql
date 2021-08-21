CREATE PROCEDURE dbo.Transaction_SelectByPeriod
	@From				datetime2,
	@To					datetime2,
	@AccountId			int
AS
BEGIN
IF @AccountId = 0
	BEGIN
		SELECT
			Id,
			AccountId,
			Amount,
			Currency,
			TransactionType,
			[Date]
		FROM [dbo].[Transaction]
			WHERE [Date] BETWEEN @From and @To
	END
ELSE
	BEGIN
		SELECT
			t.Id,		
			t.AccountId,
			t.Amount,
			t.Currency,
			t.TransactionType,
			t.[Date]
		FROM [dbo].[Transaction] t
			WHERE [Date] BETWEEN @From and @To and t.AccountId = @AccountId
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
			WHERE [Date] BETWEEN @From and @To and  t.AccountId = @AccountId and t.TransactionType = 3
			ORDER BY t.Date
	END
END