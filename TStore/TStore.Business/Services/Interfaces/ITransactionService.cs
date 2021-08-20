using System.Collections.Generic;
using TransactionStore.DAL.Models;

namespace TransactionStore.Business.Services
{
    public interface ITransactionService
    {
        TransactionDto AddDeposit(TransactionDto dto);
        TransactionDto AddWithdraw(TransactionDto dto);
        TransferDto AddTransfer(TransferDto dto);
        List<TransactionDto> GetAllTransactions();
        List<TransactionDto> GetTransactionsByAccountId(int accountId);
    }
}
