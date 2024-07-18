using HR.Dal.Services.Contracts;
using HR.Dol.Contracts;
using System.Data.SqlClient;

namespace HR.Dal.Repos
{
    public class RepositoryBase(IConnectionStringProviderService connectionStirngProvider)
    {
        protected readonly string _connectionString = connectionStirngProvider.ConnectionString;

        /// <summary>
        /// Reads records from the database using the specified SQL command and maps each record to an object of type T using the provided mapper function.
        /// </summary>
        /// <typeparam name="T">The type of objects to be returned in the list.</typeparam>
        /// <param name="command">The SQL command to execute.</param>
        /// <param name="mapper">A function that maps a SqlDataReader to an object of type T.</param>
        /// <returns>A list of objects of type T representing the records read from the database.</returns>
        public async Task<List<T>> ReadRecordsAsync<T>(SqlCommand command, Func<SqlDataReader, T> mapper) where T: class, IEntityMarker
        {
            List<T> resultList = [];
            using (SqlConnection connection = new(_connectionString))
            {
                await connection.OpenAsync();
                command.Connection = connection;

                using SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    resultList.Add(mapper(reader));
                }
            }
            return resultList;
        }

        public async Task ExecuteInTransactionAsync(Func<SqlConnection, SqlTransaction, Task> actionAsync)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();
            using var transaction = await connection.BeginTransactionAsync();
            try
            {
                if (actionAsync != null)
                {
                    await actionAsync(connection, transaction as SqlTransaction); // Pass connection and transaction to the action
                }
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(); 
                throw; 
            }
        }
    }
}
