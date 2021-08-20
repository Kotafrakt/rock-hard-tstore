using System.Collections.Generic;
using TransactionStore.DAL.Models;

namespace TransactionStore.Business.Services
{
    public interface ITransactionService
    {
        long AddDeposit(TransactionDto dto);
        long AddWithdraw(TransactionDto dto);
        (long, long) AddTransfer(TransferDto dto);
        List<TransactionDto> GetAllTransactions();
        List<TransactionDto> GetTransactionsByAccountId(int accountId);
    }
}
