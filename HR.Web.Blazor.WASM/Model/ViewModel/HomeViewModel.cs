using HR.Dal.Contracts;
using HR.Dol;
using HR.Web.Blazor.WASM.Model.Filter;
using System.ComponentModel;

namespace HR.Web.Blazor.WASM.Model.ViewModel
{
    public class HomeViewModel
    {
        private readonly ICestovnyPrikazRepository _cestovnyPrikazRepository;

        public UcastnikFilter UcastnikFilter = new();

        public HomeViewModel(ICestovnyPrikazRepository cestovnyPrikazRepository)
        {
            _cestovnyPrikazRepository = cestovnyPrikazRepository;            
        }

        public List<CestovnyPrikaz> CestovnePrikazy { get; set; } = [];

        public event PropertyChangingEventHandler? PropertyChanging;

        public async Task LoadCestovnePrikazyAsync()
        {

            CestovnePrikazy = await _cestovnyPrikazRepository.GetAsync();
        }
    }
}

