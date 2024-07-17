using HR.Dol;

namespace HR.Dal.Contracts
{
    public interface ICestovnyPrikazRepository
    {
        Task<List<CestovnyPrikaz>> GetAsync(); 
    }
}