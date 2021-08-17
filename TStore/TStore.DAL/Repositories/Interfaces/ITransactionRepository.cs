using System.Collections.Generic;
using TStore.DAL.Models;

namespace TStore.DAL.Repositories
{ 
    public interface ITransactionRepository
    {
        int AddTransaction(TransactionDto dto);
        List<TransactionDto> GetAllTransactions();
        List<TransactionDto> GetTransactionsByAccountId(int accountId);
    }
}
