CREATE PROCEDURE [dbo].[Transaction_SelectByAccountIdsForTwoMonths]
  @tblIds [dbo].[TransactionsByLeadType] readonly
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
	inner join @tblIds ids on t.AccountId=ids.accountIds
  WHERE t.[Date] BETWEEN DATEADD(month, -2, GETDATE())  and getdate() 
  EXCEPT 
  SELECT
    t.Id,    
    t.AccountId,
    t.Amount,
    t.Currency,
    t.TransactionType,
	t.[Date]
  FROM [dbo].[Transaction] t
	inner join @tblIds ids on t.AccountId=ids.accountIds
	LEFT JOIN [dbo].[Transaction] tr
	ON t.Date = tr.Date
  WHERE t.[Date] BETWEEN DATEADD(month, -2, GETDATE())  and getdate() AND t.TransactionType = 3
  AND t.Amount < 0
  End