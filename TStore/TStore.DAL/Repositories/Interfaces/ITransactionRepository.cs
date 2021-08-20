using System.Collections.Generic;
using TransactionStore.DAL.Models;

namespace TransactionStore.DAL.Repositories
{ 
    public interface ITransactionRepository
    {
        public int AddDepositeOrWithdraw(TransactionDto dto);
        public (int, int) AddTransfer(TransferDto dto);
        List<TransactionDto> GetAllTransactions();
        List<TransactionDto> GetTransactionsByAccountId(int accountId);
    }
}
