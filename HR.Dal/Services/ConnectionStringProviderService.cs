using HR.Dal.Services.Contracts;
using Microsoft.Extensions.Options;

namespace HR.Dal.Services
{
    public class ConnectionStringProviderService : IConnectionStringProviderService {

        public ConnectionStringProviderService(IOptions<string> connectionString) {
            //ArgumentNullException.ThrowIfNull(connectionString?.Value);
            ConnectionString = "Server=PL_NTB\\SQLEXPRESS;Database=CestovnePrikazyDB;Trusted_Connection=True;TrustServerCertificate=Yes";//connectionString.Value;
        }
        public string ConnectionString { get; }
    }
}
