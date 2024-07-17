using HR.Dol;
using System.Data.SqlClient;

namespace HR.Dal.Repos.Contracts
{
    public class ZamestnanecRepository
    {
        private readonly string _connectionString;

        public ZamestnanecRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Zamestnanec> GetAllZamestnanci()
        {
            List<Zamestnanec> zamestnanciList = [];

            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();

                string query = "SELECT osobne_cislo, krstne_meno, priezvisko, datum_narodenia, rodne_cislo FROM Zamestnanec";
                using SqlCommand command = new(query, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Zamestnanec zamestnanec = new Zamestnanec
                    {
                        OsobneCislo = reader.GetString(0),
                        KrstneMeno = reader.GetString(1),
                        Priezvisko = reader.GetString(2),
                        DatumNarodenia = reader.GetDateTime(3),
                        RodneCislo = reader.GetString(4)
                    };
                    zamestnanciList.Add(zamestnanec);
                }
            }
            return zamestnanciList;
        }
    }
}
