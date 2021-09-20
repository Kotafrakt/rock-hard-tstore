using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TransactionStore.Core;
using TransactionStore.DAL.Models;

namespace TransactionStore.DAL.Repositories
{
    public class TransactionRepository : BaseRepository, ITransactionRepository
    {
        private const string _transactionDepositOrWithdraw = "dbo.Transaction_DepositOrWithdraw";
        private const string _transactionTransfer = "dbo.Transaction_Transfer";
        private const string _transactionSelectByPeriod = "dbo.Transaction_SelectByPeriod";
        private const string _transactionSelectByAccountId = "dbo.Transaction_SelectByAccountId";

        public TransactionRepository(IOptions<DatabaseSettings> options) : base(options) { }
        public async Task<long> AddDepositeOrWithdrawAsync(TransactionDto dto)
        {
            return await _connection.QuerySingleOrDefaultAsync<long>(
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

        public async Task<(long, long)> AddTransferAsync(TransferDto dto)
        {
            return await _connection.QuerySingleOrDefaultAsync<(long, long)>(
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

        public async Task<List<TransactionDto>> GetTransactionsByAccountIdAsync(int accountId)
        {
            return (await _connection.QueryAsync<TransactionDto>(
                _transactionSelectByAccountId,
                new { accountId },
                    commandType: CommandType.StoredProcedure
                ))
                .ToList();
        }

        public async Task<List<TransactionDto>> GetTransactionsByPeriodAsync(DateTime from, DateTime to, int? accountId)
        {
                return (await _connection.QueryAsync<TransactionDto>(
                        _transactionSelectByPeriod,
                        new
                        {
                        from,
                        to,
                        accountId
                    },
                    commandType: CommandType.StoredProcedure,
                    commandTimeout: 300
                ))
                .ToList();
        }
    }
}