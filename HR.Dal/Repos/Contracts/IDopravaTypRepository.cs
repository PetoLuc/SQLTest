using HR.Dol;

namespace HR.Dal.Repos.Contracts
{
    public interface IDopravaTypRepository
    {
        List<DopravaTyp> GetAll();
        DopravaTyp FindById(int id);
    }
}