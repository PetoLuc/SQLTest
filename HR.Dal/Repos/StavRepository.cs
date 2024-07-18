using HR.Dal.MsSql.Repos.Contracts;
using HR.Dol;

namespace HR.Dal.MsSql.Repos
{
    public class StavRepository : IStavRepository
    {
        public List<Stav> GetAll() => Stav.GetAll().ToList();
    }
}
