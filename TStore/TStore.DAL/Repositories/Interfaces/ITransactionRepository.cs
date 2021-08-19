using System.Collections.Generic;
using TransactionStore.DAL.Models;

namespace TransactionStore.DAL.Repositories
{ 
    public interface ITransactionRepository
    {
        int AddTransaction(TransactionDto dto);
        List<TransactionDto> GetAllTransactions();
        List<TransactionDto> GetTransactionsByAccountId(int accountId);
    }
}
