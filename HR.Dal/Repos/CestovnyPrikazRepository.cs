using HR.Dal.Contracts;
using HR.Dal.Helpers;
using HR.Dol;
using System.Data.SqlClient;

namespace HR.Dal.Repos
{
    public class CestovnyPrikazRepository(IConnectionStringProviderService connectionStirngProvider) : ICestovnyPrikazRepository {

        private readonly string _connectionString = connectionStirngProvider.ConnectionString;

        public async Task<List<CestovnyPrikaz>> GetAsync() {
            var cestovnePrikazy = new List<CestovnyPrikaz>();
            var columns = MetadataHelper.GetColumns<CestovnyPrikaz>();            

            using (var connection = new SqlConnection(_connectionString)) {
                var command = new SqlCommand($"SELECT {columns} FROM {MetadataHelper.GetTableName<CestovnyPrikaz>()}", connection);
                
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync()) {
                    cestovnePrikazy.Add(MapReaderToCestovnyPrikaz(reader));
                }
            }
            return cestovnePrikazy;
        }

        public async Task<List<CestovnyPrikaz>> GetByIdAsync(int id ) {
            var cestovnePrikazy = new List<CestovnyPrikaz>();
            var columns = MetadataHelper.GetColumns<CestovnyPrikaz>();
            var idColumn = MetadataHelper.GetColumnName<CestovnyPrikaz>(nameof(CestovnyPrikaz.CpId));

            using (var connection = new SqlConnection(_connectionString)) {
                var command = new SqlCommand($"SELECT {columns} FROM {MetadataHelper.GetTableName<CestovnyPrikaz>()}  WHERE {idColumn} = @Id", connection);                
                    command.Parameters.AddWithValue("@Id", id);
                
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync()) {
                    cestovnePrikazy.Add(MapReaderToCestovnyPrikaz(reader));
                }
            }
            return cestovnePrikazy;
        }


        public async Task AddAsync(CestovnyPrikaz cestovnyPrikaz) {
            var tableName = MetadataHelper.GetTableName<CestovnyPrikaz>();

            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(
                $"INSERT INTO {tableName} ({MetadataHelper.GetColumnName<CestovnyPrikaz>(nameof(CestovnyPrikaz.DatumVytvorenia))}, " +
                $"{MetadataHelper.GetColumnName<CestovnyPrikaz>(nameof(CestovnyPrikaz.UcastnikId))}, " +
                $"{MetadataHelper.GetColumnName<CestovnyPrikaz>(nameof(CestovnyPrikaz.MiestoZaciatku))}, " +
                $"{MetadataHelper.GetColumnName<CestovnyPrikaz>(nameof(CestovnyPrikaz.MiestoKonca))}, " +
                $"{MetadataHelper.GetColumnName<CestovnyPrikaz>(nameof(CestovnyPrikaz.DatumCasZaciatku))}, " +
                $"{MetadataHelper.GetColumnName<CestovnyPrikaz>(nameof(CestovnyPrikaz.DatumCasKonca))}, " +
                $"{MetadataHelper.GetColumnName<CestovnyPrikaz>(nameof(CestovnyPrikaz.StavId))}) " +
                "VALUES (@DatumVytvorenia, @Ucastnik, @MiestoZaciatku, @MiestoKonca, @DatumCasZaciatku, @DatumCasKonca, @StavId)",
                connection);

            command.Parameters.AddWithValue("@DatumVytvorenia", cestovnyPrikaz.DatumVytvorenia);
            command.Parameters.AddWithValue("@Ucastnik", cestovnyPrikaz.UcastnikId);
            command.Parameters.AddWithValue("@MiestoZaciatku", cestovnyPrikaz.MiestoZaciatku);
            command.Parameters.AddWithValue("@MiestoKonca", cestovnyPrikaz.MiestoKonca);
            command.Parameters.AddWithValue("@DatumCasZaciatku", cestovnyPrikaz.DatumCasZaciatku);
            command.Parameters.AddWithValue("@DatumCasKonca", cestovnyPrikaz.DatumCasKonca);
            command.Parameters.AddWithValue("@StavId", cestovnyPrikaz.StavId);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(CestovnyPrikaz cestovnyPrikaz) {
            var tableName = MetadataHelper.GetTableName<CestovnyPrikaz>();
            var idColumn = MetadataHelper.GetColumnName<CestovnyPrikaz>("CpId");

            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(
                $"UPDATE {tableName} SET " +
                $"{MetadataHelper.GetColumnName<CestovnyPrikaz>("DatumVytvorenia")} = @DatumVytvorenia, " +
                $"{MetadataHelper.GetColumnName<CestovnyPrikaz>("Ucastnik")} = @Ucastnik, " +
                $"{MetadataHelper.GetColumnName<CestovnyPrikaz>("MiestoZaciatku")} = @MiestoZaciatku, " +
                $"{MetadataHelper.GetColumnName<CestovnyPrikaz>("MiestoKonca")} = @MiestoKonca, " +
                $"{MetadataHelper.GetColumnName<CestovnyPrikaz>("DatumCasZaciatku")} = @DatumCasZaciatku, " +
                $"{MetadataHelper.GetColumnName<CestovnyPrikaz>("DatumCasKonca")} = @DatumCasKonca, " +
                $"{MetadataHelper.GetColumnName<CestovnyPrikaz>("StavId")} = @StavId " +
                $"WHERE {idColumn} = @Id",
                connection);

            command.Parameters.AddWithValue("@Id", cestovnyPrikaz.CpId);
            command.Parameters.AddWithValue("@DatumVytvorenia", cestovnyPrikaz.DatumVytvorenia);
            command.Parameters.AddWithValue("@Ucastnik", cestovnyPrikaz.UcastnikId);
            command.Parameters.AddWithValue("@MiestoZaciatku", cestovnyPrikaz.MiestoZaciatku);
            command.Parameters.AddWithValue("@MiestoKonca", cestovnyPrikaz.MiestoKonca);
            command.Parameters.AddWithValue("@DatumCasZaciatku", cestovnyPrikaz.DatumCasZaciatku);
            command.Parameters.AddWithValue("@DatumCasKonca", cestovnyPrikaz.DatumCasKonca);
            command.Parameters.AddWithValue("@StavId", cestovnyPrikaz.StavId);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id) {
            var tableName = MetadataHelper.GetTableName<CestovnyPrikaz>();
            var idColumn = MetadataHelper.GetColumnName<CestovnyPrikaz>("CpId");

            using var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand($"DELETE FROM {tableName} WHERE {idColumn} = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }

        private static CestovnyPrikaz MapReaderToCestovnyPrikaz(SqlDataReader reader) => new() {
            CpId = reader.GetInt32(reader.GetOrdinal(MetadataHelper.GetColumnName<CestovnyPrikaz>(nameof(CestovnyPrikaz.CpId)))),
            DatumVytvorenia = reader.GetDateTime(reader.GetOrdinal(MetadataHelper.GetColumnName<CestovnyPrikaz>(nameof(CestovnyPrikaz.DatumVytvorenia)))),
            UcastnikId = reader.GetString(reader.GetOrdinal(MetadataHelper.GetColumnName<CestovnyPrikaz>(nameof(CestovnyPrikaz.UcastnikId)))),
            MiestoZaciatku = reader.GetInt32(reader.GetOrdinal(MetadataHelper.GetColumnName<CestovnyPrikaz>(nameof(CestovnyPrikaz.MiestoZaciatku)))),
            MiestoKonca = reader.GetInt32(reader.GetOrdinal(MetadataHelper.GetColumnName<CestovnyPrikaz>(nameof(CestovnyPrikaz.MiestoZaciatku)))),
            DatumCasZaciatku = reader.GetDateTime(reader.GetOrdinal(MetadataHelper.GetColumnName<CestovnyPrikaz>(nameof(CestovnyPrikaz.DatumCasZaciatku)))),
            DatumCasKonca = reader.GetDateTime(reader.GetOrdinal(MetadataHelper.GetColumnName<CestovnyPrikaz>(nameof(CestovnyPrikaz.DatumCasKonca)))),
            StavId = reader.GetInt32(reader.GetOrdinal(MetadataHelper.GetColumnName<CestovnyPrikaz>(nameof(CestovnyPrikaz.StavId))))
        };
    }
}

