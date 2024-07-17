using HR.Dal.Contracts;
using HR.Dal.Repos;
using HR.Dol;
using HR.Web.Blazor.Model.Filters;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HR.Web.Blazor.ViewModel {
    public class HomeViewModel: INotifyPropertyChanging {

        public string? searchTerm;
        public string? SearchTerm {
            get => searchTerm;
            set {
                searchTerm = value;
                //OnPropertyChanged(nameof(SearchTerm));
            }
        }

        private readonly ICestovnyPrikazRepository _cestovnyPrikazRepository;

        public UcastnikFilter UcastnikFilter = new();

        public HomeViewModel(ICestovnyPrikazRepository cestovnyPrikazRepository) {
            _cestovnyPrikazRepository = cestovnyPrikazRepository;
            this.UcastnikFilter.PropertyChanged += UcastnikFilter_PropertyChanged;
        }                

        public List<CestovnyPrikaz> CestovnePrikazy { get; set; } = [];

        public event PropertyChangingEventHandler? PropertyChanging;

        public async Task LoadCestovnePrikazyAsync() {

            CestovnePrikazy = await _cestovnyPrikazRepository.GetAsync();
        }

        private void UcastnikFilter_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
            //loadAsync
        }
    }
}
