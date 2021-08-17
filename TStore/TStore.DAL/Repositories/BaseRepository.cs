using System.Data;
using System.Data.SqlClient;

namespace TStore.DAL.Repositories
{
    public class BaseRepository
    {
        protected const string _connectionString =
                @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=QQQ0817; Persist Security Info=False;";

        protected IDbConnection _connection;

        protected BaseRepository()
        {
            _connection = new SqlConnection(_connectionString);
        }
    }
}
