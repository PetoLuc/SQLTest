using HR.Dol;

namespace HR.Dal.Repos.Contracts
{
    public interface ICestovnyPrikazRepository
    {
        /// <summary>
        /// filter cestovny prikaz for fields osobne_cislo,krstne_meno,priezvisko,rodne_cislo
        /// </summary>
        /// <param name="employeeFilter">filter text max 50 chars</param>
        /// <returns>list of filtered enities</returns>
        Task<List<CestovnyPrikaz>> GetFilteredAsync(string? employeeFilter = null);

         /// <summary>
         /// 
         /// </summary>
         /// <param name="cestovnyPrikazId"></param>
         /// <returns></returns>
        Task<CestovnyPrikaz?> GetByIdAsync(int cestovnyPrikazId);
        Task InsertAsync(CestovnyPrikaz cestovnyPrikaz, List<DopravaTyp> dopravaTypList);
        Task UpdateAsync(CestovnyPrikaz cestovnyPrikaz, List<DopravaTyp> dopravaTypList);
        Task DeleteAsync(int cestovnyPrikazId);
    }
}