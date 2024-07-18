using HR.Dol;

namespace HR.Dal.Repos.Contracts
{
    public interface IMestoRepository
    {
        Task<List<Mesto>> GetAllMestoForSelectAsync();
    }
}