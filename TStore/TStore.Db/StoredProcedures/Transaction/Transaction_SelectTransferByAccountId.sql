CREATE PROCEDURE dbo.Transaction_SelectTransferByAccountId
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
UNION ALL
	SELECT
		t.Id,		
		t.AccountId,
			(select tr.AccountId
			from [dbo].[Transaction] tr
			where tr.Date = t.Date
			and tr.AccountId <> t.AccountId) as RecipientId,
		t.Amount,
		t.Currency,
		t.TransactionType,
		t.[Date]
	FROM [dbo].[Transaction] t
		WHERE t.AccountId = @AccountId and t.Amount < 0 and t.TransactionType = 3
UNION ALL
	SELECT
		t.Id,
		(select tr.AccountId
			from [dbo].[Transaction] tr
			where tr.Date = t.Date
			and tr.AccountId <> t.AccountId),
		t.AccountId as RecipientId,			
		t.Amount,
		t.Currency,
		t.TransactionType,
		t.[Date]
	FROM [dbo].[Transaction] t
		WHERE t.AccountId = @AccountId and t.Amount > 0 and t.TransactionType = 3
END