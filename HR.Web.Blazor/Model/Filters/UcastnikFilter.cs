using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HR.Web.Blazor.Model.Filters {
    /// <summary>
    /// for home page filtering purposes 
    /// </summary>
    public class UcastnikFilter: INotifyPropertyChanged {

        public event PropertyChangedEventHandler? PropertyChanged;

        public string? searchTerm;
        public string? SearchTerm {
            get => searchTerm;
            set 
            {
                searchTerm = value;
                OnPropertyChanged(nameof(SearchTerm));
            }
        }        

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
