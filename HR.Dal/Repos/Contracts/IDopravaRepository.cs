using HR.Dol;
using System.Data.SqlClient;

namespace HR.Dal.MsSql.Repos.Contracts
{
    public interface IDopravaRepository
    {
        Task<List<Doprava>> GetDopravaByCestovnyPrikazIdAsync(int cpId);
        Task DeleteDopravaAsync(int cpId, SqlConnection connection, SqlTransaction transaction);
        Task InsertDopravaForCestovnyPrikazAsync(int cestovnyPrikazId, SqlConnection connection, SqlTransaction transaction, List<DopravaTyp> dopravaTypList);
        void AppendDeleteForCestovnyPrikaz(SqlCommand cestovnyPrikazCommand);
        void AppendInsertForCestovnyPrikaz(SqlCommand cestovnyPrikazCommand, List<DopravaTyp> dopravaTypList);
    }
}