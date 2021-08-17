using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using TStore.DAL.Models;

namespace TStore.DAL.Repositories
{
    public class TransactionRepository : BaseRepository, ITransactionRepository
    {
        private const string _transactionkInsert = "dbo.Transaction_Insert";
        private const string _transactionkSelectAll = "dbo.Transaction_SelectAll";

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

        public List<TransactionDto> GetAllTransaction()
        {
            return _connection.Query<TransactionDto>(
                _transactionkSelectAll,
                    commandType: CommandType.StoredProcedure
                )
                .ToList();
        }
    }
}
