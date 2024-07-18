using HR.Dal.Repos.Contracts;
using HR.Dol;
using Microsoft.AspNetCore.Mvc;

namespace HR.Web.Blazor.ViewModel
{
    public class HomeViewModel(ICestovnyPrikazRepository cestovnyPrikazRepository)
    {
        private readonly ICestovnyPrikazRepository _cestovnyPrikazRepository = cestovnyPrikazRepository;

        [BindProperty]
        public string? SearchTerm { get; set; }
        public List<CestovnyPrikaz> CestovnePrikazy { get; set; } = [];

        public async Task InitAsync()
        {
            await LoadCestovnePrikazyAsync();
        }

        public async Task LoadCestovnePrikazyAsync() {
            CestovnePrikazy = await _cestovnyPrikazRepository.GetFilteredAsync(SearchTerm);
        }

        public async Task DeleteCestovnyPrikazAsync(int cestovnyPrikazId)
        {
            await _cestovnyPrikazRepository.DeleteAsync(cestovnyPrikazId);            
            await InitAsync();
        }
    }
}
