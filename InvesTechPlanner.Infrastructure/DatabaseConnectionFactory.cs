using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace InvesTechPlanner.Infrastructure
{
    public class DatabaseConnectionFactory(IConfiguration configuration) : IDatabaseConnectionFactory
    {
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException(nameof(configuration), "Connection string cannot be null.");

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
