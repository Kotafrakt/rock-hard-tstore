using System.Collections.Generic;
using TStore.DAL.Models;

namespace TStore.Business.Services
{
    public interface ITransactionService
    {
        TransactionDto AddTransaction(TransactionDto dto);
        List<TransactionDto> GetAllTransactions();
        List<TransactionDto> GetTransactionsByAccountId(int accountId);
    }
}
