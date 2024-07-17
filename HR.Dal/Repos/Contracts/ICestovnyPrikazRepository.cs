using HR.Dol;

namespace HR.Dal.Repos.Contracts
{
    public interface ICestovnyPrikazRepository
    {
        Task<List<CestovnyPrikaz>> GetAsync(string? employeeFilter = null);
    }
}