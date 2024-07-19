using HR.Dal.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace HR.Dal.Services
{
    public class ConnectionStringProviderService(IConfiguration configuration) : IConnectionStringProviderService {
        
        public string ConnectionString { get; } = configuration.GetConnectionString("DefaultConnection")?? throw new InvalidOperationException("No connection string defined!!!!");
    }
}
