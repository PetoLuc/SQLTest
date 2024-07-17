using HR.Dol;

namespace HR.Dal.Repos.Contracts
{
    public interface IDopravaRepository
    {
        List<DopravaTyp> GetAll();
    }
}