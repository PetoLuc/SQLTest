using HR.Dal.Repos.Contracts;
using HR.Dal.Services.Contracts;
using HR.Dol;
using System.Data;
using System.Data.SqlClient;

namespace HR.Dal.Repos
{
    public class MestoRepository(IConnectionStringProviderService connectionStringProvider)
        : RepositoryBase(connectionStringProvider), IMestoRepository
    {
        public async Task<List<Mesto>> GetAllMestoForSelectAsync()
        {
            string query = "SELECT mesto_id, nazov_mesta, stat FROM Mesto";
            using SqlCommand command = new(query);
            return await ReadRecordsAsync(command, MapReaderToMestoForSelectOnly);
        }
        private static Mesto MapReaderToMestoForSelectOnly(SqlDataReader reader) => new()
        {
            MestoId = reader.GetInt32("mesto_id"),
            NazovMesta = reader.GetString("nazov_mesta"),
            Stat = reader.GetString("stat"),
           //ZemepisnaSirka = reader.GetDecimal("zemepisna_sirka"),
           //ZemepisnaDlzka = reader.GetDecimal("zemepisna_dlzka")            
        };
    }
}
