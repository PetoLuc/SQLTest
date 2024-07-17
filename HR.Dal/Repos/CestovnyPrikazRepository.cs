using HR.Dal.Contracts;
using HR.Dol;
using System.Data;
using System.Data.SqlClient;

namespace HR.Dal.Repos {
    public class CestovnyPrikazRepository(IConnectionStringProviderService connectionStirngProvider) : ICestovnyPrikazRepository {

        private readonly string _connectionString = connectionStirngProvider.ConnectionString;

        public async Task<List<CestovnyPrikaz>> GetAsync() {
            var cestovnePrikazy = new List<CestovnyPrikaz>();

            using (var connection = new SqlConnection(_connectionString)) {

                var query = @"SELECT cp.*, mz.nazov_mesta AS mz_nazov_mesta, mz.stat AS mz_stat, mz.zemepisna_sirka AS mz_zemepisna_sirka, mz.zemepisna_dlzka AS mz_zemepisna_dlzka,
                                mk.nazov_mesta AS mk_nazov_mesta, mk.stat AS mk_stat, mk.zemepisna_sirka AS mk_zemepisna_sirka, mk.zemepisna_dlzka AS mk_zemepisna_dlzka,                                
                                z.krstne_meno, z.priezvisko, z.datum_narodenia, z.rodne_cislo
                             FROM CestovnyPrikaz cp
                             JOIN Mesto mz ON cp.miesto_zaciatku = mz.mesto_id
                             JOIN Mesto mk ON cp.miesto_konca = mk.mesto_id
                             JOIN Stav st ON cp.stav_id = st.stav_id
                             JOIN Zamestnanec z ON cp.ucastnik = z.osobne_cislo";
                var command = new SqlCommand(query, connection);
                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync()) {
                    cestovnePrikazy.Add(MapReaderToCestovnyPrikaz(reader));
                }
            }
            return cestovnePrikazy;
        }



        private static CestovnyPrikaz MapReaderToCestovnyPrikaz(SqlDataReader reader) {
            var cestovnyPrikaz = new CestovnyPrikaz {
                // Basic fields from CestovnyPrikaz
                CpId = reader.GetInt32("cp_id"),
                DatumVytvorenia = reader.GetDateTime("datum_vytvorenia"),
                UcastnikId = reader.GetString("ucastnik"),
                MiestoZaciatkuId = reader.GetInt32("miesto_zaciatku"),
                MiestoKoncaId = reader.GetInt32("miesto_konca"),
                DatumCasZaciatku = reader.GetDateTime("datum_cas_zaciatku"),
                DatumCasKonca = reader.GetDateTime("datum_cas_konca"),
                StavId = reader.GetInt32("stav_id"),
                // Add other fields as needed
            };
            //states are constants
            cestovnyPrikaz.Stav = Stav.GetAll().Single(s => s.StavId == cestovnyPrikaz.StavId);

            // Fields from joined tables
            cestovnyPrikaz.Ucastnik = new Zamestnanec {
                OsobneCislo = cestovnyPrikaz.UcastnikId,
                KrstneMeno = reader.GetString("krstne_meno"),
                Priezvisko = reader.GetString("priezvisko"),
                DatumNarodenia = reader.GetDateTime("datum_narodenia"),
                RodneCislo = reader.GetString("rodne_cislo")
            };

            cestovnyPrikaz.MiestoZaciatkuMesto = new Mesto {
                MestoId = cestovnyPrikaz.MiestoZaciatkuId,
                NazovMesta = reader.GetString("mz_nazov_mesta"),
                Stat = reader.GetString("mz_stat"),
                ZemepisnaDlzka = reader.GetDecimal("mz_zemepisna_dlzka"),
                ZemepisnaSirka = reader.GetDecimal("mz_zemepisna_sirka"),
            };

            cestovnyPrikaz.MiestoKoncaMesto = new Mesto {
                MestoId = cestovnyPrikaz.MiestoKoncaId,
                NazovMesta = reader.GetString("mk_nazov_mesta"),
                Stat = reader.GetString("mk_stat"),
                ZemepisnaDlzka = reader.GetDecimal("mk_zemepisna_dlzka"),
                ZemepisnaSirka = reader.GetDecimal("mk_zemepisna_sirka"),
            };
            return cestovnyPrikaz;
        }
    }
}

