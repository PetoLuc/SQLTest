using HR.Dal.Contracts;
using Microsoft.Extensions.Options;

namespace HR.Dal.Services
{
    public class ConnectionStringProviderService : IConnectionStringProviderService {

        public ConnectionStringProviderService(IOptions<string> connectionString) {
            //ArgumentNullException.ThrowIfNull(connectionString?.Value);
            ConnectionString = "Server=localhost\\SQLEXPRESS;Database=CestovnePrikazyDB;Trusted_Connection=True;TrustServerCertificate=Yes";//connectionString.Value;
        }
        public string ConnectionString { get; }
    }
}
