using HR.Dol;
using System.Data;
using System.Data.SqlClient;

namespace HR.Dal.Repos
{
    public class MestoRepository(string connectionString)
    {
        private readonly string _connectionString = connectionString;

        public List<Mesto> GetAllMesta()
        {
            List<Mesto> mestaList = [];

            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();

                string query = "SELECT mesto_id, nazov_mesta, stat, zemepisna_sirka, zemepisna_dlzka FROM Mesto";
                using SqlCommand command = new(query, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Mesto mesto = new()
                    {
                        MestoId = reader.GetInt32("mesto_id"),
                        NazovMesta = reader.GetString("nazov_mesta"),
                        Stat = reader.GetString("stat"),
                        ZemepisnaSirka = reader.GetDecimal("zemepisna_sirka"),
                        ZemepisnaDlzka = reader.GetDecimal("zemepisna_dlzka")
                    };
                    mestaList.Add(mesto);
                }
            }
            return mestaList;
        }
    }
}
