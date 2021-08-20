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
        private const string _transactionDepositeOrWithdraw = "dbo.Transaction_DepositeOrWithdraw";
        private const string _transactionTransfer = "dbo.Transaction_Transfer";
        private const string _transactionSelectAll = "dbo.Transaction_SelectAll";
        private const string _transactionSelectByAccountId = "dbo.Transaction_SelectByAccountId";

        public TransactionRepository(IOptions<DatabaseSettings> options) : base(options) { }
        public int AddDepositeOrWithdraw(TransactionDto dto)
        {
            return _connection.QuerySingleOrDefault<int>(
                _transactionDepositeOrWithdraw,
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

        public (int, int) AddTransfer(TransferDto dto)
        {
            return _connection.QuerySingleOrDefault<(int, int)>(
                _transactionDepositeOrWithdraw,
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

        public List<TransactionDto> GetAllTransactions()
        {
            return _connection.Query<TransactionDto>(
                _transactionSelectAll,
                    commandType: CommandType.StoredProcedure
                )
                .ToList();
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
    }
}
