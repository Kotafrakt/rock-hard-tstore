using System.Data;
using System.Data.SqlClient;

namespace TStore.DAL.Repositories
{
    public class TransactionRepository
    {
        protected const string _connectionString =
            @"Data Source=80.78.240.16;Initial Catalog = DevEdu; Persist Security Info=True;User ID = student;Password=qwe!23;";
        protected IDbConnection _connection;

        private const string _TransactionkInsert = "dbo.Transaction_Insert";

        protected TransactionRepository()
        {
            _connection = new SqlConnection(_connectionString);
        }

    }
}
