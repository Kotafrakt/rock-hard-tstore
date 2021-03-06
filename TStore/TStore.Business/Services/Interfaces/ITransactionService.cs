using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionStore.DAL.Models;

namespace TransactionStore.Business.Services
{
    public interface ITransactionService
    {
        Task<long> AddDepositAsync(TransactionDto dto);
        Task<long> AddWithdrawAsync(TransactionDto dto);
        Task<List<long>> AddTransferAsync(TransferDto dto);
        Task<string> GetTransactionsByAccountIdAsync(int accountId);
        Task<string> GetTransactionsByPeriodAsync(GetByPeriodDto dto, string leadId);
        Task<string> GetTransactionsByAccountIdsForTwoMonthsAsync(List<int> accountIds);
    }
}