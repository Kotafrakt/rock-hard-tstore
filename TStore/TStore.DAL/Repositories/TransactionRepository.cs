using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using TStore.DAL.Models;

namespace TStore.DAL.Repositories
{
    public class TransactionRepository
    {
        protected const string _connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QQQ0817; Persist Security Info=False;";

        protected IDbConnection _connection;

        private const string _transactionkInsert = "dbo.Transaction_Insert";
        private const string _transactionkSelectAll = "dbo.Transaction_SelectAll";

        protected TransactionRepository()
        {
            _connection = new SqlConnection(_connectionString);
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
            return _connection
                .Query<TransactionDto>(
                _transactionkSelectAll,
                    commandType: CommandType.StoredProcedure
                )
                .ToList();
        }
    }
}
