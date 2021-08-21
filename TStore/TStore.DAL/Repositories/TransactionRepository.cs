using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Linq;
using TransactionStore.DAL.Models;
using TransactionStore.Core;
using Microsoft.Extensions.Options;
using System;

namespace TransactionStore.DAL.Repositories
{
    public class TransactionRepository : BaseRepository, ITransactionRepository
    {
        private const string _transactionDepositOrWithdraw = "dbo.Transaction_DepositOrWithdraw";
        private const string _transactionTransfer = "dbo.Transaction_Transfer";
        private const string _transactionSelectByPeriod = "dbo.Transaction_SelectByPeriod";
        private const string _transactionSelectByAccountId = "dbo.Transaction_SelectByAccountId";

        public TransactionRepository(IOptions<DatabaseSettings> options) : base(options) { }
        public long AddDepositeOrWithdraw(TransactionDto dto)
        {
            return _connection.QuerySingleOrDefault<long>(
                _transactionDepositOrWithdraw,
                new
                {
                    dto.AccountId,
                    dto.TransactionType,
                    dto.Amount,
                    dto.Currency
                },
                commandType: CommandType.StoredProcedure
                );
        }

        public (long, long) AddTransfer(TransferDto dto)
        {
            return _connection.QuerySingleOrDefault<(long, long)>(
                _transactionTransfer,
                new
                {
                    dto.AccountId,
                    dto.RecipientAccountId,
                    dto.Amount,
                    dto.RecipientAmount,
                    dto.Currency,
                    dto.RecipientCurrency,
                },
                commandType: CommandType.StoredProcedure
                );
        }

        public List<TransactionDto> GetTransactionsByAccountId(int accountId)
        {
            return _connection.Query<TransactionDto>(
                _transactionSelectByAccountId,
                new { accountId },
                    commandType: CommandType.StoredProcedure
                )
                .ToList();
        }

        public List<TransactionDto> GetTransactionsByPeriod(DateTime from, DateTime to, int accountId)
        {
            return _connection.Query<TransactionDto>(
                _transactionSelectByPeriod,
                 new 
                 { 
                     from,
                     to,
                     accountId
                 },
                    commandType: CommandType.StoredProcedure
                )
                .ToList();
        }
    }
}
