using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using TransactionStore.Core;

namespace TransactionStore.DAL.Repositories
{
    public class BaseRepository
    {
        protected const string _connectionString =
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QQQ0817; Persist Security Info=False;";

        protected IDbConnection _connection;

        protected BaseRepository(IOptions<DatabaseSettings> options)
        {
            _connection = new SqlConnection(options.Value.ConnectionString);
        }
    }
}
