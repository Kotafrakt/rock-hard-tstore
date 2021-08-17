﻿using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Linq;
using TStore.DAL.Models;

namespace TStore.DAL.Repositories
{
    public class TransactionRepository : BaseRepository, ITransactionRepository
    {
        private const string _transactionkInsert = "dbo.Transaction_Insert";
        private const string _transactionkSelectAll = "dbo.Transaction_SelectAll";
        private const string _transactionkSelectByAccountId = "dbo.Transaction_SelectByAccountId";

        public TransactionRepository()
        {

        }

        public int AddTransaction(TransactionDto dto)
        {
            return _connection.QuerySingle<int>(
                _transactionkInsert,
                new
                {
                    dto.AccountId,
                    dto.TransactionType,
                    dto.Date,
                    dto.Amount
                },
                commandType: CommandType.StoredProcedure
                );
        }

        public List<TransactionDto> GetAllTransactions()
        {
            return _connection.Query<TransactionDto>(
                _transactionkSelectAll,
                    commandType: CommandType.StoredProcedure
                )
                .ToList();
        }

        public List<TransactionDto> GetTransactionsByAccountId(int accountId)
        {
            return _connection.Query<TransactionDto>(
                _transactionkSelectByAccountId,
                new { accountId },
                    commandType: CommandType.StoredProcedure
                )
                .ToList();
        }
    }
}
