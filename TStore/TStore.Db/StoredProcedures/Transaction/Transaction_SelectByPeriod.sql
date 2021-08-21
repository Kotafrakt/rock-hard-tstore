CREATE PROCEDURE dbo.Transaction_SelectByPeriod
@From datetime2,
@To datetime2
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
		WHERE [Date] BETWEEN @From and @To
END