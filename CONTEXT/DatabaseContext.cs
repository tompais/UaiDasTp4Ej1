using Microsoft.Data.SqlClient;
using System.Data;

namespace CONTEXT
{
    public class DatabaseContext(string connectionString)
    {
        public IDbConnection CreateConnection() =>
            new SqlConnection(connectionString);

        public bool TestConnection()
        {
            try
            {
                using var connection = CreateConnection();
                connection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
