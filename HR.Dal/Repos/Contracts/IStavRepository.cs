using HR.Dol;

namespace HR.Dal.MsSql.Repos.Contracts
{
    public interface IStavRepository
    {
        List<Stav> GetAll();
    }
}