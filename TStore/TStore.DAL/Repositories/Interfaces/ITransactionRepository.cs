using System.Collections.Generic;
using TransactionStore.DAL.Models;

namespace TransactionStore.DAL.Repositories
{ 
    public interface ITransactionRepository
    {
        long AddDepositeOrWithdraw(TransactionDto dto);
        (long, long) AddTransfer(TransferDto dto);
        List<TransactionDto> GetTransactionsByAccountId(int accountId);
        List<TransferDto> GetTransfersByAccountId(int accountId);
        List<TransactionDto> GetTransactionsByPeriod();
    }
}
