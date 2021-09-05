using System;
using System.Collections.Generic;
using TransactionStore.DAL.Models;

namespace TransactionStore.Business.Services
{
    public interface ITransactionService
    {
        long AddDeposit(TransactionDto dto);
        long AddWithdraw(TransactionDto dto);
        string AddTransfer(TransferDto dto);
        List<TransactionDto> GetTransactionsByAccountId(int accountId);
        List<TransactionDto> GetTransactionsByPeriod(GetByPeriodDto dto);
    }
}
