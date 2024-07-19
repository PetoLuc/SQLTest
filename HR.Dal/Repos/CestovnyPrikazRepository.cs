using HR.Dal.MsSql.Repos.Contracts;
using HR.Dal.Repos.Contracts;
using HR.Dal.Services.Contracts;
using HR.Dol;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HR.Dal.Repos
{
    
    public class CestovnyPrikazRepository(IConnectionStringProviderService connectionStringProvider, IDopravaRepository dopravaRepository)
        : RepositoryBase(connectionStringProvider), ICestovnyPrikazRepository
    {

        private const string selectQueryBase = @"SELECT cp.*, mz.nazov_mesta AS mz_nazov_mesta, mz.stat AS mz_stat, mz.zemepisna_sirka AS mz_zemepisna_sirka, mz.zemepisna_dlzka AS mz_zemepisna_dlzka,
                                mk.nazov_mesta AS mk_nazov_mesta, mk.stat AS mk_stat, mk.zemepisna_sirka AS mk_zemepisna_sirka, mk.zemepisna_dlzka AS mk_zemepisna_dlzka,                                
                                z.krstne_meno, z.priezvisko, z.datum_narodenia, z.rodne_cislo
                             FROM CestovnyPrikaz cp
                             JOIN Mesto mz ON cp.miesto_zaciatku = mz.mesto_id
                             JOIN Mesto mk ON cp.miesto_konca = mk.mesto_id
                             JOIN Stav st ON cp.stav_id = st.stav_id
                             JOIN Zamestnanec z ON cp.ucastnik = z.osobne_cislo";

        /// <summary>
        /// filter cestovny prikaz for fields osobne_cislo,krstne_meno,priezvisko,rodne_cislo
        /// </summary>
        /// <param name="employeeFilter">filter text max 50 chars</param>
        /// <returns>list of filtered enities</returns>
        public async Task<List<CestovnyPrikaz>> GetFilteredAsync(string? employeeFilter = null)
        {
            var sanitizedFilter = string.IsNullOrWhiteSpace(employeeFilter) ? null : $"\"*{employeeFilter.Replace("'", "''")}*\"";


            string query = selectQueryBase;
            if (sanitizedFilter != null)
            {
                //fulltext search and like search sample
                //TODO pouzite oboch je nezmysel, pri realnej implementacii je potrebne si vybrat
                query = @$"{query} WHERE CONTAINS((z.osobne_cislo, z.krstne_meno, z.priezvisko, z.rodne_cislo), @filter)
                        OR z.osobne_cislo LIKE @likeSearch 
                        OR z.krstne_meno LIKE @likeSearch 
                        OR z.priezvisko LIKE @likeSearch 
                        OR z.rodne_cislo LIKE @likeSearch";
            }

            var command = new SqlCommand(query);

            //add sanitized parameter value
            if (sanitizedFilter != null)
            {
                //full text
                command.Parameters.AddWithValue("@filter", sanitizedFilter);

                //like
                string likeSearchFilter = $"%{employeeFilter}%";
                command.Parameters.AddWithValue("@likeSearch", likeSearchFilter);
            }

            return await ReadRecordsAsync(command, MapReaderToCestovnyPrikaz);
        }


        public async Task<CestovnyPrikaz?> GetByIdAsync(int cestovnyPrikazId)
        {
            var query = @$"{selectQueryBase} WHERE cp.cp_id= @cpId";                                  
            var getByIdCommand = new SqlCommand(query);
            getByIdCommand.Parameters.AddWithValue("@cpId", cestovnyPrikazId);
            var cestovnyPrikazList = await ReadRecordsAsync(getByIdCommand, MapReaderToCestovnyPrikaz);
            return cestovnyPrikazList.FirstOrDefault();
        }



        public async Task DeleteAsync(int cestovnyPrikazId)
        {
            if (cestovnyPrikazId <= 0)
                return;

            await ExecuteInTransactionAsync(async (connection, transaction) =>
            {
                //delete doprava
                await dopravaRepository.DeleteDopravaAsync(cestovnyPrikazId, connection, transaction);

                //command to insert into CestovnyPrikaz and retrieve the ID
                using SqlCommand deleteCestovnyPrikazCommand = new("DELETE CestovnyPrikaz WHERE cp_id = @cpId", connection, transaction);
                deleteCestovnyPrikazCommand.Parameters.AddWithValue("@cpId", cestovnyPrikazId);
                await deleteCestovnyPrikazCommand.ExecuteNonQueryAsync();
            });
        }
            
        public async Task InsertAsync(CestovnyPrikaz cestovnyPrikaz, List<DopravaTyp> dopravaTypList)
        {
            ArgumentNullException.ThrowIfNull(cestovnyPrikaz);
            ArgumentNullException.ThrowIfNull(dopravaTypList);
            cestovnyPrikaz.DatumVytvorenia = DateTime.UtcNow; //TODO create datetime service

            int? cestovnyPrikazId = null;

            await ExecuteInTransactionAsync(async (connection, transaction) =>
            {
                // Command to insert into CestovnyPrikaz and retrieve the ID
                //TODO see command optimilazation in update
                using (SqlCommand addCestovnyPrikazCommand = new("INSERT INTO CestovnyPrikaz (datum_vytvorenia, ucastnik, miesto_zaciatku, miesto_konca, datum_cas_zaciatku, datum_cas_konca, stav_id) " +
                                                             "OUTPUT INSERTED.cp_id " +
                                                             "VALUES (GETDATE(), @ucastnik, @miestoZaciatku, @miestoKonca, @datumCasZaciatku, @datumCasKonca, @stavId);", connection, transaction))
                {
                    addCestovnyPrikazCommand.Parameters.AddWithValue("@ucastnik", cestovnyPrikaz.UcastnikId);
                    addCestovnyPrikazCommand.Parameters.AddWithValue("@miestoZaciatku", cestovnyPrikaz.MiestoZaciatkuId);
                    addCestovnyPrikazCommand.Parameters.AddWithValue("@miestoKonca", cestovnyPrikaz.MiestoKoncaId);
                    addCestovnyPrikazCommand.Parameters.AddWithValue("@datumCasZaciatku", cestovnyPrikaz.DatumCasZaciatku);
                    addCestovnyPrikazCommand.Parameters.AddWithValue("@datumCasKonca", cestovnyPrikaz.DatumCasZaciatku);
                    addCestovnyPrikazCommand.Parameters.AddWithValue("@stavId", cestovnyPrikaz.StavId);
                    cestovnyPrikazId = (int?)await addCestovnyPrikazCommand.ExecuteScalarAsync();
                }
                //cestovnyPrikaz insert failed
                if (cestovnyPrikazId == null)
                {
                    await transaction.RollbackAsync();
                    return;
                }
                // Insert each Doprava entry from the list            
                // Command to insert into Doprava
                await dopravaRepository.InsertDopravaForCestovnyPrikazAsync(cestovnyPrikazId.Value, connection, transaction, dopravaTypList);
            });
        }

        public async Task UpdateAsync(CestovnyPrikaz cestovnyPrikaz, List<DopravaTyp> dopravaTypList)
        {
            ArgumentNullException.ThrowIfNull(cestovnyPrikaz);
            ArgumentNullException.ThrowIfNull(dopravaTypList);

            if (cestovnyPrikaz.CpId <= 0)
                return;

            await ExecuteInTransactionAsync(async (connection, transaction) =>
            {
                // Command to insert into CestovnyPrikaz and retrieve the ID
                //oprimized only one DB call for all operations 
                using SqlCommand addCestovnyPrikazCommand = new(@"UPDATE CestovnyPrikaz 
                                                                    SET ucastnik = @ucastnik, 
                                                                    miesto_zaciatku = @miestoZaciatku,
                                                                    miesto_konca = @miestoKonca, 
                                                                    datum_cas_zaciatku = @datumCasZaciatku, 
                                                                    datum_cas_konca = @datumCasKonca, 
                                                                    stav_id =  @stavId
                                                                  WHERE cp_id = @cpId", connection, transaction);
                addCestovnyPrikazCommand.Parameters.AddWithValue("@cpId", cestovnyPrikaz.CpId);
                addCestovnyPrikazCommand.Parameters.AddWithValue("@ucastnik", cestovnyPrikaz.UcastnikId);
                addCestovnyPrikazCommand.Parameters.AddWithValue("@miestoZaciatku", cestovnyPrikaz.MiestoZaciatkuId);
                addCestovnyPrikazCommand.Parameters.AddWithValue("@miestoKonca", cestovnyPrikaz.MiestoKoncaId);
                addCestovnyPrikazCommand.Parameters.AddWithValue("@datumCasZaciatku", cestovnyPrikaz.DatumCasZaciatku);
                addCestovnyPrikazCommand.Parameters.AddWithValue("@datumCasKonca", cestovnyPrikaz.DatumCasZaciatku);
                addCestovnyPrikazCommand.Parameters.AddWithValue("@stavId", cestovnyPrikaz.StavId);

                dopravaRepository.AppendDeleteForCestovnyPrikaz(addCestovnyPrikazCommand);
                dopravaRepository.AppendInsertForCestovnyPrikaz(addCestovnyPrikazCommand, dopravaTypList);
                await addCestovnyPrikazCommand.ExecuteNonQueryAsync();
            });
        }



        private static CestovnyPrikaz MapReaderToCestovnyPrikaz(SqlDataReader reader)
        {
            var cestovnyPrikaz = new CestovnyPrikaz
            {
                // Basic fields from CestovnyPrikaz
                CpId = reader.GetInt32("cp_id"),
                DatumVytvorenia = reader.GetDateTime("datum_vytvorenia"),
                UcastnikId = reader.GetString("ucastnik"),
                MiestoZaciatkuId = reader.GetInt32("miesto_zaciatku"),
                MiestoKoncaId = reader.GetInt32("miesto_konca"),
                DatumCasZaciatku = reader.GetDateTime("datum_cas_zaciatku"),
                DatumCasKonca = reader.GetDateTime("datum_cas_konca"),
                StavId = reader.GetInt32("stav_id"),                
            };
            //states are constants
            cestovnyPrikaz.Stav = Stav.GetAll().Single(s => s.StavId == cestovnyPrikaz.StavId);

            // Fields from joined tables
            cestovnyPrikaz.Ucastnik = new Zamestnanec
            {
                OsobneCislo = cestovnyPrikaz.UcastnikId,
                KrstneMeno = reader.GetString("krstne_meno"),
                Priezvisko = reader.GetString("priezvisko"),
                DatumNarodenia = reader.GetDateTime("datum_narodenia"),
                RodneCislo = reader.GetString("rodne_cislo")
            };

            cestovnyPrikaz.MiestoZaciatkuMesto = new Mesto
            {
                MestoId = cestovnyPrikaz.MiestoZaciatkuId,
                NazovMesta = reader.GetString("mz_nazov_mesta"),
                Stat = reader.GetString("mz_stat"),
                ZemepisnaDlzka = reader.GetDecimal("mz_zemepisna_dlzka"),
                ZemepisnaSirka = reader.GetDecimal("mz_zemepisna_sirka"),
            };

            cestovnyPrikaz.MiestoKoncaMesto = new Mesto
            {
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

