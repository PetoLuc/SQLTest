using HR.Dal.MsSql.Repos.Contracts;
using HR.Dal.Repos.Contracts;
using HR.Dol;
using System.Net;

namespace HR.Dal.MsSql.Services
{
    public class CestovnyVykazService(ICestovnyPrikazRepository cestovnyPrikazRepository, IDopravaRepository dopravaRepository)
    {
        private readonly ICestovnyPrikazRepository _cestovnyPrikazRepository = cestovnyPrikazRepository;
        private readonly IDopravaRepository _dopravaRepository= dopravaRepository;

        public async Task AddCestovnyPrikazAsync(CestovnyPrikaz cestovnyPrikaz, List<Doprava> dopravaList)
        {
            
            

        }
        

    }
}
