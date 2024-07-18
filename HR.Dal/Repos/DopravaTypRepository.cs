using HR.Dal.Repos.Contracts;
using HR.Dol;

namespace HR.Dal.Repos
{
    public class DopravaTypRepository : IDopravaTypRepository
    {
        public List<DopravaTyp> GetAll() => DopravaTyp.GetAll().ToList();
    }
}
