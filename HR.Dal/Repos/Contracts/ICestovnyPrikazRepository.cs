using HR.Dol;

namespace HR.Dal.Repos.Contracts
{
    public interface ICestovnyPrikazRepository
    {
        Task<List<CestovnyPrikaz>> GetFilteredAsync(string? employeeFilter = null);
        Task<List<CestovnyPrikaz>> GetByIdAsync(int cestovnyPrikazId);
        Task InsertAsync(CestovnyPrikaz cestovnyPrikaz, List<DopravaTyp> dopravaTypList);
        Task DeleteAsync(int cestovnyPrikazId);
    }
}