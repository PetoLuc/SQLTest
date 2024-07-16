using HR.Dol;

namespace HR.Dal.Contracts
{
    public interface ICestovnyPrikazRepository
    {
        Task AddAsync(CestovnyPrikaz cestovnyPrikaz);
        Task DeleteAsync(int id);
        Task<List<CestovnyPrikaz>> GetAsync();
        Task UpdateAsync(CestovnyPrikaz cestovnyPrikaz);
    }
}