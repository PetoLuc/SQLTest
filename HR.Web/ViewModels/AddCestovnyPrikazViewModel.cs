using HR.Dol;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HR.Web.ViewModels {
    public class AddCestovnyPrikazViewModel {
        
        public DateTime DatumCasZaciatku { get; set; }        
        public DateTime DatumCasKonca { get; set; }
        public Mesto? MiestoZaciatkuMesto { get; set; }
        public Mesto? MiestoKoncaMesto { get; set; }
        public Stav Stav { get; set; } = Stav.Vytvoreny;
        [BindProperty]
        public List<DopravaTypViewModel> DopravaList { get; set; } = [];
    }
}
