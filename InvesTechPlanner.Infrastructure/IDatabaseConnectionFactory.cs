using System.Data;

namespace InvesTechPlanner.Infrastructure
{
    public interface IDatabaseConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
