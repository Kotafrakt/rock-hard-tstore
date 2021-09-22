using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionStore.DAL.Models;

namespace TransactionStore.DAL.Repositories
{
    public interface ITransactionRepository
    {
        Task<long> AddDepositAsync(TransactionDto dto);
        long AddWithdrawAsync(TransactionDto dto);
        Task<(long, long)> AddTransferAsync(TransferDto dto);
        Task<List<TransactionDto>> GetTransactionsByAccountIdAsync(int accountId);
        Task<List<TransactionDto>> GetTransactionsByPeriodAsync(DateTime from, DateTime to, int? accountId);
    }
}