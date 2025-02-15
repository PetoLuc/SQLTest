﻿using HR.Dal.MsSql.Repos.Contracts;
using HR.Dal.Services.Contracts;
using HR.Dol;
using System.Data;
using System.Data.SqlClient;

namespace HR.Dal.Repos
{
    public class DopravaRepository(IConnectionStringProviderService connectionStringProvider) : RepositoryBase(connectionStringProvider), IDopravaRepository
    {
        public async Task<List<Doprava>> GetDopravaByCestovnyPrikazIdAsync(int cpId)
        {
            string query = "SELECT doprava_id, cp_id, doprava_typ_id FROM Doprava WHERE cp_id = @CpId";
            using SqlCommand command = new(query);
            command.Parameters.AddWithValue("@CpId", cpId);
            return await ReadRecordsAsync(command, MapReaderToDoprava);
        }
        public async Task DeleteDopravaAsync(int cpId, SqlConnection connection, SqlTransaction transaction)
        {
            using SqlCommand deleteDopravaCommand = new("DELETE Doprava WHERE cp_id = @cpId", connection, transaction);
            deleteDopravaCommand.Parameters.AddWithValue("@cpId", cpId);
            await deleteDopravaCommand.ExecuteNonQueryAsync();
        }
        public void AppendDeleteForCestovnyPrikaz(SqlCommand cestovnyPrikazCommand) => cestovnyPrikazCommand.CommandText += $"{Environment.NewLine}DELETE Doprava WHERE cp_id = @cpId";

        public async Task InsertDopravaForCestovnyPrikazAsync(int cestovnyPrikazId, SqlConnection connection, SqlTransaction transaction, List<DopravaTyp> dopravaTypList)
        {
            if (dopravaTypList.Count == 0)
            {
                return;
            }
            List<string> values = [];

            using SqlCommand sqlCommandDoprava = new();
            sqlCommandDoprava.Connection = connection;
            sqlCommandDoprava.Transaction = transaction;
            sqlCommandDoprava.Parameters.AddWithValue("@cpId", cestovnyPrikazId);

            for (int i = 0; i < dopravaTypList.Count; i++)
            {
                string paramName = $"@dopravaTypId{i + 1}";
                values.Add($"(@cpId,{paramName})");
                sqlCommandDoprava.Parameters.AddWithValue(paramName, dopravaTypList[i].DopravaTypId);
            }
            sqlCommandDoprava.CommandText = $"INSERT INTO Doprava (cp_id, doprava_typ_id) VALUES {string.Join(",", values)}";
            await sqlCommandDoprava.ExecuteNonQueryAsync();
        }

        public void AppendInsertForCestovnyPrikaz(SqlCommand cestovnyPrikazCommand, List<DopravaTyp> dopravaTypList)
        {
            if (dopravaTypList.Count == 0)
            {
                return;
            }
            List<string> values = [];

            for (int i = 0; i < dopravaTypList.Count; i++)
            {
                string paramName = $"@dopravaTypId{i + 1}";
                values.Add($"(@cpId,{paramName})");
                cestovnyPrikazCommand.Parameters.AddWithValue(paramName, dopravaTypList[i].DopravaTypId);
            }
            cestovnyPrikazCommand.CommandText += $"{Environment.NewLine}INSERT INTO Doprava (cp_id, doprava_typ_id) VALUES {string.Join(",", values)}";
        }
        private static Doprava MapReaderToDoprava(SqlDataReader reader) => new Doprava
        {
            DopravaId = reader.GetInt32("doprava_id"),
            CpId = reader.GetInt32("cp_id"),
            DopravaTypId = reader.GetInt32("doprava_typ_id"),
        };
    }
}
