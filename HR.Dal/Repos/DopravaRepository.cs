using HR.Dal.Repos.Contracts;
using HR.Dol;

namespace HR.Dal.Repos
{
    public class DopravaRepository : IDopravaRepository
    {
        public List<DopravaTyp> GetAll() => DopravaTyp.GetAll().ToList();
    }
}
