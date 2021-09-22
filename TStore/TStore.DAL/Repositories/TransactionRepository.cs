using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TransactionStore.Core;
using TransactionStore.DAL.Models;

namespace TransactionStore.DAL.Repositories
{
    public class TransactionRepository : BaseRepository, ITransactionRepository
    {
        private const string _transactionDeposit = "dbo.Transaction_Deposit";
        private const string _transactionWithdraw = "dbo.Transaction_Withdraw";
        private const string _transactionTransfer = "dbo.Transaction_Transfer";
        private const string _transactionSelectByPeriod = "dbo.Transaction_SelectByPeriod";
        private const string _transactionSelectByAccountId = "dbo.Transaction_SelectByAccountId";
        private const string format = "yyyy-MM-dd HH:mm:ss:fffffff";

        public TransactionRepository(IOptions<DatabaseSettings> options) : base(options) { }

        public async Task<long> AddDepositAsync(TransactionDto dto)
        {
            return await _connection.QuerySingleOrDefaultAsync<long>(
                _transactionDeposit,
                new
                {
                    dto.AccountId,
                    dto.Amount,
                    dto.Currency,
                    dto.Date
                },
                commandType: CommandType.StoredProcedure
                );
        }

        public long AddWithdrawAsync(TransactionDto dto)
        {
            SqlMapper.AddTypeMap(typeof(DateTime), System.Data.DbType.DateTime2);
            var date = dto.Date.ToString(format);
            var date2 = DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
            return _connection.QuerySingleOrDefault<long>(
                _transactionWithdraw,
                new
                {
                    dto.AccountId,
                    dto.Amount,
                    dto.Currency,
                    dto.Date
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
                    dto.Date
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