using HR.Dal.MsSql.Repos.Contracts;
using HR.Dal.Repos;
using HR.Dal.Services.Contracts;
using HR.Dol;
using System.Data;
using System.Data.SqlClient;

namespace HR.Dal.MsSql.Repos
{
    public class ZamestnanecRepository(IConnectionStringProviderService connectionStringProvider)
        : RepositoryBase(connectionStringProvider), IZamestnanecRepository
    {

        public async Task<List<Zamestnanec>> GetAllZamestnanciAsync()
        {
            string query = "SELECT osobne_cislo, krstne_meno, priezvisko, datum_narodenia,rodne_cislo FROM Zamestnanec";
            using SqlCommand command = new(query);
            return await ReadRecordsAsync(command, MapReaderToZamestnanec);
        }
        private static Zamestnanec MapReaderToZamestnanec(SqlDataReader reader) => new()
        {
            OsobneCislo = reader.GetString("osobne_cislo"),
            KrstneMeno = reader.GetString("krstne_meno"),
            Priezvisko = reader.GetString("priezvisko"),
            DatumNarodenia = reader.GetDateTime("datum_narodenia"),
            RodneCislo = reader.GetString("rodne_cislo")
        };


    }
}

