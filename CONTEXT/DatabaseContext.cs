using System.Data;
using Microsoft.Data.SqlClient;

namespace CONTEXT
{
    public class DatabaseContext
    {
        private readonly string _connectionString;

        public DatabaseContext(string connectionString)
     {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public IDbConnection CreateConnection()
        {
   return new SqlConnection(_connectionString);
        }

        public async Task<bool> TestConnectionAsync()
        {
   try
          {
     using var connection = CreateConnection();
              await ((SqlConnection)connection).OpenAsync();
         return true;
     }
            catch
            {
       return false;
    }
     }
    }
}
