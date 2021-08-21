using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Linq;
using TransactionStore.DAL.Models;
using TransactionStore.Core;
using Microsoft.Extensions.Options;

namespace TransactionStore.DAL.Repositories
{
    public class TransactionRepository : BaseRepository, ITransactionRepository
    {
        private const string _transactionDepositOrWithdraw = "dbo.Transaction_DepositOrWithdraw";
        private const string _transactionTransfer = "dbo.Transaction_Transfer";
        private const string _transactionSelectByPeriod = "dbo.Transaction_SelectByPeriod";
        private const string _transactionSelectByAccountId = "dbo.Transaction_SelectByAccountId";
        private const string _transactionSelectTransferByAccountId = "dbo.Transaction_SelectTransferByAccountId";

        public TransactionRepository(IOptions<DatabaseSettings> options) : base(options) { }
        public long AddDepositeOrWithdraw(TransactionDto dto)
        {
            return _connection.QuerySingleOrDefault<long>(
                _transactionDepositOrWithdraw,
                new
                {
                    dto.AccountId,
                    dto.Type,
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
                    dto.SenderAccountId,
                    dto.RecipientAccountId,
                    dto.SenderAmount,
                    dto.RecipientAmount,
                    dto.SenderCurrency,
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

        public List<TransferDto> GetTransfersByAccountId(int accountId)
        {
            return _connection.Query<TransferDto>(
                _transactionSelectTransferByAccountId,
                new { accountId },
                    commandType: CommandType.StoredProcedure
                )
                .ToList();
        }

        public List<TransactionDto> GetTransactionsByPeriod()
        {
            return _connection.Query<TransactionDto>(
                _transactionSelectByPeriod,
                    commandType: CommandType.StoredProcedure
                )
                .ToList();
        }
    }
}
