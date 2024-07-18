using HR.Dol;

namespace HR.Dal.MsSql.Repos.Contracts
{
    public interface IZamestnanecRepository
    {
        Task<List<Zamestnanec>> GetAllZamestnanciAsync();
    }
}