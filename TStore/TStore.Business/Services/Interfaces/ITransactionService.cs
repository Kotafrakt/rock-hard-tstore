using System;
using System.Collections.Generic;
using TransactionStore.DAL.Models;

namespace TransactionStore.Business.Services
{
    public interface ITransactionService
    {
        long AddDeposit(TransactionDto dto);
        long AddWithdraw(TransactionDto dto);
        public List<long> AddTransfer(TransferDto dto);
        string GetTransactionsByAccountId(int accountId);
        string GetTransactionsByPeriod(GetByPeriodDto dto, string leadId);
    }
}
