using HR.Dol;
using System.Data.SqlClient;

namespace HR.Dal.Repos
{
    public class DopravaRepository
    {
        private readonly string _connectionString;

        public DopravaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Doprava> GetDopravaByCpId(int cpId)
        {
            List<Doprava> dopravaList = new List<Doprava>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT doprava_id, cp_id, doprava_typ_id FROM Doprava WHERE cp_id = @CpId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CpId", cpId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Doprava doprava = new Doprava
                            {
                                DopravaId = reader.GetInt32(0),
                                CpId = reader.GetInt32(1),
                                DopravaTypId = reader.GetInt32(2),

                            };
                            dopravaList.Add(doprava);
                        }
                    }
                }
            }

            return dopravaList;
        }
    }
}
}
