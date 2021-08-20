using System.Collections.Generic;
using TransactionStore.DAL.Models;

namespace TransactionStore.DAL.Repositories
{ 
    public interface ITransactionRepository
    {
        public long AddDepositeOrWithdraw(TransactionDto dto);
        public (long, long) AddTransfer(TransferDto dto);
        List<TransactionDto> GetAllTransactions();
        List<TransactionDto> GetTransactionsByAccountId(int accountId);
    }
}
